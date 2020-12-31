using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSFCS.SCOP.Model
{
    public class SendModel
    {
        #region Field
        //private int sendId;
        private DateTime sendTime;
        private string cmdId;
        private string cmdName;
        private string cmdParam;
        #endregion

        #region Constructor
        public SendModel()
        { 
        }

        public SendModel(CmdModel model)
        {
            this.cmdId = model.CmdId;
            this.cmdName = model.CmdName;
        }
        #endregion

        #region Property
        public DateTime SendTime
        {
            get { return sendTime; }
            set { sendTime = value; }
        }

        public string CmdId
        {
            get { return cmdId; }
            set { cmdId = value; }
        }

        public string CmdName
        {
            get { return cmdName; }
            set { cmdName = value; }
        }

        public string CmdParam
        {
            get { return cmdParam; }
            set { cmdParam = value; }
        }
        #endregion
    }
}
