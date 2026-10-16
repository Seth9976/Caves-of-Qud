using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D44 RID: 3396
	[Serializable]
	public class Healing : Effect, ITierInitialized
	{
		// Token: 0x06008619 RID: 34329 RVA: 0x00304705 File Offset: 0x00302905
		public Healing()
		{
			this.DisplayName = "healing";
		}

		// Token: 0x0600861A RID: 34330 RVA: 0x00304718 File Offset: 0x00302918
		public Healing(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600861B RID: 34331 RVA: 0x00304727 File Offset: 0x00302927
		public void Initialize(int Tier)
		{
			this.Duration = 5;
		}

		// Token: 0x0600861C RID: 34332 RVA: 0x00304730 File Offset: 0x00302930
		public override int GetEffectType()
		{
			return 83886084;
		}

		// Token: 0x0600861D RID: 34333 RVA: 0x00304737 File Offset: 0x00302937
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600861E RID: 34334 RVA: 0x0030473A File Offset: 0x0030293A
		public override string GetDetails()
		{
			return "5% HP healed per turn.\nStops healing if another action is taken or damage is taken.";
		}

		// Token: 0x0600861F RID: 34335 RVA: 0x00304744 File Offset: 0x00302944
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Healing>())
			{
				this.Duration = 5;
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyingHealing", "Effect", this)))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			base.DidX("begin", "healing", null, null, null, Object, null, false, false, null, null, false, false, false, false, false, null);
			return true;
		}

		// Token: 0x06008620 RID: 34336 RVA: 0x003047C9 File Offset: 0x003029C9
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
		}

		// Token: 0x06008621 RID: 34337 RVA: 0x003047D2 File Offset: 0x003029D2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<UseEnergyEvent>.ID;
		}

		// Token: 0x06008622 RID: 34338 RVA: 0x003047F8 File Offset: 0x003029F8
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (!base.Object.CanMoveExtremities(null, false, false, true))
			{
				this.Duration = 0;
			}
			else if (this.Duration > 0 && this.Duration != 9999)
			{
				this.Duration--;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008623 RID: 34339 RVA: 0x0030484A File Offset: 0x00302A4A
		public override bool HandleEvent(EndTurnEvent E)
		{
			base.Object.Heal(Math.Max(base.Object.BaseStat("Hitpoints", 0) / 20, 1), false, false, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06008624 RID: 34340 RVA: 0x0030487C File Offset: 0x00302A7C
		public override bool HandleEvent(UseEnergyEvent E)
		{
			if (!E.Passive || (E.Type != null && !E.Type.Contains("Pass")))
			{
				if (base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("Your healing is interrupted!", 'r', true);
				}
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008625 RID: 34341 RVA: 0x003048D3 File Offset: 0x00302AD3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("TakeDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008626 RID: 34342 RVA: 0x003048E8 File Offset: 0x00302AE8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TakeDamage" && (E.GetParameter("Damage") as Damage).Amount > 0)
			{
				if (base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("Your healing is interrupted!", 'r', true);
				}
				this.Duration = 0;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008627 RID: 34343 RVA: 0x00304948 File Offset: 0x00302B48
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 25 && num < 35)
				{
					E.Tile = null;
					E.RenderString = "Z";
					E.ColorString = "&g";
				}
			}
			return true;
		}

		// Token: 0x040039CF RID: 14799
		private int DamageLeft;
	}
}
