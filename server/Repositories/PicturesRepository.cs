





namespace post_it_dotnet.Repositories;

public class PicturesRepository
{
  private readonly IDbConnection _db;

  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Picture CreatePicture(Picture pictureData)
  {
    string sql = @"
    INSERT INTO 
    pictures(imgUrl, albumId, creatorId)
    VALUES(@ImgUrl, @AlbumId, @CreatorId);
    
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    JOIN accounts ON accounts.id = pictures.creatorId
    WHERE pictures.id = LAST_INSERT_ID();";

    Picture picture = _db.Query<Picture, Profile, Picture>(sql, JoinCreator, pictureData).FirstOrDefault();
    return picture;
  }

  internal List<Picture> GetPicturesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT
    pictures.*,
    accounts.*
    FROM pictures
    JOIN accounts ON accounts.id = pictures.creatorId
    WHERE pictures.albumId = @albumId;";

    List<Picture> pictures = _db.Query<Picture, Profile, Picture>(sql, JoinCreator, new { albumId }).ToList();

    return pictures;
  }

  private Picture JoinCreator(Picture picture, Profile profile)
  {
    picture.Creator = profile;
    return picture;
  }
}
