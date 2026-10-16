using System;
using XRL.Core;
using XRL.Rules;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	// Token: 0x02000D72 RID: 3442
	[Serializable]
	public class Poisoned : Effect, ITierInitialized
	{
		// Token: 0x06008859 RID: 34905 RVA: 0x0030D43E File Offset: 0x0030B63E
		public Poisoned()
		{
			this.DisplayName = "{{G|poisoned}}";
		}

		// Token: 0x0600885A RID: 34906 RVA: 0x0030D451 File Offset: 0x0030B651
		public Poisoned(int Duration, string DamageIncrement, int Level, GameObject Owner = null, bool StartMessageUsePopup = false, bool StopMessageUsePopup = false)
			: this()
		{
			this.Duration = Duration;
			this.DamageIncrement = DamageIncrement;
			this.Level = Level;
			this.Owner = Owner;
			this.StartMessageUsePopup = StartMessageUsePopup;
			this.StopMessageUsePopup = StopMessageUsePopup;
		}

		// Token: 0x0600885B RID: 34907 RVA: 0x0030D488 File Offset: 0x0030B688
		public void Initialize(int Tier)
		{
			Tier = Tier.Constrain(Stat.Random(Tier - 2, Tier + 2));
			this.Duration = Stat.Random(4, 10);
			this.Level = (int)((double)Tier * 1.5);
			this.DamageIncrement = this.Level.ToString() + "d2";
		}

		// Token: 0x0600885C RID: 34908 RVA: 0x0030D4E3 File Offset: 0x0030B6E3
		public override int GetEffectType()
		{
			return 117506052;
		}

		// Token: 0x0600885D RID: 34909 RVA: 0x0030D4EA File Offset: 0x0030B6EA
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600885E RID: 34910 RVA: 0x0030D4ED File Offset: 0x0030B6ED
		public override string GetDetails()
		{
			return "(" + this.DamageIncrement + ")/2 damage per turn.\nDoesn't regenerate hit points.\nHealing effects are only half as effective.\nWill become ill once the poison runs its course.";
		}

		// Token: 0x0600885F RID: 34911 RVA: 0x0030D504 File Offset: 0x0030B704
		public override bool Apply(GameObject Object)
		{
			Poisoned poisoned;
			if (Object.TryGetEffect<Poisoned>(out poisoned))
			{
				if (this.Duration > poisoned.Duration)
				{
					poisoned.Duration = this.Duration;
				}
				if (this.DamageIncrement.RollMax() > poisoned.DamageIncrement.RollMax())
				{
					poisoned.DamageIncrement = this.DamageIncrement;
				}
				if (this.Level > poisoned.Level)
				{
					poisoned.Level = this.Level;
				}
				return false;
			}
			if (!Object.FireEvent("ApplyPoison") || !ApplyEffectEvent.Check(Object, "Poison", this, null))
			{
				return false;
			}
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_poisoned", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			base.DidX("have", "been poisoned", "!", null, null, null, Object, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
			return base.Apply(Object);
		}

		// Token: 0x06008860 RID: 34912 RVA: 0x0030D5EC File Offset: 0x0030B7EC
		public override void Remove(GameObject Object)
		{
		}

		// Token: 0x06008861 RID: 34913 RVA: 0x0030D5EE File Offset: 0x0030B7EE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID;
		}

		// Token: 0x06008862 RID: 34914 RVA: 0x0030D614 File Offset: 0x0030B814
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("poisoned", 30);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008863 RID: 34915 RVA: 0x0030D638 File Offset: 0x0030B838
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			GameObject.Validate(ref this.Owner);
			if (this.Duration > 0 && !base.Object.CurrentCell.HasObjectWithPart("GasPoison"))
			{
				base.Object.TakeDamage((int)Math.Ceiling((double)((float)this.DamageIncrement.RollCached() / 2f)), "from %t {{g|poison}}!", "Poison Unavoidable", null, null, this.Owner, null, null, null, null, false, false, true, false, false, false, false, false, false, 5, null);
				if (this.Duration > 0 && this.Duration != 9999)
				{
					this.Duration--;
				}
				if (this.Duration <= 0)
				{
					int num = Stat.Random((int)(35f + 0.8f * (float)this.Level * 4.5f), (int)(35f + 1.2f * (float)this.Level * 4.5f));
					base.Object.ApplyEffect(new Ill(num, this.Level, null, this.StopMessageUsePopup, this.StopMessageUsePopup), null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008864 RID: 34916 RVA: 0x0030D751 File Offset: 0x0030B951
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06008865 RID: 34917 RVA: 0x0030D761 File Offset: 0x0030B961
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Healing");
			Registrar.Register("Recuperating");
			Registrar.Register("Regenerating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008866 RID: 34918 RVA: 0x0030D78C File Offset: 0x0030B98C
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "\u0003";
				E.ColorString = "&G^k";
			}
			return true;
		}

		// Token: 0x06008867 RID: 34919 RVA: 0x0030D7CC File Offset: 0x0030B9CC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Healing")
			{
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) / 2);
			}
			else
			{
				if (E.ID == "Regenerating")
				{
					E.SetParameter("Amount", 0);
					return false;
				}
				if (E.ID == "Recuperating")
				{
					this.Duration = 0;
					base.DidX("are", "no longer poisoned", "!", null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003A4B RID: 14923
		public GameObject Owner;

		// Token: 0x04003A4C RID: 14924
		public string DamageIncrement;

		// Token: 0x04003A4D RID: 14925
		public int Level;

		// Token: 0x04003A4E RID: 14926
		public bool StartMessageUsePopup;

		// Token: 0x04003A4F RID: 14927
		public bool StopMessageUsePopup;
	}
}
