using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;

using Dapper;

using TSFCS.SCOP.Model;

namespace TSFCS.SCOP.DAL
{
    public class CmdOperation
    {
        #region Field
        private static readonly string strConnection = "Data Source=db_scop.db3";
        private static readonly int cmdCoutnMin = 8;
        public static readonly int CmdCountConst = 200;
        #endregion

        #region Method
        public static void makeCmdByte(ref List<byte> cmdList)
        {
            if (cmdList.Count > CmdCountConst)
                cmdList.RemoveRange(CmdCountConst, cmdList.Count - CmdCountConst);

            for (int i = cmdList.Count; i < CmdCountConst; i++)
                cmdList.Add(0x0);
        }

        public static List<byte> genCmdByte(string cmdId)
        {
            CmdModel model = getCmdById(cmdId);
            if (model == null || model.CmdLen < cmdCoutnMin)
                return null;

            List<byte> cmdList = new List<byte>();
            cmdList.Add(0xEB);
            cmdList.Add(0x90);
            cmdList.Add(0xB4);
            cmdList.Add(0x1C);
            cmdList.Add(0x1);
            cmdList.Add(0x2);
            cmdList.Add((byte)model.FlagMain);
            cmdList.Add((byte)model.FlagSub);

            return cmdList;
        }

        public static CmdModel getCmdById(string cmdId)
        {
            using (IDbConnection connection = new SQLiteConnection(strConnection))
            {
                return connection.Query<CmdModel>("select * from t_cmd where cmdid = @cmdid", new { cmdid = cmdId }).Single();  //获取指定ID的遥控指令
            }
        }

        public static SendModel genSendByCmd(string cmdId)
        {
            CmdModel cmdModel = getCmdById(cmdId);
            if (cmdModel == null)
                return null;

            return new SendModel(cmdModel);
        }
        #endregion


    }
}
