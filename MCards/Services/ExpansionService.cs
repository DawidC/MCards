using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MCards.Dtos;
using MCards.Entities;
using MCards.Helpers;

namespace MCards.Services
{
    public interface IExpansionService
    {
        IEnumerable<Expansion> GetAllExpansions();
        Expansion GetById(int id);
        Expansion Add(Expansion expansion);
        void Update(ExpansionDto expansion);
        void Delete(int id);
    }

    public class ExpansionService : IExpansionService
    {
        private DataContext _context;

        public ExpansionService(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Expansion> GetAllExpansions()
        {
            return _context.Expansion;
        }

        public Expansion GetById(int id)
        {
            return _context.Expansion.Find(id);
        }

        public Expansion Add(Expansion expansion)
        {
            if (string.IsNullOrWhiteSpace(expansion.ExpansionName))
                throw new AppException("Name is required");

            if (string.IsNullOrWhiteSpace(expansion.ExpansionShortName))
                throw new AppException("ShortName is required");



            if (_context.Expansion.Any(x => x.ExpansionName == expansion.ExpansionName))
                throw new AppException("Expansion \"" + expansion.ExpansionName + "\" is already created");



            _context.Expansion.Add(expansion);
            _context.SaveChanges();

            return expansion;
        }

        public void Update(ExpansionDto expansionDto)
        {
            var expansion = _context.Expansion.Find(expansionDto.PK_Expansion);

            if (expansion == null)
                throw new AppException("Expansion not found");

            if (expansionDto.ExpansionName != expansion.ExpansionName)
            {
                // expansion has changed so check if the new expansion name is already created
                if (_context.Expansion.Any(x => x.ExpansionName == expansion.ExpansionName))
                    throw new AppException("Expansion \"" + expansion.ExpansionName + "\" is already created");

            }

            // update user properties
            expansion.ExpansionName = expansionDto.ExpansionName;
            expansion.ExpansionShortName = expansionDto.ExpansionShortName;
            expansion.ExpansionCards = expansionDto.ExpansionCards;

         

            _context.Expansion.Update(expansion);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var expansion = _context.Expansion.Find(id);
            if (expansion != null)
            {
                _context.Expansion.Remove(expansion);
                _context.SaveChanges();
            }
        }
    }
}
