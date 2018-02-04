using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Simple.Models
{
    public class Editor
    {

        public string SubmitURL { get; set; }

        public object Data { get; set; }

        private bool? _IsEditing;
        public bool IsEditing
        {
            get { return _IsEditing ?? Data != null; }
            set { _IsEditing = value; }
        }

        public string SubmitText
        {
            get
            {
                return IsEditing ? "Update" : "Add";
            }
        }

        private string _Method;
        public string Method
        {
            get { return _Method ?? (IsEditing ? "PUT" : "POST"); }
            set { _Method = value; }
        }



    }
}
