using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired as an element is being entered and can prevent navigation.</summary>
	// Token: 0x02000BCA RID: 3018
	[ConversationEvent(Action = ConversationEvent.Action.Check)]
	public class EnterElementEvent : ConversationEvent
	{
		// Token: 0x0600794C RID: 31052 RVA: 0x002DC403 File Offset: 0x002DA603
		public EnterElementEvent()
			: base(EnterElementEvent.ID)
		{
		}

		// Token: 0x0600794D RID: 31053 RVA: 0x002DC410 File Offset: 0x002DA610
		public static int CountPool()
		{
			if (EnterElementEvent.Pool != null)
			{
				return EnterElementEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600794E RID: 31054 RVA: 0x002DC425 File Offset: 0x002DA625
		public static void ResetPool()
		{
			while (EnterElementEvent.PoolCounter > 0)
			{
				EnterElementEvent.Pool[--EnterElementEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600794F RID: 31055 RVA: 0x002DC44D File Offset: 0x002DA64D
		public static void ResetTo(ref EnterElementEvent E)
		{
			ConversationEvent.ResetTo<EnterElementEvent>(E, EnterElementEvent.Pool, ref EnterElementEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06007950 RID: 31056 RVA: 0x002DC463 File Offset: 0x002DA663
		public static EnterElementEvent FromPool()
		{
			return ConversationEvent.FromPool<EnterElementEvent>(ref EnterElementEvent.Pool, ref EnterElementEvent.PoolCounter, 8192);
		}

		// Token: 0x06007951 RID: 31057 RVA: 0x002DC479 File Offset: 0x002DA679
		public static EnterElementEvent FromPool(IConversationElement Element)
		{
			EnterElementEvent enterElementEvent = EnterElementEvent.FromPool();
			enterElementEvent.Element = Element;
			return enterElementEvent;
		}

		// Token: 0x06007952 RID: 31058 RVA: 0x002DC487 File Offset: 0x002DA687
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x06007953 RID: 31059 RVA: 0x002DC490 File Offset: 0x002DA690
		public static bool Check(IConversationElement Element)
		{
			if (Element.WantEvent(EnterElementEvent.ID))
			{
				EnterElementEvent enterElementEvent = EnterElementEvent.FromPool(Element);
				return Element.HandleEvent(enterElementEvent);
			}
			return true;
		}

		// Token: 0x04003775 RID: 14197
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(EnterElementEvent), null, new ConversationEvent.EventPoolCount(EnterElementEvent.CountPool), new ConversationEvent.EventPoolReset(EnterElementEvent.ResetPool));

		// Token: 0x04003776 RID: 14198
		private static List<EnterElementEvent> Pool;

		// Token: 0x04003777 RID: 14199
		private static int PoolCounter;
	}
}
