using System;
using System.Linq;

namespace XRL.World.Parts
{
	// Token: 0x02000EA6 RID: 3750
	[Serializable]
	public class EffectResistance : IPart
	{
		// Token: 0x17000A8B RID: 2699
		// (set) Token: 0x06009696 RID: 38550 RVA: 0x0036912E File Offset: 0x0036732E
		public string Values
		{
			set
			{
				if (value.IsNullOrEmpty())
				{
					return;
				}
				this.Effects = value.Split(',', StringSplitOptions.None);
			}
		}

		// Token: 0x06009697 RID: 38551 RVA: 0x00369148 File Offset: 0x00367348
		public EffectResistance()
		{
			this.Effects = new string[0];
		}

		// Token: 0x06009698 RID: 38552 RVA: 0x00369164 File Offset: 0x00367364
		public EffectResistance(params string[] Effects)
		{
			this.Effects = Effects;
		}

		// Token: 0x06009699 RID: 38553 RVA: 0x0036917C File Offset: 0x0036737C
		public override bool SameAs(IPart p)
		{
			EffectResistance effectResistance = p as EffectResistance;
			return effectResistance != null && effectResistance.Effects.SequenceEqual<string>(this.Effects);
		}

		// Token: 0x0600969A RID: 38554 RVA: 0x003691A6 File Offset: 0x003673A6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanApplyEffectEvent.ID || ID == ApplyEffectEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID;
		}

		// Token: 0x0600969B RID: 38555 RVA: 0x003691CC File Offset: 0x003673CC
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return (this.Chance < 100 || !this.Effects.Contains(E.Name)) && base.HandleEvent(E);
		}

		// Token: 0x0600969C RID: 38556 RVA: 0x003691F4 File Offset: 0x003673F4
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return (!this.Effects.Contains(E.Name) || !this.Chance.in100()) && base.HandleEvent(E);
		}

		// Token: 0x0600969D RID: 38557 RVA: 0x0036921F File Offset: 0x0036741F
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Resists", string.Join(", ", this.Effects));
			E.AddEntry(this, "Chance", this.Chance);
			return base.HandleEvent(E);
		}

		// Token: 0x04003E63 RID: 15971
		public string[] Effects;

		// Token: 0x04003E64 RID: 15972
		public int Chance = 100;
	}
}
