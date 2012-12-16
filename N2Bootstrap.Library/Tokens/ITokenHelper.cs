using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace N2Bootstrap.Library.Tokens
{
    public interface ITokenHelper
    {
        bool IsValid { get; }
        string GetHelpText();
        string GetFormName();
        string GetTokenValue();
        string GetTokenName();
    }
}
