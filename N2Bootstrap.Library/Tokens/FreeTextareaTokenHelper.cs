using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using N2.Web.Rendering;

namespace N2Bootstrap.Library.Tokens
{
    public class FreeTextareaTokenHelper : ITokenHelper
    {
        private readonly DisplayableToken _displayableToken;
        private readonly string _regex = @"(?!.*(?:\|).*)^[a-zA-Z0-9\s]*$";
        private const string ValidationMessage = "FormTextarea must have a name (alphanumeric|spaces) specified. Example:\"name\"";

        public FreeTextareaTokenHelper(DisplayableToken displayableToken)
        {
            if (!displayableToken.Name.Equals("FormTextarea"))
                throw new ArgumentException("Not a textarea token", "displayableToken");

            _displayableToken = displayableToken;
        }

        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(_displayableToken.Value) && Regex.IsMatch(_displayableToken.Value, _regex); }
        }

        public string GetFormName()
        {
            return _displayableToken.Value;
        }

        public string GetHelpText()
        {
            return ValidationMessage;
        }

        public string GetTokenValue()
        {
            return _displayableToken.Value;
        }

        public string GetTokenName()
        {
            return _displayableToken.Name;
        }
    }
}
