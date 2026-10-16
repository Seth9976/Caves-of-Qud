using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired when selecting an ending tag to apply to the display text such as [begin trade].</summary>
	// Token: 0x02000BCC RID: 3020
	[ConversationEvent(Action = ConversationEvent.Action.Get)]
	public class GetChoiceTagEvent : ConversationEvent
	{
		// Token: 0x0600795E RID: 31070 RVA: 0x002DC5D0 File Offset: 0x002DA7D0
		public GetChoiceTagEvent()
			: base(GetChoiceTagEvent.ID)
		{
		}

		// Token: 0x0600795F RID: 31071 RVA: 0x002DC5DD File Offset: 0x002DA7DD
		public static int CountPool()
		{
			if (GetChoiceTagEvent.Pool != null)
			{
				return GetChoiceTagEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06007960 RID: 31072 RVA: 0x002DC5F2 File Offset: 0x002DA7F2
		public static void ResetPool()
		{
			while (GetChoiceTagEvent.PoolCounter > 0)
			{
				GetChoiceTagEvent.Pool[--GetChoiceTagEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06007961 RID: 31073 RVA: 0x002DC61A File Offset: 0x002DA81A
		public static void ResetTo(ref GetChoiceTagEvent E)
		{
			ConversationEvent.ResetTo<GetChoiceTagEvent>(E, GetChoiceTagEvent.Pool, ref GetChoiceTagEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x002DC630 File Offset: 0x002DA830
		public static GetChoiceTagEvent FromPool()
		{
			return ConversationEvent.FromPool<GetChoiceTagEvent>(ref GetChoiceTagEvent.Pool, ref GetChoiceTagEvent.PoolCounter, 8192);
		}

		// Token: 0x06007963 RID: 31075 RVA: 0x002DC646 File Offset: 0x002DA846
		public static GetChoiceTagEvent FromPool(IConversationElement Element, string Tag = null)
		{
			GetChoiceTagEvent getChoiceTagEvent = GetChoiceTagEvent.FromPool();
			getChoiceTagEvent.Element = Element;
			getChoiceTagEvent.Tag = Tag;
			return getChoiceTagEvent;
		}

		// Token: 0x06007964 RID: 31076 RVA: 0x002DC65B File Offset: 0x002DA85B
		public override void Reset()
		{
			base.Reset();
			this.Tag = null;
		}

		// Token: 0x06007965 RID: 31077 RVA: 0x002DC66A File Offset: 0x002DA86A
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x06007966 RID: 31078 RVA: 0x002DC674 File Offset: 0x002DA874
		public static string For(IConversationElement Element, string Tag = null)
		{
			if (Element.WantEvent(GetChoiceTagEvent.ID))
			{
				GetChoiceTagEvent getChoiceTagEvent = GetChoiceTagEvent.FromPool(Element, Tag);
				Element.HandleEvent(getChoiceTagEvent);
				return getChoiceTagEvent.Tag;
			}
			return null;
		}

		// Token: 0x06007967 RID: 31079 RVA: 0x002DC6A8 File Offset: 0x002DA8A8
		public static bool Try(IConversationElement Element, out string Tag)
		{
			if (Element.WantEvent(GetChoiceTagEvent.ID))
			{
				GetChoiceTagEvent getChoiceTagEvent = new GetChoiceTagEvent
				{
					Element = Element
				};
				if (!Element.HandleEvent(getChoiceTagEvent))
				{
					Tag = getChoiceTagEvent.Tag;
					return true;
				}
			}
			Tag = null;
			return false;
		}

		// Token: 0x0400377B RID: 14203
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(GetChoiceTagEvent), null, new ConversationEvent.EventPoolCount(GetChoiceTagEvent.CountPool), new ConversationEvent.EventPoolReset(GetChoiceTagEvent.ResetPool));

		// Token: 0x0400377C RID: 14204
		private static List<GetChoiceTagEvent> Pool;

		// Token: 0x0400377D RID: 14205
		private static int PoolCounter;

		// Token: 0x0400377E RID: 14206
		[ConversationEvent.ParameterAttribute(Get = true)]
		public string Tag;
	}
}
