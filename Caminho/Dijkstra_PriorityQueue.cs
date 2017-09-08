List<Node> ShortestPathQueue(string begin, string end)
{
	PriorityQueue pQ = new PriorityQueue();
	List<Node> nodes = new List<Node>();

	VisitedFalse();

	Node n = SearchNode(begin);
	n.Visited = true;
	n.Info = Convert.ToDouble(0);
	pQ.Enqueue(0, n);

	while (!pQ.Peek().Value.Name.Equals(end))
	{
		n = pQ.Peek().Value;
		double coust = pQ.Peek().Key;
		nodes.Add(n);

		foreach (Edge e in n.Edges)
		{
			if (e.To.Visited == false || Convert.ToDouble(e.To.Info) > coust + Convert.ToDouble(e.Cost))
			{
				e.To.Parent = n;
				e.To.Visited = true;
				e.To.Info =Convert.ToDouble(coust + Convert.ToDouble(e.Cost));
				pQ.Enqueue(coust + Convert.ToDouble(e.Cost), e.To);

			}
		}
		pQ.Dequeue();
	}
	n = pQ.Peek().Value;
	n.Info = "S";
	nodes.Add(n);
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