using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using XRL.Collections;

namespace Genkit
{
	// Token: 0x02000399 RID: 921
	public class WeightedDigraph
	{
		// Token: 0x060022B7 RID: 8887 RVA: 0x000B07DC File Offset: 0x000AE9DC
		public WeightedDigraph(int Length)
		{
			this.Vertices = new WeightedDigraph.Vertex[Length];
			this.Length = Length;
			for (int i = 0; i < Length; i++)
			{
				this.Vertices[i] = new WeightedDigraph.Vertex();
			}
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000B081C File Offset: 0x000AEA1C
		public void AddArc(int From, int To, uint Weight)
		{
			this.Vertices[To].Arcs.Add(new WeightedDigraph.Arc(From, Weight));
			this.Vertices[From].OutgoingWeight += (ulong)Weight;
			this.Vertices[To].IncomingWeight += (ulong)Weight;
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x000B086E File Offset: 0x000AEA6E
		public int GetTarget(int Index)
		{
			return this.Vertices[Index].Target;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000B0880 File Offset: 0x000AEA80
		public void Resolve()
		{
			int i = 0;
			int num = 0;
			while (i < this.Length)
			{
				ulong num2 = ulong.MaxValue;
				ulong num3 = 0UL;
				for (int j = 0; j < this.Length; j++)
				{
					WeightedDigraph.Vertex vertex = this.Vertices[j];
					if (vertex.Target == -1)
					{
						ulong outgoingWeight = vertex.OutgoingWeight;
						if (outgoingWeight == 0UL)
						{
							num = j;
							break;
						}
						ulong incomingWeight = vertex.IncomingWeight;
						if (num3 == 0UL || (incomingWeight > 0UL && (outgoingWeight < num2 || (outgoingWeight == num2 && incomingWeight > num3))))
						{
							num = j;
							num2 = outgoingWeight;
							num3 = incomingWeight;
						}
					}
				}
				this.Vertices[num].Target = i;
				foreach (WeightedDigraph.Arc arc in this.Vertices[num].Arcs)
				{
					this.Vertices[arc.Index].OutgoingWeight -= (ulong)arc.Weight;
				}
				i++;
			}
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x000B098C File Offset: 0x000AEB8C
		public IEnumerable<RingDeque<int>> YieldCycles(uint MinWeight)
		{
			int num;
			for (int v = 0; v < this.Length; v = num + 1)
			{
				RingDeque<int> ringDeque = null;
				try
				{
					WeightedDigraph.<YieldCycles>g__Visit|8_0(v, this.Vertices, MinWeight);
				}
				catch (WeightedDigraph.CircularGraphException ex)
				{
					ringDeque = ex.Stack;
				}
				if (ringDeque != null)
				{
					yield return ringDeque;
				}
				num = v;
			}
			yield break;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000B09A4 File Offset: 0x000AEBA4
		[CompilerGenerated]
		internal static void <YieldCycles>g__Visit|8_0(int Index, WeightedDigraph.Vertex[] Vertices, uint MinWeight)
		{
			WeightedDigraph.Vertex vertex = Vertices[Index];
			int target = vertex.Target;
			if (target == -2147483648)
			{
				throw new WeightedDigraph.CircularGraphException(Index);
			}
			vertex.Target = int.MinValue;
			try
			{
				foreach (WeightedDigraph.Arc arc in vertex.Arcs)
				{
					if (arc.Weight >= MinWeight)
					{
						try
						{
							WeightedDigraph.<YieldCycles>g__Visit|8_0(arc.Index, Vertices, MinWeight);
						}
						catch (WeightedDigraph.CircularGraphException ex)
						{
							ex.Stack.Enqueue(Index);
							throw;
						}
					}
				}
			}
			finally
			{
				vertex.Target = target;
			}
		}

		// Token: 0x04001182 RID: 4482
		private WeightedDigraph.Vertex[] Vertices;

		// Token: 0x04001183 RID: 4483
		private int Length;

		// Token: 0x02001788 RID: 6024
		private class Vertex
		{
			// Token: 0x04005EE0 RID: 24288
			public int Target = -1;

			// Token: 0x04005EE1 RID: 24289
			public Rack<WeightedDigraph.Arc> Arcs = new Rack<WeightedDigraph.Arc>();

			// Token: 0x04005EE2 RID: 24290
			public ulong OutgoingWeight;

			// Token: 0x04005EE3 RID: 24291
			public ulong IncomingWeight;
		}

		// Token: 0x02001789 RID: 6025
		private struct Arc
		{
			// Token: 0x0600DDE4 RID: 56804 RVA: 0x0050DD23 File Offset: 0x0050BF23
			public Arc(int Index, uint Weight)
			{
				this.Index = Index;
				this.Weight = Weight;
			}

			// Token: 0x04005EE4 RID: 24292
			public int Index;

			// Token: 0x04005EE5 RID: 24293
			public uint Weight;
		}

		// Token: 0x0200178A RID: 6026
		private class CircularGraphException : Exception
		{
			// Token: 0x0600DDE5 RID: 56805 RVA: 0x0050DD33 File Offset: 0x0050BF33
			public CircularGraphException(int Index)
			{
				this.Stack.Enqueue(Index);
			}

			// Token: 0x04005EE6 RID: 24294
			public RingDeque<int> Stack = new RingDeque<int>();
		}
	}
}
