using Microsoft.EntityFrameworkCore;
using Petstagram.Context;
using Petstagram.Models;
using System.Linq.Expressions;

namespace Petstagram.Repositories
{
    public class PictureRepository : IRepository<Picture>
    {
        private readonly PetContext _context;

        public PictureRepository(PetContext context)
        {
            _context = context;
        }

        public async Task<Picture> GetByID(int id)
        {
            return await _context.Pictures.FindAsync(id);
        }

        public async Task<IEnumerable<Picture>> GetAll()
        {
            return await _context.Pictures.ToListAsync();
        }

        public async Task<IEnumerable<Picture>> Find(Expression<Func<Picture, bool>> predicate)
        {
            return await _context.Pictures.Where(predicate).ToListAsync();
        }

        public async Task<Picture> Add(Picture entity)
        {
            await _context.Pictures.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task AddPetPictures(int pictureId, IEnumerable<int> petIds)
        {
            foreach (var petId in petIds)
            {
                var petPicture = new PetPicture { PetId = petId, PictureId = pictureId };
                _context.PetPictures.Add(petPicture);
            }

            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Picture> entities, IEnumerable<Pet> pets)
        {
            foreach (var entity in entities)
            {
                _context.Pictures.Add(entity);
                // Add associations for each pet
                foreach (var pet in pets)
                {
                    var petPicture = new PetPicture { Pet = pet, Picture = entity };
                    _context.PetPictures.Add(petPicture);
                }
            }

            await _context.SaveChangesAsync();
        }


        public async void Remove(Picture entity)
        {
            _context.PetPictures.RemoveRange(_context.PetPictures.Where(pp => pp.PictureId == entity.Id));

            _context.Pictures.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async void RemoveRange(IEnumerable<Picture> entities)
        {
            foreach (var entity in entities)
            {
                // Remove associated PetPictures
                _context.PetPictures.RemoveRange(_context.PetPictures.Where(pp => pp.PictureId == entity.Id));
            }

            _context.Pictures.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Picture entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Picture> entities)
        {
            await _context.Pictures.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }
    }
}
