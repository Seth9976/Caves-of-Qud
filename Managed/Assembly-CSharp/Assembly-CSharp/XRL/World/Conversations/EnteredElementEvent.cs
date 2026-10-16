using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired after an element has successfully been entered.</summary>
	// Token: 0x02000BCB RID: 3019
	[ConversationEvent(Action = ConversationEvent.Action.Send)]
	public class EnteredElementEvent : ConversationEvent
	{
		// Token: 0x06007955 RID: 31061 RVA: 0x002DC4E9 File Offset: 0x002DA6E9
		public EnteredElementEvent()
			: base(EnteredElementEvent.ID)
		{
		}

		// Token: 0x06007956 RID: 31062 RVA: 0x002DC4F6 File Offset: 0x002DA6F6
		public static int CountPool()
		{
			if (EnteredElementEvent.Pool != null)
			{
				return EnteredElementEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06007957 RID: 31063 RVA: 0x002DC50B File Offset: 0x002DA70B
		public static void ResetPool()
		{
			while (EnteredElementEvent.PoolCounter > 0)
			{
				EnteredElementEvent.Pool[--EnteredElementEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06007958 RID: 31064 RVA: 0x002DC533 File Offset: 0x002DA733
		public static void ResetTo(ref EnteredElementEvent E)
		{
			ConversationEvent.ResetTo<EnteredElementEvent>(E, EnteredElementEvent.Pool, ref EnteredElementEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06007959 RID: 31065 RVA: 0x002DC549 File Offset: 0x002DA749
		public static EnteredElementEvent FromPool()
		{
			return ConversationEvent.FromPool<EnteredElementEvent>(ref EnteredElementEvent.Pool, ref EnteredElementEvent.PoolCounter, 8192);
		}

		// Token: 0x0600795A RID: 31066 RVA: 0x002DC55F File Offset: 0x002DA75F
		public static EnteredElementEvent FromPool(IConversationElement Element)
		{
			EnteredElementEvent enteredElementEvent = EnteredElementEvent.FromPool();
			enteredElementEvent.Element = Element;
			return enteredElementEvent;
		}

		// Token: 0x0600795B RID: 31067 RVA: 0x002DC56D File Offset: 0x002DA76D
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x0600795C RID: 31068 RVA: 0x002DC578 File Offset: 0x002DA778
		public static void Send(IConversationElement Element)
		{
			if (Element.WantEvent(EnteredElementEvent.ID))
			{
				EnteredElementEvent enteredElementEvent = EnteredElementEvent.FromPool(Element);
				Element.HandleEvent(enteredElementEvent);
			}
		}

		// Token: 0x04003778 RID: 14200
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(EnteredElementEvent), null, new ConversationEvent.EventPoolCount(EnteredElementEvent.CountPool), new ConversationEvent.EventPoolReset(EnteredElementEvent.ResetPool));

		// Token: 0x04003779 RID: 14201
		private static List<EnteredElementEvent> Pool;

		// Token: 0x0400377A RID: 14202
		private static int PoolCounter;
	}
}
