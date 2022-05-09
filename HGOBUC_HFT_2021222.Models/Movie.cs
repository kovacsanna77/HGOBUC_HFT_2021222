using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGOBUC_HFT_2021222.Models
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }
        [Required]
        [MinLength(3)]
        public string Title { get; set; }
        public int Aired { get; set; }
        public int Episodes { get; set; }
        public int Duration { get; set; }
        public int NetworkId { get; set; }
        [Range(0, 10)]
        public int Rating { get; set; }
        public virtual Network Network { get; set; }
        public virtual ICollection<Actors> Actors { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public Movie()
        {

        }
        public Movie(string line)
        {
            string[] split = line.Split('#');
            MovieId = int.Parse(split[0]);
            Title= split[1];
            Aired = int.Parse(split[2]);
            Episodes = int.Parse(split[3]);
            Duration = int.Parse(split[4]);
            NetworkId = int.Parse(split[5]);
            Rating = int.Parse(split[6]);
        }

    }
}
