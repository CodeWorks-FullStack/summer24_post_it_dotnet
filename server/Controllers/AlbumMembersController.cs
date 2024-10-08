namespace post_it_dotnet.Controllers;

[ApiController]
[Route("api/collaborators")]
public class AlbumMembersController : ControllerBase
{
  private readonly AlbumMembersService _albumMembersService;


  private readonly Auth0Provider _auth0Provider;
  public AlbumMembersController(AlbumMembersService albumMembersService, Auth0Provider auth0Provider)
  {
    _albumMembersService = albumMembersService;
    _auth0Provider = auth0Provider;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<AlbumMemberProfile>> CreateAlbumMember([FromBody] AlbumMember albumMemberData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      albumMemberData.AccountId = userInfo.Id;
      AlbumMemberProfile albumMember = _albumMembersService.CreateAlbumMember(albumMemberData);
      return Ok(albumMember);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }

  [HttpDelete("{albumMemberId}")]
  [Authorize]
  public async Task<ActionResult<string>> DestroyAlbumMember(int albumMemberId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _albumMembersService.DestroyAlbumMember(albumMemberId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
