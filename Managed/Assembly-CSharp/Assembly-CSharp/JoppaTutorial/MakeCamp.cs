using System;
using System.Linq;
using Genkit;
using Qud.UI;
using XRL;
using XRL.UI;
using XRL.World;
using XRL.World.Parts;

namespace JoppaTutorial
{
	// Token: 0x020003F7 RID: 1015
	public class MakeCamp : TutorialStep
	{
		// Token: 0x060025E9 RID: 9705 RVA: 0x000C369F File Offset: 0x000C189F
		public override bool BeforePlayerEnterCell(Cell cell)
		{
			if (this.step == 500)
			{
				string text;
				if (cell == null)
				{
					text = null;
				}
				else
				{
					Zone parentZone = cell.ParentZone;
					text = ((parentZone != null) ? parentZone.ZoneID : null);
				}
				if (text == "JoppaWorld")
				{
					return true;
				}
			}
			return base.ConstrainToCurrentZone(cell);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000C36DB File Offset: 0x000C18DB
		public override bool AllowSelectedPopupCommand(string title, QudMenuItem command)
		{
			return true;
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000C36DE File Offset: 0x000C18DE
		public override void OnTrigger(string trigger)
		{
			if (trigger == "AteRandom")
			{
				TutorialManager.ShowIntermissionPopupAsync("Tastes good.", delegate
				{
					this.step = 400;
				}, 0, 0f, "sw");
			}
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000C370F File Offset: 0x000C190F
		public override void GameSync()
		{
			if (this.step == 0)
			{
				this.step = 10;
			}
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000C3721 File Offset: 0x000C1921
		public override bool OnTradeComplete()
		{
			this.step = 500;
			return true;
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000C372F File Offset: 0x000C192F
		public override bool OnTradeOffer()
		{
			this.step = 420;
			return true;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x000C3740 File Offset: 0x000C1940
		public override void LateUpdate()
		{
			this.manager.ClearHighlight();
			if (this.campfire == null)
			{
				Zone currentZone = The.Player.CurrentZone;
				this.campfire = ((currentZone != null) ? currentZone.FindObject("Campfire") : null);
			}
			if (this.beetle == null)
			{
				Zone currentZone2 = The.Player.CurrentZone;
				this.beetle = ((currentZone2 != null) ? currentZone2.FindObject("TutorialClockworkBeetlePariah") : null);
				GameObject gameObject = this.beetle;
				GameObject gameObject2;
				if (gameObject == null)
				{
					gameObject2 = null;
				}
				else
				{
					gameObject2 = (from o in gameObject.GetPart<Inventory>().GetObjects()
						where o.Blueprint == "Witchwood Bark"
						select o).FirstOrDefault<GameObject>();
				}
				this.witchwood = gameObject2;
				this.dagger = (from o in The.Player.GetPart<Inventory>().GetObjects()
					where o.Blueprint == "TutorialDagger"
					select o).FirstOrDefault<GameObject>();
			}
			if (base.CurrentGameView == "Stage" && The.Player.CurrentZone.ZoneID == "JoppaWorld")
			{
				TutorialManager.AdvanceStep(new ExploreWorldMap());
				return;
			}
			if (this.step == 10 && base.CurrentGameView == "Stage")
			{
				The.Player.GetPart<Stomach>().CookingCounter = The.Player.GetPart<Stomach>().CalculateCookingIncrement();
				The.Player.GetPart<Stomach>().UpdateHunger();
				this.step = 100;
				TutorialManager.ShowCIDPopupAsync("Stage:TopButtonBar:ZoneText", "We’re on the surface now. We can walk the whole world this way, or we can access the worldmap to fast travel.", "sw", "[~Accept] Continue", 16, 16, 0f, delegate
				{
					this.step = 200;
					TutorialManager.ShowCIDPopupAsync("Stage:TopButtonBar:FoodWaterStatus", "But first, you just became hungry. You need to eat.", "ne", "[~Accept] Continue", 16, 16, 0f, delegate
					{
						this.step = 250;
					});
				});
			}
			if (this.step == 250 && base.CurrentGameView == "Stage")
			{
				if (The.Player.CurrentCell.Location == Location2D.Get(48, 14))
				{
					this.step = 300;
				}
				else
				{
					this.manager.HighlightCell(48, 14, "Let's make camp. How about next to that beetle over there.", "sw", 3f, 3f, 0f);
				}
			}
			if (this.step == 300 && base.CurrentGameView == "Stage")
			{
				if (this.campfire != null)
				{
					this.manager.HighlightObject(this.campfire, "Use the campfire.", "nw", 4f, 4f);
				}
				else
				{
					this.manager.HighlightByCID("AbilityBar:Button:CommandSurvivalCamp", "Make camp.", "nw", 4, 4, 0f, "horiz");
				}
				if (The.Player.GetPart<Stomach>().HungerLevel == 0 && base.CurrentGameView == "Stage")
				{
					this.step = 350;
				}
			}
			if (this.step >= 400 && this.step < 500)
			{
				GameObject gameObject3 = this.beetle;
				if (gameObject3 != null && gameObject3.IsValid())
				{
					GameObject gameObject4 = this.witchwood;
					if (((gameObject4 != null) ? gameObject4.InInventory : null) == this.beetle)
					{
						goto IL_0311;
					}
				}
				this.step = 500;
				IL_0311:
				if (base.CurrentGameView == "Stage")
				{
					if (The.Player.DistanceTo(this.beetle) <= 1)
					{
						string text = " or ~AdventureMouseContextAction.";
						if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
						{
							text = ".";
						}
						this.manager.HighlightObject(this.beetle, "Let's not be rude. Talk to the beetle.\n\nPress ~CmdUse" + text, "ne", 3f, 3f);
					}
					else
					{
						this.manager.HighlightObject(this.beetle, "Let's not be rude. Talk to the beetle.", "ne", 3f, 3f);
					}
				}
				if (GameManager.Instance.CurrentGameView == "PopupMessage")
				{
					string lastPopupID = PopupMessage.lastPopupID;
					if (lastPopupID != null && lastPopupID.StartsWith("Conversation:" + this.beetle.ID))
					{
						this.manager.HighlightByCID("QudTextMenuItem:trade", "Not much of a talker. But that's not true for everyone. As long as a creature isn't hostile, you can try to chat with them.\n\nLet see what the beetle has for trade.", (Media.sizeClass <= Media.SizeClass.Small) ? "ne" : "s", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
					}
				}
				if (base.CurrentGameView == "ModernTrade")
				{
					if (this.step == 400)
					{
						this.step = 405;
						TutorialManager.ShowCIDPopupAsync("Trade:Amounts", "The beetle's inventory is on the left, and your inventory is on the right.\n\nThe beetle has some witchwood bark. That's useful — it heals you when you eat it. Let's trade for it.", "s", "[~Accept] Continue", 16, 16, 0f, delegate
						{
							this.step = 410;
						});
					}
					if (this.step == 410)
					{
						if (this.witchwood != null && this.witchwood.InInventory == this.beetle && SingletonWindowBase<TradeScreen>.instance.howManySelected(this.witchwood) <= 0)
						{
							string text2 = " or ~AdventureMouseContextAction.";
							if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
							{
								text2 = ".";
							}
							this.manager.HighlightByCID("TradeLine:item:" + this.witchwood.ID, "The beetle has some witchwood bark. That's useful — it heals you when you eat it. Let's trade for it.\n\nPress ~CmdTradeAdd" + text2, "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
						}
						else if (this.dagger != null && this.dagger.InInventory == The.Player && SingletonWindowBase<TradeScreen>.instance.howManySelected(this.dagger) <= 0)
						{
							string text3 = " or ~AdventureMouseContextAction.";
							if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
							{
								text3 = ".";
							}
							this.manager.HighlightByCID("TradeLine:item:" + this.dagger.ID, "We don't need this dagger any more, so we can trade it away.\n\nPress ~CmdTradeAdd" + text3, "ne", 64, TutorialManager.TWIDDLE_MENU_YPADDING, 0f, "horiz");
						}
						else
						{
							this.manager.HighlightByCID("Trade:Offer", "Complete the trade.", "se", 64, 64, 0f, "horiz");
						}
					}
				}
				if (this.step == 420 && base.CurrentGameView == "PopupMessage")
				{
					this.manager.HighlightByCID("PopupMessage", "The trade is uneven, so we'll have to pony up some money.\n\nIn the salt-stippled ecosystem of Qud, {{B|fresh water}} is currency. You need to drink it, too, so don't spend it all.", "se", 64, 64, 0f, "horiz");
				}
			}
			if (this.step == 500 && base.CurrentGameView == "Stage")
			{
				if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
				{
					this.manager.HighlightByCID("Stage:TopButtonBar:UpButton", "Now to go to the world map. Try ascending again.\n\nPress ~CmdMoveU.", "sw", 64, 64, 0f, "horiz");
					return;
				}
				this.manager.HighlightByCID("Stage:TopButtonBar:UpButton", "Now to go to the world map. Try ascending again.\n\nYou can click this button or press ~CmdMoveU.", "sw", 64, 64, 0f, "horiz");
			}
		}

		// Token: 0x040012FF RID: 4863
		public GameObject campfire;

		// Token: 0x04001300 RID: 4864
		public GameObject beetle;

		// Token: 0x04001301 RID: 4865
		public GameObject witchwood;

		// Token: 0x04001302 RID: 4866
		public GameObject dagger;
	}
}
