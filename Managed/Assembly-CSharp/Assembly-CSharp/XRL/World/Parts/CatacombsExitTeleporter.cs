using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F98 RID: 3992
	[Serializable]
	public class CatacombsExitTeleporter : IPoweredPart
	{
		// Token: 0x06009E67 RID: 40551 RVA: 0x00395F57 File Offset: 0x00394157
		public CatacombsExitTeleporter()
		{
			this.ChargeUse = 0;
			this.WorksOnCellContents = true;
			this.NameForStatus = "MatterRecompositionSystem";
		}

		// Token: 0x06009E68 RID: 40552 RVA: 0x00395F83 File Offset: 0x00394183
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x06009E69 RID: 40553 RVA: 0x00395FA9 File Offset: 0x003941A9
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			E.MinWeight(60);
			return base.HandleEvent(E);
		}

		// Token: 0x06009E6A RID: 40554 RVA: 0x00395FBA File Offset: 0x003941BA
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			E.MinWeight(2);
			return base.HandleEvent(E);
		}

		// Token: 0x06009E6B RID: 40555 RVA: 0x00395FCC File Offset: 0x003941CC
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object != this.ParentObject && this.IsObjectActivePartSubject(E.Object) && E.Object.IsCombatObject())
			{
				string randomElement = this.TargetZones.CachedCommaExpansion().GetRandomElement<string>(null);
				GameObject firstObjectWithPart = The.ZoneManager.GetZone(randomElement).GetFirstObjectWithPart("StairsUp");
				Cell cell;
				if (firstObjectWithPart == null)
				{
					cell = null;
				}
				else
				{
					Cell currentCell = firstObjectWithPart.CurrentCell;
					cell = ((currentCell != null) ? currentCell.GetConnectedSpawnLocation(true) : null);
				}
				Cell cell2 = cell;
				Cell currentCell2 = E.Object.CurrentCell;
				if (cell2 != null)
				{
					E.Object.TeleportTo(cell2, new int?(0), true, false, false, false, "disappear", "appear");
					if (E.Object.IsPlayer() && E.Object.CurrentCell != currentCell2)
					{
						cell2.ParentZone.SetActive();
						IComponent<GameObject>.AddPlayerMessage("You are teleported to an exit.", null, true);
					}
					E.Object.TeleportSwirl(null, "&C", false, null, 'ù', false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400418A RID: 16778
		public string TargetZones = "JoppaWorld.53.3.2.0.11,JoppaWorld.53.3.2.2.11";
	}
}
