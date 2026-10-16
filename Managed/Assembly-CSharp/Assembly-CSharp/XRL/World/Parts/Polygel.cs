using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000E07 RID: 3591
	[Serializable]
	public class Polygel : IPart
	{
		// Token: 0x06009111 RID: 37137 RVA: 0x00344DCF File Offset: 0x00342FCF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanBeReplicatedEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009112 RID: 37138 RVA: 0x00344DED File Offset: 0x00342FED
		public override bool HandleEvent(CanBeReplicatedEvent E)
		{
			return false;
		}

		// Token: 0x06009113 RID: 37139 RVA: 0x00344DF0 File Offset: 0x00342FF0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Apply")
			{
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				if (E.Item.IsBroken() || E.Item.IsRusted())
				{
					return E.Actor.Fail("The sprayer head won't move.");
				}
				Inventory inventory = E.Actor.Inventory;
				this.ParentObject.SplitFromStack();
				List<GameObject> list = Event.NewGameObjectList();
				inventory.GetObjects(list);
				list.Remove(this.ParentObject);
				GameObject gameObject = PickItem.ShowPicker(list, null, PickItem.PickItemDialogStyle.SelectItemDialog, E.Actor, null, null, null, false, null, false, true, false);
				if (gameObject == null)
				{
					return false;
				}
				if (gameObject.HasPart<Body>() || !CanBeReplicatedEvent.Check(gameObject, E.Actor, "Polygel", false))
				{
					Popup.ShowFail("A loud buzz is emitted. The unauthorized glyph flashes on the side of the applicator.", true, true, true);
					return false;
				}
				GameObject gameObject2 = null;
				try
				{
					gameObject.SplitStack(1, The.Player, false);
					gameObject2 = gameObject.DeepCopy(true, false, null);
					Inventory inventory2 = gameObject2.Inventory;
					if (inventory2 != null)
					{
						inventory2.Clear();
					}
					if (gameObject2.HasPart<EnergyCellSocket>())
					{
						gameObject2.GetPart<EnergyCellSocket>().Cell = null;
					}
					if (gameObject2.HasPart<MagazineAmmoLoader>())
					{
						gameObject2.GetPart<MagazineAmmoLoader>().Ammo = null;
					}
					Temporary.CarryOver(this.ParentObject, gameObject2, true);
					Phase.carryOver(this.ParentObject, gameObject2);
					if (!inventory.Objects.Contains(gameObject))
					{
						inventory.AddObject(gameObject, null, true, true, true, E);
					}
					if (!inventory.Objects.Contains(gameObject2))
					{
						inventory.AddObject(gameObject2, null, true, true, true, E);
					}
					try
					{
						if (E.Actor.IsPlayer() && !E.Item.Understood())
						{
							E.Item.MakeUnderstood(false);
							Popup.Show(this.ParentObject.Itis + " " + this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
					catch (Exception ex)
					{
						MetricsManager.LogError(ex);
					}
					try
					{
						this.ParentObject.Destroy(null, false, false, null);
					}
					catch (Exception ex2)
					{
						MetricsManager.LogError(ex2);
					}
					try
					{
						Popup.Show("The polygel morphs into another " + gameObject2.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					catch (Exception ex3)
					{
						MetricsManager.LogError(ex3);
					}
					try
					{
						WasReplicatedEvent.Send(gameObject, E.Actor, gameObject2, "Polygel", false);
					}
					catch (Exception ex4)
					{
						MetricsManager.LogError(ex4);
					}
					try
					{
						ReplicaCreatedEvent.Send(gameObject2, E.Actor, gameObject, "Polygel", false);
					}
					catch (Exception ex5)
					{
						MetricsManager.LogError(ex5);
					}
				}
				finally
				{
					if (gameObject2 != null)
					{
						gameObject2.CheckStack();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C7F RID: 15487
		public const string REPLICATION_CONTEXT = "Polygel";
	}
}
