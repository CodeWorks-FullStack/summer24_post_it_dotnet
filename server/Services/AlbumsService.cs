

namespace post_it_dotnet.Services;

public class AlbumsService
{
  private readonly AlbumsRepository _repository;

  public AlbumsService(AlbumsRepository repository)
  {
    _repository = repository;
  }

  public Album CreateAlbum(Album albumData)
  {
    Album album = _repository.CreateAlbum(albumData);
    return album;
  }

  public List<Album> GetAllAlbums()
  {
    List<Album> albums = _repository.GetAllAlbums();
    return albums;
  }
}