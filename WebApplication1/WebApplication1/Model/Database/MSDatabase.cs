using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Model;

namespace WebApplication1.DB
{
    public class MSDatabase : Database
    {

        internal List<Contribution> getContributions()
        {
            return new List<Contribution>()
            {
                new Category("test"),
                new Category("cool"),
                new Message("Hallo!")
            };
        }
    }
}