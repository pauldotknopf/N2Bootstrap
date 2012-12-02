namespace N2Bootstrap.Library.Models
{
	/// <summary>
	/// This definition is registered by <see cref="FreeFormRegistration"/>.
	/// </summary>
	public class FreeForm : PartModelBase
	{
		public virtual string Form { get; set; }

		// submit

		public virtual string SubmitText { get; set; }

		// email

		public virtual string MailFrom { get; set; }
		public virtual string MailTo { get; set; }
		public virtual string MailSubject { get; set; }
		public virtual string MailBody { get; set; }
	}
}