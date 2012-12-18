using N2;
using N2.Details;
using N2.Web.UI;

namespace N2Bootstrap.Library.Models
{
    [PartDefinition("Free form",
        Description = "A form that can be sumitted and sent to an email address or viewed online.",
        IconUrl = "{IconsUrl}/report.png")]
    [FieldSetContainer("Email", "Email", 0)]
    public class FreeForm : PartModelBase
    {
        [EditableCheckBox(Title = "Use ajax", DefaultValue = true)]
        public virtual bool UseAjax { get; set; }

        [EditableCheckBox(Title = "Use submit modal", DefaultValue = false)]
        public virtual bool UseModal { get; set; }

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