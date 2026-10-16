using System;

namespace XRL.World.Parts
{
	// Token: 0x02000EC8 RID: 3784
	[Serializable]
	public class HasThralls : IBondedLeader
	{
		// Token: 0x060097AB RID: 38827 RVA: 0x0037131E File Offset: 0x0036F51E
		public HasThralls()
		{
		}

		// Token: 0x060097AC RID: 38828 RVA: 0x00371331 File Offset: 0x0036F531
		public HasThralls(string NumberOfThralls, bool StripGear)
			: this()
		{
			this.NumberOfThralls = NumberOfThralls;
			this.StripGear = StripGear;
		}

		// Token: 0x060097AD RID: 38829 RVA: 0x00371347 File Offset: 0x0036F547
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == EnteredCellEvent.ID && !this.ThrallsPlaced) || (ID == OnDeathRemovalEvent.ID && this.StopFightOnDeath);
		}

		// Token: 0x060097AE RID: 38830 RVA: 0x00371375 File Offset: 0x0036F575
		public override bool HandleEvent(OnDeathRemovalEvent E)
		{
			if (this.StopFightOnDeath)
			{
				this.ParentObject.StopFight(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097AF RID: 38831 RVA: 0x00371394 File Offset: 0x0036F594
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!this.ThrallsPlaced)
			{
				this.ThrallsPlaced = true;
				Cell currentCell = this.ParentObject.CurrentCell;
				string text = "DynamicInheritsTable:Creature:Tier" + this.ParentObject.GetTier().ToString();
				int i = 0;
				int num = this.NumberOfThralls.RollCached();
				while (i < num)
				{
					GameObjectBlueprint gameObjectBlueprint = null;
					int num2 = 0;
					while (++num2 < 10)
					{
						GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(PopulationManager.RollOneFrom(text, null, null).Blueprint);
						if (HasThralls.IsSuitableThrall(blueprintIfExists))
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
							gameObject.AddPart<PsychicThrall>(new PsychicThrall(this.ParentObject, "Seekers", null, "psychic thrall", "PsychicThrall", this.StripGear), true, false);
							firstEmptyAdjacentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null).MakeActive(false);
						}
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097B0 RID: 38832 RVA: 0x003714A8 File Offset: 0x0036F6A8
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060097B1 RID: 38833 RVA: 0x003714AC File Offset: 0x0036F6AC
		public static bool IsSuitableThrall(GameObjectBlueprint BP)
		{
			return BP != null && BP.HasPart("Body") && BP.HasPart("Brain") && BP.HasPart("Combat") && !BP.HasPart("MentalShield") && BP.GetPartParameter<bool>("Brain", "Mobile", true) && !BP.HasPart("AIWallWalker") && !BP.HasProperName() && !BP.HasPart("Uplift");
		}

		// Token: 0x04003F12 RID: 16146
		public string NumberOfThralls = "2-4";

		// Token: 0x04003F13 RID: 16147
		public bool StripGear;

		// Token: 0x04003F14 RID: 16148
		public bool ThrallsPlaced;

		// Token: 0x04003F15 RID: 16149
		public bool StopFightOnDeath;
	}
}
