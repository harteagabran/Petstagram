﻿using Petstagram.Models;
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
            //delete all pictures related to this pet
            var picturesToDelete = _repo.Pictures.Where(x => x.Pets.Count == 1 && x.Pets.Contains(pet)).ToList();

            _repo.Pictures.RemoveRange(picturesToDelete);
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

        public List<Pet> GetAllPetsByOwner(string owner)
        {
            return _repo.Pets.Include(p => p.Pictures).Where(p => p.OwnerId == owner).ToList();
        }

        public List<Picture> GetAllPics()
        {
            return _repo.Pictures.Include(p => p.Pets).ToList();
        }

        public List<Picture> GetAllPicsByPet(Pet pet)
        {
            return _repo.Pictures.Where(p => p.Pets.Contains(pet)).ToList();
        }

        public Pet GetPetById(int id)
        {
            return _repo.Pets.Find(id);
        }

        public Picture GetPicById(int id)
        {
            return _repo.Pictures.Include(p => p.Pets).FirstOrDefault(p => p.Id == id);
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
        public bool HasPetData(string owner)
        {
            Pet pet = _repo.Pets.FirstOrDefault(p => p.OwnerId == owner);

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

        public List<Picture> GetAllPicsByOwner(string owner)
        {
            return _repo.Pictures.Include(p => p.Pets).Where(p => p.Pets.Any(pet => pet.OwnerId == owner)).ToList();
        }

        public List<Pet> GetAllPetsFromPic(Picture pic)
        {
            return _repo.Pets.Where(p => p.Pictures.Any(picture => picture.Id == pic.Id)).ToList();
        }
    }
}
