﻿using Bts.IService;
using Bts.Model;
using Bts.IRepo;
using Bts.Repo;

namespace Bts.Service;

internal class UserService : IUserService
{
    IUserRepo _userRepo;
    IRoleRepo _roleRepo;

    public UserService(IUserRepo userRepo, IRoleRepo roleRepo)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
    }

    public User CreateUser(User user)
    {
        var newUser = _userRepo.CreateNewUser(user);
        return newUser;
    }

    public List<Role> GetRoleList()
    {
        var roleList = _roleRepo.GetAllRoleExcludingSuperadminAndCandidate();
        return roleList;
    }

    public List<User> GetUserListByRole(string roleCode)
    {
        return new List<User>();
    }

    public User? Login(string email, string password)
    {
        User? user = _userRepo.GetUserByEmailAndPassword(email, password);
        return user;
    }
}
