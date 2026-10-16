using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x020011B0 RID: 4528
	[Serializable]
	public class PetEitherOrRespawner : IPart
	{
		// Token: 0x0600B3D6 RID: 46038 RVA: 0x00419690 File Offset: 0x00417890
		public override IPart DeepCopy(GameObject Parent)
		{
			PetEitherOrRespawner petEitherOrRespawner = (PetEitherOrRespawner)base.DeepCopy(Parent);
			petEitherOrRespawner.respawnEither = false;
			petEitherOrRespawner.respawnOr = false;
			return petEitherOrRespawner;
		}

		// Token: 0x0600B3D7 RID: 46039 RVA: 0x004196AC File Offset: 0x004178AC
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B3D8 RID: 46040 RVA: 0x004196AF File Offset: 0x004178AF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600B3D9 RID: 46041 RVA: 0x004196C5 File Offset: 0x004178C5
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if ((this.respawnEither || this.respawnOr) && this.ParentObject.IsPlayer())
			{
				this.Respawn(E.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B3DA RID: 46042 RVA: 0x004196F8 File Offset: 0x004178F8
		public void Respawn(Cell Cell)
		{
			Zone parentZone = Cell.ParentZone;
			if (this.lastZone != "" && parentZone.ZoneID != this.lastZone && !parentZone.IsWorldMap())
			{
				if (this.respawnEither)
				{
					Cell connectedSpawnLocation = Cell.GetConnectedSpawnLocation(true);
					if (connectedSpawnLocation != null)
					{
						GameObject gameObject = connectedSpawnLocation.AddObject("EitherPet", null, null, null, null);
						gameObject.SetActive();
						gameObject.SetAlliedLeader<AllyPet>(The.Player, 0, false);
						gameObject.IsTrifling = true;
						this.respawnEither = false;
					}
				}
				if (this.respawnOr)
				{
					Cell connectedSpawnLocation2 = this.ParentObject.CurrentCell.GetConnectedSpawnLocation(true);
					if (connectedSpawnLocation2 != null)
					{
						GameObject gameObject2 = connectedSpawnLocation2.AddObject("OrPet", null, null, null, null);
						gameObject2.SetActive();
						gameObject2.SetAlliedLeader<AllyPet>(The.Player, 0, false);
						gameObject2.IsTrifling = true;
						this.respawnOr = false;
					}
				}
			}
			this.lastZone = parentZone.ZoneID;
		}

		// Token: 0x04004887 RID: 18567
		public bool respawnEither;

		// Token: 0x04004888 RID: 18568
		public bool respawnOr;

		// Token: 0x04004889 RID: 18569
		public string lastZone = "";
	}
}
