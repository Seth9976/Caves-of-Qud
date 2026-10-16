using System;
using XRL.World.Quests;

namespace XRL.World.Parts
{
	// Token: 0x02000DD4 RID: 3540
	[Serializable]
	public class OmonporchBattleNephilim : IPart
	{
		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x0600901A RID: 36890 RVA: 0x0033FB9C File Offset: 0x0033DD9C
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

		// Token: 0x0600901B RID: 36891 RVA: 0x0033FBC6 File Offset: 0x0033DDC6
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == EnteredCellEvent.ID || ID == SuspendingEvent.ID;
		}

		// Token: 0x0600901C RID: 36892 RVA: 0x0033FBE4 File Offset: 0x0033DDE4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			ReclamationSystem system = this.System;
			if (system == null || !The.Game.Quests.ContainsKey(system.QuestID))
			{
				this.ParentObject.RemovePart(this);
			}
			else
			{
				system.NephalLocation.SetCell(E.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600901D RID: 36893 RVA: 0x0033FC38 File Offset: 0x0033DE38
		public override bool HandleEvent(SuspendingEvent E)
		{
			ReclamationSystem system = this.System;
			if (system == null || !The.Game.Quests.ContainsKey(system.QuestID))
			{
				this.ParentObject.RemovePart(this);
			}
			else
			{
				Zone zone = The.ZoneManager.GetZone(system.GetProperty("NephalRetryZone", null));
				if (zone != null)
				{
					Cell randomElement = zone.GetEmptyCells(null, null).GetRandomElement<Cell>(null);
					this.ParentObject.TeleportTo(randomElement, new int?(0), true, false, false, false, "disappear", "appear");
					if (system.Active)
					{
						this.ParentObject.MakeActive(false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C1D RID: 15389
		[NonSerialized]
		private ReclamationSystem _System;
	}
}
