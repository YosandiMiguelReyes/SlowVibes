namespace Domain.Result
{
    public class OperationResult<Tresult>
    {
        public Tresult Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
