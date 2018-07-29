using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace ImageMege.Controllers
{
    public class MergeController : ApiController
    {
        private readonly PagedAlbumCollectionGenerator _pagedAlbumCollectionGenerator;


        public MergeController()
        {
            _pagedAlbumCollectionGenerator = new PagedAlbumCollectionGenerator();
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
