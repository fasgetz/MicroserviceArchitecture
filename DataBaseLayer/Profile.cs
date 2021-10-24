using System;
using System.Collections.Generic;

#nullable disable

namespace DataBaseLayer
{
    public partial class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte? IdDepartment { get; set; }

        public virtual Department Department { get; set; }
        public virtual Account Account { get; set; }
    }
}
