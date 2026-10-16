using System;
using XRL.Core;
using XRL.UI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D7F RID: 3455
	[Serializable]
	public class Salve_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x06008927 RID: 35111 RVA: 0x00311877 File Offset: 0x0030FA77
		public Salve_Tonic()
		{
		}

		// Token: 0x06008928 RID: 35112 RVA: 0x0031187F File Offset: 0x0030FA7F
		public Salve_Tonic(int Duration)
		{
			this.Duration = Duration;
		}

		// Token: 0x06008929 RID: 35113 RVA: 0x0031188E File Offset: 0x0030FA8E
		public void Initialize(int Tier)
		{
			this.Duration = 5;
		}

		// Token: 0x0600892A RID: 35114 RVA: 0x00311897 File Offset: 0x0030FA97
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600892B RID: 35115 RVA: 0x0031189A File Offset: 0x0030FA9A
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600892C RID: 35116 RVA: 0x0031189D File Offset: 0x0030FA9D
		public override string GetDescription()
		{
			return "{{Y|salve}} tonic";
		}

		// Token: 0x0600892D RID: 35117 RVA: 0x003118A4 File Offset: 0x0030FAA4
		public override string GetStateDescription()
		{
			return "under the effects of {{Y|salve}} tonic";
		}

		// Token: 0x0600892E RID: 35118 RVA: 0x003118AB File Offset: 0x0030FAAB
		public override string GetDetails()
		{
			if (base.Object.IsTrueKin())
			{
				return "Recovers 0.9 hit points per level (minimum 5) each turn.";
			}
			return "Recovers 0.6 hit points per level (minimum 3) each turn.";
		}

		// Token: 0x0600892F RID: 35119 RVA: 0x003118C8 File Offset: 0x0030FAC8
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("You feel a soothing tingle in your chest as your wounds start to close.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (Object.GetLongProperty("Overdosing", 0L) == 1L)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			return true;
		}

		// Token: 0x06008930 RID: 35120 RVA: 0x00311918 File Offset: 0x0030FB18
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("The soothing tingle fades.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			base.Remove(Object);
		}

		// Token: 0x06008931 RID: 35121 RVA: 0x0031193E File Offset: 0x0030FB3E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndAction");
			Registrar.Register("Overdose");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008932 RID: 35122 RVA: 0x0031195E File Offset: 0x0030FB5E
		public override void ApplyAllergy(GameObject subject)
		{
		}

		// Token: 0x06008933 RID: 35123 RVA: 0x00311960 File Offset: 0x0030FB60
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndAction")
			{
				if (base.Object.HasStat("Hitpoints") && base.Object.HasStat("Level"))
				{
					int num = base.Object.BaseStat("Level", 0);
					if (base.Object.IsTrueKin())
					{
						this.HealAmount += (float)Math.Max(5.0, Math.Ceiling(0.9 * (double)((float)num)));
					}
					else
					{
						this.HealAmount += (float)Math.Max(3.0, Math.Ceiling(0.6 * (double)((float)num)));
					}
					if (this.HealAmount >= 1f)
					{
						base.Object.Heal((int)Math.Floor((double)this.HealAmount), true, true, true);
						this.HealAmount -= (float)Math.Floor((double)this.HealAmount);
					}
				}
			}
			else if (E.ID == "Overdose" && this.Duration > 0)
			{
				if (base.Object.HasPart<TonicAllergy>())
				{
					TonicAllergy.SalveOverdose(base.Object);
				}
				else
				{
					this.Duration = 0;
					if (base.Object.IsPlayer())
					{
						if (base.Object.GetLongProperty("Overdosing", 0L) == 1L)
						{
							Popup.Show("Your mutant physiology reacts adversely to the tonic. The soothing tingle fades.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else
						{
							Popup.Show("The tonics you ingested react adversely to each other. The soothing tingle fades.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008934 RID: 35124 RVA: 0x00311B08 File Offset: 0x0030FD08
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 15 && num < 25)
			{
				E.Tile = null;
				E.RenderString = "+";
				E.ColorString = "&Y";
			}
			return true;
		}

		// Token: 0x04003A70 RID: 14960
		public float HealAmount;
	}
}
