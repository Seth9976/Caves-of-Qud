using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Core;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Effects
{
	// Token: 0x02000D62 RID: 3426
	[Serializable]
	public class Lovesick : Effect, ITierInitialized
	{
		// Token: 0x06008789 RID: 34697 RVA: 0x0030A0C2 File Offset: 0x003082C2
		public Lovesick()
		{
			this.DisplayName = "{{lovesickness|lovesick}}";
		}

		// Token: 0x0600878A RID: 34698 RVA: 0x0030A0E3 File Offset: 0x003082E3
		public Lovesick(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x0600878B RID: 34699 RVA: 0x0030A0F2 File Offset: 0x003082F2
		public Lovesick(int Duration, GameObject Beauty)
			: this(Duration)
		{
			this.Beauty = Beauty;
		}

		// Token: 0x0600878C RID: 34700 RVA: 0x0030A102 File Offset: 0x00308302
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(3000, 3600);
		}

		// Token: 0x0600878D RID: 34701 RVA: 0x0030A119 File Offset: 0x00308319
		public override int GetEffectType()
		{
			return 100663298;
		}

		// Token: 0x0600878E RID: 34702 RVA: 0x0030A120 File Offset: 0x00308320
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600878F RID: 34703 RVA: 0x0030A123 File Offset: 0x00308323
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008790 RID: 34704 RVA: 0x0030A126 File Offset: 0x00308326
		public override bool UseThawEventToUpdateDuration()
		{
			return true;
		}

		// Token: 0x06008791 RID: 34705 RVA: 0x0030A12C File Offset: 0x0030832C
		public override string GetDetails()
		{
			string text = "-6 Intelligence\n-6 Willpower\n-5 Move Speed";
			if (Options.AnySifrah)
			{
				text += "\nUseful in many social, ritual, and psionic Sifrah games.";
			}
			return text;
		}

		// Token: 0x06008792 RID: 34706 RVA: 0x0030A154 File Offset: 0x00308354
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Lovesick>())
			{
				return false;
			}
			if (!Object.IsOrganic)
			{
				return false;
			}
			if (this.Beauty == null)
			{
				if (Object.Target != null)
				{
					this.Beauty = Object.Target;
				}
				else
				{
					Cell currentCell = Object.CurrentCell;
					List<GameObject> list = currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 6, Object, (GameObject x) => x.IsReal, Object.IsPlayer(), false);
					this.Beauty = list.GetRandomElement<GameObject>(null) ?? Object;
				}
			}
			if (!Object.FireEvent(Event.New("ApplyLovesick", "Duration", this.Duration)))
			{
				return false;
			}
			if (this.Beauty != Object)
			{
				Object.StopFight(this.Beauty, false, this.Beauty.IsPlayer());
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_charm", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (Object.IsPlayer())
			{
				if (this.Beauty.IsPlayer())
				{
					Achievement.LOVE_YOURSELF.Unlock();
				}
				if (this.Beauty.GetBlueprint(true).InheritsFrom("Sign"))
				{
					Achievement.LOVE_SIGN.Unlock();
				}
				JournalAPI.AddAccomplishment("Your heart sang at the sight of " + this.Beauty.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", string.Concat(new string[]
				{
					"The troubadour-hero =name= rode the tides of ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" passions and shipwrecked on the shores of ",
					this.Beauty.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), string.Concat(new string[]
				{
					"<spice.elements.",
					The.Player.GetMythicDomain(5, null),
					".weddingConditions.!random.capitalize>, =name= cemented ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" friendship with ",
					Object.GetPrimaryFactionName(true, true, true),
					" by marrying ",
					Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), null, "general", MuralCategory.CommitsFolly, MuralWeight.High, null, -1L, true);
			}
			else if (Object.Brain != null)
			{
				this.PreviousLeader = Object.Brain.PartyLeader;
				if (Object != this.Beauty && !this.Beauty.IsLedBy(Object))
				{
					Object.Brain.SetPartyLeader(this.Beauty, 0, true, false);
				}
			}
			base.DidXToY("fall", "in love with", this.Beauty, null, "!", null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
			GameObject beauty = this.Beauty;
			this.Beauty = ((beauty != null) ? beauty.DeepCopy(true, true, null) : null);
			this.ApplyStats();
			return true;
		}

		// Token: 0x06008793 RID: 34707 RVA: 0x0030A468 File Offset: 0x00308668
		public override void Remove(GameObject Object)
		{
			GameObject.Validate(ref this.PreviousLeader);
			if (Object.Brain != null && this.PreviousLeader != Object.Brain.PartyLeader && Object.FireEvent(Event.New("CanRestorePartyLeader", "PreviousLeader", this.PreviousLeader)))
			{
				GameObject partyLeader = Object.PartyLeader;
				if (partyLeader != null && partyLeader.FireEvent(Event.New("CanCompanionRestorePartyLeader", "Companion", Object, "PreviousLeader", this.PreviousLeader)))
				{
					Object.PartyLeader = this.PreviousLeader;
				}
			}
			this.UnapplyStats();
			if (Object.IsPlayer() && GameObject.Validate(ref this.Beauty))
			{
				JournalAPI.AddAccomplishment("Though your affection for " + this.Beauty.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + " endowed you with a certain wisdom, you no longer feel the tug on your heartstrings.", string.Concat(new string[]
				{
					"The call of the sea rang through the troubadour-hero =name= once again, and ",
					The.Player.GetPronounProvider(false).Subjective,
					" set sail from the familiar shore of ",
					this.Beauty.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), string.Concat(new string[]
				{
					"<spice.elements.",
					The.Player.GetMythicDomain(5, null),
					".weddingConditions.!random.capitalize>, =name= cemented ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" friendship with ",
					Object.GetPrimaryFactionName(true, true, true),
					" by annulling ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" marriage to ",
					Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), null, "general", MuralCategory.HasInspiringExperience, MuralWeight.High, null, -1L, true);
			}
		}

		// Token: 0x06008794 RID: 34708 RVA: 0x0030A668 File Offset: 0x00308868
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift(base.Object, "Intelligence", -this.Penalty, false);
			base.StatShifter.SetStatShift(base.Object, "Willpower", -this.Penalty, false);
			base.StatShifter.SetStatShift(base.Object, "MoveSpeed", this.SpeedPenalty, false);
		}

		// Token: 0x06008795 RID: 34709 RVA: 0x0030A6D1 File Offset: 0x003088D1
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
		}

		// Token: 0x06008796 RID: 34710 RVA: 0x0030A6E4 File Offset: 0x003088E4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06008797 RID: 34711 RVA: 0x0030A6FA File Offset: 0x003088FA
		public override bool HandleEvent(EndTurnEvent E)
		{
			GameObject.Validate(ref this.PreviousLeader);
			return base.HandleEvent(E);
		}

		// Token: 0x06008798 RID: 34712 RVA: 0x0030A70F File Offset: 0x0030890F
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008799 RID: 34713 RVA: 0x0030A72F File Offset: 0x0030892F
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600879A RID: 34714 RVA: 0x0030A76C File Offset: 0x0030896C
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 10)
				{
					E.Tile = null;
					E.RenderString = "\u0003";
					if (this.SecondDuration > 0)
					{
						E.ColorString = "&R";
					}
					else
					{
						E.ColorString = "&R";
					}
				}
			}
			return true;
		}

		// Token: 0x04003A20 RID: 14880
		private int Penalty = 6;

		// Token: 0x04003A21 RID: 14881
		private int SpeedPenalty = 5;

		// Token: 0x04003A22 RID: 14882
		private int SecondDuration;

		// Token: 0x04003A23 RID: 14883
		public GameObject Beauty;

		// Token: 0x04003A24 RID: 14884
		public GameObject PreviousLeader;
	}
}
