int[] FindSolution()
{
	PriorityQueue<int, Node> q_begin = new PriorityQueue<int, Node>();
	PriorityQueue<int, Node> q_end = new PriorityQueue<int, Node>();

	List<Node> sucessors_begin;
	List<Node> sucessors_end;

	map_begin = new Dictionary<string, Node>();
	map_end = new Dictionary<string, Node>();

	Node n1 = new Node()
	{
		Name = Serial(initState),
		Info = initState,
		Nivel = 0
	};

	Node n2 = new Node()
	{
		Name = Serial(target),
		Info = target,
		Nivel = 0
	};

	q_begin.Enqueue(Heuristic(initState, target), n1);
	q_end.Enqueue(Heuristic(target, initState), n2);

	map_begin.Add(n1.Name, n1);
	map_end.Add(n2.Name, n2);

	while (q_begin.Count>0 && q_end.Count>0)
	{
		sucessors_begin = GetSucessors(q_begin.Dequeue());
		sucessors_end = GetSucessors(q_end.Dequeue());

		foreach (Node node in sucessors_begin)
		{
			if (!map_begin.ContainsKey(node.Name))
			{
				q_begin.Enqueue(Heuristic((int[])node.Info, target), node);
				map_begin.Add(node.Name, node);
			}
			if (map_end.ContainsKey(node.Name))
			{
				return BuildAnswer(node);
			}
		}
		foreach (Node node in sucessors_end)
		{
			if (!map_end.ContainsKey(node.Name))
			{
				q_end.Enqueue(Heuristic((int[])node.Info, initState), node);
				map_end.Add(node.Name, node);
			}
			if (map_begin.ContainsKey(node.Name))
			{
				return BuildAnswer(node);
			}
		}
	}
	return null;
}
int Heuristic(int[] valor, int[] target)
{
	int h = 0, m = 0, c = 0;
	int tam = (int)Math.Pow(valor.Length, 0.5);

	for (int i = 0; i < valor.Length; i++)
	{
		h += HammingDistance(valor, target, i);
		m += ManhattanDistance(valor, target, i, tam);
		c += LinearConflict(valor, i, tam);
	}
	return 36 * h + 18 * m + 2 * c;
}
int HammingDistance(int[] valor, int[] target, int index)
{
	if (valor[index] != target[index])
		return 1;
	return 0;
}
int ManhattanDistance(int[] valor, int[] target, int index, int tam)
{
	int x0 = target[index] / tam;
	int x1 = valor[index] / tam;
	int y0 = target[index] % tam;
	int y1 = valor[index] % tam;
	return (Math.Abs(x0 - x1) + Math.Abs(y0 - y1));
}
int LinearConflict(int[] valor, int index, int tam)
{
	if (index % tam > 0 && valor[index] + 1 == valor[index - 1])
		return 2;
	if (index % tam < (tam - 1) && valor[index] - 1 == valor[index + 1])
		return 2;
	if ((index >= tam) && valor[index] + tam == valor[index - tam])
		return 2;
	if ((index < valor.Length - tam) && valor[index] - tam == valor[index + tam])
		return 2;
	return 1;
}
string Serial(int[] valor)
{
	return string.Join(",", valor.Select(x => x.ToString()).ToArray());
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
	Node node = new Node()
	{
		Name = Serial(value),
		Info = value,
		Nivel = value[index],
		Parent = parent
	};
	return node;
}
int[] BuildAnswer(Node node)
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