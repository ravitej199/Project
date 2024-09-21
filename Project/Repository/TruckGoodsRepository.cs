
using Project.Data;
using Project.Repository.IRepository;
using Project.Models;
using Microsoft.EntityFrameworkCore;
namespace Project.Repository
{
    public class TruckGoodsRepository : ITruckGoodsRepository
    {
        private readonly ApplicationDbContext _context;

        public TruckGoodsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TruckGoodsModel>> GetInternationalGoodsAsync()
        {
            return await _context.TruckGoods
           .Where(t => t.Type == "International")
           .ToListAsync();
        }

        public async Task<TruckGoodsModel> GetByIdAsync(int id)
        {
            return await _context.TruckGoods.FindAsync(id);
        }

        public async Task AddAsync(TruckGoodsModel truckGoods)
        {
            await _context.TruckGoods.AddAsync(truckGoods);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var good = await _context.TruckGoods.FindAsync(id);
            if (good != null)
            {
                _context.TruckGoods.Remove(good);
                await _context.SaveChangesAsync();
            }
        }
    }
}
