





namespace post_it_dotnet.Repositories;

public class AlbumMembersRepository
{
  private readonly IDbConnection _db;

  public AlbumMembersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal AlbumMember CreateAlbumMember(AlbumMember albumMemberData)
  {
    string sql = @"
    INSERT INTO
    albumMembers(albumId, accountId)
    VALUES(@AlbumId, @AccountId);
    
    SELECT * FROM albumMembers WHERE id = LAST_INSERT_ID();";

    AlbumMember albumMember = _db.Query<AlbumMember>(sql, albumMemberData).FirstOrDefault();

    return albumMember;
  }

  internal List<AlbumMember> GetAlbumMembersByAlbumId(int albumId)
  {
    string sql = @"
    SELECT * 
    FROM albumMembers WHERE albumId = @albumId;";

    List<AlbumMember> albumMembers = _db.Query<AlbumMember>(sql, new { albumId }).ToList();
    return albumMembers;
  }
}
