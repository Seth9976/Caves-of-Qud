using System;
using XRL.World.ZoneBuilders;

namespace XRL.World.Parts
{
	// Token: 0x0200120A RID: 4618
	[Serializable]
	public class InteractQuestTarget : IPart
	{
		// Token: 0x0600B7AC RID: 47020 RVA: 0x0042D2F4 File Offset: 0x0042B4F4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(this.EventID);
		}

		// Token: 0x0600B7AD RID: 47021 RVA: 0x0042D304 File Offset: 0x0042B504
		public override bool FireEvent(Event E)
		{
			string eventID = this.EventID;
			if (E.ID == eventID)
			{
				InteractWithAnObjectDynamicQuestManager.System system = The.Game.GetSystem<InteractWithAnObjectDynamicQuestManager.System>();
				if (system != null)
				{
					system.QuestableInteract(this.ParentObject);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040049DD RID: 18909
		public string EventID;
	}
}
