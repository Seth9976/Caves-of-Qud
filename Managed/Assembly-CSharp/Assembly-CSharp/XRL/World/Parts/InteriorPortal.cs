using System;
using Genkit;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F74 RID: 3956
	[Serializable]
	public class InteriorPortal : IPart
	{
		// Token: 0x06009D76 RID: 40310 RVA: 0x00390BF7 File Offset: 0x0038EDF7
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009D77 RID: 40311 RVA: 0x00390BFC File Offset: 0x0038EDFC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetTransitiveLocationEvent>.ID || ID == EnteredCellEvent.ID || ID == LeftCellEvent.ID;
		}

		// Token: 0x06009D78 RID: 40312 RVA: 0x00390C55 File Offset: 0x0038EE55
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x06009D79 RID: 40313 RVA: 0x00390C6D File Offset: 0x0038EE6D
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				this.TryExit(E.Actor);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D7A RID: 40314 RVA: 0x00390C94 File Offset: 0x0038EE94
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.ParentObject.CurrentCell != null)
			{
				E.AddAction("Exit", "exit", "ExitInterior", null, 'e', false, 15, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D7B RID: 40315 RVA: 0x00390CDC File Offset: 0x0038EEDC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ExitInterior")
			{
				if (this.TryExit(E.Actor))
				{
					E.RequestInterfaceExit();
				}
				else
				{
					Popup.Show("You can't exit here.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D7C RID: 40316 RVA: 0x00390D30 File Offset: 0x0038EF30
		public override bool HandleEvent(GetTransitiveLocationEvent E)
		{
			if (E.Zone == this.ParentObject.CurrentZone && E.IsIngress)
			{
				Cell placementCell = this.GetPlacementCell(E.Actor);
				int num = 500;
				GameObject source = E.Source;
				string text = ((source != null) ? source.GetTagOrStringProperty("PortalKey", null) : null);
				if (!text.IsNullOrEmpty() && text == this.ParentObject.GetTagOrStringProperty("PortalKey", null))
				{
					num = 1000;
				}
				E.AddLocation(placementCell, this.ParentObject, num);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D7D RID: 40317 RVA: 0x00390DC0 File Offset: 0x0038EFC0
		public Cell GetPlacementCell(GameObject For = null)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			Cell cell = currentCell;
			if ((For != null && !cell.IsPassable(For, true)) || this.ExitOnEnteredCell)
			{
				string generalDirectionFrom = cell.GetGeneralDirectionFrom(Location2D.Get(40, 12));
				Cell cellFromDirection = cell.GetCellFromDirection(generalDirectionFrom, true);
				cell = (cellFromDirection.IsPassable(For, true) ? cellFromDirection : cell.getClosestPassableCellForExcept(For, Event.NewCellList(currentCell)));
			}
			return cell;
		}

		// Token: 0x06009D7E RID: 40318 RVA: 0x00390E28 File Offset: 0x0038F028
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object != this.ParentObject)
			{
				if (!this.Open && this.OpenOnEnteredCell)
				{
					this.SetOpened(true);
				}
				if (this.ExitOnEnteredCell)
				{
					this.TryExit(E.Object);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D7F RID: 40319 RVA: 0x00390E76 File Offset: 0x0038F076
		public override bool HandleEvent(EnteredCellEvent E)
		{
			E.Cell.ParentZone.AddZoneConnection("-", E.Cell.X, E.Cell.Y, "Exit", null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009D80 RID: 40320 RVA: 0x00390EB0 File Offset: 0x0038F0B0
		public override bool HandleEvent(LeftCellEvent E)
		{
			E.Cell.ParentZone.RemoveZoneConnection("-", E.Cell.X, E.Cell.Y, "Exit", null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009D81 RID: 40321 RVA: 0x00390EEC File Offset: 0x0038F0EC
		public void SetOpened(bool Value)
		{
			if (Value == this.Open)
			{
				return;
			}
			Render render = this.ParentObject.Render;
			Zone currentZone = this.ParentObject.CurrentZone;
			if (this.Open)
			{
				if (this.OpenDisplay.IsNullOrEmpty())
				{
					this.OpenDisplay = render.RenderString;
				}
				if (this.OpenTile.IsNullOrEmpty())
				{
					this.OpenTile = render.Tile;
				}
				if (!this.ClosedDisplay.IsNullOrEmpty())
				{
					render.RenderString = this.ClosedDisplay;
				}
				if (!this.ClosedTile.IsNullOrEmpty())
				{
					render.Tile = this.ClosedTile;
				}
				this.PlayCoalescedSound(this.ParentObject, "CloseSound");
				this.Open = false;
				return;
			}
			if (this.ClosedDisplay.IsNullOrEmpty())
			{
				this.ClosedDisplay = render.RenderString;
			}
			if (this.ClosedTile.IsNullOrEmpty())
			{
				this.ClosedTile = render.Tile;
			}
			if (!this.OpenDisplay.IsNullOrEmpty())
			{
				render.RenderString = this.OpenDisplay;
			}
			if (!this.OpenTile.IsNullOrEmpty())
			{
				render.Tile = this.OpenTile;
			}
			this.PlayCoalescedSound(this.ParentObject, "OpenSound");
			this.Open = true;
		}

		// Token: 0x06009D82 RID: 40322 RVA: 0x0039101C File Offset: 0x0038F21C
		public void PlayCoalescedSound(GameObject Source, string Clip)
		{
			if (!GameObject.Validate(Source))
			{
				return;
			}
			string text = this.ParentObject.GetTagOrStringProperty(Clip, null);
			if (text.IsNullOrEmpty())
			{
				InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
				string text2;
				if (interiorZone == null)
				{
					text2 = null;
				}
				else
				{
					GameObject parentObject = interiorZone.ParentObject;
					text2 = ((parentObject != null) ? parentObject.GetTagOrStringProperty(Clip, null) : null);
				}
				text = text2;
			}
			if (!text.IsNullOrEmpty())
			{
				Source.PlayWorldSound(text, 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
		}

		// Token: 0x06009D83 RID: 40323 RVA: 0x003910A1 File Offset: 0x0038F2A1
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009D84 RID: 40324 RVA: 0x003910A4 File Offset: 0x0038F2A4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ClimbDown");
			Registrar.Register("PortalTransition");
		}

		// Token: 0x06009D85 RID: 40325 RVA: 0x003910BC File Offset: 0x0038F2BC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ClimbDown")
			{
				GameObject gameObject = E.GetParameter("GO") as GameObject;
				if (gameObject != null)
				{
					return !this.TryExit(gameObject);
				}
			}
			if (E.ID == "PortalTransition" && !this.Open)
			{
				this.SetOpened(true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009D86 RID: 40326 RVA: 0x00391122 File Offset: 0x0038F322
		[Obsolete("Use TryExit")]
		public bool Exit(GameObject Actor)
		{
			return this.TryExit(Actor);
		}

		// Token: 0x06009D87 RID: 40327 RVA: 0x0039112C File Offset: 0x0038F32C
		public bool TryExit(GameObject Actor)
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			if (interiorZone != null)
			{
				Zone zone = interiorZone.ResolveParentZone();
				Cell cell;
				GameObject gameObject;
				GetTransitiveLocationEvent.GetFor(zone, "Egress,Interior", Actor, this.ParentObject, this.ParentObject.CurrentCell, out cell, out gameObject);
				if (cell == null)
				{
					cell = ((zone != null) ? zone.GetPullDownLocation(Actor) : null) ?? interiorZone.GetEscapeCell(Actor);
				}
				if (cell == null)
				{
					return false;
				}
				if (cell.OnWorldMap())
				{
					return Actor.ShowFailure("You cannot do that while traveling on the world map.");
				}
				this.PlayCoalescedSound(gameObject, "CloseSound");
				if (Actor.IsPlayer() && !this.Open)
				{
					this.PlayCoalescedSound(this.ParentObject, "OpenSound");
				}
				else
				{
					this.SetOpened(true);
				}
				FlushWeightCacheEvent.Send(interiorZone.ParentObject);
				if (Actor.DirectMoveTo(cell, 0, false, false, false, null))
				{
					this.PlayCoalescedSound(gameObject, "ExitSound");
					if (gameObject != null)
					{
						gameObject.FireEvent("PortalTransition");
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009D88 RID: 40328 RVA: 0x0039121A File Offset: 0x0038F41A
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009D89 RID: 40329 RVA: 0x00391220 File Offset: 0x0038F420
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Open)
			{
				int closeTimer = this.CloseTimer;
				this.CloseTimer = closeTimer + 1;
				if (closeTimer >= 3)
				{
					if (!this.ParentObject.CurrentCell.HasCombatObject(false))
					{
						this.SetOpened(false);
					}
					this.CloseTimer = 0;
				}
			}
			base.TurnTick(TimeTick, Amount);
		}

		// Token: 0x0400411D RID: 16669
		public bool Open;

		// Token: 0x0400411E RID: 16670
		public bool OpenOnEnteredCell;

		// Token: 0x0400411F RID: 16671
		public bool ExitOnEnteredCell;

		// Token: 0x04004120 RID: 16672
		public string ClosedDisplay;

		// Token: 0x04004121 RID: 16673
		public string OpenDisplay;

		// Token: 0x04004122 RID: 16674
		public string ClosedTile;

		// Token: 0x04004123 RID: 16675
		public string OpenTile;

		// Token: 0x04004124 RID: 16676
		[NonSerialized]
		private int CloseTimer;
	}
}
