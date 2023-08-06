using System.ComponentModel.DataAnnotations;

namespace TransactionService.Entities;

public class Transaction
{
    [Key]
    public Guid Id { get; set; }
    public int userId { get; set; }
    public int totalItem { get; set; }
    public ICollection<TransactionItem> Items { get; set; }
}
