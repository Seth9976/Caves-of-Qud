using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000EC7 RID: 3783
	[Serializable]
	public class HasSlaves : IBondedLeader
	{
		// Token: 0x060097A4 RID: 38820 RVA: 0x00371129 File Offset: 0x0036F329
		public HasSlaves()
		{
		}

		// Token: 0x060097A5 RID: 38821 RVA: 0x00371147 File Offset: 0x0036F347
		public HasSlaves(string NumberofSlaves, bool StripGear)
			: this()
		{
			this.NumberOfSlaves = NumberofSlaves;
			this.StripGear = StripGear;
		}

		// Token: 0x060097A6 RID: 38822 RVA: 0x0037115D File Offset: 0x0036F35D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.SlavesPlaced) || (ID == OnDeathRemovalEvent.ID && this.StopFightOnDeath);
		}

		// Token: 0x060097A7 RID: 38823 RVA: 0x0037118B File Offset: 0x0036F38B
		public override bool HandleEvent(OnDeathRemovalEvent E)
		{
			if (this.StopFightOnDeath)
			{
				this.ParentObject.StopFight(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097A8 RID: 38824 RVA: 0x003711A8 File Offset: 0x0036F3A8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.SlavesPlaced)
			{
				this.SlavesPlaced = true;
				Cell currentCell = this.ParentObject.CurrentCell;
				int i = 0;
				int num = this.NumberOfSlaves.RollCached();
				while (i < num)
				{
					int tier = this.SlaveTier.RollCached();
					string acreatureBlueprint = EncountersAPI.GetACreatureBlueprint((GameObjectBlueprint bp) => bp.Tier == tier && HasSlaves.IsSuitableSlave(bp));
					Cell firstEmptyAdjacentCell = currentCell.GetFirstEmptyAdjacentCell(1, 2);
					if (firstEmptyAdjacentCell != null)
					{
						GameObject gameObject = GameObject.Create(acreatureBlueprint);
						gameObject.AddPart<DomesticatedSlave>(new DomesticatedSlave(this.ParentObject, "Templar", "domesticated", null, "TemplarDomesticant", this.StripGear), true, false);
						firstEmptyAdjacentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).MakeActive(false);
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097A9 RID: 38825 RVA: 0x0037127E File Offset: 0x0036F47E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060097AA RID: 38826 RVA: 0x00371284 File Offset: 0x0036F484
		public static bool IsSuitableSlave(GameObjectBlueprint BP)
		{
			return BP != null && BP.HasPart("Body") && BP.HasPart("Brain") && BP.HasPart("Combat") && !BP.HasTag("Unenslavable", false) && BP.GetPartParameter<bool>("Brain", "Mobile", true) && !BP.HasPart("AIWallWalker") && !BP.HasProperName() && (!BP.IsBodyPartOccupied("Face") || !BP.IsBodyPartOccupied("Head"));
		}

		// Token: 0x04003F0D RID: 16141
		public string NumberOfSlaves = "2-4";

		// Token: 0x04003F0E RID: 16142
		public string SlaveTier = "4-5";

		// Token: 0x04003F0F RID: 16143
		public bool StripGear;

		// Token: 0x04003F10 RID: 16144
		public bool SlavesPlaced;

		// Token: 0x04003F11 RID: 16145
		public bool StopFightOnDeath;
	}
}
