using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TermProjectLogin
{
    public partial class ColorPicker : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public string Color
        {
            get { return clrPicker.Value; }
            set { clrPicker.Value = value; }
        }
    }
}