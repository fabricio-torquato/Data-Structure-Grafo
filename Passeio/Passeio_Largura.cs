 List<Node> BreadthFirstSearch(string begin)
{
	Node node = searchNode(begin);

	if (node == null)
		return null;

	List<Node> nodes = new List<Node>();
	Queue<Node> queue = new Queue<Node>();
	VisitedZero();

	node.Visited = true;
	queue.Enqueue(node);
	nodes.Add(node);

	while (queue.Count > 0)
	{
		node = queue.Dequeue();
		foreach (Edge edge in node.Edges)
		{
			if (edge.To.Visited == false)
			{
				edge.To.Visited = true;
				edge.To.Parent = node;
				queue.Enqueue(edge.To);
				nodes.Add(edge.To);
			}
		}
	}
	return nodes;
}
Node searchNode(String name)
{
	foreach (Node node in Nodes)
		if (node.Name.CompareTo(name) == 0)
			return node;
	return null;
}
void VisitedZero()
{
	foreach (Node node in Nodes)
	{
		node.Visited = false;
		node.Parent = null;
	}
}