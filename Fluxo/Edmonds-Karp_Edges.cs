double EdmondsKarp(string init, string target)
{
	Node begin = Find(init);
	Node end = Find(target);
	double fluxoMax = 0;
	while (true)
	{
		List<Node> path = BreadthFirstSearch(begin, end);
		double small = -1;
		if (path == null)
		{
			PrintEdge(begin, end);
			return fluxoMax;
		}
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
List<Node> BreadthFirstSearch(Node begin, Node end)
{
	List<Node> nodes = new List<Node>();
	Queue<Node> queue = new Queue<Node>();
	Node node = begin;
	VisitedZero();

	node.Visited = true;
	queue.Enqueue(node);

	while (queue.Count > 0)
	{
		node = queue.Dequeue();
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
				queue.Enqueue(edge.To);
			}
		}
	}
	return null;
}
private List<Node> ListBreadthFirstSearch(Node begin)
{
	List<Node> nodes = new List<Node>();
	Queue<Node> queue = new Queue<Node>();
	Node node = begin;
	VisitedZero();

	node.Visited = true;
	queue.Enqueue(node);
	nodes.Add(node);

	while (queue.Count > 0)
	{
		node = queue.Dequeue();
		foreach (Edge edge in node.Edges)
		{
			if (edge.To.Visited == false && edge.Cost != 0 && edge.To.Edges.Find(x => x.To.Equals(edge.From)).Cost != 0)
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
private void VisitedZero()
{
	foreach (Node node in Nodes)
	{
		node.Visited = false;
		node.Parent = null;
	}
}
 void PrintEdge(Node begin, Node end)
{
	List<Node> init = ListBreadthFirstSearch(begin);
	List<Node> target = ListBreadthFirstSearch(end);
	List<Edge> edges = new List<Edge>();
	foreach (Node nodeBegin in init)
	{
		foreach (Node nodeEnd in target)
		{
			Edge edge = nodeBegin.Edges.Find(x => x.To.Equals(nodeEnd));
			if (edge != null && edge.Cost <= 0)
				edges.Add(edge);
		}
	}
	foreach (Edge edge in edges)
		Console.WriteLine(edge);
}
