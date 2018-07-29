using System.Collections.Generic;

namespace ImageMege
{
    public interface IImageRepo
    {
        IEnumerable<T> Consume<T>(string data);
    }
}