using System;

namespace XRL
{
	// Token: 0x0200050A RID: 1290
	[Serializable]
	public class SubtypeSaveModifier
	{
		// Token: 0x06003752 RID: 14162 RVA: 0x0010C4F1 File Offset: 0x0010A6F1
		public void MergeWith(SubtypeSaveModifier newSaveModifier)
		{
			if (newSaveModifier.Amount != -999)
			{
				this.Amount = newSaveModifier.Amount;
			}
		}

		// Token: 0x04001BA8 RID: 7080
		public string Vs = "";

		// Token: 0x04001BA9 RID: 7081
		public int Amount;
	}
}
