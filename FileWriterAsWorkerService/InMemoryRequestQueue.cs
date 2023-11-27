using System.Collections.Concurrent;

namespace FileWriterServiceWorker
{
    public class InMemoryRequestQueue
    {
        private readonly ConcurrentQueue<OperationInfo> queue = new ();

        public void Enqueue(OperationInfo operationInfo)
        {
            queue.Enqueue(operationInfo);
        }

        public OperationInfo? Dequeue()
        {
            return queue.TryDequeue(out var operationInfo) ? operationInfo : null;
        }
    }
}