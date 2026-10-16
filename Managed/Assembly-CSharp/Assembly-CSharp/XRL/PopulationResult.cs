using System;

namespace XRL
{
	// Token: 0x02000539 RID: 1337
	public class PopulationResult
	{
		// Token: 0x06003966 RID: 14694 RVA: 0x0011AE50 File Offset: 0x00119050
		public PopulationResult(string Blueprint, int Number = 1, string Hint = null, string Builder = null)
		{
			this.Blueprint = Blueprint;
			this.Number = Number;
			this.Hint = Hint;
			this.Builder = Builder;
		}

		// Token: 0x04001CE8 RID: 7400
		public string Blueprint;

		// Token: 0x04001CE9 RID: 7401
		public string Hint;

		// Token: 0x04001CEA RID: 7402
		public string Builder;

		// Token: 0x04001CEB RID: 7403
		public int Number;
	}
}
