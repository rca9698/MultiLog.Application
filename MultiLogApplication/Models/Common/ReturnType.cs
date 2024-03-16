namespace MultiLogApplication.Models.Common
{
    public class ReturnType<T>
    {
        public T ReturnVal { get; set; }
        public ReturnStatus ReturnStatus { get; set; }
        public List<T> ReturnList { get; set; }
        public string ReturnMessage { get; set; }
    }
}
