using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001250 RID: 4688
	[Serializable]
	public class Container : IPart
	{
		// Token: 0x0600BA39 RID: 47673 RVA: 0x0043FF2E File Offset: 0x0043E12E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600BA3A RID: 47674 RVA: 0x0043FF5C File Offset: 0x0043E15C
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600BA3B RID: 47675 RVA: 0x0043FF74 File Offset: 0x0043E174
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				this.AttemptOpen(E.Actor, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA3C RID: 47676 RVA: 0x0043FF98 File Offset: 0x0043E198
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Open", "open", "Open", null, 'o', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA3D RID: 47677 RVA: 0x0043FFCF File Offset: 0x0043E1CF
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Open")
			{
				this.AttemptOpen(E.Actor, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA3E RID: 47678 RVA: 0x0043FFF8 File Offset: 0x0043E1F8
		public void PlayOpenSound()
		{
			string text;
			if ((text = this.OpenSound) == null)
			{
				text = (this.OpenSound = this.ParentObject.GetTagOrStringProperty("OpenSound", null));
			}
			string text2 = text;
			if (!text2.IsNullOrEmpty())
			{
				base.PlayWorldSound(text2, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			}
		}

		// Token: 0x0600BA3F RID: 47679 RVA: 0x0044005C File Offset: 0x0043E25C
		public void PlayCloseSound()
		{
			string text;
			if ((text = this.CloseSound) == null)
			{
				text = (this.CloseSound = this.ParentObject.GetTagOrStringProperty("CloseSound", null));
			}
			string text2 = text;
			if (!text2.IsNullOrEmpty())
			{
				base.PlayWorldSound(text2, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			}
		}

		// Token: 0x0600BA40 RID: 47680 RVA: 0x004400C0 File Offset: 0x0043E2C0
		public void AttemptOpen(GameObject Actor, IEvent ParentEvent = null)
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				Inventory inventory = parentObject.Inventory;
				if (inventory != null)
				{
					inventory.VerifyContents();
				}
			}
			if (!this.ParentObject.IsValid() || !this.ParentObject.FireEvent("BeforeOpen"))
			{
				return;
			}
			this.ParentObject.FireEvent("Opening");
			this.ParentObject.SetIntProperty("Autoexplored", 1, false);
			Inventory inventory2 = this.ParentObject.Inventory;
			bool flag;
			if (inventory2 == null)
			{
				flag = false;
			}
			else
			{
				flag = inventory2.HasObjectDirect((GameObject x) => x.HasIntProperty("StoredByPlayer"));
			}
			bool flag2 = flag;
			if (this.ParentObject.IsCreature)
			{
				if (Actor.IsPlayer())
				{
					if (Actor.PhaseMatches(this.ParentObject) && !this.ParentObject.HasPropertyOrTag("NoTrade") && !this.ParentObject.HasPropertyOrTag("FugueCopy") && Actor.DistanceTo(this.ParentObject) <= 1)
					{
						this.PlayOpenSound();
						if (this.ParentObject.IsPlayerLed())
						{
							TradeUI.ShowTradeScreen(this.ParentObject, 0f, TradeUI.TradeScreenMode.Trade);
						}
						else if (this.ParentObject.IsPlayer())
						{
							Screens.CurrentScreen = 2;
							Screens.Show(The.Player);
						}
						else
						{
							TradeUI.ShowTradeScreen(this.ParentObject, 1f, TradeUI.TradeScreenMode.Trade);
						}
						this.PlayCloseSound();
						return;
					}
					Popup.ShowFail("You cannot trade with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
				}
				return;
			}
			if (!this.ParentObject.HasTagOrProperty("DontWarnOnOpen") && Actor.IsPlayer() && !string.IsNullOrEmpty(this.ParentObject.Owner) && this.ParentObject.Equipped != IComponent<GameObject>.ThePlayer && this.ParentObject.InInventory != IComponent<GameObject>.ThePlayer)
			{
				if (Popup.ShowYesNoCancel("That is not owned by you. Are you sure you want to open it?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
				{
					return;
				}
				AIHelpBroadcastEvent.Send(this.ParentObject, Actor, null, null, 20, 1f, HelpCause.Trespass);
			}
			if (Actor.IsPlayer())
			{
				Inventory inventory3 = this.ParentObject.Inventory;
				if (inventory3 == null || inventory3.GetObjectCount() == 0)
				{
					if (Popup.ShowYesNo("There's nothing " + this.Preposition + " that. Would you like to store an item?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						this.PlayOpenSound();
						TradeUI.ShowTradeScreen(this.ParentObject, 0f, TradeUI.TradeScreenMode.Container);
						this.PlayCloseSound();
						if (inventory3.GetObjectCount() > 0)
						{
							Actor.FireEvent(Event.New("PutSomethingIn", "Object", this.ParentObject));
						}
					}
				}
				else
				{
					List<GameObject> list = new List<GameObject>(inventory3.GetObjectsDirect());
					bool flag3 = false;
					string text = string.Concat(new string[]
					{
						"{{W|",
						(this.Preposition == "in") ? "Opening" : "Examining",
						" ",
						this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false),
						"}}"
					});
					bool flag4 = false;
					this.PlayOpenSound();
					IList<GameObject> objects = inventory3.GetObjects();
					string text2 = null;
					PickItem.PickItemDialogStyle pickItemDialogStyle = PickItem.PickItemDialogStyle.GetItemDialog;
					GameObject parentObject2 = this.ParentObject;
					Cell cell = null;
					Func<List<GameObject>> func = new Func<List<GameObject>>(inventory3.GetObjects);
					PickItem.ShowPicker(objects, ref flag3, text2, pickItemDialogStyle, Actor, parentObject2, cell, text, false, func, false, true, flag4);
					this.PlayCloseSound();
					if (flag3 && ParentEvent != null)
					{
						ParentEvent.RequestInterfaceExit();
					}
					if (list.Count < inventory3.GetObjectCountDirect() && inventory3.GetObjectCount() > 0)
					{
						Actor.FireEvent(Event.New("PutSomethingIn", "Object", this.ParentObject));
					}
					GameObject gameObject = null;
					double num = 0.0;
					double num2 = -1.0;
					for (int i = 0; i < list.Count; i++)
					{
						GameObject gameObject2 = list[i];
						if (!inventory3.Objects.Contains(gameObject2) && this.ParentObject.IsOwned() && !gameObject2.OwnedByPlayer)
						{
							double valueEach = gameObject2.ValueEach;
							num += valueEach * (double)gameObject2.Count;
							if (valueEach > num2)
							{
								num2 = valueEach;
								gameObject = gameObject2;
							}
						}
					}
					if (gameObject != null)
					{
						float num3 = Mathf.Max(1f, (float)(num / 20.0));
						AIHelpBroadcastEvent.Send(this.ParentObject, Actor, gameObject, null, 20, num3, HelpCause.Theft);
					}
				}
				Inventory inventory4 = this.ParentObject.Inventory;
				bool flag5;
				if (inventory4 == null)
				{
					flag5 = false;
				}
				else
				{
					flag5 = inventory4.HasObjectDirect((GameObject x) => x.HasIntProperty("StoredByPlayer"));
				}
				bool flag6 = flag5;
				if (flag2 || flag6)
				{
					Inventory inventory5 = this.ParentObject.Inventory;
					if (inventory5 == null)
					{
						return;
					}
					inventory5.TryStoreBackup();
				}
			}
		}

		// Token: 0x0600BA41 RID: 47681 RVA: 0x00440579 File Offset: 0x0043E779
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600BA42 RID: 47682 RVA: 0x0044057C File Offset: 0x0043E77C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Open");
		}

		// Token: 0x0600BA43 RID: 47683 RVA: 0x00440589 File Offset: 0x0043E789
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Open")
			{
				this.AttemptOpen(E.GetGameObjectParameter("Opener"), E);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004ADC RID: 19164
		public string Preposition = "in";

		// Token: 0x04004ADD RID: 19165
		public string OpenSound = "Sounds/Interact/sfx_interact_open_genericContainer";

		// Token: 0x04004ADE RID: 19166
		public string CloseSound;
	}
}
