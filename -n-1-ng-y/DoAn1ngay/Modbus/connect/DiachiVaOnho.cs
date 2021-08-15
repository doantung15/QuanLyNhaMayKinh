using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modbus.connect
{
   public class DiachiVaOnho
    {

        public DiachiVaOnho(int id, byte slaveid,int address)
        {
            this.Id = id;
            this.SlaveId = slaveid;
            this.Address = address;
        }

        private int address;

        private byte slaveId;

        private int id;

        public int Id { get => id; set => id = value; }
        public byte SlaveId { get => slaveId; set => slaveId = value; }
        public int Address { get => address; set => address = value; }
    }
}
