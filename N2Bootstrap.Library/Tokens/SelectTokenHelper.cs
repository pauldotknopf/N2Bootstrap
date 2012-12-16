using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using N2.Web.Rendering;

namespace N2Bootstrap.Library.Tokens
{
    public class SelectTokenHelper : ITokenHelper
    {
        private readonly DisplayableToken _displayableToken;
        private readonly string _regex = @"^[a-zA-Z0-9\s]+\|(?!.*(?:\|).*)[a-zA-Z0-9\s,]+$";
        private const string ValidationMessage = "FormSelect must have a name (alphanumeric|spaces), followed by at least one option. Example:\"name|option1,option2\"";

        public SelectTokenHelper(DisplayableToken displayableToken)
        {
            if (!displayableToken.Name.Equals("FormSelect"))
                throw new ArgumentException("Not a select token", "displayableToken");

            _displayableToken = displayableToken;
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(_displayableToken.Value) && Regex.IsMatch(_displayableToken.Value, _regex);
            }
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

        public string GetFormName()
        {
            return _displayableToken.GetComponents()[0];
        }

        public string[] GetOptions()
        {
            return _displayableToken.GetComponents().Skip(1).First().Split(Convert.ToChar(","));
        }
    }
}
