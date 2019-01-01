using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCards.Dtos;
using MCards.Entities;
using MCards.Helpers;

namespace MCards.Services
{
    public interface ICardTypeService
    {
        IEnumerable<CardType> GetAll();
        CardType GetById(int id);
        CardType Add(CardType cardType);
        void Update(CardTypeDto cardType);
        void Delete(int id);
    }
    public class CardTypeService : ICardTypeService
    {
        private DataContext _context;

        public CardTypeService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<CardType> GetAll()
        {
            return _context.CardType;
        }

        public CardType GetById(int id)
        {
            return _context.CardType.Find(id);
        }

        public CardType Add(CardType cardType)
        {
            if (string.IsNullOrWhiteSpace(cardType.CardTypeName))
                throw new AppException("Name is required");
            

            if (_context.CardType.Any(x => x.CardTypeName == cardType.CardTypeName))
                throw new AppException("CardType \"" + cardType.CardTypeName + "\" is already created");



            _context.CardType.Add(cardType);
            _context.SaveChanges();

            return cardType;
        }

        public void Update(CardTypeDto cardTypeDto)
        {
            var cardType = _context.CardType.Find(cardTypeDto.PK_CardType);

            if (cardType == null)
                throw new AppException("Expansion not found");

            if (cardTypeDto.CardTypeName != cardType.CardTypeName)
            {
                // expansion has changed so check if the new expansion name is already created
                if (_context.CardType.Any(x => x.CardTypeName == cardTypeDto.CardTypeName))
                    throw new AppException("CardType \"" + cardType.CardTypeName + "\" is already created");

            }

            // update card type properties
            cardType.CardTypeName = cardTypeDto.CardTypeName;



            _context.CardType.Update(cardType);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var cardType = _context.CardType.Find(id);
            if (cardType != null)
            {
                _context.CardType.Remove(cardType);
                _context.SaveChanges();
            }
        }
    }
}
