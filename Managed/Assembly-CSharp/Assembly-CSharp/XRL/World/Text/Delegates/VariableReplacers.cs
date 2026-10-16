using System;
using System.Collections.Generic;
using System.Reflection;
using ConsoleLib.Console;
using HistoryKit;
using Qud.API;
using XRL.Collections;
using XRL.Language;
using XRL.Wish;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Text.Attributes;

namespace XRL.World.Text.Delegates
{
	// Token: 0x02001467 RID: 5223
	[HasModSensitiveStaticCache]
	[HasVariableReplacer]
	[HasWishCommand]
	public static class VariableReplacers
	{
		// Token: 0x0600CD30 RID: 52528 RVA: 0x004A5E6C File Offset: 0x004A406C
		[VariableObjectReplacer]
		public static string Subjective(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.it : null)) == null)
				{
					return Context.Pronouns.Subjective;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.It : null) ?? Context.Pronouns.CapitalizedSubjective;
			}
			return text;
		}

		// Token: 0x0600CD31 RID: 52529 RVA: 0x004A5EC4 File Offset: 0x004A40C4
		[VariableObjectReplacer]
		public static string Objective(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.them : null)) == null)
				{
					return Context.Pronouns.Objective;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.Them : null) ?? Context.Pronouns.CapitalizedObjective;
			}
			return text;
		}

		// Token: 0x0600CD32 RID: 52530 RVA: 0x004A5F1C File Offset: 0x004A411C
		[VariableObjectReplacer(new string[] { "possessive", "possessiveAdjective" })]
		public static string Possessive(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.its : null)) == null)
				{
					return Context.Pronouns.PossessiveAdjective;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.Its : null) ?? Context.Pronouns.CapitalizedPossessiveAdjective;
			}
			return text;
		}

		// Token: 0x0600CD33 RID: 52531 RVA: 0x004A5F74 File Offset: 0x004A4174
		[VariableObjectReplacer]
		public static string SubstantivePossessive(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.theirs : null)) == null)
				{
					return Context.Pronouns.SubstantivePossessive;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.Theirs : null) ?? Context.Pronouns.CapitalizedSubstantivePossessive;
			}
			return text;
		}

		// Token: 0x0600CD34 RID: 52532 RVA: 0x004A5FCC File Offset: 0x004A41CC
		[VariableObjectReplacer]
		public static string Reflexive(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.itself : null)) == null)
				{
					return Context.Pronouns.Reflexive;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.Itself : null) ?? Context.Pronouns.CapitalizedReflexive;
			}
			return text;
		}

		// Token: 0x0600CD35 RID: 52533 RVA: 0x004A6024 File Offset: 0x004A4224
		[VariableObjectReplacer]
		public static string PersonTerm(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.personTerm : null)) == null)
				{
					return Context.Pronouns.PersonTerm;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.PersonTerm : null) ?? Context.Pronouns.CapitalizedPersonTerm;
			}
			return text;
		}

		// Token: 0x0600CD36 RID: 52534 RVA: 0x004A607C File Offset: 0x004A427C
		[VariableObjectReplacer]
		public static string ImmaturePersonTerm(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.immaturePersonTerm : null)) == null)
				{
					return Context.Pronouns.ImmaturePersonTerm;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.ImmaturePersonTerm : null) ?? Context.Pronouns.CapitalizedImmaturePersonTerm;
			}
			return text;
		}

		// Token: 0x0600CD37 RID: 52535 RVA: 0x004A60D4 File Offset: 0x004A42D4
		[VariableObjectReplacer]
		public static string FormalAddressTerm(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.formalAddressTerm : null)) == null)
				{
					return Context.Pronouns.FormalAddressTerm;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.FormalAddressTerm : null) ?? Context.Pronouns.CapitalizedFormalAddressTerm;
			}
			return text;
		}

		// Token: 0x0600CD38 RID: 52536 RVA: 0x004A612C File Offset: 0x004A432C
		[VariableObjectReplacer]
		public static string OffspringTerm(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.offspringTerm : null)) == null)
				{
					return Context.Pronouns.OffspringTerm;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.OffspringTerm : null) ?? Context.Pronouns.CapitalizedOffspringTerm;
			}
			return text;
		}

		// Token: 0x0600CD39 RID: 52537 RVA: 0x004A6184 File Offset: 0x004A4384
		[VariableObjectReplacer]
		public static string SiblingTerm(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.siblingTerm : null)) == null)
				{
					return Context.Pronouns.SiblingTerm;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.SiblingTerm : null) ?? Context.Pronouns.CapitalizedSiblingTerm;
			}
			return text;
		}

		// Token: 0x0600CD3A RID: 52538 RVA: 0x004A61DC File Offset: 0x004A43DC
		[VariableObjectReplacer]
		public static string ParentTerm(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.parentTerm : null)) == null)
				{
					return Context.Pronouns.ParentTerm;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.ParentTerm : null) ?? Context.Pronouns.CapitalizedParentTerm;
			}
			return text;
		}

		// Token: 0x0600CD3B RID: 52539 RVA: 0x004A6234 File Offset: 0x004A4434
		[VariableObjectReplacer]
		public static string IndicativeProximal(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.indicativeProximal : null)) == null)
				{
					return Context.Pronouns.IndicativeProximal;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.IndicativeProximal : null) ?? Context.Pronouns.CapitalizedIndicativeProximal;
			}
			return text;
		}

		// Token: 0x0600CD3C RID: 52540 RVA: 0x004A628C File Offset: 0x004A448C
		[VariableObjectReplacer]
		public static string IndicativeDistal(DelegateContext Context)
		{
			string text;
			if (!Context.Capitalize)
			{
				GameObject target = Context.Target;
				if ((text = ((target != null) ? target.indicativeDistal : null)) == null)
				{
					return Context.Pronouns.IndicativeDistal;
				}
			}
			else
			{
				GameObject target2 = Context.Target;
				text = ((target2 != null) ? target2.IndicativeDistal : null) ?? Context.Pronouns.CapitalizedIndicativeDistal;
			}
			return text;
		}

		// Token: 0x0600CD3D RID: 52541 RVA: 0x004A62E4 File Offset: 0x004A44E4
		[VariableReplacer(new string[] { "stringgamestate", "state.string" }, Default = "")]
		public static string StringGameState(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Parameters.Count > 0)
			{
				XRLGame game = The.Game;
				text = ((game != null) ? game.GetStringGameState(Context.Parameters[0], null) : null) ?? text;
				if (Context.Parameters.Count > 1 && text.IsNullOrEmpty())
				{
					text = Context.Parameters[1];
				}
			}
			return text;
		}

		// Token: 0x0600CD3E RID: 52542 RVA: 0x004A6350 File Offset: 0x004A4550
		[VariableReplacer(new string[] { "intgamestate", "state.int" }, Default = "0")]
		public static string Int32GameState(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Parameters.Count > 0 && The.Game != null)
			{
				int intGameState = The.Game.GetIntGameState(Context.Parameters[0], 0);
				text = ((Context.Parameters.Count > 1) ? VariableReplacers.NumeralTransform(Context.Parameters[1], intGameState) : intGameState.ToString());
			}
			return text;
		}

		// Token: 0x0600CD3F RID: 52543 RVA: 0x004A63BC File Offset: 0x004A45BC
		[VariableReplacer(new string[] { "int64gamestate", "state.long" }, Default = "0")]
		public static string Int64GameState(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Parameters.Count > 0 && The.Game != null)
			{
				long int64GameState = The.Game.GetInt64GameState(Context.Parameters[0], 0L);
				if (Context.Parameters.Count > 1)
				{
					text = ((Context.Parameters.Count > 1) ? VariableReplacers.NumeralTransform(Context.Parameters[1], int64GameState) : int64GameState.ToString());
				}
				else
				{
					text = int64GameState.ToString();
				}
			}
			return text;
		}

		// Token: 0x0600CD40 RID: 52544 RVA: 0x004A6444 File Offset: 0x004A4644
		[VariableReplacer(new string[] { "booleangamestate", "state.bool" }, Default = "false")]
		public static string BooleanGameState(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Parameters.Count > 0 && The.Game != null)
			{
				if (Context.Parameters.Count > 1)
				{
					string text2 = "true";
					string text3 = "false";
					bool flag = false;
					if (Context.Parameters.Count > 3)
					{
						flag = bool.TryParse(Context.Parameters[1], out flag);
						Context.Parameters.RemoveAt(1);
					}
					if (Context.Parameters.Count > 1)
					{
						text2 = Context.Parameters[1];
						text3 = ((Context.Parameters.Count > 2) ? Context.Parameters[2] : "");
					}
					text = (The.Game.GetBooleanGameState(Context.Parameters[0], flag) ? text2 : text3);
				}
				else
				{
					text = The.Game.GetBooleanGameState(Context.Parameters[0], false).ToString();
				}
			}
			return text;
		}

		// Token: 0x0600CD41 RID: 52545 RVA: 0x004A653F File Offset: 0x004A473F
		[VariableReplacer(new string[] { "MarkOfDeath" }, Default = "*MARK OF DEATH MISSING*")]
		public static string GameStateMarkOfDeath(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("MarkOfDeath", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD42 RID: 52546 RVA: 0x004A6566 File Offset: 0x004A4766
		[VariableReplacer(new string[] { "GR1" }, Default = "*GLOTROT CURE LIQUID 1 MISSING*")]
		public static string GameStateGlotrotCure1(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("GlotrotCure1", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD43 RID: 52547 RVA: 0x004A658D File Offset: 0x004A478D
		[VariableReplacer(new string[] { "GR2" }, Default = "*GLOTROT CURE LIQUID 2 MISSING*")]
		public static string GameStateGlotrotCure2(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("GlotrotCure2", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD44 RID: 52548 RVA: 0x004A65B4 File Offset: 0x004A47B4
		[VariableReplacer(new string[] { "GR3" }, Default = "*GLOTROT CURE LIQUID 3 MISSING*")]
		public static string GameStateGlotrotCure3(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("GlotrotCure3", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD45 RID: 52549 RVA: 0x004A65DB File Offset: 0x004A47DB
		[VariableReplacer(new string[] { "IS1" }, Default = "*IRONSHANK CURE MISSING*")]
		public static string GameStateIronshankCure(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("IronshankCure", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD46 RID: 52550 RVA: 0x004A6602 File Offset: 0x004A4802
		[VariableReplacer(new string[] { "MC1" }, Default = "*MONOCHROME CURE MISSING*")]
		public static string GameStateMonochromeCure(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("MonochromeCure", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD47 RID: 52551 RVA: 0x004A6629 File Offset: 0x004A4829
		[VariableReplacer(new string[] { "FCC" }, Default = "*FUNGAL CURE WORM MISSING*")]
		public static string GameStateFungalCureWorm(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("FungalCureWormDisplay", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD48 RID: 52552 RVA: 0x004A6650 File Offset: 0x004A4850
		[VariableReplacer(new string[] { "FCL" }, Default = "*FUNGAL CURE LIQUID MISSING*")]
		public static string GameStateFungalCureLiquid(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("FungalCureLiquidDisplay", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD49 RID: 52553 RVA: 0x004A6677 File Offset: 0x004A4877
		[VariableReplacer(new string[] { "EskhindRoadDirection" }, Default = "*ESKHIND ROAD DIRECTION MISSING*")]
		public static string GameStateEskhindRoadDirection(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("EskhindRoadDirection", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD4A RID: 52554 RVA: 0x004A669E File Offset: 0x004A489E
		[VariableReplacer(new string[] { "SEEKERENEMY" }, Default = "*SEEKER ENEMY FACTION MISSING*")]
		public static string GameStateSeekerEnemy(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("SeekerEnemyFaction", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD4B RID: 52555 RVA: 0x004A66C5 File Offset: 0x004A48C5
		[VariableReplacer(new string[] { "villageZeroName" }, Default = "*VILLAGE ZERO NAME MISSING*")]
		public static string GameStateVillageZeroName(DelegateContext Context)
		{
			XRLGame game = The.Game;
			return ((game != null) ? game.GetStringGameState("villageZeroName", "") : null) ?? Context.Default;
		}

		// Token: 0x0600CD4C RID: 52556 RVA: 0x004A66EC File Offset: 0x004A48EC
		[VariableReplacer(new string[] { "V0tinkeraddendum" }, Default = "*VILLAGE ZERO TINKER ADDENDUM MISSING*")]
		public static string GameStateVillageZeroTinkerAddendum(DelegateContext Context)
		{
			XRLGame game = The.Game;
			if (string.IsNullOrEmpty((game != null) ? game.GetStringGameState("villageZeroName", "") : null))
			{
				return " It's been live for over a year.";
			}
			return " A tinker must have detected it and then recorded it onto the data disk you brought us. However, we've known about since it went live over a year ago.";
		}

		// Token: 0x0600CD4D RID: 52557 RVA: 0x004A671C File Offset: 0x004A491C
		[VariableReplacer(new string[] { "RebekahRegion" }, Default = "*REBEKAH REGION MISSING*")]
		public static string GameStateRebekahRegion(DelegateContext Context)
		{
			HistoricEvent @event = HistoryAPI.GetResheph().GetEvent((HistoricEvent x) => x.HasEventProperty("rebekah") && x.HasEventProperty("region"), -1L);
			return ((@event != null) ? @event.GetEventProperty("region", null) : null) ?? Context.Default;
		}

		// Token: 0x0600CD4E RID: 52558 RVA: 0x004A6770 File Offset: 0x004A4970
		[VariableReplacer(new string[] { "MARKOVPARAGRAPH" })]
		public static string MarkovParagraph(DelegateContext Context)
		{
			return GameText.GenerateMarkovMessageParagraph();
		}

		// Token: 0x0600CD4F RID: 52559 RVA: 0x004A6777 File Offset: 0x004A4977
		[VariableReplacer(new string[] { "MARKOVSENTENCE" })]
		public static string MarkovSentence(DelegateContext Context)
		{
			return GameText.GenerateMarkovMessageSentence();
		}

		// Token: 0x0600CD50 RID: 52560 RVA: 0x004A677E File Offset: 0x004A497E
		[VariableReplacer(new string[] { "MARKOVCORVIDSENTENCE" })]
		public static string MarkovCorvidSentence(DelegateContext Context)
		{
			return TextFilters.Corvid(GameText.GenerateMarkovMessageSentence());
		}

		// Token: 0x0600CD51 RID: 52561 RVA: 0x004A678A File Offset: 0x004A498A
		[VariableReplacer(new string[] { "MARKOVWATERBIRDSENTENCE" })]
		public static string MarkovWaterBirdSentence(DelegateContext Context)
		{
			return TextFilters.WaterBird(GameText.GenerateMarkovMessageSentence());
		}

		// Token: 0x0600CD52 RID: 52562 RVA: 0x004A6796 File Offset: 0x004A4996
		[VariableReplacer(new string[] { "MARKOVFISHSENTENCE" })]
		public static string MarkovFishSentence(DelegateContext Context)
		{
			return TextFilters.Fish(GameText.GenerateMarkovMessageSentence());
		}

		// Token: 0x0600CD53 RID: 52563 RVA: 0x004A67A2 File Offset: 0x004A49A2
		[VariableReplacer(new string[] { "WEIRDMARKOVSENTENCE" })]
		public static string MarkovWeirdSentence(DelegateContext Context)
		{
			return Grammar.Weirdify(GameText.GenerateMarkovMessageSentence(), 100);
		}

		// Token: 0x0600CD54 RID: 52564 RVA: 0x004A67B0 File Offset: 0x004A49B0
		private static string NumeralTransform(string Command, int Value)
		{
			string text;
			if (!(Command == "cardinal"))
			{
				if (!(Command == "ordinal"))
				{
					if (!(Command == "roman"))
					{
						if (!(Command == "multiplicative"))
						{
							text = Value.ToString();
						}
						else
						{
							text = Grammar.Multiplicative(Value);
						}
					}
					else
					{
						text = Grammar.GetRomanNumeral(Value);
					}
				}
				else
				{
					text = Grammar.Ordinal(Value);
				}
			}
			else
			{
				text = Grammar.Cardinal(Value);
			}
			return text;
		}

		// Token: 0x0600CD55 RID: 52565 RVA: 0x004A6820 File Offset: 0x004A4A20
		private static string NumeralTransform(string Command, long Value)
		{
			string text;
			if (!(Command == "cardinal"))
			{
				if (!(Command == "ordinal"))
				{
					if (!(Command == "roman"))
					{
						if (!(Command == "multiplicative"))
						{
							text = Value.ToString();
						}
						else
						{
							text = Grammar.Multiplicative(Value);
						}
					}
					else
					{
						text = Grammar.GetRomanNumeral(Value);
					}
				}
				else
				{
					text = Grammar.Ordinal(Value);
				}
			}
			else
			{
				text = Grammar.Cardinal(Value);
			}
			return text;
		}

		// Token: 0x0600CD56 RID: 52566 RVA: 0x004A6890 File Offset: 0x004A4A90
		[VariableReplacer]
		public unsafe static string Year(DelegateContext Context)
		{
			int year = Calendar.GetYear();
			if (Context.Parameters.Contains("noera"))
			{
				return year.ToString();
			}
			int num = 0;
			int num2 = year.CountDigits() + 3;
			Span<char> span;
			checked
			{
				span = new Span<char>(stackalloc byte[unchecked((UIntPtr)num2) * 2], num2);
				(ref span).Insert(ref num, year);
			}
			*span[num++] = ' ';
			*span[num++] = 'A';
			*span[num] = 'R';
			return new string(span);
		}

		// Token: 0x0600CD57 RID: 52567 RVA: 0x004A6913 File Offset: 0x004A4B13
		[VariableReplacer]
		public static string Month(DelegateContext Context)
		{
			return Calendar.GetMonth();
		}

		// Token: 0x0600CD58 RID: 52568 RVA: 0x004A691A File Offset: 0x004A4B1A
		[VariableReplacer]
		public static string Day(DelegateContext Context)
		{
			return Calendar.GetDay();
		}

		// Token: 0x0600CD59 RID: 52569 RVA: 0x004A6921 File Offset: 0x004A4B21
		[VariableReplacer]
		public static string Time(DelegateContext Context)
		{
			return Calendar.GetTime();
		}

		// Token: 0x0600CD5A RID: 52570 RVA: 0x004A6928 File Offset: 0x004A4B28
		private static string GetFragment(GameObject Target, string Key, string Default)
		{
			Dictionary<string, string> dictionary;
			string text;
			if (Target != null && Target.GetBlueprint(true).xTags.TryGetValue("TextFragments", out dictionary) && dictionary.TryGetValue(Key, out text) && !text.IsNullOrEmpty())
			{
				return text.GetRandomSubstring(',', false, null);
			}
			return Default.GetRandomSubstring(',', false, null);
		}

		// Token: 0x0600CD5B RID: 52571 RVA: 0x004A697A File Offset: 0x004A4B7A
		[VariableObjectReplacer(new string[] { "fragment.poetic" }, Capitalization = false)]
		public static string FragmentPoetic(DelegateContext Context)
		{
			return VariableReplacers.GetFragment(Context.Target, "PoeticFeatures", VariableReplacers.DefaultPoeticFeatures);
		}

		// Token: 0x0600CD5C RID: 52572 RVA: 0x004A6991 File Offset: 0x004A4B91
		[VariableObjectReplacer(new string[] { "fragment.activity" }, Capitalization = false)]
		public static string FragmentActivity(DelegateContext Context)
		{
			return VariableReplacers.GetFragment(Context.Target, "Activity", VariableReplacers.DefaultActivities);
		}

		// Token: 0x0600CD5D RID: 52573 RVA: 0x004A69A8 File Offset: 0x004A4BA8
		[VariableObjectReplacer(new string[] { "fragment.village" }, Capitalization = false)]
		public static string FragmentVillage(DelegateContext Context)
		{
			return VariableReplacers.GetFragment(Context.Target, "VillageActivity", VariableReplacers.DefaultVillageActivities);
		}

		// Token: 0x0600CD5E RID: 52574 RVA: 0x004A69BF File Offset: 0x004A4BBF
		[VariableObjectReplacer(new string[] { "fragment.sacred" }, Capitalization = false)]
		public static string FragmentSacred(DelegateContext Context)
		{
			return VariableReplacers.GetFragment(Context.Target, "SacredThing", VariableReplacers.DefaultSacredThings);
		}

		// Token: 0x0600CD5F RID: 52575 RVA: 0x004A69D6 File Offset: 0x004A4BD6
		[VariableObjectReplacer(new string[] { "fragment.arable" }, Capitalization = false)]
		public static string FragmentArable(DelegateContext Context)
		{
			return VariableReplacers.GetFragment(Context.Target, "ArableLand", VariableReplacers.DefaultArableLands);
		}

		// Token: 0x0600CD60 RID: 52576 RVA: 0x004A69ED File Offset: 0x004A4BED
		[VariableObjectReplacer(new string[] { "fragment.ore" }, Capitalization = false)]
		public static string FragmentOre(DelegateContext Context)
		{
			return VariableReplacers.GetFragment(Context.Target, "ValuedOre", VariableReplacers.DefaultValuedOres);
		}

		// Token: 0x0600CD61 RID: 52577 RVA: 0x004A6A04 File Offset: 0x004A4C04
		[VariableObjectReplacer(new string[] { "fragment" }, Capitalization = false)]
		public static string Fragment(DelegateContext Context)
		{
			string text = VariableReplacers.DefaultSacredThings;
			if (Context.Parameters.Count > 0)
			{
				if (Context.Parameters.Count > 1)
				{
					text = Context.Parameters[1];
				}
				text = VariableReplacers.GetFragment(Context.Target, Context.Parameters[0], text);
			}
			return text;
		}

		// Token: 0x0600CD62 RID: 52578 RVA: 0x004A6A5A File Offset: 0x004A4C5A
		[VariableReplacer(new string[] { "EitherOrWhisper" })]
		public static string EitherOrWhisper(DelegateContext Context)
		{
			return PetEitherOr.GetEitherOrAccomplishment();
		}

		// Token: 0x0600CD63 RID: 52579 RVA: 0x004A6A64 File Offset: 0x004A4C64
		[VariableReplacer(new string[] { "sultan" })]
		public static string SultanName(DelegateContext Context)
		{
			int num;
			if (Context.Parameters.Count == 0 || !int.TryParse(Context.Parameters[0], out num))
			{
				return "a sultan";
			}
			HistoricEntitySnapshot sultanForPeriod = HistoryAPI.GetSultanForPeriod(num);
			return ((sultanForPeriod != null) ? sultanForPeriod.GetProperty("name", "unknown") : null) ?? ("the " + Grammar.Ordinal(num) + " sultan");
		}

		// Token: 0x0600CD64 RID: 52580 RVA: 0x004A6AD0 File Offset: 0x004A4CD0
		[VariableReplacer(Capitalization = true)]
		public static string SultanTerm(DelegateContext Context)
		{
			string text = HistoryAPI.GetSultanTerm();
			if (Context.Capitalize)
			{
				text = Grammar.InitCap(text);
			}
			return text;
		}

		// Token: 0x0600CD65 RID: 52581 RVA: 0x004A6AF4 File Offset: 0x004A4CF4
		[VariableReplacer(new string[] { "generic" })]
		public static string SimpleConversation(DelegateContext Context)
		{
			GameObject target = Context.Target;
			string text = ((target != null) ? target.GetPropertyOrTag("SimpleConversation", null) : null);
			if (text != null)
			{
				return text.GetRandomSubstring('~', false, null);
			}
			return "";
		}

		// Token: 0x0600CD66 RID: 52582 RVA: 0x004A6B30 File Offset: 0x004A4D30
		[VariableObjectReplacer(Default = "do", Capitalization = false)]
		public static string Verb(DelegateContext Context)
		{
			string text = Context.Default;
			bool flag = Context.Parameters.Count >= 2 && Context.Parameters[1] == "afterpronoun";
			if (Context.Parameters.Count >= 1)
			{
				text = Context.Parameters[0];
			}
			if (Context.Target != null)
			{
				return Context.Target.GetVerb(text, false, flag, false, true, false);
			}
			if (Context.Explicit == null)
			{
				return text;
			}
			if (Context.Pronouns != null)
			{
				if (Context.Pronouns.Plural)
				{
					return text;
				}
				if (Context.Pronouns.PseudoPlural && flag)
				{
					return text;
				}
			}
			return Grammar.ThirdPerson(text, false);
		}

		// Token: 0x0600CD67 RID: 52583 RVA: 0x004A6BDC File Offset: 0x004A4DDC
		[VariableObjectReplacer(Default = "the thing does")]
		public static string Does(DelegateContext Context)
		{
			string text = ((Context.Parameters.Count >= 1) ? Context.Parameters[0] : "do");
			if (Context.Target != null)
			{
				if (!Context.Capitalize)
				{
					return Context.Target.does(text, int.MaxValue, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false);
				}
				return Context.Target.Does(text, int.MaxValue, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false);
			}
			else
			{
				if (Context.Explicit != null)
				{
					return Context.Value.Append(Context.Capitalize ? "The " : "the ").Append(Context.Explicit).Compound(Grammar.ThirdPerson(text, false))
						.ToString();
				}
				return Context.Default;
			}
		}

		// Token: 0x0600CD68 RID: 52584 RVA: 0x004A6CC4 File Offset: 0x004A4EC4
		[VariableObjectReplacer(Default = "creature")]
		public static string Species(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Target != null)
			{
				text = Context.Target.GetSpecies();
				if (Context.Capitalize)
				{
					text.Capitalize();
				}
			}
			return text;
		}

		// Token: 0x0600CD69 RID: 52585 RVA: 0x004A6CFC File Offset: 0x004A4EFC
		[VariableObjectReplacer(Default = "creature")]
		public static string ApparentSpecies(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Target != null)
			{
				text = Context.Target.GetApparentSpecies(null);
				if (Context.Capitalize)
				{
					text.Capitalize();
				}
			}
			return text;
		}

		// Token: 0x0600CD6A RID: 52586 RVA: 0x004A6D38 File Offset: 0x004A4F38
		[VariableObjectReplacer(new string[] { "t" }, Default = "the thing")]
		public static string WithDefiniteArticle(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Context.Target.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, false, true, null, false, Context.Capitalize, false, false, null, false, null, false, true);
				}
				if (!Context.Capitalize)
				{
					return "you";
				}
				return "You";
			}
			else
			{
				if (Context.Explicit != null)
				{
					return (Context.Capitalize ? "The " : "the ") + Context.Explicit;
				}
				return Context.Default;
			}
		}

		// Token: 0x0600CD6B RID: 52587 RVA: 0x004A6DDC File Offset: 0x004A4FDC
		[VariableObjectReplacer(new string[] { "t's" }, Default = "the thing's")]
		public static string PossessiveWithDefiniteArticle(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Grammar.MakePossessive(Context.Target.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, false, true, null, false, Context.Capitalize, false, false, null, false, null, false, true));
				}
				if (!Context.Capitalize)
				{
					return "your";
				}
				return "Your";
			}
			else
			{
				if (Context.Explicit != null)
				{
					return (Context.Capitalize ? "The " : "the ") + Grammar.MakePossessive(Context.Explicit);
				}
				return Context.Default;
			}
		}

		// Token: 0x0600CD6C RID: 52588 RVA: 0x004A6E88 File Offset: 0x004A5088
		[VariableObjectReplacer(new string[] { "faction" }, Default = "faction")]
		public static string FactionName(DelegateContext Context)
		{
			GameObject target = Context.Target;
			Faction ifExists = Factions.GetIfExists((target != null) ? target.GetPrimaryFaction(false) : null);
			string text = ((ifExists != null) ? ifExists.DisplayName : null);
			if (!text.IsNullOrEmpty())
			{
				return text;
			}
			if (Context.Parameters.Count > 0)
			{
				return Context.Parameters[0];
			}
			return Context.Default;
		}

		// Token: 0x0600CD6D RID: 52589 RVA: 0x004A6EE8 File Offset: 0x004A50E8
		[VariableObjectReplacer(new string[] { "faction.t" }, Default = "the faction")]
		public static string TheFactionName(DelegateContext Context)
		{
			GameObject target = Context.Target;
			Faction ifExists = Factions.GetIfExists((target != null) ? target.GetPrimaryFaction(false) : null);
			string text = ((ifExists != null) ? ifExists.GetFormattedName() : null);
			if (!text.IsNullOrEmpty())
			{
				return text;
			}
			if (Context.Parameters.Count > 0)
			{
				return Context.Parameters[0];
			}
			return Context.Default;
		}

		// Token: 0x0600CD6E RID: 52590 RVA: 0x004A6F45 File Offset: 0x004A5145
		[VariableReplacer(new string[] { "mostHatedFaction" })]
		public static string MostHatedFactionName(DelegateContext Context)
		{
			return Factions.GetMostHated(true).DisplayName;
		}

		// Token: 0x0600CD6F RID: 52591 RVA: 0x004A6F52 File Offset: 0x004A5152
		[VariableReplacer(new string[] { "mostHatedFaction.t" })]
		public static string TheMostHatedFactionName(DelegateContext Context)
		{
			return Factions.GetMostHated(true).GetFormattedName();
		}

		// Token: 0x0600CD70 RID: 52592 RVA: 0x004A6F5F File Offset: 0x004A515F
		[VariableReplacer(new string[] { "secondMostHatedFaction" })]
		public static string MostSecondHatedFactionName(DelegateContext Context)
		{
			return Factions.GetSecondMostHated(true).DisplayName;
		}

		// Token: 0x0600CD71 RID: 52593 RVA: 0x004A6F6C File Offset: 0x004A516C
		[VariableReplacer(new string[] { "secondMostHatedFaction.t" })]
		public static string TheSecondMostHatedFactionName(DelegateContext Context)
		{
			return Factions.GetSecondMostHated(true).GetFormattedName();
		}

		// Token: 0x0600CD72 RID: 52594 RVA: 0x004A6F7C File Offset: 0x004A517C
		[VariableObjectReplacer(Default = "water")]
		public static string WaterRitualLiquid(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Target != null)
			{
				text = Context.Target.GetWaterRitualLiquidName(null);
				if (Context.Capitalize)
				{
					text = text.Capitalize();
				}
			}
			return ColorUtility.StripFormatting(text);
		}

		// Token: 0x0600CD73 RID: 52595 RVA: 0x004A6FBC File Offset: 0x004A51BC
		[VariableObjectReplacer(Default = "somewhere")]
		public static string Direction(DelegateContext Context)
		{
			string text = Context.Default;
			if (The.Player != null && Context.Target != null)
			{
				text = The.Player.DescribeDirectionToward(Context.Target, false, false);
				if (Context.Capitalize)
				{
					text = text.Capitalize();
				}
			}
			return text;
		}

		// Token: 0x0600CD74 RID: 52596 RVA: 0x004A7004 File Offset: 0x004A5204
		[VariableObjectReplacer]
		public static string DirectionIfAny(DelegateContext Context)
		{
			if (Context.Target == null || The.Player == null)
			{
				return "";
			}
			string text = The.Player.DescribeDirectionToward(Context.Target, false, false);
			if (text.IsNullOrEmpty() || text == "here")
			{
				return "";
			}
			if (Context.Capitalize)
			{
				text = text.Capitalize();
			}
			return " " + text;
		}

		// Token: 0x0600CD75 RID: 52597 RVA: 0x004A7070 File Offset: 0x004A5270
		[VariableObjectReplacer(new string[] { "an" }, Default = "a thing")]
		public static string WithIndefiniteArticle(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Context.Target.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, true, false, null, false, Context.Capitalize, false, false, null, false, null, false, true);
				}
				if (!Context.Capitalize)
				{
					return "you";
				}
				return "You";
			}
			else
			{
				if (Context.Explicit == null)
				{
					return Context.Default;
				}
				return Grammar.A(Context.Explicit, Context.Capitalize);
			}
		}

		// Token: 0x0600CD76 RID: 52598 RVA: 0x004A7104 File Offset: 0x004A5304
		[VariableObjectReplacer(new string[] { "an's" }, Default = "a thing's")]
		public static string PossessiveWithIndefiniteArticle(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Grammar.MakePossessive(Context.Target.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, true, false, null, false, Context.Capitalize, false, false, null, false, null, false, true));
				}
				if (!Context.Capitalize)
				{
					return "your";
				}
				return "Your";
			}
			else
			{
				if (Context.Explicit == null)
				{
					return Context.Default;
				}
				return Grammar.MakePossessive(Grammar.A(Context.Explicit, Context.Capitalize));
			}
		}

		// Token: 0x0600CD77 RID: 52599 RVA: 0x004A71A2 File Offset: 0x004A53A2
		[VariableObjectReplacer(new string[] { "the" }, Default = "the ")]
		public static string DefiniteArticle(DelegateContext Context)
		{
			if (Context.Target == null)
			{
				return Context.Default;
			}
			return Context.Target.DefiniteArticle(Context.Capitalize, null, null, false, false);
		}

		// Token: 0x0600CD78 RID: 52600 RVA: 0x004A71C8 File Offset: 0x004A53C8
		[VariableObjectReplacer(new string[] { "a" }, Default = "a ")]
		public static string IndefiniteArticle(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				return Context.Target.IndefiniteArticle(Context.Capitalize, null, false, false);
			}
			if (!Grammar.IndefiniteArticleShouldBeAn(Context.Explicit))
			{
				return Context.Default;
			}
			if (!Context.Capitalize)
			{
				return "an ";
			}
			return "An ";
		}

		// Token: 0x0600CD79 RID: 52601 RVA: 0x004A721C File Offset: 0x004A541C
		[VariableObjectReplacer(new string[] { "name" }, Default = "thing")]
		public static string TargetName(DelegateContext Context)
		{
			if (Context.Type == TargetType.None)
			{
				XRLGame game = The.Game;
				return ((game != null) ? game.PlayerName : null) ?? "";
			}
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Context.Target.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, true, false, true, false, false, false, null, false, Context.Capitalize, false, false, null, false, null, false, true);
				}
				if (!Context.Capitalize)
				{
					return "you";
				}
				return "You";
			}
			else
			{
				if (Context.Explicit == null)
				{
					return Context.Default;
				}
				if (!Context.Capitalize)
				{
					return Context.Explicit;
				}
				return Context.Explicit.Capitalize();
			}
		}

		// Token: 0x0600CD7A RID: 52602 RVA: 0x004A72DC File Offset: 0x004A54DC
		[VariableObjectReplacer(new string[] { "name's" }, Default = "thing's")]
		public static string TargetNamePossessive(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Grammar.MakePossessive(Context.Target.GetDisplayName(int.MaxValue, null, null, false, true, false, false, true, false, true, true, false, true, false, false, false, null, false, Context.Capitalize, false, false, null, false, null, false, true));
				}
				if (!Context.Capitalize)
				{
					return "your";
				}
				return "Your";
			}
			else
			{
				if (Context.Explicit != null)
				{
					return Grammar.MakePossessive(Context.Capitalize ? Context.Explicit.Capitalize() : Context.Explicit);
				}
				return Context.Default;
			}
		}

		// Token: 0x0600CD7B RID: 52603 RVA: 0x004A7384 File Offset: 0x004A5584
		[VariableObjectReplacer(new string[] { "nameSingle" }, Default = "thing")]
		public static string TargetNameSingle(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Context.Target.GetDisplayName(int.MaxValue, null, null, false, true, false, false, false, false, true, true, false, true, false, false, false, null, false, Context.Capitalize, false, false, null, false, null, false, true);
				}
				if (!Context.Capitalize)
				{
					return "you";
				}
				return "You";
			}
			else
			{
				if (Context.Explicit == null)
				{
					return Context.Default;
				}
				if (!Context.Capitalize)
				{
					return Context.Explicit;
				}
				return Context.Explicit.Capitalize();
			}
		}

		// Token: 0x0600CD7C RID: 52604 RVA: 0x004A7424 File Offset: 0x004A5624
		[VariableObjectReplacer(new string[] { "longname" }, Default = "thing")]
		public static string TargetNameLong(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Context.Target.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true);
				}
				if (!Context.Capitalize)
				{
					return "you";
				}
				return "You";
			}
			else
			{
				if (Context.Explicit == null)
				{
					return Context.Default;
				}
				if (!Context.Capitalize)
				{
					return Context.Explicit;
				}
				return Context.Explicit.Capitalize();
			}
		}

		// Token: 0x0600CD7D RID: 52605 RVA: 0x004A74BC File Offset: 0x004A56BC
		[VariableObjectReplacer(new string[] { "refname" }, Default = "thing")]
		public static string TargetNameReference(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Context.Target.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, true, false, false, false, null, false);
				}
				if (!Context.Capitalize)
				{
					return "you";
				}
				return "You";
			}
			else
			{
				if (Context.Explicit == null)
				{
					return Context.Default;
				}
				if (!Context.Capitalize)
				{
					return Context.Explicit;
				}
				return Context.Explicit.Capitalize();
			}
		}

		// Token: 0x0600CD7E RID: 52606 RVA: 0x004A7540 File Offset: 0x004A5740
		[VariableObjectReplacer(new string[] { "refname's" }, Default = "thing's")]
		public static string TargetNameReferencePossessive(DelegateContext Context)
		{
			if (Context.Target != null)
			{
				if (!Context.Target.IsPlayer() || !Grammar.AllowSecondPerson)
				{
					return Grammar.MakePossessive(Context.Target.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, true, false, false, false, null, false));
				}
				if (!Context.Capitalize)
				{
					return "your";
				}
				return "Your";
			}
			else
			{
				if (Context.Explicit != null)
				{
					return Grammar.MakePossessive(Context.Capitalize ? Context.Explicit.Capitalize() : Context.Explicit);
				}
				return Context.Default;
			}
		}

		// Token: 0x0600CD7F RID: 52607 RVA: 0x004A75D0 File Offset: 0x004A57D0
		[VariableObjectReplacer(new string[] { "bodypart" }, Capitalization = false)]
		public static string BodyPartOrdinalName(DelegateContext Context)
		{
			GameObject target = Context.Target;
			BodyPart bodyPart;
			if (target == null)
			{
				bodyPart = null;
			}
			else
			{
				Body body = target.Body;
				bodyPart = ((body != null) ? body.GetBody() : null);
			}
			BodyPart bodyPart2 = bodyPart;
			if (bodyPart2 != null)
			{
				List<BodyPart> list = new List<BodyPart>();
				int i = 0;
				int count = Context.Parameters.Count;
				while (i < count)
				{
					list.Clear();
					bodyPart2.GetPart(Context.Parameters[i], list);
					list.ShuffleInPlace<BodyPart>(null);
					int j = 0;
					int count2 = list.Count;
					while (j < count2)
					{
						BodyPart bodyPart3 = list[i];
						if (!bodyPart3.Abstract)
						{
							return bodyPart3.GetOrdinalName();
						}
						j++;
					}
					i++;
				}
				return bodyPart2.GetOrdinalName();
			}
			return "body";
		}

		// Token: 0x0600CD80 RID: 52608 RVA: 0x004A767E File Offset: 0x004A587E
		[VariableObjectReplacer(new string[] { "bodypart.shuffled" }, Capitalization = false)]
		public static string BodyPartOrdinalNameShuffled(DelegateContext Context)
		{
			Context.Parameters.ShuffleInPlace<string>(null);
			return VariableReplacers.BodyPartOrdinalName(Context);
		}

		// Token: 0x0600CD81 RID: 52609 RVA: 0x004A7694 File Offset: 0x004A5894
		[VariableObjectReplacer(Default = "somewhere")]
		public static string GeneralDirection(DelegateContext Context)
		{
			GameObject player = The.Player;
			string text = ((player != null) ? player.DescribeDirectionToward(Context.Target, true, false) : null);
			if (text == null)
			{
				return Context.Default;
			}
			if (!Context.Capitalize)
			{
				return text;
			}
			return text.Capitalize();
		}

		// Token: 0x0600CD82 RID: 52610 RVA: 0x004A76D8 File Offset: 0x004A58D8
		[VariableObjectReplacer]
		public static string GeneralDirectionIfAny(DelegateContext Context)
		{
			GameObject player = The.Player;
			string text = ((player != null) ? player.DescribeDirectionToward(Context.Target, true, false) : null);
			if (text.IsNullOrEmpty() || !(text != "here"))
			{
				return "here";
			}
			if (!Context.Capitalize)
			{
				return text;
			}
			return text.Capitalize();
		}

		// Token: 0x0600CD83 RID: 52611 RVA: 0x004A772C File Offset: 0x004A592C
		[VariableObjectReplacer(new string[] { "isplural" }, Capitalization = false)]
		public static string TernaryPlural(DelegateContext Context)
		{
			string text = "";
			if (Context.Parameters.Count > 0)
			{
				GameObject target = Context.Target;
				if ((target != null) ? target.IsPlural : Context.Pronouns.Plural)
				{
					text = Context.Parameters[0];
				}
				else if (Context.Parameters.Count > 1)
				{
					text = Context.Parameters[1];
				}
			}
			return text;
		}

		// Token: 0x0600CD84 RID: 52612 RVA: 0x004A7796 File Offset: 0x004A5996
		[VariableReplacer(new string[] { "ifplayerplural" })]
		public static string PlayerPlural(DelegateContext Context)
		{
			Context.Target = The.Player;
			return VariableReplacers.TernaryPlural(Context);
		}

		// Token: 0x0600CD85 RID: 52613 RVA: 0x004A77AC File Offset: 0x004A59AC
		[VariableReplacer(new string[] { "factionaddress" })]
		public static string FactionAddress(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Parameters.Count > 0)
			{
				string text2 = Context.Parameters[0];
				bool flag = false;
				bool flag2 = false;
				XRLGame game = The.Game;
				text = ((game != null) ? game.PlayerReputation.GetFactionRank(text2) : null);
				if (Context.Parameters.Count > 1)
				{
					flag = Context.Parameters.Remove("singular");
					flag2 = Context.Parameters.Remove("formal");
					if (Context.Parameters.Count > 1 && text.IsNullOrEmpty())
					{
						text = Context.Parameters[1];
					}
				}
				if (text.IsNullOrEmpty())
				{
					text = Faction.GetDefaultAddress(text2);
				}
				if (text.IsNullOrEmpty())
				{
					text = (flag2 ? The.Player.formalAddressTerm : The.Player.personTerm);
				}
				if (!flag && The.Player.IsPlural)
				{
					text = text.Pluralize();
				}
			}
			return text;
		}

		// Token: 0x0600CD86 RID: 52614 RVA: 0x004A7898 File Offset: 0x004A5A98
		[VariableReplacer(new string[] { "factionrank" })]
		public static string FactionRank(DelegateContext Context)
		{
			string text = Context.Default;
			if (Context.Parameters.Count > 0)
			{
				string text2 = Context.Parameters[0];
				bool flag = false;
				XRLGame game = The.Game;
				text = ((game != null) ? game.PlayerReputation.GetFactionRank(text2) : null) ?? "";
				if (Context.Parameters.Count > 1)
				{
					flag = Context.Parameters.Remove("singular");
					if (Context.Parameters.Count > 1 && text.IsNullOrEmpty())
					{
						text = Context.Parameters[1];
					}
				}
				if (!flag && The.Player.IsPlural)
				{
					text = text.Pluralize();
				}
			}
			return text;
		}

		// Token: 0x0600CD87 RID: 52615 RVA: 0x004A7946 File Offset: 0x004A5B46
		[VariableObjectReplacer(new string[] { "terrain.t" }, Capitalization = false)]
		public static string TerrainDisplayName(DelegateContext Context)
		{
			object obj;
			if (Context.Type != TargetType.None)
			{
				GameObject target = Context.Target;
				obj = ((target != null) ? target.CurrentZone : null);
			}
			else
			{
				obj = The.ActiveZone;
			}
			object obj2 = obj;
			return ((obj2 != null) ? obj2.GetTerrainDisplayName(true) : null) ?? "the unknown";
		}

		// Token: 0x0600CD88 RID: 52616 RVA: 0x004A797F File Offset: 0x004A5B7F
		[VariableObjectReplacer(new string[] { "landmark.nearest" }, Capitalization = false)]
		public static string NearestLandmark(DelegateContext Context)
		{
			return JournalAPI.GetLandmarkNearest((Context.Type == TargetType.None) ? The.ActiveZone : Context.Target.CurrentZone).Text;
		}

		// Token: 0x0600CD89 RID: 52617 RVA: 0x004A79A8 File Offset: 0x004A5BA8
		[VariablePostProcessor]
		public static void Capitalize(DelegateContext Context)
		{
			if (Context.Value.Length <= 0)
			{
				return;
			}
			char c = Context.Value[0];
			if (char.IsLetter(c))
			{
				Context.Value[0] = char.ToUpperInvariant(Context.Value[0]);
				return;
			}
			if (c == '{')
			{
				int num = Context.Value.IndexOf('|', 0) + 1;
				Context.Value[num] = char.ToUpperInvariant(Context.Value[num]);
			}
		}

		// Token: 0x0600CD8A RID: 52618 RVA: 0x004A7A2C File Offset: 0x004A5C2C
		[VariablePostProcessor]
		public static void Lower(DelegateContext Context)
		{
			int i = 0;
			int length = Context.Value.Length;
			while (i < length)
			{
				Context.Value[i] = char.ToLowerInvariant(Context.Value[i]);
				i++;
			}
		}

		// Token: 0x0600CD8B RID: 52619 RVA: 0x004A7A70 File Offset: 0x004A5C70
		[VariablePostProcessor]
		public static void Upper(DelegateContext Context)
		{
			int i = 0;
			int length = Context.Value.Length;
			while (i < length)
			{
				Context.Value[i] = char.ToUpperInvariant(Context.Value[i]);
				i++;
			}
		}

		// Token: 0x0600CD8C RID: 52620 RVA: 0x004A7AB2 File Offset: 0x004A5CB2
		[VariablePostProcessor]
		public static void Strip(DelegateContext Context)
		{
			ColorUtility.StripFormatting(Context.Value);
		}

		// Token: 0x0600CD8D RID: 52621 RVA: 0x004A7AC0 File Offset: 0x004A5CC0
		[VariablePostProcessor]
		public static void Pluralize(DelegateContext Context)
		{
			string text = Context.Value.ToString();
			text = Grammar.Pluralize(text);
			Context.Value.Clear();
			Context.Value.Append(text);
		}

		// Token: 0x0600CD8E RID: 52622 RVA: 0x004A7AF9 File Offset: 0x004A5CF9
		[VariablePostProcessor(new string[] { "playerpluralize" })]
		public static void PluralizeForPlayer(DelegateContext Context)
		{
			if (The.Player != null && The.Player.IsPlural)
			{
				VariableReplacers.Pluralize(Context);
			}
		}

		// Token: 0x0600CD8F RID: 52623 RVA: 0x004A7B14 File Offset: 0x004A5D14
		[VariablePostProcessor(Capitalization = true)]
		public static void Article(DelegateContext Context)
		{
			string text = Context.Value.ToString();
			Context.Value.Clear();
			Grammar.A(text, Context.Value, Context.Capitalize);
		}

		// Token: 0x0600CD90 RID: 52624 RVA: 0x004A7B40 File Offset: 0x004A5D40
		[VariablePostProcessor]
		public static void Title(DelegateContext Context)
		{
			string text = Context.Value.ToString();
			text = Grammar.MakeTitleCase(text);
			Context.Value.Clear();
			Context.Value.Append(text);
		}

		// Token: 0x0600CD91 RID: 52625 RVA: 0x004A7B79 File Offset: 0x004A5D79
		[ModSensitiveCacheInit]
		public static void Reset()
		{
			VariableReplacers.Map.Clear();
			VariableReplacers.PostMap.Clear();
			VariableReplacers.Initialized = false;
		}

		// Token: 0x0600CD92 RID: 52626 RVA: 0x004A7B98 File Offset: 0x004A5D98
		public static void LoadReplacers()
		{
			List<string> list = new List<string>();
			List<ReplacerEntry> list2 = new List<ReplacerEntry>();
			foreach (MethodInfo methodInfo in ModManager.GetMethodsWithAttribute(typeof(VariableReplacerAttribute), typeof(HasVariableReplacerAttribute), false))
			{
				try
				{
					list.Clear();
					VariableReplacerAttribute customAttribute = methodInfo.GetCustomAttribute<VariableReplacerAttribute>();
					StringMap<ReplacerEntry> stringMap = ((customAttribute is VariablePostProcessorAttribute) ? VariableReplacers.PostMap : VariableReplacers.Map);
					if (customAttribute.Keys.IsNullOrEmpty<string>())
					{
						list.Add(Grammar.InitLower(methodInfo.Name));
					}
					else
					{
						list.AddRange(customAttribute.Keys);
					}
					list2.Clear();
					Replacer replacer;
					if (methodInfo.ReturnType == typeof(void))
					{
						Action<DelegateContext> action = (Action<DelegateContext>)methodInfo.CreateDelegate(typeof(Action<DelegateContext>));
						replacer = delegate(DelegateContext x)
						{
							action(x);
							return null;
						};
					}
					else
					{
						replacer = (Replacer)methodInfo.CreateDelegate(typeof(Replacer));
					}
					list2.Add(new ReplacerEntry(replacer, customAttribute.Capitalization ? Grammar.InitLower(customAttribute.Default) : customAttribute.Default, customAttribute.Flags, false));
					if (customAttribute.Capitalization)
					{
						list2.Add(new ReplacerEntry(replacer, customAttribute.Capitalization ? Grammar.InitCap(customAttribute.Default) : customAttribute.Default, customAttribute.Flags, true));
					}
					int i = 0;
					int count = list2.Count;
					while (i < count)
					{
						ReplacerEntry replacerEntry = list2[i];
						bool flag = i % 2 != 0;
						int j = 0;
						int count2 = list.Count;
						while (j < count2)
						{
							string text = list[j];
							if (customAttribute.Capitalization)
							{
								text = (flag ? Grammar.InitCap(list[j]) : Grammar.InitLower(list[j]));
							}
							if (customAttribute.Override)
							{
								stringMap[text] = replacerEntry;
							}
							else
							{
								stringMap.TryAdd(text, replacerEntry);
							}
							j++;
						}
						i++;
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogAssemblyError(methodInfo, ex);
				}
			}
			VariableReplacers.Initialized = true;
		}

		// Token: 0x04004F64 RID: 20324
		private static readonly string DefaultPoeticFeatures = "shiny teeth,coarse hair,sunken eyes";

		// Token: 0x04004F65 RID: 20325
		private static readonly string DefaultActivities = "roaming around idly";

		// Token: 0x04004F66 RID: 20326
		private static readonly string DefaultVillageActivities = "sleeping in our homes";

		// Token: 0x04004F67 RID: 20327
		private static readonly string DefaultSacredThings = "the act of procreating";

		// Token: 0x04004F68 RID: 20328
		private static readonly string DefaultArableLands = "arable land";

		// Token: 0x04004F69 RID: 20329
		private static readonly string DefaultValuedOres = "precious metals";

		// Token: 0x04004F6A RID: 20330
		public static StringMap<ReplacerEntry> Map = new StringMap<ReplacerEntry>();

		// Token: 0x04004F6B RID: 20331
		public static StringMap<ReplacerEntry> PostMap = new StringMap<ReplacerEntry>();

		// Token: 0x04004F6C RID: 20332
		public static bool Initialized;
	}
}
