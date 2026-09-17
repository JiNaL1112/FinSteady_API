using FinSteady_API.Infrastructure;
using FinSteady_API.Models;
using FinSteady_API.Models.Request;
using FinSteady_API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FinSteady_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavingRuleController : ControllerBase
    {
        private readonly ISavingRuleRepository _savingRuleRepository;
        protected APIResponse _response;

        public SavingRuleController(ISavingRuleRepository savingRuleRepository)
        {
            _savingRuleRepository = savingRuleRepository;
            _response = new APIResponse();
        }

        [HttpGet("{id:int}", Name = "GetSavingRule")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetSavingRule(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var savingRule = await _savingRuleRepository.GetSavingRuleById(id);
                if (savingRule == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = savingRule;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetSavingRules()
        {
            try
            {
                IEnumerable<SavingRule> savingRuleList = await _savingRuleRepository.GetSavingRules();
                _response.Result = savingRuleList;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteSavingRule")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteSavingRule(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var savingRule = await _savingRuleRepository.GetSavingRuleById(id);
                if (savingRule == null)
                {
                    return NotFound();
                }
                await _savingRuleRepository.DeleteSavingRule(savingRule);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateSavingRule")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateSavingRule(int id, [FromBody] SavingRuleRequestModel updateDTO)
        {
            try
            {
                if (updateDTO == null)
                {
                    return BadRequest();
                }
                SavingRule dbSavingRule = await _savingRuleRepository.GetSavingRuleById(id);
                SavingRule model = updateDTO.ToEntity();

                await _savingRuleRepository.UpdateSavingRule(dbSavingRule, model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpPost("createSavingRule")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateSavingRule([FromBody] SavingRuleRequestModel createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                SavingRule savingRule = createDTO.ToEntity();

                savingRule = await _savingRuleRepository.AddSavingRule(savingRule);
                _response.Result = savingRule;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetSavingRule", new { id = savingRule.SavingRuleId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
    }
}
