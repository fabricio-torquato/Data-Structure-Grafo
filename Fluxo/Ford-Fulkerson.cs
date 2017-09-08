double EdmondsKarp(string init, string target)
{
	Node begin = Find(init);
	Node end = Find(target);
	double fluxoMax = 0;
	while (true)
	{
		List<Node> path = DepthFirstSearch(begin, end);
		double small = -1;
		if (path == null)
			return fluxoMax;
		for (int i = 0; i < path.Count - 1; i++)
		{
			int index = path[i].Edges.FindIndex(x => x.To.Equals(path[i + 1]));
			if (small > path[i].Edges[index].Cost || small == -1)
				small = (double)path[i].Edges[index].Cost;
		}
		fluxoMax += small;
		for (int i = 0; i < path.Count - 1; i++)
		{
			int indexA = path[i].Edges.FindIndex(x => x.To.Equals(path[i + 1]));
			if (!path[i + 1].Edges.Exists(x => x.To.Equals(path[i])))
			{
				path[i + 1].AddEdge(path[i], 0);
			}
			int indexB = path[i + 1].Edges.FindIndex(x => x.To.Equals(path[i]));
			path[i + 1].Edges[indexB].Cost += small;
			path[i].Edges[indexA].Cost -= small;
		}
	}
}
List<Node> DepthFirstSearch(Node begin, Node end)
{
	List<Node> nodes = new List<Node>();
	Stack<Node> stack = new Stack<Node>();
	Node node = begin;
	VisitedZero();

	node.Visited = true;
	stack.Push(node);

	while (stack.Count > 0)
	{
		node = stack.Pop();
		if (node.Equals(end))
		{
			while (node != null)
			{
				nodes.Add(node);
				node = node.Parent;
			}
			nodes.Reverse();
			return nodes;
		}
		foreach (Edge edge in node.Edges)
		{
			if (edge.To.Visited == false && edge.Cost != 0)
			{
				edge.To.Visited = true;
				edge.To.Parent = node;
				stack.Push(edge.To);
			}
		}
	}
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
Node Find(string name)
{
	return this.nodes.SingleOrDefault(e => e.Name == name);
}