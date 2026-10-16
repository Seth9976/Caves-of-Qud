using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000F25 RID: 3877
	[Serializable]
	public class CyberneticsEffectSuppressor : IActivePart
	{
		// Token: 0x06009A9B RID: 39579 RVA: 0x00381638 File Offset: 0x0037F838
		public CyberneticsEffectSuppressor()
		{
			this.WorksOnImplantee = true;
		}

		// Token: 0x06009A9C RID: 39580 RVA: 0x0038164F File Offset: 0x0037F84F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A9D RID: 39581 RVA: 0x00381652 File Offset: 0x0037F852
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID || ID == CanApplyEffectEvent.ID || ID == ImplantedEvent.ID;
		}

		// Token: 0x06009A9E RID: 39582 RVA: 0x00381678 File Offset: 0x0037F878
		public override bool HandleEvent(ImplantedEvent E)
		{
			if (E.Implantee.Effects.Count > 0)
			{
				List<Effect> list = null;
				List<string> list2 = this.Effects.CachedCommaExpansion();
				foreach (Effect effect in E.Implantee.Effects)
				{
					if (list2.Contains(effect.ClassName))
					{
						if (list == null)
						{
							list = new List<Effect>();
						}
						list.Add(effect);
					}
				}
				if (list != null)
				{
					foreach (Effect effect2 in list)
					{
						E.Implantee.RemoveEffect(effect2, true);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A9F RID: 39583 RVA: 0x0038175C File Offset: 0x0037F95C
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return (this.Chance < 100 || !this.Effects.CachedCommaExpansion().Contains(E.Name) || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x06009AA0 RID: 39584 RVA: 0x003817BC File Offset: 0x0037F9BC
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return (!this.Effects.CachedCommaExpansion().Contains(E.Name) || !this.Chance.in100() || !base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0400404A RID: 16458
		public int Chance = 100;

		// Token: 0x0400404B RID: 16459
		public string Effects;
	}
}
