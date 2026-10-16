using System;
using XRL.World.Quests;

namespace XRL.World.Parts
{
	// Token: 0x02000DD5 RID: 3541
	[Serializable]
	public class OmonporchBattleWarleader : FinishQuestStepWhenSlain
	{
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x0600901F RID: 36895 RVA: 0x0033FCE4 File Offset: 0x0033DEE4
		public ReclamationSystem System
		{
			get
			{
				ReclamationSystem reclamationSystem;
				if ((reclamationSystem = this._System) == null)
				{
					reclamationSystem = (this._System = The.Game.GetSystem<ReclamationSystem>());
				}
				return reclamationSystem;
			}
		}

		// Token: 0x06009020 RID: 36896 RVA: 0x0033FD0E File Offset: 0x0033DF0E
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == SuspendingEvent.ID;
		}

		// Token: 0x06009021 RID: 36897 RVA: 0x0033FD24 File Offset: 0x0033DF24
		public override bool HandleEvent(SuspendingEvent E)
		{
			ReclamationSystem system = this.System;
			if (system == null || !The.Game.Quests.ContainsKey(system.QuestID))
			{
				this.ParentObject.RemovePart(this);
			}
			else
			{
				Zone currentZone = this.ParentObject.CurrentZone;
				if (currentZone != null && !system.Perimeter.Contains(currentZone.ZoneID))
				{
					this.Trigger();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C1E RID: 15390
		[NonSerialized]
		private ReclamationSystem _System;
	}
}
