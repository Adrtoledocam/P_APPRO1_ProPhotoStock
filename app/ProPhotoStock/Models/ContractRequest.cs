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
        public string contractType { get; set; }
        public string usageType { get; set; }
    }
}
