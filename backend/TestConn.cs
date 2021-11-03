using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace temalabor_2021_todo_backend
{
    public static class TestConn
    {
        public static string SqlConnectionString
        {
            // you may edit the connection string here
            get { return @"data source=GERGOLAPTOP\SQLEXPRESS;initial catalog=temalab;integrated security=SSPI"; }
        }
    }
}
