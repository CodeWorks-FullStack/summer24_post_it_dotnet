





namespace post_it_dotnet.Services;

public class PicturesService
{
  private readonly PicturesRepository _repository;
  private readonly AlbumsService _albumsService;

  public PicturesService(PicturesRepository repository, AlbumsService albumsService)
  {
    _repository = repository;
    _albumsService = albumsService;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    Album album = _albumsService.GetAlbumById(pictureData.AlbumId);

    if (album.Archived) // if(album.Archived == true)
    {
      throw new Exception($"{album.Title} has been archived and is no longer accepting picture submissions");
    }

    Picture picture = _repository.CreatePicture(pictureData);
    return picture;
  }

  internal string DestroyPicture(int pictureId, string userId)
  {
    Picture picture = GetPictureById(pictureId);

    if (picture.CreatorId != userId)
    {
      throw new Exception("NO YOU CANNOT DELETE A PICTURE THAT YOU DID NOT CREATE");
    }

    _repository.DestroyPicture(pictureId);

    return "Picture has been deleted, big dawg";
  }

  private Picture GetPictureById(int pictureId)
  {
    Picture picture = _repository.GetPictureById(pictureId);

    if (picture == null)
    {
      throw new Exception($"No picture found with id of {pictureId}");
    }

    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    List<Picture> pictures = _repository.GetPicturesByAlbumId(albumId);
    return pictures;
  }
}