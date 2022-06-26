using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tangy_Business.Repository.IRepository;
using Tangy_DataAccess;
using Tangy_DataAccess.Data;
using Tangy_Models;
namespace Tangy_Business.Repository
{
    public class ProductPriceRepository : IProductPriceRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        //dependency Injection
        public ProductPriceRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductPriceDTO> Create(ProductPriceDTO objDTO)
        {
            var obj = _mapper.Map<ProductPriceDTO, ProductPrice>(objDTO);
            ////Aggregate the category obj to the database
            var addedObj = _db.ProductsPrices.Add(obj);
            //The save confirm the changes in the db, its obligatory to perform changes in db
            _db.SaveChanges();
            return _mapper.Map<ProductPrice, ProductPriceDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.ProductsPrices.FirstOrDefaultAsync(p => p.Id == id);
            if (obj is not null)
            {
                _db.ProductsPrices.Remove(obj);
                await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductPriceDTO> Get(int id)
        {
            var obj = await _db.ProductsPrices.Include(p => p.Price).FirstOrDefaultAsync(p => p.Id == id);
            if (obj is not null)
            {
                return _mapper.Map<ProductPrice, ProductPriceDTO>(obj);
            }
            return new ProductPriceDTO();
        }

        public async Task<IEnumerable<ProductPriceDTO>> GetAll(int? id = null)
        {
            if (id is not null && id > 0)
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>
                    (_db.ProductsPrices.Where(u => u.ProductId==id));
            }
            else
            {
                return _mapper.Map<IEnumerable<ProductPrice>, IEnumerable<ProductPriceDTO>>(_db.ProductsPrices);
            }
        }

        public async Task<ProductPriceDTO> Update(ProductPriceDTO objDTO)
        {
            var objFromDb = await _db.ProductsPrices.FirstOrDefaultAsync(p => p.Id == objDTO.Id);
            if (objFromDb is not null)
            {
                //EF doesnt need to explicit the properties that im changing
                objFromDb.ProductId = objDTO.ProductId;
                objFromDb.Size = objDTO.Size;
                objFromDb.Price = objDTO.Price;
                _db.ProductsPrices.Update(objFromDb);
                _db.SaveChangesAsync();
                return _mapper.Map<ProductPrice, ProductPriceDTO>(objFromDb);
            }
            return objDTO;
        }
    }

}
