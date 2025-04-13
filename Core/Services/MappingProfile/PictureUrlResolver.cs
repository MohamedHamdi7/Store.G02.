using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Shared.Dto;

namespace Services.MappingProfile
{

    //use this class to resolve PicUrl in productprofile

                                  // configuration to call appsetting
    public class PictureUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
           if(string.IsNullOrEmpty(source.PictureUrl))return string.Empty;

            return $"{configuration["BaseUrl"]}/{source.PictureUrl}";
        }         // configuration to call appsetting
    }
}
