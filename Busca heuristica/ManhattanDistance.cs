int ManhattanDistance(int[] valor, int i, int tam)
{
	int x0 = i / tam;
	int x1 = valor[i] / tam;
	int y0 = i % tam;
	int y1 = valor[i] % tam;
	return (Math.Abs(x0 - x1) + Math.Abs(y0 - y1));
}