using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Settings
{
    public class Settings : BaseEntity<int>
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
