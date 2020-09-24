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
        [Required]
        public DateTime CreatedAt { get; set; }
        public Teacher Teacher { get; set; }
    }
}
