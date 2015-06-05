using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication1.Model
{
    class Category : Contribution
    {
        public string Name { get; set; }
        public Category Parent { get; set; }

        public Category(Contribution con, string name, Category parent) : base(con)
        {
            this.Name = name;
            this.Parent = parent;
        }
    }
}
