using Petstagram.Models;
using Microsoft.EntityFrameworkCore;
using Petstagram.Context;

namespace Petstagram.Repositories
{
    public class Repository : IRepository
    {
        private readonly PetContext _repo;
        public Repository(PetContext repository) 
        {
            _repo = repository;
        }

        public void DeletePet(Pet pet)
        {
            _repo.Pets.Remove(pet);
            _repo.SaveChanges();
        }

        public void DeletePic(Picture pic)
        {
            _repo.Pictures.Remove(pic);
            _repo.SaveChanges();
        }

        public List<Pet> GetAllPets()
        {
            return _repo.Pets.Include(p => p.Pictures).ToList();
        }

        public List<Pet> GetAllPetsByPic(Picture pic)
        {
            throw new NotImplementedException();
        }

        public List<Picture> GetAllPics()
        {
            return _repo.Pictures.Include(p => p.Pets).ToList();
        }

        public List<Picture> GetAllPicsByPet(Pet pet)
        {
            throw new NotImplementedException();
        }

        public Pet GetPetById(int id)
        {
            return _repo.Pets.Find(id);
        }

        public Picture GetPicById(int id)
        {
            return _repo.Pictures.Find(id);
        }

        public void UpdatePet(Pet pet)
        {
            _repo.Pets.Update(pet);
            _repo.SaveChanges();
        }

        public void UpdatePic(Picture picture)
        {
            _repo.Pictures.Update(picture);
            _repo.SaveChanges();
        }
        public bool HasPetData()
        {
            Pet pet = _repo.Pets.FirstOrDefault();

            return pet != null;
        }

        public void AddPic(Picture pic)
        {
            _repo.Pictures.Add(pic);
            _repo.SaveChanges();
        }

        public void AddPet(Pet pet)
        {
            _repo.Pets.Add(pet);
            _repo.SaveChanges();
        }
    }
}
