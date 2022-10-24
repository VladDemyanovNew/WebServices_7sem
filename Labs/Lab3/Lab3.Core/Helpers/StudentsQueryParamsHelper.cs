using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab3.Core.Models;

namespace Lab3.Core.Helpers
{
    public static class StudentsQueryParamsHelper
    {
        public static void Fill(StudentsQueryParams src, StudentsQueryParams dest)
        {
            dest.GlobalLike = src.GlobalLike;
            dest.Limit = src.Limit;
            dest.Like = src.Like;
            dest.Offset = src.Offset;
            dest.Columns = src.Columns;
            dest.MaxId = src.MaxId;
            dest.Sort = src.Sort;
            dest.MinId = src.MinId;
        }
    }
}
