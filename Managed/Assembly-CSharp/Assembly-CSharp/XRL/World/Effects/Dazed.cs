using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D1F RID: 3359
	[Serializable]
	public class Dazed : Effect, ITierInitialized
	{
		// Token: 0x0600843C RID: 33852 RVA: 0x002FC5D3 File Offset: 0x002FA7D3
		public Dazed()
		{
			this.DisplayName = "{{C|dazed}}";
		}

		// Token: 0x0600843D RID: 33853 RVA: 0x002FC5F5 File Offset: 0x002FA7F5
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(100, 200);
		}

		// Token: 0x0600843E RID: 33854 RVA: 0x002FC609 File Offset: 0x002FA809
		public override string GetDescription()
		{
			return this.DisplayName;
		}

		// Token: 0x0600843F RID: 33855 RVA: 0x002FC611 File Offset: 0x002FA811
		public Dazed(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008440 RID: 33856 RVA: 0x002FC620 File Offset: 0x002FA820
		public Dazed(int Duration, bool DontStunIfPlayer)
			: this(Duration)
		{
			this.DontStunIfPlayer = DontStunIfPlayer;
		}

		// Token: 0x06008441 RID: 33857 RVA: 0x002FC630 File Offset: 0x002FA830
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x06008442 RID: 33858 RVA: 0x002FC638 File Offset: 0x002FA838
		public override bool SameAs(Effect e)
		{
			Dazed dazed = e as Dazed;
			return dazed.Penalty == this.Penalty && dazed.SpeedPenalty == this.SpeedPenalty && dazed.DontStunIfPlayer == this.DontStunIfPlayer && base.SameAs(e);
		}

		// Token: 0x06008443 RID: 33859 RVA: 0x002FC683 File Offset: 0x002FA883
		public override string GetDetails()
		{
			return "-4 Agility\n-4 Intelligence\n-10 Move Speed";
		}

		// Token: 0x06008444 RID: 33860 RVA: 0x002FC68C File Offset: 0x002FA88C
		public override bool Apply(GameObject Object)
		{
			if (Object.Brain == null)
			{
				return false;
			}
			if (Object.HasEffect<Dazed>())
			{
				if (!this.DontStunIfPlayer || !Object.IsPlayer() || !Object.HasEffect<Stun>())
				{
					Object.ApplyEffect(new Stun(1, 30, this.DontStunIfPlayer), null);
				}
				return false;
			}
			if (!Object.FireEvent(Event.New("ApplyDazed", "Duration", this.Duration)))
			{
				return false;
			}
			base.DidX("are", "dazed", null, null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
			Object.ParticleText("*dazed*", IComponent<GameObject>.ConsequentialColorChar(null, Object), false, 1.5f, -8f);
			this.ApplyStats();
			return true;
		}

		// Token: 0x06008445 RID: 33861 RVA: 0x002FC73D File Offset: 0x002FA93D
		public override void Remove(GameObject Object)
		{
			this.UnapplyStats();
		}

		// Token: 0x06008446 RID: 33862 RVA: 0x002FC748 File Offset: 0x002FA948
		private void ApplyStats()
		{
			this.Penalty = 4;
			this.SpeedPenalty = 10;
			base.StatShifter.SetStatShift(base.Object, "Intelligence", -this.Penalty, false);
			base.StatShifter.SetStatShift(base.Object, "Agility", -this.Penalty, false);
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", this.SpeedPenalty, false);
		}

		// Token: 0x06008447 RID: 33863 RVA: 0x002FC7C0 File Offset: 0x002FA9C0
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
			this.Penalty = 0;
			this.SpeedPenalty = 0;
		}

		// Token: 0x06008448 RID: 33864 RVA: 0x002FC7E1 File Offset: 0x002FA9E1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06008449 RID: 33865 RVA: 0x002FC7F7 File Offset: 0x002FA9F7
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0 && this.Duration != 9999 && !base.Object.HasEffect<Stun>())
			{
				this.Duration--;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600844A RID: 33866 RVA: 0x002FC831 File Offset: 0x002FAA31
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("ApplyDazed");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600844B RID: 33867 RVA: 0x002FC85C File Offset: 0x002FAA5C
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 10)
				{
					E.RenderString = "?";
					E.ColorString = "&c^b";
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600844C RID: 33868 RVA: 0x002FC8A0 File Offset: 0x002FAAA0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyDazed")
			{
				if (this.Duration > 0)
				{
					return false;
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003972 RID: 14706
		public int Penalty = 4;

		// Token: 0x04003973 RID: 14707
		public int SpeedPenalty = 10;

		// Token: 0x04003974 RID: 14708
		public bool DontStunIfPlayer;
	}
}
