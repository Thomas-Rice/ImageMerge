using System.Collections.Generic;

namespace ImageMege.Services
{
    public interface IImageRepo
    {
        IEnumerable<T> Consume<T>(string data);
    }
}