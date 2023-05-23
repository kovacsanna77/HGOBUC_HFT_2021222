using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HGOBUC_HFT_2021222.Models
{
    public class Network
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NetworkId { get; set; }
        public string NetworkName { get; set; }

        [JsonIgnore]
        [NotMapped]
        public virtual ICollection<Movie> Movies { get; set; }

        public Network()
        {

        }

        public Network(string line)
        {
            string[] split = line.Split('#');
            NetworkId = int.Parse(split[0]);
            NetworkName = split[1];

        }

        public Network getCopy()
        {

            return new Network()
            {
                NetworkId = this.NetworkId,
                NetworkName = this.NetworkName
            };

        }
    }
}
