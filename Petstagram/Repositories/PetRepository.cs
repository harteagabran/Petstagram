using Microsoft.EntityFrameworkCore;
using Petstagram.Context;
using Petstagram.Models;
using System.Linq.Expressions;

namespace Petstagram.Repositories
{
    public class PetRepository : IRepository<Pet>
    {
        private readonly PetContext _context;

        public PetRepository(PetContext context)
        {
            _context = context;
        }

        public async Task<Pet> GetByID(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public async Task<IEnumerable<Pet>> GetAll()
        {
            return await _context.Pets.ToListAsync();
        }

        public async Task<IEnumerable<Pet>> FindAsync(Expression<Func<Pet, bool>> predicate)
        {
            return await _context.Pets.Where(predicate).ToListAsync();
        }

        public async Task<Pet> Add(Pet entity)
        {
            await _context.Pets.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task AddRange(IEnumerable<Pet> entities)
        {
            await _context.Pets.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Pet entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async void Remove(Pet entity)
        {
            _context.PetPictures.RemoveRange(_context.PetPictures.Where(pp => pp.PetId == entity.Id));

            _context.Pets.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async void RemoveRange(IEnumerable<Pet> entities)
        {
            foreach (var entity in entities)
            {
                // Remove associated PetPictures
                _context.PetPictures.RemoveRange(_context.PetPictures.Where(pp => pp.PetId == entity.Id));
            }

            _context.Pets.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
