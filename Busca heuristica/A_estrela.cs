int[] FindSolution()
{
	PriorityQueue<int, Node> q = new PriorityQueue<int, Node>();
	Dictionary<string, Node> map = new Dictionary<string, Node>();
	Node n = new Node();

	n.Name = Serial(initState);
	n.Info = initState;
	n.Nivel = 0;
	n.Profundidade = 0;
	q.Enqueue(Heuristic(initState), n);
	map.Add(n.Name, n);

	while (q.Count > 0)
	{
		n = q.Dequeue();

	 //   if (TargetFound(n))
	   //     return BuildAnswer(n);

		List<Node> sucessors = GetSucessors(n);

		foreach (Node node in sucessors)
		{
			if (!map.ContainsKey(node.Name))
			{
				int key = Heuristic((int[])node.Info);
				q.Enqueue(key+node.Profundidade*4, node);
				map.Add(node.Name, node);
				if(key==0)
					return BuildAnswer(node);
			}
		}
	}
	return null;
}
int Heuristic(int[] valor)
{
	int h = 0, m = 0, c = 0;
	int tam = (int)Math.Pow(valor.Length, 0.5);

	for (int i = 0; i < valor.Length; i++)
	{
		h += HammingDistance(valor, i);
		m += ManhattanDistance(valor, i, tam);
		c += LinearConflict(valor, i, tam);
	}
	 return 36 * h + 18 * m + 2 * c;
}
int HammingDistance(int[] valor, int index)
{
	if (valor[index] != target[index])
		return 1;
	return 0;
}
int ManhattanDistance(int[] valor, int i, int tam)
{
	int x0 = i / tam;
	int x1 = valor[i] / tam;
	int y0 = i % tam;
	int y1 = valor[i] % tam;
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
	return 0;
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
