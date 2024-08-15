





namespace post_it_dotnet.Services;

public class AlbumMembersService
{
  private readonly AlbumMembersRepository _repository;

  public AlbumMembersService(AlbumMembersRepository repository)
  {
    _repository = repository;
  }

  internal AlbumMember CreateAlbumMember(AlbumMember albumMemberData)
  {
    AlbumMember albumMember = _repository.CreateAlbumMember(albumMemberData);
    return albumMember;
  }

  internal List<AlbumMember> GetAlbumMembersByAlbumId(int albumId)
  {
    List<AlbumMember> albumMembers = _repository.GetAlbumMembersByAlbumId(albumId);
    return albumMembers;
  }
}
