using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCards.Dtos;
using MCards.Entities;
using MCards.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MCards.Services
{
    public interface ICardService
    {
        IEnumerable<Card> GetAllCards();
        Card GetById(int id);
        Card Add(Card card);
        void Update(CardDto card);
        void Delete(int id);
    }
    public class CardService : ICardService
    {
        private DataContext _context;

        public CardService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Card> GetAllCards()
        {
            var cardsWithFKProps = _context.Card
                .Select(c => new Card
                {
                    PK_Card = c.PK_Card,
                    CardName = c.CardName,
                    FK_CardType = c.FK_CardType,
                    FK_Expansion = c.FK_Expansion,
                    CardNumber = c.CardNumber,
                    IsFoil = c.IsFoil,
                    FK_Condition = c.FK_Condition,
                    CardType = _context.CardType.FirstOrDefault(x => x.PK_CardType == c.FK_CardType),
                    Expansion = _context.Expansion.FirstOrDefault(x => x.PK_Expansion == c.FK_Expansion)


                }).ToList();
            return cardsWithFKProps;
        }

        public Card GetById(int id)
        {
            var cardWithFKProps = _context.Card.Select(c => new Card
            {
                PK_Card = c.PK_Card,
                CardName = c.CardName,
                FK_CardType = c.FK_CardType,
                FK_Expansion = c.FK_Expansion,
                CardNumber = c.CardNumber,
                IsFoil = c.IsFoil,
                FK_Condition = c.FK_Condition,
                CardType = _context.CardType.FirstOrDefault(x => x.PK_CardType == c.FK_CardType),
                Expansion = _context.Expansion.FirstOrDefault(x => x.PK_Expansion == c.FK_Expansion)
                
            }).FirstOrDefault(x => x.PK_Card == id);
            return cardWithFKProps;
        }

        public Card Add(Card card)
        {
            if (string.IsNullOrWhiteSpace(card.CardName))
                throw new AppException("Name is required");

            if (card.FK_CardType==0)
                throw new AppException("CardType is required");

            if (card.FK_Expansion == 0)
                throw new AppException("Expansion is required");

            if (card.FK_Condition == 0)
                throw new AppException("Condition is required");

            if (card.CardNumber <= 0)
                throw new AppException("Card Number is required");
            



            if (_context.Card.Any(x => x.CardName == card.CardName))
                throw new AppException("Card \"" + card.CardName + "\" is already added");


            card.CardType = new CardType();
            card.Expansion=new Expansion();

            _context.Card.Add(card);
            _context.SaveChanges();

            return card;
        }

        public void Update(CardDto cardDto)
        {
            var card = _context.Card.Find(cardDto.PK_Card);

            if (card == null)
                throw new AppException("Expansion not found");

            if (cardDto.CardName != card.CardName)
            {
                // expansion has changed so check if the new expansion name is already created
                if (_context.Card.Any(x => x.CardName == cardDto.CardName))
                    throw new AppException("Card \"" + card.CardName + "\" is already exists");

            }


            card.CardName = cardDto.CardName;
            card.FK_CardType = cardDto.FK_CardType;
            card.FK_Expansion = cardDto.FK_Expansion;
            card.CardNumber = cardDto.CardNumber;
            card.IsFoil = cardDto.IsFoil;
            card.FK_Condition = cardDto.FK_Condition;



            _context.Card.Update(card);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var card = _context.Card.Find(id);
            if (card != null)
            {
                _context.Card.Remove(card);
                _context.SaveChanges();
            }
        }
    }
}
