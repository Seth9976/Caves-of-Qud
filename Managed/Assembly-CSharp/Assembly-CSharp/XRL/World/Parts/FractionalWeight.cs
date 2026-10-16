using System;

namespace XRL.World.Parts
{
	// Token: 0x02001031 RID: 4145
	[Serializable]
	public class FractionalWeight : IPart
	{
		// Token: 0x0600A521 RID: 42273 RVA: 0x003C614A File Offset: 0x003C434A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetIntrinsicWeightEvent.ID;
		}

		// Token: 0x0600A522 RID: 42274 RVA: 0x003C6160 File Offset: 0x003C4360
		public override bool HandleEvent(GetIntrinsicWeightEvent E)
		{
			E.Weight += this.Weight;
			return base.HandleEvent(E);
		}

		// Token: 0x0600A523 RID: 42275 RVA: 0x003C617C File Offset: 0x003C437C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0400444D RID: 17485
		public double Weight;
	}
}
