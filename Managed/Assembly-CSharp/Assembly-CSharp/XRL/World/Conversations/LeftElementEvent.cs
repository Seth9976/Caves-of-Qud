using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired after an element has successfully been exited.</summary>
	// Token: 0x02000BD4 RID: 3028
	[ConversationEvent(Action = ConversationEvent.Action.Send)]
	public class LeftElementEvent : ConversationEvent
	{
		// Token: 0x0600799C RID: 31132 RVA: 0x002DCC5D File Offset: 0x002DAE5D
		public LeftElementEvent()
			: base(LeftElementEvent.ID)
		{
		}

		// Token: 0x0600799D RID: 31133 RVA: 0x002DCC6A File Offset: 0x002DAE6A
		public static int CountPool()
		{
			if (LeftElementEvent.Pool != null)
			{
				return LeftElementEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600799E RID: 31134 RVA: 0x002DCC7F File Offset: 0x002DAE7F
		public static void ResetPool()
		{
			while (LeftElementEvent.PoolCounter > 0)
			{
				LeftElementEvent.Pool[--LeftElementEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600799F RID: 31135 RVA: 0x002DCCA7 File Offset: 0x002DAEA7
		public static void ResetTo(ref LeftElementEvent E)
		{
			ConversationEvent.ResetTo<LeftElementEvent>(E, LeftElementEvent.Pool, ref LeftElementEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060079A0 RID: 31136 RVA: 0x002DCCBD File Offset: 0x002DAEBD
		public static LeftElementEvent FromPool()
		{
			return ConversationEvent.FromPool<LeftElementEvent>(ref LeftElementEvent.Pool, ref LeftElementEvent.PoolCounter, 8192);
		}

		// Token: 0x060079A1 RID: 31137 RVA: 0x002DCCD3 File Offset: 0x002DAED3
		public static LeftElementEvent FromPool(IConversationElement Element)
		{
			LeftElementEvent leftElementEvent = LeftElementEvent.FromPool();
			leftElementEvent.Element = Element;
			return leftElementEvent;
		}

		// Token: 0x060079A2 RID: 31138 RVA: 0x002DCCE1 File Offset: 0x002DAEE1
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x060079A3 RID: 31139 RVA: 0x002DCCEC File Offset: 0x002DAEEC
		public static void Send(IConversationElement Element)
		{
			if (Element.WantEvent(LeftElementEvent.ID))
			{
				LeftElementEvent leftElementEvent = LeftElementEvent.FromPool(Element);
				Element.HandleEvent(leftElementEvent);
			}
		}

		// Token: 0x0400379D RID: 14237
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(LeftElementEvent), null, new ConversationEvent.EventPoolCount(LeftElementEvent.CountPool), new ConversationEvent.EventPoolReset(LeftElementEvent.ResetPool));

		// Token: 0x0400379E RID: 14238
		private static List<LeftElementEvent> Pool;

		// Token: 0x0400379F RID: 14239
		private static int PoolCounter;
	}
}
