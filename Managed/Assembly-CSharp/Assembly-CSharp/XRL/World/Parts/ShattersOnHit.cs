using System;

namespace XRL.World.Parts
{
	// Token: 0x02001114 RID: 4372
	[Serializable]
	public class ShattersOnHit : IPart
	{
		// Token: 0x0600AD2A RID: 44330 RVA: 0x003F8AE9 File Offset: 0x003F6CE9
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponAfterAttack");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AD2B RID: 44331 RVA: 0x003F8B09 File Offset: 0x003F6D09
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterThrownEvent.ID;
		}

		// Token: 0x0600AD2C RID: 44332 RVA: 0x003F8B1F File Offset: 0x003F6D1F
		public override bool HandleEvent(AfterThrownEvent E)
		{
			this.Shatter(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD2D RID: 44333 RVA: 0x003F8B30 File Offset: 0x003F6D30
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponAfterAttack")
			{
				this.Shatter(E.GetGameObjectParameter("Defender"));
			}
			else if (E.ID == "ProjectileHit")
			{
				this.Shatter(null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AD2E RID: 44334 RVA: 0x003F8B84 File Offset: 0x003F6D84
		public void Shatter(GameObject Target = null)
		{
			base.DidX("shatter", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			if (Target != null)
			{
				Inventory inventory = Target.Inventory;
				if (inventory != null)
				{
					this.ParentObject.RemoveFromContext(null);
					inventory.AddObjectNoStack(this.ParentObject);
				}
				else if (Target.CurrentCell != null)
				{
					this.ParentObject.RemoveFromContext(null);
					this.ParentObject.Physics.CurrentCell = Target.CurrentCell;
				}
			}
			else if (this.ParentObject.CurrentCell != null)
			{
				GameObject firstObjectWithPart = this.ParentObject.CurrentCell.GetFirstObjectWithPart("Inventory", (GameObject o) => o != this.ParentObject);
				if (firstObjectWithPart != null)
				{
					this.ParentObject.RemoveFromContext(null);
					firstObjectWithPart.Inventory.AddObjectNoStack(this.ParentObject);
				}
			}
			this.ParentObject.Destroy(null, false, false, null);
		}
	}
}
