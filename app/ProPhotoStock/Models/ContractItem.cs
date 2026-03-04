using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProPhotoStock.Models
{
    public class ContractItem
    {
        public int contractId { get; set; }
        public string photoTitle { get; set; }
        public string photoUrl { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string price { get; set; }
        public string status { get; set; }
        public string photographerCommission { get; set; }
        public int fkUsage { get; set; }
        public int fkType { get; set; }
        public string DisplayDate => endDate.ToString("dd/MM/yyyy");
        public string TypeName => fkType switch
        {
            1 => "Exclusif",
            2 => "Diffusion",
            _ => "Non spécifié"
        };
        public string UsageName => fkUsage switch
        {
            1 => "Publicité",
            2 => "Graphisme",
            3 => "Média",
            _ => "Indéfini"
        };
    }
}
