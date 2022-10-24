using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.Core.Models
{
    public class StudentsQueryParams
    {
        public int Limit { get; set; } = 1000;

        public bool Sort { get; set; }

        public int Offset { get; set; }

        public int MinId { get; set; }

        public int MaxId { get; set; } = -1;

        public string Like { get; set; }

        public string Columns { get; set; }

        public string GlobalLike { get; set; }

        public string ToQueryString()
        {
            var maxId = this.MaxId < 0 ? string.Empty : $"maxid={this.MaxId}";
            return string.Format(
                "limit={0}&sort={1}&offset={2}&minid={3}&{4}&like={5}&columns={6}&globallike={7}",
                this.Limit, this.Sort, this.Offset, this.MinId, maxId, this.Like, this.Columns, this.GlobalLike
            );
        }
    }
}