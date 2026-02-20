using WebRadisRabittSol.Data;
using Microsoft.AspNetCore.Mvc;
using WebRadisRabittSol.Models;
using WebRadisRabittSol.Models.Dto;
using WebRadisRabittPro.Services.Caching;

namespace WebRadisRabittSol.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDBContext _db;
        private ResponseDto _response;
        private readonly IRedisCacheService _cacheService;

        public ProductAPIController(AppDBContext  db, IRedisCacheService cacheService)
        {
            _db = db;
            _response = new ResponseDto();
            _cacheService = cacheService;
        }

        //[HttpGet]
        //public ResponseDto Get()
        //{
        //    try
        //    {
        //        var userId = Request.Headers["UserId"];
        //        //var cachingKey = $"product_{userId}";
        //        var cachingKey = $"product";
        //        var objList = _cacheService.GetData<IEnumerable<Product>>(cachingKey);

        //        if (objList is not null)
        //        {
        //            _response.Result = objList;
        //            return _response;
        //        }

        //        objList = _db.Product.ToList();
        //        _response.Result = objList;

        //        _cacheService.SetData(cachingKey, objList);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //    }
        //    return _response;
        //}

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                var userId = Request.Headers["UserId"];
                //var cachingKey = $"product_{userId}";
                var cachingKey = $"product";
                var objList = _cacheService.GetData<IEnumerable<Product>>(cachingKey);

                if (objList is not null)
                {
                    _response.Result = objList;
                    return _response;
                }

                objList = _db.Product.ToList();
                _response.Result = objList;

                await _cacheService.SetDataAsync(cachingKey, objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Product objProduct = _db.Product.First(f => f.ProductId == id);
                _response.Result = objProduct;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        public ResponseDto Post([FromBody] Product product)
        {
            try
            {
                product.ProductId = 0;
                product.ImageUrl = "https://placehold.co/603x403";
                _db.Product.Add(product);
                _db.SaveChanges();

                var cachingKey = $"product";
                _cacheService.RemoveDataAsync(cachingKey);

                //if (ProductDto.Image != null)
                //{

                //    string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                //    string filePath = @"wwwroot\ProductImages\" + fileName;

                //    //I have added the if condition to remove the any image with same name if that exist in the folder by any change
                //    var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                //    FileInfo file = new FileInfo(directoryLocation);
                //    if (file.Exists)
                //    {
                //        file.Delete();
                //    }

                //    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                //    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                //    {
                //        ProductDto.Image.CopyTo(fileStream);
                //    }
                //    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                //    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                //    product.ImageLocalPath = filePath;
                //}
                //else
                //{
                //    product.ImageUrl = "https://placehold.co/600x400";
                //}
                //_db.Product.Update(product);
                //_db.SaveChanges();
                _response.Result = product;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] Product product)
        {
            try
            {
               // var product = _mapper.Map<ProductAPI.Models.Product>(ProductDto);

                //if (ProductDto.Image != null)
                //{
                //    if (!string.IsNullOrEmpty(product.ImageLocalPath))
                //    {
                //        var oldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), product.ImageLocalPath);
                //        FileInfo file = new FileInfo(oldFilePathDirectory);
                //        if (file.Exists)
                //        {
                //            file.Delete();
                //        }
                //    }

                //    string fileName = product.ProductId + Path.GetExtension(ProductDto.Image.FileName);
                //    string filePath = @"wwwroot\ProductImages\" + fileName;
                //    var filePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                //    using (var fileStream = new FileStream(filePathDirectory, FileMode.Create))
                //    {
                //        ProductDto.Image.CopyTo(fileStream);
                //    }
                //    var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                //    product.ImageUrl = baseUrl + "/ProductImages/" + fileName;
                //    product.ImageLocalPath = filePath;
                //}


                _db.Product.Update(product);
                _db.SaveChanges();

                _response.Result = product;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                var _obj = _db.Product.First(f => f.ProductId == id);
                //if (!string.IsNullOrEmpty(_obj.ImageLocalPath))
                //{
                //    var OldFilePathDirectory = Path.Combine(Directory.GetCurrentDirectory(), _obj.ImageLocalPath);
                //    FileInfo file = new FileInfo(OldFilePathDirectory);
                //    if (file.Exists)
                //    {
                //        file.Delete();
                //    }
                //}

                _db.Product.Remove(_obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
