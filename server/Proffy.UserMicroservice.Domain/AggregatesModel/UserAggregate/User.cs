using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proffy.UserMicroservice.Domain.AggregatesModel.UserAggregate
{
    public class User
    {
        public Guid Id { get; set; }
        [StringLength(40)]
        public string Email { get; set; }
        [StringLength(40)]
        public string Password { get; set; }
        [StringLength(120)]
        public string Name { get; set; }
        [StringLength(255)]
        public string Photo { get; set; }
        [StringLength(13)]
        public string Whatsapp { get; set; }
        [StringLength(255)]
        public string Facebook { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Actived { get; set; }
        public ICollection<Connection> Connections { get; set; }
    }
}
