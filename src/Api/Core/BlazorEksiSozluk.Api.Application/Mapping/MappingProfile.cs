using AutoMapper;
using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Common.Models.Queries;
using BlazorEksiSozluk.Common.Models.RequestModels;

namespace BlazorEksiSozluk.Api.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, LoginUserViewModel>().ReverseMap();
            CreateMap<User, CreateUserCommand>().ReverseMap();
            CreateMap<User, UpdateUserCommand>().ReverseMap();
            CreateMap<Entry, CreateEntryCommand>().ReverseMap();
            CreateMap<EntryComment, CreateEntryCommentCommand>().ReverseMap();
            CreateMap<Entry, GetEntriesViewModel>()
                .ForMember(x => x.CommentCount, y => y.MapFrom(z => z.EntryComments.Count));
        }
    }
}
