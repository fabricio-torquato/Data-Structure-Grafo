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