using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace VsePikchi.Models
{
    public class Picture
    {
        public int Id { get; set; }

        public string Url { get; set; }

        [Column(TypeName = "Datetime")]
        public DateTime? CreationDate { get; set; }
    }
}
