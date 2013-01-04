#if DEBUG
using System;
using System.Diagnostics;
using Caliburn.Micro;

namespace MultasSociais.WinPhone8App
{
    public class DebugLogger : ILog
    {
        public DebugLogger(Type type) {}

        private string CreateLogMessage(string format, params object[] args)
        {
            return string.Format("[{0}] {1}", DateTime.Now.ToString("o"), string.Format(format, args));
        }
        public void Error(Exception exception)
        {
            Debug.WriteLine("ERROR: " + CreateLogMessage(exception.ToString()));
        }
        public void Info(string format, params object[] args)
        {
            Debug.WriteLine("INFO: " + CreateLogMessage(format, args));
        }
        public void Warn(string format, params object[] args)
        {
            Debug.WriteLine("WARN: " + CreateLogMessage(format, args));
        }
    }
}
#endif