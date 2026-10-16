using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired as an element is being left and can prevent navigation.</summary>
	// Token: 0x02000BD3 RID: 3027
	[ConversationEvent(Action = ConversationEvent.Action.Check)]
	public class LeaveElementEvent : ConversationEvent
	{
		// Token: 0x06007993 RID: 31123 RVA: 0x002DCB75 File Offset: 0x002DAD75
		public LeaveElementEvent()
			: base(LeaveElementEvent.ID)
		{
		}

		// Token: 0x06007994 RID: 31124 RVA: 0x002DCB82 File Offset: 0x002DAD82
		public static int CountPool()
		{
			if (LeaveElementEvent.Pool != null)
			{
				return LeaveElementEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06007995 RID: 31125 RVA: 0x002DCB97 File Offset: 0x002DAD97
		public static void ResetPool()
		{
			while (LeaveElementEvent.PoolCounter > 0)
			{
				LeaveElementEvent.Pool[--LeaveElementEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06007996 RID: 31126 RVA: 0x002DCBBF File Offset: 0x002DADBF
		public static void ResetTo(ref LeaveElementEvent E)
		{
			ConversationEvent.ResetTo<LeaveElementEvent>(E, LeaveElementEvent.Pool, ref LeaveElementEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06007997 RID: 31127 RVA: 0x002DCBD5 File Offset: 0x002DADD5
		public static LeaveElementEvent FromPool()
		{
			return ConversationEvent.FromPool<LeaveElementEvent>(ref LeaveElementEvent.Pool, ref LeaveElementEvent.PoolCounter, 8192);
		}

		// Token: 0x06007998 RID: 31128 RVA: 0x002DCBEB File Offset: 0x002DADEB
		public static LeaveElementEvent FromPool(IConversationElement Element)
		{
			LeaveElementEvent leaveElementEvent = LeaveElementEvent.FromPool();
			leaveElementEvent.Element = Element;
			return leaveElementEvent;
		}

		// Token: 0x06007999 RID: 31129 RVA: 0x002DCBF9 File Offset: 0x002DADF9
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x0600799A RID: 31130 RVA: 0x002DCC04 File Offset: 0x002DAE04
		public static bool Check(IConversationElement Element)
		{
			if (Element.WantEvent(LeaveElementEvent.ID))
			{
				LeaveElementEvent leaveElementEvent = LeaveElementEvent.FromPool(Element);
				return Element.HandleEvent(leaveElementEvent);
			}
			return true;
		}

		// Token: 0x0400379A RID: 14234
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(LeaveElementEvent), null, new ConversationEvent.EventPoolCount(LeaveElementEvent.CountPool), new ConversationEvent.EventPoolReset(LeaveElementEvent.ResetPool));

		// Token: 0x0400379B RID: 14235
		private static List<LeaveElementEvent> Pool;

		// Token: 0x0400379C RID: 14236
		private static int PoolCounter;
	}
}
