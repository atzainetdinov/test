using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace MVCBlog
{
    public class HelpFunction
    {

        public static string appFolder = ConfigurationManager.AppSettings["WorkFolder"];
        public static string LogFolder = Path.Combine(appFolder, "Log");

        public static string host = ConfigurationManager.AppSettings["host"];
        public static Mutex mutexSql = new Mutex(false, "mutexSql");
        static Mutex mutexLog = new Mutex(false, "mutexLog");

        public static void CreateFolders()
        {
            try
            {
                CreateFolder(appFolder);
                CreateFolder(LogFolder);
            }
            catch (Exception ex)
            {
                GenerateExReturn(ex);
            }
        }

        public static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        public static void AddToLog(string str)
        {
            WriteLog(str);
        }



        public static void WriteLog(string str)
        {
            //    try
            //    {
            //        using (StreamWriter sw = File.AppendText(fileLog))
            //        {
            //            sw.WriteLine(DateTime.Now.ToString() + "  " + str);
            //            sw.Flush();
            //            sw.Close();
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        // GenerateExService(ex);
            //    }
            var fileStream = InitSW();
            fileStream.WriteLine(DateTime.Now + "-" + str);
            fileStream.Close();
        }

        public static void GenerateExReturn(Exception ex)
        {
            GenerateEx(ex);
            GC.Collect();
            return;
        }

        public static void GenerateEx(Exception ex)
        {
            try
            {
                mutexLog.WaitOne();
                //// вывод отдельно строки, где произошло исключение
                //var st = new StackTrace(ex, true);
                //AddToLog(" Ошибка " + st.GetFrame(st.FrameCount - 1).GetMethod());
                //AddToLog("Текст ошибки " + ex.Message.ToString());
                //if (ex.InnerException != null) AddToLog(ex.InnerException.Message);
                //var frame = st.GetFrame(0);
                //for (int i = 0; i < st.FrameCount; i++)
                //{
                //    string ss = st.GetFrame(i).ToString().Trim();
                //    AddToLog(ss);
                //}
                //AddToLog("-------------------------------------------------------------");

                var fileStream = InitSW();
                fileStream.WriteLine(DateTime.Now + "-" + ex.Message);
                fileStream.WriteLine(ex.StackTrace);
                fileStream.Close();
            }
            catch (Exception)
            {

            }
            finally
            {
                mutexLog.ReleaseMutex();
            }

        }


        private static StreamWriter InitSW()
        {
            string name = LogFolder + DateTime.Now.ToString("dd-MM-yyyy") + ".log";
            if (!Directory.Exists(LogFolder))
            {
                Directory.CreateDirectory(LogFolder);
            }
            FileStream fs;
            try
            {
                fs = new FileStream(name, FileMode.Append, FileAccess.Write);
                return new StreamWriter(fs);
            }
            catch (Exception ex)
            {
                fs = null;
            }
            return null;
        }
    }
}