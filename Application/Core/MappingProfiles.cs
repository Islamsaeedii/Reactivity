using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Application.Comments;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
            .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees
            .FirstOrDefault(x => x.IsHost).AppUser.UserName));


            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(a => a.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(a => a.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(a => a.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url));



            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(a => a.Author.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(a => a.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(a => a.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}