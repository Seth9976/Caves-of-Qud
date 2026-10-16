using System;

namespace XRL.World.Parts
{
	// Token: 0x02001219 RID: 4633
	[Serializable]
	public class AfterEarlSpawner : IPart
	{
		// Token: 0x0600B814 RID: 47124 RVA: 0x0042EEB9 File Offset: 0x0042D0B9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600B815 RID: 47125 RVA: 0x0042EED8 File Offset: 0x0042D0D8
		public void checkSpawn()
		{
			if (!this.Spawned && (The.Game.HasIntGameState("ForcePostEarlSpawn") || (The.Game.GetQuestFinishTime("The Earl of Omonporch") > 0L && Calendar.TotalTimeTicks - The.Game.GetQuestFinishTime("The Earl of Omonporch") > 16800L)))
			{
				this.Spawned = true;
				base.currentCell.ParentZone.GetCell(45, 16).AddObject("OmonporchBarathrumiteCamp1", null, null, null, null);
				base.currentCell.ParentZone.GetCell(51, 6).AddObject("OmonporchBarathrumiteCamp2", null, null, null, null);
				base.currentCell.ParentZone.GetCell(62, 2).AddObject("OmonporchBarathrumiteCamp3", null, null, null, null);
			}
		}

		// Token: 0x0600B816 RID: 47126 RVA: 0x0042EFA2 File Offset: 0x0042D1A2
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.checkSpawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B817 RID: 47127 RVA: 0x0042EFB1 File Offset: 0x0042D1B1
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.checkSpawn();
			return base.HandleEvent(E);
		}

		// Token: 0x04004A0D RID: 18957
		public bool Spawned;
	}
}
