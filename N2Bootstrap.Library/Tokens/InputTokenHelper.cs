﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using N2.Web.Rendering;

namespace N2Bootstrap.Library.Tokens
{
    public class InputTokenHelper : ITokenHelper
    {
        private readonly DisplayableToken _displayableToken;
        private readonly string _regex = @"(?!.*(?:\|).*)^[a-zA-Z0-9\s]*$";
        private const string ValidationMessage = "FormInput must have a name (alphanumeric|spaces) specified. Example:\"name\"";

        public InputTokenHelper(DisplayableToken displayableToken)
        {
            if (!displayableToken.Name.Equals("FormInput"))
                throw new ArgumentException("Not an input token", "displayableToken");

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