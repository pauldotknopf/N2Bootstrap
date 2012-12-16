using N2;
using N2.Details;
using N2.Web.UI;

namespace N2Bootstrap.Library.Models
{
    //register.Part(title: "Free form", description: "A form that can be sumitted and sent to an email address or viewed online.");

    //        register.ControlledBy<FreeFormController>();

    //        register.Definition.SortOrder = 250;
    //        register.Icon("{IconsUrl}/report.png");

    //        register.On(ff => ff.Form).FreeText("Form (with tokens)").Configure(eft =>
    //        {
    //            eft.HelpTitle = "This text supports tokens";
    //            eft.HelpText = "{{FormCheckbox}}, {{FormFile}}, {{FormInput}}, {{FormRadio}}, {{FormSelect}}, {{FormSubmit}}, {{FormTextarea}}";
    //        }).WithTokens();
    //        register.On(ff => ff.SubmitText).FreeText("Thank you text");

    //        using (register.FieldSetContainer("Email", "Email").Begin())
    //        {
    //            register.On(ff => ff.MailFrom).Text("Mail from").Placeholder("something@mycompany.com");
    //            register.On(ff => ff.MailTo).Text("Mail to").Placeholder("someone@mycompany.com");
    //            register.On(ff => ff.MailSubject).Text("Mail subject").Placeholder("Mail title");
    //            register.On(ff => ff.MailBody).Text("Mail intro text").Placeholder("Mail text before form answers")
    //                .Configure(et => et.TextMode = TextBoxMode.MultiLine);
    //        }

    [PartDefinition("Free form", 
        Description = "A form that can be sumitted and sent to an email address or viewed online.",
        IconUrl="{IconsUrl}/report.png")]
    [FieldSetContainer("Email", "Email", 0)]
	public class FreeForm : PartModelBase
	{
        [EditableFreeTextArea(Title = "Form (with tokens)", 
            HelpTitle = "This text supports tokens", 
            HelpText = "{{FormCheckbox}}, {{FormFile}}, {{FormInput}}, {{FormRadio}}, {{FormSelect}}, {{FormSubmit}}, {{FormTextarea}}")]
        [DisplayableTokens]
		public virtual string Form { get; set; }

        [EditableFreeTextArea(Title = "Thank you text")]
		public virtual string SubmitText { get; set; }

        [EditableTextBox(Title = "Mail from", Placeholder = "something@mycompany.com")]
		public virtual string MailFrom { get; set; }

        [EditableTextBox(Title = "Mail to", Placeholder = "something@mycompany.com")]
		public virtual string MailTo { get; set; }

        [EditableTextBox(Title = "Mail subject", Placeholder = "Mail title")]
		public virtual string MailSubject { get; set; }

        [EditableTextBox(Title = "Mail intro text", Placeholder = "Mail text before form answers")]
		public virtual string MailBody { get; set; }
	}
}