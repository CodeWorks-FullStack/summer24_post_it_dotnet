



namespace post_it_dotnet.Services;

public class PicturesService
{
  private readonly PicturesRepository repository;

  public PicturesService(PicturesRepository repository)
  {
    this.repository = repository;
  }
}