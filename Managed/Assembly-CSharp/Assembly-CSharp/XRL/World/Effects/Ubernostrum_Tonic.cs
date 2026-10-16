using System;
using XRL.Core;
using XRL.UI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000DA4 RID: 3492
	[Serializable]
	public class Ubernostrum_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x06008B62 RID: 35682 RVA: 0x00318FC7 File Offset: 0x003171C7
		public Ubernostrum_Tonic()
		{
		}

		// Token: 0x06008B63 RID: 35683 RVA: 0x00318FCF File Offset: 0x003171CF
		public void Initialize(int Tier)
		{
			this.Duration = 10;
		}

		// Token: 0x06008B64 RID: 35684 RVA: 0x00318FD9 File Offset: 0x003171D9
		public override void ApplyAllergy(GameObject subject)
		{
		}

		// Token: 0x06008B65 RID: 35685 RVA: 0x00318FDB File Offset: 0x003171DB
		public Ubernostrum_Tonic(int Duration)
		{
			this.Duration = Duration;
		}

		// Token: 0x06008B66 RID: 35686 RVA: 0x00318FEA File Offset: 0x003171EA
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008B67 RID: 35687 RVA: 0x00318FED File Offset: 0x003171ED
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008B68 RID: 35688 RVA: 0x00318FF0 File Offset: 0x003171F0
		public override string GetDescription()
		{
			return "{{ubernostrum|ubernostrum}} tonic";
		}

		// Token: 0x06008B69 RID: 35689 RVA: 0x00318FF7 File Offset: 0x003171F7
		public override string GetStateDescription()
		{
			return "under the effects of {{ubernostrum|ubernostrum}} tonic";
		}

		// Token: 0x06008B6A RID: 35690 RVA: 0x00318FFE File Offset: 0x003171FE
		public override string GetDetails()
		{
			if (base.Object.IsTrueKin())
			{
				return "Recovers 0.9 hit points per level (minimum 5) each turn.\nPurged of all short-term, biological debuffs and up to one severed appendage is regrown at the end of the tenth turn.";
			}
			return "Recovers 0.6 hit points per level (minimum 3) each turn.\nPurged of all short-term, biological debuffs and up to one severed appendage is regrown at the end of the tenth turn.";
		}

		// Token: 0x06008B6B RID: 35691 RVA: 0x00319018 File Offset: 0x00317218
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("A torrent of life rushes over you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			this.SendRegeneraEvent();
			if (Object.GetLongProperty("Overdosing", 0L) == 1L)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			return true;
		}

		// Token: 0x06008B6C RID: 35692 RVA: 0x00319070 File Offset: 0x00317270
		public override void Remove(GameObject Object)
		{
			Object.FireEvent("Recuperating");
			RegenerateLimbEvent.Send(Object, null, null, true, false, true, true, null, null, null, null, null);
			this.SendRegeneraEvent();
			if (Object.IsPlayer())
			{
				Popup.Show("The torrent of life sweeps away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			base.Remove(Object);
		}

		// Token: 0x06008B6D RID: 35693 RVA: 0x003190DD File Offset: 0x003172DD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndActionEvent>.ID;
		}

		// Token: 0x06008B6E RID: 35694 RVA: 0x003190F4 File Offset: 0x003172F4
		public override bool HandleEvent(EndActionEvent E)
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
			return base.HandleEvent(E);
		}

		// Token: 0x06008B6F RID: 35695 RVA: 0x003191F2 File Offset: 0x003173F2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Overdose");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008B70 RID: 35696 RVA: 0x00319208 File Offset: 0x00317408
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Overdose" && this.Duration > 0)
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
							Popup.Show("Your mutant physiology reacts adversely to the tonic. The torrent of life sweeps away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else
						{
							Popup.Show("The tonics you ingested react adversely to each other. The torrent of life sweeps away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008B71 RID: 35697 RVA: 0x003192A8 File Offset: 0x003174A8
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 20 && num < 30)
			{
				E.Tile = null;
				E.RenderString = "+";
				E.ColorString = "&G";
			}
			return true;
		}

		// Token: 0x06008B72 RID: 35698 RVA: 0x003192EF File Offset: 0x003174EF
		public void SendRegeneraEvent(GameObject Object)
		{
			Object.FireEvent(Event.New("Regenera", "SourceDescription", "The " + this.GetDescription() + " cures you of", "Level", 1));
		}

		// Token: 0x06008B73 RID: 35699 RVA: 0x00319322 File Offset: 0x00317522
		public void SendRegeneraEvent()
		{
			this.SendRegeneraEvent(base.Object);
		}

		// Token: 0x04003ACD RID: 15053
		public float HealAmount;
	}
}
