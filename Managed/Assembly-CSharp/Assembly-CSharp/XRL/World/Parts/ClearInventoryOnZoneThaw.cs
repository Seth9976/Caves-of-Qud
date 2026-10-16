using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F9B RID: 3995
	[Serializable]
	public class ClearInventoryOnZoneThaw : IActivePart
	{
		// Token: 0x06009E9A RID: 40602 RVA: 0x0039755B File Offset: 0x0039575B
		public ClearInventoryOnZoneThaw()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x06009E9B RID: 40603 RVA: 0x00397574 File Offset: 0x00395774
		public override bool SameAs(IPart Part)
		{
			ClearInventoryOnZoneThaw clearInventoryOnZoneThaw = Part as ClearInventoryOnZoneThaw;
			return !(clearInventoryOnZoneThaw.Blueprint != this.Blueprint) && clearInventoryOnZoneThaw.RequireOwnerIfOwned == this.RequireOwnerIfOwned && base.SameAs(Part);
		}

		// Token: 0x06009E9C RID: 40604 RVA: 0x003975B4 File Offset: 0x003957B4
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return !this.HasNearbyOwnerIfNeeded() || base.GetActivePartLocallyDefinedFailure();
		}

		// Token: 0x06009E9D RID: 40605 RVA: 0x003975C6 File Offset: 0x003957C6
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			if (!this.HasNearbyOwnerIfNeeded())
			{
				return "NoOwner";
			}
			return base.GetActivePartLocallyDefinedFailureDescription();
		}

		// Token: 0x06009E9E RID: 40606 RVA: 0x003975DC File Offset: 0x003957DC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneThawedEvent.ID;
		}

		// Token: 0x06009E9F RID: 40607 RVA: 0x003975F4 File Offset: 0x003957F4
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			if (base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ActivePartHasMultipleSubjects())
				{
					this.ForeachActivePartSubject(new Action<GameObject>(this.ClearInventory), true);
				}
				else
				{
					this.ClearInventory(this.GetActivePartFirstSubject());
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009EA0 RID: 40608 RVA: 0x00397659 File Offset: 0x00395859
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009EA1 RID: 40609 RVA: 0x0039765C File Offset: 0x0039585C
		public void ClearInventory(GameObject Object)
		{
			foreach (GameObject gameObject in Object.GetWholeInventoryReadonly())
			{
				if (this.Blueprint == null || gameObject.Blueprint == this.Blueprint)
				{
					gameObject.Obliterate(null, false, null);
				}
			}
		}

		// Token: 0x06009EA2 RID: 40610 RVA: 0x003976D0 File Offset: 0x003958D0
		public bool HasNearbyOwnerIfNeeded()
		{
			if (!this.RequireOwnerIfOwned)
			{
				return true;
			}
			string owners = this.ParentObject.Owner;
			if (owners.IsNullOrEmpty())
			{
				return true;
			}
			Zone currentZone = this.ParentObject.CurrentZone;
			return currentZone != null && currentZone.HasObject((GameObject o) => o.IsFactionMember(owners));
		}

		// Token: 0x04004195 RID: 16789
		public string Blueprint;

		// Token: 0x04004196 RID: 16790
		public bool RequireOwnerIfOwned = true;
	}
}
