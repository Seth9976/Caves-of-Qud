using System;
using System.Threading;
using Qud.API;
using XRL.Core;
using XRL.UI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000DAA RID: 3498
	public class WakingDream : Effect
	{
		// Token: 0x06008BB6 RID: 35766 RVA: 0x00319F2C File Offset: 0x0031812C
		public WakingDream()
		{
			this.DisplayName = "waking dream";
			this.Duration = 1;
		}

		// Token: 0x06008BB7 RID: 35767 RVA: 0x00319F46 File Offset: 0x00318146
		public WakingDream(GameObject Dreamer, GameObject Source)
			: this()
		{
			this.Dreamer = Dreamer;
			this.Source = Source;
			this.FromOriginalPlayerBody = Dreamer.IsOriginalPlayerBody();
		}

		// Token: 0x06008BB8 RID: 35768 RVA: 0x00319F68 File Offset: 0x00318168
		public override string GetDetails()
		{
			return "Gain a level as this creature to wake pleasantly.\nDie or abandon character to wake fitfully.";
		}

		// Token: 0x06008BB9 RID: 35769 RVA: 0x00319F6F File Offset: 0x0031816F
		public override string GetStateDescription()
		{
			return "in a waking dream";
		}

		// Token: 0x06008BBA RID: 35770 RVA: 0x00319F76 File Offset: 0x00318176
		public override int GetEffectType()
		{
			return 2;
		}

		// Token: 0x06008BBB RID: 35771 RVA: 0x00319F79 File Offset: 0x00318179
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterDieEvent.ID || ID == PooledEvent<AfterLevelGainedEvent>.ID;
		}

		// Token: 0x06008BBC RID: 35772 RVA: 0x00319F97 File Offset: 0x00318197
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, PooledEvent<GenericCommandEvent>.ID, 0, false);
		}

		// Token: 0x06008BBD RID: 35773 RVA: 0x00319FAB File Offset: 0x003181AB
		public override bool HandleEvent(AfterDieEvent E)
		{
			base.Object.RemoveEffect(this, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06008BBE RID: 35774 RVA: 0x00319FC2 File Offset: 0x003181C2
		public override bool HandleEvent(AfterLevelGainedEvent E)
		{
			E.RequestInterfaceExit();
			GameEventCommand.Issue("EndWakingDream", null, this.ID.GetHashCode(), 1);
			return base.HandleEvent(E);
		}

		// Token: 0x06008BBF RID: 35775 RVA: 0x00319FF0 File Offset: 0x003181F0
		public override bool HandleEvent(GenericCommandEvent E)
		{
			if (E.Command == "EndWakingDream" && E.Level == this.ID.GetHashCode() && base.Object != null && this.Duration > 0)
			{
				this.Duration = 0;
				this.Pleasant = E.Flags.HasBit(1);
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008BC0 RID: 35776 RVA: 0x0031A067 File Offset: 0x00318267
		public static bool CanApply(GameObject Object)
		{
			return !Object.HasEffect<WakingDream>() && CanApplyEffectEvent.Check<WakingDream>(Object, 0);
		}

		// Token: 0x06008BC1 RID: 35777 RVA: 0x0031A07F File Offset: 0x0031827F
		public static bool ApplyTo(GameObject Object, GameObject Dreamer, GameObject Source)
		{
			return WakingDream.CanApply(Object) && Object.ApplyEffect(new WakingDream(Dreamer, Source), null);
		}

		// Token: 0x06008BC2 RID: 35778 RVA: 0x0031A09C File Offset: 0x0031829C
		public override void Remove(GameObject Object)
		{
			if (!Object.IsPlayer())
			{
				return;
			}
			Object.PullDown(false);
			if (GameObject.Validate(ref this.Dreamer) && !this.Dreamer.IsDying && !this.Dreamer.IsNowhere() && !this.Metempsychosis)
			{
				SoundManager.PlaySound("sfx_characterTrigger_dreamcrungle_start", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				GameManager.Instance.Spin = 10f;
				DeepDream.TransitionOut(2f, null);
				The.Game.Player.Body = this.Dreamer;
				this.Dreamer.CurrentZone.SetActive();
				Thread.Sleep(2000);
				bool flag = !Options.ModernUI;
				if (this.Pleasant)
				{
					if (flag)
					{
						DeepDream.ClassicFade();
					}
					this.Dreamer.PlayWorldSound("sfx_characterTrigger_dreamcrungle_wake_peaceful", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					Popup.Show("{{G|You wake up peacefully.}}", null, null, true, true, true, true, null);
					DeepDream.TransitionIn(flag ? 0f : 2f);
					Achievement.SOUND_SLEEPER.Progress.Increment(1);
					JournalAPI.AddAccomplishment("You woke from a peaceful dream.", "<spice.commonPhrases.blessed.!random.capitalize> =name= dreamed of a thousand years of peace, and the people of Qud <spice.history.gospels.Celebration.LateSultanate.!random> in <spice.commonPhrases.celebration.!random>.", string.Concat(new string[]
					{
						"One auspicious evening, =name= had a dream that <entity.subjectPronoun> ",
						The.Player.GetVerb("were", true, false, false, true, false),
						" not =name= but ",
						The.Player.GetVerb("were", true, false, false, true, false),
						" instead the ",
						Object.GetBlueprint(true).CachedDisplayNameStripped,
						" ",
						Object.DisplayNameOnlyDirect,
						". After a full otherlife, ",
						The.Player.GetPronounProvider(false).Subjective,
						" woke from the dream in a state of deep peace."
					}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.High, null, -1L, true);
				}
				else
				{
					if (flag)
					{
						DeepDream.ClassicFade();
					}
					this.Dreamer.PlayWorldSound("sfx_characterTrigger_dreamcrungle_wake_fitful", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					Popup.Show("{{r|You wake in a fitful start.}}", null, null, true, true, true, true, null);
					DeepDream.TransitionIn(flag ? 0f : 2f);
					JournalAPI.AddAccomplishment("You woke from a fitful dream.", "<spice.commonPhrases.blessed.!random.capitalize> =name= dreamed of a thousand years of woe, and the people of Qud <spice.history.gospels.Celebration.LateSultanate.!random> in <spice.commonPhrases.woe.!random>.", string.Concat(new string[]
					{
						"One auspicious evening, =name= had a dream that <entity.subjectPronoun> ",
						The.Player.GetVerb("were", true, false, false, true, false),
						" not =name= but ",
						The.Player.GetVerb("were", true, false, false, true, false),
						" instead the ",
						Object.GetBlueprint(true).CachedDisplayNameStripped,
						" ",
						Object.DisplayNameOnlyDirect,
						". After a full otherlife, ",
						The.Player.GetPronounProvider(false).Subjective,
						" woke from the dream in a state of deep horror."
					}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.High, null, -1L, true);
				}
				this.Award(this.Dreamer);
			}
			else
			{
				Domination.Metempsychosis(Object, this.FromOriginalPlayerBody);
				this.Award(Object);
			}
			if (Object.HasRegisteredEvent("EndWakingDream"))
			{
				Object.FireEvent(Event.New("EndWakingDream", "Pleasant", this.Pleasant ? 1 : 0, "Metempsychosis", this.Metempsychosis ? 1 : 0));
			}
		}

		// Token: 0x06008BC3 RID: 35779 RVA: 0x0031A408 File Offset: 0x00318608
		public void Award(GameObject Target)
		{
			if (this.Pleasant)
			{
				if (GameObject.Validate(ref Target))
				{
					Target.AwardXP(15000, -1, 0, int.MaxValue, null, base.Object, null, null, null, null);
				}
				if (GameObject.Validate(ref this.Source) && !this.Source.IsNowhere())
				{
					this.Source.ReplaceWith("Sated Dreamcrungle");
					return;
				}
			}
			else
			{
				if (GameObject.Validate(ref Target))
				{
					Statistic stat = Target.GetStat("Willpower");
					int baseValue = stat.BaseValue;
					stat.BaseValue = baseValue - 1;
					if (Target.IsPlayer())
					{
						Popup.Show("You lose {{rules|1}} point of Willpower permanently.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				if (GameObject.Validate(ref this.Source) && !this.Source.IsNowhere())
				{
					this.Source.SetIntProperty("NoXP", 1, false);
					this.Source.Die(Target, null, "Your dream collapsed.", this.Source.Poss("dream", true) + " collapsed", false, null, null, false, false, null, null, null);
				}
			}
		}

		// Token: 0x04003AD5 RID: 15061
		public const string END_CMD = "EndWakingDream";

		// Token: 0x04003AD6 RID: 15062
		public const int FLAG_PLEASANT = 1;

		// Token: 0x04003AD7 RID: 15063
		public GameObject Dreamer;

		// Token: 0x04003AD8 RID: 15064
		public GameObject Source;

		// Token: 0x04003AD9 RID: 15065
		public bool FromOriginalPlayerBody;

		// Token: 0x04003ADA RID: 15066
		[NonSerialized]
		public bool Pleasant;

		// Token: 0x04003ADB RID: 15067
		[NonSerialized]
		public bool Metempsychosis;
	}
}
