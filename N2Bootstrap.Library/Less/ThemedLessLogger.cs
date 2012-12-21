using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace N2Bootstrap.Library.Less
{
    public class ThemedLessLogger : dotless.Core.Loggers.DiagnosticsLogger
    {
        public string Theme { get; set; }

        public ThemedLessLogger(string theme)
            :base(dotless.Core.Loggers.LogLevel.Debug)
        {
            Theme = theme;
        }
    }
}
