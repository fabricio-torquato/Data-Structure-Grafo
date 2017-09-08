List<Node> DepthFirstSearchRec(string begin)
{
	Node node = searchNode(begin);

	if (node == null)
		return null;

	VisitedZero();

	node.Visited = true;
	return recursivaDFS(node, new List<Node>());
}
List<Node> recursivaDFS(Node node, List<Node> nodes)
{
	foreach (Edge edge in node.Edges)
	{
		if (edge.To.Visited == false)
		{
			edge.To.Visited = true;
			edge.To.Parent = node;
			nodes.Add(edge.To);
			recursivaDFS(edge.To, nodes);
		}
	}
	return nodes;
}
void VisitedZero()
{
	foreach (Node node in Nodes)
	{
		node.Visited = false;
		node.Parent = null;
	}
}