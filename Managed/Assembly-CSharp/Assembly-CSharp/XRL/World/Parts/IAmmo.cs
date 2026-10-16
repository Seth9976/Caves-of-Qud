using System;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02001111 RID: 4369
	[Serializable]
	public abstract class IAmmo : IPart, IContextRelationManager
	{
		// Token: 0x0600AD18 RID: 44312 RVA: 0x003F87F1 File Offset: 0x003F69F1
		public override bool SameAs(IPart p)
		{
			return (p as IAmmo).LoadedIn == this.LoadedIn && base.SameAs(p);
		}

		// Token: 0x0600AD19 RID: 44313 RVA: 0x003F8810 File Offset: 0x003F6A10
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == DropOnDeathEvent.ID || ID == PooledEvent<GetContextEvent>.ID || ID == RemoveFromContextEvent.ID || ID == PooledEvent<ReplaceInContextEvent>.ID || ID == TryRemoveFromContextEvent.ID || (Options.AutogetAmmo && ID == AutoexploreObjectEvent.ID);
		}

		// Token: 0x0600AD1A RID: 44314 RVA: 0x003F8864 File Offset: 0x003F6A64
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (E.Command == null && Options.AutogetAmmo && ((this.LoadedIn == null) ? this.ParentObject.CanAutoget(true) : (this.LoadedIn.Understood() && this.LoadedIn.CanAutoget(true))))
			{
				E.Command = ((this.LoadedIn != null) ? "UnloadMagazineAmmo" : "Autoget");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD1B RID: 44315 RVA: 0x003F88D8 File Offset: 0x003F6AD8
		public override bool HandleEvent(DropOnDeathEvent E)
		{
			if (this.ScatterOnDeathThreshold > 0 && !this.ScatterOnDeathPercentage.IsNullOrEmpty())
			{
				int count = this.ParentObject.Count;
				if (count >= this.ScatterOnDeathThreshold)
				{
					this.ParentObject.Count = Math.Max(count * this.ScatterOnDeathPercentage.RollCached() / 100, this.ScatterOnDeathThreshold);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD1C RID: 44316 RVA: 0x003F893D File Offset: 0x003F6B3D
		public override bool HandleEvent(GetContextEvent E)
		{
			if (GameObject.Validate(ref this.LoadedIn))
			{
				E.ObjectContext = this.LoadedIn;
				E.Relation = 5;
				E.RelationManager = this;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD1D RID: 44317 RVA: 0x003F896F File Offset: 0x003F6B6F
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			this.ReplaceAmmo(E.Replacement);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD1E RID: 44318 RVA: 0x003F8984 File Offset: 0x003F6B84
		public override bool HandleEvent(RemoveFromContextEvent E)
		{
			this.ReplaceAmmo(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD1F RID: 44319 RVA: 0x003F8994 File Offset: 0x003F6B94
		public override bool HandleEvent(TryRemoveFromContextEvent E)
		{
			this.ReplaceAmmo(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD20 RID: 44320 RVA: 0x003F89A4 File Offset: 0x003F6BA4
		private void ReplaceAmmo(GameObject Replacement)
		{
			if (GameObject.Validate(ref this.LoadedIn))
			{
				MagazineAmmoLoader part = this.LoadedIn.GetPart<MagazineAmmoLoader>();
				if (part != null && part.Ammo == this.ParentObject)
				{
					part.SetAmmo(Replacement);
				}
			}
		}

		// Token: 0x0600AD21 RID: 44321 RVA: 0x003F89E2 File Offset: 0x003F6BE2
		public GameObject GetLoadedIn()
		{
			GameObject.Validate(ref this.LoadedIn);
			return this.LoadedIn;
		}

		// Token: 0x0600AD22 RID: 44322 RVA: 0x003F89F8 File Offset: 0x003F6BF8
		public bool RestoreContextRelation(GameObject Object, GameObject ObjectContext, Cell CellContext, BodyPart BodyPartContext, int Relation, bool Silent = true)
		{
			if (Relation == 5 && ObjectContext != null)
			{
				MagazineAmmoLoader part = ObjectContext.GetPart<MagazineAmmoLoader>();
				if (part != null)
				{
					if (part.Ammo != Object || this.LoadedIn != ObjectContext)
					{
						part.SetAmmo(Object);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04004776 RID: 18294
		public GameObject LoadedIn;

		// Token: 0x04004777 RID: 18295
		public int ScatterOnDeathThreshold = 5;

		// Token: 0x04004778 RID: 18296
		public string ScatterOnDeathPercentage = "8-10";
	}
}
