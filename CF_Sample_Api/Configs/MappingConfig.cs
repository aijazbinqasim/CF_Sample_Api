namespace CF_Sample_Api.Configs
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<AuthorModel, GetAuthor>()
                .ReverseMap();

            CreateMap<PostAuthor, AuthorModel>()
                .ReverseMap();

            CreateMap<PutAuthor, AuthorModel>()
                .ReverseMap();

            CreateMap<PostAppUser, AppUserModel>();
        }
    }
}
