using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired after leaving the current node and can control the navigation target.</summary>
	// Token: 0x02000BCD RID: 3021
	[ConversationEvent(Action = ConversationEvent.Action.Send)]
	public class GetTargetElementEvent : ConversationEvent
	{
		// Token: 0x06007969 RID: 31081 RVA: 0x002DC715 File Offset: 0x002DA915
		public GetTargetElementEvent()
			: base(GetTargetElementEvent.ID)
		{
		}

		// Token: 0x0600796A RID: 31082 RVA: 0x002DC722 File Offset: 0x002DA922
		public static int CountPool()
		{
			if (GetTargetElementEvent.Pool != null)
			{
				return GetTargetElementEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600796B RID: 31083 RVA: 0x002DC737 File Offset: 0x002DA937
		public static void ResetPool()
		{
			while (GetTargetElementEvent.PoolCounter > 0)
			{
				GetTargetElementEvent.Pool[--GetTargetElementEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600796C RID: 31084 RVA: 0x002DC75F File Offset: 0x002DA95F
		public static void ResetTo(ref GetTargetElementEvent E)
		{
			ConversationEvent.ResetTo<GetTargetElementEvent>(E, GetTargetElementEvent.Pool, ref GetTargetElementEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600796D RID: 31085 RVA: 0x002DC775 File Offset: 0x002DA975
		public static GetTargetElementEvent FromPool()
		{
			return ConversationEvent.FromPool<GetTargetElementEvent>(ref GetTargetElementEvent.Pool, ref GetTargetElementEvent.PoolCounter, 8192);
		}

		// Token: 0x0600796E RID: 31086 RVA: 0x002DC78B File Offset: 0x002DA98B
		public static GetTargetElementEvent FromPool(IConversationElement Element, string Target)
		{
			GetTargetElementEvent getTargetElementEvent = GetTargetElementEvent.FromPool();
			getTargetElementEvent.Element = Element;
			getTargetElementEvent.Target = Target;
			return getTargetElementEvent;
		}

		// Token: 0x0600796F RID: 31087 RVA: 0x002DC7A0 File Offset: 0x002DA9A0
		public override void Reset()
		{
			base.Reset();
			this.Target = null;
		}

		// Token: 0x06007970 RID: 31088 RVA: 0x002DC7AF File Offset: 0x002DA9AF
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x06007971 RID: 31089 RVA: 0x002DC7B8 File Offset: 0x002DA9B8
		public static void Send(IConversationElement Element, ref string Target)
		{
			if (Element.WantEvent(GetTargetElementEvent.ID))
			{
				GetTargetElementEvent getTargetElementEvent = GetTargetElementEvent.FromPool(Element, Target);
				Element.HandleEvent(getTargetElementEvent);
				Target = getTargetElementEvent.Target;
			}
		}

		// Token: 0x0400377F RID: 14207
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(GetTargetElementEvent), null, new ConversationEvent.EventPoolCount(GetTargetElementEvent.CountPool), new ConversationEvent.EventPoolReset(GetTargetElementEvent.ResetPool));

		// Token: 0x04003780 RID: 14208
		private static List<GetTargetElementEvent> Pool;

		// Token: 0x04003781 RID: 14209
		private static int PoolCounter;

		// Token: 0x04003782 RID: 14210
		[ConversationEvent.ParameterAttribute(Reference = true)]
		public string Target;
	}
}
