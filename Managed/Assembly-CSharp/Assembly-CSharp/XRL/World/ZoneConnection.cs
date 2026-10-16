using System;
using Genkit;

namespace XRL.World
{
	// Token: 0x02000994 RID: 2452
	[Serializable]
	public class ZoneConnection
	{
		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x060068FA RID: 26874 RVA: 0x0020F0F5 File Offset: 0x0020D2F5
		public Location2D Loc2D
		{
			get
			{
				return Location2D.Get(this.X, this.Y);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x060068FB RID: 26875 RVA: 0x0020F108 File Offset: 0x0020D308
		public Point2D Pos2D
		{
			get
			{
				return new Point2D(this.X, this.Y);
			}
		}

		// Token: 0x060068FC RID: 26876 RVA: 0x0020F11B File Offset: 0x0020D31B
		public override string ToString()
		{
			if (this.Object == null)
			{
				return this.Type + " <no object>";
			}
			return this.Type + " " + this.Object;
		}

		// Token: 0x0400304E RID: 12366
		public string Type;

		// Token: 0x0400304F RID: 12367
		public int X;

		// Token: 0x04003050 RID: 12368
		public int Y;

		// Token: 0x04003051 RID: 12369
		public string Object;
	}
}
