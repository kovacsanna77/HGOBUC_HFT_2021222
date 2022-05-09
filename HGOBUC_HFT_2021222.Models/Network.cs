using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Models
{
   public  class Network
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NetworkId { get; set; }
        public string NetworkName { get;   set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Movie> Movies{ get; set; }

        public Network()
        {
            Movies = new HashSet<Movie>();
        }

        public Network(string line)
        {
            string[] split = line.Split('#');
            NetworkId = int.Parse(split[0]);
            NetworkName = split[1];
            Movies = new HashSet<Movie>();
        }
    }
}
