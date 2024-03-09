namespace MultiLogApplication.Models.Common
{
    public class ReturnType<T>
    {
        public T ReturnValue;
        public List<T> ReturnList;
        public ReturnStatus ReturnStatus;
        public string ReturnMessage;
    }
}
