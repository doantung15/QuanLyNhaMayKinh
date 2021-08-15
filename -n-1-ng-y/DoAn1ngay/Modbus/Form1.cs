using NModbus;
using NModbus.Serial;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modbus.connect;
namespace Modbus
{
    public partial class Form1 : Form
    {
        Modbus_Poll_CS.modbus mb = new Modbus_Poll_CS.modbus();
        bool[] ketqa;

        IModbusMaster master;
        IModbusRtuTransport RTUtransport;
        IModbusSerialMaster serialMaster;

        private static System.Timers.Timer timer2;
        private static System.Timers.Timer timer3;
        private static System.Timers.Timer timer4;

        StringBuilder data = new StringBuilder();
        StringBuilder check = new StringBuilder();
        StringBuilder _check = new StringBuilder();
        Thread thread;
        string Reconnect;
        SerialPort serialPort1 = new SerialPort();
        private bool Isconnected = false;
        bool k = false;

        int flag1;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            cbx_ID.Items.AddRange(SerialPort.GetPortNames());
            checkopen();
        }

        void checkopen()
        {
            if (cbx_ID.Text=="") 
            {
                txt_Kiemtra.Enabled = false;
                txt_Receive.Enabled = false;
                Txt_SlaveId.Enabled = false;
                txtData.Enabled = false;
                txtAddress.Enabled = false;
                btnDisconnect.Enabled = false;
                btnRefresh.Enabled = false;
                btn_Connect.Enabled = false;
                //btn_Slave.Enabled = false;
                btn_Transmit.Enabled = false;

            }
            else
            {
                btn_Connect.Enabled = true;
                if (Isconnected == true) 
                {
                    txt_Kiemtra.Enabled = true;
                    txt_Receive.Enabled = true;
                    Txt_SlaveId.Enabled = true;
                    txtData.Enabled = true;
                    txtAddress.Enabled = true;
                    btnDisconnect.Enabled = true;
                    btnRefresh.Enabled = true;
                    btn_Connect.Enabled = false;
                    //btn_Slave.Enabled = true;
                    btn_Transmit.Enabled = true;
                }
                else
                {
                    txt_Kiemtra.Enabled = false;
                    txt_Receive.Enabled = false;
                    Txt_SlaveId.Enabled = false;
                    txtData.Enabled = false;
                    txtAddress.Enabled = false;
                    btnDisconnect.Enabled = false;
                    btnRefresh.Enabled = false;
                    btn_Connect.Enabled = true;
                    //btn_Slave.Enabled = false;
                    btn_Transmit.Enabled = false;
                }
            }
        }

        #region old_event

        void check_toggle()
        {
            timer2 = new System.Timers.Timer(1000);
            timer3 = new System.Timers.Timer(1000);

            timer2.Elapsed += Timer2_Elapsed;
            timer3.Elapsed += Timer3_Elapsed;

            timer2.AutoReset = true;
            timer3.AutoReset = true;

            
        }

        private void Timer3_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            button1.BackColor = Color.Red;
            
        }

        private void Timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (k == true)
            {
                button1.BackColor = Color.Green;                
            }
            else
            {
                button1.BackColor = Color.Red;
            }
            k = !k;

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
            //RTUtransport = factory.CreateRtuTransport(serialPort1);
            //serialMaster = factory.CreateMaster(RTUtransport);
            master = factory.CreateRtuMaster(serialPort1);
            master.Transport.Retries = 1;
            master.Transport.SlaveBusyUsesRetryCount = true;
            master.Transport.RetryOnOldResponseThreshold = 1;
        }
        #region Rawdata

        public string modbusStatus;

        private void Opencomport_Rawdata()
        {
            serialPort1.PortName = cbx_ID.Text;
            serialPort1.BaudRate = 115200;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Open();
            serialPort1.ReadTimeout = 1;
            serialPort1.WriteTimeout = 1;
        }

        private void BuildMessage(byte address, byte type, ushort start, ushort registers, ref byte[] message)
        {
            //Array to receive CRC bytes:
            byte[] CRC = new byte[2];

            message[0] = address;
            message[1] = type;
            message[2] = (byte)(start >> 8);
            message[3] = (byte)start;
            message[4] = (byte)(registers >> 8);
            message[5] = (byte)registers;

            GetCRC(message, ref CRC);
            message[message.Length - 2] = CRC[0];
            message[message.Length - 1] = CRC[1];
        }

        private void GetResponse(ref byte[] response)
        {
            for (int i = 0; i < response.Length; i++)
            {
                response[i] = (byte)(serialPort1.ReadByte());
            }
        }
        private bool CheckResponse(byte[] response)
        {
            //Perform a basic CRC check:
            byte[] CRC = new byte[2];
            GetCRC(response, ref CRC);
            if (CRC[0] == response[response.Length - 2] && CRC[1] == response[response.Length - 1])
                return true;
            else
                return false;
        }

        private void GetCRC(byte[] message, ref byte[] CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to 
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length) - 2; i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }

        public bool SendFc3(byte address, ushort start, ushort registers, ref short[] values)
        {
            
            if (serialPort1.IsOpen)
            {

                serialPort1.DiscardOutBuffer();
                serialPort1.DiscardInBuffer();
               
                byte[] message = new byte[8];
                
                byte[] response = new byte[5 + 2 * registers];
                
                BuildMessage(address, (byte)3, start, registers, ref message);
                //Send modbus message to Serial Port:
                try
                {
                    serialPort1.Write(message, 0, message.Length);
                    GetResponse(ref response);
                }
                catch (Exception err)
                {
                    modbusStatus = "Error in read event: " + err.Message;
                    return false;
                }
                //Evaluate message:
                if (CheckResponse(response))
                {
                    //Return requested register values:
                    for (int i = 0; i < (response.Length - 5) / 2; i++)
                    {
                        values[i] = response[2 * i + 3];
                        values[i] <<= 8;
                        values[i] += response[2 * i + 4];
                    }
                    modbusStatus = "Read successful";
                    return true;
                }
                else
                {
                    modbusStatus = "CRC error";
                    return false;
                }
            }
            else
            {
                modbusStatus = "Serial port not open";
                return false;
            }

        }


        
        private void btn_RawData_Click(object sender, EventArgs e)
        {

            Opencomport_Rawdata();
            timer4 = new System.Timers.Timer(1);
            timer4.Elapsed += Timer4_Elapsed;
            timer4.AutoReset = true;
            timer4.Enabled = true;


        }

        private void Timer4_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Thread thread = new Thread(() => {

                short[] values = new short[12];
                try
                {
                    while (!SendFc3(Convert.ToByte(15), Convert.ToUInt16(0), Convert.ToUInt16(12), ref values)) ;
                }
                catch (Exception err)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txt_Receive.Text = err.Message;
                    });

                }
                this.Invoke((MethodInvoker)delegate
                {
                    txt_Rawdata.Text = values[10].ToString();
                });

            });
            thread.Start();      
            
        }


        #endregion

        public async void new_opencomport(byte i)
        {
            
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            try
            {
                //this.Invoke((MethodInvoker)delegate
                //{
                //    master.WriteSingleCoil(i, 0, true);
                //});
                // master.WriteSingleCoil(i, 0, true);
                //while (!SendFc3(Convert.ToByte(15), Convert.ToUInt16(0), Convert.ToUInt16(12), ref values)) ;
                ketqa = await master.ReadCoilsAsync(Convert.ToByte(i+1), 0, 1);
                flag1++;
            }
            catch (Exception)
            {
                flag1++;

            }

                        

            watch.Stop();
            this.Invoke((MethodInvoker)delegate
            {
                _check.Append($"Execution Time: " + watch.ElapsedMilliseconds + " ms" +"|"+ i.ToString() + "\r\n");
                txtbtn1.Text = _check.ToString();
            });
            if (flag1 == 30)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    txt_Rawdata.Text = "ok";
                    
                    
                });

               
            }
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
                lblError.Text = "";
            }
        }

        private void transmit(byte slaveId, ushort startAddress, ushort[] numRegister)
        {
            
            try
            {
                master.WriteMultipleRegisters(slaveId, startAddress, numRegister);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                txt_Receive.Text = Convert.ToString(data);
            }
            catch
            {
                lblError.Text = "Lỗi rồi, đường truyền có vấn đề";
                Isconnected = false;
            }
        }
        void aa()
        {
            receive(1, 0, 11);
        }
        private void btn_Connect_Click(object sender, EventArgs e)
        {
          
            try
            {
                Opencomport();
                Reconnect = cbx_ID.Text;
                Isconnected = true;
               // timer1.Enabled = true;

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                Isconnected = false;
            }
            checkopen();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            Isconnected = false;
           // timer1.Stop();
           // thread.Abort();

            checkopen();
        }

        private void btn_Transmit_Click(object sender, EventArgs e)
        {
            byte a = Convert.ToByte(Txt_SlaveId.Text);
            ushort addr = Convert.ToUInt16(txtAddress.Text);
            string ox = Convert.ToString(txtData.Text);
            ox = ox.Replace(" ", "");
            string[] mx = ox.Split(';');
            ushort[] bbb = new ushort[mx.Length];
            for (int i = 0; i < mx.Length; i++)
            {
                bbb[i] = Convert.ToUInt16(mx[i]);
            }
            transmit(a, addr, bbb);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            thread = new Thread(aa)
            {
                IsBackground = true
            };
            thread.Start();
            if (Isconnected == false)
            {
                OpencomportWhenDisconnect();

            }
        }
        #endregion

        void checktimeout()
        {            
                Task<ushort[]> t1 = master.ReadHoldingRegistersAsync(Convert.ToByte(Txt_SlaveId.Text), 0, 1);
                //Task<bool[]> t2 = master.ReadCoilsAsync(Convert.ToByte(Txt_SlaveId.Text), 0, 10);
                if (!t1.Wait(5000))
                {
                    txt_Kiemtra.Text= Txt_SlaveId.Text + "Deo ket noi duoc \r\n";
                }
                else
                {
                    txt_Kiemtra.Text= Txt_SlaveId.Text + " ngon \r\n";
                }
            

        }

        void check_task()
        {
            Task t = Task.Run(() =>
            {
                Random rnd = new Random();
                long sum = 0;
                int n = 5000000;
                for (int ctr = 1; ctr <= n; ctr++)
                {
                    int number = rnd.Next(0, 101);
                    sum += number;
                }
                txtbtn1.Text = "Total:  " + sum.ToString() + "\r\n" + "Mean:  " + (sum / n).ToString() + "\r\n" + "N:     " + n.ToString();
            });
            TimeSpan ts = TimeSpan.FromMilliseconds(150);
            if (!t.Wait(ts))
                MessageBox.Show("The timeout interval elapsed.");
        }


        void check_task_Connection_Timeout()
        {

            this.Invoke(new Action(() =>
            {
                Task t = Task.Run(() =>
                {
                    master.ReadHoldingRegisters(Convert.ToByte(Txt_SlaveId.Text), 0, 1);
                });
                TimeSpan ts = TimeSpan.FromMilliseconds(1500);
                if (!t.Wait(ts))
                {
                    txt_Kiemtra.Text = Txt_SlaveId.Text + "Deo ket noi duoc \r\n";                    
                }
                else
                {
                    txt_Kiemtra.Text = Txt_SlaveId.Text + " ngon \r\n";
                }

            }));    
                
                
           
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

            checktimeout();

        }


        #region slave
        
        void Opencomport_slave()
        {

            serialPort1.PortName = cbx_ID.Text;
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.Parity = Parity.None;
            serialPort1.StopBits = StopBits.One;
            serialPort1.Open();
            serialPort1.ReadTimeout = 5000;
            serialPort1.WriteTimeout = 5000;

            var factory = new ModbusFactory();
            var slaveNetwork = factory.CreateRtuSlaveNetwork(serialPort1);

            IModbusSlave slave1 = factory.CreateSlave(1);
            IModbusSlave slave2 = factory.CreateSlave(2);
            IModbusSlave slave3 = factory.CreateSlave(3);

            slaveNetwork.AddSlave(slave1);
            slaveNetwork.AddSlave(slave2);
            slaveNetwork.AddSlave(slave3);

            slaveNetwork.ListenAsync().GetAwaiter().GetResult();

            
        }
                

        private void btn_Slave_Click(object sender, EventArgs e)
        {
            
        }
        #endregion

        private void cbx_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkopen();
        }


        void aaa()
        {

            for (int i = 0; i < 30; i++)
            {

                //int temp = i;
                //Thread t = new Thread(() =>
                //{
                //    new_opencomport(Convert.ToByte(temp));

                //});
                //t.IsBackground = true;
                //t.Start();
                new_opencomport(Convert.ToByte(i));            
                
            }    
            
        }

        private void btnHamThu_Click(object sender, EventArgs e)
        {
            //aaa();
            Thread thread = new Thread(() => { aaa(); });

            thread.Start();



            //for(int i = 1; i < 30; i++)
            //{
            //    int temp = i;
            //    Thread t = new Thread(() =>
            //    {
            //        DemoThread("Thread" + temp);

            //    });
            //    //t.IsBackground = true;
            //    t.Start();
            //}
        }

        void DemoThread(string threadIndex)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            for (int i = 0; i < 1; i++)
            {                             
                check.Append( threadIndex + i + "\r\n");
                txtbtn2.Text = check.ToString();
            }
            watch.Stop();
            _check.Append($"Execution Time: " + watch.ElapsedMilliseconds + " ms" + "\r\n");
            txtbtn1.Text = _check.ToString();
        }


        void chuoi()
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
                    

                    string[] tach = _data.Split('\n');                    

                    foreach (string item in tach)
                    {
                        DataXuatKho.Add(item);
                    }

                    DataXuatKho.Sort();
                    for (int i = 0; i < 30; i++)
                    {
                        __DataXuatKho[i] = new List<string>();
                        foreach(string item in DataXuatKho)
                        {
                            if (Convert.ToInt32(item.Substring(1, 2)) == i + 1) __DataXuatKho[i].Add(item);
                            
                        }                        
                    }

                    
                }
            }
        }
        private void btnChuoi_Click(object sender, EventArgs e)
        {
            chuoi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string k = "02";
            if (Convert.ToInt32(k.Substring(0, 2)) == 1) MessageBox.Show(k);
            else MessageBox.Show("sai");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            check_toggle();
            timer2.Enabled = true;
        }

        
    }
}
