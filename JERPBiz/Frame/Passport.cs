using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Runtime.InteropServices;
namespace JERPBiz.Frame
{
    public class Passport
    {
        public static string GetPassport()
        {
            return GetMACByIP(JERPData.ServerParameter .ServerName  );
        }
        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, byte[] mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        /// <summary>
        /// ����ip�õ�����mac��ַ
        /// </summary>
        /// <param name="ip">������ip��ַ</param>
        /// <returns>��Ӧip������mac��ַ</returns>
        private static string GetMACByIP(string ip)
        {
            try
            {
                byte[] aa = new byte[6];

                Int32 ldest = inet_addr(ip); //Ŀ�ĵص�ip
                Int32 len = 6;
                int res = SendARP(ldest, 0, aa, ref len);

                return BitConverter.ToString(aa, 0, 6); ;
            }
            catch (Exception err)
            {
                throw err;
            }


        }
    }
}
