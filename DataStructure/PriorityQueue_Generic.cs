public class PriorityQueue<T, K> where K : class
{
	private Dictionary<T,Queue<K>> queue;
	public int Count { get; set; }

	public PriorityQueue()
	{
		queue = new Dictionary<T, Queue<K>>();
		Count = 0;
	}

	public void Enqueue(T key, K v)
	{
		if (!queue.ContainsKey(key))
		{
			queue.Add(key, new Queue<K>());
		}
		queue[key].Enqueue(v);
		Count++;
	}

	public K Dequeue()
	{
		if (Count == 0)
			return null;

		T minKey = queue.Keys.Min();
		K v = queue[minKey].Dequeue();

		if (queue[minKey].Count == 0)
			queue.Remove(minKey);

		Count--;
		return v;
	}
	public T MinKey()
	{
		if (Count == 0)
			return default(T);

		return queue.Keys.Min();
	}

}