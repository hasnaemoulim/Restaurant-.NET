namespace UniversDélices.Repositories
{
    public interface ICartRepository
    {
        Task<int> AddItem(int platId, int qty);
        Task<int> RemoveItem(int platId);
        Task<Cart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<Cart> GetCart(string userId);
        Task<bool> DoCheckout();
    }
}