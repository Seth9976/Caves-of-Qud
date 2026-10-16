using System;

namespace XRL.World.Parts
{
	// Token: 0x02001197 RID: 4503
	public class VisCell
	{
		// Token: 0x0600B313 RID: 45843 RVA: 0x00415550 File Offset: 0x00413750
		public VisCell(Cell _C, int _Turns)
		{
			this.C = _C;
			this.Turns = _Turns;
		}

		// Token: 0x0600B314 RID: 45844 RVA: 0x00415568 File Offset: 0x00413768
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.C.X.ToString(),
				".",
				this.C.Y.ToString(),
				".",
				this.C.ParentZone.ZoneID
			});
		}

		// Token: 0x04004844 RID: 18500
		public Cell C;

		// Token: 0x04004845 RID: 18501
		public int Turns;
	}
}
