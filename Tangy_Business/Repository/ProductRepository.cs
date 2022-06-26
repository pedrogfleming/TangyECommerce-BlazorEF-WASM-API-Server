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
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        //dependency Injection
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {
            var obj = _mapper.Map<ProductDTO, Product>(objDTO);
            ////Aggregate the category obj to the database
            var addedObj = _db.Products.Add(obj);
            //The save confirm the changes in the db, its obligatory to perform changes in db
            _db.SaveChanges();
            return _mapper.Map<Product, ProductDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (obj is not null)
            {
                _db.Products.Remove(obj);
                await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> Get(int id)
        {
            var obj = await _db.Products.Include(p => p.Category).Include(p=>p.ProductPrices).FirstOrDefaultAsync(p => p.Id == id);
            if (obj is not null)
            {
                return _mapper.Map<Product, ProductDTO>(obj);
            }
            return new ProductDTO();
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_db.Products.Include(p => p.Category).Include(p => p.ProductPrices));
        }

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var objFromDb = await _db.Products.FirstOrDefaultAsync(p => p.Id == objDTO.Id);
            if (objFromDb is not null)
            {
                //EF doesnt need to explicit the properties that im changing
                objFromDb.Name = objDTO.Name;
                objFromDb.Description = objDTO.Description;
                objFromDb.ImageUrl = objDTO.ImageUrl;
                objFromDb.CategoryId = objDTO.CategoryId;
                objFromDb.Color = objDTO.Color;
                objFromDb.ShopFavorites = objDTO.ShopFavorites;
                objFromDb.CustomerFavorites = objDTO.CustomerFavorites;
                _db.Products.Update(objFromDb);
                _db.SaveChangesAsync();
                return _mapper.Map<Product, ProductDTO>(objFromDb);
            }
            return objDTO;
        }
    }

}