using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E72 RID: 3698
	[Serializable]
	public class Fracti : IPart
	{
		// Token: 0x0600955C RID: 38236 RVA: 0x0036094C File Offset: 0x0035EB4C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<PhysicalContactEvent>.ID || ID == ObjectCreatedEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == GetNavigationWeightEvent.ID;
		}

		// Token: 0x0600955D RID: 38237 RVA: 0x0036097A File Offset: 0x0035EB7A
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (E.PhaseMatches(this.ParentObject) && this.CanAndWillDamage(E.Actor))
			{
				E.MinWeight(98);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600955E RID: 38238 RVA: 0x003609A8 File Offset: 0x0035EBA8
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.Render.Tile = "Terrain/sw_fracti" + Stat.Random(1, 8).ToString() + ".bmp";
			return base.HandleEvent(E);
		}

		// Token: 0x0600955F RID: 38239 RVA: 0x003609EA File Offset: 0x0035EBEA
		public bool CanAndWillDamage(GameObject obj)
		{
			return obj != null && obj.IsCombat && Factions.GetFeelingFactionToObject("Succulents", obj, null) < 50 && obj.PhaseAndFlightMatches(this.ParentObject) && obj != this.ParentObject;
		}

		// Token: 0x06009560 RID: 38240 RVA: 0x00360A24 File Offset: 0x0035EC24
		public void Damage(GameObject Object)
		{
			if (this.CanAndWillDamage(Object))
			{
				if (Object.TakeDamage(1, "from %t thorns.", "Thorns", null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null))
				{
					Object.Bloodsplatter();
				}
				if (Object.Energy != null)
				{
					Object.Energy.BaseValue -= 500;
				}
			}
		}

		// Token: 0x06009561 RID: 38241 RVA: 0x00360A89 File Offset: 0x0035EC89
		public override bool HandleEvent(PhysicalContactEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				this.Damage(E.Actor);
			}
			else if (E.Actor == this.ParentObject)
			{
				this.Damage(E.Object);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009562 RID: 38242 RVA: 0x00360AC8 File Offset: 0x0035ECC8
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.Damage(E.Object);
			return base.HandleEvent(E);
		}

		// Token: 0x06009563 RID: 38243 RVA: 0x00360AE0 File Offset: 0x0035ECE0
		public void Grow(string OkFloor, int Size, string Direction = null)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			if (Size == 0)
			{
				return;
			}
			if (Direction == null)
			{
				this.Grow(OkFloor, Size, "N");
				this.Grow(OkFloor, Size, "S");
				this.Grow(OkFloor, Size, "E");
				this.Grow(OkFloor, Size, "W");
				return;
			}
			if (15.in100())
			{
				if (40.in100())
				{
					Cell localCellFromDirection = currentCell.GetLocalCellFromDirection(Direction, true);
					if (localCellFromDirection != null && localCellFromDirection.Objects.Count == 1 && localCellFromDirection.HasObject(OkFloor))
					{
						localCellFromDirection.AddObject("Fracti", null, null, null, null).GetPart<Fracti>().Grow(OkFloor, Size - 1, Direction);
					}
				}
				if (Direction == "N")
				{
					Direction = "W";
				}
				else if (Direction == "W")
				{
					Direction = "S";
				}
				else if (Direction == "S")
				{
					Direction = "E";
				}
				else if (Direction == "E")
				{
					Direction = "N";
				}
			}
			Cell localCellFromDirection2 = currentCell.GetLocalCellFromDirection(Direction, true);
			if (localCellFromDirection2 != null && localCellFromDirection2.Objects.Count == 1 && localCellFromDirection2.HasObject(OkFloor))
			{
				localCellFromDirection2.AddObject("Fracti", null, null, null, null).GetPart<Fracti>().Grow(OkFloor, Size - 1, Direction);
			}
		}
	}
}
