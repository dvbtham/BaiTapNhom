using Model.EF;

namespace Model.DAO
{
    public class WebsiteDAO
    {
        private itforumEntities db = null;

        public WebsiteDAO()
        {
            db = new itforumEntities();
        }
    }
}