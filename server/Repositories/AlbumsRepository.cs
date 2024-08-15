



namespace post_it_dotnet.Repositories;



public class AlbumsRepository
{
  private readonly IDbConnection _db;

  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal void ArchiveAlbum(Album albumToArchive)
  {
    string sql = @"
    UPDATE albums
    SET archived = @Archived
    WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, albumToArchive);

    if (rowsAffected == 0) throw new Exception("ARCHIVE FAILED");
    if (rowsAffected > 1) throw new Exception("ARCHIVED MORE THAN ONE ALBUM, UH OH");
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

    // Album album = _db.Query<Album, Profile, Album>(sql, (album, profile) =>
    // {
    //   album.Creator = profile;
    //   return album;
    // }, albumData).FirstOrDefault();

    Album album = _db.Query<Album, Profile, Album>(sql, JoinCreator, albumData).FirstOrDefault();
    return album;
  }

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
    SELECT
    albums.*,
    accounts.*
    FROM albums
    JOIN accounts ON accounts.id = albums.creatorId
    WHERE albums.id = @albumId;";

    Album album = _db.Query<Album, Profile, Album>(sql, JoinCreator, new { albumId }).FirstOrDefault();
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

    List<Album> albums = _db.Query<Album, Profile, Album>(sql, JoinCreator).ToList();

    return albums;
  }

  private Album JoinCreator(Album album, Profile profile)
  {
    album.Creator = profile;
    return album;
  }
}