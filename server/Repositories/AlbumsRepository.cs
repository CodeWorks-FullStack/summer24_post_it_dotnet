

namespace post_it_dotnet.Repositories;

public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO
    albums(creatorId, title, description, coverImg, category)
    VALUES(@CreatorId, @Title, @Description, @CoverImg, @CatEgory);
    
    SELECT 
    albums.*,
    accounts.*
    FROM albums 
    JOIN accounts ON accounts.id = albums.creatorId
    WHERE albums.id = LAST_INSERT_ID();";

    Album album = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    {
      album.Creator = profile;
      return album;
    }, albumData).FirstOrDefault();
    return album;
  }

  internal List<Album> GetAllAlbums()
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON accounts.id = albums.creatorId;";

    List<Album> albums = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    {
      album.Creator = profile;
      return album;
    }).ToList();

    return albums;
  }
}