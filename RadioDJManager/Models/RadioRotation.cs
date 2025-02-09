using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadioDJManager.Models
{
    [Table("rotation")]
    public  class RadioRotation
    {
        public int ID { get; set; }
        public string name { get; set; }
    }
}
