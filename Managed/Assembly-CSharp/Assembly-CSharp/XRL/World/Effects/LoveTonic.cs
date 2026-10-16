using System;
using HistoryKit;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D61 RID: 3425
	[Serializable]
	public class LoveTonic : Effect, ITierInitialized
	{
		// Token: 0x0600877A RID: 34682 RVA: 0x00309DF2 File Offset: 0x00307FF2
		public LoveTonic()
		{
			this.DisplayName = "{{amorous|love}} tonic";
		}

		// Token: 0x0600877B RID: 34683 RVA: 0x00309E05 File Offset: 0x00308005
		public LoveTonic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600877C RID: 34684 RVA: 0x00309E14 File Offset: 0x00308014
		public void Initialize(int Tier)
		{
			if (If.CoinFlip(null, null))
			{
				this.bOverdose = true;
			}
			this.Duration = Stat.Roll(500, 700);
		}

		// Token: 0x0600877D RID: 34685 RVA: 0x00309E3B File Offset: 0x0030803B
		public override string GetStateDescription()
		{
			return "under the effects of {{amorous|love}} tonic";
		}

		// Token: 0x0600877E RID: 34686 RVA: 0x00309E42 File Offset: 0x00308042
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600877F RID: 34687 RVA: 0x00309E45 File Offset: 0x00308045
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008780 RID: 34688 RVA: 0x00309E48 File Offset: 0x00308048
		public override bool UseThawEventToUpdateDuration()
		{
			return true;
		}

		// Token: 0x06008781 RID: 34689 RVA: 0x00309E4B File Offset: 0x0030804B
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x06008782 RID: 34690 RVA: 0x00309E52 File Offset: 0x00308052
		public override string GetDescription()
		{
			return "{{amorous|love}} tonic";
		}

		// Token: 0x06008783 RID: 34691 RVA: 0x00309E59 File Offset: 0x00308059
		public override string GetDetails()
		{
			return "Will fall in love with the first thing examined.";
		}

		// Token: 0x06008784 RID: 34692 RVA: 0x00309E60 File Offset: 0x00308060
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("Your heart swells with a burning sensation.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (Object.GetLongProperty("Overdosing", 0L) == 1L || this.bOverdose)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			return true;
		}

		// Token: 0x06008785 RID: 34693 RVA: 0x00309EB8 File Offset: 0x003080B8
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("Your heart rate slows again.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			base.Remove(Object);
		}

		// Token: 0x06008786 RID: 34694 RVA: 0x00309EDE File Offset: 0x003080DE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("LookedAt");
			Registrar.Register("Overdose");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008787 RID: 34695 RVA: 0x00309F00 File Offset: 0x00308100
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LookedAt")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (gameObjectParameter != null && (gameObjectParameter.IsCombat || gameObjectParameter.GetBlueprint(true).InheritsFrom("Furniture")))
				{
					base.Object.ApplyEffect(new Lovesick(Stat.Random(3000, 3600), gameObjectParameter), null);
					this.Duration = 0;
					base.Object.CleanEffects();
				}
			}
			else if (E.ID == "Overdose" && this.Duration > 0)
			{
				this.Duration = 0;
				if (base.Object.IsPlayer())
				{
					if (base.Object.GetLongProperty("Overdosing", 0L) == 1L)
					{
						Popup.Show("Your mutant physiology reacts adversely to the tonic. You erupt into flames!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show("The tonics you ingested react adversely to each other. You erupt into flames!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				base.Object.Physics.Temperature = base.Object.Physics.FlameTemperature + 200;
				base.Object.CleanEffects();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008788 RID: 34696 RVA: 0x0030A03C File Offset: 0x0030823C
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 35 && num < 45)
			{
				E.Tile = null;
				E.RenderString = "\u0003";
				int num2 = Stat.RandomCosmetic(1, 4);
				if (num2 == 1)
				{
					E.ColorString = "&r";
				}
				else if (num2 == 2)
				{
					E.ColorString = "&R";
				}
				else if (num2 == 3)
				{
					E.ColorString = "&M";
				}
				else if (num2 == 4)
				{
					E.ColorString = "&m";
				}
			}
			return true;
		}

		// Token: 0x04003A1F RID: 14879
		public bool bOverdose;
	}
}
