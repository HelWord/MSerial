using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yangsi.Telemetry
{
    public static class GenerateMSerial
    {
        public static string GetMSerial(bool[] fbconnection, bool[] initregister)
        {
            if (fbconnection == null || fbconnection.Length <= 0 || initregister == null || initregister.Length <= 0 || fbconnection.Length != initregister.Length)
                throw new System.ArgumentException("Invalid Paras.");

            int n = fbconnection.Length;
            int N = (int)(Math.Pow(2, n) - 1);
            bool[] register = initregister;
            bool[] newregister = new bool[n];
            bool[] result = new bool[N];
            string Str = "";
            for (int i = 0; i < N; i++)
            {
                result[i] = register[n - 1];
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += Convert.ToInt32(fbconnection[j]) * Convert.ToInt32(register[j]);
                }
                newregister[0] = Convert.ToBoolean(sum % 2);
                for (int k = 1; k < n; k++)
                {
                    newregister[k] = register[k - 1];
                }
                Array.Copy(newregister, register, newregister.Length);
            }
            
            for (int i = 0; i < result.Length; i++)
            {
                Str += Convert.ToInt32(result[i]).ToString();
                //if (i % 8 == 7)
                    //Str += "\n";
            }
            return Str;
        }


        public static string GetMSerial(string fbconnection, string initregister)
        {
            if (fbconnection == null || fbconnection.Length <= 0 || initregister == null || initregister.Length <= 0 || fbconnection.Length != initregister.Length)
                throw new System.ArgumentException("Invalid Paras.");

            //判断字符串中是否只包含1和0
            Regex r;
            string pattern;
            pattern = @"^[0-1]*$";
            r = new Regex(pattern);
            if (! r.IsMatch(fbconnection) || ! r.IsMatch(initregister))
            {
                throw new System.ArgumentException("Invalid Paras.");
            }

            int n = fbconnection.ToCharArray().Length;
            int N = (int)(Math.Pow(2, n) - 1);
            char[] register = initregister.ToCharArray();
            char[] newregister = new char[n];
            char[] result = new char[N];
            char[] con = fbconnection.ToCharArray();
            for (int i = 0; i < N; i++)
            {
                result[i] = register[n - 1];
                int sum = 0;
                for (int j = 0; j < n; j++)
                {
                    sum += ((int)(con[j]) - '0') * ((int)(register[j]) - '0');
                }
                newregister[0] = (char)(sum % 2 + '0');
                for (int k = 1; k < n; k++)
                {
                    newregister[k] = register[k - 1];
                }
                Array.Copy(newregister, register, newregister.Length);
            }
            string Str = new string(result);
            return Str;
        }
    }
}
