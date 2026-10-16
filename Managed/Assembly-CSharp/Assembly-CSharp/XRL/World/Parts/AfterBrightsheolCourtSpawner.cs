using System;

namespace XRL.World.Parts
{
	// Token: 0x02001217 RID: 4631
	[Serializable]
	public class AfterBrightsheolCourtSpawner : IPart
	{
		// Token: 0x0600B808 RID: 47112 RVA: 0x0042ED5F File Offset: 0x0042CF5F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID || ID == ZoneBuiltEvent.ID || ID == ZoneThawedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600B809 RID: 47113 RVA: 0x0042ED90 File Offset: 0x0042CF90
		public void checkSpawn()
		{
			if (!this.Spawned && The.Game.HasBooleanGameState("Recame"))
			{
				this.Spawned = true;
				base.currentCell.ParentZone.GetCell(26, 14).AddObject("CourtOfTheSultans_ReshapingNook", null, null, null, null);
			}
		}

		// Token: 0x0600B80A RID: 47114 RVA: 0x0042EDE0 File Offset: 0x0042CFE0
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.checkSpawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B80B RID: 47115 RVA: 0x0042EDEF File Offset: 0x0042CFEF
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.checkSpawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B80C RID: 47116 RVA: 0x0042EDFE File Offset: 0x0042CFFE
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.checkSpawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B80D RID: 47117 RVA: 0x0042EE0D File Offset: 0x0042D00D
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			this.checkSpawn();
			return base.HandleEvent(E);
		}

		// Token: 0x04004A0B RID: 18955
		public bool Spawned;
	}
}
