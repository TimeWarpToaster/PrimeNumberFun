//Eratosthenes Sieve By Kind
//(c) 2026 - TimeWarpToaster

//https://www.gnu.org/licenses/gpl-3.0.html

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace EratosthenesSieveByKind
{
    public static class L
    {
        public const string CLASSNAME = "L";


        public static string logPath = @"";
        public static int cntLogHelper = 0;

        public static RichTextBox rtbLog = null;
        public static ListBox lbLog = null;

        public static bool isFileLogging = true;
        public static bool isDebug = true;

        public static long clearLogs()
        {
            const string location = CLASSNAME + ".clearLogs";
            long retVal = 0;
            try
            {
                // If for some reason rtb and lb are both active, take ListBox count for return value
                long temp = 0;
                if (rtbLog != null)
                {
                    if (rtbLog.InvokeRequired)
                    {
                        rtbLog.Invoke(new Action(() => clearLogs()));
                    }
                    else
                    {
                        temp = rtbLog.Text.Length;
                        rtbLog.Text = "";
                    }
                }

                if (lbLog != null)
                {
                    if (lbLog.InvokeRequired)
                    {
                        lbLog.Invoke(new Action(() => clearLogs()));
                    }
                    else
                    {
                        temp = lbLog.Items.Count;
                        lbLog.Items.Clear();
                    }
                }
                retVal = temp;
            }
            catch (Exception ex)
            {
                L.ex(location, ex);
            }
            return retVal;
        }

        public static bool logInit(string _logPath, bool _isDebug)
        {
            bool retValue = false;
            try
            {
                L.isDebug = _isDebug;
                if (_logPath != null && _logPath.Length > 0)
                {
                    L.logPath = _logPath;
                    L.isFileLogging = true;
                    retValue = true;
                }
                else
                {
                    L.isFileLogging = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Form1.logInit(lb) - Exception - Failed to initialize log UI!");
            }
            return retValue;
        }

        public static bool logInit(string _logPath, ListBox inLbLog, bool _isFileLogging)
        {
            bool retValue = false;
            try
            {
                // handle file logging
                int cntErrors = 0;
                L.isFileLogging = _isFileLogging;
                if (L.isFileLogging)
                {
                    if (_logPath == null)
                    {
                        cntErrors++;
                    }
                    else
                    {
                        L.logPath = _logPath;
                    }
                }

                // handle ui logging
                if (inLbLog == null)
                {
                    cntErrors++;// decide if ui logging should be a failure, comment out to pass null and use file only
                }
                else 
                {
                    L.lbLog = inLbLog;
                }

                retValue = cntErrors == 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Form1.logInit(lb) - Exception - Failed to initialize log UI!");
            }
            return retValue;
        }

        public static bool logInit(string _logPath, RichTextBox inRtbLog, bool _isFileLogging)
        {
            bool retValue = false;
            try
            {
                // Only one independent condition must succeed
                if (L.logInit(_logPath, _isFileLogging) && inRtbLog != null)
                {
                    retValue = true;
                }

                if (inRtbLog != null)
                {
                    L.rtbLog = inRtbLog;
                    L.clearLogs();// remove any default / prior text
                    retValue = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Form1.logInit(rtb) - Exception - Failed to initialize log UI!");
            }
            return retValue;
        }

        private static void logger(string location, string msg)
        {
            L.logger(location, msg, TAG.GENERAL);
        }

        private static void logger(string location, string msg, string grade)
        {
            try
            {
                if (msg.Length > 3000)
                {
                    int idx = 0;
                    while (idx <= msg.Length)
                    {
                        string temp = msg.Substring(idx, (idx + 3000 > msg.Length ? msg.Length : idx + 3000));

                        temp = DateTime.Now.ToString(TAG.DTF) + " - " + location + " - " + grade + " - " + (idx > 0 ? "... " : "") + temp;
                        Console.WriteLine(msg);
                        if (grade != TAG.DEBUG || L.isDebug)// TODO - Fix these, OR was a quick thing
                        {
                            if (L.isFileLogging) L.logWriter(msg);
                            L.updateUI(msg, true);// Allow invoke
                            cntLogHelper++;
                        }
                        idx += 3000;
                    }
                }
                else
                {
                    msg = DateTime.Now.ToString(TAG.DTF) + " - " + location + " - " + grade + " - " + msg;
                    Console.WriteLine(msg);
                    if (grade != TAG.DEBUG || L.isDebug)
                    {
                        if (L.isFileLogging) L.logWriter(msg);
                        L.updateUI(msg, true);// Allow invoke
                        cntLogHelper++;
                    }
                }
            }
            catch (Exception ex)
            {
                /* Do Nothing - Catch exception to prevent being kicked to engine */
            }
        }

        public static void d(string location, string msg) { L.logger(location, msg, TAG.DEBUG); }
        public static void err(string location, string msg) { L.logger(location, msg, TAG.ERR); }
        public static void ex(string location, string msg) { L.logger(location, msg, TAG.EX); }
        public static void ex(string location, Exception ex) { if (ex != null && ex.Message.Length > 0) L.logger(location, ex.Message, TAG.EX); }
        public static void l(string location, string msg) { L.logger(location, msg, TAG.GENERAL); }
        public static void t(string location, string msg) { L.logger(location, msg, TAG.TMG); }


        public static void logWriter(string s)
        {
            try
            {
                if (L.isFileLogging)
                {
                    if (!File.Exists(logPath))
                    {
                        // Create a file to write to.
                        using (StreamWriter fs = File.CreateText(logPath))
                        {
                            fs.WriteLine(s);
                        }
                    }
                    else
                    {
                        using (StreamWriter fs = File.AppendText(logPath))
                        {
                            fs.WriteLine(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Form1.logWriter - Failed to write log file!");
            }
        }

        public static void logWriter(List<string> s)
        {
            try
            {
                if (L.isFileLogging) File.AppendAllLines(logPath, s);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Form1.logWriter - Failed to write log file!");
            }
        }



        public static void updateUI(string s, bool allowInvoke)
        {
            const string location = CLASSNAME + ".updateUI";
            try
            {
                if (s != null && s.Length > 0)
                {
                    if (L.lbLog != null)
                    {
                        if (L.lbLog.InvokeRequired)
                        {
                            if (allowInvoke) L.lbLog.Invoke(new Action(() => updateUI(s, false)));
                        }
                        else
                        {
                            L.lbLog.Items.Add(s);
                        }
                    }

                    if (L.rtbLog != null)
                    {
                        if (L.rtbLog.InvokeRequired)
                        {
                            if (allowInvoke) L.rtbLog.Invoke(new Action(() => updateUI(s, false)));
                        }
                        else
                        {
                            L.rtbLog.AppendText(s + "\n");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Form1.updateUI - Failed to update ui!");
            }
        }


        public static class TAG
        {
            public const string DEBUG = "Debug";
            public const string ERR = "Error";
            public const string EX = "Exception";
            public const string GENERAL = "General";
            public const string NORMAL = "General";
            public const string IMPORTANT = "Important";
            public const string TMG = "Timing";

            public const string FAILURE = "Failure";
            public const string SUCCESS = "Success";

            public const string FILE = "file";
            public const string FOLDER = "folder";

            public const string DTF = "yyyy-MM-dd HH:mm:ss.fff";
        }
    }
}
