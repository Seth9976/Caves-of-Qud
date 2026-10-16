using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C33 RID: 3123
	[Serializable]
	public class AshPoison : Effect, ITierInitialized
	{
		// Token: 0x06007F25 RID: 32549 RVA: 0x002EEF2B File Offset: 0x002ED12B
		public AshPoison()
		{
			this.DisplayName = "{{K|choking on ash}}";
		}

		// Token: 0x06007F26 RID: 32550 RVA: 0x002EEF45 File Offset: 0x002ED145
		public AshPoison(int Duration, GameObject Owner)
			: this()
		{
			this.Owner = Owner;
			this.Duration = Duration;
		}

		// Token: 0x06007F27 RID: 32551 RVA: 0x002EEF5B File Offset: 0x002ED15B
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(4, 10);
			this.Damage = Stat.Random(2, 4);
		}

		// Token: 0x06007F28 RID: 32552 RVA: 0x002EEF78 File Offset: 0x002ED178
		public override string GetStateDescription()
		{
			return "{{K|choked by ash}}";
		}

		// Token: 0x06007F29 RID: 32553 RVA: 0x002EEF7F File Offset: 0x002ED17F
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007F2A RID: 32554 RVA: 0x002EEF82 File Offset: 0x002ED182
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06007F2B RID: 32555 RVA: 0x002EEF85 File Offset: 0x002ED185
		public override int GetEffectType()
		{
			return 117440520;
		}

		// Token: 0x06007F2C RID: 32556 RVA: 0x002EEF8C File Offset: 0x002ED18C
		public override string GetDetails()
		{
			return this.Damage.ToString() + " damage per turn.";
		}

		// Token: 0x06007F2D RID: 32557 RVA: 0x002EEFA4 File Offset: 0x002ED1A4
		public override bool Apply(GameObject Object)
		{
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_poisoned", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			return Object.FireEvent("ApplyAshPoison") && ApplyEffectEvent.Check(Object, "AshPoison", this, null);
		}

		// Token: 0x06007F2E RID: 32558 RVA: 0x002EEFF7 File Offset: 0x002ED1F7
		public override void Remove(GameObject Object)
		{
		}

		// Token: 0x06007F2F RID: 32559 RVA: 0x002EEFF9 File Offset: 0x002ED1F9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID;
		}

		// Token: 0x06007F30 RID: 32560 RVA: 0x002EF01F File Offset: 0x002ED21F
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("choking on ash", 90);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F31 RID: 32561 RVA: 0x002EF044 File Offset: 0x002ED244
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject.Validate(ref this.Owner);
			if (this.Duration > 0)
			{
				GameObject @object = base.Object;
				if (((@object != null) ? @object.CurrentCell : null) != null && !base.Object.CurrentCell.HasObjectWithPart("GasAsh"))
				{
					base.Object.TakeDamage(this.Damage, "from %t {{W|choking ash}}!", "Asphyxiation Gas Unavoidable", null, null, null, this.Owner, null, null, null, false, false, true, false, false, false, false, false, false, 5, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x002EF0CB File Offset: 0x002ED2CB
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06007F33 RID: 32563 RVA: 0x002EF0DB File Offset: 0x002ED2DB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Recuperating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007F34 RID: 32564 RVA: 0x002EF0F0 File Offset: 0x002ED2F0
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "!";
				E.ColorString = "&W^r";
			}
			return true;
		}

		// Token: 0x06007F35 RID: 32565 RVA: 0x002EF130 File Offset: 0x002ED330
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Recuperating")
			{
				this.Duration = 0;
				base.DidX("are", "no longer choking", "!", null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x040038BF RID: 14527
		public int Damage = 2;

		// Token: 0x040038C0 RID: 14528
		public GameObject Owner;
	}
}
