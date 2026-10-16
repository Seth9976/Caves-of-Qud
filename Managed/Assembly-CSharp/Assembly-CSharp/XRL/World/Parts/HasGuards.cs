using System;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000EC5 RID: 3781
	[Serializable]
	public class HasGuards : IBondedLeader
	{
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06009793 RID: 38803 RVA: 0x00370BF6 File Offset: 0x0036EDF6
		// (set) Token: 0x06009794 RID: 38804 RVA: 0x00370BFE File Offset: 0x0036EDFE
		[Obsolete("mod compat, will be removed after Q1 2024")]
		public string numberOfGuards
		{
			get
			{
				return this.NumberOfGuards;
			}
			set
			{
				this.NumberOfGuards = value;
			}
		}

		// Token: 0x06009795 RID: 38805 RVA: 0x00370C07 File Offset: 0x0036EE07
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.GuardsPlaced) || (ID == OnDeathRemovalEvent.ID && this.StopFightOnDeath);
		}

		// Token: 0x06009796 RID: 38806 RVA: 0x00370C35 File Offset: 0x0036EE35
		public override bool HandleEvent(OnDeathRemovalEvent E)
		{
			if (this.StopFightOnDeath)
			{
				this.ParentObject.StopFight(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009797 RID: 38807 RVA: 0x00370C54 File Offset: 0x0036EE54
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.GuardsPlaced)
			{
				this.GuardsPlaced = true;
				Cell currentCell = this.ParentObject.CurrentCell;
				string text = "DynamicInheritsTable:Creature:Tier" + Tier.Constrain(currentCell.ParentZone.NewTier + 1).ToString();
				int i = 0;
				int num = this.NumberOfGuards.RollCached();
				while (i < num)
				{
					GameObjectBlueprint gameObjectBlueprint = null;
					int num2 = 0;
					while (++num2 < 10)
					{
						GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(PopulationManager.RollOneFrom(text, null, null).Blueprint);
						if (HasGuards.IsSuitableGuard(blueprintIfExists))
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
							gameObject.AddPart<HiredGuard>(new HiredGuard(this.ParentObject, "Merchants", null, "hired guard", "MerchantGuard", false), true, false);
							firstEmptyAdjacentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).MakeActive(false);
						}
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009798 RID: 38808 RVA: 0x00370D6A File Offset: 0x0036EF6A
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009799 RID: 38809 RVA: 0x00370D70 File Offset: 0x0036EF70
		public static bool IsSuitableGuard(GameObjectBlueprint BP)
		{
			return BP != null && BP.HasPart("Body") && BP.HasPart("Brain") && BP.HasPart("Combat") && BP.GetPartParameter<bool>("Brain", "Mobile", true) && !BP.HasPart("AIWallWalker") && !BP.HasProperName() && !BP.HasTagOrProperty("NoGuard");
		}

		// Token: 0x04003F06 RID: 16134
		public string NumberOfGuards = "2-3";

		// Token: 0x04003F07 RID: 16135
		public bool GuardsPlaced;

		// Token: 0x04003F08 RID: 16136
		public bool StopFightOnDeath;
	}
}
