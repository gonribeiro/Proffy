using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proffy.Domain.AggregatesModel.UserAggregate
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
