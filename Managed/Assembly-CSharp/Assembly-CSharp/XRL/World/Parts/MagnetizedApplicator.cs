using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02000E06 RID: 3590
	[Serializable]
	public class MagnetizedApplicator : IPart
	{
		// Token: 0x0600910D RID: 37133 RVA: 0x00344AC3 File Offset: 0x00342CC3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600910E RID: 37134 RVA: 0x00344ADC File Offset: 0x00342CDC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Apply")
			{
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				if (E.Item.IsBroken() || E.Item.IsRusted() || E.Item.IsEMPed())
				{
					E.Actor.Fail(this.ParentObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " nothing.");
					return false;
				}
				List<GameObject> objects = E.Actor.Inventory.GetObjects((GameObject o) => this.CanMagnetize(o, E.Actor));
				if (objects.Count == 0)
				{
					if (this.ParentObject.Understood())
					{
						E.Actor.Fail(this.ParentObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " nothing.");
					}
					else
					{
						E.Actor.Fail("You have no items that can be magnetized.");
					}
					return false;
				}
				GameObject gameObject = PickItem.ShowPicker(objects, null, PickItem.PickItemDialogStyle.SelectItemDialog, E.Actor, null, null, null, false, null, false, true, false);
				if (gameObject == null)
				{
					return false;
				}
				gameObject.SplitFromStack();
				if (E.Actor.IsPlayer())
				{
					this.ParentObject.MakeUnderstood(false);
				}
				string text = gameObject.Does("become", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " magnetized!";
				bool flag = gameObject.Understood();
				if (!ItemModding.ApplyModification(gameObject, new ModMagnetized(), true, E.Actor, false))
				{
					E.Actor.Fail("Nothing happens.");
					gameObject.CheckStack();
					return false;
				}
				if (E.Actor.IsPlayer())
				{
					Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					Popup.Show(this.ParentObject.Does("lose", int.MaxValue, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " its magnetic charge and crumbles to powder.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				if (flag && !gameObject.Understood())
				{
					gameObject.MakeUnderstood(false);
				}
				this.ParentObject.Destroy(null, false, false, null);
				gameObject.CheckStack();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600910F RID: 37135 RVA: 0x00344DB8 File Offset: 0x00342FB8
		private bool CanMagnetize(GameObject obj, GameObject by)
		{
			return ItemModding.ModificationApplicable("ModMagnetized", obj, by, null);
		}
	}
}
