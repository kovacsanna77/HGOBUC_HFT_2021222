using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Models
{
   public  class Network
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NetworkId { get; set; }
        public string NetworkName { get;   set; }
        public virtual ICollection<Movie> Movies{ get; set; }
    }
}
