using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Xml.Linq;

namespace CoffeeShop.Services.Implementations
{
    public class ProductService : IProductService
    {
        public ProductService(ILogger<IProductService> logger, IProductRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        private readonly ILogger<IProductService> _logger;
        private readonly IProductRepository _repository;

        public async Task<BaseResponce<List<List<Product>>>> GetAllByCategory()
        {
            try
            {
                return new BaseResponce<List<List<Product>>>()
                {
                    Data = await _repository.GetAllByCategory(),
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetAllByCategory] : " + ex);
                return new BaseResponce<List<List<Product>>>()
                {
                    Description = "[GetAllByCategory] : " + ex,
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError
                };
            }
        }

        public async Task<BaseResponce<Product>> GetById(int id)
        {
            try
            {
                var data = await _repository.GetById(id);
                if (data == null)
                {
                    return new BaseResponce<Product>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Not found"
                    };
                }
                return new BaseResponce<Product>()
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetById] : " + ex);
                return new BaseResponce<Product>()
                {
                    Description = "[GetById] : " + ex,
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError
                };
            }
        }

        public async Task<BaseResponce<Product>> GetByName(string name)
        {
            try
            {
                var data = await _repository.GetByName(name);
                if (data == null)
                {
                    return new BaseResponce<Product>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Not found"
                    };
                }
                return new BaseResponce<Product>()
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetByName] : " + ex);
                return new BaseResponce<Product>()
                {
                    Description = "[GetByName] : " + ex,
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError
                };
            }
        }

        public async Task<BaseResponce<bool>> Create(ProductViewModel viewModel)
        {
            try
            {
                var product = new Product()
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    ImageURL = viewModel.ImageURL,
                    Category = viewModel.Category,
                    Price = viewModel.Price,
                    Discount = viewModel.Discount,
                    Weight = viewModel.Weight,
                    Units = viewModel.Units,
                    InStock = viewModel.InStock,
                    Proteins = viewModel.Proteins,
                    Fats = viewModel.Fats,
                    Carbohydrates = viewModel.Carbohydrates,
                    Caffeine = viewModel.Caffeine,
                    Lactose = viewModel.Lactose,
                    Gluten = viewModel.Gluten,
                    Sugar = viewModel.Sugar
                };
                await _repository.Create(product);
                return new BaseResponce<bool>()
                {
                    StatusCode = Domain.Enums.StatusCode.Success,
                    Data = true
                };
            }
            catch(Exception ex)
            {
                _logger.LogError("[Create] : " + ex);
                return new BaseResponce<bool>()
                {
                    Description = "[Create] : " + ex,
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError
                };
            }
        }

        public async Task<BaseResponce<bool>> Delete(int id)
        {
            try
            {
                var product = await _repository.GetById(id);
                if (product == null)
                {
                    return new BaseResponce<bool>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Product was not found"
                    };
                }
                await _repository.Delete(product);
                return new BaseResponce<bool>()
                {
                    Data = true,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[Delete] : " + ex);
                return new BaseResponce<bool>()
                {
                    Description = "[Delete] : " + ex,
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError
                };
            }
        }

        public async Task<BaseResponce<Product>> Edit(int id, ProductViewModel viewModel)
        {
            try
            {
                var product = await _repository.GetById(id);
                if (product == null)
                {
                    return new BaseResponce<Product>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Product was not found"
                    };
                }
                product.Name = viewModel.Name;
                product.Description = viewModel.Description;
                product.ImageURL = viewModel.ImageURL;
                product.Category = viewModel.Category;
                product.Price = viewModel.Price;
                product.Discount = viewModel.Discount;
                product.Weight = viewModel.Weight;
                product.Units = viewModel.Units;
                product.InStock = viewModel.InStock;
                product.Proteins = viewModel.Proteins;
                product.Fats = viewModel.Fats;
                product.Carbohydrates = viewModel.Carbohydrates;
                product.Lactose = viewModel.Lactose;
                product.Caffeine = viewModel.Caffeine;
                product.Gluten = viewModel.Gluten;
                product.Sugar = viewModel.Sugar;
                await _repository.Update(product);
                return new BaseResponce<Product>()
                {
                    Data = product,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[Edit] : " + ex);
                return new BaseResponce<Product>()
                {
                    Description = "[Edit] : " + ex,
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError
                };
            }
        }
    }
}
