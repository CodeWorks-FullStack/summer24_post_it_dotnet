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
}
