using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Details;

namespace N2Bootstrap.Library.Models
{
    [PartDefinition("Hero", IconUrl = "{IconsUrl}/page_white_text.png")]
    [WithEditableTitle]
    public class Hero : PartModelBase
    {
        [EditableFreeTextArea]
        public virtual string Text { get; set; }
    }
}
