using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentication;
using eCommerceApp.Application.Services.Interfaces.Logging;
using eCommerceApp.Application.Validations;
using eCommerceApp.Application.Validations.Authentication;
using eCommerceApp.Domain.Entities.Identity;
using eCommerceApp.Domain.Interfaces.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementations.Authentication
{
    public class AuthenticationService(ITokenManagement tokenManagement, IUserManagement userManagement, IRoleManagement roleManagement, IAppLogger<AuthenticationService> logger, IMapper mapper, IValidator<CreateUser> validatorCreate, IValidator<LoginUser> validatorLogin, IValidationService validationService) : IAuthenticationService
    {
        public async Task<ServicesResponse> CreateUserAsync(CreateUser user)
        {
            var _validation = await validationService.ValidateAsync(user, validatorCreate);
            if (!_validation.Success) return _validation;
            var mapperModel = mapper.Map<AppUser>(user);
            mapperModel.UserName = user.Email;
            mapperModel.PasswordHash = user.Password;
            var result = await userManagement.CreateUser(mapperModel);
            if (!result) return new ServicesResponse { Message = "Email Address might be alraedy in use or unknow errror occurred" };

            var _user = await userManagement.GetUserByEmail(user.Email);
            var users = await userManagement.GetAllUsers();
            bool assignedResult = await roleManagement.AddUserRole(_user!, users!.Count() > 1 ? "User" : "Admin");
            if (!assignedResult)
            {
                // xoa user
                int removeUserResult = await userManagement.RemoveUserByEmail(_user.Email!);
                if(removeUserResult <= 0)
                {
                    logger.LogError(new Exception($"User with email as{_user.Email} failed to be remove as result of role assigning issue"),"User could not be assigned Role");
                    return new ServicesResponse { Message = "Error occurred in creating account" };
                }
            }
            return new ServicesResponse { Success = true, Message = "Create User Success" };

            // xac nhan email
        }

        public async Task<LoginResponse> LoginUser(LoginUser user)
        {
            var _validationResult = await validationService.ValidateAsync(user, validatorLogin);
            if (!_validationResult.Success)
                return new LoginResponse(message: _validationResult.Message);

            var mappedModel = mapper.Map<AppUser>(user);
            mappedModel.PasswordHash = user.Password;

            bool loginResult = await userManagement.LoginUser(mappedModel);
            if (!loginResult)
                return new LoginResponse(message: "Email not found or invalid credentials");

            var _user = await userManagement.GetUserByEmail(user.Email);
            var claims = await userManagement.GetUserClaim(_user!.Email!);

            string jwtToken = tokenManagement.GeneratedToken(claims);
            string refreshToken = tokenManagement.GetRefreshToken();

            int saveTokenResul = 0;

            bool userTokenCheck = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (userTokenCheck) saveTokenResul = await tokenManagement.UpdateRefeshToken(_user.Id, refreshToken);
            saveTokenResul = await tokenManagement.AddRefeshToken(_user.Id, refreshToken); 
            return saveTokenResul <= 0 ? new LoginResponse(message: "Internal error occurred while authenticating") : 
                new LoginResponse(success: true, token: jwtToken, RefreshToken: refreshToken);
        }

        public async Task<LoginResponse> ReviveToken(string refreshToken)
        {
            bool validateTokenResult = await tokenManagement.ValidateRefreshToken(refreshToken);
            if (!validateTokenResult)
                return new LoginResponse(message: "Invalid token");

            string userId = await tokenManagement.GetUserIdByRefeshToken(refreshToken);
            AppUser? user = await userManagement.GetUserById(userId);
            var claims = await userManagement.GetUserClaim(user!.Email!);
            string newJwtToken = tokenManagement.GeneratedToken(claims);
            string newRefreshToken = tokenManagement.GetRefreshToken();
            await tokenManagement.UpdateRefeshToken(userId, newRefreshToken);
            return new LoginResponse(success: true, token: newJwtToken, RefreshToken: newRefreshToken);
        }
    }
}
