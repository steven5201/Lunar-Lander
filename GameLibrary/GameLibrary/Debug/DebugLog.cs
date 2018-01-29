using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameLibrary.Debug
{
    public static class DebugLog
    {
        private static string debugFileName = "";
        private static bool debugFileInitialized = false;
        private static List<string> debugLines = new List<string>();

        public static void WriteDebugLine(string debugLine)
        {
            if (debugFileInitialized)
            {
                using (StreamWriter writer = File.AppendText($@".../.../.../.../DebugFiles/{debugFileName}"))
                {
                    writer.WriteLine(debugLine);
                }
            }
        }

        public static void AddDebugLineToGroup(string debugLine)
        {
            debugLines.Add(debugLine);
        }

        public static void SaveDebugLineGroup()
        {
            if (debugFileInitialized)
            {
                using (StreamWriter writer = File.AppendText($@".../.../.../.../DebugFiles/{debugFileName}"))
                {
                    foreach (string line in debugLines)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
        }

        public static void InitializeDebugFile()
        {
            debugFileName = DateTime.Now.ToFileTime().ToString() + ".txt";
            debugFileInitialized = true;
            WriteDebugLine($"Debug File created by the system at {DateTime.Now}");
        }

        public static void InitializeDebugFile(string customName)
        {
            if (customName.Contains(".txt"))
            {
                debugFileName = customName;
                debugFileInitialized = true;
                WriteDebugLine($"Debug File created by the system at {DateTime.Now}");
                WriteDebugLine($"File name changed by user from {DateTime.Now.ToFileTime()} to {customName}");
            }
            else
            {
                debugFileName = customName + ".txt";
                debugFileInitialized = true;
                WriteDebugLine($"Debug File created by the system at {DateTime.Now}");
                WriteDebugLine($"File name changed by user from {DateTime.Now.ToFileTime()} to {customName}");
                WriteDebugLine($"System automatically added .txt");
            }
        }
    }
}
