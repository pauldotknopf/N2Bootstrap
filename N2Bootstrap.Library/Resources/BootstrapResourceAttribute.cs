using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using N2.Plugin;
using N2.Security;

namespace N2Bootstrap.Library.Resources
{
    [AttributeUsage(AttributeTargets.Assembly,AllowMultiple=true)]
    public class BootstrapResourceAttribute : Attribute, IPlugin
    {
        private string _themedResourceLocation;
        private ResourceTypeEnum _resourceType = ResourceTypeEnum.CssOrLess;    // guess?
        private ResponsiveModeEnum _responsiveMode = ResponsiveModeEnum.Both;
        private int _sortOrder;

        public BootstrapResourceAttribute(string themedResourceLocation, int sortOrder = 0)
        {
            if (string.IsNullOrEmpty(themedResourceLocation))
                throw new ArgumentNullException("themedResourceLocation", "You must provide a themed resource location");

            var extension = Path.GetExtension(themedResourceLocation);

            if (!string.IsNullOrEmpty(extension))
            {
                switch (extension.ToLower())
                {
                    case ".less":
                    case ".css":
                        _resourceType = ResourceTypeEnum.CssOrLess;
                        break;
                    case ".js":
                        _resourceType = ResourceTypeEnum.Javascript;
                        break;
                }
            }

            _themedResourceLocation = themedResourceLocation;
            _sortOrder = sortOrder;

            Name = _themedResourceLocation.ToLower();
        }

        #region Properties

        public string ThemedResourceLocation
        {
            get { return _themedResourceLocation; }
        }

        public ResourceTypeEnum ResourceType
        {
            get { return _resourceType; }
            set { _resourceType = value; }
        }

        public ResponsiveModeEnum ResponsiveMode
        {
            get { return _responsiveMode; }
            set { _responsiveMode = value; }
        }

        #endregion

        #region IPlugin

        public Type Decorates { get; set; }

        public string Name { get; set; }

        public int SortOrder
        {
            get { return _sortOrder; }
            set { _sortOrder = value; }
        }

        public int CompareTo(IPlugin other)
        {
            if (other == null)
                return 1;
            var result = SortOrder.CompareTo(other.SortOrder) * 2 + Name.CompareTo(other.Name);
            return result;
        }

        #endregion

        #region Enums

        public enum ResponsiveModeEnum
        {
            Responsive,
            NotResponsive,
            Both
        }

        public enum ResourceTypeEnum
        {
            CssOrLess,
            Javascript
        }

        #endregion

        
    }
}
