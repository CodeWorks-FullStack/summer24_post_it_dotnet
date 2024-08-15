








namespace post_it_dotnet.Repositories;

public class AlbumMembersRepository
{
  private readonly IDbConnection _db;

  public AlbumMembersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal AlbumMemberProfile CreateAlbumMember(AlbumMember albumMemberData)
  {
    string sql = @"
    INSERT INTO
    albumMembers(albumId, accountId)
    VALUES(@AlbumId, @AccountId);
    
    SELECT 
    albumMembers.*,
    accounts.*
    FROM albumMembers
    JOIN accounts ON accounts.id = albumMembers.accountId
    WHERE albumMembers.id = LAST_INSERT_ID();";

    AlbumMemberProfile albumMember = _db.Query<AlbumMember, AlbumMemberProfile, AlbumMemberProfile>(sql, (albumMember, profile) =>
    {
      profile.AlbumId = albumMember.AlbumId;
      profile.AlbumMemberId = albumMember.Id;
      return profile;
    }, albumMemberData).FirstOrDefault();

    return albumMember;
  }

  internal void DestroyAlbumMember(int albumMemberId)
  {
    string sql = "DELETE FROM albumMembers WHERE id = @albumMemberId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { albumMemberId });

    switch (rowsAffected)
    {
      case 0:
        throw new Exception("FAILED TO DELETE");
      case 1:
        break;
      default:
        throw new Exception("DELETED TOO MANY ALBUM MEMBERS, CHECK YOUR SQL MANUAL FOR HOW TO RECTIFY THIS BAD THING THAT HAPPENED");
    }
  }

  internal List<AlbumMemberAlbum> GetAlbumMemberAlbumsByAccountId(string userId)
  {
    string sql = @"
    SELECT
    albumMembers.*,
    albums.*,
    accounts.*
    FROM albumMembers
    JOIN albums ON albumMembers.albumId = albums.id
    JOIN accounts ON accounts.id = albums.creatorId
    WHERE albumMembers.accountId = @userId;";

    List<AlbumMemberAlbum> albumMemberAlbums = _db.Query<AlbumMember, AlbumMemberAlbum, Profile, AlbumMemberAlbum>(sql,
    (albumMember, album, profile) =>
    {
      album.AccountId = albumMember.AccountId;
      album.AlbumMemberId = albumMember.Id;
      album.Creator = profile;
      return album;
    }, new { userId }).ToList();

    return albumMemberAlbums;
  }

  internal AlbumMember GetAlbumMemberById(int albumMemberId)
  {
    string sql = "SELECT * FROM albumMembers WHERE id = @albumMemberId;";

    AlbumMember albumMember = _db.Query<AlbumMember>(sql, new { albumMemberId }).FirstOrDefault();
    return albumMember;
  }

  internal List<AlbumMemberProfile> GetAlbumMemberProfilesByAlbumId(int albumId)
  {
    string sql = @"
    SELECT 
    albumMembers.*,
    accounts.* 
    FROM albumMembers
    JOIN accounts ON accounts.id = albumMembers.accountId
    WHERE albumId = @albumId;";

    List<AlbumMemberProfile> albumMembersProfiles = _db.Query<AlbumMember, AlbumMemberProfile, AlbumMemberProfile>(sql, (albumMember, profile) =>
    {
      profile.AlbumMemberId = albumMember.Id; // attaches the Id of the many-to-many to our DTO
      profile.AlbumId = albumMember.AlbumId;
      return profile;
    }, new { albumId }).ToList();
    return albumMembersProfiles;
  }
}
