using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2;
using N2.Details;

namespace N2Bootstrap.Library.Models
{
    [PartDefinition("Tweets", IconUrl = "{ThemesUrl}/Default/Content/Img/ico-twitter.png")]
    public class Tweets : SidebarPart
    {
        [EditableTextBox(Title = "Twitter handle", HelpTitle = "Leave empty if using a query.", SortOrder = 200)]
        public virtual string TwitterHandle { get; set; }

        [EditableTextBox(Title = "Twitter query", HelpTitle = "Leave empty if using a twitter handle.", HelpText = "See \"http://search.twitter.com/\" for valid search operators.", SortOrder = 201)]
        public virtual string Query { get; set; }

        [EditableCheckBox(Title = "Enable retweets", CheckBoxText = "", SortOrder = 202)]
        public virtual bool EnableRetweets { get; set; }

        [EditableNumber(Title = "Number of tweets", DefaultValue = 3, MinimumValue = "1", MaximumValue = "99", InvalidRangeText = "Please specify a valid number of tweets to display (1-99).", SortOrder = 202)]
        public virtual int NumberOfTweets { get; set; }

        [EditableCheckBox(Title = "Show avatar", CheckBoxText = "", SortOrder = 204)]
        public virtual bool ShowAvatar { get; set; }

        [EditableNumber(Title = "Avatar size", DefaultValue = 46, Required = true, MinimumValue = "16", MaximumValue = "96", InvalidRangeText = "Please specify a valid avatar size (16-96).", SortOrder = 205)]
        public virtual int AvatarSize { get; set; }
    }
}
