int[] FindSolution()
{
	Dictionary<string, Node> map_begin = new Dictionary<string, Node>();
	Dictionary<string, Node> map_end = new Dictionary<string, Node>();
	Queue<Node> q_begin = new Queue<Node>();
	Queue<Node> q_end = new Queue<Node>();
	Node n = new Node();

	n.Name = Serial(initState);
	n.Info = initState;
	n.Nivel = 0;
	n.Profundidade = 0;
	q_begin.Enqueue(n);
	map_begin.Add(n.Name, n);

	n = new Node();
	n.Name = Serial(target);
	n.Info = target;
	n.Nivel = 0;
	n.Profundidade = 0;
	q_end.Enqueue(n);
	map_end.Add(n.Name, n);

	while (q_begin.Count > 0 && q_end.Count > 0)
	{
		n = q_begin.Dequeue();
		List<Node> sucessors = GetSucessors(n);

		foreach (Node node in sucessors)
		{
			if (!map_begin.ContainsKey(node.Name))
			{
				q_begin.Enqueue(node);
				map_begin.Add(node.Name, node);
				if (map_end.ContainsKey(node.Name))
					return BuildAnswer(map_begin,map_end,node);
			}
		}

		n = q_end.Dequeue();
		sucessors = GetSucessors(n);

		foreach (Node node in sucessors)
		{
			if (!map_end.ContainsKey(node.Name))
			{
				q_end.Enqueue(node);
				map_end.Add(node.Name, node);
				if (map_begin.ContainsKey(node.Name))
					return BuildAnswer(map_begin, map_end, node);
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
int[] BuildAnswer(Dictionary<string, Node> map_begin, Dictionary<string, Node> map_end,Node node)
{
	List<int> list = new List<int>();
	Node n = map_begin[node.Name];
	while (n.Parent != null)
	{
		list.Add((int)n.Nivel);
		n = n.Parent;
	}
	list.Reverse();

	n = map_end[node.Name];
	while (n.Parent != null)
	{
		list.Add((int)n.Nivel);
		n = n.Parent;
	}
	return list.ToArray();
}