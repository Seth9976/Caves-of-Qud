using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020011BF RID: 4543
	[Serializable]
	public class CursedCellSocket : IPart
	{
		// Token: 0x0600B47A RID: 46202 RVA: 0x0041D363 File Offset: 0x0041B563
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B47B RID: 46203 RVA: 0x0041D366 File Offset: 0x0041B566
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeRemoveCell");
		}

		// Token: 0x0600B47C RID: 46204 RVA: 0x0041D373 File Offset: 0x0041B573
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "BeforeRemoveCell") && base.FireEvent(E);
		}

		// Token: 0x0600B47D RID: 46205 RVA: 0x0041D390 File Offset: 0x0041B590
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CellChangedEvent.ID || ID == PooledEvent<CellDepletedEvent>.ID || ID == CanBeSlottedEvent.ID;
		}

		// Token: 0x0600B47E RID: 46206 RVA: 0x0041D3B8 File Offset: 0x0041B5B8
		public override bool HandleEvent(CellChangedEvent E)
		{
			if (E.NewCell != null)
			{
				EnergyCell part = E.NewCell.GetPart<EnergyCell>();
				if (part != null)
				{
					part.IsRechargeable = false;
				}
				LiquidVolume liquidVolume = E.NewCell.LiquidVolume;
				if (liquidVolume != null)
				{
					liquidVolume.Sealed = true;
					liquidVolume.ManualSeal = false;
					liquidVolume.Collector = false;
					liquidVolume.AutoCollectLiquidType = null;
				}
				if (E.Actor.IsPlayer())
				{
					Popup.Show(E.NewCell.Does("lock", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " firmly into the socket, preventing removal.", null, "Sounds/UI/ui_notification", true, true, true, false, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B47F RID: 46207 RVA: 0x0041D470 File Offset: 0x0041B670
		public override bool HandleEvent(CellDepletedEvent E)
		{
			if (E.Cell.SlottedIn == this.ParentObject)
			{
				base.EmitMessage(E.Object.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " burns into bright slag.", ' ', false, false, false, null, null);
				E.Object.RemoveFromContext(E);
				E.Object.Destroy(null, false, false, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B480 RID: 46208 RVA: 0x0041D4F8 File Offset: 0x0041B6F8
		public override bool HandleEvent(CanBeSlottedEvent E)
		{
			if (E.Cell != null && E.Actor == this.ParentObject && this.AllowedBlueprint != null && this.AllowedBlueprint != "*")
			{
				if (!E.Cell.HasAnyCharge())
				{
					return false;
				}
				if (!this.AllowedBlueprint.CachedCommaExpansion().Contains(E.Item.Blueprint))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040048BF RID: 18623
		public string AllowedBlueprint = "*";
	}
}
