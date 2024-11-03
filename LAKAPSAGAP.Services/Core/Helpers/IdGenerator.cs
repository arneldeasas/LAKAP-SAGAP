using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LAKAPSAGAP.Services.Core.Helpers
{
    public class IdGenerator
    {
        public static string PFX_USERINFO = "ACC_";
        public static string PFX_ATTACHMENT = "ATT_";
        public static string PFX_RELIEFRECEIVED = "BATCH_";

        public static string GenerateId(string prefix,int count)
        {
            try
            {
                var userCount = count+ 1;
                return prefix + userCount.ToString("D3");
            }
            catch (Exception)
            {
                throw;
            }
        }
    
    }
}
