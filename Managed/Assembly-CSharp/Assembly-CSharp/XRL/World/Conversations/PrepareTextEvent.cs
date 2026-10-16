using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Conversations
{
	/// <summary>Fired when preparing spoken text for display after a node has been entered.</summary>
	/// <remarks>This precedes the standard variable replacements like =subject.name= and allows setting a new Subject and Object.</remarks>
	// Token: 0x02000BD6 RID: 3030
	[ConversationEvent(Action = ConversationEvent.Action.Send)]
	public class PrepareTextEvent : ITemplateTextEvent
	{
		// Token: 0x060079AF RID: 31151 RVA: 0x002DCE5B File Offset: 0x002DB05B
		public PrepareTextEvent()
			: base(PrepareTextEvent.ID)
		{
		}

		// Token: 0x060079B0 RID: 31152 RVA: 0x002DCE68 File Offset: 0x002DB068
		public static int CountPool()
		{
			if (PrepareTextEvent.Pool != null)
			{
				return PrepareTextEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060079B1 RID: 31153 RVA: 0x002DCE7D File Offset: 0x002DB07D
		public static void ResetPool()
		{
			while (PrepareTextEvent.PoolCounter > 0)
			{
				PrepareTextEvent.Pool[--PrepareTextEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060079B2 RID: 31154 RVA: 0x002DCEA5 File Offset: 0x002DB0A5
		public static void ResetTo(ref PrepareTextEvent E)
		{
			ConversationEvent.ResetTo<PrepareTextEvent>(E, PrepareTextEvent.Pool, ref PrepareTextEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060079B3 RID: 31155 RVA: 0x002DCEBB File Offset: 0x002DB0BB
		public static PrepareTextEvent FromPool()
		{
			return ConversationEvent.FromPool<PrepareTextEvent>(ref PrepareTextEvent.Pool, ref PrepareTextEvent.PoolCounter, 8192);
		}

		// Token: 0x060079B4 RID: 31156 RVA: 0x002DCED1 File Offset: 0x002DB0D1
		public static PrepareTextEvent FromPool(IConversationElement Element, StringBuilder Text, GameObject Subject, GameObject Object)
		{
			PrepareTextEvent prepareTextEvent = PrepareTextEvent.FromPool();
			prepareTextEvent.Element = Element;
			prepareTextEvent.Text = Text;
			prepareTextEvent.Subject = Subject;
			prepareTextEvent.Object = Object;
			return prepareTextEvent;
		}

		// Token: 0x060079B5 RID: 31157 RVA: 0x002DCEF4 File Offset: 0x002DB0F4
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x060079B6 RID: 31158 RVA: 0x002DCF00 File Offset: 0x002DB100
		public static void Send(IConversationElement Element, StringBuilder Text, ref GameObject Subject, ref GameObject Object, out string ExplicitSubject, out bool ExplicitSubjectPlural, out string ExplicitObject, out bool ExplicitObjectPlural)
		{
			if (Element.WantEvent(PrepareTextEvent.ID))
			{
				PrepareTextEvent prepareTextEvent = PrepareTextEvent.FromPool(Element, Text, Subject, Object);
				Element.HandleEvent(prepareTextEvent);
				Subject = prepareTextEvent.Subject;
				Object = prepareTextEvent.Object;
				ExplicitSubject = prepareTextEvent.ExplicitSubject;
				ExplicitSubjectPlural = prepareTextEvent.ExplicitSubjectPlural;
				ExplicitObject = prepareTextEvent.ExplicitObject;
				ExplicitObjectPlural = prepareTextEvent.ExplicitObjectPlural;
				return;
			}
			ExplicitSubject = null;
			ExplicitSubjectPlural = false;
			ExplicitObject = null;
			ExplicitObjectPlural = false;
		}

		// Token: 0x040037A4 RID: 14244
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(PrepareTextEvent), null, new ConversationEvent.EventPoolCount(PrepareTextEvent.CountPool), new ConversationEvent.EventPoolReset(PrepareTextEvent.ResetPool));

		// Token: 0x040037A5 RID: 14245
		private static List<PrepareTextEvent> Pool;

		// Token: 0x040037A6 RID: 14246
		private static int PoolCounter;
	}
}
