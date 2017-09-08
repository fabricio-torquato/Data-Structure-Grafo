List<Node> ShortestPath(string begin, string end)
{
	Node node = searchNode(begin);
	Node limit = searchNode(end);

	if (node == null || limit == null)
		return null;

	List<Node> nodes = new List<Node>();
	Graph src = new Graph();

	VisitedZero();

	node.Visited = true;
	src.AddNode(node.Name, "0");

	while (!node.Equals(limit))
	{
		Edge smaller = null;
		double smallerCust = 0;

		foreach (Node outer in src.Nodes)
		{
			Node inner = this.searchNode(outer.Name);
			foreach (Edge edge in inner.Edges)
			{
				if (edge.To.Visited == false)
				{
					if (smaller == null || (edge.Cost + Convert.ToDouble( outer.Info)) < smallerCust)
					{
						smaller = edge;
						smallerCust = (edge.Cost + Convert.ToDouble( outer.Info));

						nodes.Add(edge.To);
					}
				}
			}
		}

		src.AddNode(smaller.To.Name, smallerCust.ToString());
		src.AddEdge(smaller.From.Name, smaller.To.Name, 0);

		smaller.To.Visited = true;
		smaller.To.Parent = smaller.From;

		node = smaller.To;
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