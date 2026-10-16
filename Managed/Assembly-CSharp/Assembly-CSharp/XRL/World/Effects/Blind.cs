using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C3C RID: 3132
	[Serializable]
	public class Blind : Effect, ITierInitialized
	{
		// Token: 0x06007FC8 RID: 32712 RVA: 0x002F1B81 File Offset: 0x002EFD81
		public Blind()
		{
			this.DisplayName = "{{K|blind}}";
		}

		// Token: 0x06007FC9 RID: 32713 RVA: 0x002F1B94 File Offset: 0x002EFD94
		public Blind(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06007FCA RID: 32714 RVA: 0x002F1BA3 File Offset: 0x002EFDA3
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(20, 30);
		}

		// Token: 0x06007FCB RID: 32715 RVA: 0x002F1BB4 File Offset: 0x002EFDB4
		public override string GetDetails()
		{
			return "Can't see.";
		}

		// Token: 0x06007FCC RID: 32716 RVA: 0x002F1BBB File Offset: 0x002EFDBB
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06007FCD RID: 32717 RVA: 0x002F1BBE File Offset: 0x002EFDBE
		public override int GetEffectType()
		{
			return 117448704;
		}

		// Token: 0x06007FCE RID: 32718 RVA: 0x002F1BC8 File Offset: 0x002EFDC8
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_bewilderment", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			return Object.FireEvent(Event.New("ApplyBlind", "Duration", this.Duration));
		}

		// Token: 0x06007FCF RID: 32719 RVA: 0x002F1C1D File Offset: 0x002EFE1D
		public override void Remove(GameObject Object)
		{
		}

		// Token: 0x06007FD0 RID: 32720 RVA: 0x002F1C1F File Offset: 0x002EFE1F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x06007FD1 RID: 32721 RVA: 0x002F1C38 File Offset: 0x002EFE38
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.Duration > 0 && base.Object.IsPlayer())
			{
				Cell currentCell = base.Object.CurrentCell;
				if (currentCell != null)
				{
					currentCell.ParentZone.ClearLightMap();
				}
				return false;
			}
			return base.HandleEvent(E);
		}
	}
}
