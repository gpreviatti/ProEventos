using System.Collections.Generic;

namespace ProEventos.Domain.Messages
{
    public class ListResponse<T> : BaseResponse where T: class
    {
        public IEnumerable<T> Data { get; set; }
    }
}
