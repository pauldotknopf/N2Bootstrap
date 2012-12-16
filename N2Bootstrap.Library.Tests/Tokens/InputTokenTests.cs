using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace N2Bootstrap.Library.Tests.Tokens
{
    [TestClass]
    public class InputTokenTests
    {
        [TestMethod]
        public void Can_invalidate()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormInput";

            token.Value = "invalid|value";
            Assert.IsFalse(new Library.Tokens.InputTokenHelper(token).IsValid);

            token.Value = null;
            Assert.IsFalse(new Library.Tokens.InputTokenHelper(token).IsValid);

            token.Value = "";
            Assert.IsFalse(new Library.Tokens.InputTokenHelper(token).IsValid);
        }

        [TestMethod]
        public void Can_validate()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormInput";

            token.Value = "test";
            Assert.IsTrue(new Library.Tokens.InputTokenHelper(token).IsValid);

            token.Value = "test value";
            Assert.IsTrue(new Library.Tokens.InputTokenHelper(token).IsValid);
        }

        [TestMethod]
        public void Can_get_form_name()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormInput";

            token.Value = "form name";
            Assert.AreEqual("form name", new Library.Tokens.InputTokenHelper(token).GetFormName());
        }
    }
}
