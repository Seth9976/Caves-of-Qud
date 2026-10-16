using System;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x020011D7 RID: 4567
	[Serializable]
	public class MergeConduit : IPart
	{
		// Token: 0x0600B5F8 RID: 46584 RVA: 0x004264A0 File Offset: 0x004246A0
		public override bool SameAs(IPart p)
		{
			MergeConduit mergeConduit = p as MergeConduit;
			return !(mergeConduit.ModPart != this.ModPart) && !(mergeConduit.DestroyIfPart != this.DestroyIfPart) && !(mergeConduit.SkipIfPart != this.SkipIfPart) && mergeConduit.AllowExistenceSupport == this.AllowExistenceSupport && base.SameAs(p);
		}

		// Token: 0x0600B5F9 RID: 46585 RVA: 0x0042650A File Offset: 0x0042470A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterZoneBuiltEvent.ID || ID == PooledEvent<CheckSpawnMergeEvent>.ID;
		}

		// Token: 0x0600B5FA RID: 46586 RVA: 0x00426528 File Offset: 0x00424728
		public override bool HandleEvent(CheckSpawnMergeEvent E)
		{
			if (!this.DestroyIfPart.IsNullOrEmpty() && E.Other.HasPart(this.DestroyIfPart))
			{
				E.Object.Obliterate(null, false, null);
				return false;
			}
			if (!this.ModPart.IsNullOrEmpty() && this.ValidInstall(E.Other) && ItemModding.ApplyModification(E.Other, this.ModPart, true, null, false))
			{
				E.Object.Obliterate(null, false, null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5FB RID: 46587 RVA: 0x004265AF File Offset: 0x004247AF
		public override bool HandleEvent(AfterZoneBuiltEvent E)
		{
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B5FC RID: 46588 RVA: 0x004265C4 File Offset: 0x004247C4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B5FD RID: 46589 RVA: 0x004265C8 File Offset: 0x004247C8
		private bool ValidInstall(GameObject obj)
		{
			return !obj.IsTakeable() && (obj.IsWall() || obj.IsDoor()) && obj.IsReal && (this.AllowExistenceSupport || !obj.HasPart<ExistenceSupport>()) && !obj.IsCombatObject() && (this.ModPart.IsNullOrEmpty() || !obj.HasPart(this.ModPart)) && (this.SkipIfPart.IsNullOrEmpty() || !obj.HasPart(this.SkipIfPart));
		}

		// Token: 0x0400495D RID: 18781
		public string ModPart;

		// Token: 0x0400495E RID: 18782
		public string DestroyIfPart;

		// Token: 0x0400495F RID: 18783
		public string SkipIfPart;

		// Token: 0x04004960 RID: 18784
		public bool AllowExistenceSupport;
	}
}
