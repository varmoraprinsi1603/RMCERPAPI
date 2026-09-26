using RMCERPAPI.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Runtime.CompilerServices;

namespace RMCERPAPI.Helpers
{
    public class CommonHelpers
    {

        public class EConverter
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
                        return (Convert.ToInt32(obj));
                    }
                }
                catch { return 0; }
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
                        return (Convert.ToString(obj));
                    }
                }
                catch { return ""; }
            }
            public static int ToWeekDayInt(string day)
            {
                switch (day.ToLower())
                {
                    case "monday":
                        return 0;
                    case "tuesday":
                        return 1;
                    case "thursday":
                        return 3;
                    case "friday":
                        return 4;
                    case "saturday":
                        return 5;
                    case "sunday":
                        return 6;
                    default:
                        return 2;
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
                        return (Math.Round(Convert.ToDecimal(ToString(obj)), 4));
                    }
                }
                catch { return 0; }
            }

            public static Boolean ToBoolean(object obj)
            {
                try
                {
                    if (obj.ToString().ToLower() == "y")
                    {
                        return true;
                    }
                    else if (obj.ToString().ToLower() == "1")
                    {
                        return true;
                    }
                    else if (obj.ToString().ToLower() == "n")
                    {
                        return false;
                    }
                    else if (obj.ToString().ToLower() == "0")
                    {
                        return false;
                    }
                    else
                    {
                        return (Convert.ToBoolean(obj));
                    }
                }
                catch { return false; }
            }
            private static string RelativePath(string absolutePath, string relativeTo)
            {
                string[] absoluteDirectories = absolutePath.Split('\\');
                string[] relativeDirectories = relativeTo.Split('\\');
                //Get the shortest of the two paths            
                int length = absoluteDirectories.Length < relativeDirectories.Length ? absoluteDirectories.Length : relativeDirectories.Length;
                //Use to determine where in the loop we exited          
                int lastCommonRoot = -1;
                int index;
                //Find common root           
                for (index = 0; index < length; index++)
                    if (absoluteDirectories[index] == relativeDirectories[index])
                        lastCommonRoot = index;
                    else
                        break;
                //If we didn't find a common prefix then throw           
                if (lastCommonRoot == -1)
                    throw new ArgumentException("Paths do not have a common base");
                //Build up the relative path            
                StringBuilder relativePath = new StringBuilder();
                //Add on the ..            
                for (index = lastCommonRoot + 1; index < absoluteDirectories.Length; index++)
                    if (absoluteDirectories[index].Length > 0)
                        relativePath.Append("..\\");
                //Add on the folders           
                for (index = lastCommonRoot + 1; index < relativeDirectories.Length - 1; index++)
                    relativePath.Append(relativeDirectories[index] + "\\");
                relativePath.Append(relativeDirectories[relativeDirectories.Length - 1]);
                return relativePath.ToString();
            }
            public static string ToSDateTime(object obj)
            {
                try
                {
                    if (obj.ToString() == "")
                    {
                        return "";
                    }
                    return Convert.ToDateTime(obj.ToString()).ToString("dd-MMM-yyyy HH:mm");

                }
                catch { return ""; }
            }
            public static string ToShortDate(object obj)
            {
                try
                {
                    if (obj.ToString() == "")
                    {
                        return "";
                    }
                    return Convert.ToDateTime(obj.ToString()).ToString("dd-MMM");

                }
                catch { return ""; }
            }
            public static string ToSDate(object obj)
            {
                try
                {
                    if (obj.ToString() == "")
                    {
                        return "";
                    }
                    return Convert.ToDateTime(obj.ToString()).ToString("dd-MMM-yyyy");

                }
                catch { return ""; }
            }
            public static string ToSTime(object obj)
            {
                try
                {
                    if (obj.ToString() == "")
                    {
                        return "";
                    }
                    return Convert.ToDateTime(obj.ToString()).ToString("HH:mm");

                }
                catch { return ""; }
            }
            public static string ToDateTime(object obj)
            {
                try
                {
                    if (obj.ToString() == "")
                    {
                        return "";
                    }
                    return Convert.ToDateTime(obj.ToString()).ToString("dd-MMM-yyyy HH:mm");

                }
                catch { return ""; }
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
                        return (Convert.ToDouble(ToString(obj)));
                    }
                }
                catch { return 0; }
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
                        return (Convert.ToInt64(ToString(p)));
                    }
                }
                catch { return 0; }
            }

            #region GenerateFile and WriteLog into File
            public static void WriteToLibraryFile(string message,
            [CallerMemberName] string methodName = "",
            [CallerFilePath] string filePath = "")
            {
                // Get the class name from the file path
                string className = System.IO.Path.GetFileNameWithoutExtension(filePath);
                string path = AppDomain.CurrentDomain.BaseDirectory + "\\fileupload";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\fileupload\\RepositoryErrorlog.txt";
                if (!System.IO.File.Exists(filepath))
                {
                    // Create a file to write to. 
                    using (StreamWriter sw = System.IO.File.CreateText(filepath))
                    {
                        sw.WriteLine($"{className} || {methodName} || " + DateTime.Now.ToString() + " || " + message);
                    }
                }
                else
                {
                    using (StreamWriter sw = System.IO.File.AppendText(filepath))
                    {
                        sw.WriteLine($"{className} || {methodName} || " + DateTime.Now.ToString() + " || " + message);
                    }
                }
            }
            #endregion
        }

        public static class ModelToUTT
        {
            //public static DataTable ConvertToUTT<T>(string UTTName, IList<T> Listdata)
            //{
            //    try
            //    {
            //        DataSet UTTListdataset = new DataSet();
            //        UTTListdataset = null;

            //        System.ComponentModel.PropertyDescriptorCollection properties = System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));
            //        DataTable table = new DataTable();

            //        foreach (System.ComponentModel.PropertyDescriptor prop in properties)
            //        {
            //            foreach (DataRow dr in UTTListdataset.Tables[0].Rows)
            //            {
            //                if (dr[0].ToString() == prop.DisplayName)
            //                {
            //                    if (prop.PropertyType.Namespace == "System.Collections.Generic")
            //                    {
            //                        table.Columns.Add(prop.Name);
            //                    }
            //                    else
            //                    {
            //                        table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            //                    }
            //                }
            //            }

            //        }

            //        foreach (T item in Listdata)
            //        {
            //            DataRow row = table.NewRow();
            //            foreach (System.ComponentModel.PropertyDescriptor prop in properties)
            //            {
            //                foreach (DataRow dr in UTTListdataset.Tables[0].Rows)
            //                {
            //                    if (dr[0].ToString() == prop.DisplayName)
            //                    {
            //                        if (prop.PropertyType.Namespace == "System.Collections.Generic")
            //                        {
            //                            try
            //                            {
            //                                //Blank for store XML
            //                                row[prop.Name] = "";
            //                            }
            //                            catch (Exception)
            //                            {

            //                            }

            //                        }
            //                        else
            //                        {
            //                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            //                        }

            //                    }
            //                }
            //            }
            //            table.Rows.Add(row);
            //        }

            //        return table;
            //    }
            //    catch (Exception)
            //    {
            //        return null;

            //    }

            //}


            public static DataTable ConvertToUTT<T>(string UTTName, IList<T> Listdata)
            {
                try
                {
                    System.ComponentModel.PropertyDescriptorCollection properties =
                        System.ComponentModel.TypeDescriptor.GetProperties(typeof(T));

                    DataTable table = new DataTable();

                    foreach (System.ComponentModel.PropertyDescriptor prop in properties)
                    {
                        if (prop.Name == "ItemName" || prop.Name == "UOMName")
                            continue;

                        if (prop.PropertyType.Namespace == "System.Collections.Generic")
                        {
                            table.Columns.Add(prop.Name);
                        }
                        else
                        {
                            table.Columns.Add(
                                prop.Name,
                                Nullable.GetUnderlyingType(prop.PropertyType)
                                ?? prop.PropertyType
                            );
                        }
                    }

                    foreach (T item in Listdata)
                    {
                        DataRow row = table.NewRow();

                        foreach (System.ComponentModel.PropertyDescriptor prop in properties)
                        {
                            if (prop.Name == "ItemName" || prop.Name == "UOMName")
                                continue;

                            if (prop.PropertyType.Namespace == "System.Collections.Generic")
                            {
                                row[prop.Name] = "";
                            }
                            else
                            {
                                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                            }
                        }

                        table.Rows.Add(row);
                    }

                    return table;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}