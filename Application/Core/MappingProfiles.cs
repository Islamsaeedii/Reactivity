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
            string currentUsername = null;
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
            .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees
            .FirstOrDefault(x => x.IsHost).AppUser.UserName));


            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(a => a.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(a => a.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(a => a.AppUser.Bio))
                .ForMember(d => d.Image, o => o.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(x => x.AppUser.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(x => x.AppUser.Followings.Count))
                .ForMember(d => d.Following, o => o.MapFrom(s => s.AppUser.Followers.Any(x => x.Observer.UserName == currentUsername)));



            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(d => d.Image, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(d => d.FollowersCount, o => o.MapFrom(x => x.Followers.Count))
                .ForMember(d => d.FollowingCount, o => o.MapFrom(x => x.Followings.Count))
                .ForMember(d => d.Following, o => o.MapFrom(s => s.Followers.Any(x => x.Observer.UserName == currentUsername)));


            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(a => a.Author.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(a => a.Author.UserName))
                .ForMember(d => d.Image, o => o.MapFrom(a => a.Author.Photos.FirstOrDefault(x => x.IsMain).Url));

            CreateMap<ActivityAttendee, Profiles.UserActivityDto>()
                 .ForMember(d => d.Id, o => o.MapFrom(s => s.Activity.Id))
                 .ForMember(d => d.Date, o => o.MapFrom(s => s.Activity.Date))
                 .ForMember(d => d.Title, o => o.MapFrom(s => s.Activity.Title))
                  .ForMember(d => d.Category, o => o.MapFrom(s => s.Activity.Category))
                  .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Activity.Attendees.FirstOrDefault(x => x.IsHost).AppUser.UserName));
        }
    }
}