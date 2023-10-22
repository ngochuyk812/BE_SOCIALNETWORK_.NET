using AutoMapper;
using BE_SOCIALNETWORK.Config;
using BE_SOCIALNETWORK.Database.Model;
using BE_SOCIALNETWORK.DTO;
using BE_SOCIALNETWORK.Extensions;
using BE_SOCIALNETWORK.Payload.Response;

namespace BE_SOCIALNETWORK.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<Friend, FriendDto>().ReverseMap();
            CreateMap<Like, LikeDto>().ReverseMap();
            CreateMap<MediaComment, MediaCommentDto>().ReverseMap();
            CreateMap<MediaMessage, MediaMessageDto>().ReverseMap();
            CreateMap<MediaPost, MediaPostDto>().ReverseMap();
            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Participant, ParticipantDto>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
            CreateMap<CustomPostHome, CustomPostHomeDto>().ReverseMap();
            CreateMap<PaginatedItems<CustomPostHome>, PaginatedItems<CustomPostHomeDto>>().ReverseMap();

            CreateMap<LikeType, LikeTypeDto>().ForMember(des => des.Icon,
                opt => opt.MapFrom<UrlBase, string>(src=>src.Icon)).ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, SignInResponse>().ReverseMap();
            CreateMap<PaginatedItems<Post>, PaginatedItems<PostDto>>().ReverseMap();
            CreateMap<PaginatedItems<Comment>, PaginatedItems<CommentDto>>().ReverseMap();
            CreateMap<PaginatedItems<Like>, PaginatedItems<LikeDto>>().ReverseMap();



        }
    }
}
