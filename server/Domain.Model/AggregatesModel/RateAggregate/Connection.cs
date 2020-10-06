using Domain.Model.AggregatesModel.UserAggregate;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.AggregatesModel.RateAggregate
{
    public class Connection
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
    }
}
