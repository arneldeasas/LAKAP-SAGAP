using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAKAPSAGAP.Services.Core.Helpers
{
	public class PartialTextSearch
	{
		public static int CalculateLevenshteinDistance(string source, string target)
		{
			if (source == target) return 0;
			if (source.Length == 0) return target.Length;
			if (target.Length == 0) return source.Length;

			var distance = new int[source.Length + 1, target.Length + 1];

			for (int i = 0; i <= source.Length; distance[i, 0] = i++) { }
			for (int j = 0; j <= target.Length; distance[0, j] = j++) { }

			for (int i = 1; i <= source.Length; i++)
			{
				for (int j = 1; j <= target.Length; j++)
				{
					int cost = (target[j - 1] == source[i - 1]) ? 0 : 1;
					distance[i, j] = Math.Min(
						Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
						distance[i - 1, j - 1] + cost);
				}
			}

			return distance[source.Length, target.Length];
		}

	}
}
