using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E08 RID: 3592
	[Serializable]
	public class Spraybottle : IPart
	{
		// Token: 0x06009115 RID: 37141 RVA: 0x00345144 File Offset: 0x00343344
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009116 RID: 37142 RVA: 0x0034515C File Offset: 0x0034335C
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
				LiquidVolume liquidVolume = this.ParentObject.LiquidVolume;
				if (liquidVolume.Volume <= 0)
				{
					return E.Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " empty.");
				}
				List<GameObject> equippedObjects = E.Actor.Body.GetEquippedObjects();
				equippedObjects.AddRange(E.Actor.Inventory.GetObjects());
				equippedObjects.Remove(this.ParentObject);
				GameObject gameObject = PickItem.ShowPicker(equippedObjects, "Fungal Infection", PickItem.PickItemDialogStyle.SelectItemDialog, E.Actor, null, null, null, false, null, true, true, false);
				if (gameObject == null)
				{
					return false;
				}
				if (E.Actor.IsPlayer())
				{
					Popup.Show(gameObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " covered in " + liquidVolume.GetLiquidName() + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					this.ParentObject.MakeUnderstood(false);
				}
				gameObject.ApplyEffect(new LiquidCovered(liquidVolume, 1, Stat.Random(5, 10), false, null, false), null);
			}
			return base.HandleEvent(E);
		}
	}
}
