using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace G2CarRentalCustomers.Models;

[Table("Customer")]
public partial class G2Customer
{
    [Key]
    [Required]
    public int Id { get; set; }

	[StringLength(100)]
	[Required]
    public string FirstName { get; set; } = null!;

	[StringLength(100)]
	public string LastName { get; set; } = null!;

    [StringLength(20)]  
    public string Phone { get; set; } = null!;

    [StringLength(255)]
    public string Email { get; set; } = null!;
}
