using Domain.Model.AggregatesModel.RateAggregate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.AggregatesModel.UserAggregate
{
    public class Teacher : User
    {
        public string Whatsapp { get; set; }
        [StringLength(255)]
        public string Facebook { get; set; }
        public string Bio { get; set; }
        public ICollection<Connection> Connections { get; set; }
    }
}
