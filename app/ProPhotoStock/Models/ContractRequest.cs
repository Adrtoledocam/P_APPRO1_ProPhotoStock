using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPhotoStock.Models
{
    public class ContractRequest
    {
        public int fkPhoto { get; set; }
        public int fkUsage { get; set; }
        public int fkType { get; set; }
    }
}
