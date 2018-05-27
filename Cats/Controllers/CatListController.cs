using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AglPets.Models;
using AglPets.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cats.Controllers
{
    [Produces("application/json")]
    [Route("api/CatList")]
    public class CatListController : Controller
    {
        private readonly IOwnerModelPresenter _ownerModelPresenter;
        private readonly ILogger<CatListController> _logger;

        public CatListController(IOwnerModelPresenter ownerModelPresenter, ILogger<CatListController> logger)
        {
            _ownerModelPresenter = ownerModelPresenter;
            _logger = logger;
        }

        public async Task<IEnumerable<OwnerModel>> Get()
        {
            try
            {
                return await _ownerModelPresenter.GetOwnerModelsAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Exception retrieving cat lists");
                throw;
            }
        }
    }
}