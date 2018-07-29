using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using Unity;

namespace ImageMege.Controllers
{
    public class MergeController : ApiController
    {
        private readonly IPagedAlbumCollectionGenerator _pagedAlbumCollectionGenerator;
        private readonly UnityContainer _container;

        public MergeController(IPagedAlbumCollectionGenerator pagedAlbumCollectionGenerator)
        {
            _pagedAlbumCollectionGenerator = pagedAlbumCollectionGenerator;
            _container = new UnityContainer();
            _container.RegisterType<IPagedAlbumCollectionGenerator, PagedAlbumCollectionGenerator>();
        }

        [HttpGet]
        [SwaggerResponseRemoveDefaults]
        public IHttpActionResult GetAlbums(int pageNumber, int numberOfObjectsPerPage)
        {
            var pagedAlbumCollection = _pagedAlbumCollectionGenerator.Generate(pageNumber, numberOfObjectsPerPage);

            if (!ModelState.IsValid)
                return Content(HttpStatusCode.NoContent, ModelState);

            return Ok(pagedAlbumCollection);
        }
    }
}
