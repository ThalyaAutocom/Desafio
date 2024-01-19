using Microsoft.AspNetCore.Mvc;

namespace Desafio.API;

[ApiController]
[Route("v{version:apiVersion}/[controller]")]
public abstract class DesafioControllerBase : ControllerBase
{
    protected DesafioControllerBase()
    {

    }
}
