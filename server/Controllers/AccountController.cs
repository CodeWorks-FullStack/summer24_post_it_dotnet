namespace post_it_dotnet.Controllers;

[Authorize] // All routes in controller require authorization
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
  private readonly AlbumMembersService _albumMembersService;

  public AccountController(AccountService accountService, Auth0Provider auth0Provider, AlbumMembersService albumMembersService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _albumMembersService = albumMembersService;
  }

  [HttpGet]
  public async Task<ActionResult<Account>> Get()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      return Ok(_accountService.GetOrCreateAccount(userInfo));
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("collaborators")] // no slash before route
  public async Task<ActionResult<List<Album>>> GetAlbumMemberAlbumsByAccountId()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      List<Album> albumMemberAlbums = _albumMembersService.GetAlbumMemberAlbumsByAccountId(userInfo.Id);
      return Ok(albumMemberAlbums);
    }
    catch (Exception exception)
    {
      return BadRequest(exception.Message);
    }
  }
}
