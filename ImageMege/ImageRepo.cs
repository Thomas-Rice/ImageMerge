using System.Collections.Generic;
using Newtonsoft.Json;

namespace ImageMege
{
    public class ImageRepo : IImageRepo
    {
        public IEnumerable<T> Consume<T>(string data)
        {
            return JsonConvert.DeserializeObject<IEnumerable<T>>(data);
        }
    }
}
