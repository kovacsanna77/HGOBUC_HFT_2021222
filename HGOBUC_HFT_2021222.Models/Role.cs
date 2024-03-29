﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HGOBUC_HFT_2021222.Models
{
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public int Priority { get; set; }
        public string RoleName { get; set; }
        public int MovieId { get; set; }
        public int ActorId { get; set; }


        public virtual Actors Actor { get;  set; }

        [JsonIgnore]
        public virtual Movie Movie { get;  set; }
        public Role()
        {

        }

        public Role(string line)
        {
            string[] split = line.Split('#');
            RoleId = int.Parse(split[0]);
            MovieId = int.Parse(split[1]);
            ActorId = int.Parse(split[2]);
            Priority = int.Parse(split[3]);
            RoleName = split[4];
        }

        public Role getCopy()
        {
            
                return new Role()
                {
                    RoleId = this.RoleId,
                    RoleName = this.RoleName,
                    Priority = this.Priority
                };
            
        }
    }
}
