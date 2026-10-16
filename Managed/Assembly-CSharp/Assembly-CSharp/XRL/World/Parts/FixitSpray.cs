using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000E05 RID: 3589
	[Serializable]
	public class FixitSpray : IPart
	{
		// Token: 0x06009108 RID: 37128 RVA: 0x00344707 File Offset: 0x00342907
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009109 RID: 37129 RVA: 0x0034470A File Offset: 0x0034290A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600910A RID: 37130 RVA: 0x00344720 File Offset: 0x00342920
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
					E.Actor.ShowFailure("The sprayer head won't move.");
					return false;
				}
				List<GameObject> inventoryAndEquipment = E.Actor.GetInventoryAndEquipment();
				this.ProcessCellForTargets(E.Actor, E.Actor.CurrentCell, inventoryAndEquipment);
				foreach (Cell cell in E.Actor.CurrentCell.GetLocalAdjacentCells())
				{
					this.ProcessCellForTargets(E.Actor, cell, inventoryAndEquipment);
				}
				GameObject gameObject = PickItem.ShowPicker(inventoryAndEquipment, null, PickItem.PickItemDialogStyle.SelectItemDialog, E.Actor, null, null, null, false, null, true, true, false);
				if (gameObject == null)
				{
					return false;
				}
				if (gameObject.HasTagOrProperty("NoRepair"))
				{
					E.Actor.ShowFailure("You can't repair that.");
					return false;
				}
				string text = null;
				int matterPhase = gameObject.GetMatterPhase();
				if (matterPhase >= 3 || !gameObject.PhaseMatches(E.Actor))
				{
					if (E.Actor.IsPlayer())
					{
						Popup.Show("Some sticky goop passes through " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				else if (matterPhase == 2)
				{
					if (E.Actor.IsPlayer())
					{
						Popup.Show("Some sticky goop mixes in with " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					if (liquidVolume != null)
					{
						liquidVolume.MixWith(new LiquidVolume("gel", 1), null, null, null, false);
					}
				}
				else
				{
					E.Actor.PlayWorldOrUISound("Sounds/Misc/sfx_interact_artifact_repair", null);
					if (E.Actor.IsPlayer())
					{
						if (gameObject == E.Actor)
						{
							Popup.Show("You are covered in sticky goop!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else
						{
							Popup.Show(gameObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " covered in sticky goop!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						this.ParentObject.MakeUnderstood(out text);
					}
					RepairedEvent.Send(E.Actor, gameObject, this.ParentObject, null, 0);
				}
				this.ParentObject.Destroy(null, false, false, null);
				if (!text.IsNullOrEmpty())
				{
					Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600910B RID: 37131 RVA: 0x00344A1C File Offset: 0x00342C1C
		private void ProcessCellForTargets(GameObject Actor, Cell C, List<GameObject> Objects)
		{
			if (C != null)
			{
				IList<GameObject> list2;
				if (!C.IsSolidFor(Actor, false))
				{
					IList<GameObject> list = C.Objects;
					list2 = list;
				}
				else
				{
					IList<GameObject> list = C.GetCanInteractInCellWithSolidObjectsFor(Actor);
					list2 = list;
				}
				IList<GameObject> list3 = list2;
				int i = 0;
				int count = list3.Count;
				while (i < count)
				{
					GameObject gameObject = list3[i];
					Physics physics = gameObject.Physics;
					if (physics != null && physics.IsReal && IComponent<GameObject>.Visible(gameObject) && gameObject.Render != null && gameObject.Render.RenderLayer > 0 && !Objects.Contains(gameObject) && gameObject != this.ParentObject)
					{
						Objects.Add(gameObject);
					}
					i++;
				}
			}
		}
	}
}
