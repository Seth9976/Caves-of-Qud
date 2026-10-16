using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D6D RID: 3437
	[Serializable]
	public class PhasePoisoned : Effect, ITierInitialized
	{
		// Token: 0x06008812 RID: 34834 RVA: 0x0030C1AD File Offset: 0x0030A3AD
		public PhasePoisoned()
		{
			this.DisplayName = "{{g|phase spider venom}}";
		}

		// Token: 0x06008813 RID: 34835 RVA: 0x0030C1C0 File Offset: 0x0030A3C0
		public PhasePoisoned(int Duration, string DamageIncrement, int Level, GameObject Owner)
			: this()
		{
			this.Duration = Duration;
			this.DamageIncrement = DamageIncrement;
			this.Level = Level;
			this.Owner = Owner;
		}

		// Token: 0x06008814 RID: 34836 RVA: 0x0030C1E8 File Offset: 0x0030A3E8
		public void Initialize(int Tier)
		{
			Tier = Stat.Random(Tier - 2, Tier + 2);
			if (Tier < 1)
			{
				Tier = 1;
			}
			if (Tier > 8)
			{
				Tier = 8;
			}
			this.Duration = Stat.Random(4, 10);
			this.Level = (int)((double)Tier * 1.5);
			this.DamageIncrement = this.Level.ToString() + "d2";
		}

		// Token: 0x06008815 RID: 34837 RVA: 0x0030C24C File Offset: 0x0030A44C
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x06008816 RID: 34838 RVA: 0x0030C253 File Offset: 0x0030A453
		public override string GetDescription()
		{
			return "{{g|phase spider venom}}";
		}

		// Token: 0x06008817 RID: 34839 RVA: 0x0030C25A File Offset: 0x0030A45A
		public override string GetStateDescription()
		{
			return "{{g|poisoned with phase spider venom}}";
		}

		// Token: 0x06008818 RID: 34840 RVA: 0x0030C261 File Offset: 0x0030A461
		public override string GetDetails()
		{
			return "Phased.\n(" + this.DamageIncrement + ")/2 damage per turn.\nDoesn't regenerate hit points.\nHealing effects are only half as effective.\nWill become ill once the poison runs its course.";
		}

		// Token: 0x06008819 RID: 34841 RVA: 0x0030C278 File Offset: 0x0030A478
		public override bool Apply(GameObject Object)
		{
			Poisoned effect = Object.GetEffect<Poisoned>();
			if (effect != null)
			{
				if (this.Duration > effect.Duration)
				{
					effect.Duration = this.Duration;
				}
				if (this.DamageIncrement.RollMax() > effect.DamageIncrement.RollMax())
				{
					effect.DamageIncrement = this.DamageIncrement;
				}
				if (this.Level > effect.Level)
				{
					effect.Level = this.Level;
				}
				return false;
			}
			Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_poisoned", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (Object.FireEvent("ApplyPoison") && ApplyEffectEvent.Check(Object, "Poison", this, null))
			{
				base.DidX("have", "been poisoned", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
				return true;
			}
			return false;
		}

		// Token: 0x0600881A RID: 34842 RVA: 0x0030C355 File Offset: 0x0030A555
		public override void Remove(GameObject Object)
		{
			base.Remove(Object);
		}

		// Token: 0x0600881B RID: 34843 RVA: 0x0030C35E File Offset: 0x0030A55E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID;
		}

		// Token: 0x0600881C RID: 34844 RVA: 0x0030C37C File Offset: 0x0030A57C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			GameObject.Validate(ref this.Owner);
			if (this.Duration > 0)
			{
				if (base.Object.HasEffect<Phased>())
				{
					base.Object.GetEffect<Phased>().Duration += Stat.Random(1, 2);
				}
				else
				{
					base.Object.ApplyEffect(new Phased(Stat.Random(3, 6)), null);
				}
				if (!base.Object.CurrentCell.HasObjectWithPart("GasPoison"))
				{
					base.Object.TakeDamage((int)Math.Ceiling((double)((float)this.DamageIncrement.RollCached() / 2f)), "from %t {{g|poison}}!", "Poison Unavoidable", null, null, this.Owner, null, null, null, null, false, false, true, false, false, false, false, false, false, 5, null);
					if (this.Duration > 0 && this.Duration != 9999)
					{
						this.Duration--;
					}
					if (this.Duration <= 0)
					{
						int num = Stat.Random((int)(35f + 0.8f * (float)this.Level * 4.5f), (int)(35f + 1.2f * (float)this.Level * 4.5f));
						base.Object.ApplyEffect(new Ill(num, this.Level, null, false, false), null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600881D RID: 34845 RVA: 0x0030C4D1 File Offset: 0x0030A6D1
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x0600881E RID: 34846 RVA: 0x0030C4E1 File Offset: 0x0030A6E1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Healing");
			Registrar.Register("Recuperating");
			Registrar.Register("Regenerating");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600881F RID: 34847 RVA: 0x0030C50C File Offset: 0x0030A70C
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

		// Token: 0x06008820 RID: 34848 RVA: 0x0030C54C File Offset: 0x0030A74C
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

		// Token: 0x04003A3B RID: 14907
		public GameObject Owner;

		// Token: 0x04003A3C RID: 14908
		public string DamageIncrement;

		// Token: 0x04003A3D RID: 14909
		public int Level;
	}
}
