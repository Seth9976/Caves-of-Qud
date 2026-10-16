using System;

namespace XRL
{
	// Token: 0x02000504 RID: 1284
	[Serializable]
	public class GenotypeSaveModifier
	{
		// Token: 0x0600372D RID: 14125 RVA: 0x0010B164 File Offset: 0x00109364
		public void MergeWith(GenotypeSaveModifier newSaveModifier)
		{
			if (newSaveModifier.Amount != -999)
			{
				this.Amount = newSaveModifier.Amount;
			}
		}

		// Token: 0x04001B7B RID: 7035
		public string Vs = "";

		// Token: 0x04001B7C RID: 7036
		public int Amount;
	}
}
