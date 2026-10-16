using System;

namespace XRL.World.Parts
{
	// Token: 0x02001218 RID: 4632
	[Serializable]
	public class AfterBrightsheolStudySpawner : IPart
	{
		// Token: 0x0600B80F RID: 47119 RVA: 0x0042EE24 File Offset: 0x0042D024
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x0600B810 RID: 47120 RVA: 0x0042EE44 File Offset: 0x0042D044
		public void checkSpawn()
		{
			if (!this.Spawned && The.Game.HasBooleanGameState("Recame"))
			{
				this.Spawned = true;
				base.currentCell.ParentZone.GetCell(18, 6).AddObject("BarathrumsStudy_Klanq", null, null, null, null);
			}
		}

		// Token: 0x0600B811 RID: 47121 RVA: 0x0042EE93 File Offset: 0x0042D093
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.checkSpawn();
			return base.HandleEvent(E);
		}

		// Token: 0x0600B812 RID: 47122 RVA: 0x0042EEA2 File Offset: 0x0042D0A2
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.checkSpawn();
			return base.HandleEvent(E);
		}

		// Token: 0x04004A0C RID: 18956
		public bool Spawned;
	}
}
