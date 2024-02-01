using System;
using AutoMapper;
using ForSendKH.Models;
using ForSendKH.Models.ModelsDto.Beneficiere;
using ForSendKH.Models.ModelsDto.Expediteur;
using ForSendKH.Models.ModelsDto.Transfert;
using ForSendKH.Models.ModelsDto.Users;

namespace ForSendKH.Configurations
{
	public class MapperConfig: Profile
	{
		public MapperConfig()
		{
			CreateMap<Expediteur, CreateExpediteurDto>().ReverseMap();
            CreateMap<Expediteur, GetExpediteurDto>().ReverseMap();
			CreateMap<Expediteur, ExpediteurDto>().ReverseMap();
            CreateMap<Expediteur, UpdateExpediteurDto>().ReverseMap();


            CreateMap<Transfert , TransfertDto>().ReverseMap();
            CreateMap<Transfert, GetTransfertDto>().ReverseMap();
            CreateMap<Transfert, CreateTransfertDto>().ReverseMap();
            CreateMap<Transfert, UpdateTransfertDto>().ReverseMap();
            CreateMap<Transfert, ExecuteTransactDto>().ReverseMap();


            CreateMap<Beneficiere, BeneficiereDto>().ReverseMap();
            CreateMap<Beneficiere, GetBeneficiereDto>().ReverseMap();
            CreateMap<Beneficiere, CreateBeneficiereDto>().ReverseMap();
            CreateMap<Beneficiere, UpdateBeneficiereDto>().ReverseMap();

            CreateMap<ApiUserDto, ApiUser>().ReverseMap();
        }
	}
}

