using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Details;
using N2Bootstrap.Library.Adapters;

namespace N2Bootstrap.Library.Models
{
    [PartDefinition("Text",
        IconUrl = "{IconsUrl}/text_align_left.png")]
    public class TextPart : PartModelBase
    {
        /// <summary>
        /// The text to render on the page
        /// </summary>
        [EditableFreeTextArea]
        public virtual string Text { get; set; }

        public override object this[string detailName]
        {
            set
            {
                base[detailName] = value;
            }
        }
    }
}
