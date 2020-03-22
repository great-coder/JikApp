using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JikApp.Data.Calendar
{
    public enum TYPE
    {
        WORK_DAY,
        DAY_OFF
    }

    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long EventId { get; set; }

        public DateTime DateTime { get; set; }

        public string Title { get; set; }

        public TYPE EventType { get; set; }
    }
}
