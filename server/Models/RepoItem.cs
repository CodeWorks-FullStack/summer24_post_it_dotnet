namespace post_it_dotnet.Models;

// <T> allows the user to supply a type when inheriting from this class
// abstract denotes that this class can never be newed up, only inherited from
public abstract class RepoItem<T>
{
  public T Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

}