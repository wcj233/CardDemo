using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardDemo.Model
{
    public class CardContent
    {
        private string contenttitle;

        public string ContentTitle
        {
            get { return contenttitle; }
            set { contenttitle = value; }
        }

        private string contentdetail;

        public string ContentDetail
        {
            get { return contentdetail; }
            set { contentdetail = value; }
        }

        private string contentcolor;

        public string ContentColor
        {
            get { return contentcolor; }
            set { contentcolor = value; }
        }


    }
}
