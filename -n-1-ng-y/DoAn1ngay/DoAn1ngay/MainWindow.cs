using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using NModbus;
using NModbus.Serial;
using DoAn1ngay.DAO;
using System.Threading;
using System.IO;
using DoAn1ngay.DTO;
using System.Text.RegularExpressions;

namespace DoAn1ngay
{
    public partial class MainWindow : Form
    {
        #region register        
       
        IModbusMaster master;
        StringBuilder data = new StringBuilder();
        StringBuilder chekc1 = new StringBuilder();

        string Reconnect;

        SerialPort serialPort1 = new SerialPort();

        private bool Isconnected = false;

        private static System.Timers.Timer timer2;

        private static System.Timers.Timer timer3;


        List<string> DataXuatKhoTuanTu = new List<string>();
        KhoHangToByte khoHangToByte = new KhoHangToByte();
        List<DoAn1ngay.DTO.Storage> storagesList_previous = new List<Storage>();

        bool k;
        int _k=0,i=0, flag1;
        string diachi, onho;

        
        #endregion

        #region Method

        public MainWindow()
        {
            InitializeComponent();             
            
        }        

        void query()
        {
            string query1 = "SELECT * FROM THONGTINKHO";
            dataGridView1.DataSource= Dataprovider.Instance.ExecuteQuery(query1);
        }
        void Toggle_pin()
        {
            double a = Convert.ToDouble(thoigiannhapnhay.Text) / Convert.ToDouble(solannhapnhay.Text);

            timer2 = new System.Timers.Timer(Convert.ToInt32(a*1000));
            timer2.Elapsed += Timer2_Elapsed;
            timer2.AutoReset = true;
            timer2.Enabled = true;
        }

        void update_station()
        {
            timer3 = new System.Timers.Timer(2000);
            timer3.Elapsed += Timer3_Elapsed;
            timer3.AutoReset = true;
            timer3.Enabled = true;

        }        

        void LoadStorage() 
        {
            this.Invoke((MethodInvoker)delegate
            {
                flpStorage.Controls.Clear();
                List<DoAn1ngay.DTO.Storage> storagesList = DoAn1ngay.DAO.StorageDAO.Instance.LoadTableList();


                foreach (Storage item in storagesList)
                {

                    Button btn = new Button() { Width = StorageDAO.TableWidth, Height = StorageDAO.TableHeight };
                    btn.Text = item.StorageName + Environment.NewLine + item.Status;
                    btn.Click += Btn_Click;
                    btn.Tag = item;

                    switch (item.Status)
                    {
                        case "Đã kết nối":
                            btn.BackColor = Color.LightGreen;
                            break;
                        default:
                            btn.BackColor = Color.IndianRed;
                            break;
                    }
                    flpStorage.Controls.Add(btn);
                }
            });
            
        }

        void Load_Storage_New()
        {            
            bool compare;

            List<DoAn1ngay.DTO.Storage> storageList = DoAn1ngay.DAO.StorageDAO.Instance.LoadTableList();

            compare = DoAn1ngay.Timer.find.CompareList(storageList, storagesList_previous);

            if(compare == false)
            {
                LoadStorage();
            }            
           
            storagesList_previous = storageList;

        }

        void ShowStorage(int id)
        {
            progressBar1.Value = 0;
            lsvStorage.Items.Clear();
            List<StorageInfo> storagesInfo = StorageInfoDAO.Instance.GetListStorageInfoById(id);
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 45;
            foreach(StorageInfo item in storagesInfo) 
            {
                ListViewItem lsvItem = new ListViewItem(item.Id.ToString());
                lsvItem.SubItems.Add(item.StackName.ToString());
                lsvItem.SubItems.Add(item.StorageName.ToString());
                lsvStorage.Items.Add(lsvItem);
                progressBar1.Value++;
            }
        }

        public async void new_opencomport(byte i)
        {
            bool[] check;
            master.Transport.Retries = 1;
            try
            {
                check = await master.ReadCoilsAsync(i, 0, 1); 
                
                StorageInfoDAO.Instance.Update_Storage(i, "Đã kết nối");
                flag1++;
                    
            }
            catch (Exception)
            {                    
                StorageInfoDAO.Instance.Update_Storage(i, "Chưa kết nối");
                flag1++;    
            } 
            if(flag1==30)
            {
                Load_Storage_New();
                flag1 = 0;
            }
        }

        public void Check_Connection()
        {
            for (int i = 1; i <= 30; i++)
            {                
                new_opencomport(Convert.ToByte(i));
            }
        }

        private void Opencomport()
        {
            
            serialPort1.PortName = cbx_ID.Text;
            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Open();
            serialPort1.ReadTimeout = 1;
            serialPort1.WriteTimeout = 1;

            var factory = new ModbusFactory();
            master = factory.CreateRtuMaster(serialPort1);            
        }

        private void OpencomportWhenDisconnect()
        {
            try
            {                
                serialPort1.PortName = Reconnect;
                serialPort1.BaudRate = 9600;
                serialPort1.DataBits = 8;
                serialPort1.Parity = Parity.None;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Open();
                serialPort1.ReadTimeout = 50;
                serialPort1.WriteTimeout = 50;

                var factory = new ModbusFactory();
                master = factory.CreateRtuMaster(serialPort1);
            }
            catch
            {
                //lỗi đường truyền
            }
        }

        private void transmit(byte slaveId, ushort startAddress, ushort[] numRegister)
        {
            try
            {
                master.WriteMultipleRegisters(slaveId, startAddress, numRegister);
            }
            catch (Exception)
            {
                
            }
        }

        private void receive(byte slaveId, ushort startAddress, ushort numRegisters)
        {
            try
            {
                ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);
                data.Clear();
                for (int i = 0; i < numRegisters; i++)
                {
                    data.Append($"Register {startAddress + i}={registers[i]}" + "\r\n");
                }                
                // hiển thị thông tin truyền nhận
            }
            catch
            {
                // lỗi đường truyền
                Isconnected = false;
            }
        }
             
        private void Xuat_Kho()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }
                    
                    string _data = Convert.ToString(fileContent);
                    _data = _data.Replace("\r", "");
                    _data = _data.Replace("[]", "");
                    _data = _data.Trim();
                    Regex trimer = new Regex(@"\s\s+");
                    _data = trimer.Replace(_data, " ");

                    List<string> DataXuatKho = new List<string>();
                    List<string>[] __DataXuatKho = new List<string>[30];
                    List<string[]> aa = new List<string[]>();

                    string[] tach = _data.Split('\n');

                    foreach (string item in tach)
                    {
                        DataXuatKho.Add(item);
                    }

                    DataXuatKho.Sort();
                    for (int i = 0; i < 30; i++)
                    {
                        __DataXuatKho[i] = new List<string>();
                        foreach (string item in DataXuatKho)
                        {
                            if (Convert.ToInt32(item.Substring(1, 2)) == i + 1) __DataXuatKho[i].Add(item.Substring(3,2));

                        }

                    }

                    KhoHangToByte khoHangToByte = new KhoHangToByte();
                    
                    for(int i = 0; i < 30; i++)
                    {
                        if (__DataXuatKho[i].Count > 0)
                        {
                            khoHangToByte.MaHoa(__DataXuatKho[i].ToArray());
                            
                        }
                        transmit(Convert.ToByte(i + 1), 0, khoHangToByte.a);
                        Array.Clear(khoHangToByte.a, 0, khoHangToByte.a.Length);
                    }
                    

                    if (MessageBox.Show("Hoàn tất hiển thị?", "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        for(int i = 0; i < 30; i++)
                        {
                            transmit(Convert.ToByte(i + 1), 0, khoHangToByte.a);
                        }
                        MessageBox.Show("ok");
                    }
                }
            }
        }

        private void Xuat_Kho_Tuan_Tu()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    string _data = Convert.ToString(fileContent);
                    _data = _data.Replace("\r", "");
                    _data = _data.Replace("[]", "");
                    _data = _data.Trim();
                    Regex trimer = new Regex(@"\s\s+");
                    _data = trimer.Replace(_data, " ");
                                                            

                    string[] tach = _data.Split('\n');
                    
                    foreach (string item in tach)
                    {
                        DataXuatKhoTuanTu.Add(item);
                    }

                    DataXuatKhoTuanTu.Sort();
                    Toggle_pin();
                    

                    
                }
            }
        }
        private void Nhap_Kho()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();
                    }

                    string data = Convert.ToString(fileContent);
                    data = data.Replace("\r", "");
                    string[] _data = data.Split('\n');

                    for (int i = 0; i < _data.Length; i++)
                    {
                        string Station = _data[i].Substring(0, 5);
                        string Barcode = _data[i].Substring(7);
                        string query1 = "UPDATE dbo.THONGTINKHO set StackName=N'" + Barcode.ToString() + "'where idKho = '" + Station.ToString() + "'";
                        Dataprovider.Instance.ExecuteNonQuery(query1);
                    }
                }
            }
        }

        #endregion

        #region events
        private void MainWindow_Load(object sender, EventArgs e)
        {
            cbx_TuanTu.Checked = false;
            groupBox1.Enabled = false;
            query();
            Control.CheckForIllegalCrossThreadCalls = false;
            cbx_ID.Items.AddRange(SerialPort.GetPortNames());
        }
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                Opencomport();
                Reconnect = cbx_ID.Text;
                Isconnected = true;                
                Check_Connection();
                update_station();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                Isconnected = false;
            }
        }

        private void Timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {

                if (_k == (Convert.ToInt32(solannhapnhay.Text) * 2))
                {
                    i++;
                    if (i >= DataXuatKhoTuanTu.Count)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            btnXuatkho.BackColor = Color.Transparent;
                            i = 0;
                            _k = 0;
                            k = true;
                            
                            transmit(Convert.ToByte(diachi), 0, khoHangToByte.b);
                            diachi = null;
                            onho = null;
                            DataXuatKhoTuanTu.Clear();
                            Array.Clear(khoHangToByte.a, 0, khoHangToByte.a.Length);
                            timer2.Stop();      

                        });
                        
                    }
                    else
                    {
                        Array.Clear(khoHangToByte.a, 0, khoHangToByte.a.Length);
                        _k = 0;
                    }
                }

                
                if (timer2.Enabled == true)
                {
                    if (_k == 0)
                    {
                        diachi = DataXuatKhoTuanTu[i].Substring(1, 2);
                        onho = DataXuatKhoTuanTu[i].Substring(3, 2);
                        khoHangToByte.MaHoa_TuanTu(onho);

                    }
                    if (k == true)
                    {
                        transmit(Convert.ToByte(diachi), 0, khoHangToByte.a);
                        this.Invoke((MethodInvoker)delegate
                        {
                            btnXuatkho.BackColor = Color.Red;
                        });

                    }
                    else
                    {
                        transmit(Convert.ToByte(diachi), 0, khoHangToByte.b);

                        this.Invoke((MethodInvoker)delegate
                        {
                            btnXuatkho.BackColor = Color.Green;
                        });
                    }
                    k = !k;
                    _k++;
                }
            });


        }
        private void Timer3_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Check_Connection();

            Load_Storage_New();
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            serialPort1.Close();                        
        }
        private void btn_Transmit_Click(object sender, EventArgs e)
        {
            // hàm này dùng để truyền tay dữ liệu

            //byte a = Convert.ToByte(Txt_SlaveId.Text);
            //ushort addr = Convert.ToUInt16(txtAddress.Text);
            //string ox = Convert.ToString(txtData.Text);
            //ox = ox.Replace(" ", "");
            //string[] mx = ox.Split(';');
            //ushort[] bbb = new ushort[mx.Length];
            //for (int i = 0; i < mx.Length; i++)
            //{
            //    bbb[i] = Convert.ToUInt16(mx[i]);
            //}
            //transmit(a, addr, bbb);
        }
        private void btnXuatkho_Click(object sender, EventArgs e)
        {
            timer3.Stop();
            master.Transport.Retries = 2;
            if (cbx_TuanTu.Checked == true)
            {
                Xuat_Kho_Tuan_Tu();
            }
            else
            {
                Xuat_Kho();
            }
        }

        private void cbx_TuanTu_CheckedChanged(object sender, EventArgs e)
        {
            if(!cbx_TuanTu.Checked)
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }        

        private void btnNhapkho_Click(object sender, EventArgs e)
        {
            
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            int StorageID = ((sender as Button).Tag as Storage).ID;
            lsvStorage.Tag = (sender as Button).Tag;
            ShowStorage(StorageID);
        }

        #endregion


    }
}
