namespace TestFromOtherGroup.Models.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public DateTime Date { get; set; }
    public double TotalAmount { get; set; }
}

public class CustomerDto
{
    public int Id { get; set; }
    public string FName { get; set; } = string.Empty;
    public string LName { get; set; } = string.Empty;
}

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public double Price { get; set; }
    public int StockQuantity { get; set; }
}