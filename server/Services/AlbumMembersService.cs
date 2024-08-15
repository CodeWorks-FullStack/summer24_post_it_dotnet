








namespace post_it_dotnet.Services;

public class AlbumMembersService
{
  private readonly AlbumMembersRepository _repository;

  public AlbumMembersService(AlbumMembersRepository repository)
  {
    _repository = repository;
  }

  internal AlbumMemberProfile CreateAlbumMember(AlbumMember albumMemberData)
  {
    AlbumMemberProfile albumMember = _repository.CreateAlbumMember(albumMemberData);
    return albumMember;
  }

  internal string DestroyAlbumMember(int albumMemberId, string userId)
  {
    AlbumMember albumMember = GetAlbumMemberById(albumMemberId);

    if (albumMember.AccountId != userId)
    {
      throw new Exception("YOU CAN NOT DELETE ANOTHER USER'S ALBUM MEMBER");
    }

    _repository.DestroyAlbumMember(albumMemberId);

    return "No longer an album member!";
  }

  private AlbumMember GetAlbumMemberById(int albumMemberId)
  {
    AlbumMember albumMember = _repository.GetAlbumMemberById(albumMemberId);

    if (albumMember == null)
    {
      throw new Exception($"No album member found with the id of {albumMemberId}");
    }

    return albumMember;
  }

  internal List<AlbumMemberAlbum> GetAlbumMemberAlbumsByAccountId(string userId)
  {
    List<AlbumMemberAlbum> albumMemberAlbums = _repository.GetAlbumMemberAlbumsByAccountId(userId);
    return albumMemberAlbums;
  }

  internal List<AlbumMemberProfile> GetAlbumMemberProfilesByAlbumId(int albumId)
  {
    List<AlbumMemberProfile> albumMembersProfiles = _repository.GetAlbumMemberProfilesByAlbumId(albumId);
    return albumMembersProfiles;
  }
}
