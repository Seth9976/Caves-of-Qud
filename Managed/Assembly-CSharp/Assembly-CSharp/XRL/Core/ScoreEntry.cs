using System;

namespace XRL.Core
{
	// Token: 0x02000636 RID: 1590
	[Obsolete]
	[Serializable]
	public class ScoreEntry
	{
		// Token: 0x0600428A RID: 17034 RVA: 0x00160073 File Offset: 0x0015E273
		public ScoreEntry(int _Score, string _Description, string _Details)
		{
			this.Score = _Score;
			this.Description = _Description;
			this.Details = _Details;
		}

		// Token: 0x040020DD RID: 8413
		public int Score;

		// Token: 0x040020DE RID: 8414
		public string Description;

		// Token: 0x040020DF RID: 8415
		public string Details;
	}
}
