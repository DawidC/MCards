using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MCards.Services;
using MCards.Helpers;
using MCards.Dtos;
using MCards.Entities;
using AutoMapper;

namespace MCards.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ExpansionController : ControllerBase
    {
        private IExpansionService _expansionService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public ExpansionController(IExpansionService expansionService, IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _expansionService = expansionService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAll()
        {
            var expansion = _expansionService.GetAllExpansions();
            var expansionDtos = _mapper.Map<IList<ExpansionDto>>(expansion);
            return Ok(expansionDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var expansion = _expansionService.GetById(id);
            var expansionDtos = _mapper.Map<UserDto>(expansion);
            return Ok(expansionDtos);
        }


        [HttpPost("add")]
        public IActionResult Add([FromBody]ExpansionDto expansionDto)
        {
            // map dto to entity
            var expansion = _mapper.Map<Expansion>(expansionDto);

            try
            {
                // save 
                _expansionService.Add(expansion);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ExpansionDto expansionDto)
        {
            // map dto to entity and set id
            var expansion = _mapper.Map<ExpansionDto>(expansionDto);
            expansion.PK_Expansion = id;

            try
            {
                // save 
                _expansionService.Update(expansion);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _expansionService.Delete(id);
            return Ok();
        }
    }
}
