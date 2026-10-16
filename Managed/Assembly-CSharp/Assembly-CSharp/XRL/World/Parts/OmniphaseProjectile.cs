using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001113 RID: 4371
	[Serializable]
	public class OmniphaseProjectile : IPart
	{
		// Token: 0x0600AD27 RID: 44327 RVA: 0x003F8AAA File Offset: 0x003F6CAA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600AD28 RID: 44328 RVA: 0x003F8AC0 File Offset: 0x003F6CC0
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.ForceApplyEffect(new Omniphase(base.Name), null);
			return base.HandleEvent(E);
		}
	}
}
