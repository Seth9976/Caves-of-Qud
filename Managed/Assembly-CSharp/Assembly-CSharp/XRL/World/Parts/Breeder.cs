using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000E50 RID: 3664
	[Serializable]
	public class Breeder : IPart
	{
		// Token: 0x06009466 RID: 37990 RVA: 0x00356ABC File Offset: 0x00354CBC
		public override bool SameAs(IPart p)
		{
			Breeder breeder = p as Breeder;
			return breeder.Chance == this.Chance && breeder.ReductionChance == this.ReductionChance && !(breeder.Blueprint != this.Blueprint) && !(breeder.Range != this.Range) && breeder.TakeOnAttitudes == this.TakeOnAttitudes;
		}

		// Token: 0x06009467 RID: 37991 RVA: 0x00356B2B File Offset: 0x00354D2B
		public override bool WantTurnTick()
		{
			return this.Chance > 0;
		}

		// Token: 0x06009468 RID: 37992 RVA: 0x00356B36 File Offset: 0x00354D36
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckBreed(Amount);
		}

		// Token: 0x06009469 RID: 37993 RVA: 0x00356B40 File Offset: 0x00354D40
		public int CheckBreed(int Turns = 1)
		{
			int result = 0;
			if (this.Chance <= 0)
			{
				return result;
			}
			if (!GameObject.Validate(this.ParentObject))
			{
				return result;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null || currentCell.OnWorldMap())
			{
				return result;
			}
			string text = this.Blueprint ?? this.ParentObject.Blueprint;
			for (int i = 1; i <= Turns; i++)
			{
				if (this.Chance.in100())
				{
					bool any = false;
					Cell cell;
					if (this.Range == "1")
					{
						cell = currentCell.GetRandomLocalAdjacentCell((Cell c) => c.IsEmptyForPopulation());
					}
					else
					{
						cell = currentCell.GetRandomLocalAdjacentCell(this.Range.RollCached(), (Cell c) => c.IsEmptyForPopulation(), false);
					}
					if (cell != null)
					{
						GameObjectFactory.ProcessSpecification(text, delegate(GameObject obj)
						{
							this.Chance--;
							Breeder breeder;
							if (obj.TryGetPart<Breeder>(out breeder))
							{
								if (this.ReductionChance.in100())
								{
									breeder.Chance = this.Chance - 1;
								}
								else
								{
									breeder.Chance = this.Chance;
								}
							}
							if (this.TakeOnAttitudes)
							{
								obj.TakeAllegiance<AllyBirth>(this.ParentObject);
								obj.CopyTarget(this.ParentObject);
								obj.CopyLeader(this.ParentObject);
							}
							obj.MakeActive(false);
							cell.AddObject(obj, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							int result2 = result;
							result = result2 + 1;
							any = true;
						}, null, 1, 0, 0, null, "Breeder", null, null, null, null);
						if (any)
						{
							break;
						}
					}
					if (!any || this.Chance <= 0)
					{
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x04003D52 RID: 15698
		public int Chance;

		// Token: 0x04003D53 RID: 15699
		public int ReductionChance;

		// Token: 0x04003D54 RID: 15700
		public string Blueprint;

		// Token: 0x04003D55 RID: 15701
		public string Range = "1";

		// Token: 0x04003D56 RID: 15702
		public bool TakeOnAttitudes = true;
	}
}
