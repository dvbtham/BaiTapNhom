using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class WebsiteDAO
    {
        itforumEntities db = null;
        public WebsiteDAO()
        {
            db = new itforumEntities();
        }
        
    }
}
