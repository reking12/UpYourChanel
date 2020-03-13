using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.MappingConfiguration
{
    public class UpYourChannelProfile : Profile
    {
        public UpYourChannelProfile()
        {
            CreateMap<VideoInputModel, Video>();
            CreateMap<AddVideoInputViewModel, RequestedVideo>();

        }
    }
}
