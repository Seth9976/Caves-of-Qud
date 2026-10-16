using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired when evaluating elements to display that are hidden by special outside conditions.</summary>
	/// <remarks>One such condition is the last choice selected that will be hidden if navigation was successful but did not leave the current node.</remarks>
	// Token: 0x02000BCF RID: 3023
	[ConversationEvent(Action = ConversationEvent.Action.Check)]
	public class HideElementEvent : ConversationEvent
	{
		// Token: 0x0600797C RID: 31100 RVA: 0x002DC92E File Offset: 0x002DAB2E
		public HideElementEvent()
			: base(HideElementEvent.ID)
		{
		}

		// Token: 0x0600797D RID: 31101 RVA: 0x002DC93B File Offset: 0x002DAB3B
		public static int CountPool()
		{
			if (HideElementEvent.Pool != null)
			{
				return HideElementEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600797E RID: 31102 RVA: 0x002DC950 File Offset: 0x002DAB50
		public static void ResetPool()
		{
			while (HideElementEvent.PoolCounter > 0)
			{
				HideElementEvent.Pool[--HideElementEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600797F RID: 31103 RVA: 0x002DC978 File Offset: 0x002DAB78
		public static void ResetTo(ref HideElementEvent E)
		{
			ConversationEvent.ResetTo<HideElementEvent>(E, HideElementEvent.Pool, ref HideElementEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06007980 RID: 31104 RVA: 0x002DC98E File Offset: 0x002DAB8E
		public static HideElementEvent FromPool()
		{
			return ConversationEvent.FromPool<HideElementEvent>(ref HideElementEvent.Pool, ref HideElementEvent.PoolCounter, 8192);
		}

		// Token: 0x06007981 RID: 31105 RVA: 0x002DC9A4 File Offset: 0x002DABA4
		public static HideElementEvent FromPool(IConversationElement Element, string Context = null)
		{
			HideElementEvent hideElementEvent = HideElementEvent.FromPool();
			hideElementEvent.Element = Element;
			hideElementEvent.Context = Context;
			return hideElementEvent;
		}

		// Token: 0x06007982 RID: 31106 RVA: 0x002DC9B9 File Offset: 0x002DABB9
		public override void Reset()
		{
			base.Reset();
			this.Context = null;
		}

		// Token: 0x06007983 RID: 31107 RVA: 0x002DC9C8 File Offset: 0x002DABC8
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x06007984 RID: 31108 RVA: 0x002DC9D4 File Offset: 0x002DABD4
		public static bool Check(IConversationElement Element, string Context = null)
		{
			if (Element.WantEvent(HideElementEvent.ID))
			{
				HideElementEvent hideElementEvent = HideElementEvent.FromPool(Element, Context);
				return Element.HandleEvent(hideElementEvent);
			}
			return true;
		}

		// Token: 0x0400378A RID: 14218
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(HideElementEvent), null, new ConversationEvent.EventPoolCount(HideElementEvent.CountPool), new ConversationEvent.EventPoolReset(HideElementEvent.ResetPool));

		// Token: 0x0400378B RID: 14219
		private static List<HideElementEvent> Pool;

		// Token: 0x0400378C RID: 14220
		private static int PoolCounter;

		// Token: 0x0400378D RID: 14221
		public string Context;
	}
}
