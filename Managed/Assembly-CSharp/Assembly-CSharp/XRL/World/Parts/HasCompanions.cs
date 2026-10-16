using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EC4 RID: 3780
	[Serializable]
	public class HasCompanions : IBondedLeader
	{
		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600978A RID: 38794 RVA: 0x003709D6 File Offset: 0x0036EBD6
		// (set) Token: 0x0600978B RID: 38795 RVA: 0x003709DE File Offset: 0x0036EBDE
		[Obsolete("mod compat, will be removed after Q1 2024")]
		public string numberOfCompanions
		{
			get
			{
				return this.NumberOfCompanions;
			}
			set
			{
				this.NumberOfCompanions = value;
			}
		}

		// Token: 0x0600978C RID: 38796 RVA: 0x003709E7 File Offset: 0x0036EBE7
		public HasCompanions()
		{
		}

		// Token: 0x0600978D RID: 38797 RVA: 0x003709FA File Offset: 0x0036EBFA
		public HasCompanions(string NumberOfCompanions, bool StripGear)
			: this()
		{
			this.NumberOfCompanions = NumberOfCompanions;
			this.StripGear = StripGear;
		}

		// Token: 0x0600978E RID: 38798 RVA: 0x00370A10 File Offset: 0x0036EC10
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.CompanionsPlaced) || (ID == OnDeathRemovalEvent.ID && this.StopFightOnDeath);
		}

		// Token: 0x0600978F RID: 38799 RVA: 0x00370A3E File Offset: 0x0036EC3E
		public override bool HandleEvent(OnDeathRemovalEvent E)
		{
			if (this.StopFightOnDeath)
			{
				this.ParentObject.StopFight(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009790 RID: 38800 RVA: 0x00370A5C File Offset: 0x0036EC5C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.CompanionsPlaced)
			{
				this.CompanionsPlaced = true;
				Cell currentCell = this.ParentObject.CurrentCell;
				string text = "DynamicInheritsTable:Creature:Tier" + this.ParentObject.GetTier().ToString();
				int i = 0;
				int num = this.NumberOfCompanions.RollCached();
				while (i < num)
				{
					GameObjectBlueprint gameObjectBlueprint = null;
					int num2 = 0;
					while (++num2 < 10)
					{
						GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(PopulationManager.RollOneFrom(text, null, null).Blueprint);
						if (HasCompanions.IsSuitableCompanion(blueprintIfExists))
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
							gameObject.AddPart<Companion>(new Companion(this.ParentObject, null, null, null, null, this.StripGear), true, false);
							firstEmptyAdjacentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).MakeActive(false);
						}
					}
					i++;
				}
			}
			return true;
		}

		// Token: 0x06009791 RID: 38801 RVA: 0x00370B5E File Offset: 0x0036ED5E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009792 RID: 38802 RVA: 0x00370B64 File Offset: 0x0036ED64
		public static bool IsSuitableCompanion(GameObjectBlueprint BP)
		{
			return BP != null && BP.HasPart("Body") && BP.HasPart("Brain") && BP.HasPart("Combat") && BP.HasStat("Level") && BP.GetPartParameter<bool>("Brain", "Mobile", true) && !BP.GetPartParameter<bool>("Brain", "Aquatic", false) && !BP.HasPart("AIWallWalker") && !BP.HasProperName();
		}

		// Token: 0x04003F02 RID: 16130
		public string NumberOfCompanions = "2-4";

		// Token: 0x04003F03 RID: 16131
		public bool StripGear;

		// Token: 0x04003F04 RID: 16132
		public bool CompanionsPlaced;

		// Token: 0x04003F05 RID: 16133
		public bool StopFightOnDeath;
	}
}
