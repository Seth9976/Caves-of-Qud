using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using Genkit;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Anatomy;
using XRL.World.Parts;

namespace Qud.API
{
	// Token: 0x02000407 RID: 1031
	public static class EquipmentAPI
	{
		// Token: 0x060026CA RID: 9930 RVA: 0x000C7CAC File Offset: 0x000C5EAC
		public static void TwiddleObject(GameObject GO, Action After = null, bool Distant = false, bool TelekineticOnly = false)
		{
			bool flag = false;
			EquipmentAPI.TwiddleObject(The.Player, GO, ref flag, Distant, TelekineticOnly, false);
			if (After != null)
			{
				After();
			}
		}

		// Token: 0x060026CB RID: 9931 RVA: 0x000C7CD4 File Offset: 0x000C5ED4
		public static void TwiddleObject(GameObject GO, ref bool Done, Action After = null, bool Distant = false, bool TelekineticOnly = false, bool MouseClick = false)
		{
			EquipmentAPI.TwiddleObject(The.Player, GO, ref Done, Distant, TelekineticOnly, MouseClick);
			if (After != null)
			{
				After();
			}
		}

		// Token: 0x060026CC RID: 9932 RVA: 0x000C7CF0 File Offset: 0x000C5EF0
		public static InventoryAction ShowInventoryActionMenu(Dictionary<string, InventoryAction> ActionTable, GameObject Owner = null, GameObject GO = null, bool Distant = false, bool TelekineticOnly = false, string Intro = null, IComparer<InventoryAction> Comparer = null, bool MouseClick = false)
		{
			List<InventoryAction> list = new List<InventoryAction>();
			foreach (KeyValuePair<string, InventoryAction> keyValuePair in ActionTable)
			{
				InventoryAction value = keyValuePair.Value;
				if ((!TelekineticOnly || value.WorksTelekinetically) && value.IsVisible(Owner, GO, Distant))
				{
					list.Add(value);
				}
			}
			List<InventoryAction> list2 = list;
			IComparer<InventoryAction> comparer;
			if ((comparer = Comparer) == null)
			{
				comparer = (Comparer = new InventoryAction.Comparer());
			}
			list2.Sort(comparer);
			if (CapabilityManager.AllowKeyboardHotkeys)
			{
				Dictionary<char, InventoryAction> dictionary = new Dictionary<char, InventoryAction>(16);
				List<InventoryAction> list3 = null;
				StringBuilder stringBuilder = null;
				foreach (InventoryAction inventoryAction in list)
				{
					if (inventoryAction.Key != ' ' && !ControlManager.isKeyMapped(inventoryAction.Key, new List<string> { "UINav", "Menus" }))
					{
						if (dictionary.ContainsKey(inventoryAction.Key))
						{
							if (list3 == null)
							{
								list3 = new List<InventoryAction>();
							}
							list3.Add(inventoryAction);
						}
						else
						{
							dictionary.Add(inventoryAction.Key, inventoryAction);
							inventoryAction.Display = EquipmentAPI.ApplyHotkey(inventoryAction.Display, inventoryAction.Key, inventoryAction.PreferToHighlight, ref stringBuilder);
						}
					}
					else
					{
						inventoryAction.Key = ' ';
					}
				}
				if (list3 != null)
				{
					if (stringBuilder == null)
					{
						stringBuilder = Event.NewStringBuilder(null);
					}
					foreach (InventoryAction inventoryAction2 in list3)
					{
						char c = char.ToUpper(inventoryAction2.Key);
						if (c != inventoryAction2.Key && !dictionary.ContainsKey(c))
						{
							if (!ControlManager.isKeyMapped(c, new List<string> { "UINav", "Menus" }))
							{
								inventoryAction2.Key = c;
								dictionary.Add(c, inventoryAction2);
								inventoryAction2.Display = EquipmentAPI.ApplyHotkey(ColorUtility.StripFormatting(inventoryAction2.Display), c, inventoryAction2.PreferToHighlight, ref stringBuilder);
							}
						}
						else
						{
							string text = inventoryAction2.Display;
							text = ColorUtility.StripFormatting(text);
							bool flag = false;
							stringBuilder.Clear();
							int i = 0;
							int length = text.Length;
							while (i < length)
							{
								char c2 = text[i];
								if (!dictionary.ContainsKey(c2) && !ControlManager.isKeyMapped(c2, new List<string> { "UINav", "Menus" }))
								{
									inventoryAction2.Key = c2;
									dictionary.Add(c2, inventoryAction2);
									stringBuilder.Append("{{hotkey|").Append(c2).Append("}}")
										.Append(text, i + 1, length - i - 1);
									flag = true;
									break;
								}
								stringBuilder.Append(c2);
								i++;
							}
							if (!flag)
							{
								inventoryAction2.Key = ' ';
							}
							inventoryAction2.Display = stringBuilder.ToString();
						}
					}
					list.Sort(Comparer);
				}
			}
			List<string> list4 = new List<string>();
			List<char> list5 = new List<char>();
			foreach (InventoryAction inventoryAction3 in list)
			{
				list4.Add(inventoryAction3.Display);
				list5.Add(inventoryAction3.Key);
			}
			int num = 0;
			int num2 = int.MinValue;
			int j = 0;
			int count = list.Count;
			while (j < count)
			{
				if (list[j].Default > num2)
				{
					num = j;
					num2 = list[j].Default;
				}
				j++;
			}
			bool isConfused = The.Player.IsConfused;
			Location2D location2D = null;
			if (MouseClick)
			{
				Location2D location2D2;
				if (GO == null)
				{
					location2D2 = null;
				}
				else
				{
					Cell currentCell = GO.CurrentCell;
					location2D2 = ((currentCell != null) ? currentCell.Location : null);
				}
				location2D = location2D2;
			}
			int num3 = Popup.PickOption("", Intro ?? (isConfused ? GO.DisplayName : null), "", "Sounds/UI/ui_notification", list4.ToArray(), list5.ToArray(), null, null, isConfused ? null : GO, isConfused ? null : GO.RenderForUI(null, false), null, 0, 60, num, -1, true, true, true, true, false, location2D, "InventoryActionMenu:" + (((GO != null) ? GO.IDIfAssigned : null) ?? "(noid)"));
			if (num3 < 0)
			{
				return null;
			}
			return list[num3];
		}

		// Token: 0x060026CD RID: 9933 RVA: 0x000C81C0 File Offset: 0x000C63C0
		public static void TwiddleObject(GameObject Owner, GameObject GO, ref bool Done, bool Distant = false, bool TelekineticOnly = false, bool MouseClick = false)
		{
			InventoryAction inventoryAction;
			EquipmentAPI.TwiddleObject(Owner, GO, ref Done, out inventoryAction, Distant, TelekineticOnly, MouseClick);
		}

		// Token: 0x060026CE RID: 9934 RVA: 0x000C81DC File Offset: 0x000C63DC
		public static void TwiddleObject(GameObject Owner, GameObject GO, ref bool Done, out InventoryAction ResultingAction, bool Distant = false, bool TelekineticOnly = false, bool MouseClick = false)
		{
			ResultingAction = null;
			try
			{
				if (Options.ModernUI)
				{
					GameManager.Instance.PushGameView("ModernPopupTwiddleObject", true);
				}
				ControlManager.ResetInput(false, true);
				Keyboard.ClearInput(false, false);
				if (GameObject.Validate(GO))
				{
					InventoryAction inventoryAction;
					for (;;)
					{
						Dictionary<string, InventoryAction> dictionary = new Dictionary<string, InventoryAction>(16);
						if (GO.HasRegisteredEvent("GetInventoryActions") || GO.HasRegisteredEvent("GetInventoryActionsAlways") || Owner.HasRegisteredEvent("OwnerGetInventoryActions"))
						{
							EventParameterGetInventoryActions eventParameterGetInventoryActions = new EventParameterGetInventoryActions(dictionary);
							GO.FireEvent(Event.New("GetInventoryActions", "Actions", eventParameterGetInventoryActions));
							GO.FireEvent(Event.New("GetInventoryActionsAlways", "Actions", eventParameterGetInventoryActions));
							Owner.FireEvent(Event.New("OwnerGetInventoryActions", "Actions", eventParameterGetInventoryActions, "Object", GO));
						}
						GetInventoryActionsEvent.Send(Owner, GO, dictionary);
						GetInventoryActionsAlwaysEvent.Send(Owner, GO, dictionary);
						OwnerGetInventoryActionsEvent.Send(Owner, GO, dictionary);
						GameObject inInventory = GO.InInventory;
						GameObject equipped = GO.Equipped;
						Cell currentCell = GO.CurrentCell;
						inventoryAction = EquipmentAPI.ShowInventoryActionMenu(dictionary, Owner, GO, Distant, TelekineticOnly, null, null, MouseClick);
						if (inventoryAction == null)
						{
							break;
						}
						if (Options.ModernUI && inventoryAction != null && inventoryAction.ReturnToModernUI)
						{
							goto Block_11;
						}
						bool flag;
						if (!inventoryAction.IsUsable(Owner, GO, Distant, out flag))
						{
							if (flag)
							{
								Popup.Show(GO.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " out of your telekinetic range.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							else
							{
								Popup.Show("You cannot do that from here.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
						}
						else
						{
							IEvent @event = inventoryAction.Process(GO, Owner, flag);
							if (@event != null && @event.InterfaceExitRequested())
							{
								goto Block_15;
							}
							if (GO.IsInvalid())
							{
								goto Block_16;
							}
							if (currentCell != GO.CurrentCell)
							{
								goto Block_17;
							}
							if (inInventory != GO.InInventory)
							{
								goto Block_18;
							}
							if (equipped != GO.Equipped)
							{
								goto Block_19;
							}
						}
					}
					GO.CheckStack();
					return;
					Block_11:
					ResultingAction = inventoryAction;
					return;
					Block_15:
					Done = true;
					Block_16:
					Block_17:
					Block_18:
					Block_19:;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("TwiddleObject", ex, "game_exception");
			}
			finally
			{
				if (Options.ModernUI)
				{
					GameManager.Instance.PopGameView(false);
				}
			}
		}

		// Token: 0x060026CF RID: 9935 RVA: 0x000C8434 File Offset: 0x000C6634
		private static string ApplyHotkey(string display, char key, string prefer, ref StringBuilder SB)
		{
			if (SB == null)
			{
				SB = Event.NewStringBuilder(null);
			}
			else
			{
				SB.Clear();
			}
			if (!display.Contains("{{") && !display.Contains("&"))
			{
				char c = char.ToLower(key);
				int num = -1;
				int num2 = -1;
				if (!string.IsNullOrEmpty(prefer))
				{
					num2 = display.IndexOf(prefer);
					if (num2 != -1)
					{
						num = prefer.IndexOf(key);
						if (num == -1 && c != key)
						{
							num = prefer.IndexOf(c);
						}
					}
				}
				if (num != -1)
				{
					int num3 = num + num2;
					display = SB.Append(display, 0, num3).Append("{{hotkey|").Append(key)
						.Append("}}")
						.Append(display, num3 + 1, display.Length - num3 - 1)
						.ToString();
				}
				else
				{
					int num4 = display.IndexOf(key);
					if (num4 != -1)
					{
						display = SB.Append(display, 0, num4).Append("{{hotkey|").Append(key)
							.Append("}}")
							.Append(display, num4 + 1, display.Length - num4 - 1)
							.ToString();
					}
					else if (c != key)
					{
						num4 = display.IndexOf(c);
						if (num4 != -1)
						{
							display = SB.Append(display, 0, num4).Append("{{hotkey|").Append(key)
								.Append("}}")
								.Append(display, num4 + 1, display.Length - num4 - 1)
								.ToString();
						}
					}
				}
			}
			return display;
		}

		// Token: 0x060026D0 RID: 9936 RVA: 0x000C85A0 File Offset: 0x000C67A0
		private static bool GotUsableAction(GameObject GO, GameObject Actor, bool TelekineticOnly)
		{
			if (TelekineticOnly)
			{
				foreach (InventoryAction inventoryAction in EquipmentAPI.CheckActionTable.Values)
				{
					if (inventoryAction.WorksTelekinetically && inventoryAction.IsVisible(Actor, GO, true))
					{
						return true;
					}
				}
				return false;
			}
			return EquipmentAPI.CheckActionTable.Count > 0;
		}

		// Token: 0x060026D1 RID: 9937 RVA: 0x000C861C File Offset: 0x000C681C
		public static bool CanBeTwiddled(GameObject GO, GameObject Actor, bool TelekineticOnly = false)
		{
			if (GO == null)
			{
				return false;
			}
			if (GO.HasTag("NoTwiddle"))
			{
				return false;
			}
			if (GO.HasRegisteredEvent("GetInventoryActions") || GO.HasRegisteredEvent("GetInventoryActionsAlways") || Actor.HasRegisteredEvent("OwnerGetInventoryActions") || GO.WantEvent(GetInventoryActionsEvent.ID, MinEvent.CascadeLevel) || GO.WantEvent(GetInventoryActionsAlwaysEvent.ID, MinEvent.CascadeLevel) || Actor.WantEvent(OwnerGetInventoryActionsEvent.ID, OwnerGetInventoryActionsEvent.CascadeLevel))
			{
				EquipmentAPI.CheckActionTable.Clear();
				try
				{
					if (GO.HasRegisteredEvent("GetInventoryActions") || GO.HasRegisteredEvent("GetInventoryActionsAlways") || Actor.HasRegisteredEvent("OwnerGetInventoryActions"))
					{
						EventParameterGetInventoryActions eventParameterGetInventoryActions = new EventParameterGetInventoryActions(EquipmentAPI.CheckActionTable);
						if (GO.HasRegisteredEvent("GetInventoryActions"))
						{
							GO.FireEvent(Event.New("GetInventoryActions", "Actions", eventParameterGetInventoryActions));
							if (EquipmentAPI.GotUsableAction(GO, Actor, TelekineticOnly))
							{
								return true;
							}
						}
						if (GO.HasRegisteredEvent("GetInventoryActionsAlways"))
						{
							GO.FireEvent(Event.New("GetInventoryActionsAlways", "Actions", eventParameterGetInventoryActions));
							if (EquipmentAPI.GotUsableAction(GO, Actor, TelekineticOnly))
							{
								return true;
							}
						}
						if (Actor.HasRegisteredEvent("OwnerGetInventoryActions"))
						{
							Actor.FireEvent(Event.New("OwnerGetInventoryActions", "Actions", eventParameterGetInventoryActions, "Object", GO));
							if (EquipmentAPI.GotUsableAction(GO, Actor, TelekineticOnly))
							{
								return true;
							}
						}
					}
					GetInventoryActionsEvent.Send(Actor, GO, EquipmentAPI.CheckActionTable);
					if (EquipmentAPI.GotUsableAction(GO, Actor, TelekineticOnly))
					{
						return true;
					}
					GetInventoryActionsAlwaysEvent.Send(Actor, GO, EquipmentAPI.CheckActionTable);
					if (EquipmentAPI.GotUsableAction(GO, Actor, TelekineticOnly))
					{
						return true;
					}
					OwnerGetInventoryActionsEvent.Send(Actor, GO, EquipmentAPI.CheckActionTable);
					if (EquipmentAPI.GotUsableAction(GO, Actor, TelekineticOnly))
					{
						return true;
					}
				}
				finally
				{
					EquipmentAPI.CheckActionTable.Clear();
				}
				return false;
			}
			return false;
		}

		// Token: 0x060026D2 RID: 9938 RVA: 0x000C87F8 File Offset: 0x000C69F8
		public static List<InventoryAction> GetInventoryActions(GameObject GO, GameObject Actor, bool TelekineticOnly = false, bool TelekineticRequireUsable = true)
		{
			if (GO.HasTag("NoInventoryActions"))
			{
				return null;
			}
			List<InventoryAction> list = null;
			if (GO.HasRegisteredEvent("GetInventoryActions") || GO.HasRegisteredEvent("GetInventoryActionsAlways") || Actor.HasRegisteredEvent("OwnerGetInventoryActions") || GO.WantEvent(GetInventoryActionsEvent.ID, MinEvent.CascadeLevel) || GO.WantEvent(GetInventoryActionsAlwaysEvent.ID, MinEvent.CascadeLevel) || Actor.WantEvent(OwnerGetInventoryActionsEvent.ID, OwnerGetInventoryActionsEvent.CascadeLevel))
			{
				EquipmentAPI.CheckActionTable.Clear();
				if (GO.HasRegisteredEvent("GetInventoryActions") || GO.HasRegisteredEvent("GetInventoryActionsAlways") || Actor.HasRegisteredEvent("OwnerGetInventoryActions"))
				{
					EventParameterGetInventoryActions eventParameterGetInventoryActions = new EventParameterGetInventoryActions(EquipmentAPI.CheckActionTable);
					if (GO.HasRegisteredEvent("GetInventoryActions"))
					{
						GO.FireEvent(Event.New("GetInventoryActions", "Actions", eventParameterGetInventoryActions));
					}
					if (GO.HasRegisteredEvent("GetInventoryActionsAlways"))
					{
						GO.FireEvent(Event.New("GetInventoryActionsAlways", "Actions", eventParameterGetInventoryActions));
					}
					if (Actor.HasRegisteredEvent("OwnerGetInventoryActions"))
					{
						Actor.FireEvent(Event.New("OwnerGetInventoryActions", "Actions", eventParameterGetInventoryActions, "Object", GO));
					}
				}
				GetInventoryActionsEvent.Send(Actor, GO, EquipmentAPI.CheckActionTable);
				GetInventoryActionsAlwaysEvent.Send(Actor, GO, EquipmentAPI.CheckActionTable);
				OwnerGetInventoryActionsEvent.Send(Actor, GO, EquipmentAPI.CheckActionTable);
				if (EquipmentAPI.CheckActionTable.Count > 0)
				{
					list = new List<InventoryAction>(EquipmentAPI.CheckActionTable.Values);
					EquipmentAPI.CheckActionTable.Clear();
					if (TelekineticOnly && list.Count > 0)
					{
						List<InventoryAction> list2 = new List<InventoryAction>();
						foreach (InventoryAction inventoryAction in list)
						{
							if (inventoryAction.WorksTelekinetically)
							{
								if (TelekineticRequireUsable)
								{
									if (inventoryAction.IsUsable(Actor, GO, true))
									{
										list2.Add(inventoryAction);
									}
								}
								else if (inventoryAction.IsVisible(Actor, GO, true))
								{
									list2.Add(inventoryAction);
								}
							}
						}
						list = list2;
					}
				}
			}
			return list;
		}

		// Token: 0x060026D3 RID: 9939 RVA: 0x000C89F8 File Offset: 0x000C6BF8
		public static bool UnequipObject(GameObject itemToUnequip)
		{
			GameObject equipped = itemToUnequip.Equipped;
			bool flag = true;
			if (equipped != null)
			{
				flag = InventoryActionEvent.Check(equipped, equipped, itemToUnequip, "Unequip", false, false, false, false, false, 0, 0, 0, null, null, null, null);
			}
			if (itemToUnequip.Equipped == null && itemToUnequip.InInventory == null)
			{
				Inventory inventory = equipped.Inventory;
				if (inventory != null)
				{
					inventory.AddObject(itemToUnequip, null, false, false, true, null);
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060026D4 RID: 9940 RVA: 0x000C8A58 File Offset: 0x000C6C58
		public static bool ForceUnequipObject(GameObject itemToUnequip, bool Silent = false)
		{
			GameObject equipped = itemToUnequip.Equipped;
			bool flag = true;
			if (equipped != null)
			{
				Event @event = Event.New("CommandForceUnequipObject", "Object", itemToUnequip);
				@event.SetSilent(Silent);
				equipped.FireEvent(@event);
			}
			if (itemToUnequip.Equipped == null && itemToUnequip.InInventory == null)
			{
				Inventory inventory = equipped.Inventory;
				if (inventory != null)
				{
					inventory.AddObject(itemToUnequip, null, Silent, false, true, null);
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x060026D5 RID: 9941 RVA: 0x000C8ABD File Offset: 0x000C6CBD
		public static void EquipObjectToPlayer(GameObject itemToEquip, BodyPart partToEquipOn)
		{
			EquipmentAPI.EquipObject(The.Player, itemToEquip, partToEquipOn);
		}

		// Token: 0x060026D6 RID: 9942 RVA: 0x000C8ACC File Offset: 0x000C6CCC
		public static int GetPlayerCurrentCarryWeight()
		{
			GameObject player = The.Player;
			if (player == null)
			{
				return 0;
			}
			return player.GetCarriedWeight();
		}

		// Token: 0x060026D7 RID: 9943 RVA: 0x000C8ADE File Offset: 0x000C6CDE
		public static int GetPlayerMaxCarryWeight()
		{
			GameObject player = The.Player;
			if (player == null)
			{
				return 0;
			}
			return player.GetMaxCarriedWeight();
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x000C8AF0 File Offset: 0x000C6CF0
		public static bool EquipObject(GameObject equippingObject, GameObject itemToEquip, BodyPart partToEquipOn)
		{
			return itemToEquip != null && (itemToEquip.Equipped == null || itemToEquip.EquippedOn().TryUnequip(false, false, true, false)) && equippingObject.FireEvent(Event.New("CommandEquipObject", "Object", itemToEquip, "BodyPart", partToEquipOn, "Force", true));
		}

		// Token: 0x060026D9 RID: 9945 RVA: 0x000C8B44 File Offset: 0x000C6D44
		public static void DropObject(GameObject objectToDrop)
		{
			GameObject inInventory = objectToDrop.InInventory;
			if (inInventory == null)
			{
				return;
			}
			InventoryActionEvent.Check(inInventory, inInventory, objectToDrop, "CommandDropObject", false, false, false, false, false, 0, 0, 0, null, null, null, null);
		}

		// Token: 0x04001355 RID: 4949
		private static Dictionary<string, InventoryAction> CheckActionTable = new Dictionary<string, InventoryAction>(16);
	}
}
