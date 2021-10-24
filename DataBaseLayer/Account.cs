using System;
using System.Collections.Generic;

#nullable disable

namespace DataBaseLayer
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public virtual Profile Profile { get; set; }
    }
}
