List<Node> DepthFirstSearch(string begin)
{
	Node node = searchNode(begin);

	if (node == null)
		return null;

	List<Node> nodes = new List<Node>();
	Stack<Node> stack = new Stack<Node>();
	VisitedZero();

	node.Visited = true;
	stack.Push(node);
	nodes.Add(node);

	while (stack.Count > 0)
	{
		node = stack.Pop();
		foreach (Edge edge in node.Edges)
		{
			if (edge.To.Visited == false)
			{
				edge.To.Visited = true;
				edge.To.Parent = node;
				stack.Push(edge.To);
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