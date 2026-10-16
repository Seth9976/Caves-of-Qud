using System;

namespace XRL.World.Anatomy
{
	// Token: 0x02000BC5 RID: 3013
	[Serializable]
	public class BodyPartPositionSpec
	{
		// Token: 0x0600790B RID: 30987 RVA: 0x002DB8E5 File Offset: 0x002D9AE5
		public BodyPartPositionSpec()
		{
		}

		// Token: 0x0600790C RID: 30988 RVA: 0x002DB8ED File Offset: 0x002D9AED
		public BodyPartPositionSpec(BodyPart Parent, int Position, int Score)
			: this()
		{
			this.Parent = Parent;
			this.Position = Position;
			this.Score = Score;
		}

		// Token: 0x04003742 RID: 14146
		public BodyPart Parent;

		// Token: 0x04003743 RID: 14147
		public int Position;

		// Token: 0x04003744 RID: 14148
		public int Score;
	}
}
