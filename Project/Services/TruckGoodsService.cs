using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Repository.IRepository;

namespace Project.Services
{
    public class TruckGoodsService
    {
        private readonly ITruckGoodsRepository _truckGoodsRepository;

        public TruckGoodsService(ITruckGoodsRepository truckGoodsRepository)
        {
            _truckGoodsRepository = truckGoodsRepository;
        }
        public async Task<IEnumerable<TruckGoodsModel>> GetInternationalGoodsAsync()
        {
            return await _truckGoodsRepository.GetInternationalGoodsAsync();
        }

        public async Task ApproveGood(int id)
        {
            var truckGoods = await _truckGoodsRepository.GetByIdAsync(id);
            if (truckGoods != null && truckGoods.Type == "International")
            {
                truckGoods.IsCustomsApproved = true;
                await _truckGoodsRepository.SaveAsync();
            }
          
        }



        public async Task RejectGood(int id)
        {
            await _truckGoodsRepository.DeleteAsync(id);
        }


        public async Task CreateTruckGoodsAsync(TruckGoodsModel truckGoodsModel)
        {
            if (truckGoodsModel.Documents != null && truckGoodsModel.Documents.Count > 0)
            {
                foreach (var document in truckGoodsModel.Documents)
                {
                    if (document.DocumentFile != null && document.DocumentFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                        if (!Directory.Exists(uploadsFolder))
                        {
                            Directory.CreateDirectory(uploadsFolder);
                        }

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(document.DocumentFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await document.DocumentFile.CopyToAsync(stream);
                        }

                        document.DocumentPath = "/uploads/" + uniqueFileName;
                    }
                }
            }

            if (truckGoodsModel.Type == "Domestic")
            {
                truckGoodsModel.IsCustomsApproved = true;
            }

            truckGoodsModel.ArrivalTime = DateTime.Now;

            await _truckGoodsRepository.AddAsync(truckGoodsModel);
            await _truckGoodsRepository.SaveAsync();
        }
    }
}
