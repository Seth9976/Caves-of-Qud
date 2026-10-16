using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000F9E RID: 3998
	[Serializable]
	public class DeployWith : IPart
	{
		// Token: 0x06009EAC RID: 40620 RVA: 0x003978C4 File Offset: 0x00395AC4
		public override bool SameAs(IPart p)
		{
			DeployWith deployWith = p as DeployWith;
			return !(deployWith.Blueprint != this.Blueprint) && deployWith.SolidOkay == this.SolidOkay && deployWith.SeepingOkay == this.SeepingOkay && deployWith.SameCell == this.SameCell && !(deployWith.PreferredDirection != this.PreferredDirection) && deployWith.Chance == this.Chance && deployWith.CarryOverOwner == this.CarryOverOwner;
		}

		// Token: 0x06009EAD RID: 40621 RVA: 0x00397953 File Offset: 0x00395B53
		public override bool CanGenerateStacked()
		{
			return false;
		}

		// Token: 0x06009EAE RID: 40622 RVA: 0x00397956 File Offset: 0x00395B56
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x06009EAF RID: 40623 RVA: 0x00397974 File Offset: 0x00395B74
		public override bool HandleEvent(EnteredCellEvent E)
		{
			GameObject parentObject = this.ParentObject;
			bool flag;
			if (parentObject == null)
			{
				flag = false;
			}
			else
			{
				Zone currentZone = parentObject.CurrentZone;
				bool? flag2 = ((currentZone != null) ? new bool?(currentZone.Built) : null);
				bool flag3 = true;
				flag = (flag2.GetValueOrDefault() == flag3) & (flag2 != null);
			}
			if (flag)
			{
				this.PerformDeploy();
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009EB0 RID: 40624 RVA: 0x003979DC File Offset: 0x00395BDC
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.PerformDeploy();
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x06009EB1 RID: 40625 RVA: 0x003979F7 File Offset: 0x00395BF7
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009EB2 RID: 40626 RVA: 0x003979FC File Offset: 0x00395BFC
		public void PerformDeploy()
		{
			if (!this.Chance.in100())
			{
				return;
			}
			Cell ourCell = this.ParentObject.CurrentCell;
			if (ourCell == null)
			{
				return;
			}
			Cell targetCell = null;
			if (this.SameCell)
			{
				targetCell = ourCell;
			}
			else
			{
				int num = 0;
				ourCell.ForeachLocalAdjacentCell(delegate(Cell altCell)
				{
					if (this.SolidOkay || !altCell.IsSolid(this.SeepingOkay))
					{
						if (!this.PreferredDirection.IsNullOrEmpty() && ourCell.GetDirectionFromCell(altCell, false) == this.PreferredDirection)
						{
							targetCell = altCell;
							return false;
						}
						int num3 = num;
						num = num3 + 1;
					}
					return true;
				});
				if (targetCell == null && num > 0)
				{
					int selected = Stat.Random(1, num);
					int pos = 0;
					ourCell.ForeachLocalAdjacentCell(delegate(Cell altCell)
					{
						if (this.SolidOkay || !altCell.IsSolid(this.SeepingOkay))
						{
							int num2 = pos + 1;
							pos = num2;
							if (num2 == selected)
							{
								targetCell = altCell;
								return false;
							}
						}
						return true;
					});
				}
			}
			if (targetCell != null)
			{
				if (this.CarryOverOwner)
				{
					string owner = this.ParentObject.Owner;
					GameObjectFactory.ProcessSpecification(this.Blueprint, delegate(GameObject obj)
					{
						if (!owner.IsNullOrEmpty() && obj.Physics != null)
						{
							obj.Physics.Owner = owner;
						}
						targetCell.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}, null, 1, 0, 0, null, "DeployWith", null, null, null, null);
					return;
				}
				GameObjectFactory.ProcessSpecification(this.Blueprint, delegate(GameObject obj)
				{
					targetCell.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}, null, 1, 0, 0, null, null, null, null, null, null);
			}
		}

		// Token: 0x04004197 RID: 16791
		public string Blueprint;

		// Token: 0x04004198 RID: 16792
		public bool SolidOkay;

		// Token: 0x04004199 RID: 16793
		public bool SeepingOkay;

		// Token: 0x0400419A RID: 16794
		public bool SameCell;

		// Token: 0x0400419B RID: 16795
		public string PreferredDirection;

		// Token: 0x0400419C RID: 16796
		public int Chance = 100;

		// Token: 0x0400419D RID: 16797
		public bool CarryOverOwner = true;
	}
}
