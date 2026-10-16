using System;
using System.Linq;

namespace XRL.World.Parts
{
	// Token: 0x02000FDB RID: 4059
	[Serializable]
	public class EskhindSpawner : IPart
	{
		// Token: 0x0600A13D RID: 41277 RVA: 0x003AAF92 File Offset: 0x003A9192
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A13E RID: 41278 RVA: 0x003AAF95 File Offset: 0x003A9195
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600A13F RID: 41279 RVA: 0x003AAFAC File Offset: 0x003A91AC
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (!this.spawned && The.Game.HasQuest("Kith and Kin") && !The.Game.HasGameState("EskhindKilled"))
			{
				this.spawned = true;
				GameObject gameObject = The.ZoneManager.FindObjectsReadonly((GameObject x) => x.Blueprint == "Eskhind").FirstOrDefault<GameObject>();
				if (gameObject == null)
				{
					gameObject = GameObject.Create("Eskhind");
				}
				if (!gameObject.IsPlayer())
				{
					this.ParentObject.CurrentCell.GetCellOrFirstConnectedSpawnLocation(true).AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					gameObject.Brain.StartingCell = null;
					gameObject.Brain.Goals.Clear();
				}
				this.ParentObject.CurrentCell.GetCellOrFirstConnectedSpawnLocation(true).AddObject("Meyehind", null, null, null, null);
				this.ParentObject.CurrentCell.GetCellOrFirstConnectedSpawnLocation(true).AddObject("Liihart", null, null, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040042FB RID: 17147
		public bool spawned;
	}
}
