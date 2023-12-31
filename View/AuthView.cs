﻿using Bts.Constant;
using Bts.IService;
using Bts.Model;
using Bts.Service;
using System.Runtime.ConstrainedExecution;

namespace Bts.View;

internal class AuthView
{
    CandidateView _candidateView;
    HRView _hrView;
    ReviewerView _reviewerView;
    SuperAdminView _superAdminView;

    IUserService _userService;

    public AuthView(IUserService userService, SuperAdminView superadminView, HRView hrView, ReviewerView reviewerView, CandidateView candidateView)
    {
        _userService = userService;
        _superAdminView = superadminView;
        _candidateView = candidateView;
        _hrView = hrView;
        _reviewerView = reviewerView;
    }

    public void Login()
    {
        while (true)
        {
            Console.WriteLine("\n==== Bootcamp Test Management System ====");
            Console.WriteLine("---- Login Page ----");
            Console.Write("Email : ");
            var email = Console.ReadLine();
            Console.Write("Password : ");
            var password = Console.ReadLine();

            var user = _userService.Login(email, password);
            while (user == null)
            {
                Console.WriteLine("\nCredential is wrong!\n");

                Console.Write("Email : ");
                email = Console.ReadLine();
                Console.Write("Password : ");
                password = Console.ReadLine();
                user = _userService.Login(email, password);
            }

            if (user.Role.RoleCode == UserRoleCode.SuperAdmin)
            {
                _superAdminView.MainMenu(user);
            }
            else if (user.Role.RoleCode == UserRoleCode.HumanResource)
            {
                _hrView.MainMenu(user);
            }
            else if (user.Role.RoleCode == UserRoleCode.Reviewer)
            {
                _reviewerView.MainMenu(user);
            }
            else if (user.Role.RoleCode == UserRoleCode.Candidate)
            {
                _candidateView.MainMenu(user);
            }
        }
    }
}
