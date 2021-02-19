using AutoMapper;
using Roller.DataContext.Entity;
using Roller.Repository;
using Roller.Repository.AccountRepo;
using Roller.Repository.Customers;
using Roller.Repository.LoanRepos;
using Roller.Repository.TransactRepo;
using Roller.Web.Models;
using Roller.Web.Models.RoleViewModel;
using Roller.Web.Models.UserViewModels;
using Roller.Web.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roller.Web.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //CreateMap<AdminModel, Admin>().ReverseMap();
            CreateMap<RoleModel, Role>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<RegisterViewModel, User>().ReverseMap();
           CreateMap<LoginViewModel, User>().ReverseMap();
            CreateMap<CustomerProfile, Customer>().ReverseMap();
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<EmailSettings, Customer>().ReverseMap();
            CreateMap<CustomerAccountDto, Account>().ReverseMap();
            CreateMap<ShowBalanceDto, Account>().ReverseMap();
            CreateMap<ShowBalanceDto, Customer>().ReverseMap();
            CreateMap<TransactionDto, Transaction>().ReverseMap();
            CreateMap<LoanModel, Loan>().ReverseMap();
            CreateMap<SendMialViewModel, Customer>().ReverseMap();
            CreateMap<RecievedMail, Customer>().ReverseMap();
            CreateMap<SenderModel, Customer>().ReverseMap();
            CreateMap<EmailSentLog, Customer>().ReverseMap();           
            CreateMap<UserViewWebModel, User>().ReverseMap();
            CreateMap<UserWebModel, User>().ReverseMap();
            CreateMap<EditProfileDto, Customer>().ReverseMap();
            
        }

    }
}
