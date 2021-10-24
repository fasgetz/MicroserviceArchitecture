using System;
using System.Collections.Generic;

#nullable disable

namespace DataBaseLayer
{
    public partial class Department
    {
        public Department()
        {
            Profiles = new HashSet<Profile>();
        }

        public byte Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }
    }
}
