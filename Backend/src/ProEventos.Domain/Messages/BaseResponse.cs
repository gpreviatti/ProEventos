namespace ProEventos.Domain.Messages
{
    public class BaseResponse<T> where T: class
    {
        public bool Valid { get; set; } = true;
        public T Data { get; set; }
    }
}
