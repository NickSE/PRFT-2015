using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication1.Model
{
    class Category : Contribution
    {
        public string Name { get; set; }

        public Category(string name)
        {
            this.Name = name;
        }
    }
}
