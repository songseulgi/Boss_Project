using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTCDProject
{
    public class DATAClass
    {
        String mName = "";
        public DATAClass(String name)
        {
            this.mName = name;
        }

        public String getName()
        {
            return mName;
        }
    }
}