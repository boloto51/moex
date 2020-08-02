using moex.DbContext;
using moex.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace moex
{
    public class DataBaseGet
    {
        public List<Security> FromSecurityTable()
        {
            DataContext context = new DataContext();
            return context.Securities.Select(a => a).ToList();
        }
    }
}
