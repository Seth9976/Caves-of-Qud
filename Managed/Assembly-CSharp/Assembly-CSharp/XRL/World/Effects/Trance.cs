using System;
using XRL.Core;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000DA3 RID: 3491
	[Serializable]
	public class Trance : Effect
	{
		// Token: 0x06008B57 RID: 35671 RVA: 0x00318E89 File Offset: 0x00317089
		public Trance()
		{
			this.DisplayName = "trance";
		}

		// Token: 0x06008B58 RID: 35672 RVA: 0x00318E9C File Offset: 0x0031709C
		public Trance(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008B59 RID: 35673 RVA: 0x00318EAB File Offset: 0x003170AB
		public override int GetEffectType()
		{
			return 2;
		}

		// Token: 0x06008B5A RID: 35674 RVA: 0x00318EAE File Offset: 0x003170AE
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008B5B RID: 35675 RVA: 0x00318EB1 File Offset: 0x003170B1
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008B5C RID: 35676 RVA: 0x00318EB4 File Offset: 0x003170B4
		public override string GetDescription()
		{
			return "entranced";
		}

		// Token: 0x06008B5D RID: 35677 RVA: 0x00318EBC File Offset: 0x003170BC
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect(typeof(Trance)))
			{
				this.Duration = 50;
				return true;
			}
			if (Object.FireEvent(Event.New("ApplyTrance", "Effect", this)))
			{
				base.DidX("enter", "a trance", "!", null, null, Object, null, false, false, null, null, false, false, false, false, false, null);
				Object.ModIntProperty("MentalMutationShift", this.MentalBonus, true);
				return true;
			}
			return false;
		}

		// Token: 0x06008B5E RID: 35678 RVA: 0x00318F36 File Offset: 0x00317136
		public override void Remove(GameObject Object)
		{
			Object.ModIntProperty("MentalMutationShift", -this.MentalBonus, true);
		}

		// Token: 0x06008B5F RID: 35679 RVA: 0x00318F4C File Offset: 0x0031714C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06008B60 RID: 35680 RVA: 0x00318F50 File Offset: 0x00317150
		public override void TurnTick(long TimeTick, int Amount)
		{
			ActivatedAbilities activatedAbilities;
			if (this.Duration > 0 && base.Object.TryGetPart<ActivatedAbilities>(out activatedAbilities))
			{
				activatedAbilities.TickCooldowns(10);
			}
		}

		// Token: 0x06008B61 RID: 35681 RVA: 0x00318F80 File Offset: 0x00317180
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 25 && num < 35)
				{
					E.Tile = null;
					E.RenderString = "*";
					E.ColorString = "&G";
				}
			}
			return true;
		}

		// Token: 0x04003ACC RID: 15052
		public int MentalBonus;
	}
}
