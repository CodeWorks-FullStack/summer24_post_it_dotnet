namespace post_it_dotnet.Models;

public class AlbumMember : RepoItem<int>
{
  public int AlbumId { get; set; }
  public string AccountId { get; set; }
}