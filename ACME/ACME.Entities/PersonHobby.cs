using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACME.Entities
{
    public class PersonHobby
    {
        public int PersonId { get; set; }
        public int HobbyId { get; set; }

        public Hobby? Hobby { get; set; }
        public Person? Person { get; set; }
    }
}
