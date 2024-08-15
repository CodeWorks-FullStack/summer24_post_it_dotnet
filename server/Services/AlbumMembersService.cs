






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

  internal List<AlbumMemberAlbum> GetAlbumMemberAlbumsByAccountId(string userId)
  {
    List<AlbumMemberAlbum> albumMemberAlbums = _repository.GetAlbumMemberAlbumsByAccountId(userId);
    return albumMemberAlbums;
  }

  internal List<AlbumMemberProfile> GetAlbumMemberProfilesByAlbumId(int albumId)
  {
    List<AlbumMemberProfile> albumMembers = _repository.GetAlbumMemberProfilesByAlbumId(albumId);
    return albumMembers;
  }
}
