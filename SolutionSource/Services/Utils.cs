using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SolutionSource.Services
{
    public class Utils
    {
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        public static string getLastSlashContent(string str)
        {
            int pos = str.LastIndexOf("/") + 1;
            return str.Substring(pos, str.Length - pos);
        }

        public static string getLastBackSlashContent(string str)
        {
            int pos = str.LastIndexOf("\\") + 1;
            return str.Substring(pos, str.Length - pos);
        }

        public static string list2string(List<string> data)
        {//use '#;', no spacing between it
            string result = "";
            foreach (string str in data)
            {
                result += str + "#;";
            }
            return result;
        }

        public static string list2string2(List<string> data)//use ','    no spacing between it
        {//use comma, no spacing between it
            string result = "";
            foreach (string str in data)
            {
                result += str + ",";
            }
            return result;
        }


        public static string dateTime2SqlDate(DateTime dt)
        {
            string sqlFormattedDate = dt.ToString("yyyy-MM-dd");
            return sqlFormattedDate;
        }

        public static DateTime str2date(string str)
        {
            return Convert.ToDateTime(str);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}