using System;
using System.Collections.Generic;
using XRL.Language;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, a chance to take effect that is below 100
	/// will be increased by ((power load - 100) / 30), i.e. 10 for the
	/// standard overload power load of 400.
	/// </remarks>
	// Token: 0x02001079 RID: 4217
	[Serializable]
	public class PartsGas : IPoweredPart
	{
		// Token: 0x0600A7CF RID: 42959 RVA: 0x003D51A9 File Offset: 0x003D33A9
		public PartsGas()
		{
			this.NameForStatus = "GasDispersal";
		}

		// Token: 0x0600A7D0 RID: 42960 RVA: 0x003D51D4 File Offset: 0x003D33D4
		public override bool SameAs(IPart p)
		{
			PartsGas partsGas = p as PartsGas;
			return partsGas.Radius == this.Radius && partsGas.Chance == this.Chance && partsGas.UsesChargePerTurn == this.UsesChargePerTurn && partsGas.UsesChargePerEachEffect == this.UsesChargePerEachEffect && partsGas.UsesChargePerAnyEffect == this.UsesChargePerAnyEffect && partsGas.UsesCircularRadius == this.UsesCircularRadius && partsGas.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(p);
		}

		// Token: 0x0600A7D1 RID: 42961 RVA: 0x003D525F File Offset: 0x003D345F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription);
		}

		// Token: 0x0600A7D2 RID: 42962 RVA: 0x003D5280 File Offset: 0x003D3480
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				E.Postfix.Append("\n{{rules|");
				int effectiveChance = this.GetEffectiveChance();
				if (effectiveChance >= 100)
				{
					E.Postfix.Append("Repels");
				}
				else
				{
					E.Postfix.Append(effectiveChance).Append("% chance per turn to repel");
				}
				E.Postfix.Append(" gases ");
				if (this.Radius == 1)
				{
					E.Postfix.Append("near ");
				}
				else if (this.UsesCircularRadius)
				{
					E.Postfix.Append("within ").Append(Grammar.A(this.Radius, false)).Append("-square radius of ");
				}
				else
				{
					E.Postfix.Append("within ").Append(this.Radius).Append(" squares of ");
				}
				E.Postfix.Append(base.GetOperationalScopeDescription()).Append('.');
				base.AddStatusSummary(E.Postfix);
				E.Postfix.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7D3 RID: 42963 RVA: 0x003D53A5 File Offset: 0x003D35A5
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A7D4 RID: 42964 RVA: 0x003D53A8 File Offset: 0x003D35A8
		public override void TurnTick(long TimeTick, int Amount)
		{
			int num = this.MyPowerLoadLevel();
			bool usesChargePerTurn = this.UsesChargePerTurn;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			int num2 = 1;
			int? num3 = new int?(num);
			if (base.IsReady(usesChargePerTurn, flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, num2, null, false, 0L, num3))
			{
				this.PerformGasRepulsion(null, num);
			}
			base.TurnTick(TimeTick, Amount);
		}

		// Token: 0x0600A7D5 RID: 42965 RVA: 0x003D53FC File Offset: 0x003D35FC
		public void PerformGasRepulsion(Cell FromCell = null, int PowerLoad = -2147483648)
		{
			if (FromCell == null)
			{
				FromCell = this.ParentObject.GetCurrentCell();
			}
			if (FromCell == null || FromCell.OnWorldMap())
			{
				return;
			}
			List<Cell> localAdjacentCells = FromCell.GetLocalAdjacentCells(this.Radius, false);
			localAdjacentCells.Remove(FromCell);
			localAdjacentCells.Insert(0, FromCell);
			List<GameObject> list = Event.NewGameObjectList();
			bool flag = !this.UsesChargePerAnyEffect;
			if (PowerLoad == -2147483648)
			{
				PowerLoad = this.MyPowerLoadLevel();
			}
			int effectiveChance = this.GetEffectiveChance(PowerLoad);
			int i = 0;
			int count = localAdjacentCells.Count;
			while (i < count)
			{
				Cell cell = localAdjacentCells[i];
				list.Clear();
				int j = 0;
				int count2 = cell.Objects.Count;
				while (j < count2)
				{
					if (cell.Objects[j].HasPart<Gas>() && effectiveChance.in100())
					{
						if (!flag)
						{
							bool flag2 = true;
							bool flag3 = false;
							bool flag4 = false;
							bool flag5 = false;
							bool flag6 = false;
							bool flag7 = false;
							bool flag8 = false;
							bool flag9 = false;
							bool flag10 = false;
							bool flag11 = false;
							int num = 1;
							int? num2 = new int?(PowerLoad);
							if (!base.IsReady(flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, flag11, num, null, false, 0L, num2))
							{
								return;
							}
							flag = true;
						}
						list.Add(cell.Objects[j]);
					}
					j++;
				}
				if (list.Count > 0)
				{
					string directionFromCell = FromCell.GetDirectionFromCell(cell, false);
					Cell cell2 = cell.GetCellFromDirection(directionFromCell, true);
					if (cell2 == null || localAdjacentCells.Contains(cell2))
					{
						cell2 = cell.getClosestPassableCellExcept(localAdjacentCells);
					}
					if (cell2 != null && !localAdjacentCells.Contains(cell2))
					{
						int k = 0;
						int count3 = list.Count;
						while (k < count3)
						{
							if (!this.UsesChargePerEachEffect)
							{
								goto IL_018A;
							}
							bool flag12 = true;
							bool flag13 = false;
							bool flag14 = false;
							bool flag15 = false;
							bool flag16 = false;
							bool flag17 = false;
							bool flag18 = false;
							bool flag19 = false;
							bool flag20 = false;
							bool flag21 = false;
							int num3 = 1;
							int? num2 = new int?(PowerLoad);
							if (base.IsReady(flag12, flag13, flag14, flag15, flag16, flag17, flag18, flag19, flag20, flag21, num3, null, false, 0L, num2))
							{
								goto IL_018A;
							}
							IL_019F:
							k++;
							continue;
							IL_018A:
							list[k].DirectMoveTo(cell2, 0, true, false, false, null);
							goto IL_019F;
						}
					}
				}
				i++;
			}
		}

		// Token: 0x0600A7D6 RID: 42966 RVA: 0x003D55C4 File Offset: 0x003D37C4
		public int GetEffectiveChance()
		{
			int num = this.Chance;
			if (num < 100)
			{
				num += this.MyPowerLoadBonus(int.MinValue, 100, 30);
			}
			return num;
		}

		// Token: 0x0600A7D7 RID: 42967 RVA: 0x003D55F0 File Offset: 0x003D37F0
		public int GetEffectiveChance(int PowerLoad)
		{
			int num = this.Chance;
			if (num < 100)
			{
				num += this.MyPowerLoadBonus(PowerLoad, 100, 30);
			}
			return num;
		}

		// Token: 0x0400453A RID: 17722
		public int Radius = 1;

		// Token: 0x0400453B RID: 17723
		public int Chance = 100;

		// Token: 0x0400453C RID: 17724
		public bool UsesChargePerTurn;

		// Token: 0x0400453D RID: 17725
		public bool UsesChargePerEachEffect;

		// Token: 0x0400453E RID: 17726
		public bool UsesChargePerAnyEffect;

		// Token: 0x0400453F RID: 17727
		public bool UsesCircularRadius;

		// Token: 0x04004540 RID: 17728
		public bool ShowInShortDescription = true;
	}
}
