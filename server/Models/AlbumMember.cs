namespace post_it_dotnet.Models;

// backs the many-to-many only
public class AlbumMember : RepoItem<int>
{
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
}

// backs the profile view of the many-to-many
public class AlbumMemberProfile : Profile
{
  // All members (properties) from profile are inherited
  public int AlbumMemberId { get; set; }
  public int AlbumId { get; set; }
}


// backs the album view of the many-to-many
public class AlbumMemberAlbum : Album
{
  // All members (properties) from album are inherited
  public int AlbumMemberId { get; set; }
  public string AccountId { get; set; }
}