






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
