﻿using Petstagram.Models;

namespace Petstagram.Repositories
{
    public interface IRepository
    {
        Picture GetPicById(int id);
        Pet GetPetById(int id);
        List<Picture> GetAllPics();
        List<Picture> GetAllPicsByPet(Pet pet);
        List<Pet> GetAllPets();
        List<Pet> GetAllPetsByPic(Picture pic);
        void AddPic(Picture pic);
        void AddPet(Pet pet);
        void UpdatePic(Picture picture);
        void UpdatePet(Pet pet);
        void DeletePic(Picture pic);
        void DeletePet(Pet pet);
        bool HasPetData();
    }
}
