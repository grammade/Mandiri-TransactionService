using Newtonsoft.Json;

namespace TransactionService.Dtos
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public record FoodRecOrder(List<FoodRecOrderItems> foods, int userId);
    public record FoodRecOrderItems(int id, string name);
}
