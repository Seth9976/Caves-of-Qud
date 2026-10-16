using System;
using UnityEngine;

namespace LaundryBear.Math
{
	// Token: 0x02000008 RID: 8
	public class Graph
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002530 File Offset: 0x00000730
		static Graph()
		{
			Graph.channel[0] = new Channel(Color.red);
			Graph.channel[1] = new Channel(Color.green);
			Graph.channel[2] = new Channel(Color.blue);
		}

		// Token: 0x04000009 RID: 9
		public static float YMin = -1f;

		// Token: 0x0400000A RID: 10
		public static float YMax = 1f;

		// Token: 0x0400000B RID: 11
		public const int MAX_HISTORY = 1024;

		// Token: 0x0400000C RID: 12
		public const int MAX_CHANNELS = 3;

		// Token: 0x0400000D RID: 13
		public static Channel[] channel = new Channel[3];
	}
}
