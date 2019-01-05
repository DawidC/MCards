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
    public class CardController : ControllerBase
    {
        private ICardService _cardService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        
        public CardController(ICardService cardService, IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _cardService = cardService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var cards = _cardService.GetAllCards();
            var cardDtos = _mapper.Map<IList<CardDto>>(cards);
            return Ok(cardDtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var card = _cardService.GetById(id);
            var cardDto = _mapper.Map<CardDto>(card);
            return Ok(cardDto);
        }

        [HttpPost("add")]
        public IActionResult Add([FromBody]CardDto cardDto)
        {
            // map dto to entity
            var card = _mapper.Map<Card>(cardDto);

            try
            {
                // save 
                _cardService.Add(card);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]CardDto cardDto)
        {
            // map dto to entity and set id
            var card = _mapper.Map<CardDto>(cardDto);
            card.PK_Card = id;

            try
            {
                // save 
                _cardService.Update(card);
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
            _cardService.Delete(id);
            return Ok();
        }
    }
}
