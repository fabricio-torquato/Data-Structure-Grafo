Graph Prim()
{
	Graph tree = new Graph();

	int limit = lista.Count - 1;
	int cont = 0;

	tree.AddNode(lista[0].Name, "");

	while (cont < limit)
	{
		double menor = 0;
		Edge edgeMenor = null;
		Node aux;
		foreach (Node node in tree.lista)
		{
			aux = SearchNode(node.Name);
			foreach (Edge edge in aux.Edges)
			{
				if (edgeMenor == null || edge.Cost < menor)
				{
					if (tree.SearchNode(edge.To.Name) == null)
					{
						menor = edge.Cost;
						edgeMenor = edge;
					}
				}
			}
		}
		if (edgeMenor != null)
		{
			tree.AddNode(edgeMenor.To.Name, "");
			tree.AddEdge(edgeMenor.From.Name, edgeMenor.To.Name, Convert.ToInt32(edgeMenor.Cost));
			cont++;
		}
	}
	return tree;
}