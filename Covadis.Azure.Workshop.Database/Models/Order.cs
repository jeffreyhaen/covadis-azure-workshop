namespace Covadis.Azure.Database.Models;

public class Order
{
    public int Id { get; set; }

    public string ArticleNumber { get; set; }

    public decimal TotalPrice { get; set; }

    public int Quantity { get; set; }

    public bool IsPaid { get; set; }

    public bool IsShipped { get; set; }
}
