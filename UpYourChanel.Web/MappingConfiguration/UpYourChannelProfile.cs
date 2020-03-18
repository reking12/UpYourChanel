using AutoMapper;
using System.Linq;
using UpYourChannel.Data.Models;
using UpYourChannel.Web.ViewModels;
using UpYourChannel.Web.ViewModels.Post;
using UpYourChannel.Web.ViewModels.Video;

namespace UpYourChannel.Web.MappingConfiguration
{
    public class UpYourChannelProfile : Profile
    {
        public UpYourChannelProfile()
        {
            CreateMap<VideoInputModel, Video>();
            CreateMap<AddVideoInputViewModel, RequestedVideo>();
            CreateMap<PostInputViewModel, Post>();
            CreateMap<Post, PostViewModel>();
        }
    }
}
