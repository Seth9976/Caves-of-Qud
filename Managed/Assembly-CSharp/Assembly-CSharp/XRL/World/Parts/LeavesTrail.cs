using System;

namespace XRL.World.Parts
{
	// Token: 0x02000ED2 RID: 3794
	[Serializable]
	public class LeavesTrail : IPart
	{
		// Token: 0x060097E8 RID: 38888 RVA: 0x00372558 File Offset: 0x00370758
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && this.OnEnter && this.Active) || (ID == LeftCellEvent.ID && !this.OnEnter && this.Active);
		}

		// Token: 0x060097E9 RID: 38889 RVA: 0x00372596 File Offset: 0x00370796
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.OnEnter && this.Active && !this.ParentObject.OnWorldMap())
			{
				this.LeaveTrailIn(E.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097EA RID: 38890 RVA: 0x003725C9 File Offset: 0x003707C9
		public override bool HandleEvent(LeftCellEvent E)
		{
			if (!this.OnEnter && this.Active && !this.ParentObject.OnWorldMap())
			{
				this.LeaveTrailIn(E.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097EB RID: 38891 RVA: 0x003725FC File Offset: 0x003707FC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x060097EC RID: 38892 RVA: 0x00372611 File Offset: 0x00370811
		public override bool FireEvent(Event E)
		{
			if (E.ID == "VillageInit" && this.VillageDeactivate)
			{
				this.Active = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x060097ED RID: 38893 RVA: 0x0037263C File Offset: 0x0037083C
		public bool LeaveTrailIn(Cell Cell)
		{
			if (Cell == null || Cell.OnWorldMap())
			{
				return false;
			}
			if (this.TrailBlueprint.IsNullOrEmpty())
			{
				return false;
			}
			if (!this.TrailChance.in100())
			{
				return false;
			}
			bool zeroRangeIncluded = this.Range.RollMinCached() <= 0;
			bool any = false;
			GameObjectFactory.ProcessSpecification(this.TrailBlueprint, delegate(GameObject obj)
			{
				int num = 0;
				Cell randomLocalAdjacentCell;
				do
				{
					randomLocalAdjacentCell = Cell.GetRandomLocalAdjacentCell(this.Range.RollCached(), zeroRangeIncluded);
				}
				while ((randomLocalAdjacentCell == null || !randomLocalAdjacentCell.IsEmptyForPopulation()) && ++num < 10);
				if (randomLocalAdjacentCell != null && randomLocalAdjacentCell.IsEmptyForPopulation())
				{
					if (this.TemporaryChance.in100())
					{
						Temporary temporary;
						if (!this.TemporaryTurnInto.IsNullOrEmpty() && this.TemporaryTurnIntoChance.in100())
						{
							temporary = new Temporary(this.TemporaryDuration.RollCached(), this.TemporaryTurnInto);
						}
						else
						{
							temporary = new Temporary(this.TemporaryDuration.RollCached());
						}
						obj.AddPart<Temporary>(temporary, true, false);
					}
					if (this.PassAttitudes)
					{
						obj.TakeBaseAllegiance(this.ParentObject);
					}
					randomLocalAdjacentCell.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					any = true;
					return;
				}
				obj.Obliterate(null, false, null);
			}, null, 1, 0, 0, null, "LeavesTrail", null, null, null, null);
			return any;
		}

		// Token: 0x04003F36 RID: 16182
		public string TrailBlueprint = "SmallSlimePuddle";

		// Token: 0x04003F37 RID: 16183
		public int TrailChance = 100;

		// Token: 0x04003F38 RID: 16184
		public int TemporaryChance;

		// Token: 0x04003F39 RID: 16185
		public string TemporaryDuration = "2d3";

		// Token: 0x04003F3A RID: 16186
		public string TemporaryTurnInto;

		// Token: 0x04003F3B RID: 16187
		public int TemporaryTurnIntoChance = 100;

		// Token: 0x04003F3C RID: 16188
		public bool PassAttitudes;

		// Token: 0x04003F3D RID: 16189
		public bool VillageDeactivate = true;

		// Token: 0x04003F3E RID: 16190
		public bool Active = true;

		// Token: 0x04003F3F RID: 16191
		public bool OnEnter = true;

		// Token: 0x04003F40 RID: 16192
		public string Range = "0";
	}
}
