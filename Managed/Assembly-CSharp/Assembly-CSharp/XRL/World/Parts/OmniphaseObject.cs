using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020011FC RID: 4604
	[Serializable]
	public class OmniphaseObject : IPart
	{
		// Token: 0x0600B725 RID: 46885 RVA: 0x0042B3F3 File Offset: 0x004295F3
		public override bool CanGenerateStacked()
		{
			return false;
		}

		// Token: 0x0600B726 RID: 46886 RVA: 0x0042B3F6 File Offset: 0x004295F6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600B727 RID: 46887 RVA: 0x0042B40C File Offset: 0x0042960C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			E.Object.ForceApplyEffect(new Omniphase(9999, base.Name, this.Visual), null);
			E.Object.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x040049C2 RID: 18882
		public bool Visual = true;
	}
}
