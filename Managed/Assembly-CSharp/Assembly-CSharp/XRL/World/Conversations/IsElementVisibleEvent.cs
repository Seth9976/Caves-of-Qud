using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired when determining whether an element is possibly available for rendering and selection, after any predicates defined on the element.</summary>
	// Token: 0x02000BD2 RID: 3026
	[ConversationEvent(Action = ConversationEvent.Action.Check)]
	public class IsElementVisibleEvent : ConversationEvent
	{
		// Token: 0x0600798A RID: 31114 RVA: 0x002DCA8F File Offset: 0x002DAC8F
		public IsElementVisibleEvent()
			: base(IsElementVisibleEvent.ID)
		{
		}

		// Token: 0x0600798B RID: 31115 RVA: 0x002DCA9C File Offset: 0x002DAC9C
		public static int CountPool()
		{
			if (IsElementVisibleEvent.Pool != null)
			{
				return IsElementVisibleEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600798C RID: 31116 RVA: 0x002DCAB1 File Offset: 0x002DACB1
		public static void ResetPool()
		{
			while (IsElementVisibleEvent.PoolCounter > 0)
			{
				IsElementVisibleEvent.Pool[--IsElementVisibleEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600798D RID: 31117 RVA: 0x002DCAD9 File Offset: 0x002DACD9
		public static void ResetTo(ref IsElementVisibleEvent E)
		{
			ConversationEvent.ResetTo<IsElementVisibleEvent>(E, IsElementVisibleEvent.Pool, ref IsElementVisibleEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600798E RID: 31118 RVA: 0x002DCAEF File Offset: 0x002DACEF
		public static IsElementVisibleEvent FromPool()
		{
			return ConversationEvent.FromPool<IsElementVisibleEvent>(ref IsElementVisibleEvent.Pool, ref IsElementVisibleEvent.PoolCounter, 8192);
		}

		// Token: 0x0600798F RID: 31119 RVA: 0x002DCB05 File Offset: 0x002DAD05
		public static IsElementVisibleEvent FromPool(IConversationElement Element)
		{
			IsElementVisibleEvent isElementVisibleEvent = IsElementVisibleEvent.FromPool();
			isElementVisibleEvent.Element = Element;
			return isElementVisibleEvent;
		}

		// Token: 0x06007990 RID: 31120 RVA: 0x002DCB13 File Offset: 0x002DAD13
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x06007991 RID: 31121 RVA: 0x002DCB1C File Offset: 0x002DAD1C
		public static bool Check(IConversationElement Element)
		{
			if (Element.WantEvent(IsElementVisibleEvent.ID))
			{
				IsElementVisibleEvent isElementVisibleEvent = IsElementVisibleEvent.FromPool(Element);
				return Element.HandleEvent(isElementVisibleEvent);
			}
			return true;
		}

		// Token: 0x04003797 RID: 14231
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(IsElementVisibleEvent), null, new ConversationEvent.EventPoolCount(IsElementVisibleEvent.CountPool), new ConversationEvent.EventPoolReset(IsElementVisibleEvent.ResetPool));

		// Token: 0x04003798 RID: 14232
		private static List<IsElementVisibleEvent> Pool;

		// Token: 0x04003799 RID: 14233
		private static int PoolCounter;
	}
}
