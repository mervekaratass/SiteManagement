using AutoMapper;
using SiteManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteManagement.Schema;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<UserRequest, User>();
        CreateMap<User,UserResponse>();
 

        CreateMap<ApartmentRequest, Apartment>();
        CreateMap<Apartment,ApartmentResponse>();

        CreateMap<MessageRequest, Message>();
        CreateMap<Message, MessageResponse>();

        CreateMap<Message,UserResponseReceiver>();
        CreateMap<Message, UserResponseSender>();
      
        CreateMap<UserMessageRequest, Message>();


        CreateMap<DuesBillRequest,DuesBill>();
        CreateMap<DuesBill, DuesBillResponse>();

        CreateMap<BankRequest, Bank>();
        CreateMap<Bank, BankResponse>();
        CreateMap<Bank,BankResponse2>();

        CreateMap<User,UserBankResponse>();
    }
}
