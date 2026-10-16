using System;
using System.Linq;
using Qud.UI;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace JoppaTutorial
{
	// Token: 0x020003F8 RID: 1016
	public class MoveToChest : TutorialStep
	{
		// Token: 0x060025F5 RID: 9717 RVA: 0x000C3E41 File Offset: 0x000C2041
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			if (cell.X > 17)
			{
				Popup.Show("Before you move on, you should equip yourself from the nearby chest.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				return false;
			}
			return base.ConstrainToCurrentZone(cell);
		}

		// Token: 0x060025F6 RID: 9718 RVA: 0x000C3E6B File Offset: 0x000C206B
		public override bool AllowCommand(string id)
		{
			return !(id == "Take All") && base.AllowCommand(id);
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x000C3E84 File Offset: 0x000C2084
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			if (string.IsNullOrEmpty(title))
			{
				return true;
			}
			if (this.step == 100)
			{
				if (title == "InventoryActionMenu:(noid)")
				{
					return command.command == "Cancel";
				}
				if (title == "InventoryActionMenu:" + this.torch.ID)
				{
					if (command.simpleText == "equip (auto)" || command.command == "Cancel")
					{
						return true;
					}
					SoundManager.PlayUISound("Sounds/UI/ui_notification_warning", 1f, false, true, SoundRequest.SoundEffectType.None);
					return false;
				}
				else if (title == "InventoryActionMenu:" + this.dagger.ID)
				{
					if (command.simpleText == "equip (auto)" || command.command == "Cancel")
					{
						return true;
					}
					SoundManager.PlayUISound("Sounds/UI/ui_notification_warning", 1f, false, true, SoundRequest.SoundEffectType.None);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x000C3F7C File Offset: 0x000C217C
		public override void OnTrigger(string trigger)
		{
			if (trigger == "OpeningStoryClosed")
			{
				TutorialManager.ShowIntermissionPopupAsync("This is the main gameplay stage. At the top of the screen are your attributes, HP, and XP.\n\nOn the right are action buttons, minimap, and the message log.\n\nAt the bottom is your ability hotbar.", delegate
				{
					TutorialManager.ShowIntermissionPopupAsync("Let's explore this cave we've found ourselves in.", delegate
					{
						this.step = 100;
					}, 0, 0f, "sw");
				}, 0, 0f, "sw");
			}
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x000C3FB0 File Offset: 0x000C21B0
		public override void LateUpdate()
		{
			GameObject gameObject = this.chest;
			if (gameObject != null && !gameObject.IsValid())
			{
				this.manager.Highlight(null, null, null, 64f, 64f, 0f, "big");
				Popup.Show("Huh, you destroyed the tutorial chest we were going to teach you how to use.\n\nGo ahead and pick of the torch and dagger from the floor.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				TutorialManager.AdvanceStep(new FightSnapjaw());
				return;
			}
			GameObject gameObject2 = this.torch;
			if (gameObject2 == null || gameObject2.IsValid())
			{
				GameObject gameObject3 = this.torch;
				bool? flag;
				if (gameObject3 == null)
				{
					flag = null;
				}
				else
				{
					GameObject inInventory = gameObject3.InInventory;
					flag = ((inInventory != null) ? new bool?(inInventory.IsPlayer()) : null);
				}
				bool? flag2 = flag;
				if (!flag2.GetValueOrDefault())
				{
					GameObject gameObject4 = this.torch;
					bool? flag3;
					if (gameObject4 == null)
					{
						flag3 = null;
					}
					else
					{
						GameObject gameObject5 = gameObject4.equippedOrSelf();
						flag3 = ((gameObject5 != null) ? new bool?(gameObject5.IsPlayer()) : null);
					}
					flag2 = flag3;
					if (!flag2.GetValueOrDefault())
					{
						goto IL_01AB;
					}
				}
			}
			GameObject gameObject6 = this.dagger;
			if (gameObject6 == null || gameObject6.IsValid())
			{
				GameObject gameObject7 = this.dagger;
				bool? flag4;
				if (gameObject7 == null)
				{
					flag4 = null;
				}
				else
				{
					GameObject inInventory2 = gameObject7.InInventory;
					flag4 = ((inInventory2 != null) ? new bool?(inInventory2.IsPlayer()) : null);
				}
				bool? flag2 = flag4;
				if (!flag2.GetValueOrDefault())
				{
					GameObject gameObject8 = this.dagger;
					bool? flag5;
					if (gameObject8 == null)
					{
						flag5 = null;
					}
					else
					{
						GameObject gameObject9 = gameObject8.equippedOrSelf();
						flag5 = ((gameObject9 != null) ? new bool?(gameObject9.IsPlayer()) : null);
					}
					flag2 = flag5;
					if (!flag2.GetValueOrDefault())
					{
						goto IL_01AB;
					}
				}
			}
			this.manager.Highlight(null, null, null, 64f, 64f, 0f, "big");
			TutorialManager.AdvanceStep(new FightSnapjaw());
			return;
			IL_01AB:
			GameObject player = The.Player;
			if (((player != null) ? player.CurrentCell : null) == null)
			{
				this.manager.Highlight(null, null, null, 64f, 64f, 0f, "big");
				return;
			}
			if (this.chest == null)
			{
				this.chest = The.Player.CurrentCell.ParentZone.FindObject("TutorialChest1");
				if (this.chest != null)
				{
					this.dagger = this.chest.GetPart<Inventory>().Objects.Where<GameObject>((GameObject o) => o.Blueprint == "TutorialDagger").FirstOrDefault<GameObject>();
					GameObject gameObject10 = this.dagger;
					if (gameObject10 != null)
					{
						gameObject10.RequireID();
					}
					this.torch = this.chest.GetPart<Inventory>().Objects.Where<GameObject>((GameObject o) => o.Blueprint == "TutorialTorch").FirstOrDefault<GameObject>();
					GameObject gameObject11 = this.torch;
					if (gameObject11 != null)
					{
						gameObject11.RequireID();
					}
				}
			}
			if (this.step == 0 && The.Player != null)
			{
				foreach (GameObject gameObject12 in The.Player.Body.GetEquippedObjects())
				{
					gameObject12.Obliterate(null, false, null);
				}
				this.step = 50;
			}
			if (this.step == 50 && GameManager.Instance.CurrentGameView == "Stage")
			{
				this.step = 75;
			}
			if (this.step != 100)
			{
				this.manager.ClearHighlight();
				return;
			}
			if (GameManager.Instance.CurrentGameView == "PopupMessage")
			{
				string lastPopupID = PopupMessage.lastPopupID;
				if (lastPopupID != null && lastPopupID.StartsWith("InventoryActionMenu"))
				{
					string lastPopupID2 = PopupMessage.lastPopupID;
					if (lastPopupID2 != null && lastPopupID2.StartsWith("InventoryActionMenu:(noid)"))
					{
						this.manager.HighlightByCID("QudTextMenuItem:cancel", "<no message>", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
						return;
					}
					this.manager.HighlightByCID("QudTextMenuItem:equip (auto)", "<no message>", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
					return;
				}
			}
			if (GameManager.Instance.CurrentGameView == "PickGameObject")
			{
				if (this.torch != null && this.torch.IsValid() && this.torch.InInventory == this.chest)
				{
					this.manager.HighlightByCID("PickGameObject:Item:" + this.torch.ID, "A torch. When it's nighttime or when you're underground, you need a torch equipped to see. Select it.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
					return;
				}
				if (this.dagger != null && this.dagger.IsValid() && this.dagger.InInventory == this.chest)
				{
					this.manager.HighlightByCID("PickGameObject:Item:" + this.dagger.ID, "Equip the dagger too.", "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
					return;
				}
				this.manager.ClearHighlight();
				return;
			}
			else
			{
				if (!(GameManager.Instance.CurrentGameView == "Stage"))
				{
					this.manager.ClearHighlight();
					return;
				}
				if (The.Player.CurrentCell.X == 15 && The.Player.CurrentCell.Y == 12)
				{
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						this.manager.HighlightCell(16, 12, "You can interact with objects you're next to. Open the chest.\n\nPress {{hotkey|~Accept}}", "ne", 3f, 3f, 0f);
						return;
					}
					this.manager.HighlightCell(16, 12, "You can interact with objects you're next to. Open the chest.\n\nPress {{hotkey|~Accept}} or {{hotkey|~AdventureMouseContextAction}}", "ne", 3f, 3f, 0f);
					return;
				}
				else
				{
					if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
					{
						this.manager.HighlightCell(15, 12, "Looks like there's a chest over at the end of the room. Let's walk over to it.\n\nHold ~IndicateDirection in the direction you want to move and press ~Take A Step.", "ne", 3f, 3f, 0f);
						return;
					}
					this.manager.HighlightCell(15, 12, "Looks like there's a chest over at the end of the room. Let's walk over to it.\n\nYou can click ~AdventureNavMouseLeftClick the square you want to move to, or use the {{hotkey|arrow keys}} or {{hotkey|numpad.}}\n\nWith arrow keys, you can press {{hotkey|shift+arrow keys}} to move diagonally.", "ne", 3f, 3f, 0f);
					return;
				}
			}
		}

		// Token: 0x04001303 RID: 4867
		public GameObject chest;

		// Token: 0x04001304 RID: 4868
		public GameObject dagger;

		// Token: 0x04001305 RID: 4869
		public GameObject torch;
	}
}
