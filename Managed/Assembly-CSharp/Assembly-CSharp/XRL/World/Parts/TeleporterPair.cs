using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020010AD RID: 4269
	[Serializable]
	public class TeleporterPair : IPoweredPart
	{
		// Token: 0x0600A9CB RID: 43467 RVA: 0x003E14C8 File Offset: 0x003DF6C8
		public TeleporterPair()
		{
			this.ChargeUse = 0;
			this.WorksOnCellContents = true;
			this.WorksOnAdjacentCellContents = true;
		}

		// Token: 0x0600A9CC RID: 43468 RVA: 0x003E1519 File Offset: 0x003DF719
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A9CD RID: 43469 RVA: 0x003E151C File Offset: 0x003DF71C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A9CE RID: 43470 RVA: 0x003E1552 File Offset: 0x003DF752
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("travel", 5);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9CF RID: 43471 RVA: 0x003E1575 File Offset: 0x003DF775
		public override bool HandleEvent(EquippedEvent E)
		{
			this.SyncLocation();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9D0 RID: 43472 RVA: 0x003E1584 File Offset: 0x003DF784
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.SyncLocation();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9D1 RID: 43473 RVA: 0x003E1594 File Offset: 0x003DF794
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			long num = (long)this.Cooldown - (The.Game.Turns - this.lastUsed);
			if (num <= 0L)
			{
				E.AddAction("Activate", "activate", "ActivateTeleporterPair", null, 'a', false, 100, 0, false, false, false, false, true, null, false);
			}
			else
			{
				E.AddAction("Activate", "{{K|activate}} [{{C|" + num.ToString() + "}} turn cooldown]", "ActivateTeleporterPair", null, 'a', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9D2 RID: 43474 RVA: 0x003E1624 File Offset: 0x003DF824
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ActivateTeleporterPair" && this.AttemptTeleport(E.Actor, E))
			{
				E.Actor.UseEnergy(1000, "Item Teleporter Pair", null, null, false);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A9D3 RID: 43475 RVA: 0x003E1680 File Offset: 0x003DF880
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A9D4 RID: 43476 RVA: 0x003E1683 File Offset: 0x003DF883
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			Registrar.Register("AddedToInventory");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A9D5 RID: 43477 RVA: 0x003E16A4 File Offset: 0x003DF8A4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Equipped" || E.ID == "Unequipped" || E.ID == "EnteredCell" || E.ID == "AddedToInventory" || E.ID == "OnDestroyObject")
			{
				this.SyncLocation();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A9D6 RID: 43478 RVA: 0x003E1718 File Offset: 0x003DF918
		public void UpdateTracking()
		{
			GameObject gameObject = this.ParentObject.Equipped ?? this.ParentObject.InInventory;
			if (gameObject != this.tracking)
			{
				this.Untrack();
				this.tracking = gameObject;
				if (this.tracking != null)
				{
					this.tracking.RegisterPartEvent(this, "Equipped");
					this.tracking.RegisterPartEvent(this, "Unequipped");
					this.tracking.RegisterPartEvent(this, "EnteredCell");
					this.tracking.RegisterPartEvent(this, "AddedToInventory");
					this.tracking.RegisterPartEvent(this, "OnDestroyObject");
				}
			}
		}

		// Token: 0x0600A9D7 RID: 43479 RVA: 0x003E17B4 File Offset: 0x003DF9B4
		public void Untrack()
		{
			if (this.tracking == null)
			{
				return;
			}
			this.tracking.UnregisterPartEvent(this, "Equipped");
			this.tracking.UnregisterPartEvent(this, "Unequipped");
			this.tracking.UnregisterPartEvent(this, "EnteredCell");
			this.tracking.UnregisterPartEvent(this, "AddedToInventory");
			this.tracking.UnregisterPartEvent(this, "OnDestroyObject");
			this.tracking = null;
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600A9D8 RID: 43480 RVA: 0x003E1826 File Offset: 0x003DFA26
		// (set) Token: 0x0600A9D9 RID: 43481 RVA: 0x003E1858 File Offset: 0x003DFA58
		public long lastUsed
		{
			get
			{
				if (The.Game.GetObjectGameState(this.CooldownKey) == null)
				{
					return (long)(-(long)this.Cooldown);
				}
				return (long)The.Game.GetObjectGameState(this.CooldownKey);
			}
			set
			{
				The.Game.SetObjectGameState(this.CooldownKey, value);
			}
		}

		// Token: 0x0600A9DA RID: 43482 RVA: 0x003E1870 File Offset: 0x003DFA70
		public bool AttemptTeleport(GameObject who, IEvent FromEvent = null)
		{
			if (string.IsNullOrEmpty(this.DestinationKey))
			{
				if (who.IsPlayer())
				{
					Popup.ShowFail("Nothing happens.", true, true, true);
				}
				return false;
			}
			string stringGameState = The.Game.GetStringGameState(this.DestinationKey, "");
			if (string.IsNullOrEmpty(stringGameState) || !this.IsObjectActivePartSubject(who))
			{
				if (who.IsPlayer())
				{
					Popup.ShowFail("Nothing happens.", true, true, true);
				}
				return false;
			}
			string[] array = stringGameState.Split('@', StringSplitOptions.None);
			string text = array[0];
			if (who.AreHostilesNearby() && !who.InZone(text))
			{
				if (who.IsPlayer())
				{
					Popup.ShowFail("You can't teleport with hostiles nearby!", true, true, true);
				}
				return false;
			}
			if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (who.IsPlayer())
				{
					Popup.ShowFail("Nothing happens.", true, true, true);
				}
				return false;
			}
			string[] array2 = array[1].Split(',', StringSplitOptions.None);
			int num = Convert.ToInt32(array2[0]);
			int num2 = Convert.ToInt32(array2[1]);
			int num3 = (int)((long)this.Cooldown - (The.Game.Turns - this.lastUsed));
			if (num3 > 0)
			{
				if (who.IsPlayer())
				{
					Popup.Show(string.Concat(new string[]
					{
						"You must wait ",
						num3.Things("turn", null),
						" before using ",
						this.ParentObject.indicativeProximal,
						" again."
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			bool flag = who.ZoneTeleport(text, num, num2, FromEvent, this.ParentObject, who, null, "You are transported!", true, null);
			if (flag)
			{
				this.lastUsed = The.Game.Turns;
			}
			return flag;
		}

		// Token: 0x0600A9DB RID: 43483 RVA: 0x003E1A1C File Offset: 0x003DFC1C
		private void SyncLocation()
		{
			this.UpdateTracking();
			Cell currentCell = this.ParentObject.GetCurrentCell();
			if (currentCell != null && currentCell != this.lastCell)
			{
				The.Game.SetStringGameState(this.LocationKey, currentCell.GetAddress());
				this.lastCell = currentCell;
			}
		}

		// Token: 0x040045FE RID: 17918
		public int Cooldown = 100;

		// Token: 0x040045FF RID: 17919
		public string CooldownKey = "TeleporterOrbLastUsed";

		// Token: 0x04004600 RID: 17920
		public string LocationKey = "";

		// Token: 0x04004601 RID: 17921
		public string DestinationKey = "";

		// Token: 0x04004602 RID: 17922
		public GameObject tracking;

		// Token: 0x04004603 RID: 17923
		[NonSerialized]
		public Cell lastCell;
	}
}
