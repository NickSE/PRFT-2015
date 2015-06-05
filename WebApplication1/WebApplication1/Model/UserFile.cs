using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication1.Model
{
    class UserFile : Contribution
    {
        public string Path { get; set; }

        public int Size { get; set; }

        public Category Belongs { get; set; }
     

        public UserFile(Contribution con, string path, int size, Category belongs) : base(con)
        {
            this.Path = path;
            this.Size = size;
            this.Belongs = belongs;
        }

        public string getType()
        {
            string[] split = Path.Split('.');
            return split[split.Length - 1];
        }
    }
}
