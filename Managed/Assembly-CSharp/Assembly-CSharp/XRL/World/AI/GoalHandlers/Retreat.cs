using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI.Pathfinding;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B90 RID: 2960
	[Serializable]
	public class Retreat : IMovementGoal
	{
		// Token: 0x06007636 RID: 30262 RVA: 0x002C6CF5 File Offset: 0x002C4EF5
		public Retreat()
		{
		}

		// Token: 0x06007637 RID: 30263 RVA: 0x002C6D0C File Offset: 0x002C4F0C
		public Retreat(int Duration = -1, Cell Target = null, int MaxWeight = 95)
			: this()
		{
			this.Duration = Duration;
			this.Target = Target;
			this.MaxWeight = MaxWeight;
		}

		// Token: 0x06007638 RID: 30264 RVA: 0x002C6D29 File Offset: 0x002C4F29
		public override bool Finished()
		{
			return this.Duration == 0;
		}

		// Token: 0x06007639 RID: 30265 RVA: 0x002C6D34 File Offset: 0x002C4F34
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x0600763A RID: 30266 RVA: 0x002C6D37 File Offset: 0x002C4F37
		public bool TryRetreatAbilities()
		{
			return AICommandList.HandleCommandList(AIGetRetreatAbilityListEvent.GetFor(base.ParentObject, null, this.Target, null, -1, 0), base.ParentObject, null, this.Target, 0);
		}

		// Token: 0x0600763B RID: 30267 RVA: 0x002C6D64 File Offset: 0x002C4F64
		public GameObject FindWorstVisibleEnemy()
		{
			Cell currentCell = base.ParentObject.CurrentCell;
			GameObject gameObject = null;
			int num = int.MinValue;
			foreach (GameObject gameObject2 in currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, 30, "Brain", base.ParentObject, null, null))
			{
				if (gameObject2.IsHostileTowards(base.ParentObject))
				{
					int valueOrDefault = base.ParentObject.Con(gameObject2, false).GetValueOrDefault();
					if (valueOrDefault > num)
					{
						gameObject = gameObject2;
						num = valueOrDefault;
					}
				}
			}
			return gameObject;
		}

		// Token: 0x0600763C RID: 30268 RVA: 0x002C6E1C File Offset: 0x002C501C
		public List<GameObject> FindVisibleEnemies()
		{
			List<GameObject> list = Event.NewGameObjectList();
			Cell currentCell = base.ParentObject.CurrentCell;
			foreach (GameObject gameObject in currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, 30, "Brain", base.ParentObject, null, null))
			{
				if (gameObject.IsHostileTowards(base.ParentObject))
				{
					list.Add(gameObject);
				}
			}
			return list;
		}

		// Token: 0x0600763D RID: 30269 RVA: 0x002C6EB0 File Offset: 0x002C50B0
		public List<GameObject> FindVisibleEnemies(out GameObject WorstEnemy)
		{
			List<GameObject> list = Event.NewGameObjectList();
			Cell currentCell = base.ParentObject.CurrentCell;
			WorstEnemy = null;
			int num = int.MinValue;
			foreach (GameObject gameObject in currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, 30, "Brain", base.ParentObject, null, null))
			{
				if (gameObject.IsHostileTowards(base.ParentObject))
				{
					list.Add(gameObject);
					int valueOrDefault = base.ParentObject.Con(gameObject, false).GetValueOrDefault();
					if (valueOrDefault > num)
					{
						WorstEnemy = gameObject;
						num = valueOrDefault;
					}
				}
			}
			return list;
		}

		// Token: 0x0600763E RID: 30270 RVA: 0x002C6F78 File Offset: 0x002C5178
		public override void TakeAction()
		{
			if (this.Duration > 0)
			{
				this.Duration--;
			}
			else if (this.Duration == 0)
			{
				base.FailToParent();
				return;
			}
			base.ParentObject.FireEvent("AIRetreatStart");
			bool flag = false;
			Cell currentCell;
			GameObject gameObject;
			for (;;)
			{
				if (this.Target != null)
				{
					flag = true;
					if (this.Target.DistanceTo(base.ParentObject) == 0)
					{
						base.Think("I'm at my chosen retreat point.");
						if (base.ParentObject.AreHostilesAdjacent(true))
						{
							base.Think("There are hostiles here, so I'm going to choose a new retreat point.");
							this.Target = null;
						}
						else
						{
							bool flag2 = this.Target.IsHealingLocation(base.ParentObject);
							if (base.ParentObject.AreHostilesNearby() && !flag2)
							{
								base.Think("There are hostiles nearby and I'm not at a healing location, so I'm going to choose a new retreat point.");
								this.Target = null;
							}
							else
							{
								if (flag2)
								{
									break;
								}
								if (Stat.Random(1, 10) != 1)
								{
									goto IL_0134;
								}
								this.Target = null;
							}
						}
					}
					else
					{
						if (!base.ParentObject.IsMobile())
						{
							goto IL_02F8;
						}
						if (!base.ParentObject.FireEvent(Event.New("AIMovingToRetreat", "Target", this.Target)))
						{
							goto IL_02DC;
						}
						base.Think("I'm going to move towards my chosen retreat point.");
						currentCell = base.ParentObject.CurrentCell;
						Zone parentZone = currentCell.ParentZone;
						Zone parentZone2 = this.Target.ParentZone;
						if (parentZone2.IsWorldMap())
						{
							base.Think("Retreat point is on the world map, can't go there!");
							this.Target = null;
						}
						else
						{
							if (parentZone.ZoneID != null && parentZone2.ZoneID != null)
							{
								goto IL_01EF;
							}
							base.Think("Retreat point is invalid!");
							this.Target = null;
						}
					}
				}
				base.Think("I'm going to choose a retreat point.");
				Cell currentCell2 = base.ParentObject.CurrentCell;
				List<Cell> list = currentCell2.ParentZone.FastFloodNeighbors(currentCell2.X, currentCell2.Y, 30);
				List<GameObject> list2 = this.FindVisibleEnemies(out gameObject);
				int num = int.MaxValue;
				List<Cell> list3 = null;
				int num2 = 268435456;
				foreach (Cell cell in list)
				{
					int? num3 = this.CellScore(cell, gameObject, list2, ref num2);
					if (num3 != null)
					{
						if (list3 == null)
						{
							list3 = new List<Cell>(16);
						}
						int value = num3.Value;
						if (value < num)
						{
							num = value;
							list3.Clear();
							list3.Add(cell);
						}
						else if (value == num)
						{
							list3.Add(cell);
						}
					}
				}
				if (list3 == null)
				{
					goto IL_0417;
				}
				this.Target = list3.GetRandomElement<Cell>(null);
				if (flag)
				{
					goto Block_22;
				}
			}
			base.Think("I'm going to try to use my healing location.");
			int num4 = base.ParentObject.Stat("Energy", 0);
			this.Target.UseHealingLocation(base.ParentObject);
			if (base.ParentObject.Stat("Energy", 0) >= num4)
			{
				base.ParentObject.UseEnergy(1000);
			}
			return;
			IL_0134:
			base.ParentObject.UseEnergy(1000);
			return;
			IL_01EF:
			base.Think("I'm going to try to use abilities suitable for retreating.");
			if (this.TryRetreatAbilities())
			{
				return;
			}
			base.Think("I'm going to try to pathfind.");
			FindPath findPath = new FindPath(currentCell, this.Target, base.ParentObject.IsPlayer(), true, base.ParentObject, this.MaxWeight, false, false, false, false, false, null);
			if (findPath.Usable && findPath.Directions.Count > 0)
			{
				this.PushChildGoal(new Step(findPath.Directions[0], false, false, false, false, null, false));
				return;
			}
			base.Think("I can't find a path.");
			if (!base.ParentObject.FireEvent(Event.New("AIFailRetreatPathfind", "Target", this.Target)))
			{
				base.FailToParent();
				return;
			}
			this.Target = null;
			GameObject gameObject2 = this.FindWorstVisibleEnemy();
			if (gameObject2 != null)
			{
				this.PushChildGoal(new Flee(gameObject2, 2, false, true));
				return;
			}
			base.ParentObject.UseEnergy(1000);
			return;
			IL_02DC:
			base.ParentObject.UseEnergy(1000);
			base.Think("I'm not allowed to move.");
			return;
			IL_02F8:
			base.ParentObject.UseEnergy(1000);
			base.Think("My target is too far and I'm immobile.");
			base.FailToParent();
			return;
			Block_22:
			base.ParentObject.UseEnergy(1000);
			return;
			IL_0417:
			if (gameObject != null)
			{
				base.Think("I couldn't choose a retreat point so I'm going to flee from my worst enemy.");
				this.PushChildGoal(new Flee(gameObject, 2, false, true));
				return;
			}
			base.Think("I couldn't choose a retreat point or going to flee from my worst enemy so I'm going to wait.");
			base.ParentObject.UseEnergy(1000);
		}

		// Token: 0x0600763F RID: 30271 RVA: 0x002C73E8 File Offset: 0x002C55E8
		private int? CellScore(Cell C, GameObject WorstEnemy, List<GameObject> Enemies, ref int Nav)
		{
			if (!C.IsEmpty())
			{
				return null;
			}
			if (C.HasObjectWithPart("Combat"))
			{
				return null;
			}
			int num = C.NavigationWeight(base.ParentObject, ref Nav);
			if (num >= 98)
			{
				return null;
			}
			if (!base.ParentObject.HasLOSTo(C, true, false, false, true, null))
			{
				return null;
			}
			int num2 = num / 5;
			int healingLocationValue = C.GetHealingLocationValue(base.ParentObject);
			if (healingLocationValue > 0)
			{
				num2 -= healingLocationValue;
			}
			if (WorstEnemy != null)
			{
				num2 -= C.DistanceTo(WorstEnemy);
			}
			if (Enemies != null)
			{
				GameObject gameObject = null;
				int num3 = int.MaxValue;
				foreach (GameObject gameObject2 in Enemies)
				{
					if (gameObject2 != WorstEnemy)
					{
						int num4 = C.DistanceTo(gameObject2);
						if (num4 < num3)
						{
							gameObject = gameObject2;
							num3 = num4;
						}
					}
				}
				if (gameObject != null)
				{
					num2 -= num3;
				}
			}
			return new int?(num2);
		}

		// Token: 0x0400362D RID: 13869
		public const int DEFAULT_MAX_WEIGHT = 95;

		// Token: 0x0400362E RID: 13870
		public int Duration = -1;

		// Token: 0x0400362F RID: 13871
		public Cell Target;

		// Token: 0x04003630 RID: 13872
		public int MaxWeight = 95;
	}
}
