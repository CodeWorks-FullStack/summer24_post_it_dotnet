
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
    
    SELECT * FROM albums WHERE id = LAST_INSERT_ID();";

    Album album = _db.Query<Album>(sql, albumData).FirstOrDefault();
    return album;
  }
}