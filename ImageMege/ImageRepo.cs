using System.Collections.Generic;
using ImageMege.Services;
using Newtonsoft.Json;

namespace ImageMege
{
    public class ImageRepo : IImageRepo
    {
        public IEnumerable<T> Consume<T>(string data)
        {
            return data != "" ? JsonConvert.DeserializeObject<IEnumerable<T>>(data) : new List<T>();
        }
    }
}
