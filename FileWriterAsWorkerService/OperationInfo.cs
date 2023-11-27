namespace FileWriterServiceWorker
{
    public class OperationInfo
    {
        public string Path { get; set; }
        public string Value { get; set; }

        public OperationInfo(string path, string value)
        {
            Path = path;
            Value = value;
        }
    }
}