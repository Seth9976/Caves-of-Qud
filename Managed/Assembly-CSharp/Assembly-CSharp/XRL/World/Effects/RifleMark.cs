using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D79 RID: 3449
	[Serializable]
	public class RifleMark : Effect
	{
		// Token: 0x060088D9 RID: 35033 RVA: 0x00310599 File Offset: 0x0030E799
		public RifleMark()
		{
			this.DisplayName = "{{R|marked}}";
			this.Duration = 1;
		}

		// Token: 0x060088DA RID: 35034 RVA: 0x003105B3 File Offset: 0x0030E7B3
		public RifleMark(GameObject Marker)
			: this()
		{
			this.Marker = Marker;
		}

		// Token: 0x060088DB RID: 35035 RVA: 0x003105C2 File Offset: 0x0030E7C2
		public override int GetEffectType()
		{
			return 33554433;
		}

		// Token: 0x060088DC RID: 35036 RVA: 0x003105C9 File Offset: 0x0030E7C9
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060088DD RID: 35037 RVA: 0x003105CC File Offset: 0x0030E7CC
		public override string GetDescription()
		{
			if (GameObject.Validate(ref this.Marker) && this.Marker.IsVisible())
			{
				return "{{R|marked by " + this.Marker.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "}}";
			}
			return null;
		}

		// Token: 0x060088DE RID: 35038 RVA: 0x00310630 File Offset: 0x0030E830
		public override string GetDetails()
		{
			if (GameObject.Validate(ref this.Marker))
			{
				return "Easier to hit with bows and rifles wielded by " + this.Marker.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".";
			}
			return "Easier to hit with bows and rifles.";
		}

		// Token: 0x060088DF RID: 35039 RVA: 0x00310688 File Offset: 0x0030E888
		public override bool Apply(GameObject Object)
		{
			return ApplyEffectEvent.Check(Object, "RifleMark", this, null);
		}

		// Token: 0x060088E0 RID: 35040 RVA: 0x0031069C File Offset: 0x0030E89C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x060088E1 RID: 35041 RVA: 0x003106B4 File Offset: 0x0030E8B4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!GameObject.Validate(ref this.Marker) || !base.Object.InActiveZone() || !this.Marker.HasLOSTo(base.Object, true, false, true, true, null))
			{
				this.Duration = 0;
				base.Object.CleanEffects();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060088E2 RID: 35042 RVA: 0x00310710 File Offset: 0x0030E910
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && GameObject.Validate(ref this.Marker) && this.Marker.IsVisible() && base.Object != null && !base.Object.IsPlayer())
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 35 && num < 40)
				{
					E.Tile = null;
					E.RenderString = "Î";
					E.ColorString = "^R&k";
				}
			}
			return true;
		}

		// Token: 0x04003A62 RID: 14946
		public GameObject Marker;
	}
}
