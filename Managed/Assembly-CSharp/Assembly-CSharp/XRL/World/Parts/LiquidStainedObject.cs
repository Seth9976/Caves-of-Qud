using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020011F5 RID: 4597
	[Serializable]
	public class LiquidStainedObject : IPart
	{
		// Token: 0x0600B705 RID: 46853 RVA: 0x0042B0DB File Offset: 0x004292DB
		public override bool CanGenerateStacked()
		{
			return false;
		}

		// Token: 0x0600B706 RID: 46854 RVA: 0x0042B0DE File Offset: 0x004292DE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B707 RID: 46855 RVA: 0x0042B0F4 File Offset: 0x004292F4
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			E.Object.ForceApplyEffect(new LiquidStained(new LiquidVolume(this.Liquid, this.Volume.RollCached())), null);
			E.Object.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x040049BD RID: 18877
		public string Liquid = "blood";

		// Token: 0x040049BE RID: 18878
		public string Volume = "3d4";
	}
}
