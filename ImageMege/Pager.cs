using System.Collections.Generic;
using System.Linq;
using ImageMege.Models;

namespace ImageMege
{
    public static class Pager
    {
        public static IEnumerable<Album> Page(IEnumerable<Album> collection, int pageNumber, int numberOfObjectsPerPage)
        {
            return collection
                .Skip(numberOfObjectsPerPage * (pageNumber - 1))
                .Take(numberOfObjectsPerPage);
        }
    }
}