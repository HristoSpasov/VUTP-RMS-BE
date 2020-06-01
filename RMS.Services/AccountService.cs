namespace RMS.Services
{
    using AutoMapper;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Internal;
    using Microsoft.Extensions.Logging;
    using RMS.API.Models.RequestModels;
    using RMS.Data.Entities;
    using RMS.Repositories.Contracts;
    using RMS.Services.Contracts;
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class AccountService : IAccountService
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly ILogger<AccountService> logger;
        private readonly IAccountRepository accountRepository;
        private readonly IJwtService jwtService;

        public AccountService(IMapper mapper, UserManager<User> userManager, ILogger<AccountService> logger, IAccountRepository accountRepository, IJwtService jwtService)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.logger = logger;
            this.accountRepository = accountRepository;
            this.jwtService = jwtService;
        }

        public async Task<ClaimsIdentity> GetClaimsIdentityAsync(LoginRequestModel loginRequestModel)
        {
            var userName = loginRequestModel.UserName;
            var password = loginRequestModel.Password;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            // get the user to verifty
            var userToVerify = await this.userManager.FindByNameAsync(userName);

            if (userToVerify == null)
            {
                return await Task.FromResult<ClaimsIdentity>(null);
            }

            var userRoles = await this.userManager.GetRolesAsync(userToVerify);

            // check the credentials
            if (await this.userManager.CheckPasswordAsync(userToVerify, password))
            {
                return await Task.FromResult(this.jwtService.GenerateClaimsIdentity(userName, userToVerify.Id, userRoles));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        public async  Task<User> GetUserByEmailAsync(string email)
        {
            return await this.userManager.FindByEmailAsync(email);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await this.userManager.FindByNameAsync(userName);
        }

        public async Task RegisterUserAsync(RegisterUserRequstModel registerUserRequstModel)
        {
            var identityUser = this.mapper.Map<User>(registerUserRequstModel);

            var result = await this.userManager.CreateAsync(identityUser, registerUserRequstModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => $"Code: {e.Code}; Description:{e.Description }").Join(Environment.NewLine);
                this.logger.LogError(errors);
                throw new InvalidOperationException(errors);
            }

            await this.userManager.AddToRoleAsync(identityUser, "User");
            await this.accountRepository.AddAsync(identityUser);
            await this.accountRepository.SaveAsync();
        }
    }
}
