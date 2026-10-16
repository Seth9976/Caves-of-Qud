using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Text;
using HistoryKit;
using Platform.IO;
using Qud.API;
using Qud.UI;
using UnityEngine;
using UnityEngine.UI;
using XRL.Collections;
using XRL.Core;
using XRL.Messages;
using XRL.Names;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Effects;
using XRL.World.Parts;
using XRL.World.Skills.Cooking;
using XRL.World.Tinkering;
using XRL.World.ZoneBuilders;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BF9 RID: 3065
	[HasWishCommand]
	public class EndGame : IConversationPart
	{
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06007B13 RID: 31507 RVA: 0x002E3394 File Offset: 0x002E1594
		public static bool IsBrightsheol
		{
			get
			{
				return The.Game.GetStringGameState("EndType", "") == "Brightsheol";
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06007B14 RID: 31508 RVA: 0x002E33B4 File Offset: 0x002E15B4
		public static bool IsMarooned
		{
			get
			{
				return The.Game.GetStringGameState("EndType", "") == "Marooned";
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06007B15 RID: 31509 RVA: 0x002E33D4 File Offset: 0x002E15D4
		public static bool IsReturn
		{
			get
			{
				return The.Game.GetStringGameState("EndType", "") == "Return";
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06007B16 RID: 31510 RVA: 0x002E33F4 File Offset: 0x002E15F4
		public static bool IsCovenant
		{
			get
			{
				return The.Game.GetStringGameState("EndType", "") == "Covenant";
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06007B17 RID: 31511 RVA: 0x002E3414 File Offset: 0x002E1614
		public static bool IsAccede
		{
			get
			{
				return The.Game.GetStringGameState("EndType", "") == "Accede";
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06007B18 RID: 31512 RVA: 0x002E3434 File Offset: 0x002E1634
		public static bool IsLaunch
		{
			get
			{
				return The.Game.GetStringGameState("EndType", "") == "Launch";
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06007B19 RID: 31513 RVA: 0x002E3454 File Offset: 0x002E1654
		public static bool IsStarshiib
		{
			get
			{
				return The.Game.HasDelimitedGameState("EndExtra", ',', "Starshiib");
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06007B1A RID: 31514 RVA: 0x002E346C File Offset: 0x002E166C
		public static bool IsArkOpened
		{
			get
			{
				return The.Game.HasDelimitedGameState("EndExtra", ',', "OpenArk");
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06007B1B RID: 31515 RVA: 0x002E3484 File Offset: 0x002E1684
		public static bool IsWithBarathrum
		{
			get
			{
				return The.Game.HasDelimitedGameState("EndExtra", ',', "WithBarathrum");
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06007B1C RID: 31516 RVA: 0x002E349C File Offset: 0x002E169C
		public static bool IsAnyNorthSheva
		{
			get
			{
				return EndGame.IsReturn || EndGame.IsCovenant || EndGame.IsAccede || EndGame.IsLaunch || EndGame.IsMarooned;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06007B1D RID: 31517 RVA: 0x002E34C1 File Offset: 0x002E16C1
		public static bool IsSuper
		{
			get
			{
				return The.Game.GetStringGameState("EndGrade", "") == "Super";
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06007B1E RID: 31518 RVA: 0x002E34E1 File Offset: 0x002E16E1
		public static bool IsUltimate
		{
			get
			{
				return EndGame.IsSuper;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06007B1F RID: 31519 RVA: 0x002E34E8 File Offset: 0x002E16E8
		public static bool IsUltra
		{
			get
			{
				return EndGame.IsSuper;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06007B20 RID: 31520 RVA: 0x002E34EF File Offset: 0x002E16EF
		public static bool IsAnyEnding
		{
			get
			{
				return EndGame.IsBrightsheol || EndGame.IsAnyNorthSheva;
			}
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x002E3500 File Offset: 0x002E1700
		public static bool IsGolemDead()
		{
			List<GameObject> list = VehicleRecord.ResolveRecordsFor(The.Player, null, "Golem");
			if (list.Count <= 0)
			{
				return true;
			}
			bool flag = true;
			foreach (GameObject gameObject in list)
			{
				if (((gameObject != null) ? gameObject.CurrentCell : null) != null)
				{
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06007B22 RID: 31522 RVA: 0x002E3578 File Offset: 0x002E1778
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == EnterElementEvent.ID;
		}

		// Token: 0x06007B23 RID: 31523 RVA: 0x002E3596 File Offset: 0x002E1796
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = (NephalProperties.AllFoiled() ? "{{M|[greater victory]}}" : "{{M|[victory]}}");
			return false;
		}

		// Token: 0x06007B24 RID: 31524 RVA: 0x002E35B4 File Offset: 0x002E17B4
		public override bool HandleEvent(EnterElementEvent E)
		{
			string text = this.Prompt;
			if (text.IsNullOrEmpty())
			{
				text = this.Type.ToUpper();
			}
			string text2 = "";
			string text3 = null;
			if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
			{
				text2 = "\n\nType " + text + " to confirm.";
				text3 = "";
			}
			if (Popup.AskString("End game?" + text2, text3, "Sounds/UI/ui_notification", null, text, text.Length, 0, false, true, new bool?(false)).ToUpper() == text)
			{
				if (!this.Type.IsNullOrEmpty())
				{
					The.Game.SetStringGameState("EndType", this.Type);
				}
				if (!this.Grade.IsNullOrEmpty())
				{
					The.Game.SetStringGameState("EndGrade", this.Grade);
				}
				GameManager.Instance.gameQueue.queueTask(new Action(EndGame.Start), 0);
				return base.HandleEvent(E);
			}
			return false;
		}

		// Token: 0x06007B25 RID: 31525 RVA: 0x002E36A4 File Offset: 0x002E18A4
		public static string ApplyLocation(Zone Z)
		{
			Dictionary<string, CellBlueprint> cellBlueprintsByApplication = WorldFactory.Factory.getWorld("JoppaWorld").CellBlueprintsByApplication;
			string text = EndGame.Regions.GetRandomElement<string>(null);
			CellBlueprint cellBlueprint;
			while (!cellBlueprintsByApplication.TryGetValue("Terrain" + text, out cellBlueprint))
			{
				text = EndGame.Regions.GetRandomElement<string>(null);
			}
			cellBlueprint.LevelBlueprint[1, 1, 10].Builders.ApplyTo(Z, Z.ZoneID, false);
			Z.SetZoneProperty("Region", text);
			return text;
		}

		// Token: 0x06007B26 RID: 31526 RVA: 0x002E3724 File Offset: 0x002E1924
		public static HistoricEntity ApplyVillage(Zone Z, string Region)
		{
			VillageCoda villageCoda = new VillageCoda();
			History sultanHistory = The.Game.sultanHistory;
			sultanHistory.currentYear = (long)(HistoryAPI.GetFlipYear() + Calendar.GetYear() + 1000);
			HistoricEntity historicEntity = VillageCoda.GenerateVillageEntity(Region, EndGame.GetBaseFaction().Name, 400, 900);
			sultanHistory.currentYear = Math.Max(sultanHistory.currentYear, historicEntity.lastYear);
			JournalAPI.AddVillageGospels(historicEntity);
			villageCoda.villageEntity = historicEntity;
			Faction faction = VillageBase.CreateVillageFaction(historicEntity.GetCurrentSnapshot());
			faction.EntityID = historicEntity.id;
			faction.EventID = historicEntity.events[0].id;
			villageCoda.villageFaction = faction.Name;
			villageCoda.SurfaceRevealer = false;
			The.ZoneManager.SetZoneName(Z.ZoneID, historicEntity.Name, null, null, null, null, true, true);
			ZoneManager.ZoneGenerationContext = Z;
			ZoneManager.zoneGenerationContextTier = Z.NewTier;
			ZoneManager.zoneGenerationContextZoneID = Z.ZoneID;
			NameStyle nameStyle = NameStyles.NameStyleTable["Chiliad Qudish"];
			NameScope nameScope = new NameScope
			{
				Name = "General",
				Priority = 10,
				Combine = false
			};
			try
			{
				nameStyle.Scopes.Add(nameScope);
				villageCoda.BuildZone(Z);
			}
			finally
			{
				nameStyle.Scopes.Remove(nameScope);
			}
			return historicEntity;
		}

		// Token: 0x06007B27 RID: 31527 RVA: 0x002E387C File Offset: 0x002E1A7C
		public static Faction GetBaseFaction()
		{
			Faction faction = Factions.Get("Beasts");
			float num = float.MinValue;
			bool isAccede = EndGame.IsAccede;
			foreach (KeyValuePair<string, float> keyValuePair in The.Game.PlayerReputation.ReputationValues)
			{
				Faction ifExists = Factions.GetIfExists(keyValuePair.Key);
				float value = keyValuePair.Value;
				if (ifExists != null && ifExists.Visible && value != num && keyValuePair.Value < num == isAccede && ifExists.AnyMembers(new Predicate<GameObjectBlueprint>(VillageCodaBase.IsVillagerEligible), false))
				{
					faction = ifExists;
					num = keyValuePair.Value;
				}
			}
			return faction;
		}

		// Token: 0x06007B28 RID: 31528 RVA: 0x002E3944 File Offset: 0x002E1B44
		public static void UnlockAchievement()
		{
			Achievement.CAVES_OF_QUD.Unlock();
			if (EndGame.IsBrightsheol)
			{
				Achievement.CROSS_BRIGHTSHEOL.Unlock();
			}
			else if (EndGame.IsCovenant)
			{
				Achievement.END_COVENANT.Unlock();
			}
			else if (EndGame.IsLaunch)
			{
				Achievement.END_LAUNCH.Unlock();
			}
			else if (EndGame.IsReturn)
			{
				Achievement.END_RETURN.Unlock();
			}
			else if (EndGame.IsAccede)
			{
				Achievement.END_ACCEDE.Unlock();
			}
			if (EndGame.IsUltra && EndGame.IsAnyNorthSheva)
			{
				Achievement.UNGYRE.Unlock();
			}
		}

		// Token: 0x06007B29 RID: 31529 RVA: 0x002E39D0 File Offset: 0x002E1BD0
		[WishCommand("gocreditscoda", null)]
		public static void Start()
		{
			EndGame.Start(true);
		}

		// Token: 0x06007B2A RID: 31530 RVA: 0x002E39D8 File Offset: 0x002E1BD8
		public static void Start(bool fade = true)
		{
			if (fade)
			{
				GameManager.Instance.PushGameView("Cinematic", true);
				FadeToBlack.FadeOut(6f, new Color(0f, 0f, 0f, 1f));
				Thread.Sleep(11000);
			}
			EndGame.UnlockAchievement();
			EndGame.RunCredits();
			EndGame.GoCoda();
		}

		// Token: 0x06007B2B RID: 31531 RVA: 0x002E3A34 File Offset: 0x002E1C34
		[WishCommand("gocoda", null)]
		public static void GoCodaWish()
		{
			if (!EndGame.EvaluateState())
			{
				EndGame.PickState();
			}
			EndGame.GoCoda();
		}

		// Token: 0x06007B2C RID: 31532 RVA: 0x002E3A47 File Offset: 0x002E1C47
		[WishCommand("gocredits", null)]
		public static void GoCreditsWish()
		{
			if (!EndGame.EvaluateState())
			{
				EndGame.PickState();
			}
			EndGame.RunCredits();
			FadeToBlack.FadeIn(3f);
		}

		// Token: 0x06007B2D RID: 31533 RVA: 0x002E3A64 File Offset: 0x002E1C64
		[WishCommand("goend", null)]
		public static void GoEndWish()
		{
			if (!EndGame.EvaluateState())
			{
				EndGame.PickState();
			}
			EndGame.Start(false);
		}

		// Token: 0x06007B2E RID: 31534 RVA: 0x002E3A78 File Offset: 0x002E1C78
		public static void RunCredits()
		{
			Task.Run(() => EndGame.RunCreditsAsync()).Wait();
		}

		// Token: 0x06007B2F RID: 31535 RVA: 0x002E3AA4 File Offset: 0x002E1CA4
		public static async Task RunCreditsAsync()
		{
			AmbientSoundsSystem.StopAmbientBeds(true);
			float mintime = 0f;
			ManualResetEvent ev = new ManualResetEvent(false);
			ev.Reset();
			bool runInBackground = false;
			FadeToBlack.FadeIn(3f, new Color(0f, 0f, 0f, 0f));
			Action <>9__3;
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				SoundManager.MusicSources.SetMusicBackground(true);
				SoundManager.PlayMusic("Music/Soundscape I", "music", true, 3f, 1f, null, false);
				runInBackground = Application.runInBackground;
				Application.runInBackground = true;
				GameObject gameObject = UIManager.mainCanvas.transform.Find("EndgameCredits").gameObject;
				gameObject.SetActive(true);
				EndgameCredits component = gameObject.GetComponent<EndgameCredits>();
				BookUI.Books["EndCredits"].Pages[0].Lines.Max<string>((string x) => x.Length);
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
				{
					foreach (string text in BookUI.Books["EndCredits"].Pages[0].Lines)
					{
						utf16ValueStringBuilder.Append(text);
					}
					utf16ValueStringBuilder.Replace("~NBSP", "\u2007");
					string text2 = utf16ValueStringBuilder.ToString();
					component.creditsTextSkin.SetText(text2);
				}
				string text3 = "The End";
				if (EndGame.IsBrightsheol)
				{
					The.Game.SetStringGameState("FinalEndmark", "brightsheol");
					component.SetEndmark("brightsheol", EndGame.IsUltimate);
					if (EndGame.IsUltimate)
					{
						text3 = "You annulled the plagues of the Gyre.\n\nThen you freed the Spindle for another's ascent and crossed into Brightsheol";
					}
					else
					{
						text3 = "You freed the Spindle for another's ascent and crossed into Brightsheol.";
					}
				}
				else if (EndGame.IsMarooned)
				{
					The.Game.SetStringGameState("FinalEndmark", "marooned");
					component.SetEndmark("marooned", EndGame.IsUltimate);
					if (EndGame.IsUltimate)
					{
						text3 = "You annulled the plagues of the Gyre.\n\nThen you destroyed Resheph to save the burgeoning world but marooned yourself at the North Sheva.";
					}
					else
					{
						text3 = "You destroyed Resheph to save the burgeoning world but marooned yourself at the North Sheva.";
					}
				}
				else if (EndGame.IsCovenant)
				{
					The.Game.SetStringGameState("FinalEndmark", "covenant");
					component.SetEndmark("covenant", EndGame.IsUltimate);
					if (EndGame.IsUltimate)
					{
						text3 = "You annulled the plagues of the Gyre.\n\nThen you entered into a covenant with Resheph to help prepare Qud for the Coven's return.";
					}
					else
					{
						text3 = "You entered into a covenant with Resheph to help prepare Qud for the Coven's return.";
					}
				}
				else if (EndGame.IsReturn)
				{
					The.Game.SetStringGameState("FinalEndmark", "return");
					component.SetEndmark("return", EndGame.IsUltimate);
					if (EndGame.IsArkOpened)
					{
						if (EndGame.IsUltimate)
						{
							text3 = "You annulled the plagues of the Gyre.\n\nThen you destroyed Resheph and returned to Qud to help garden the burgeoning world.";
						}
						else
						{
							text3 = "You destroyed Resheph and returned to Qud to help garden the burgeoning world.";
						}
					}
					else if (EndGame.IsUltimate)
					{
						text3 = "You annulled the plagues of the Gyre.\n\nThen you returned to Qud to help garden the burgeoning world.";
					}
					else
					{
						text3 = "You rebuked Resheph and returned to Qud to help garden the burgeoning world.";
					}
				}
				else if (EndGame.IsAccede)
				{
					The.Game.SetStringGameState("FinalEndmark", "accede");
					component.SetEndmark("accede", EndGame.IsUltimate);
					if (EndGame.IsUltimate)
					{
						text3 = "You annulled the plagues of the Gyre.\n\nThen you reversed course and acceded to Resheph's plan to purge the world of higher life, in preparation for the Coven's return.";
					}
					else
					{
						text3 = "You acceded to Resheph's plan to purge the world of higher life in preparation for the Coven's return.";
					}
				}
				else if (EndGame.IsLaunch)
				{
					The.Game.SetStringGameState("FinalEndmark", "spaceship");
					component.SetEndmark("spaceship", EndGame.IsUltimate);
					if (EndGame.IsArkOpened)
					{
						if (EndGame.IsWithBarathrum)
						{
							if (EndGame.IsUltimate)
							{
								text3 = "You annulled the plagues of the Gyre.\n\nThen you destroyed Resheph and launched yourself into the dusted cosmos to ply the stars with Barathrum.";
							}
							else
							{
								text3 = "You destroyed Resheph and launched yourself into the dusted cosmos to ply the stars with Barathrum.";
							}
						}
						else if (EndGame.IsUltimate)
						{
							text3 = "You annulled the plagues of the Gyre.\n\nThen you destroyed Resheph and launched  yourself into the dusted cosmos to ply the stars.";
						}
						else
						{
							text3 = "You destroyed Resheph and launched yourself into the dusted cosmos to ply the stars.";
						}
					}
					else if (EndGame.IsWithBarathrum)
					{
						if (EndGame.IsUltimate)
						{
							text3 = "You annulled the plagues of the Gyre.\n\nThen you launched yourself into the dusted cosmos to ply the stars with Barathrum.";
						}
						else
						{
							text3 = "You launched yourself into the dusted cosmos to ply the stars with Barathrum.";
						}
					}
					else if (EndGame.IsUltimate)
					{
						text3 = "You annulled the plagues of the Gyre.\n\nThen you launched yourself into the dusted cosmos to ply the stars.";
					}
					else
					{
						text3 = "You launched yourself into the dusted cosmos to ply the stars.";
					}
				}
				else
				{
					The.Game.SetStringGameState("FinalEndmark", "tombstone");
					component.SetEndmark("tombstone", EndGame.IsUltimate);
				}
				The.Game.SetStringGameState("EndgameCause", text3);
				component.tombstoneText.SetText("\n" + text3);
				mintime = component.scrollTime;
				EndgameCredits endgameCredits = component;
				Action action;
				if ((action = <>9__3) == null)
				{
					action = (<>9__3 = delegate
					{
						ev.Set();
						SoundManager.PlayMusic(null, "music", false, 12f, 1f, null, true);
						SoundManager.PlayMusic(null, "music", false, 12f, 1f, null, true);
						MetricsManager.LogInfo("endgame credits complete");
						SoundManager.MusicSources.SetMusicBackground(Options.MusicBackground);
					});
				}
				endgameCredits.complete = action;
				component.runtime = 0f;
				component.testRun = true;
			}, 0);
			ev.WaitOne(145000);
			Thread.Sleep(3000);
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				Application.runInBackground = runInBackground;
				GameObject gameObject2 = UIManager.mainCanvas.transform.Find("EndgameCredits").gameObject;
				FadeToBlack.FadeNow(0f, new float?((float)1), new float?((float)1), new Color?(Color.black), new Color?(Color.black));
				gameObject2.SetActive(false);
			}, 0);
		}

		// Token: 0x06007B30 RID: 31536 RVA: 0x002E3AE0 File Offset: 0x002E1CE0
		public static GameObject GetSultanBlueprint()
		{
			GameObject gameObject = The.Player;
			if (gameObject.IsOriginalPlayerBody())
			{
				return gameObject;
			}
			Dominated dominated;
			if (gameObject.TryGetEffect<Dominated>(out dominated) && GameObject.Validate(dominated.Dominator))
			{
				gameObject = dominated.Dominator;
				if (gameObject.IsOriginalPlayerBody())
				{
					return gameObject;
				}
			}
			Vehicle vehicle;
			if (gameObject.TryGetPart<Vehicle>(out vehicle) && GameObject.Validate(vehicle.Pilot))
			{
				gameObject = vehicle.Pilot;
				if (gameObject.IsOriginalPlayerBody())
				{
					return gameObject;
				}
			}
			foreach (KeyValuePair<string, Zone> keyValuePair in The.ZoneManager.CachedZones)
			{
				string text;
				Zone zone;
				keyValuePair.Deconstruct(out text, out zone);
				Zone.ObjectEnumerator enumerator2 = zone.IterateObjects().GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Current.IsOriginalPlayerBody())
					{
						return gameObject;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x06007B31 RID: 31537 RVA: 0x002E3BD0 File Offset: 0x002E1DD0
		public static void GoCoda()
		{
			FadeToBlack.Fade(0f, new float?((float)1), new float?((float)1), new Color?(Color.black), new Color?(Color.black));
			AmbientSoundsSystem.StopAmbientBeds(true);
			GameManager.Instance.PushGameView("Empty", true);
			Loading.SetHideLoadStatus(true);
			Popup.Suppress = true;
			try
			{
				The.Core.StashScore();
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Stashing Score for Coda", ex, "game_exception");
			}
			try
			{
				The.Game.IsCoda = true;
				EndGame.EvaluateState();
				CodaSystem codaSystem = The.Game.RequireSystem<CodaSystem>();
				codaSystem.Sultan = EndGame.GetSultanBlueprint();
				codaSystem.EndTime = Calendar.TotalTimeTicks;
				VillageCoda.GenerateSultanEntity(codaSystem.Sultan);
				ManualResetEvent ev = new ManualResetEvent(false);
				Action <>9__1;
				GameManager.Instance.uiQueue.queueTask(delegate
				{
					Action action;
					if ((action = <>9__1) == null)
					{
						action = (<>9__1 = delegate
						{
							ev.Set();
						});
					}
					EndGame.TransitionText(action);
				}, 0);
				ev.WaitOne();
				GameObject gameObject = EndGame.CreatePlayer();
				gameObject.SystemMoveTo(The.PlayerCell, null, false, true, false, false);
				The.Game.Player.Body = gameObject;
				long num = XRLCore.FrameTimer.ElapsedMilliseconds + 6000L;
				Interior interior;
				if (The.ZoneManager.GetZone("JoppaWorld.11.22.1.1.10").GetCell(0, 0).AddObject("CodaHost", null, null, null, null)
					.TryGetPart<Interior>(out interior))
				{
					string text = EndGame.ApplyLocation(interior.Zone);
					HistoricEntity historicEntity = EndGame.ApplyVillage(interior.Zone, text);
					EndGame.EvaluateFactionReputation();
					MetricsManager.LogInfo("Coda region: " + text);
					gameObject.Brain.TakeBaseAllegiance(The.Player);
					interior.Zone.SetMusic("Music/Onward Romantique");
					Cell pullDownLocation = interior.Zone.GetPullDownLocation(gameObject);
					gameObject.SystemMoveTo(pullDownLocation, null, false, true, false, false);
					gameObject.GiveProperName(null, false, null, false, null, null, null, null);
					EndGame.ClearHistory();
					The.Game.TimeTicks += (long)(438000000 + The.Game.TimeOffset);
					EndGame.ArriveAccomplishment(historicEntity.Name);
					The.Game.SaveGame("Primary", "Saving game", false, false);
					The.Game.Checkpoint();
					EndGame.SaveCoda();
				}
				while (XRLCore.FrameTimer.ElapsedMilliseconds < num)
				{
					The.Core.RenderBase(true, false);
				}
			}
			finally
			{
				if (GameManager.Instance.CurrentGameView == "Empty")
				{
					GameManager.Instance.PopGameView(false);
				}
				float num2 = 3f;
				float? num3 = new float?(0f);
				FadeToBlack.Fade(num2, null, num3, null, null);
				Loading.SetHideLoadStatus(false);
				Popup.Suppress = false;
			}
		}

		// Token: 0x06007B32 RID: 31538 RVA: 0x002E3EDC File Offset: 0x002E20DC
		public static void ArriveAccomplishment(string VillageName)
		{
			OpeningStory.AddAccomplishment(VillageName);
		}

		// Token: 0x06007B33 RID: 31539 RVA: 0x002E3EE4 File Offset: 0x002E20E4
		public static void SaveCoda()
		{
			string cacheDirectory = The.Game.GetCacheDirectory("coda.sav.gz");
			if (!File.Exists(cacheDirectory))
			{
				string text = DataManager.SyncedPath("Codas/");
				The.Game.SaveGame("coda", "Saving game", false, false);
				Directory.CreateDirectory(text);
				File.Copy(cacheDirectory, Path.Combine(new string[]
				{
					text,
					"coda-" + The.Game.GameID.ToLower() + ".sav.gz"
				}), false);
			}
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x002E3F6C File Offset: 0x002E216C
		public static void ClearHistory()
		{
			try
			{
				StringMap<Quest> quests = The.Game.Quests;
				if (quests != null)
				{
					quests.Clear();
				}
				XRLCore.Core.Game.Player.Messages.Messages.Clear();
				foreach (IBaseJournalEntry baseJournalEntry in JournalAPI.GetAllNotes())
				{
					baseJournalEntry.Revealed = false;
				}
				JournalScreen.ResetRawHash();
				MessageQueue.UnityMessages.Clear();
				MessageLogWindow.GameInit();
				List<TinkerData> knownRecipes = TinkerData.KnownRecipes;
				if (knownRecipes != null)
				{
					knownRecipes.Clear();
				}
				CookingGameState instance = CookingGameState.instance;
				if (instance != null)
				{
					List<CookingRecipe> knownRecipies = instance.knownRecipies;
					if (knownRecipies != null)
					{
						knownRecipies.Clear();
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("HistoryClear", ex, "game_exception");
			}
		}

		// Token: 0x06007B35 RID: 31541 RVA: 0x002E404C File Offset: 0x002E224C
		public static void TransitionText(Action action = null)
		{
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/CodaText"), GameObject.Find("FadeToBlack").gameObject.transform);
			gameObject.SetActive(false);
			FadeText fadeText = gameObject.AddComponent<FadeText>();
			fadeText.After = action;
			fadeText.Skin = gameObject.GetComponent<Image>();
			fadeText.FadeIn = 2f;
			fadeText.Hold = 1f;
			fadeText.FadeOut = 2f;
			fadeText.EndDestroy = false;
			gameObject.SetActive(true);
			(gameObject.transform as RectTransform).anchoredPosition = new Vector3(0f, 0f, 1f);
			gameObject.transform.SetAsLastSibling();
		}

		// Token: 0x06007B36 RID: 31542 RVA: 0x002E4100 File Offset: 0x002E2300
		protected static void PostprocessPlayerItem(GameObject Object)
		{
			Object.Seen();
			Object.MakeUnderstood(false);
			EnergyCellSocket part = Object.GetPart<EnergyCellSocket>();
			if (part != null)
			{
				GameObject cell = part.Cell;
				if (cell != null)
				{
					cell.MakeUnderstood(false);
				}
			}
			MagazineAmmoLoader part2 = Object.GetPart<MagazineAmmoLoader>();
			if (part2 == null)
			{
				return;
			}
			GameObject ammo = part2.Ammo;
			if (ammo == null)
			{
				return;
			}
			ammo.MakeUnderstood(false);
		}

		// Token: 0x06007B37 RID: 31543 RVA: 0x002E4158 File Offset: 0x002E2358
		public static GameObject CreatePlayer()
		{
			if (VillageCoda.IsRuined)
			{
				return GameObject.Create("GiantAmoeba");
			}
			GameObject gameObject = GameObject.Create("Humanoid");
			GenotypeEntry genotypeEntry = GenotypeFactory.RequireGenotypeEntry("Mutated Human");
			SubtypeEntry randomElement = SubtypeFactory.GetSubtypeClass(genotypeEntry).Categories.GetRandomElement<SubtypeCategory>(null).Subtypes.GetRandomElement<SubtypeEntry>(null);
			gameObject.Render.Tile = (EndGame.CallingTiles.ContainsKey(randomElement.Name) ? EndGame.CallingTiles[randomElement.Name] : "Creatures/coda_stranger.png");
			gameObject.Render.DetailColor = randomElement.DetailColor.Coalesce(gameObject.Render.DetailColor);
			gameObject.SetStringProperty("Genotype", genotypeEntry.Name, false);
			gameObject.SetStringProperty("Culture", "Chiliad Qudish", false);
			gameObject.RequirePart<Description>(false).Short = "It's you.";
			string text;
			if (EndGame.CallingAliases.TryGetValue(randomElement.Name, out text))
			{
				gameObject.SetStringProperty("Subtype", text, false);
			}
			else
			{
				gameObject.SetStringProperty("Subtype", randomElement.Name, false);
			}
			if (!genotypeEntry.Stats.IsNullOrEmpty<KeyValuePair<string, GenotypeStat>>())
			{
				foreach (KeyValuePair<string, GenotypeStat> keyValuePair in genotypeEntry.Stats)
				{
					string text2;
					GenotypeStat genotypeStat;
					keyValuePair.Deconstruct(out text2, out genotypeStat);
					string text3 = text2;
					GenotypeStat genotypeStat2 = genotypeStat;
					gameObject.GetStat(text3).BaseValue = Stat.Random(genotypeStat2.Minimum, genotypeStat2.Maximum);
				}
			}
			if (!randomElement.Stats.IsNullOrEmpty<KeyValuePair<string, SubtypeStat>>())
			{
				foreach (KeyValuePair<string, SubtypeStat> keyValuePair2 in randomElement.Stats)
				{
					string text2;
					SubtypeStat subtypeStat;
					keyValuePair2.Deconstruct(out text2, out subtypeStat);
					string text4 = text2;
					SubtypeStat subtypeStat2 = subtypeStat;
					gameObject.GetStat(text4).BaseValue += subtypeStat2.Bonus;
				}
			}
			genotypeEntry.AddSkills(gameObject, randomElement.RemoveSkills);
			randomElement.AddSkills(gameObject, genotypeEntry.RemoveSkills);
			if (!randomElement.Gear.IsNullOrEmpty())
			{
				string[] array = randomElement.Gear.Split(',', StringSplitOptions.None);
				int i = 0;
				while (i < array.Length)
				{
					string text5 = array[i];
					if (PopulationManager.Populations.ContainsKey(text5))
					{
						using (List<PopulationResult>.Enumerator enumerator3 = PopulationManager.Populations[text5].Generate(new Dictionary<string, string>(), "").GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								PopulationResult populationResult = enumerator3.Current;
								gameObject.ReceiveObject(populationResult.Blueprint, populationResult.Number, false, 0, 0, null, null, new Action<GameObject>(EndGame.PostprocessPlayerItem), null);
							}
							goto IL_02B5;
						}
						goto IL_02A4;
					}
					goto IL_02A4;
					IL_02B5:
					i++;
					continue;
					IL_02A4:
					Debug.LogError("Unknown gear population table: " + text5);
					goto IL_02B5;
				}
				gameObject.CheckStacks();
			}
			int mutationPoints = genotypeEntry.MutationPoints;
			while (mutationPoints > 0 && EndGame.TryAddMutation(gameObject, ref mutationPoints))
			{
			}
			return gameObject;
		}

		// Token: 0x06007B38 RID: 31544 RVA: 0x002E4474 File Offset: 0x002E2674
		private static bool TryAddMutation(GameObject Object, ref int Points)
		{
			Mutations mutations = Object.RequirePart<Mutations>(false);
			List<MutationEntry> mutatePool = mutations.GetMutatePool(null, false);
			mutatePool.ShuffleInPlace<MutationEntry>(null);
			foreach (MutationEntry mutationEntry in mutatePool)
			{
				if (mutationEntry.Cost <= Points)
				{
					mutations.AddMutation(mutationEntry, 1);
					Points -= mutationEntry.Cost;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007B39 RID: 31545 RVA: 0x002E44F8 File Offset: 0x002E26F8
		public static bool EvaluateState()
		{
			if (EndGame.IsReturn || EndGame.IsMarooned)
			{
				The.Game.SetStringGameState("SultanTerm", "freeholder");
				if (!EndGame.IsSuper)
				{
					The.Game.SetIntGameState("CodaPlagued", 1);
				}
			}
			else if (EndGame.IsCovenant)
			{
				The.Game.SetStringGameState("SultanTerm", "Resheph");
			}
			else if (EndGame.IsAccede)
			{
				The.Game.SetIntGameState("CodaRuined", 1);
				The.Game.SetIntGameState("CodaDespised", 1);
			}
			else
			{
				if (!EndGame.IsLaunch)
				{
					return false;
				}
				The.Game.SetIntGameState("CodaStarshiibLocket", 1);
				if (!EndGame.IsSuper)
				{
					if (EndGame.IsArkOpened)
					{
						The.Game.SetIntGameState("CodaPlagued", 1);
					}
					else
					{
						The.Game.SetIntGameState("CodaRuined", 1);
					}
				}
			}
			if (EndGame.IsSuper)
			{
				The.Game.SetStringGameState("CodaStatueMaterial", "Gold");
			}
			if (The.Game.HasFinishedQuest("Raising Indrix"))
			{
				The.Game.SetIntGameState("CodaAmaranthineDust", 1);
			}
			else
			{
				The.Game.SetIntGameState("CodaAmaranthinePrism", 1);
			}
			if (The.Game.HasQuest("If, Then, Else"))
			{
				if (The.Game.HasDelimitedGameState("TauElse", ',', "KilledByPlayer"))
				{
					The.Game.SetIntGameState("CodaDeadTau", 1);
				}
				else if (The.Game.HasIntGameState("ElseingComplete"))
				{
					The.Game.SetIntGameState("CodaTauNoLonger", 1);
				}
				else if (The.Game.HasDelimitedGameState("TauCompanion", ',', "Dead"))
				{
					The.Game.SetIntGameState("CodaWanderingTau", 1);
				}
			}
			if (The.Game.HasFinishedQuest("Love and Fear"))
			{
				The.Game.SetIntGameState("CodaReturnedSonnet", 1);
			}
			else if (The.Game.HasUnfinishedQuest("Love and Fear"))
			{
				The.Game.SetIntGameState("CodaFoundSonnet", 1);
			}
			if (EndGame.IsStarshiib)
			{
				The.Game.SetIntGameState("CodaStarshiibLocket", 1);
			}
			EndGame.EvaluateFactionState();
			return true;
		}

		// Token: 0x06007B3A RID: 31546 RVA: 0x002E470C File Offset: 0x002E290C
		public static void PickState()
		{
			int num = Popup.PickOption("Pick end game state", null, "", "Sounds/UI/ui_notification", new string[] { "Return", "Return Ultra", "Covenant", "Covenant Ultra", "Accede", "Accede Ultra", "Launch", "Launch Ultra", "Marooned", "Marooned Ultra" }, null, null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null);
			if (num == 0)
			{
				The.Game.SetStringGameState("EndType", "Return");
			}
			else if (num == 1)
			{
				The.Game.SetStringGameState("EndType", "Return");
				The.Game.SetStringGameState("EndGrade", "Super");
			}
			else if (num == 2)
			{
				The.Game.SetStringGameState("EndType", "Covenant");
			}
			else if (num == 3)
			{
				The.Game.SetStringGameState("EndType", "Covenant");
				The.Game.SetStringGameState("EndGrade", "Super");
			}
			else if (num == 4)
			{
				The.Game.SetStringGameState("EndType", "Accede");
			}
			else if (num == 5)
			{
				The.Game.SetStringGameState("EndType", "Accede");
				The.Game.SetStringGameState("EndGrade", "Super");
			}
			else if (num == 6)
			{
				The.Game.SetStringGameState("EndType", "Launch");
			}
			else if (num == 7)
			{
				The.Game.SetStringGameState("EndType", "Launch");
				The.Game.SetStringGameState("EndGrade", "Super");
			}
			else if (num == 8)
			{
				The.Game.SetStringGameState("EndType", "Marooned");
			}
			else if (num == 9)
			{
				The.Game.SetStringGameState("EndType", "Marooned");
				The.Game.SetStringGameState("EndGrade", "Super");
			}
			EndGame.EvaluateState();
		}

		// Token: 0x06007B3B RID: 31547 RVA: 0x002E4918 File Offset: 0x002E2B18
		public static bool CheckMarooned()
		{
			if (EndGame.IsArkOpened && EndGame.IsGolemDead() && The.Game.HasGameState("StarshipLaunched"))
			{
				The.Game.SetStringGameState("EndType", "Marooned");
				if (NephalProperties.IsFoiled("Ehalcodon"))
				{
					The.Game.SetStringGameState("EndGrade", "Super");
				}
				EndGame.Start();
				return true;
			}
			return false;
		}

		// Token: 0x06007B3C RID: 31548 RVA: 0x002E4980 File Offset: 0x002E2B80
		private static void EvaluateVisibilityProperty()
		{
			string stringGameState = The.Game.GetStringGameState("EndType", "");
			List<Faction> list = new List<Faction>();
			List<Faction> list2 = new List<Faction>();
			foreach (Faction faction in Factions.GetList())
			{
				bool? flag = null;
				string stringProperty = faction.GetStringProperty("EndVisibility", null);
				if (!stringProperty.IsNullOrEmpty())
				{
					bool flag2;
					if (bool.TryParse(stringProperty, out flag2))
					{
						flag = new bool?(flag2);
					}
					else if (stringProperty == "Conceal")
					{
						list.Add(faction);
					}
					else if (stringProperty == "General")
					{
						list2.Add(faction);
					}
					else if (stringProperty.HasDelimitedSubstring(',', stringGameState, StringComparison.Ordinal))
					{
						flag = new bool?(true);
					}
					else
					{
						flag = new bool?(false);
					}
				}
				string stringProperty2 = faction.GetStringProperty("EndVisibilityState", null);
				if (!stringProperty2.IsNullOrEmpty())
				{
					bool flag3 = The.Game.TestGameState(stringProperty2);
					if (flag != null)
					{
						flag = flag && flag3;
					}
					else
					{
						flag = new bool?(flag3);
					}
				}
				if (flag != null)
				{
					faction.Visible = flag.Value;
				}
			}
			list.Remove(EndGame.GetBaseFaction());
			for (int i = Stat.Random(3, 4); i > 0; i--)
			{
				Faction faction2 = list.RemoveRandomElement<Faction>(null);
				if (faction2 != null)
				{
					faction2.Visible = false;
				}
			}
			for (int j = Stat.Random(3, 4); j > 0; j--)
			{
				Faction faction3 = list2.RemoveRandomElement<Faction>(null);
				if (faction3 != null)
				{
					faction3.Visible = true;
				}
			}
		}

		// Token: 0x06007B3D RID: 31549 RVA: 0x002E4B48 File Offset: 0x002E2D48
		public static void EvaluateFactionState()
		{
			if (VillageCoda.IsRuined)
			{
				using (IEnumerator<Faction> enumerator = Factions.GetList().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Faction faction = enumerator.Current;
						string stringProperty = faction.GetStringProperty("EndVisibility", null);
						faction.Visible = stringProperty == "true" || stringProperty.HasDelimitedSubstring(',', "Ruined", StringComparison.Ordinal);
					}
					return;
				}
			}
			EndGame.EvaluateCults();
			EndGame.EvaluateVillageFactions();
			EndGame.EvaluateSpecific();
			EndGame.EvaluateVisibilityProperty();
		}

		// Token: 0x06007B3E RID: 31550 RVA: 0x002E4BD8 File Offset: 0x002E2DD8
		private static void EvaluateCults()
		{
			BallBag<Faction> ballBag = new BallBag<Faction>(0)
			{
				{
					Factions.Get("SultanCult1"),
					400
				},
				{
					Factions.Get("SultanCult2"),
					350
				},
				{
					Factions.Get("SultanCult3"),
					300
				},
				{
					Factions.Get("SultanCult4"),
					250
				},
				{
					Factions.Get("SultanCult5"),
					200
				}
			};
			for (int i = 3; i > 0; i--)
			{
				ballBag.PickOne().Visible = false;
			}
		}

		// Token: 0x06007B3F RID: 31551 RVA: 0x002E4C70 File Offset: 0x002E2E70
		private static void EvaluateVillageFactions()
		{
			List<Faction> list = new List<Faction>();
			foreach (Faction faction in Factions.GetList())
			{
				if (faction.GetIntProperty("Village", 0) == 1)
				{
					list.Add(faction);
				}
			}
			list.ShuffleInPlace<Faction>(null);
			int i = 0;
			int num = list.Count / 2;
			while (i < num)
			{
				Faction faction2 = list[i];
				faction2.DisplayName = "villagers of " + NameStyles.NameStyleTable["Chiliad Qudish Site"].Generate(null, null, null, null, null, null, null, null, null, null, null, "Site", null, false, false, null, null, null, null, null, null);
				VillageBase.SetVillageFactionEmblem(faction2, faction2.DisplayName);
				i++;
			}
		}

		// Token: 0x06007B40 RID: 31552 RVA: 0x002E4D64 File Offset: 0x002E2F64
		private static void EvaluateSpecific()
		{
			Faction faction = Factions.Get("Barathrumites");
			if (EndGame.IsCovenant && The.Game.HasBooleanGameState("GemaraSophiaSharedGritGate"))
			{
				faction.DisplayName = "Synagogue at Grit Gate";
				faction.Plural = false;
			}
			else if (EndGame.IsCovenant && The.Game.HasBooleanGameState("GemaraSophiaSharedMe"))
			{
				faction.DisplayName = "Tabernacle at Grit Gate";
				faction.Plural = false;
			}
			else if (EndGame.IsCovenant)
			{
				faction.DisplayName = "monastery at Grit Gate";
				faction.Plural = false;
			}
			else if ((EndGame.IsLaunch && EndGame.IsSuper) || EndGame.IsMarooned)
			{
				faction.DisplayName = "Tinkers' Guild";
				faction.Plural = false;
			}
			else if (EndGame.IsReturn)
			{
				faction.DisplayName = "denizens of the Grit Gate Freehold";
			}
			Faction faction2 = Factions.Get("Templar");
			if (EndGame.GetBaseFaction() != faction2)
			{
				faction2.Visible = false;
				faction.Interests.RemoveAll((FactionInterest x) => x.Tags == "templar,lair");
			}
			faction2 = Factions.Get("Mechanimists");
			faction2.DisplayName = "Chromaic Church";
			faction2 = Factions.Get("Hindren");
			string text;
			if (!The.Game.HasBooleanGameState("HindrenQuestFullyResolved") || The.Game.HasBooleanGameState("HindrenVillageDoomed"))
			{
				faction2.Visible = false;
			}
			else if (The.Game.StringGameState.TryGetValue("HindrenMysteryOutcomeHindriarch", out text))
			{
				if (text == "Keh")
				{
					faction2.DisplayName = "Hindriarchy of Bey Lah";
				}
				else if (text == "Esk")
				{
					faction2.DisplayName = "denizens of the Bey Lah Freehold";
				}
			}
			if (The.Game.HasFinishedQuest("Raising Indrix"))
			{
				Factions.Get("Chiliad Mamon").Visible = true;
			}
			if (EndGame.IsSuper)
			{
				faction2 = Factions.Get("Girsh");
				faction2.Visible = false;
				faction2 = Factions.Get("Gyre Wights");
				faction2.Visible = false;
				if (NephalProperties.AnyPacified())
				{
					faction2 = Factions.Get("Nephilim");
					faction2.Visible = true;
				}
			}
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x002E4F70 File Offset: 0x002E3170
		public static void EvaluateFactionReputation()
		{
			foreach (Faction faction in Factions.GetList())
			{
				int num = faction.GetIntProperty("EndReputation", int.MinValue);
				if (num == -2147483648)
				{
					int intProperty = faction.GetIntProperty("EndReputationMin", int.MinValue);
					int intProperty2 = faction.GetIntProperty("EndReputationMax", int.MinValue);
					if (intProperty == -2147483648 || intProperty2 == -2147483648)
					{
						num = faction.InitialPlayerReputation;
					}
					else
					{
						num = (int)Math.Round((double)Stat.Roll(intProperty, intProperty2) / 100.0) * 100;
					}
				}
				The.Game.PlayerReputation.Set(faction, num);
			}
		}

		// Token: 0x04003818 RID: 14360
		private static string[] Regions = new string[] { "Saltmarsh", "DesertCanyon", "Hills" };

		// Token: 0x04003819 RID: 14361
		private static Dictionary<string, string> CallingAliases = new Dictionary<string, string>
		{
			{ "Apostle", "Sibyl" },
			{ "Arconaut", "Caveweird" },
			{ "Greybeard", "Whitebeard" },
			{ "Gunslinger", "Stickwhiz" },
			{ "Marauder", "Corsair" },
			{ "Scholar", "Rabbi" },
			{ "Tinker", "Machinist" },
			{ "Water Merchant", "Waterfreak" },
			{ "Watervine Farmer", "Grassfolk" }
		};

		// Token: 0x0400381A RID: 14362
		private static Dictionary<string, string> CallingTiles = new Dictionary<string, string>
		{
			{ "Apostle", "Creatures/chiliad-sibyl.png" },
			{ "Arconaut", "Creatures/chiliad-caveweird.png" },
			{ "Greybeard", "Creatures/chiliad-whitebeard.png" },
			{ "Gunslinger", "Creatures/chiliad-stickwhiz.png" },
			{ "Marauder", "Creatures/chiliad-corsair.png" },
			{ "Nomad", "Creatures/chiliad-nomad.png" },
			{ "Pilgrim", "Creatures/chiliad-pilgrim.png" },
			{ "Scholar", "Creatures/chiliad-rabbi.png" },
			{ "Tinker", "Creatures/chiliad-machinist.png" },
			{ "Warden", "Creatures/chiliad-warden.png" },
			{ "Water Merchant", "Creatures/chiliad-waterfreak.png" },
			{ "Watervine Farmer", "Creatures/chiliad-grassfolk.png" }
		};

		// Token: 0x0400381B RID: 14363
		public string Prompt;

		// Token: 0x0400381C RID: 14364
		public string Type;

		// Token: 0x0400381D RID: 14365
		public string Grade;
	}
}
