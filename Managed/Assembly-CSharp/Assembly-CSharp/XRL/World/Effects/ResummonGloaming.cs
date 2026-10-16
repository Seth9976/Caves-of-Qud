using System;
using XRL.World.AI;

namespace XRL.World.Effects
{
	// Token: 0x02000DB6 RID: 3510
	[Serializable]
	public class ResummonGloaming : Effect
	{
		// Token: 0x06008C18 RID: 35864 RVA: 0x0031B6E7 File Offset: 0x003198E7
		public ResummonGloaming()
		{
			this.Duration = 1;
		}

		// Token: 0x06008C19 RID: 35865 RVA: 0x0031B6F6 File Offset: 0x003198F6
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008C1A RID: 35866 RVA: 0x0031B6F9 File Offset: 0x003198F9
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<ResummonGloaming>())
			{
				return false;
			}
			if (Object.CurrentZone != null)
			{
				this.currentZone = Object.CurrentZone.ZoneID;
			}
			return true;
		}

		// Token: 0x06008C1B RID: 35867 RVA: 0x0031B71F File Offset: 0x0031991F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x06008C1C RID: 35868 RVA: 0x0031B738 File Offset: 0x00319938
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Duration > 0)
			{
				Zone zone = base.Object.CurrentZone;
				if (((zone != null) ? zone.ZoneID : null) != this.currentZone && !base.Object.OnWorldMap())
				{
					Cell connectedSpawnLocation = base.Object.CurrentCell.GetConnectedSpawnLocation(true);
					if (connectedSpawnLocation != null)
					{
						GameObject gameObject = connectedSpawnLocation.AddObject("Gloaming", null, null, null, null);
						gameObject.SetAlliedLeader<AllyPet>(base.Object, 0, false);
						this.Duration = 0;
						IComponent<GameObject>.XDidY(gameObject, "reappear", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003AF2 RID: 15090
		public string currentZone;
	}
}
