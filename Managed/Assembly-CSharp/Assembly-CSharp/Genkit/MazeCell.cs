using System;

namespace Genkit
{
	// Token: 0x02000380 RID: 896
	[Serializable]
	public class MazeCell
	{
		// Token: 0x060021BD RID: 8637 RVA: 0x000A809F File Offset: 0x000A629F
		public MazeCell()
		{
			this.N = false;
			this.S = false;
			this.E = false;
			this.W = false;
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x000A80C3 File Offset: 0x000A62C3
		public MazeCell(bool Init)
		{
			this.N = Init;
			this.S = Init;
			this.E = Init;
			this.W = Init;
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x000A80E7 File Offset: 0x000A62E7
		public bool AnyOpen()
		{
			return this.N || this.S || this.E || this.W;
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000A8109 File Offset: 0x000A6309
		public bool DeadEnd()
		{
			return this.NorthOnly() || this.SouthOnly() || this.EastOnly() || this.WestOnly();
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000A812B File Offset: 0x000A632B
		public bool NorthOnly()
		{
			return this.N && !this.S && !this.E && !this.W;
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000A8150 File Offset: 0x000A6350
		public bool SouthOnly()
		{
			return !this.N && this.S && !this.E && !this.W;
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x000A8175 File Offset: 0x000A6375
		public bool EastOnly()
		{
			return !this.N && !this.S && this.E && !this.W;
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x000A819A File Offset: 0x000A639A
		public bool WestOnly()
		{
			return !this.N && !this.S && !this.E && this.W;
		}

		// Token: 0x040010EC RID: 4332
		public bool N;

		// Token: 0x040010ED RID: 4333
		public bool S;

		// Token: 0x040010EE RID: 4334
		public bool E;

		// Token: 0x040010EF RID: 4335
		public bool W;

		// Token: 0x040010F0 RID: 4336
		public int x;

		// Token: 0x040010F1 RID: 4337
		public int y;
	}
}
