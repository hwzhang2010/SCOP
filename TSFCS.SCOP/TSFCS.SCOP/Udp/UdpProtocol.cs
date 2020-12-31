using System;
using System.Collections.Generic;

using Sodao.FastSocket.Server.Protocol;

namespace TSFCS.SCOP.Udp
{
    public class UdpProtocol : IUdpProtocol<UdpMessage>
    {
        private List<byte> buffer = new List<byte>();  //接收数据缓存
        private bool isFind = false;  //帧头找到标识

        public UdpMessage Parse(ArraySegment<byte> data)
        {
            int len = data.Count;
            byte[] payload = new byte[len];
            Buffer.BlockCopy(data.Array, data.Offset, payload, 0, len);

            buffer.AddRange(payload);  //加入缓存
            if (!isFind)  //未发现帧头时
            {
                for (int i = 0; i < buffer.Count - 1; i++)  //分析缓存数据，查找帧头
                {
                    if (buffer[i] == 0xEB && buffer[i + 1] == 0x90)  //帧头EB 90
                    {
                        isFind = true;
                        if (i > 0)  //不是起始位置
                            buffer.RemoveRange(0, i);  //移除帧头之前的无效字节
                        break;
                    }
                }
            }
            if (isFind)  //找到帧头
            {
                for (int i = 2; i < buffer.Count - 1; i++)  //分析缓存数据，查找帧尾
                {
                    if (buffer[i] == 0x09 && buffer[i + 1] == 0xD7)  //帧尾09 D7
                    {
                        byte[] frame = new byte[i + 2];
                        buffer.CopyTo(0, frame, 0, i + 2);  //提取包含帧头帧尾的有效数据
                        buffer.RemoveRange(0, i + 2);  //从缓存数据中移除有效帧
                        isFind = false;  //下次重新开始寻找

                        return new UdpMessage(frame, i + 2);
                    }
                }

                if (isFind)  //一直未找到帧尾，但是找到已经帧头
                {
                    if (buffer.Count > 4096)  //当4K的数据量都未找到帧尾时
                        buffer.Clear();
                }
            }
            else  //一直未找到帧头
            {
                if (buffer.Count > 4096)  //当4K的数据量都未找到帧头时
                    buffer.Clear();
            }

            return null;
        }
    }
}
