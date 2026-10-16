using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B81 RID: 2945
	[Serializable]
	public class ExtinguishSelf : GoalHandler
	{
		// Token: 0x060075D5 RID: 30165 RVA: 0x002C3259 File Offset: 0x002C1459
		public override bool CanFight()
		{
			return false;
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x002C325C File Offset: 0x002C145C
		public override bool Finished()
		{
			return !base.ParentObject.IsAflame();
		}

		// Token: 0x060075D7 RID: 30167 RVA: 0x002C326C File Offset: 0x002C146C
		public GameObject FindPoolInRadius(int Radius)
		{
			Cell currentCell = base.ParentObject.CurrentCell;
			List<Cell> list = currentCell.ParentZone.FastFloodNeighbors(currentCell.X, currentCell.Y, Radius);
			GameObject gameObject = null;
			float num = 0f;
			int num2 = int.MaxValue;
			foreach (Cell cell in list)
			{
				if (!cell.IsSolidFor(base.ParentObject, false) && !cell.HasObjectWithPart("Combat") && cell.IsReachable())
				{
					foreach (GameObject gameObject2 in cell.Objects)
					{
						LiquidVolume liquidVolume = gameObject2.LiquidVolume;
						if (liquidVolume != null && liquidVolume.IsWadingDepth())
						{
							float liquidCooling = liquidVolume.GetLiquidCooling();
							if (liquidCooling > 1f)
							{
								int num3 = currentCell.PathDistanceTo(cell);
								if (liquidCooling > num || (liquidCooling == num && num3 < num2))
								{
									gameObject = gameObject2;
									num = liquidCooling;
									num2 = num3;
								}
							}
						}
					}
				}
			}
			return gameObject;
		}

		// Token: 0x060075D8 RID: 30168 RVA: 0x002C33A4 File Offset: 0x002C15A4
		public GameObject FindDousingContainer()
		{
			GameObject BestContainer = null;
			float BestContainerCooling = 0f;
			int BestContainerVolume = 0;
			base.ParentObject.ForeachInventoryAndEquipment(delegate(GameObject obj)
			{
				LiquidVolume liquidVolume = obj.LiquidVolume;
				if (liquidVolume == null)
				{
					return;
				}
				if (liquidVolume.Volume <= 0)
				{
					return;
				}
				if (liquidVolume.EffectivelySealed())
				{
					return;
				}
				float liquidCooling = liquidVolume.GetLiquidCooling();
				if (liquidCooling < 1f)
				{
					return;
				}
				if (liquidCooling > BestContainerCooling || (liquidCooling == BestContainerCooling && liquidVolume.Volume > BestContainerVolume))
				{
					BestContainer = obj;
					BestContainerCooling = liquidCooling;
					BestContainerVolume = liquidVolume.Volume;
				}
			});
			return BestContainer;
		}

		// Token: 0x060075D9 RID: 30169 RVA: 0x002C33F0 File Offset: 0x002C15F0
		public override void TakeAction()
		{
			if (!GameObject.Validate(ref this.Target))
			{
				this.Target = null;
			}
			int num = base.ParentObject.Stat("Intelligence", 0);
			if (num > 3 && Stat.Random(3, 21) < num)
			{
				int num2 = this.PoolTries + 1;
				this.PoolTries = num2;
				if (num2 < 3)
				{
					this.Target = this.FindPoolInRadius(3);
					if (this.Target != null)
					{
						Cell currentCell = this.Target.CurrentCell;
						if (base.ParentObject.IsFlying)
						{
							this.PushChildGoal(new Land(true));
						}
						base.PushChildGoal(new MoveTo(currentCell.ParentZone.ZoneID, currentCell.X, currentCell.Y, false, true, 0, false, false, false, -1, -1, 95), this.ParentHandler);
						return;
					}
				}
			}
			if (num >= 7 && Stat.Random(6, 25) < num)
			{
				GameObject gameObject = this.FindDousingContainer();
				if (gameObject != null)
				{
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					float liquidCooling = liquidVolume.GetLiquidCooling();
					int num3 = (int)Math.Ceiling((double)((float)(base.ParentObject.Physics.Temperature / base.ParentObject.Physics.FlameTemperature) / liquidCooling));
					if (liquidVolume.Pour(base.ParentObject, null, false, true, num3, false))
					{
						return;
					}
				}
			}
			if (Firefighting.NeedsToLandToAttemptFirefighting(base.ParentObject, base.ParentObject))
			{
				this.PushChildGoal(new Land(true));
				return;
			}
			if (Firefighting.AttemptFirefighting(base.ParentObject, base.ParentObject, 1000, false, false))
			{
				return;
			}
			if (num > 3 && Stat.Random(3, 21) < num)
			{
				this.Target = this.FindPoolInRadius(40);
				if (this.Target != null)
				{
					Cell currentCell2 = this.Target.CurrentCell;
					if (base.ParentObject.IsFlying)
					{
						this.PushChildGoal(new Land(true));
					}
					base.PushChildGoal(new MoveTo(currentCell2.ParentZone.ZoneID, currentCell2.X, currentCell2.Y, false, true, 0, false, false, false, -1, -1, 95), this.ParentHandler);
					return;
				}
			}
		}

		// Token: 0x040035F4 RID: 13812
		public GameObject Target;

		// Token: 0x040035F5 RID: 13813
		public int PoolTries;

		// Token: 0x040035F6 RID: 13814
		[NonSerialized]
		public static List<AICommandList> CommandList = new List<AICommandList>();
	}
}
