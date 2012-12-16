using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using N2.Web.Rendering;

namespace N2Bootstrap.Library.Tokens
{
    public class CheckboxTokenHelper : ITokenHelper
    {
        private readonly DisplayableToken _displayableToken;
        private readonly string _regex = @"^[a-zA-Z0-9\s]+\|[a-zA-Z0-9\s]+$";
        private const string ValidationMessage = "FormCheckbox must have a name (alphanumeric|spaces) and a value (alphanumeric|spaces) specified. Example:\"name|value\"";

        public CheckboxTokenHelper(DisplayableToken displayableToken)
        {
            if (!displayableToken.Name.Equals("FormCheckbox"))
                throw new ArgumentException("Not a checkbox token", "displayableToken");

            _displayableToken = displayableToken;
        }

        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(_displayableToken.Value) && Regex.IsMatch(_displayableToken.Value, _regex); }
        }

        public string GetFormName()
        {
            return _displayableToken.GetComponents().First();
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

        public string GetValue()
        {
            return _displayableToken.GetComponents().Skip(1).First();
        }
    }
}
