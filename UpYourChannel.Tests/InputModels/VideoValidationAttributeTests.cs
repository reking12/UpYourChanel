using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UpYourChannel.Web.ViewModels.Video;
using Xunit;

namespace UpYourChannel.Tests.InputModels
{
    public class VideoValidationAttributeTests
    {
        [Fact]
        public void VideoShouldBeValid()
        {
           // var input = new VideoInputModel
           // {
           //   //  Link = "https://www.youtube.com/watch?v=szzG2KxPUpQ&list=RDszzG2KxPUpQ&start_radio=1",
           //     Title = "Torino & Pashata - Усещаш ли[Official 4K Video]",
           //     Description = "Искаме да ви пожелаем, да бъдете по - добри,да се обичате",
           // };
           // var validator = new ConnectionModelValidator();
           //
           // var result = validator.Validate(connectionModel);
           // Validator.ValidateObject(input, VideoInputModel);


        }
    }
}
