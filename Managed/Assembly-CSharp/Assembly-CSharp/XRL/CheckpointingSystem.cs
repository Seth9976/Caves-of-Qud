using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsoleLib.Console;
using UnityEngine;
using XRL.Core;
using XRL.Messages;
using XRL.UI;
using XRL.Wish;
using XRL.World;

namespace XRL
{
	// Token: 0x020004EE RID: 1262
	[HasWishCommand]
	[Serializable]
	public class CheckpointingSystem : IGameSystem
	{
		// Token: 0x0600367F RID: 13951 RVA: 0x001071C8 File Offset: 0x001053C8
		public static bool ShowDeathMessage(string message, string deathCategory = null)
		{
			Debug.Log("deathCategory: " + deathCategory);
			if (!CheckpointingSystem.IsCheckpointingEnabled())
			{
				if (!string.IsNullOrEmpty(deathCategory) && CheckpointingSystem.deathIcons.ContainsKey(deathCategory))
				{
					string text = "";
					if (message.Contains("."))
					{
						text = message.Substring(0, message.IndexOf('.') + 1);
						message = message.Substring(message.IndexOf('.') + 2);
					}
					Popup.ShowSpace(message.Replace("You died.", ""), text, "Sounds/UI/ui_notification_death", CheckpointingSystem.deathIcons[deathCategory], true, false, "DeathMessage");
				}
				else
				{
					Popup.ShowSpace(message, null, "Sounds/UI/ui_notification_death", null, true, true, "DeathMessage");
				}
				return false;
			}
			if (Options.AllowReallydie && Popup.ShowYesNo("DEBUG: Do you really want to die?", "Sounds/UI/ui_notification", true, DialogResult.No, null) != DialogResult.Yes)
			{
				The.Player.RestorePristineHealth(false, false);
				return true;
			}
			int num;
			for (;;)
			{
				bool flag = Options.GetOption("OptionDefaultDeathToViewFinalMessages", "") == "Yes";
				string text2 = "";
				string text3 = message + "\n";
				string text4 = "";
				string text5 = "Sounds/UI/ui_notification";
				IReadOnlyList<string> readOnlyList;
				if (!flag)
				{
					string[] array = new string[4];
					array[0] = "Reload from checkpoint";
					array[1] = "View final messages";
					array[2] = "Retire character";
					readOnlyList = array;
					array[3] = "Quit to main menu";
				}
				else
				{
					string[] array2 = new string[4];
					array2[0] = "View final messages";
					array2[1] = "Reload from checkpoint";
					array2[2] = "Retire character";
					readOnlyList = array2;
					array2[3] = "Quit to main menu";
				}
				num = Popup.PickOption(text2, text3, text4, text5, readOnlyList, null, null, null, null, null, null, 0, 60, 0, -1, false, false, false, true, false, null, null);
				if (num == (flag ? 1 : 0))
				{
					break;
				}
				if (num == (flag ? 0 : 1))
				{
					XRLCore.Core.Game.Player.Messages.Show();
				}
				else
				{
					if (num != 2)
					{
						goto IL_0214;
					}
					string text6 = " Type 'RETIRE' to confirm.";
					string text7 = string.Empty;
					if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.Console)
					{
						text6 = string.Empty;
						text7 = null;
					}
					string text8 = Popup.AskString("If you retire this character, your score will be recorded and your character will be lost. Are you sure you want to RETIRE THIS CHARACTER FOREVER?" + text6, text7, "Sounds/UI/ui_notification", null, "RETIRE", 7, 0, false, true, new bool?(false));
					if (text8 != null && text8.ToUpper() == "RETIRE")
					{
						return false;
					}
				}
			}
			CheckpointingSystem.QueueRestore();
			return true;
			IL_0214:
			if (num == 3)
			{
				The.Game.DeathReason = "<nodeath>";
				The.Game.DeathCategory = "exit";
				The.Game.forceNoDeath = true;
				The.Game.Running = false;
				return true;
			}
			return true;
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x00107424 File Offset: 0x00105624
		public static bool IsPlayerInCheckpoint()
		{
			if (The.Player == null)
			{
				return false;
			}
			XRL.World.GameObject player = The.Player;
			bool? flag;
			if (player == null)
			{
				flag = null;
			}
			else
			{
				Zone currentZone = player.CurrentZone;
				flag = ((currentZone != null) ? new bool?(currentZone.IsCheckpoint()) : null);
			}
			bool? flag2 = flag;
			return flag2.GetValueOrDefault();
		}

		// Token: 0x06003681 RID: 13953 RVA: 0x00107474 File Offset: 0x00105674
		public static bool IsCheckpointingEnabled()
		{
			XRLGame game = The.Game;
			if (game.GetStringGameState("Checkpointing", "") != "Enabled")
			{
				string stringGameState = game.GetStringGameState("GameMode", "");
				if (stringGameState != "Wander" && stringGameState != "Roleplay")
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x001074D1 File Offset: 0x001056D1
		[WishCommand(null, null)]
		public static bool CheckpointOn()
		{
			The.Game.SetStringGameState("Checkpointing", "Enabled");
			MessageQueue.AddPlayerMessage("Checkpointing enabled", null, true);
			return true;
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x001074F4 File Offset: 0x001056F4
		[WishCommand(null, null)]
		public static bool CheckpointOff()
		{
			The.Game.RemoveStringGameState("Checkpointing");
			string stringGameState = The.Game.GetStringGameState("GameMode", "");
			if (stringGameState == "Wander" || stringGameState == "Roleplay")
			{
				The.Game.SetStringGameState("GameMode", stringGameState + "NoCheckpoint");
			}
			return true;
		}

		// Token: 0x06003684 RID: 13956 RVA: 0x0010755A File Offset: 0x0010575A
		public static void DoCheckpoint()
		{
			The.Game.Checkpoint();
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x00107568 File Offset: 0x00105768
		public static void ManualCheckpoint(Zone Z, string Key)
		{
			if (!CheckpointingSystem.IsCheckpointingEnabled() || !The.Game.Running)
			{
				return;
			}
			CheckpointingSystem system = The.Game.GetSystem<CheckpointingSystem>();
			if (system != null)
			{
				if (Z.ZoneID == system.lastZone)
				{
					return;
				}
				if (Key == system.lastCheckpointKey)
				{
					return;
				}
				system.lastZone = Z.ZoneID;
				system.lastCheckpointKey = Key;
			}
			CheckpointingSystem.QueueCheckpoint();
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x001075D2 File Offset: 0x001057D2
		public static void ManualCheckpoint()
		{
			if (!CheckpointingSystem.IsCheckpointingEnabled() || !The.Game.Running)
			{
				return;
			}
			CheckpointingSystem.QueueCheckpoint();
		}

		// Token: 0x06003687 RID: 13959 RVA: 0x001075ED File Offset: 0x001057ED
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x001075FC File Offset: 0x001057FC
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (!CheckpointingSystem.IsCheckpointingEnabled() || !base.Game.Running)
			{
				return base.HandleEvent(E);
			}
			if (E.Zone.ZoneID == this.lastZone)
			{
				return base.HandleEvent(E);
			}
			string checkpointKey = E.Zone.GetCheckpointKey();
			if ((checkpointKey != null || this.lastCheckpointKey != null) && checkpointKey != this.lastCheckpointKey)
			{
				CheckpointingSystem.QueueCheckpoint();
			}
			this.lastZone = E.Zone.ZoneID;
			this.lastCheckpointKey = checkpointKey;
			return base.HandleEvent(E);
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x0010768E File Offset: 0x0010588E
		public static void QueueCheckpoint()
		{
			CheckpointingSystem.SaveCommand.Issue();
		}

		// Token: 0x0600368A RID: 13962 RVA: 0x00107695 File Offset: 0x00105895
		public static void QueueRestore()
		{
			CheckpointingSystem.LoadCommand.Issue();
		}

		// Token: 0x04001B44 RID: 6980
		[NonSerialized]
		public static Dictionary<string, Renderable> deathIcons = new Dictionary<string, Renderable> { 
		{
			"lased to death",
			new Renderable(null, "Mutations/flaming_ray.bmp", "f", "&W", "&W", new char?('R'))
		} };

		// Token: 0x04001B45 RID: 6981
		public string lastZone;

		// Token: 0x04001B46 RID: 6982
		public string lastCheckpointKey;

		// Token: 0x0200192E RID: 6446
		public class SaveCommand : ISystemSaveCommand, IActionCommand, IComposite
		{
			// Token: 0x17000E53 RID: 3667
			// (get) Token: 0x0600E28E RID: 57998 RVA: 0x0052509A File Offset: 0x0052329A
			public string Type
			{
				get
				{
					return "Checkpoint";
				}
			}

			// Token: 0x0600E28F RID: 57999 RVA: 0x005250A1 File Offset: 0x005232A1
			public static void Issue()
			{
				ActionManager actionManager = The.ActionManager;
				actionManager.DequeueActionsDescendedFrom<ISystemSaveCommand>();
				actionManager.EnqueueAction(CheckpointingSystem.SaveCommand.Instance, 0);
			}

			// Token: 0x0600E290 RID: 58000 RVA: 0x005250BA File Offset: 0x005232BA
			public void Execute(XRLGame Game, ActionManager Manager)
			{
				Game.Checkpoint();
			}

			// Token: 0x04006523 RID: 25891
			private static CheckpointingSystem.SaveCommand Instance = new CheckpointingSystem.SaveCommand();
		}

		// Token: 0x0200192F RID: 6447
		public class LoadCommand : IActionCommand, IComposite
		{
			// Token: 0x0600E293 RID: 58003 RVA: 0x005250D8 File Offset: 0x005232D8
			public static void Issue()
			{
				XRLGame game = The.Game;
				if (game == null || !game.Running)
				{
					return;
				}
				ActionManager actionManager = game.ActionManager;
				if (!actionManager.HasAction(typeof(CheckpointingSystem.LoadCommand)))
				{
					if (actionManager.IsPlayerTurn())
					{
						actionManager.SkipPlayerTurn = true;
					}
					actionManager.SkipSegment = true;
					actionManager.DequeueActionsDescendedFrom<ISystemSaveCommand>();
					actionManager.EnqueueAction(CheckpointingSystem.LoadCommand.Instance, 0);
				}
			}

			// Token: 0x0600E294 RID: 58004 RVA: 0x0052513C File Offset: 0x0052333C
			public void Execute(XRLGame Game, ActionManager Manager)
			{
				XRLGame xrlgame = XRLGame.LoadCurrentGame("Checkpoint", false, null);
				if (xrlgame == null)
				{
					return;
				}
				Task task = xrlgame.SaveCopy("Checkpoint", "Primary");
				Task task2 = xrlgame.CacheCopy("CheckpointCache", "Cache", true);
				task.Wait();
				task2.Wait();
			}

			// Token: 0x04006524 RID: 25892
			private static CheckpointingSystem.LoadCommand Instance = new CheckpointingSystem.LoadCommand();
		}
	}
}
