





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

  internal List<Profile> GetAlbumMembersByAlbumId(int albumId)
  {
    string sql = @"
    SELECT 
    albumMembers.*,
    accounts.* 
    FROM albumMembers
    JOIN accounts ON accounts.id = albumMembers.accountId
    WHERE albumId = @albumId;";

    List<Profile> albumMembers = _db.Query<AlbumMember, Profile, Profile>(sql, (albumMember, profile) =>
    {
      return profile;
    }, new { albumId }).ToList();
    return albumMembers;
  }
}
