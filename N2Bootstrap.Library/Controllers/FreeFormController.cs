using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Mvc;
using N2.Web;
using N2.Web.Mail;
using N2.Web.Mvc;
using N2.Web.Rendering;
using N2Bootstrap.Library.Models;
using N2Bootstrap.Library.Tokens;

namespace N2Bootstrap.Library.Controllers
{
    [Controls(typeof(FreeForm))]
    public class FreeFormController : ContentController<FreeForm>
    {
        private readonly IMailSender mailSender;

        public FreeFormController(IMailSender mailSender)
        {
            this.mailSender = mailSender;
        }

        public override ActionResult Index()
        {
            bool formSubmit;
            if (Boolean.TryParse(Convert.ToString(TempData["FormSubmit"]), out formSubmit) && formSubmit)
                return PartialView("Submitted");

            if (CurrentItem.GetTokens("Form").Any(dt => dt.Is("FormSubmit")))
                return PartialView("Form");
            else
                return PartialView();
        }

        public ActionResult Submit(FormCollection collection)
        {
            var mm = new MailMessage(CurrentItem.MailFrom, CurrentItem.MailTo.Replace(";", ","));
            mm.Subject = CurrentItem.MailSubject;
            mm.Headers["X-FreeForm-Submitter-IP"] = Request.UserHostName;
            mm.Headers["X-FreeForm-Submitter-Date"] = DateTime.Now.ToString();
            using (var sw = new StringWriter())
            {
                sw.WriteLine(CurrentItem.MailBody);

                var tokens = CurrentItem.GetTokens("Form").ToList();

                // these are processed in goups
                var radioTokens = new Dictionary<string, List<DisplayableToken>>();
                var checkboxTokens = new Dictionary<string, List<DisplayableToken>>();

                foreach (var radioToken in tokens.Where(x => x.Is("FormRadio")))
                {
                    var helper = new RadioTokenHelper(radioToken);
                    if (!helper.IsValid)
                        continue;
                    if (radioTokens.ContainsKey(helper.GetFormName()))
                        radioTokens[helper.GetFormName()].Add(radioToken);
                    else
                        radioTokens.Add(helper.GetFormName(), new List<DisplayableToken> {radioToken});
                }

                foreach (var checkboxToken in tokens.Where(x => x.Is("FormCheckbox")))
                {
                    var helper = new CheckboxTokenHelper(checkboxToken);
                    if (!helper.IsValid)
                        continue;
                    if (checkboxTokens.ContainsKey(helper.GetFormName()))
                        checkboxTokens[helper.GetFormName()].Add(checkboxToken);
                    else
                        checkboxTokens.Add(helper.GetFormName(), new List<DisplayableToken> { checkboxToken });
                }

                foreach (var token in tokens.Where(dt => dt.Name.StartsWith("Form", StringComparison.InvariantCultureIgnoreCase)))
                {
                    if (token.Is("FormSubmit"))
                        continue;

                    if (token.Is("FormCheckbox"))
                    {
                        var helper = new CheckboxTokenHelper(token);
                        if (!helper.IsValid)
                            continue;

                        // only process if it is the last checkbox with this name
                        var name = helper.GetFormName();
                        var checkboxes = checkboxTokens[name];
                        if(checkboxes.IndexOf(token) == (checkboxes.Count - 1))
                        {
                            sw.WriteLine(name + ": " + collection[name]);
                        }
                    }else if (token.Is("FormFile"))
                    {
                        var helper = new FileTokenHelper(token);
                        if (!helper.IsValid)
                            continue;

                        var name = helper.GetFormName();
                        if (Request.Files[name] == null)
                            continue;

                        var postedFile = Request.Files[name];
                        if (postedFile.ContentLength == 0)
                            continue;

                        var fileName = Path.GetFileName(postedFile.FileName);
                        sw.WriteLine(name + ": " + fileName + " (" + (int)(postedFile.ContentLength / 1024) + "kB)");
                        mm.Attachments.Add(new Attachment(postedFile.InputStream, fileName, postedFile.ContentType));
                    }else if (token.Is("FormInput"))
                    {
                        var helper = new InputTokenHelper(token);
                        if (!helper.IsValid)
                            continue;

                        var name = helper.GetFormName();
                        sw.WriteLine(name + ": " + collection[name]);
                    }else if (token.Is("FormRadio"))
                    {
                        var helper = new RadioTokenHelper(token);
                        if (!helper.IsValid)
                            continue;

                        // only process if it is the last radio button with this name
                        var name = helper.GetFormName();
                        var radios = radioTokens[name];
                        if (radios.IndexOf(token) == (radios.Count - 1))
                        {
                            sw.WriteLine(name + ": " + collection[name]);
                        }
                    }else if (token.Is("FormSelect"))
                    {
                        var helper = new SelectTokenHelper(token);
                        if (!helper.IsValid)
                            continue;

                        var name = helper.GetFormName();
                        sw.WriteLine(name + ": " + collection[name]);
                    }else if (token.Is("FormTextarea"))
                    {
                        var helper = new FreeTextareaTokenHelper(token);
                        if (!helper.IsValid)
                            continue;

                        var name = helper.GetFormName();
                        sw.WriteLine(name + ": " + collection[name]);
                    }

                    
                    //{
                    //    continue;
                    //}
                    //else if (token.Is("FormTextarea"))
                    //{
                        
                    //    sw.WriteLine(name + ": " + collection[name]);
                    //}
                    //else if (token.Is("FormFile"))
                    //{
                    //    name = token.Value ?? token.GenerateInputName();

                    //    if (Request.Files[name] == null)
                    //        continue;

                    //    var postedFile = Request.Files[name];
                    //    if (postedFile.ContentLength == 0)
                    //        continue;

                    //    var fileName = Path.GetFileName(postedFile.FileName);
                    //    sw.WriteLine(name + ": " + fileName + " (" + (int) (postedFile.ContentLength/1024) + "kB)");
                    //    mm.Attachments.Add(new Attachment(postedFile.InputStream, fileName, postedFile.ContentType));
                    //}
                    //else if (token.Is("FormInput") || token.Is("FormTextarea"))
                    //{
                    //    var inputName = token.Value ?? token.GenerateInputName();
                    //    var value = collection[inputName];
                    //    sw.WriteLine(name + ": " + value);
                    //}
                    //else if (token.Is("FormCheckbox"))
                    //{
                    //    var inputName = token.Value ?? token.GenerateInputName();
                    //    var value = collection.Keys.Cast<string>().Contains(inputName);
                    //    sw.WriteLine(name + ": " + value);
                    //}
                    //else
                    //{
                    //    var inputName = token.GetOptionalInputName(0, 1);
                    //    var value = collection[inputName];
                    //    sw.WriteLine(name + ": " + value);
                    //}
                }

                mm.Body = sw.ToString();
            }

            mailSender.Send(mm);

            TempData.Add("FormSubmit", true);

            return RedirectToParentPage();
        }
    }
}
