using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab3.WebApi.Models.Resources
{
    public class LinkedResourceCollection<TResource> : LinkedResource
        where TResource : LinkedResource
    {
        public ICollection<TResource> Embedded { get; set; }
    }
}