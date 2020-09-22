using System;
using System.ComponentModel.DataAnnotations;

namespace Proffy.Domain.AggregatesModel.UserAggregate
{
    public class Connection
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
    }
}
