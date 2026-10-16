using System;
using XRL.Wish;

namespace XRL.World.Effects
{
	// Token: 0x02000D3E RID: 3390
	[HasWishCommand]
	[Serializable]
	public class FungalVisionary : Effect, ITierInitialized
	{
		// Token: 0x060085CA RID: 34250 RVA: 0x00303ABE File Offset: 0x00301CBE
		public FungalVisionary()
		{
			this.DisplayName = "{{O|shimmering}}";
			this.Duration = 1;
		}

		// Token: 0x060085CB RID: 34251 RVA: 0x00303AD8 File Offset: 0x00301CD8
		public FungalVisionary(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060085CC RID: 34252 RVA: 0x00303AE7 File Offset: 0x00301CE7
		public void Initialize(int Tier)
		{
			this.Duration = 1000;
		}

		// Token: 0x060085CD RID: 34253 RVA: 0x00303AF4 File Offset: 0x00301CF4
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060085CE RID: 34254 RVA: 0x00303AF7 File Offset: 0x00301CF7
		public override int GetEffectType()
		{
			return 8194;
		}

		// Token: 0x060085CF RID: 34255 RVA: 0x00303AFE File Offset: 0x00301CFE
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060085D0 RID: 34256 RVA: 0x00303B01 File Offset: 0x00301D01
		public override string GetDetails()
		{
			return "-60 Quickness\nCan see into dimensions half a step over.";
		}

		// Token: 0x060085D1 RID: 34257 RVA: 0x00303B08 File Offset: 0x00301D08
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent(Event.New("ApplyFungalVisionary", "Duration", this.Duration)))
			{
				return false;
			}
			FungalVisionary effect = Object.GetEffect<FungalVisionary>();
			if (effect != null)
			{
				effect.Duration = Math.Max(this.Duration, effect.Duration);
				return false;
			}
			this.ApplyStats();
			if (Object.IsPlayer())
			{
				FungalVisionary.VisionLevel = 1;
			}
			return true;
		}

		// Token: 0x060085D2 RID: 34258 RVA: 0x00303B6C File Offset: 0x00301D6C
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
			if (Object.IsPlayer())
			{
				FungalVisionary.VisionLevel = 0;
			}
		}

		// Token: 0x060085D3 RID: 34259 RVA: 0x00303B82 File Offset: 0x00301D82
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("Speed", -60, false);
		}

		// Token: 0x060085D4 RID: 34260 RVA: 0x00303B98 File Offset: 0x00301D98
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x060085D5 RID: 34261 RVA: 0x00303BAB File Offset: 0x00301DAB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x060085D6 RID: 34262 RVA: 0x00303BCB File Offset: 0x00301DCB
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x060085D7 RID: 34263 RVA: 0x00303C06 File Offset: 0x00301E06
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<AfterPlayerBodyChangeEvent>.ID || ID == SingletonEvent<AfterGameLoadedEvent>.ID;
		}

		// Token: 0x060085D8 RID: 34264 RVA: 0x00303C2C File Offset: 0x00301E2C
		public static void CheckVisionLevel()
		{
			FungalVisionary.VisionLevel = (The.Player.HasEffect<FungalVisionary>() ? 1 : 0);
		}

		// Token: 0x060085D9 RID: 34265 RVA: 0x00303C43 File Offset: 0x00301E43
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			FungalVisionary.CheckVisionLevel();
			return base.HandleEvent(E);
		}

		// Token: 0x060085DA RID: 34266 RVA: 0x00303C51 File Offset: 0x00301E51
		public override bool HandleEvent(AfterPlayerBodyChangeEvent E)
		{
			if (E.NewBody == null || !E.NewBody.HasEffect<FungalVisionary>())
			{
				FungalVisionary.VisionLevel = 0;
			}
			else
			{
				FungalVisionary.VisionLevel = 1;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060085DB RID: 34267 RVA: 0x00303C7D File Offset: 0x00301E7D
		public override bool HandleEvent(AfterGameLoadedEvent E)
		{
			FungalVisionary.CheckVisionLevel();
			return base.HandleEvent(E);
		}

		// Token: 0x060085DC RID: 34268 RVA: 0x00303C8B File Offset: 0x00301E8B
		[WishCommand("shimmering", null)]
		public void Wish()
		{
			The.Player.ForceApplyEffect(new FungalVisionary(1000), null);
		}

		// Token: 0x040039C7 RID: 14791
		public static int VisionLevel;
	}
}
