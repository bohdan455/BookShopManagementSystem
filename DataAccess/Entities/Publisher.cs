using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = default!;

        [Required]
        public IdentityUser User { get; set; } = default!;

        [Required]
        public string UserId { get; set; } = default!;

        public List<Book> Books { get; set; } = default!;
    }
}
