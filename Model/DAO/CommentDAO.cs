﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.EF;
using PagedList;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CommentDAO
    {
        itforumEntities db = null;
        public CommentDAO()
        {
            db = new itforumEntities();
        }
        public IEnumerable<Comment> GetCommentList(long id, int page, int pageSize)
        {
            IQueryable<Comment> model = db.Comments;
            model = model.Where(x => x.PostID == id);
            return model.OrderByDescending(x => x.CommentedDate).ToPagedList(page, pageSize);
        }
        public int GetItemNumber(long id)
        {
            var post = db.Comments.Count(x => x.CommentID == id);
            return post;
        }
    }
}
