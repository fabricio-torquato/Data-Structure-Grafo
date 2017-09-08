public class PriorityQueue
{
	private SortedDictionary<double, Queue<Node>> queue;
	public PriorityQueue()
	{
		queue = new SortedDictionary<double, Queue<Node>>();
	}
	public void Enqueue(double value, Node node)
	{
		if (queue.ContainsKey(value) == false)
		{
			queue.Add(value, new Queue<Node>());
		}
		queue[value].Enqueue(node);
	}
	public KeyValuePair<double, Node> Dequeue()
	{
		Dictionary<double, Node> aux = new Dictionary<double, Node>();
		double k = queue.Keys.First();
		Node v = queue.Values.First().Dequeue();
		aux.Add(k, v);
		if (queue.Values.First().Count == 0)
			queue.Remove(k);
		return aux.First();
	}
	public KeyValuePair<double, Node> Peek()
	{
		Dictionary<double, Node> aux = new Dictionary<double, Node>();
		double k = queue.Keys.First();
		Node v = queue.Values.First().Peek();
		aux.Add(k, v);
		return aux.First();
	}
	public int ItsEmpty()
	{
		return queue.Count;
	}
}