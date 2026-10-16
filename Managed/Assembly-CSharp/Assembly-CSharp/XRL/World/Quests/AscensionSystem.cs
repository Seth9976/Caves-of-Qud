using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Genkit;
using Qud.API;
using Qud.UI;
using UnityEngine;
using XRL.Rules;
using XRL.Sound;
using XRL.UI;
using XRL.Wish;
using XRL.World.AI;
using XRL.World.AI.Pathfinding;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Quests.GolemQuest;

namespace XRL.World.Quests
{
	// Token: 0x02000B1C RID: 2844
	[HasWishCommand]
	[Serializable]
	public class AscensionSystem : IQuestSystem
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06007229 RID: 29225 RVA: 0x002B22E8 File Offset: 0x002B04E8
		public override bool RemoveWithQuest
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600722A RID: 29226 RVA: 0x002B22EC File Offset: 0x002B04EC
		private HashSet<string> SpindleZones
		{
			get
			{
				if (this._SpindleZones == null)
				{
					this._SpindleZones = new HashSet<string>(25);
					foreach (Location2D location2D in Zone.zoneIDTo240x72Location("JoppaWorld.53.3.1.1.10").YieldAdjacent(2))
					{
						this._SpindleZones.Add(ZoneID.Assemble("JoppaWorld", location2D, 10));
					}
				}
				return this._SpindleZones;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600722B RID: 29227 RVA: 0x002B2370 File Offset: 0x002B0570
		// (set) Token: 0x0600722C RID: 29228 RVA: 0x002B23A3 File Offset: 0x002B05A3
		public GameObject Climber
		{
			get
			{
				if (!GameObject.Validate(ref this._Climber) && this.ClimberID != 0)
				{
					this._Climber = The.ZoneManager.FindObjectByID(this.ClimberID);
				}
				return this._Climber;
			}
			set
			{
				this._Climber = value;
				this.ClimberID = ((value != null) ? value.BaseID : 0);
			}
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x002B23C0 File Offset: 0x002B05C0
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(SingletonEvent<EndTurnEvent>.ID, 0, false);
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
			Registrar.Register(AfterConversationEvent.ID, 0, false);
			Registrar.Register(PooledEvent<GenericQueryEvent>.ID, 0, false);
			Registrar.Register(PooledEvent<GenericNotifyEvent>.ID, 0, false);
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x002B240E File Offset: 0x002B060E
		public override void RegisterPlayer(GameObject Player, IEventRegistrar Registrar)
		{
			Registrar.Register(EnteringZoneEvent.ID, 0, false);
			Registrar.Register(PooledEvent<BeforePilotChangeEvent>.ID, 0, false);
		}

		// Token: 0x0600722F RID: 29231 RVA: 0x002B242C File Offset: 0x002B062C
		public override bool HandleEvent(EnteringZoneEvent E)
		{
			if (this.Stage == 2)
			{
				Cell origin = E.Origin;
				if (((origin != null) ? origin.ParentZone : null) == The.ActiveZone && E.Cell.ParentZone != The.ActiveZone && !E.System)
				{
					return E.Object.ShowFailure("You can't leave the golem while it is ascending.");
				}
			}
			if (E.Object.IsPlayer())
			{
				if (this.Stage == 0)
				{
					if (base.Game.HasQuest("Reclamation"))
					{
						this.Stage = 1;
					}
					else if (this.SpindleZones.Contains(E.Cell.ParentZone.ZoneID))
					{
						The.Game.StartQuest("Reclamation", null, null, null);
						this.Stage = 1;
					}
				}
				else if (this.Stage == 1 && base.Game.HasFinishedQuest("Reclamation") && E.Cell.ParentZone.ZoneID == "JoppaWorld.53.3.1.1.10")
				{
					CheckpointingSystem.ManualCheckpoint();
				}
			}
			else if (E.Object.Blueprint == "Barathrum")
			{
				if (base.Quest == null || base.Quest.Finished || this.Stage >= 2)
				{
					E.Object.UnregisterEvent(this, EnteringZoneEvent.ID);
				}
				else
				{
					InteriorZone interiorZone = E.Cell.ParentZone as InteriorZone;
					if (interiorZone == null || !interiorZone.ParentObject.HasTagOrProperty("Golem"))
					{
						if (base.Quest.IsStepFinished("Barathrum"))
						{
							The.Game.FailQuestStep(base.Quest, "Barathrum", false);
						}
					}
					else
					{
						The.Game.FinishQuestStep(base.Quest, "Barathrum", -1, true, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007230 RID: 29232 RVA: 0x002B2600 File Offset: 0x002B0800
		public bool CanFallbackWait()
		{
			return !base.Game.HasBooleanGameState("ReadTornSheet") && !base.Game.HasBooleanGameState("SawCrimePunishment") && !base.Game.HasBooleanGameState("MakHatesResheph") && !base.Game.HasBooleanGameState("UrsineApprenticeMentioned") && (!base.Game.HasBooleanGameState("TalkedToYlaHaj") || !JournalAPI.HasSultanNoteWithTag("rebekahWasHealer")) && (base.Game.HasBooleanGameState("WaitArrivePartial1") && base.Game.HasBooleanGameState("WaitArrivePartial2") && base.Game.HasBooleanGameState("WaitArrivePartial3")) && base.Game.HasBooleanGameState("WaitArrivePartial4");
		}

		// Token: 0x06007231 RID: 29233 RVA: 0x002B26C0 File Offset: 0x002B08C0
		public override bool HandleEvent(AfterConversationEvent E)
		{
			if (this.Stage == 2 && base.Game.TimeTicks < this.ArrivalTime && (E.Conversation.HasState("WaitArrive") || this.CanFallbackWait()) && Popup.ShowYesNo("The control pit dissolves to silence.\n\nDo you want to wait until the ascent is done?", "Sounds/UI/ui_notification", true, DialogResult.Cancel, null) == DialogResult.Yes)
			{
				AutoAct.Setting = ":" + (base.Game.Turns + (this.ArrivalTime - base.Game.TimeTicks) + 1L).ToString();
				The.Player.PassTurn();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007232 RID: 29234 RVA: 0x002B2764 File Offset: 0x002B0964
		public override bool HandleEvent(GenericQueryEvent E)
		{
			Interior interior;
			if (E.Query == "AscendQueryGolem" && E.Subject.TryGetPart<Interior>(out interior) && !GameObject.Validate(interior.Zone.FindObject("Barathrum")) && Popup.ShowYesNo("Barathrum is not in the golem. Ascend anyway?", "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
			{
				E.Result = false;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007233 RID: 29235 RVA: 0x002B27D0 File Offset: 0x002B09D0
		public override bool HandleEvent(GenericNotifyEvent E)
		{
			Vehicle vehicle;
			if (E.Notify == "AscendNotifyGolem" && E.Subject.TryGetPart<Vehicle>(out vehicle))
			{
				SoundManager.PlaySound("sfx_grenade_highExplosive_explode", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				CombatJuice.cameraShake(3f, false);
				FadeToBlack.FadeOut(2f, false);
				Thread.Sleep(2000);
				SoundManager.PlaySound("sfx_interact_generictechitem_activate", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				Thread.Sleep(1000);
				SoundManager.PlaySound("sfx_grenade_highExplosive_explode", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				Thread.Sleep(2000);
				SoundManager.PlaySound("sfx_interact_generictechitem_activate", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				Thread.Sleep(2000);
				CombatJuice.cameraShake(3f, false);
				SoundManager.PlaySound("sfx_grenade_highExplosive_explode", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				vehicle.Pilot = null;
				this.Stage = 2;
				this.Climber = E.Subject;
				this.Climber.RequirePart<NoDamage>(false);
				this.ArrivalTime = base.Game.TimeTicks + 500L;
				The.Game.SetIntGameState("StarfreightAscending", 1);
				SoundManager.PlayMusic("Lattice Loops First", "music", true, 12f, 1f, null, true);
				this.LeaveBehindFollowers();
				GameObject barathrum = this.GetBarathrum();
				if (barathrum != null && barathrum.IsPlayerLed())
				{
					barathrum.Brain.Goals.Clear();
					barathrum.RemovePartsDescendedFrom<AIBehaviorPart>();
					barathrum.RequirePart<PriorityChat>(false);
					barathrum.RequirePart<AIBarathrumShuttle>(false).PushGoal(barathrum);
				}
				else
				{
					base.Game.SetIntGameState("StarfreightAskWait", 1);
				}
				this.StartAscendAnimation();
				FadeToBlack.FadeIn(3f);
			}
			else if (E.Notify == "AscentMilestone")
			{
				AscensionSystem.PlayLatticeSecond();
			}
			else if (E.Notify == "AscentAdvance")
			{
				The.Game.TimeTicks = Math.Min(The.Game.TimeTicks + 100L, this.ArrivalTime);
				The.Core.RenderBase(true, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007234 RID: 29236 RVA: 0x002B2A1D File Offset: 0x002B0C1D
		public void StartAscendAnimation()
		{
			The.ActiveZone.GetCell(0, 0).AddObject("Widget", null, null, null, null).AddPart<AscensionRenderer>(true, false);
		}

		// Token: 0x06007235 RID: 29237 RVA: 0x002B2A44 File Offset: 0x002B0C44
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (this.Stage == 2)
			{
				if (The.Game.GetBooleanGameState("AscentMilestone", false))
				{
					SoundManager.PlayMusic("Lattice Loops Second", "music", true, 12f, 1f, null, true);
				}
				else
				{
					SoundManager.PlayMusic("Lattice Loops First", "music", true, 12f, 1f, null, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007236 RID: 29238 RVA: 0x002B2AB0 File Offset: 0x002B0CB0
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Stage == 2)
			{
				if (base.Game.TimeTicks >= this.ArrivalTime)
				{
					this.Stage = 3;
					The.Game.RemoveIntGameState("StarfreightAscending");
					The.Game.RemoveIntGameState("StarfreightAskWait");
					this.Arrival();
				}
				else if (base.Game.HasIntGameState("StarfreightAskWait") && this.ArrivalTime - base.Game.TimeTicks <= 490L && !AutoAct.IsActive(false))
				{
					The.Game.RemoveIntGameState("StarfreightAskWait");
					if (Popup.ShowYesNo("Do you want to wait until the ascent is done?", "Sounds/UI/ui_notification", true, DialogResult.Cancel, null) == DialogResult.Yes)
					{
						AutoAct.Setting = ":" + (base.Game.Turns + (this.ArrivalTime - base.Game.TimeTicks) + 1L).ToString();
						The.Player.PassTurn();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007237 RID: 29239 RVA: 0x002B2BB0 File Offset: 0x002B0DB0
		public override bool HandleEvent(BeforePilotChangeEvent E)
		{
			if (this.Stage == 2 && E.NewPilot != null && E.NewPilot.IsPlayer() && E.Vehicle.HasTagOrProperty("Golem"))
			{
				return E.NewPilot.ShowFailure("You can't pilot the golem while it is ascending.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007238 RID: 29240 RVA: 0x002B2C08 File Offset: 0x002B0E08
		public override void Start()
		{
			GameObject gameObject = The.ActiveZone.FindFirstObject("Barathrum");
			if (gameObject == null)
			{
				MetricsManager.LogError("StarfreightQuest::Could not find Barathrum to set up quest tracking.");
				return;
			}
			gameObject.RegisterEvent(this, EnteringZoneEvent.ID, 0, true);
			gameObject.SetAlliedLeader<AllyAscend>(The.Player, 0, false);
		}

		// Token: 0x06007239 RID: 29241 RVA: 0x002B2C50 File Offset: 0x002B0E50
		public override void Finish()
		{
			GameObject gameObject = The.ActiveZone.FindFirstObject("Barathrum");
			if (gameObject != null)
			{
				gameObject.UnregisterEvent(this, EnteringZoneEvent.ID);
			}
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x002B2C7C File Offset: 0x002B0E7C
		public void Arrival()
		{
			AscensionSystem.<>c__DisplayClass35_0 CS$<>8__locals1 = new AscensionSystem.<>c__DisplayClass35_0();
			CS$<>8__locals1.<>4__this = this;
			AutoAct.Interrupt(null, null, null, false);
			GameObject climber = this.Climber;
			if (climber != null)
			{
				climber.RemovePart<NoDamage>();
			}
			The.Game.SetBooleanGameState("SpindleAscended", true);
			CS$<>8__locals1.interior = (InteriorZone)The.ActiveZone;
			Location2D location = CS$<>8__locals1.interior.ParentObject.CurrentCell.Location;
			CS$<>8__locals1.zone = The.ZoneManager.GetZone("NorthSheva.39.13.1.0.10");
			List<GameObject> list = CS$<>8__locals1.zone.FindObjectsWithPart("AscensionCable");
			CS$<>8__locals1.selected = list.GetRandomElement<GameObject>(null);
			int num = int.MaxValue;
			foreach (GameObject gameObject in list)
			{
				int num2 = gameObject.CurrentCell.Location.Distance(location);
				if (num2 < num)
				{
					CS$<>8__locals1.selected = gameObject;
					num = num2;
				}
			}
			The.Core.RenderBase(true, false);
			CS$<>8__locals1.barathrum = null;
			CS$<>8__locals1.barathrumAvailable = false;
			if (!AscensionSystem.DISABLE_ANIMATIONS)
			{
				GameManager.Instance.PushGameView("Cinematic", true);
				AmbientSoundsSystem.StopAmbientBeds(true);
				Task.Run(delegate
				{
					Action action;
					if ((action = CS$<>8__locals1.<>9__2) == null)
					{
						action = (CS$<>8__locals1.<>9__2 = delegate
						{
							base.<Arrival>g__PlayerArrive|0();
						});
					}
					return StarfreightClimbAnimation.RunSpindleIntroSequence(action);
				}).Wait();
				The.Core.RenderDelay(9000, false, true, false);
				AmbientSoundsSystem.PlayAmbientBeds(CS$<>8__locals1.zone, null, null, null, null);
				FadeToBlack.FadeIn(3f);
				SoundManager.PlayMusic("Music/Another World", "music", true, 12f, 1f, null, true);
				The.Core.RenderDelay(9000, false, true, false);
				GameManager.Instance.PopGameView(false);
			}
			else
			{
				CS$<>8__locals1.<Arrival>g__PlayerArrive|0();
				The.Core.RenderBase(true, false);
			}
			The.Game.FinishQuestStep(base.Quest, "Ascend", -1, false, null);
			The.Game.FinishQuest(base.Quest);
			if (CS$<>8__locals1.barathrumAvailable)
			{
				this.BarathrumStartConversation(CS$<>8__locals1.barathrum);
				Preacher preacher = CS$<>8__locals1.barathrum.RequirePart<Preacher>(false);
				preacher.ChatWait = Stat.Random(20, 30);
				preacher.Lines = new string[] { "Heh heh heh!", "Ho, ho!", "Heh, heh!" };
			}
			The.Player.ForfeitTurn(false);
		}

		// Token: 0x0600723B RID: 29243 RVA: 0x002B2EDC File Offset: 0x002B10DC
		public void LeaveBehindFollowers()
		{
			GameObject player = The.Player;
			foreach (KeyValuePair<string, Zone> keyValuePair in The.ZoneManager.CachedZones)
			{
				string text;
				Zone zone;
				keyValuePair.Deconstruct(out text, out zone);
				Zone zone2 = zone;
				InteriorZone interiorZone = zone2 as InteriorZone;
				if (interiorZone == null || !interiorZone.ParentObject.HasTagOrProperty("Golem"))
				{
					foreach (GameObject gameObject in zone2.IterateObjects())
					{
						if (gameObject.PartyLeader == player && !gameObject.HasTagOrProperty("Golem"))
						{
							gameObject.PartyLeader = null;
						}
					}
				}
			}
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x002B2FA8 File Offset: 0x002B11A8
		public GameObject GetBarathrum()
		{
			foreach (KeyValuePair<string, Zone> keyValuePair in The.ZoneManager.CachedZones)
			{
				string text;
				Zone zone;
				keyValuePair.Deconstruct(out text, out zone);
				foreach (GameObject gameObject in zone.IterateObjects())
				{
					if (!(gameObject.Blueprint != "Barathrum"))
					{
						return gameObject;
					}
				}
			}
			return null;
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x002B3048 File Offset: 0x002B1248
		public void BarathrumStartConversation(GameObject Object)
		{
			Cell currentCell = Object.CurrentCell;
			Cell playerCell = The.PlayerCell;
			if (!currentCell.IsAdjacentTo(playerCell, true))
			{
				FindPath findPath = new FindPath(currentCell, playerCell, false, true, null, 100, false, false, false, false, false, null);
				if (findPath.Found)
				{
					int num = 0;
					int num2 = findPath.Directions.Count - 1;
					while (num < num2 && Object.Move(findPath.Directions[num], false, false, false, false, true, true, null, null, false, new int?(0), null, null, false, false, null, null, 0))
					{
						The.Core.RenderDelay(500, false, true, false);
						num++;
					}
				}
			}
			ConversationScript conversationScript;
			if (Object.TryGetPart<ConversationScript>(out conversationScript))
			{
				conversationScript.AttemptConversation(true, null, null);
				Object.RemovePart<PriorityChat>();
				Object.Brain.SetPartyLeader(null, 0, false, false);
				Object.Brain.Goals.Clear();
				AIBarathrumShuttle aibarathrumShuttle = Object.GetPart<AIBarathrumShuttle>();
				if (aibarathrumShuttle == null)
				{
					Object.RemovePartsDescendedFrom<AIBehaviorPart>();
					aibarathrumShuttle = Object.AddPart<AIBarathrumShuttle>(true, false);
				}
				aibarathrumShuttle.Stage = 0;
				aibarathrumShuttle.PushGoal(Object);
				Popup.Show(Object.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " left your party.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x002B31AC File Offset: 0x002B13AC
		public static void WishSetBarathrumState()
		{
			The.Game.SetBooleanGameState("ReadTornSheet", true);
			The.Game.SetBooleanGameState("SawCrimePunishment", true);
			The.Game.SetBooleanGameState("MakHatesResheph", true);
			The.Game.SetBooleanGameState("UrsineApprenticeMentioned", true);
			The.Game.SetBooleanGameState("TalkedToYlaHaj", true);
			JournalSultanNote firstSultanNoteWithTag = JournalAPI.GetFirstSultanNoteWithTag("rebekahWasHealer");
			if (firstSultanNoteWithTag == null)
			{
				return;
			}
			firstSultanNoteWithTag.Reveal(null, true);
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x002B3220 File Offset: 0x002B1420
		[WishCommand("starfreight:spindle", null)]
		public static void WishStart()
		{
			XRLGame game = The.Game;
			GameObject body = game.Player.Body;
			Zone zone = The.ZoneManager.GetZone("JoppaWorld.53.3.1.1.10");
			Popup.Suppress = true;
			ItemNaming.Suppress = true;
			body.SystemMoveTo(zone.GetPullDownLocation(body), null, false, true, false, false);
			if (body.CurrentZone.FindObject("Barathrum") == null)
			{
				body.CurrentCell.getClosestPassableCell().AddObject("Barathrum", null, null, null, null);
			}
			ReclamationSystem.WishStage();
			AscensionSystem.WishSetBarathrumState();
			if (!game.HasFinishedQuest("The Golem"))
			{
				try
				{
					GolemQuestSelection.WishRandomEffectsGolem();
					Popup.Suppress = true;
					game.CompleteQuest("The Golem");
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Starfreight:Start", ex, "game_exception");
				}
			}
			game.StartQuest("We Are Starfreight", null, null, null);
			game.CompleteQuest("Reclamation");
			Popup.Suppress = false;
			ItemNaming.Suppress = false;
		}

		// Token: 0x06007240 RID: 29248 RVA: 0x002B331C File Offset: 0x002B151C
		[WishCommand("starfreight:arrive", null)]
		public static void WishArrive()
		{
			AscensionSystem system = The.Game.GetSystem<AscensionSystem>();
			if (system == null || system.Stage != 2)
			{
				return;
			}
			The.Game.TimeTicks = Math.Max(The.Game.TimeTicks, system.ArrivalTime);
			The.Player.ForfeitTurn(false);
		}

		// Token: 0x06007241 RID: 29249 RVA: 0x002B336B File Offset: 0x002B156B
		[WishCommand("starfreight:arrivefast", null)]
		public static void WishArriveFast()
		{
			AscensionSystem.DISABLE_ANIMATIONS = true;
			AscensionSystem.WishArrive();
		}

		// Token: 0x06007242 RID: 29250 RVA: 0x002B3378 File Offset: 0x002B1578
		[WishCommand("starfreight:anim", null)]
		public static void WishAnimations()
		{
			AscensionSystem.DISABLE_ANIMATIONS = !AscensionSystem.DISABLE_ANIMATIONS;
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x002B3387 File Offset: 0x002B1587
		[WishCommand("arrivaltest", null)]
		public void ArrivalTest()
		{
			Task.Run(() => StarfreightClimbAnimation.RunSpindleIntroSequence(delegate
			{
			})).Wait();
			The.Core.RenderDelay(6000, false, true, false);
		}

		// Token: 0x06007244 RID: 29252 RVA: 0x002B33C5 File Offset: 0x002B15C5
		[WishCommand("lattice1", null)]
		public static void PlayLatticeFirst()
		{
			SoundManager.PlayMusic("Lattice Loops First", "music", true, 12f, 1f, null, true);
		}

		// Token: 0x06007245 RID: 29253 RVA: 0x002B33E3 File Offset: 0x002B15E3
		[WishCommand("lattice2", null)]
		public static void PlayLatticeSecond()
		{
			GameManager.Instance.uiQueue.queueTask(async delegate
			{
				AudioEntrySet set = SoundManager.GetClipSet("Lattice Loops Second", false);
				if (!set.Initialized)
				{
					await set.Load();
				}
				AudioClip clip = set.Next().Clip;
				MusicSource musicSource = SoundManager.RequireMusicSource("music", false);
				MusicSource musicSource2 = musicSource;
				AudioSource audioSource = musicSource.Audio;
				float time = audioSource.time;
				int timeSamples = audioSource.timeSamples;
				if (audioSource.isPlaying)
				{
					SoundManager.MusicSources.Remove("music");
					musicSource2.Fade.StartFade(4f);
					musicSource2.Channel = "Crossfade";
					musicSource = SoundManager.RequireMusicSource("music", true);
					audioSource = musicSource.Audio;
				}
				musicSource.Track = "Lattice Loops Second";
				musicSource.EntrySet = set;
				musicSource.TargetVolume = musicSource2.TargetVolume;
				audioSource.Stop();
				audioSource.volume = 0f;
				audioSource.clip = clip;
				audioSource.timeSamples = timeSamples % clip.samples;
				audioSource.Play();
				musicSource.EventTime = AudioSettings.dspTime + (double)clip.length - (double)(time % clip.length);
			}, 0);
		}

		// Token: 0x04003500 RID: 13568
		public static bool DISABLE_ANIMATIONS;

		// Token: 0x04003501 RID: 13569
		public const string BARATHRUM_STEP_ID = "Barathrum";

		// Token: 0x04003502 RID: 13570
		public const string ASCEND_STEP_ID = "Ascend";

		// Token: 0x04003503 RID: 13571
		public const string ARRIVAL_ZONE = "NorthSheva.39.13.1.0.10";

		// Token: 0x04003504 RID: 13572
		public const string MILESTONE_KEY = "AscentMilestone";

		// Token: 0x04003505 RID: 13573
		public const string ADVANCE_KEY = "AscentAdvance";

		// Token: 0x04003506 RID: 13574
		public const int STAGE_START = 0;

		// Token: 0x04003507 RID: 13575
		public const int STAGE_RECLAMATION = 1;

		// Token: 0x04003508 RID: 13576
		public const int STAGE_ASCENDING = 2;

		// Token: 0x04003509 RID: 13577
		public const int STAGE_ARRIVED = 3;

		// Token: 0x0400350A RID: 13578
		public int Stage;

		// Token: 0x0400350B RID: 13579
		public long ArrivalTime;

		// Token: 0x0400350C RID: 13580
		public int ClimberID;

		// Token: 0x0400350D RID: 13581
		[NonSerialized]
		private HashSet<string> _SpindleZones;

		// Token: 0x0400350E RID: 13582
		[NonSerialized]
		private GameObject _Climber;
	}
}
