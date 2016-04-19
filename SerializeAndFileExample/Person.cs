using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SerializeAndFileExample
{
    [Serializable]
    [DataContract]
    class Person
    {
        [DataMember(Name = "firstName")]
        internal string FirstName { get; set; }
        [DataMember(Name = "lastName")]
        internal string LastName { get; set; }
    }
}
