using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.DTOs
{
    public class CreateRoleDto
    {
        [Required(ErrorMessage ="Role không được để trống")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Role phải có ít nhất 50 kí tự.")]
        [NotNull]
        public string Name { get; set; }
    }

}
