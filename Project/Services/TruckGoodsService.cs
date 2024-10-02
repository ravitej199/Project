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
            try
            {
                return await _truckGoodsRepository.GetInternationalGoodsAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve international goods.", ex);
            }
        }

        public async Task ApproveGood(int id)
        {

            var truckGoods = await _truckGoodsRepository.GetByIdAsync(id);
            if (truckGoods == null)
            {
                throw new ArgumentNullException(nameof(truckGoods), "Truck goods not found.");
            }

            if (truckGoods.Type != "International")
            {
                throw new InvalidOperationException("Only international goods can be approved.");
            }

            truckGoods.ApprovalStatus = ApprovalStatus.Approved;
            await _truckGoodsRepository.SaveAsync();

        }



        public async Task RejectGood(int id)
        {
            var truckGoods = await _truckGoodsRepository.GetByIdAsync(id);
            if (truckGoods == null)
            {
                throw new ArgumentNullException(nameof(truckGoods), "Truck goods not found.");
            }
            truckGoods.ApprovalStatus = ApprovalStatus.Rejected;
            await _truckGoodsRepository.SaveAsync();
        }


        public async Task CreateTruckGoodsAsync(TruckGoodsModel truckGoodsModel)
        {
            try
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
                    truckGoodsModel.ApprovalStatus = ApprovalStatus.Approved;
                }

                truckGoodsModel.ArrivalTime = DateTime.Now;

                await _truckGoodsRepository.AddAsync(truckGoodsModel);
                await _truckGoodsRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to create TruckGoods.", ex);
            }
        }

        public async Task<IEnumerable<TruckGoodsModel>> GetDaysGoodsAsync()
        {
            try
            {
                return await _truckGoodsRepository.GetDaysGoodsAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve international goods.", ex);
            }
        }

        internal async Task<TruckGoodsModel> GetDetails(int invoiceNo)
        {
            try
            {
                var truckGoods = await _truckGoodsRepository.GetByIdAsync(invoiceNo);
                return truckGoods;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Failed to retrieve goods details.", ex);
            }
    
        }
    }
}
