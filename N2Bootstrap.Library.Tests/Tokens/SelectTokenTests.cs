using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace N2Bootstrap.Library.Tests.Tokens
{
    [TestClass]
    public class SelectTokenTests
    {
        [TestMethod]
        public void Can_invalidate()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormSelect";

            token.Value = "";
            Assert.IsFalse(new Library.Tokens.SelectTokenHelper(token).IsValid);

            token.Value = null;
            Assert.IsFalse(new Library.Tokens.SelectTokenHelper(token).IsValid);

            token.Value = "test|";
            Assert.IsFalse(new Library.Tokens.SelectTokenHelper(token).IsValid);

            token.Value = "test|test2|test3";
            Assert.IsFalse(new Library.Tokens.SelectTokenHelper(token).IsValid);

            token.Value = "test|test2|test3,test4|test5";
            Assert.IsFalse(new Library.Tokens.SelectTokenHelper(token).IsValid);

            token.Value = "|option1,option2";
            Assert.IsFalse(new Library.Tokens.SelectTokenHelper(token).IsValid);
        }

        [TestMethod]
        public void Can_validate()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormSelect";

            token.Value = "test|option";
            Assert.IsTrue(new Library.Tokens.SelectTokenHelper(token).IsValid);

            token.Value = "test|option,option2";
            Assert.IsTrue(new Library.Tokens.SelectTokenHelper(token).IsValid);

            token.Value = "test|option,option2,";
            Assert.IsTrue(new Library.Tokens.SelectTokenHelper(token).IsValid);
        }

        [TestMethod]
        public void Can_get_form_name()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormSelect";

            token.Value = "test1|option1";
            Assert.AreEqual("test1", new Library.Tokens.SelectTokenHelper(token).GetFormName());

            token.Value = "test2|option2,option3";
            Assert.AreEqual("test2", new Library.Tokens.SelectTokenHelper(token).GetFormName());
        }

        [TestMethod]
        public void Can_get_options()
        {
            var token = new N2.Web.Rendering.DisplayableToken();
            token.Name = "FormSelect";

            token.Value = "test1|option1";
            Assert.IsTrue(new[] { "option1" }.SequenceEqual(new Library.Tokens.SelectTokenHelper(token).GetOptions()));

            token.Value = "test1|option1,option2";
            Assert.IsTrue(new[] { "option1", "option2" }.SequenceEqual(new Library.Tokens.SelectTokenHelper(token).GetOptions()));

            token.Value = "test1|,option1,,option2,";
            Assert.IsTrue(new[] { "", "option1", "", "option2", "" }.SequenceEqual(new Library.Tokens.SelectTokenHelper(token).GetOptions()));
        }
    }
}
