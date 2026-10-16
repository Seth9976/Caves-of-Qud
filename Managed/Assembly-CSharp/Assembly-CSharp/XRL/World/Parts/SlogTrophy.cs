using System;

namespace XRL.World.Parts
{
	// Token: 0x020010B9 RID: 4281
	[Serializable]
	public class SlogTrophy : IPart
	{
		// Token: 0x0600AA90 RID: 43664 RVA: 0x003E65A9 File Offset: 0x003E47A9
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600AA91 RID: 43665 RVA: 0x003E65AC File Offset: 0x003E47AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600AA92 RID: 43666 RVA: 0x003E65CA File Offset: 0x003E47CA
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterEvent(this, BeforeApplyDamageEvent.ID, 0, true);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA93 RID: 43667 RVA: 0x003E65E6 File Offset: 0x003E47E6
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterEvent(this, BeforeApplyDamageEvent.ID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA94 RID: 43668 RVA: 0x003E6600 File Offset: 0x003E4800
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Damage.HasAttribute("Poison"))
			{
				E.Damage.Amount /= 2;
				if (E.Damage.Amount <= 0)
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}
	}
}
