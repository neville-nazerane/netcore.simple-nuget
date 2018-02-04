using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetCore.Simple.Models
{
    public class InputInfo
    {

        public string Name { get; set; }

        private string _DataType;
        public string DataType
        {
            get { return _DataType ?? "text"; }
            set { _DataType = value; }
        }

        private string _Label;
        public string Label
        {
            get
            {
                return _Label ??
                  Name[0].ToString().ToUpper()
                          + Regex.Replace(Name.Substring(1), @"\B[A-Z]", c => " " + c);
            }
            set { _Label = value; }
        }

        public bool IsRequired { get; set; }

    }
}
