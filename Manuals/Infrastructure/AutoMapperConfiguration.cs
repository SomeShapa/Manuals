using AutoMapper;
using Manuals.Entities;
using Manuals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manuals.Infrastructure
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Configuration.AllowNullCollections = true;
            Mapper.Configuration.AllowNullDestinationValues = true;

            Mapper.CreateMap<Manual, ManualViewModel>().
                ForMember(d => d.Rating, s => s.MapFrom(e => (e.Ratings.Count(r => r.Liked == true) - e.Ratings.Count(r => r.Liked == false))));
            Mapper.CreateMap<ManualViewModel, Manual>();

            Mapper.CreateMap<Category, CategoryViewModel>();
            Mapper.CreateMap<CategoryViewModel, Category>();

            Mapper.CreateMap<Comment, CommentViewModel>().
                ForMember(d => d.Rating, s => s.MapFrom(e => (e.RatingComments.Count())));
            Mapper.CreateMap<CommentViewModel, Comment>();

            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<TagViewModel,Tag>();

            //Mapper.CreateMap<ApplicationUser, ApplicationUser>()
            //    .ForMember(d => d.Manuals, opt => opt.Ignore());
            Mapper.CreateMap<UserViewModel, ApplicationUser>();

        }
    }
}