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