namespace post_it_dotnet.Models;

public class AlbumMember : RepoItem<int>
{
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
}

public class AlbumMemberProfile : Profile
{
  // All members (properties) from profile are inherited
  public int AlbumMemberId { get; set; }
  public int AlbumId { get; set; }
}


public class AlbumMemberAlbum : Album
{
  public int AlbumMemberId { get; set; }
  public string AccountId { get; set; }
}