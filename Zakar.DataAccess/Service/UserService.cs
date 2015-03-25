using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions;
using Zakar.Models;
using Zakar.ViewModels;

namespace Zakar.DataAccess.Service
{
    public class UserService
    {
      
        private readonly IRepository<UserProfile> _userRepo;
        private readonly IRepository<Webpages_Membership> _wRepository;

        public UserService(IRepository<UserProfile> userrepo,
            IRepository<Webpages_Membership> wRepository)
        {
            _userRepo = userrepo;
            _wRepository = wRepository;
        }

        public void AddUser(UserProfile profile)
        {
            _userRepo.Insert(profile);
        }


        public void DisableUser(int id)
        {
            var entity =
                _wRepository.Find(i => i.UserId == id)
                    .FirstOrDefault();
            if (entity != null)
            {
                entity.IsConfirmed = false;
            }
        }

        public void EnableUser(int id)
        {
            Webpages_Membership entity =
                _wRepository.Find(i => i.UserId == id)
                    .FirstOrDefault();
            if (entity != null) 
                entity.IsConfirmed = true;
        }

        public UserProfile Find(int id)
        {
            return _userRepo.Find(i => i.UserId == id).FirstOrDefault();
        }

        public UserProfile Find(string username)
        {
            return
                _userRepo.Find(i => i.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
        }

        public IQueryable<UserProfile> GetAll()
        {
            return _userRepo.GetAll();
        }

        public UserProfile GetById(int id)
        {
            return _userRepo.Find(u => u.UserId == id).FirstOrDefault();
        }

        public void Save(UserViewModel model)
        {
            UserProfile entity = Find(model.UserId);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            entity.UserName = model.UserName;
            entity.PhoneNumber = model.PhoneNumber;
            entity.LastName = model.LastName;
            entity.FirstName = model.FirstName;
        }

        public IQueryable<UserViewModel> Search(UserSearchModel model)
        {
            return
                (from i in
                    _userRepo.Find(i => (i.UserName == model.UserName) || (i.PhoneNumber == model.PhoneNumber))
                    orderby i.UserId
                    select i).AsQueryable<UserProfile>().Project<UserProfile>().To<UserViewModel>();
        }
    }
}