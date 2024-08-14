



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

  public Album GetAlbumById(int albumId)
  {
    Album album = _repository.GetAlbumById(albumId);

    if (album == null)
    {
      throw new Exception($"No album found with the id of {albumId}");
    }

    return album;
  }

  internal Album ArchiveAlbum(int albumId, string userId)
  {
    Album albumToArchive = GetAlbumById(albumId);

    if (albumToArchive.CreatorId != userId)
    {
      throw new Exception("YOU ARE NOT ALLOWED TO ARCHIVE SOMEONE ELSE'S ALBUM");
    }

    albumToArchive.Archived = !albumToArchive.Archived;

    _repository.ArchiveAlbum(albumToArchive);

    return albumToArchive;
  }
}