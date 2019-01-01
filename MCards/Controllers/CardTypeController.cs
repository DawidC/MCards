using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MCards.Dtos;
using MCards.Entities;
using MCards.Helpers;
using MCards.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MCards.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CardTypeController : ControllerBase
    {

        private ICardTypeService _cardTypeService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public CardTypeController(ICardTypeService cardTypeService, IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _cardTypeService = cardTypeService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var expansion = _cardTypeService.GetAll();
            var expansionDtos = _mapper.Map<IList<CardTypeDto>>(expansion);
            return Ok(expansionDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var cardType = _cardTypeService.GetById(id);
            var cardTypeDtos = _mapper.Map<CardTypeDto>(cardType);
            return Ok(cardTypeDtos);
        }


        [HttpPost("add")]
        public IActionResult Add([FromBody]CardTypeDto cardTypeDto)
        {
            // map dto to entity
            var cardType = _mapper.Map<CardType>(cardTypeDto);

            try
            {
                // save 
                _cardTypeService.Add(cardType);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CardTypeDto cardTypeDto)
        {
            // map dto to entity and set id
            var cardType = _mapper.Map<CardTypeDto>(cardTypeDto);
            cardType.PK_CardType = id;

            try
            {
                // save 
                _cardTypeService.Update(cardType);
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
            _cardTypeService.Delete(id);
            return Ok();
        }
    }
}
