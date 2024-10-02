
using Project.Data;
using Project.Repository.IRepository;
using Project.Models;
using Microsoft.EntityFrameworkCore;
using Project.Controllers;
namespace Project.Repository
{
    public class TruckGoodsRepository : ITruckGoodsRepository
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<GateOperatorDashboardController> _logger;

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

        public async Task<IEnumerable<TruckGoodsModel>> GetDaysGoodsAsync()
        {
            return await _context.TruckGoods
           .Where(t => t.ArrivalTime.Date == DateTime.Today )
           .ToListAsync();
        }


        public async Task<TruckGoodsModel> GetByIdAsync(int id)
        {
            try
            {
                var truckGoods = await _context.TruckGoods
                                               .Include(t => t.Documents)
                                               .FirstOrDefaultAsync(t => t.SerialNo == id);

                if (truckGoods == null)
                {
                    throw new Exception($"TruckGoods with ID {id} not found.");
                }

                return truckGoods;
            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred while retrieving the TruckGoods.");
            }
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
