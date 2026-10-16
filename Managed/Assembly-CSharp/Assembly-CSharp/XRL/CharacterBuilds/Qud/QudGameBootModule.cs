using System;
using System.Collections.Generic;
using System.Reflection;
using ConsoleLib.Console;
using Platform.Performance;
using Qud.UI;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005A4 RID: 1444
	public class QudGameBootModule : AbstractEmbarkBuilderModule
	{
		// Token: 0x06003DA1 RID: 15777 RVA: 0x0012DEF8 File Offset: 0x0012C0F8
		public override void InitFromSeed(string seed)
		{
			this.builder.info.GameSeed = seed;
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x0012DF0C File Offset: 0x0012C10C
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECT)
			{
				GameObject gameObject = element as GameObject;
				gameObject.Brain.Goals.Clear();
				gameObject.DisplayName = The.Game.PlayerName;
				gameObject.HasProperName = true;
				gameObject.RequirePart<Description>(false).Short = "It's you.";
				gameObject.AddPart<OpeningStory>(new OpeningStory(), true, false);
				return gameObject;
			}
			return base.handleBootEvent(id, game, info, element);
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x0012DF80 File Offset: 0x0012C180
		public override void Init()
		{
			this.builder.info.GameSeed = Guid.NewGuid().ToString();
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x0012DFB0 File Offset: 0x0012C1B0
		public static void SeedGame(XRLGame Game, EmbarkInfo Info)
		{
			if (!Game.IsSeeded)
			{
				Info.fireBootEvent(QudGameBootModule.BOOTEVENT_GENERATESEEDS, Game);
				Game.SetStringGameState("OriginalWorldSeed", Info.GameSeed);
				Game.IsSeeded = true;
				Stat.ReseedFrom(Info.GameSeed, true);
				Game.RemoveIntGameState("WorldSeed");
				Game.GetWorldSeed(null);
				Info.fireBootEvent(QudGameBootModule.BOOTEVENT_AFTERGENERATESEEDS, Game);
			}
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x0012E014 File Offset: 0x0012C214
		public override void bootGame(XRLGame game, EmbarkInfo info)
		{
			using (CPUBoost.AutoScopeForceMainThread())
			{
				TutorialManager.OnBootGame(game, info);
				MetricsManager.LogInfo("Beginning world build for seed: " + info.GameSeed);
				if (Options.ModernUI)
				{
					bool result = WorldGenerationScreen.ShowWorldGenerationScreen(258).Result;
				}
				XRLCore core = The.Core;
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_BEGINBOOT, game);
				GameManager.Instance.PushGameView("WorldCreationProgress", true);
				Loading.SetHideLoadStatus(true);
				WorldCreationProgress.Begin(7);
				WorldCreationProgress.NextStep("Initializing protocols...", 2);
				WorldCreationProgress.StepProgress("Hardening math...", false);
				game.GameID = Guid.NewGuid().ToString();
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_AFTERBEGINBOOT, game);
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_CACHERESET, game);
				WorldCreationProgress.StepProgress("Planting world seeds...", false);
				game.GetCacheDirectory(null);
				game.bZoned = false;
				The.Core.ResetGameBasedStaticCaches();
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_AFTERCACHERESET, game);
				QudGameBootModule.SeedGame(game, info);
				GameObjectFactory.Factory.DispatchLoadBlueprints(false);
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_INITIALIZESYSTEMS, game);
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_AFTERINITIALIZESYSTEMS, game);
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_INITIALIZEGAMESTATESINGLETONS, game);
				foreach (Type type in ModManager.GetTypesWithAttribute(typeof(GameStateSingleton), false))
				{
					string text = type.GetCustomAttribute<GameStateSingleton>().ID ?? type.Name;
					Stat.ReseedFrom("GAMESTATE" + text, false);
					object obj = Activator.CreateInstance(type);
					game.SetObjectGameState(text, obj);
					IGameStateSingleton gameStateSingleton = obj as IGameStateSingleton;
					if (gameStateSingleton != null)
					{
						gameStateSingleton.Initialize();
						info.GameStateSingletons.Add(gameStateSingleton);
					}
				}
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_AFTERINITIALIZEGAMESTATESINGLETONS, game);
				Stat.ReseedFrom("HISTORYINIT", false);
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_BEFOREINITIALIZEHISTORY, game);
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_INITIALIZEHISTORY, game);
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_AFTERINITIALIZEHISTORY, game);
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_INITIALIZEWORLDS, game);
				WorldCreationProgress.NextStep("Generating topography...", WorldFactory.Factory.countWorlds() * 40);
				Stat.ReseedFrom("NAMEMAP", false);
				WorldFactory.Factory.BuildZoneNameMap();
				Stat.ReseedFrom("BUILDWORLDS", false);
				WorldFactory.Factory.BuildWorlds();
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_AFTERINITIALIZEWORLDS, game);
				WorldCreationProgress.NextStep("Resolving faction relationships...", 2);
				game.Running = true;
				WorldCreationProgress.StepProgress("Adding player to world...", false);
				game.PlayerReputation.Init();
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_BOOTPLAYERREPUTATION, game);
				GlobalLocation globalLocation = info.fireBootEvent<GlobalLocation>(QudGameBootModule.BOOTEVENT_BOOTSTARTINGLOCATION, game, new GlobalLocation());
				string playerName = game.PlayerName;
				if (string.IsNullOrEmpty((playerName != null) ? playerName.Trim() : null))
				{
					game.PlayerName = info.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_GENERATERANDOMPLAYERNAME, game, core.GenerateRandomPlayerName(info.getModule<QudSubtypeModule>().data.Subtype));
				}
				GameObject gameObject = info.fireBootEvent<GameObject>(QudGameBootModule.BOOTEVENT_BEFOREBOOTPLAYEROBJECT, game, null);
				gameObject = info.fireBootEvent<GameObject>(QudGameBootModule.BOOTEVENT_BOOTPLAYEROBJECT, game, gameObject);
				Render render = gameObject.Render;
				string text2 = info.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYERTILE, game, null) ?? render.Tile;
				string text3 = info.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEFOREGROUND, game, null) ?? render.GetForegroundColor();
				string text4 = info.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEBACKGROUND, game, null) ?? render.GetBackgroundColor();
				string text5 = info.fireBootEvent<string>(QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEDETAIL, game, null) ?? render.DetailColor;
				render.Tile = text2;
				render.ColorString = "&" + text3 + "^" + text4;
				render.DetailColor = text5;
				foreach (Type type2 in ModManager.GetTypesWithAttribute(typeof(PlayerMutator), true))
				{
					Stat.ReseedFrom("PLAYERMUTATOR" + type2.Name, false);
					IPlayerMutator playerMutator = Activator.CreateInstance(type2) as IPlayerMutator;
					if (playerMutator != null)
					{
						playerMutator.mutate(gameObject);
					}
				}
				info.fireBootEvent<GameObject>(QudGameBootModule.BOOTEVENT_AFTERBOOTPLAYEROBJECT, game, gameObject);
				MetricsManager.SendTelemetryWithPayload("game_start", "funnel.stages", new Dictionary<string, string>
				{
					{ "GameMode", game.gameMode },
					{
						"ControlType",
						(ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad) ? "gamepad" : "keyboard"
					}
				});
				WorldCreationProgress.StepProgress("Starting game!", false);
				WorldCreationProgress.StepProgress(" ", false);
				WorldCreationProgress.StepProgress("  ", false);
				Stat.ReseedFrom("InitialSeeds", false);
				game.SetIntGameState("RandomSeed", Stat.Rnd.Next());
				Stat.Rnd = new Random(game.GetIntGameState("RandomSeed", 0));
				game.SetIntGameState("RandomSeed2", Stat.Rnd.Next());
				Stat.Rnd2 = new Random(game.GetIntGameState("RandomSeed2", 0));
				game.SetIntGameState("RandomSeed3", Stat.Rnd.Next());
				game.SetIntGameState("RandomSeed4", Stat.Rnd.Next());
				Stat.Rnd4 = new Random(game.GetIntGameState("RandomSeed4", 0));
				Loading.SetHideLoadStatus(false);
				MetricsManager.LogInfo("Cached objects: " + game.ZoneManager.CachedObjects.Count.ToString());
				MemoryHelper.GCCollect();
				Cell cell = globalLocation.ResolveCell();
				if (!cell.IsReachable())
				{
					cell = cell.getClosestReachableCell();
				}
				cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				game.Player.Body = gameObject;
				Keyboard.ClearInput(false, false);
				game.PlayerReputation.InitFeeling();
				info.fireBootEvent(QudGameBootModule.BOOTEVENT_GAMESTARTING, game);
				Stat.ReseedFrom("GameStart", false);
			}
		}

		/// <summary>
		/// fires before anything else happens during game bootup
		/// has no element
		/// </summary>
		// Token: 0x04001E8C RID: 7820
		public static readonly string BOOTEVENT_BEGINBOOT = "BeginBoot";

		/// <summary>
		/// world progress UI is now setup and the game has an ID
		/// has no element
		/// </summary>
		// Token: 0x04001E8D RID: 7821
		public static readonly string BOOTEVENT_AFTERBEGINBOOT = "AfterBeginBoot";

		/// <summary>
		/// caches will be reset
		/// has no element
		/// </summary>
		// Token: 0x04001E8E RID: 7822
		public static readonly string BOOTEVENT_CACHERESET = "CacheReset";

		/// <summary>
		/// caches are now reset
		/// has no element
		/// </summary>
		// Token: 0x04001E8F RID: 7823
		public static readonly string BOOTEVENT_AFTERCACHERESET = "AfterCacheReset";

		/// <summary>
		/// game seeds will be setup
		/// has no element
		/// </summary>
		// Token: 0x04001E90 RID: 7824
		public static readonly string BOOTEVENT_GENERATESEEDS = "GenerateSeeds";

		/// <summary>
		/// game seeds are now setup
		/// has no element
		/// </summary>
		// Token: 0x04001E91 RID: 7825
		public static readonly string BOOTEVENT_AFTERGENERATESEEDS = "AfterGenerateSeeds";

		/// <summary>
		/// typically used to use game.AddSystem(...) your systems
		/// has no element
		/// </summary>
		// Token: 0x04001E92 RID: 7826
		public static readonly string BOOTEVENT_INITIALIZESYSTEMS = "InitializeSystems";

		/// <summary>
		/// typically used to use game.AddSystem(...) your systems
		/// has no element
		/// </summary>
		// Token: 0x04001E93 RID: 7827
		public static readonly string BOOTEVENT_AFTERINITIALIZESYSTEMS = "AfterInitializeSystems";

		/// <summary>
		/// GamestateSingletons are about to be initialized. This will probably be handled rarely.
		/// has no element
		/// </summary>
		// Token: 0x04001E94 RID: 7828
		public static readonly string BOOTEVENT_INITIALIZEGAMESTATESINGLETONS = "InitializeGamestateSingletons";

		/// <summary>
		/// GamestateSingletons have been initialized. This will probably be handled rarely.
		/// has no element
		/// </summary>
		// Token: 0x04001E95 RID: 7829
		public static readonly string BOOTEVENT_AFTERINITIALIZEGAMESTATESINGLETONS = "AfterInitializeGamestateSingletons";

		/// <summary>
		/// worlds are about to be built
		/// has no element
		/// </summary>
		// Token: 0x04001E96 RID: 7830
		public static readonly string BOOTEVENT_BEFOREINITIALIZEHISTORY = "BeforeInitializeHistory";

		/// <summary>
		/// worlds are about to be built
		/// has no element
		/// </summary>
		// Token: 0x04001E97 RID: 7831
		public static readonly string BOOTEVENT_INITIALIZEHISTORY = "InitializeHistory";

		/// <summary>
		/// worlds have been built
		/// element is a HistoryKit.History
		/// </summary>
		// Token: 0x04001E98 RID: 7832
		public static readonly string BOOTEVENT_AFTERINITIALIZEHISTORY = "AfterInitializeHistory";

		/// <summary>
		/// worlds are about to be built
		/// has no element
		/// </summary>
		// Token: 0x04001E99 RID: 7833
		public static readonly string BOOTEVENT_INITIALIZESULTANHISTORY = "InitializeSultanHistory";

		/// <summary>
		/// worlds have been built
		/// element is a HistoryKit.History
		/// </summary>
		// Token: 0x04001E9A RID: 7834
		public static readonly string BOOTEVENT_AFTERINITIALIZESULTANHISTORY = "AfterInitializeSultanHistory";

		/// <summary>
		/// worlds are about to be built
		/// has no element
		/// </summary>
		// Token: 0x04001E9B RID: 7835
		public static readonly string BOOTEVENT_INITIALIZEWORLDS = "InitializeWorlds";

		/// <summary>
		/// worlds have been built
		/// has no element
		/// </summary>
		// Token: 0x04001E9C RID: 7836
		public static readonly string BOOTEVENT_AFTERINITIALIZEWORLDS = "AfterInitializeWorlds";

		/// <summary>
		/// element is a GlobalLocation that will be the player's starting cell
		/// </summary>
		// Token: 0x04001E9D RID: 7837
		public static readonly string BOOTEVENT_BOOTSTARTINGLOCATION = "BootStartingLocation";

		/// <summary>
		/// generate the player's name if the player didn't choose one
		/// element is a string that will be the player's name
		/// </summary>
		// Token: 0x04001E9E RID: 7838
		public static readonly string BOOTEVENT_GENERATERANDOMPLAYERNAME = "GenerateRandomPlayerName";

		/// <summary>
		/// Generate the blueprint for the player's GameObject
		/// element is a string that will be the player's body
		/// </summary>
		// Token: 0x04001E9F RID: 7839
		public static readonly string BOOTEVENT_BOOTPLAYEROBJECTBLUEPRINT = "BootPlayerObjectBlueprint";

		/// <summary>
		/// create the player's GameObject
		/// element is a GameObject that will be the player's body
		/// </summary>
		// Token: 0x04001EA0 RID: 7840
		public static readonly string BOOTEVENT_BEFOREBOOTPLAYEROBJECT = "BeforeBootPlayerObject";

		/// <summary>
		/// create the player's GameObject
		/// element is a GameObject that will be the player's body
		/// </summary>
		// Token: 0x04001EA1 RID: 7841
		public static readonly string BOOTEVENT_BOOTPLAYEROBJECT = "BootPlayerObject";

		/// <summary>
		/// the player's GameObject has been created
		/// element is a GameObject that will be the player's body
		/// </summary>
		// Token: 0x04001EA2 RID: 7842
		public static readonly string BOOTEVENT_AFTERBOOTPLAYEROBJECT = "AfterBootPlayerObject";

		/// <summary>
		/// Generate the player's tile.
		/// Element is a string path to the tile texture.
		/// </summary>
		// Token: 0x04001EA3 RID: 7843
		public static readonly string BOOTEVENT_BOOTPLAYERTILE = "BootPlayerTile";

		/// <summary>
		/// Generate the player's foreground color.
		/// Element is a string color character.
		/// </summary>
		// Token: 0x04001EA4 RID: 7844
		public static readonly string BOOTEVENT_BOOTPLAYERTILEFOREGROUND = "BootPlayerTileForeground";

		/// <summary>
		/// Generate the player's background color.
		/// Element is a string color character.
		/// </summary>
		// Token: 0x04001EA5 RID: 7845
		public static readonly string BOOTEVENT_BOOTPLAYERTILEBACKGROUND = "BootPlayerTileBackground";

		/// <summary>
		/// Generate the player's detail color.
		/// Element is a string color character.
		/// </summary>
		// Token: 0x04001EA6 RID: 7846
		public static readonly string BOOTEVENT_BOOTPLAYERTILEDETAIL = "BootPlayerTileDetail";

		/// <summary>
		/// Generate the player's reputation.
		/// Has no element.
		/// </summary>
		// Token: 0x04001EA7 RID: 7847
		public static readonly string BOOTEVENT_BOOTPLAYERREPUTATION = "BootPlayerReputation";

		/// <summary>
		/// the game is just about to start this is the last event before it does
		/// has no element
		/// </summary>
		// Token: 0x04001EA8 RID: 7848
		public static readonly string BOOTEVENT_GAMESTARTING = "GameStarting";
	}
}
