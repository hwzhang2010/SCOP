using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TSFCS.SCOP.Model
{
    public class CmdModel
    {
        #region Field
        private String cmdId;
        private String cmdName;
        private int cmdLen;
        private int flagMain;
        private int flagSub;
        #endregion

        #region Constructor
        public CmdModel()
        { 
        }

        public CmdModel(String cmdId, String cmdName, int flagMain, int flagSub)
        {
            this.cmdId = cmdId;
            this.cmdName = cmdName;
            this.flagMain = flagMain;
            this.flagSub = flagSub;
        }
        #endregion


        #region Property
        public String CmdId
        {
            get { return cmdId; }
            set { cmdId = value; }
        }

        public String CmdName
        {
            get { return cmdName; }
            set { cmdName = value; }
        }

        public int CmdLen
        {
            get { return cmdLen; }
            set { cmdLen = value; }
        }

        public int FlagMain
        {
            get { return flagMain; }
            set { flagMain = value; }
        }

        public int FlagSub
        {
            get { return flagSub; }
            set { flagSub = value; }
        }
        #endregion



    }
}
