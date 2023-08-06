using System.ComponentModel.DataAnnotations;

namespace TransactionService.Entities;

public class TransactionItem
{
    [Key]
    public Guid Id { get; set; }
    public Guid TransactionId { get; set; }
    public int FoodId { get; set; }
    public string FoodName { get; set; }
}
