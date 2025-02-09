using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadioDJManager.Models
{
    [Table("events_categories")]
    public  class EventCategories
    {
        public int ID { get; set; }
        public string name { get; set; }
    }
}
