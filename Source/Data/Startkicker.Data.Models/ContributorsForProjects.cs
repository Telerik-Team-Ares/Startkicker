namespace Startkicker.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ContributorsForProjects
    {
        [Key, Column(Order = 0)]
        public int ProjectId { get; set; }

        [Key, Column(Order = 1)]
        public string UserId { get; set; }

        public DateTime JoinDate { get; set; }

        public virtual Project Project { get; set; }

        public virtual User User { get; set; }
    }
}
