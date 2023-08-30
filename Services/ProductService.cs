using Back_End.Context;
using Back_End.IServices;
using Back_End.Models;
using DinkToPdf;
using Microsoft.EntityFrameworkCore;


namespace Back_End.Services
{
    public class ProductService:IProductService
    {
        private readonly AplicationDbContext aplicationDbContext;
        

        public ProductService(AplicationDbContext aplicationDbContext)
        {
            this.aplicationDbContext = aplicationDbContext;
           
        }

        public async Task RegisterProduct(Product product)
        {
            aplicationDbContext.Products.Add(product);
            await aplicationDbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProducts(int id)
        {
            return await aplicationDbContext.Products.Where(x=>x.userId!=id&&x.hidden==0).ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await aplicationDbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task BuyProduct(Purchase purchase)
        {
            aplicationDbContext.Purchases.Add(purchase);
          await aplicationDbContext.SaveChangesAsync();
        }
        public async Task<List<Product>> GetMyProducts(int id)
        {
            return await aplicationDbContext.Products.Where(x => x.userId == id).ToListAsync();
        }
        public async Task SetHideProduct(Product product)
        {
            aplicationDbContext.Products.Update(product);
            await aplicationDbContext.SaveChangesAsync();
        }
        public async Task<List<Purchase>> GetPurchases(int id)
        {
            return  await aplicationDbContext.Purchases.Where(x=>x.userId==id).Include(x=>x.user).Include(x=>x.product).ThenInclude(x=>x.user).ToListAsync();
        }

       public async Task<Product> GetProductDetails(int idproduct,int iduser)
        {
            return await aplicationDbContext.Products.Where(x => x.Id == idproduct && x.userId != iduser).FirstOrDefaultAsync();
        }


        public async Task<Product> SeeeEditProduct(int idproduct, int iduser)
        {
            return await aplicationDbContext.Products.Where(x => x.Id == idproduct && x.userId == iduser).FirstOrDefaultAsync();
        }

   

    }
}
