using FeedApp.Bll.Entities;
using FeedApp.Bll.Exceptions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Bll.Services
{
        public class EatingService : IEatingService
        {
            private readonly ApplicationDbContext _context;


            public EatingService(ApplicationDbContext context)
            {
                _context = context;
            }

            //DELETE
            public void DeleteEating(int eatingId)
            {
                _context.Eatings.Remove(new Eating { ID = eatingId });

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new EntityNotFoundException("Eating not found");
                }

            }

            //GET entity
            public Eating GetEating(int eatingId)
            {
                 return _context.Eatings
                    .Include(e => e.FoodsForEatings)
                        .ThenInclude(fee => fee.Food)
                    .SingleOrDefault(e => e.ID == eatingId) ?? throw new EntityNotFoundException("Eating not found!");
            }

            public IEnumerable<Eating> GetEatings()
            {
                var eatings = _context.Eatings
                    .Include(e => e.FoodsForEatings)
                        .ThenInclude(fee => fee.Food)
                    .ToList();

                return eatings;
            }

            public Eating InsertEating(Eating newEating)
            {
                _context.Eatings.Add(newEating);

                _context.SaveChanges();

                return newEating;
            }

            public void UpdateEating(int eatingId, Eating updatedEating)
            {
                updatedEating.ID = eatingId;
                var entry = _context.Attach(updatedEating);
                entry.State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw new EntityNotFoundException("Eating not found!");
                }
            }

        }
    
}
