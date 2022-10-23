using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;
using WebApi.ViewModels.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/watches/watchmodels")]
    public class ModelController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ModelController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpPost()]
        public async Task<IActionResult> AddModel(PostViewModel name)
        {
            try
            {
                var model = _mapper.Map<WatchModel>(name, opt => opt.Items["repo"] = _unitOfWork.Context);

                if (await _unitOfWork.WatchModelRepository.AddWatchModelAsync(model))
                {
                    if (!await _unitOfWork.Complete())
                        return StatusCode(500, "Something went wrong while saving model.");

                    var result = _mapper.Map<ViewModel>(model);
                    return StatusCode(201, result);
                }
                return StatusCode(500, "Something went wrong while saving model.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllModels()
        {
            var result = await _unitOfWork.WatchModelRepository.GetAllWatchModelsAsync();
            if (result == null) return NotFound("Could not find any watchmodels in database");

            return Ok(_mapper.Map<IList<ViewModel>>(result));
        }

        [HttpGet("id/{modelId}")]
        public async Task<IActionResult> GetModel(int modelId)
        {
            var model = await _unitOfWork.WatchModelRepository.FindWatchModelByIdAsync(modelId);
            if (model == null) return NotFound("Could not find any watchmodel with id");

            return Ok(_mapper.Map<ViewModel>(model));
        }

        [HttpGet("modelname/{modelName}")]
        public async Task<IActionResult> FindModelByName(string modelName)
        {
            var result = await _unitOfWork.WatchModelRepository.FindWatchModelByNameAsync(modelName);
            if (result == null) return NotFound($"Could not find watchmodel with the name \"{modelName}\"");

            var response = _mapper.Map<ViewModel>(result);
            return Ok(response);
        }

        [HttpGet("bybrandname/{watchBrand}")]
        public async Task<IActionResult> FindModelByBrandName(string watchBrand)
        {
            var result = await _unitOfWork.WatchModelRepository.FindWatchModelsByWatchBrand(watchBrand);
            if (watchBrand == null) return NotFound($"Could not find any watchmodels with brandname \"{watchBrand}\"");

            var response = _mapper.Map<List<ViewModel>>(result);
            return Ok(response);
        }

        [HttpPut("id/{id}")]
        public async Task<IActionResult> UpdateModel(int id, [FromBody] PutViewModel model)
        {
            var toUpdate = await _unitOfWork.WatchModelRepository.FindWatchModelByIdAsync(id);
            if (toUpdate == null) return NotFound($"Could not find watchmodel with id {id}");

            toUpdate.ModelName = model.ModelName;
            toUpdate.ReferenceNumber = model.ReferenceNumber;

            if (_unitOfWork.WatchModelRepository.UpdateWatchModel(toUpdate))
                if (await _unitOfWork.Complete()) return Ok("Changes made successfully!");

            return StatusCode(500, "Something went wrong.");
        }

        [HttpDelete("id/{id}")]
        public async Task<IActionResult> RemoveBrand(int id)
        {
            var toDelete = await _unitOfWork.WatchModelRepository.FindWatchModelByIdAsync(id);
            if (toDelete == null) return NotFound($"Could not find watchmodel with id {id}");

            if (_unitOfWork.WatchModelRepository.RemoveWatchModel(toDelete))
                if (await _unitOfWork.Complete()) return Ok("delete successfull!");

            return StatusCode(500, "Could not delete model.");
        }
    }
}
