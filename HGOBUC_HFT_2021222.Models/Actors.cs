using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Models
{
    public class Actors
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActorId { get; set; }  

       public string ActorName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public Actors()
        {

        }

        public Actors(string line)
        {
            string[] split = line.Split('#');
            ActorId = int.Parse(split[0]);
            ActorName = split[1];
        }
    }
}
