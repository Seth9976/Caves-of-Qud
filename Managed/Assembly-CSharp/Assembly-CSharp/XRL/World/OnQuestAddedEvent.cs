using System;

namespace XRL.World
{
	// Token: 0x0200081B RID: 2075
	[Obsolete("Use QuestStartedEvent.")]
	public abstract class OnQuestAddedEvent : IQuestEvent
	{
		// Token: 0x060052B7 RID: 21175 RVA: 0x0018DAA3 File Offset: 0x0018BCA3
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x0018DAB7 File Offset: 0x0018BCB7
		public GameObject Subject
		{
			get
			{
				return The.Player;
			}
		}

		// Token: 0x0400298F RID: 10639
		public new static readonly int ID = QuestStartedEvent.ID;
	}
}
