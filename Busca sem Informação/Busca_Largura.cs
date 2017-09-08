int[] FindSolution()
{
	Dictionary<string, Node> map = new Dictionary<string, Node>();
	Queue<Node> q = new Queue<Node>();
	Node n = new Node();

	n.Name = Serial(initState);
	n.Info = initState;
	n.Nivel = 0;
	n.Profundidade = 0;
	q.Enqueue(n);
	map.Add(n.Name, n);

	while (q.Count > 0)
	{
		n = q.Dequeue();

		List<Node> sucessors = GetSucessors(n);

		foreach (Node node in sucessors)
		{
			if (!map.ContainsKey(node.Name))
			{
				q.Enqueue(node);
				map.Add(node.Name, node);
				if (TargetFound(node))
					return BuildAnswer(node);
			}
		}
	}
	return null;
}
string Serial(int[] valor)
{
	return string.Join(" ", valor.Select(x => x.ToString()).ToArray());
}
List<Node> GetSucessors(Node n)
{
	List<Node> list = new List<Node>();
	int[] aux = (int[])n.Info;
	int pos = Array.IndexOf(aux, 0);
	int tam = (int)Math.Pow(aux.Length, 0.5);

	if (pos % tam > 0)
	{
		int[] copy = (int[])aux.Clone();
		copy[pos] = copy[pos - 1];
		copy[pos - 1] = 0;

		list.Add(CreateNode(copy, n, pos));
	}
	if (pos % tam < (tam - 1))
	{
		int[] copy = (int[])aux.Clone();
		copy[pos] = copy[pos + 1];
		copy[pos + 1] = 0;

		list.Add(CreateNode(copy, n, pos));
	}
	if (pos >= tam)
	{
		int[] copy = (int[])aux.Clone();
		copy[pos] = copy[pos - tam];
		copy[pos - tam] = 0;

		list.Add(CreateNode(copy, n, pos));
	}
	if (pos < aux.Length - tam)
	{
		int[] copy = (int[])aux.Clone();
		copy[pos] = copy[pos + tam];
		copy[pos + tam] = 0;
		list.Add(CreateNode(copy, n, pos));
	}
	return list;
}
Node CreateNode(int[] value, Node parent, int index)
{
	Node node = new Node();
	node.Name = Serial(value);
	node.Info = value;
	node.Nivel = value[index];
	node.Profundidade = parent.Profundidade + 1;
	node.Parent = parent;
	return node;
}

int[] BuildAnswer(Node n)
{
	List<int> list = new List<int>();
	while (n.Parent != null)
	{
		list.Add((int)n.Nivel);
		n = n.Parent;
	}
	list.Reverse();
	return list.ToArray();
}

bool TargetFound(Node n)
{
	return Enumerable.SequenceEqual((int[])n.Info, target);
}
