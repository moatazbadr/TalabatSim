namespace Talabat.Domain.Entities;

public class CustomerBasket
{
    public string Id { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>(); //could be a list later on

    public CustomerBasket(string Id)

    {
        this.Id = Id;

    }


}
