using System;
using System.Data;

namespace RMCERPAPI.Helpers
{
    public static class EConverter
    {
        public static Int32 ToInt32(object obj)
        {
            try
            {
                if (obj.ToString() == "")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static String ToString(object obj)
        {
            try
            {
                if (obj == null || obj == DBNull.Value)
                {
                    return "";
                }
                else
                {
                    return Convert.ToString(obj);
                }
            }
            catch
            {
                return "";
            }
        }

        public static Decimal ToDecimal(object obj)
        {
            try
            {
                if (ToString(obj) == "")
                {
                    return 0;
                }
                else
                {
                    return Math.Round(Convert.ToDecimal(ToString(obj)), 4);
                }
            }
            catch
            {
                return 0;
            }
        }

        public static Boolean ToBoolean(object obj)
        {
            try
            {
                if (obj.ToString().ToLower() == "y")
                    return true;
                else if (obj.ToString().ToLower() == "1")
                    return true;
                else if (obj.ToString().ToLower() == "n")
                    return false;
                else if (obj.ToString().ToLower() == "0")
                    return false;
                else
                    return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }

        public static Double ToDouble(object obj)
        {
            try
            {
                if (ToString(obj) == "")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(ToString(obj));
                }
            }
            catch
            {
                return 0;
            }
        }

        internal static long ToInt64(object p)
        {
            try
            {
                if (ToString(p) == "")
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(ToString(p));
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}