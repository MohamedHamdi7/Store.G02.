namespace Shared
{
    public class ValidtionError
    {
        public string Field { get; set; }

        public IEnumerable<string> Error { get; set; }
    }
}