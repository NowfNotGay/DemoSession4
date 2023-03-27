using System;
using System.Collections.Generic;

namespace DemoSession4_MVC.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public int Quantity { get; set; }

    public string? Description { get; set; }

    public bool Status { get; set; }

    public string? Photo { get; set; }

    public DateTime Created { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<InvoiceDetail> InvoiceDetails { get; } = new List<InvoiceDetail>();
}
