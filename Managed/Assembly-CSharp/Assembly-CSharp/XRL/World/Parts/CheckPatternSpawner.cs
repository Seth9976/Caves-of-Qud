using System;

namespace XRL.World.Parts
{
	// Token: 0x020012F9 RID: 4857
	[Serializable]
	public class CheckPatternSpawner : IPart
	{
		// Token: 0x0600BE7F RID: 48767 RVA: 0x0045C1F9 File Offset: 0x0045A3F9
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BE80 RID: 48768 RVA: 0x0045C1FC File Offset: 0x0045A3FC
		public override bool WantEvent(int ID, int cascade)
		{
			return ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BE81 RID: 48769 RVA: 0x0045C208 File Offset: 0x0045A408
		public override bool HandleEvent(EnteredCellEvent E)
		{
			string whiteVariant = this.WhiteVariant;
			if ((E.Cell.X + E.Cell.Y) % 2 == 0)
			{
				E.Cell.AddObject(this.ObjectBaseName + " " + this.BlackVariant, null, null, null, null);
			}
			else
			{
				E.Cell.AddObject(this.ObjectBaseName + " " + this.WhiteVariant, null, null, null, null);
			}
			this.ParentObject.Destroy(null, false, false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x04004CB4 RID: 19636
		public string ObjectBaseName = "Coral Polyp";

		// Token: 0x04004CB5 RID: 19637
		public string WhiteVariant = "A";

		// Token: 0x04004CB6 RID: 19638
		public string BlackVariant = "B";
	}
}
