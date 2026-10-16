using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired when choosing a text element for preparation and can control the chosen text.</summary>
	// Token: 0x02000BCE RID: 3022
	[ConversationEvent(Action = ConversationEvent.Action.Custom)]
	public class GetTextElementEvent : ConversationEvent
	{
		// Token: 0x06007973 RID: 31091 RVA: 0x002DC81A File Offset: 0x002DAA1A
		public GetTextElementEvent()
			: base(GetTextElementEvent.ID)
		{
		}

		// Token: 0x06007974 RID: 31092 RVA: 0x002DC848 File Offset: 0x002DAA48
		public static int CountPool()
		{
			if (GetTextElementEvent.Pool != null)
			{
				return GetTextElementEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06007975 RID: 31093 RVA: 0x002DC85D File Offset: 0x002DAA5D
		public static void ResetPool()
		{
			while (GetTextElementEvent.PoolCounter > 0)
			{
				GetTextElementEvent.Pool[--GetTextElementEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06007976 RID: 31094 RVA: 0x002DC885 File Offset: 0x002DAA85
		public static void ResetTo(ref GetTextElementEvent E)
		{
			ConversationEvent.ResetTo<GetTextElementEvent>(E, GetTextElementEvent.Pool, ref GetTextElementEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06007977 RID: 31095 RVA: 0x002DC89B File Offset: 0x002DAA9B
		public static GetTextElementEvent FromPool()
		{
			return ConversationEvent.FromPool<GetTextElementEvent>(ref GetTextElementEvent.Pool, ref GetTextElementEvent.PoolCounter, 8192);
		}

		// Token: 0x06007978 RID: 31096 RVA: 0x002DC8B1 File Offset: 0x002DAAB1
		public static GetTextElementEvent FromPool(IConversationElement Element, ConversationText Selected)
		{
			GetTextElementEvent getTextElementEvent = GetTextElementEvent.FromPool();
			getTextElementEvent.Element = Element;
			getTextElementEvent.Selected = Selected;
			return getTextElementEvent;
		}

		// Token: 0x06007979 RID: 31097 RVA: 0x002DC8C6 File Offset: 0x002DAAC6
		public override void Reset()
		{
			base.Reset();
			this.Texts.Clear();
			this.Visible.Clear();
			this.Group.Clear();
			this.Selected = null;
		}

		// Token: 0x0600797A RID: 31098 RVA: 0x002DC8F6 File Offset: 0x002DAAF6
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x04003783 RID: 14211
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(GetTextElementEvent), null, new ConversationEvent.EventPoolCount(GetTextElementEvent.CountPool), new ConversationEvent.EventPoolReset(GetTextElementEvent.ResetPool));

		// Token: 0x04003784 RID: 14212
		private static List<GetTextElementEvent> Pool;

		// Token: 0x04003785 RID: 14213
		private static int PoolCounter;

		// Token: 0x04003786 RID: 14214
		[ConversationEvent.ParameterAttribute(Exclude = true)]
		public List<ConversationText> Texts = new List<ConversationText>();

		// Token: 0x04003787 RID: 14215
		[ConversationEvent.ParameterAttribute(Exclude = true)]
		public List<ConversationText> Visible = new List<ConversationText>();

		// Token: 0x04003788 RID: 14216
		[ConversationEvent.ParameterAttribute(Exclude = true)]
		public List<ConversationText> Group = new List<ConversationText>();

		// Token: 0x04003789 RID: 14217
		[ConversationEvent.ParameterAttribute(Reference = true)]
		public ConversationText Selected;
	}
}
