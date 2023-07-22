using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Models
{
    public class Command
    {
        [Key]
        [Required]
        public required int Id { get; set; }
        [Required]
        public required string HowTo { get; set; }
        [Required]
        public required string CommandLine { get; set; }
        public int PlatformId { get; set; }
        public required Platform Platform { get; set; }
    }
}