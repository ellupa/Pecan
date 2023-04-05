
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Data.DataModel
{ 
    public static class PecanContext
    {       
        public static string ConnectionString()
        {
           return @"Server=aws.connect.psdb.cloud;Database=smartstore;user=mssznfxvmkzidmqjqyzw;password=pscale_pw_oUkXJR3Yg7505H3FkRUYY1Go4nyyq9pKJDytqhumibM;SslMode=VerifyFull;";
        }
    }
}
