using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D89 RID: 3465
	[Serializable]
	public class ShieldWall : Effect, ITierInitialized
	{
		// Token: 0x060089DE RID: 35294 RVA: 0x00313C0F File Offset: 0x00311E0F
		public ShieldWall()
		{
			this.DisplayName = "shield wall";
		}

		// Token: 0x060089DF RID: 35295 RVA: 0x00313C22 File Offset: 0x00311E22
		public ShieldWall(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060089E0 RID: 35296 RVA: 0x00313C31 File Offset: 0x00311E31
		public ShieldWall(int Duration, GameObject Shield)
			: this(Duration)
		{
			this.Shield = Shield;
		}

		// Token: 0x060089E1 RID: 35297 RVA: 0x00313C41 File Offset: 0x00311E41
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Roll(5, 15);
		}

		// Token: 0x060089E2 RID: 35298 RVA: 0x00313C51 File Offset: 0x00311E51
		public override int GetEffectType()
		{
			return 67108992;
		}

		// Token: 0x060089E3 RID: 35299 RVA: 0x00313C58 File Offset: 0x00311E58
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060089E4 RID: 35300 RVA: 0x00313C5B File Offset: 0x00311E5B
		public override string GetDescription()
		{
			return "{{g|shield wall}}";
		}

		// Token: 0x060089E5 RID: 35301 RVA: 0x00313C62 File Offset: 0x00311E62
		public override string GetDetails()
		{
			return "Blocks all incoming melee attacks.";
		}

		// Token: 0x060089E6 RID: 35302 RVA: 0x00313C6C File Offset: 0x00311E6C
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<ShieldWall>())
			{
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyShieldWall", "Effect", this)))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_defensiveBuff", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (this.Shield != null)
			{
				base.DidXToY("raise", this.Shield, "in wall formation", "!", null, null, Object, null, false, false, false, false, false, null, Object, null, false, false, false, false, false, null);
			}
			else
			{
				base.DidX("raise", Object.its + " shield in wall formation", "!", null, null, Object, null, false, false, null, null, false, false, false, false, false, null);
			}
			return true;
		}

		// Token: 0x060089E7 RID: 35303 RVA: 0x00313D34 File Offset: 0x00311F34
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 21 && num < 31)
			{
				E.Tile = null;
				E.RenderString = "\u0004";
				E.ApplyColors("&B", Effect.ICON_COLOR_PRIORITY);
			}
			return true;
		}

		// Token: 0x060089E8 RID: 35304 RVA: 0x00313D77 File Offset: 0x00311F77
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x060089E9 RID: 35305 RVA: 0x00313D8D File Offset: 0x00311F8D
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (base.Object.GetShield(null, null) == null)
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003A87 RID: 14983
		[NonSerialized]
		private GameObject Shield;
	}
}
