using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired when coloring the display text of an element.</summary>
	/// <see cref="T:XRL.World.Conversations.DisplayTextEvent" />
	// Token: 0x02000BC8 RID: 3016
	[ConversationEvent(Action = ConversationEvent.Action.Send)]
	public class ColorTextEvent : ConversationEvent
	{
		// Token: 0x06007938 RID: 31032 RVA: 0x002DC19D File Offset: 0x002DA39D
		public ColorTextEvent()
			: base(ColorTextEvent.ID)
		{
		}

		// Token: 0x06007939 RID: 31033 RVA: 0x002DC1AA File Offset: 0x002DA3AA
		public static int CountPool()
		{
			if (ColorTextEvent.Pool != null)
			{
				return ColorTextEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600793A RID: 31034 RVA: 0x002DC1BF File Offset: 0x002DA3BF
		public static void ResetPool()
		{
			while (ColorTextEvent.PoolCounter > 0)
			{
				ColorTextEvent.Pool[--ColorTextEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600793B RID: 31035 RVA: 0x002DC1E7 File Offset: 0x002DA3E7
		public static void ResetTo(ref ColorTextEvent E)
		{
			ConversationEvent.ResetTo<ColorTextEvent>(E, ColorTextEvent.Pool, ref ColorTextEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600793C RID: 31036 RVA: 0x002DC1FD File Offset: 0x002DA3FD
		public static ColorTextEvent FromPool()
		{
			return ConversationEvent.FromPool<ColorTextEvent>(ref ColorTextEvent.Pool, ref ColorTextEvent.PoolCounter, 8192);
		}

		// Token: 0x0600793D RID: 31037 RVA: 0x002DC213 File Offset: 0x002DA413
		public static ColorTextEvent FromPool(IConversationElement Element, string Color)
		{
			ColorTextEvent colorTextEvent = ColorTextEvent.FromPool();
			colorTextEvent.Element = Element;
			colorTextEvent.Color = Color;
			return colorTextEvent;
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x002DC228 File Offset: 0x002DA428
		public override void Reset()
		{
			base.Reset();
			this.Color = null;
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x002DC237 File Offset: 0x002DA437
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x002DC240 File Offset: 0x002DA440
		public static void Send(IConversationElement Element, ref string Color)
		{
			if (Element.WantEvent(ColorTextEvent.ID))
			{
				ColorTextEvent colorTextEvent = ColorTextEvent.FromPool(Element, Color);
				Element.HandleEvent(colorTextEvent);
				Color = colorTextEvent.Color;
			}
		}

		// Token: 0x0400376D RID: 14189
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(ColorTextEvent), null, new ConversationEvent.EventPoolCount(ColorTextEvent.CountPool), new ConversationEvent.EventPoolReset(ColorTextEvent.ResetPool));

		// Token: 0x0400376E RID: 14190
		private static List<ColorTextEvent> Pool;

		// Token: 0x0400376F RID: 14191
		private static int PoolCounter;

		// Token: 0x04003770 RID: 14192
		[ConversationEvent.ParameterAttribute(Reference = true)]
		public string Color;
	}
}
