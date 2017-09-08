Graph Kruskal()
{
	PriorityQueue pQ = new PriorityQueue();
	Graph tree = new Graph();

	int limit = lista.Count - 1;
	int cont = 0;

	foreach (Node node in lista)
	{
		foreach (Edge edge in node.Edges)
		{
			pQ.Enqueue(edge.Cost, edge);
		}
	}
	while (cont < limit)
	{
		Edge edge = pQ.Dequeue();
		if (tree.SearchNode(edge.To.Name) == null)
			tree.AddNode(edge.To.Name, "");
		if (tree.SearchNode(edge.From.Name) == null)
			tree.AddNode(edge.From.Name, "");
		Node to = tree.SearchNode(edge.To.Name);
		Node from = tree.SearchNode(edge.From.Name);
		if (!tree.FindUnion(to, from))
		{
			tree.AddEdge(edge.From.Name, edge.To.Name, Convert.ToInt32(edge.Cost));
			cont++;
		}
	}
	return tree;
}
private Boolean FindUnion(Node a, Node b)
{
	Queue<Node> pilha = new Queue<Node>();

	VisitedFalse();

	Node node = a;

	if (node != null)
	{
		pilha.Enqueue(node);
		node.Visited = true;

		while (pilha.Count > 0)
		{
			Node aux = pilha.Dequeue();
			foreach (Edge edge in aux.Edges)
			{
				if (edge.To.Visited == false)
				{
					pilha.Enqueue(edge.To);
					edge.To.Visited = true;
				}
				if (edge.To.Name.CompareTo(b.Name) == 0)
					return true;
			}
		}
	}
	return false;
}
void VisitedFalse()
{
	foreach (Node node in lista)
	{
		node.Visited = false;
		node.Parent = null;
	}
}