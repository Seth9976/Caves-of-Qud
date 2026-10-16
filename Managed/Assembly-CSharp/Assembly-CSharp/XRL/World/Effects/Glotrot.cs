using System;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.Conversations;
using XRL.World.Conversations.Parts;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D23 RID: 3363
	[Serializable]
	public class Glotrot : Effect, ITierInitialized
	{
		// Token: 0x06008482 RID: 33922 RVA: 0x002FDF97 File Offset: 0x002FC197
		public Glotrot()
		{
			this.DisplayName = "glotrot";
		}

		// Token: 0x06008483 RID: 33923 RVA: 0x002FDFB9 File Offset: 0x002FC1B9
		public override string GetDetails()
		{
			if (this.Stage < 3)
			{
				return "Tongue is rotting away.\nStarts bleeding when eating or drinking.";
			}
			return "Tongue has rotted away.\nCan't speak.";
		}

		// Token: 0x06008484 RID: 33924 RVA: 0x002FDFCF File Offset: 0x002FC1CF
		public override int GetEffectType()
		{
			return 100679700;
		}

		// Token: 0x06008485 RID: 33925 RVA: 0x002FDFD6 File Offset: 0x002FC1D6
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008486 RID: 33926 RVA: 0x002FDFDC File Offset: 0x002FC1DC
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Glotrot>())
			{
				return false;
			}
			if (Object.FireEvent("ApplyDisease") && ApplyEffectEvent.Check(Object, "Disease", this, null) && Object.FireEvent("ApplyGlotrot") && ApplyEffectEvent.Check(Object, "Glotrot", this, null))
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_diseased", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (Object.IsPlayer())
				{
					int num = Stat.Random(2, 6);
					Achievement.GET_GLOTROT.Unlock();
					Popup.Show("You have contracted glotrot! Your tongue begins to bleed as the muscle rots away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					JournalAPI.AddAccomplishment("You contracted glotrot.", "Woe to the scroundrels and dastards who conspired to have =name= contract the rotting tongue!", string.Format("Near the location of Golgotha, =name= was captured by bandits. {0} languished in captivity for {1} years, eventually contracting glotrot before escaping to {2}.", The.Player.GetPronounProvider(false).CapitalizedSubjective, num, The.Player.CurrentZone.GetTerrainDisplayName(true)), null, "general", MuralCategory.BodyExperienceBad, MuralWeight.Medium, null, -1L, true);
					Object.ApplyEffect(new Bleeding("1", 25, Object, true, false, false, false), null);
					this.AskPulldown();
				}
				this.Duration = 1;
				this.Stage = 1;
				return true;
			}
			return false;
		}

		// Token: 0x06008487 RID: 33927 RVA: 0x002FE112 File Offset: 0x002FC312
		public override string GetDescription()
		{
			if (this.DrankIck > 0)
			{
				return null;
			}
			return "{{glotrot|glotrot}}";
		}

		// Token: 0x06008488 RID: 33928 RVA: 0x002FE124 File Offset: 0x002FC324
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AnyRegenerableLimbsEvent.ID || ID == BeginConversationEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetTradePerformanceEvent>.ID || ID == RegenerateLimbEvent.ID;
		}

		// Token: 0x06008489 RID: 33929 RVA: 0x002FE15A File Offset: 0x002FC35A
		public override bool HandleEvent(AnyRegenerableLimbsEvent E)
		{
			return (!E.IncludeMinor || ((this.Stage < 3 || !E.Voluntary) && this.DrankIck <= 0)) && base.HandleEvent(E);
		}

		// Token: 0x0600848A RID: 33930 RVA: 0x002FE187 File Offset: 0x002FC387
		public override bool HandleEvent(RegenerateLimbEvent E)
		{
			if (E.IncludeMinor && ((this.Stage >= 3 && E.Voluntary) || this.DrankIck > 0))
			{
				this.RegrowTongue();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600848B RID: 33931 RVA: 0x002FE1B8 File Offset: 0x002FC3B8
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.AdvanceGlotrot(1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600848C RID: 33932 RVA: 0x002FE1C8 File Offset: 0x002FC3C8
		public override bool HandleEvent(GetTradePerformanceEvent E)
		{
			E.LinearAdjustment -= 3.0;
			return base.HandleEvent(E);
		}

		// Token: 0x0600848D RID: 33933 RVA: 0x002FE1E8 File Offset: 0x002FC3E8
		public override bool HandleEvent(BeginConversationEvent E)
		{
			if (base.Object.IsPlayer() && this.Stage >= 3 && !base.Object.HasPart<Telepathy>())
			{
				Conversation currentConversation = ConversationUI.CurrentConversation;
				if (currentConversation != null)
				{
					currentConversation.AddPart(new GlotrotFilter
					{
						Propagation = 1
					}, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600848E RID: 33934 RVA: 0x002FE23C File Offset: 0x002FC43C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDrank");
			Registrar.Register("DrinkingFrom");
			Registrar.Register("Eating");
			Registrar.Register("EndTurn");
			Registrar.Register("Regenera");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600848F RID: 33935 RVA: 0x002FE288 File Offset: 0x002FC488
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Regenera" && E.GetIntParameter("Level", 0) >= 5 && (!E.HasIntParameter("Involuntary") || this.DrankIck > 0))
			{
				this.RegrowTongue();
			}
			else if (E.ID == "AfterDrank" || E.ID == "Eating")
			{
				if (this.Stage < 3 && !E.HasFlag("External") && The.Game.Turns != this.TearTurn)
				{
					if (base.Object.IsPlayer())
					{
						Popup.ShowBlock("You tear open the tender muscle fibers of your tongue.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					base.Object.ApplyEffect(new Bleeding("1", 25, base.Object, true, false, false, false), null);
					this.AskPulldown();
					this.TearTurn = The.Game.Turns;
				}
			}
			else if (E.ID == "DrinkingFrom")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Container");
				if (Glotrot.IsFlamingIck(gameObjectParameter))
				{
					this.DrankIck = 100;
					if (base.Object.IsPlayer())
					{
						Popup.Show("It tastes even worse than you had imagined -- like a dead turtle boiled in phlegm.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				else if (25.in100())
				{
					int num = 4;
					LiquidVolume liquidVolume = gameObjectParameter.LiquidVolume;
					int num2 = Math.Min(num, (liquidVolume != null) ? liquidVolume.Volume : 0);
					if (num2 > 0)
					{
						LiquidVolume liquidVolume2 = gameObjectParameter.LiquidVolume;
						if (liquidVolume2 != null)
						{
							liquidVolume2.UseDrams(num2);
						}
					}
					Cell currentCell = base.Object.CurrentCell;
					if (currentCell != null && !currentCell.OnWorldMap())
					{
						GameObject gameObject = GameObject.Create("Water");
						LiquidVolume liquidVolume3 = gameObject.LiquidVolume;
						liquidVolume3.InitialLiquid = "water-1000";
						liquidVolume3.Volume = num2;
						liquidVolume3.AddDrams("putrid", 1);
						currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008490 RID: 33936 RVA: 0x002FE48C File Offset: 0x002FC68C
		public void RegrowTongue()
		{
			if (this.Duration <= 0)
			{
				return;
			}
			this.Stage = 1;
			if (this.DrankIck > 0)
			{
				if (base.Object.IsPlayer())
				{
					Achievement.CURE_GLOTROT.Unlock();
					Popup.ShowBlock("You are cured of glotrot. Your tongue regrows.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					JournalAPI.AddAccomplishment("You were cured of glotrot and your tongue regrew.", string.Concat(new string[]
					{
						"Blessed was the ",
						Calendar.GetDay(),
						" of ",
						Calendar.GetMonth(),
						", in the year of ",
						Calendar.GetYear().ToString(),
						" AR, when =name= was cured of the rotting tongue!"
					}), string.Concat(new string[]
					{
						"Some time in =year=, =name= came upon the bandits who had captured ",
						The.Player.GetPronounProvider(false).Objective,
						" and let ",
						The.Player.GetPronounProvider(false).Objective,
						" languish in captivity until ",
						The.Player.GetPronounProvider(false).Subjective,
						" contracted glotrot. =name= murdered their leader <spice.elements.",
						The.Player.GetMythicDomain(5, null),
						".murdermethods.!random> and cured ",
						The.Player.GetPronounProvider(false).Reflexive,
						" of the disease."
					}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Medium, null, -1L, true);
				}
				this.Duration = 0;
				this.Stage = 0;
				return;
			}
			if (base.Object.IsPlayer())
			{
				Popup.Show("Your tongue regrows.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment("Your tongue regrew.", string.Concat(new string[]
				{
					"Remember the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", in the year of ",
					Calendar.GetYear().ToString(),
					" AR, when through sheer strength of will =name= regrew ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" tongue."
				}), string.Concat(new string[]
				{
					"One auspicious eve while exploring the chutes of Golgotha, =name= dreamed that ",
					The.Player.GetPronounProvider(false).Subjective,
					" contracted a rotting disease and that ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" tongue festered and fell out. From then on, =name= vowed to always keep ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" tongue on his person."
				}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Medium, null, -1L, true);
			}
		}

		// Token: 0x06008491 RID: 33937 RVA: 0x002FE6FC File Offset: 0x002FC8FC
		public void AdvanceGlotrot(int Amount)
		{
			if (this.DrankIck > 0)
			{
				return;
			}
			this.Count += Amount;
			if (this.Count >= 1200 && this.Stage < 3)
			{
				this.Stage++;
				if (this.Stage == 2)
				{
					if (base.Object.IsPlayer())
					{
						Popup.ShowBlock("Your tongue begins to bleed as the muscle rots away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					base.Object.ApplyEffect(new Bleeding("1", 25, base.Object, true, false, false, false), null);
					this.AskPulldown();
				}
				if (this.Stage == 3)
				{
					if (base.Object.IsPlayer())
					{
						Popup.ShowBlock("Your tongue has rotted away.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						JournalAPI.AddAccomplishment("Your tongue rotted away.", string.Concat(new string[]
						{
							"On ",
							Calendar.GetDay(),
							" of ",
							Calendar.GetMonth(),
							", in the year of ",
							Calendar.GetYear().ToString(),
							" AR, =name= took a vow of silence and removed ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" own tongue."
						}), string.Concat(new string[]
						{
							"One auspicious eve while exploring the chutes of Golgotha, =name= dreamed that ",
							The.Player.GetPronounProvider(false).Subjective,
							" contracted a rotting disease and that ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" tongue festered and fell out. From then on, =name= vowed to always keep ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" tongue on his person."
						}), null, "general", MuralCategory.BodyExperienceBad, MuralWeight.Medium, null, -1L, true);
					}
					base.Object.ApplyEffect(new Bleeding("1", 25, base.Object, true, false, false, false), null);
					this.AskPulldown();
				}
			}
		}

		// Token: 0x06008492 RID: 33938 RVA: 0x002FE8DD File Offset: 0x002FCADD
		public void AskPulldown()
		{
			if (base.Object.OnWorldMap() && Popup.ShowYesNo("Do you want to stop travelling?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
			{
				base.Object.PullDown(false);
			}
		}

		// Token: 0x06008493 RID: 33939 RVA: 0x002FE90C File Offset: 0x002FCB0C
		public static bool IsIck(LiquidVolume Volume)
		{
			if (Volume == null)
			{
				return false;
			}
			if (Volume.ComponentLiquids.Count < 3)
			{
				return false;
			}
			if (Volume.IsOpenVolume())
			{
				return false;
			}
			for (int i = 1; i <= 3; i++)
			{
				string stringGameState = The.Game.GetStringGameState("GlotrotCure" + i.ToString(), "");
				if (!Volume.HasLiquid(stringGameState))
				{
					return false;
				}
				if (i == 3 && Volume.GetPrimaryLiquidID() != stringGameState)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008494 RID: 33940 RVA: 0x002FE985 File Offset: 0x002FCB85
		public static bool IsIck(GameObject Object)
		{
			return GameObject.Validate(ref Object) && Glotrot.IsIck(Object.LiquidVolume);
		}

		// Token: 0x06008495 RID: 33941 RVA: 0x002FE99D File Offset: 0x002FCB9D
		public static bool IsFlamingIck(GameObject Object)
		{
			return GameObject.Validate(ref Object) && Object.IsAflame() && Glotrot.IsIck(Object);
		}

		// Token: 0x04003984 RID: 14724
		public int Stage = 1;

		// Token: 0x04003985 RID: 14725
		public int Count;

		// Token: 0x04003986 RID: 14726
		public int DrankIck;

		// Token: 0x04003987 RID: 14727
		[NonSerialized]
		private long TearTurn = -1L;
	}
}
