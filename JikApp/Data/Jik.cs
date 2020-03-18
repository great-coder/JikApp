using System;
using System.ComponentModel.DataAnnotations;

namespace JikApp.Data
{
    public class Jik
    {
        [Key]
        public string JikId { get; set; } = KeyGenerator.CreateId();

        [Required]
        public string SenderId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created_At { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
