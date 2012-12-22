using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using dotless.Core.Loggers;

namespace N2Bootstrap.Library.Less
{
    public class ThemedLessLogger : ILogger
    {
        private readonly string _theme;

        public ThemedLessLogger(string theme)
        {
            _theme = theme;
            LoggedErrors = new List<string>();
        }

        public List<string> LoggedErrors { get; set; }

        public void Debug(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(message, args);
        }

        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Error(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(message, args);
            LoggedErrors.Add(string.Format(message, args));
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            LoggedErrors.Add(message);
        }

        public void Info(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(message, args);
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Log(LogLevel level, string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        public void Warn(string message, params object[] args)
        {
            System.Diagnostics.Debug.WriteLine(message, args);
        }

        public void Warn(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
