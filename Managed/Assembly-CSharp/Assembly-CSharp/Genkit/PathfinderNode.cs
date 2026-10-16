using System;
using System.Collections.Generic;

namespace Genkit
{
	// Token: 0x0200038A RID: 906
	public class PathfinderNode : IDisposable
	{
		// Token: 0x0600220B RID: 8715 RVA: 0x000AA431 File Offset: 0x000A8631
		public void clear()
		{
			this.weight = 0;
			this.pos = Location2D.Invalid;
			this.adjacentNodes.Clear();
			this.nodesByDirection.Clear();
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000AA45B File Offset: 0x000A865B
		public override int GetHashCode()
		{
			return this.pos.GetHashCode();
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x0600220D RID: 8717 RVA: 0x000AA468 File Offset: 0x000A8668
		public int X
		{
			get
			{
				return this.pos.X;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000AA475 File Offset: 0x000A8675
		public int Y
		{
			get
			{
				return this.pos.Y;
			}
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000AA482 File Offset: 0x000A8682
		public int SquareDistance(PathfinderNode target)
		{
			return this.pos.SquareDistance(target.pos);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000AA498 File Offset: 0x000A8698
		public PathfinderNode GetNodeFromDirection(string dir)
		{
			PathfinderNode pathfinderNode;
			if (this.nodesByDirection.TryGetValue(dir, out pathfinderNode))
			{
				return pathfinderNode;
			}
			return null;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000AA4B8 File Offset: 0x000A86B8
		public static PathfinderNode fromPool()
		{
			if (PathfinderNode.nodePool.Count > 0)
			{
				return PathfinderNode.nodePool.Dequeue();
			}
			return new PathfinderNode();
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000AA4D7 File Offset: 0x000A86D7
		public void Dispose()
		{
			this.clear();
			PathfinderNode.nodePool.Enqueue(this);
		}

		// Token: 0x0400110A RID: 4362
		public int weight;

		// Token: 0x0400110B RID: 4363
		public Location2D pos;

		// Token: 0x0400110C RID: 4364
		public List<PathfinderNode> adjacentNodes = new List<PathfinderNode>();

		// Token: 0x0400110D RID: 4365
		public Dictionary<string, PathfinderNode> nodesByDirection = new Dictionary<string, PathfinderNode>();

		// Token: 0x0400110E RID: 4366
		private static Queue<PathfinderNode> nodePool = new Queue<PathfinderNode>();
	}
}
