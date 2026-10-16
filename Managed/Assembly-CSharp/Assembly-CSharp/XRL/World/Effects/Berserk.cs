using System;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C39 RID: 3129
	[Serializable]
	public class Berserk : Effect, ITierInitialized
	{
		// Token: 0x06007F85 RID: 32645 RVA: 0x002F0B01 File Offset: 0x002EED01
		public Berserk()
		{
			this.DisplayName = "berserk";
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x002F0B14 File Offset: 0x002EED14
		public Berserk(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06007F87 RID: 32647 RVA: 0x002F0B23 File Offset: 0x002EED23
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(5, 15);
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x002F0B33 File Offset: 0x002EED33
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007F89 RID: 32649 RVA: 0x002F0B36 File Offset: 0x002EED36
		public override int GetEffectType()
		{
			return 67108866;
		}

		// Token: 0x06007F8A RID: 32650 RVA: 0x002F0B3D File Offset: 0x002EED3D
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x002F0B40 File Offset: 0x002EED40
		public override string GetDetails()
		{
			return "100% chance to dismember with axes.";
		}

		// Token: 0x06007F8C RID: 32652 RVA: 0x002F0B48 File Offset: 0x002EED48
		public override bool Apply(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_enraged", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			Berserk berserk;
			if (Object.TryGetEffect<Berserk>(out berserk))
			{
				if (this.Duration > berserk.Duration)
				{
					berserk.Duration = this.Duration;
				}
				return false;
			}
			return true;
		}

		// Token: 0x06007F8D RID: 32653 RVA: 0x002F0BA9 File Offset: 0x002EEDA9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06007F8E RID: 32654 RVA: 0x002F0BC0 File Offset: 0x002EEDC0
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0 && this.Duration != 9999 && base.Object.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage(this.Duration.Things("turn remains", "turns remain") + " until your berserker rage ends.", null, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F8F RID: 32655 RVA: 0x002F0C1D File Offset: 0x002EEE1D
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				E.RenderEffectIndicator("!", "Abilities/abil_berserk.bmp", "&R", "R", 45, 55);
			}
			return base.Render(E);
		}
	}
}
