bool Hamiltonian()
{
	foreach (Node n in this.nodes)
	{
		bool ret = this.Hamiltonian(n);
		if (ret) return true;
	}
	return false;
}
bool Hamiltonian(Node n)
{
	// Cria lista para armazenar o resultado..
	Queue<Node> queue = new Queue<Node>();
	// Arvore
	Graph arvore = new Graph();
	int id = 0;
	id++;
	arvore.AddNode(id.ToString(),n.Name);
	queue.Enqueue(arvore.Find(id.ToString()));

	// Realiza a busca..
	while (queue.Count > 0)
	{
		Node np = queue.Dequeue();
		Node currentNode = this.Find(np.Info.ToString());
		if (this.nodes.Count == CountNodes(np))
			return true;

		foreach (Edge edge in currentNode.Edges)
		{
			if (!ExistNode(np, edge.To.Name))
			{
				id++;
				arvore.AddNode(id.ToString(), edge.To.Name);
				Node nf = arvore.Find(id.ToString());
				queue.Enqueue(nf);
				arvore.AddEdge(nf.Name, np.Name, 1);
			}
		}
	}
	return false;
}
bool ExistNode(Node np, string p)
{
	if (np == null) return false;
	while (np.Edges.Count > 0) 
	{
		if (np.Info.ToString() == p) return true;
		np = np.Edges[0].To; 
	}
	return np.Info.ToString() == p;
}
int CountNodes(Node np)
{
	if (np == null) return 0;
	int count = 1;
	while (np.Edges.Count > 0)
	{ count++; np = np.Edges[0].To; }
	return count;
}