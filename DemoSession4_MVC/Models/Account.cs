using System;
using System.Collections.Generic;

namespace DemoSession4_MVC.Models;

public partial class Account
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public bool Status { get; set; }
    public string? SecurityCode { get; set; }
    public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}
