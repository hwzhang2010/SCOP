using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSFCS.SCOP.Model
{
    public class UdpModel
    {
        #region Field
        private int id;
        private string type;
        private string ip;
        private int port;
        #endregion

        #region Property
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        #endregion

        #region Constructor
        public UdpModel() 
        {
        }
        #endregion


    }
}
