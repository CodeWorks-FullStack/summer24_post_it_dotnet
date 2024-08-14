namespace post_it_dotnet.Models;

public class Picture : RepoItem<int>
{
  // NOTE these properties are inherited from RepoItem
  // public int Id { get; set; }
  // public DateTime CreatedAt { get; set; }
  // public DateTime UpdatedAt { get; set; }
  public string ImgUrl { get; set; }
  public string CreatorId { get; set; }
  public int AlbumId { get; set; }
  public Profile Creator { get; set; }
}
