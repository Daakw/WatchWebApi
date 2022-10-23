using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.ViewModels.Brand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/watches/watchbrands")]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BrandController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost()]
        public async Task<IActionResult> AddBrand(PostViewModel brandName)
        {
            var brand = _mapper.Map<WatchBrand>(brandName);

            if (await _unitOfWork.WatchBrandRepository.AddWatchBrandAsync(brand))
                if (await _unitOfWork.Complete())
                    return StatusCode(201, _mapper.Map<ViewModel>(brand));

            return StatusCode(500, "Something went wrong");
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllWatchBrands()
        {
            var result = await _unitOfWork.WatchBrandRepository.GetAllWatchBrandsAsync();
            if (result == null) return NotFound("Could not find any brands in database");

            return Ok(_mapper.Map<IList<ViewModel>>(result));
        }

        [HttpGet("brandname/{brandName}")]
        public async Task<IActionResult> FindbyBrand(string brandName)
        {
            var result = await _unitOfWork.WatchBrandRepository.GetWatchBrandbyNameAsync(brandName);
            if (result == null) return NotFound($"could not find any brand with the name \"{brandName}\"");

            return Ok(_mapper.Map<ViewModel>(result));
        }

        [HttpPut("brandname/{brandName}")]
        public async Task<IActionResult> UpdateCategory(string brandName, [FromBody] PutViewModel brand)
        {
            var toUpdate = await _unitOfWork.WatchBrandRepository.GetWatchBrandbyNameAsync(brandName);
            if (toUpdate == null) return NotFound($"Could not find any brand with the name \"{brandName}\"");

            toUpdate.BrandName = brand.BrandName;

            if (_unitOfWork.WatchBrandRepository.UpdateWatchBrand(toUpdate))
                if (await _unitOfWork.Complete()) return Ok("Changes saved successfully.");

            return StatusCode(500, "Something went wrong.");
        }


        [HttpDelete("brandname/{brandName}")]
        public async Task<IActionResult> RemoveCategory(string brandName)
        {
            var toDelete = await _unitOfWork.WatchBrandRepository.GetWatchBrandbyNameAsync(brandName);
            if (toDelete == null) return NotFound($"Could not find any brand with the name \"{brandName}\"");

            if (_unitOfWork.WatchBrandRepository.DeleteWatchBrand(toDelete))
                if (await _unitOfWork.Complete()) return Ok("delete successfull!");

            return StatusCode(500, "Could not delete brand.");
        }
    }
}
