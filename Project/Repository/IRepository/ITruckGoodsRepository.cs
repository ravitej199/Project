using Project.Models;
namespace Project.Repository.IRepository
{
    public interface ITruckGoodsRepository
    {
        Task<IEnumerable<TruckGoodsModel>> GetInternationalGoodsAsync();
        Task<TruckGoodsModel> GetByIdAsync(int id);
        Task AddAsync(TruckGoodsModel truckGoods);
        Task SaveAsync();
        Task<IEnumerable<TruckGoodsModel>> GetDaysGoodsAsync();
        Task DeleteAsync(int id);
    }
}
