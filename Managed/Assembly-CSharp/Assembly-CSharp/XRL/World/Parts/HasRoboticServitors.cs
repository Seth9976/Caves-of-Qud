using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000EC6 RID: 3782
	[Serializable]
	public class HasRoboticServitors : IBondedLeader
	{
		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x0600979B RID: 38811 RVA: 0x00370E00 File Offset: 0x0036F000
		// (set) Token: 0x0600979C RID: 38812 RVA: 0x00370E08 File Offset: 0x0036F008
		[Obsolete("mod compat, will be removed after Q1 2024")]
		public string numberOfRoboticServitors
		{
			get
			{
				return this.NumberOfRoboticServitors;
			}
			set
			{
				this.NumberOfRoboticServitors = value;
			}
		}

		// Token: 0x0600979D RID: 38813 RVA: 0x00370E11 File Offset: 0x0036F011
		public HasRoboticServitors()
		{
		}

		// Token: 0x0600979E RID: 38814 RVA: 0x00370E2B File Offset: 0x0036F02B
		public HasRoboticServitors(string NumberOfRoboticServitors, bool StripGear)
			: this()
		{
			this.NumberOfRoboticServitors = NumberOfRoboticServitors;
			this.StripGear = StripGear;
		}

		// Token: 0x0600979F RID: 38815 RVA: 0x00370E41 File Offset: 0x0036F041
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.ServitorsPlaced) || (ID == OnDeathRemovalEvent.ID && this.StopFightOnDeath);
		}

		// Token: 0x060097A0 RID: 38816 RVA: 0x00370E6F File Offset: 0x0036F06F
		public override bool HandleEvent(OnDeathRemovalEvent E)
		{
			if (this.StopFightOnDeath)
			{
				this.ParentObject.StopFight(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097A1 RID: 38817 RVA: 0x00370E8C File Offset: 0x0036F08C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.ServitorsPlaced)
			{
				this.ServitorsPlaced = true;
				Cell currentCell = this.ParentObject.CurrentCell;
				int tier = this.ParentObject.GetTier();
				List<string> list = new List<string>();
				list.Add("DynamicInheritsTable:Creature:Tier" + tier.ToString());
				list.Add("DynamicInheritsTable:Creature:Tier" + tier.ToString());
				list.Add("DynamicInheritsTable:Creature:Tier" + tier.ToString());
				if (tier >= 8)
				{
					list.Add("DynamicInheritsTable:Creature:Tier" + (tier - 1).ToString());
					list.Add("DynamicInheritsTable:Creature:Tier" + (tier - 2).ToString());
				}
				else if (tier <= 1)
				{
					list.Add("DynamicInheritsTable:Creature:Tier" + (tier + 1).ToString());
					list.Add("DynamicInheritsTable:Creature:Tier" + (tier + 2).ToString());
				}
				else
				{
					list.Add("DynamicInheritsTable:Creature:Tier" + (tier + 1).ToString());
					list.Add("DynamicInheritsTable:Creature:Tier" + (tier - 1).ToString());
				}
				int i = 0;
				int num = this.NumberOfRoboticServitors.RollCached();
				while (i < num)
				{
					GameObjectBlueprint gameObjectBlueprint = null;
					int num2 = 0;
					while (++num2 < 50)
					{
						GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(PopulationManager.RollOneFrom(list.GetRandomElement<string>(null), null, null).Blueprint);
						if (HasRoboticServitors.IsSuitableServitor(blueprintIfExists))
						{
							gameObjectBlueprint = blueprintIfExists;
							break;
						}
					}
					if (gameObjectBlueprint != null)
					{
						Cell firstEmptyAdjacentCell = currentCell.GetFirstEmptyAdjacentCell(1, 2);
						if (firstEmptyAdjacentCell != null)
						{
							GameObject gameObject = GameObject.Create(gameObjectBlueprint.Name);
							gameObject.AddPart<RoboticServitor>(new RoboticServitor(this.ParentObject, null, null, null, null, this.StripGear), true, false);
							firstEmptyAdjacentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).MakeActive(false);
						}
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097A2 RID: 38818 RVA: 0x00371085 File Offset: 0x0036F285
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060097A3 RID: 38819 RVA: 0x00371088 File Offset: 0x0036F288
		public static bool IsSuitableServitor(GameObjectBlueprint BP)
		{
			return BP != null && BP.HasPart("Body") && BP.HasPart("Robot") && BP.HasPart("Brain") && BP.HasPart("Combat") && BP.HasStat("Level") && BP.GetPartParameter<bool>("Brain", "Mobile", true) && !BP.GetPartParameter<bool>("Brain", "Aquatic", false) && !BP.HasPart("AIWallWalker") && !BP.HasProperName();
		}

		// Token: 0x04003F09 RID: 16137
		public string NumberOfRoboticServitors = "2-4";

		// Token: 0x04003F0A RID: 16138
		public bool StripGear;

		// Token: 0x04003F0B RID: 16139
		public bool ServitorsPlaced;

		// Token: 0x04003F0C RID: 16140
		public bool StopFightOnDeath = true;
	}
}
