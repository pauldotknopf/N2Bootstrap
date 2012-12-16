using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace N2Bootstrap.Library.Tests.Tokens
{
    [TestClass]
    public class RadioTokenTests
    {
        [TestMethod]
        public void Can_invalidate()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormRadio";

            token.Value = "invalid";
            Assert.IsFalse(new Library.Tokens.RadioTokenHelper(token).IsValid);

            token.Value = "invalid|value|secondvalue";
            Assert.IsFalse(new Library.Tokens.RadioTokenHelper(token).IsValid);

            token.Value = null;
            Assert.IsFalse(new Library.Tokens.RadioTokenHelper(token).IsValid);

            token.Value = "";
            Assert.IsFalse(new Library.Tokens.RadioTokenHelper(token).IsValid);
        }

        [TestMethod]
        public void Can_validate()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormRadio";

            token.Value = "test|value";
            Assert.IsTrue(new Library.Tokens.RadioTokenHelper(token).IsValid);
        }

        [TestMethod]
        public void Can_get_form_name()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormRadio";

            token.Value = "test1|value1";
            Assert.AreEqual("test1", new Library.Tokens.RadioTokenHelper(token).GetFormName());
        }

        [TestMethod]
        public void Can_get_value()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormRadio";

            token.Value = "test1|value1";
            Assert.AreEqual("value1", new Library.Tokens.RadioTokenHelper(token).GetValue());
        }
    }
}
