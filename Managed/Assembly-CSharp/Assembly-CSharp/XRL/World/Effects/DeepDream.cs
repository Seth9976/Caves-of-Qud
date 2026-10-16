using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using Qud.API;
using Qud.UI;
using UnityEngine;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI.GoalHandlers;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D20 RID: 3360
	[HasWishCommand]
	public class DeepDream : Effect
	{
		// Token: 0x0600844D RID: 33869 RVA: 0x002FC903 File Offset: 0x002FAB03
		public DeepDream()
		{
			this.DisplayName = "{{b|deep dreaming}}";
			this.Duration = 1;
		}

		// Token: 0x0600844E RID: 33870 RVA: 0x002FC91D File Offset: 0x002FAB1D
		public DeepDream(GameObject Source)
			: this()
		{
			this.Source = Source;
		}

		// Token: 0x0600844F RID: 33871 RVA: 0x002FC92C File Offset: 0x002FAB2C
		public override string GetDetails()
		{
			return "This creature is unresponsive.";
		}

		// Token: 0x06008450 RID: 33872 RVA: 0x002FC933 File Offset: 0x002FAB33
		public override int GetEffectType()
		{
			return 33554434;
		}

		// Token: 0x06008451 RID: 33873 RVA: 0x002FC93A File Offset: 0x002FAB3A
		public override string GetStateDescription()
		{
			return "{{b|deeply dreaming}}";
		}

		// Token: 0x06008452 RID: 33874 RVA: 0x002FC941 File Offset: 0x002FAB41
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == TookDamageEvent.ID || ID == EffectAppliedEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == BeforeDeathRemovalEvent.ID || ID == GetZoneFreezabilityEvent.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x06008453 RID: 33875 RVA: 0x002FC97F File Offset: 0x002FAB7F
		public override bool HandleEvent(TookDamageEvent E)
		{
			this.Target.RemoveEffect(typeof(WakingDream));
			E.RequestInterfaceExit();
			return base.HandleEvent(E);
		}

		// Token: 0x06008454 RID: 33876 RVA: 0x002FC9A4 File Offset: 0x002FABA4
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			if (E.Effect != this && E.Effect.IsOfType(33554432))
			{
				base.Object.RemoveEffect(this, true);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008455 RID: 33877 RVA: 0x002FC9DC File Offset: 0x002FABDC
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			WakingDream effect = this.Target.GetEffect<WakingDream>();
			if (effect != null && effect.Dreamer == base.Object)
			{
				effect.Metempsychosis = true;
				this.Target.RemoveEffect(effect, true);
				base.Object.RemoveEffect(this, true);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008456 RID: 33878 RVA: 0x002FCA36 File Offset: 0x002FAC36
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == base.Object)
			{
				E.Message = Asleep.GetSleepMessage(base.Object, E.Physical, E.Mental);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008457 RID: 33879 RVA: 0x002FCA6C File Offset: 0x002FAC6C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (base.Object.IsPlayer() || !GameObject.Validate(ref this.Target) || !this.Target.HasEffect(typeof(WakingDream)))
			{
				base.Object.RemoveEffect(this, true);
				return true;
			}
			if (!(base.Object.Brain.Goals.Peek() is Dormant))
			{
				base.Object.Brain.Goals.Clear();
				base.Object.Brain.PushGoal(new Dormant(-1));
			}
			return false;
		}

		// Token: 0x06008458 RID: 33880 RVA: 0x002FCB03 File Offset: 0x002FAD03
		public override bool HandleEvent(GetZoneFreezabilityEvent E)
		{
			E.Freezability = Freezability.FormerPlayerObject;
			return false;
		}

		// Token: 0x06008459 RID: 33881 RVA: 0x002FCB10 File Offset: 0x002FAD10
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect(typeof(DeepDream)) || !ApplyEffectEvent.Check(Object, "DeepDream", this, null))
			{
				return false;
			}
			if (!Object.IsPlayer())
			{
				Object.ApplyEffect(new Asleep(20, false, false, false, true), null);
				return false;
			}
			if (Object.HasEffect(typeof(WakingDream)))
			{
				Achievement.DEEP_DREAM.Unlock();
			}
			SoundManager.PlaySound("sfx_characterTrigger_dreamcrungle_start", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			GameObject player = The.Player;
			if (player != null)
			{
				player.SleepytimeParticles();
			}
			GameManager.Instance.Spin = 10f;
			DeepDream.TransitionOut(2f, null);
			return this.Crungle() && base.Apply(Object);
		}

		// Token: 0x0600845A RID: 33882 RVA: 0x002FCBD3 File Offset: 0x002FADD3
		public override void Applied(GameObject Object)
		{
			The.ZoneManager.SuspendAll();
			The.ZoneManager.CheckCached(true, false);
		}

		// Token: 0x0600845B RID: 33883 RVA: 0x002FCBEC File Offset: 0x002FADEC
		public bool IsOurTargetEffect(Effect FX)
		{
			WakingDream wakingDream = FX as WakingDream;
			return wakingDream != null && wakingDream.Dreamer == base.Object;
		}

		// Token: 0x0600845C RID: 33884 RVA: 0x002FCC13 File Offset: 0x002FAE13
		public override void Remove(GameObject Object)
		{
			if (GameObject.Validate(ref this.Target))
			{
				this.Target.RemoveEffect(new Predicate<Effect>(this.IsOurTargetEffect));
			}
			base.Remove(Object);
		}

		// Token: 0x0600845D RID: 33885 RVA: 0x002FCC44 File Offset: 0x002FAE44
		public int GetObjectWeight(GameObject Object)
		{
			if (!Object.IsCombatObject() || !Object.IsMobile() || !Object.HasStat("XP") || Object.HasEffect(typeof(WakingDream)) || Object.HasEffect(typeof(DeepDream)))
			{
				return 0;
			}
			int num;
			if (DeepDream.BlueprintWeights.TryGetValue(Object.Blueprint, out num))
			{
				return num;
			}
			return 100;
		}

		// Token: 0x0600845E RID: 33886 RVA: 0x002FCCAC File Offset: 0x002FAEAC
		public GameObject GetDreamSubject()
		{
			Zone zone = The.ActiveZone;
			BallBag<GameObject> ballBag = new BallBag<GameObject>(0);
			for (int i = 0; i < 15; i++)
			{
				if (!zone.IsActive())
				{
					The.ZoneManager.SuspendZone(zone);
					The.ZoneManager.CheckCached(true, false);
				}
				string randomDestinationZoneID = SpaceTimeVortex.GetRandomDestinationZoneID(The.Player.CurrentZone.ZoneWorld, false);
				if (!The.ZoneManager.IsZoneLive(randomDestinationZoneID))
				{
					zone = The.ZoneManager.GetZone(randomDestinationZoneID);
					ballBag.Clear();
					ballBag.AddRange(zone.YieldObjects(), new Func<GameObject, int>(this.GetObjectWeight));
					if (ballBag.Count != 0)
					{
						int num = ballBag.TotalWeight / ballBag.Count;
						MetricsManager.LogInfo(string.Format("WakingDream::{0}:{1}", randomDestinationZoneID, num));
						if (num.ChanceIn(100))
						{
							break;
						}
					}
				}
			}
			return ballBag.PeekOne();
		}

		// Token: 0x0600845F RID: 33887 RVA: 0x002FCD88 File Offset: 0x002FAF88
		public bool Crungle()
		{
			GameObject player = The.Player;
			bool flag = !Options.ModernUI;
			Loading.SetHideLoadStatus(true);
			Popup.Suppress = true;
			try
			{
				this.Target = this.GetDreamSubject();
				if (this.Target == null)
				{
					return false;
				}
				MetricsManager.LogInfo("WakingDream::Subject:" + this.Target.Blueprint);
				this.Target.GiveProperName(null, false, null, false, null, null, null, null);
				if (!WakingDream.ApplyTo(this.Target, player, this.Source))
				{
					return false;
				}
				int level = this.Target.Level;
				int num = Leveler.GetXPForLevel(level + 1);
				num -= Math.Max(1, Stat.Random(level * 25, level * 75));
				this.Target.GetStat("XP").BaseValue = num;
				this.Target.Brain.PartyLeader = null;
				this.Target.Brain.Goals.Clear();
				The.Game.Player.Body = this.Target;
				this.Target.CurrentZone.SetActive();
				Thread.Sleep(2000);
				Popup.Suppress = false;
				if (flag)
				{
					DeepDream.ClassicFade();
				}
				Popup.Show("You have the feeling of waking from a dream.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment(string.Concat(new string[]
				{
					"You woke from a long, convincing dream and returned to your life as the ",
					this.Target.GetBlueprint(true).CachedDisplayNameStripped,
					" ",
					this.Target.DisplayNameOnlyDirect,
					"."
				}), string.Concat(new string[]
				{
					"<spice.commonPhrases.blessed.!random.capitalize> =name= revealed the details of ",
					IComponent<GameObject>.ThePlayer.GetPronounProvider(false).PossessiveAdjective,
					" former life as a ",
					this.Target.GetBlueprint(true).CachedDisplayNameStripped,
					", and announced ",
					IComponent<GameObject>.ThePlayer.GetPronounProvider(false).PossessiveAdjective,
					" plans to return to it!"
				}), string.Concat(new string[]
				{
					"One auspicious evening, ",
					this.Target.DisplayNameOnlyDirect,
					" had a dream that <entity.subjectPronoun> was not the ",
					this.Target.GetBlueprint(true).CachedDisplayNameStripped,
					" ",
					this.Target.DisplayNameOnlyDirect,
					" but was instead the Moon King =name=. <spice.commonPhrases.fromThenOn.!random.capitalize>, <entity.subjectPronoun> always kept some moondust on <entity.possessivePronoun> person."
				}), null, "general", MuralCategory.WeirdThingHappens, MuralWeight.High, null, -1L, true);
				this.RequireLightSource(this.Target);
				Popup.Suppress = true;
			}
			finally
			{
				DeepDream.TransitionIn(flag ? 0f : 4f);
				Loading.SetHideLoadStatus(false);
				Popup.Suppress = false;
			}
			return true;
		}

		// Token: 0x06008460 RID: 33888 RVA: 0x002FD050 File Offset: 0x002FB250
		public void RequireLightSource(GameObject Object)
		{
			if (Object.HasPart<DarkVision>())
			{
				return;
			}
			if (Object.Body.HasPart("Hand"))
			{
				Object.ReceiveObject("Torch", 3, false, 0, 0, null, null, null, null);
				Object.Brain.PerformReequip(true, false, false, null);
				return;
			}
			Object.RequirePart<Mutations>(false).AddMutation("DarkVision", 1);
		}

		// Token: 0x06008461 RID: 33889 RVA: 0x002FD0AF File Offset: 0x002FB2AF
		public static void ClassicFade()
		{
			ScreenBuffer scrapBuffer = TextConsole.GetScrapBuffer1(false);
			scrapBuffer.Clear();
			scrapBuffer.Draw();
			FadeToBlack.FadeIn(0.5f, Color.black);
		}

		// Token: 0x06008462 RID: 33890 RVA: 0x002FD0D4 File Offset: 0x002FB2D4
		public static void TransitionOut(float Fade = 0f, string Sound = null)
		{
			SoundManager.StopMusic("music", Fade > 0f, Fade);
			if (Sound != null)
			{
				SoundManager.PlaySound(Sound, 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			}
			FadeToBlack.FadeOut(Fade, Color.black);
			GameManager.Instance.PushGameView("Empty", true);
			if (Fade > 0f)
			{
				The.Core.RenderDelay((int)(Fade * 1000f), false, true, false);
			}
		}

		// Token: 0x06008463 RID: 33891 RVA: 0x002FD14C File Offset: 0x002FB34C
		public static void TransitionIn(float Fade = 0f)
		{
			FadeToBlack.FadeIn(Fade, Color.black);
			GameManager.Instance.PushGameView("Empty", true);
			if (Fade > 0f)
			{
				The.Core.RenderDelay((int)(Fade * 1000f), false, true, false);
			}
			if (GameManager.Instance.CurrentGameView == "Empty")
			{
				GameManager.Instance.PopGameView(false);
			}
		}

		// Token: 0x06008464 RID: 33892 RVA: 0x002FD1B3 File Offset: 0x002FB3B3
		[WishCommand("crungle", null)]
		public static void Wish()
		{
			The.Player.ApplyEffect(new DeepDream(), null);
		}

		// Token: 0x04003975 RID: 14709
		public const int DefaultWeight = 100;

		// Token: 0x04003976 RID: 14710
		public static readonly Dictionary<string, int> BlueprintWeights = new Dictionary<string, int>
		{
			{ "Cave Spider", 30 },
			{ "Glowfish", 30 },
			{ "GiantAmoeba", 30 }
		};

		// Token: 0x04003977 RID: 14711
		public GameObject Target;

		// Token: 0x04003978 RID: 14712
		[NonSerialized]
		public GameObject Source;
	}
}
