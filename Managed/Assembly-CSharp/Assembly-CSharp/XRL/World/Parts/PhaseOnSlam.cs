using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02000EEE RID: 3822
	[Serializable]
	public class PhaseOnSlam : IActivePart
	{
		// Token: 0x060098C3 RID: 39107 RVA: 0x003776C6 File Offset: 0x003758C6
		public PhaseOnSlam()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x060098C4 RID: 39108 RVA: 0x003776D5 File Offset: 0x003758D5
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060098C5 RID: 39109 RVA: 0x003776D8 File Offset: 0x003758D8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<SlamEvent>.ID;
		}

		// Token: 0x060098C6 RID: 39110 RVA: 0x003776F0 File Offset: 0x003758F0
		public override bool HandleEvent(SlamEvent E)
		{
			if (E.Target != null && this.IsObjectActivePartSubject(E.Actor))
			{
				if (E.Target.HasEffect<Phased>())
				{
					if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						E.Target.ApplyEffect(new Omniphase(E.SlamPower), null);
					}
				}
				else if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					E.Target.ApplyEffect(new Phased(E.SlamPower), null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060098C7 RID: 39111 RVA: 0x003777AF File Offset: 0x003759AF
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
