using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired when preparing spoken text for display after a node has been entered.</summary>
	/// <remarks>This comes after the standard variable replacements like =subject.name=.</remarks>
	// Token: 0x02000BD7 RID: 3031
	[ConversationEvent(Action = ConversationEvent.Action.Send)]
	public class PrepareTextLateEvent : ConversationEvent
	{
		// Token: 0x060079B8 RID: 31160 RVA: 0x002DCFA2 File Offset: 0x002DB1A2
		public PrepareTextLateEvent()
			: base(PrepareTextLateEvent.ID)
		{
		}

		// Token: 0x060079B9 RID: 31161 RVA: 0x002DCFAF File Offset: 0x002DB1AF
		public static int CountPool()
		{
			if (PrepareTextLateEvent.Pool != null)
			{
				return PrepareTextLateEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060079BA RID: 31162 RVA: 0x002DCFC4 File Offset: 0x002DB1C4
		public static void ResetPool()
		{
			while (PrepareTextLateEvent.PoolCounter > 0)
			{
				PrepareTextLateEvent.Pool[--PrepareTextLateEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060079BB RID: 31163 RVA: 0x002DCFEC File Offset: 0x002DB1EC
		public static void ResetTo(ref PrepareTextLateEvent E)
		{
			ConversationEvent.ResetTo<PrepareTextLateEvent>(E, PrepareTextLateEvent.Pool, ref PrepareTextLateEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060079BC RID: 31164 RVA: 0x002DD002 File Offset: 0x002DB202
		public static PrepareTextLateEvent FromPool()
		{
			return ConversationEvent.FromPool<PrepareTextLateEvent>(ref PrepareTextLateEvent.Pool, ref PrepareTextLateEvent.PoolCounter, 8192);
		}

		// Token: 0x060079BD RID: 31165 RVA: 0x002DD018 File Offset: 0x002DB218
		public static PrepareTextLateEvent FromPool(IConversationElement Element, GameObject Subject, GameObject Object, string Text)
		{
			PrepareTextLateEvent prepareTextLateEvent = PrepareTextLateEvent.FromPool();
			prepareTextLateEvent.Element = Element;
			prepareTextLateEvent.Subject = Subject;
			prepareTextLateEvent.Object = Object;
			prepareTextLateEvent.Text = Text;
			return prepareTextLateEvent;
		}

		// Token: 0x060079BE RID: 31166 RVA: 0x002DD03B File Offset: 0x002DB23B
		public override void Reset()
		{
			base.Reset();
			this.Subject = null;
			this.Object = null;
			this.Text = null;
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x002DD058 File Offset: 0x002DB258
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x060079C0 RID: 31168 RVA: 0x002DD064 File Offset: 0x002DB264
		public static void Send(IConversationElement Element, GameObject Subject, GameObject Object, ref string Text)
		{
			if (Element.WantEvent(PrepareTextLateEvent.ID))
			{
				PrepareTextLateEvent prepareTextLateEvent = PrepareTextLateEvent.FromPool(Element, Subject, Object, Text);
				Element.HandleEvent(prepareTextLateEvent);
				Text = prepareTextLateEvent.Text;
			}
		}

		// Token: 0x040037A7 RID: 14247
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(PrepareTextLateEvent), null, new ConversationEvent.EventPoolCount(PrepareTextLateEvent.CountPool), new ConversationEvent.EventPoolReset(PrepareTextLateEvent.ResetPool));

		// Token: 0x040037A8 RID: 14248
		private static List<PrepareTextLateEvent> Pool;

		// Token: 0x040037A9 RID: 14249
		private static int PoolCounter;

		// Token: 0x040037AA RID: 14250
		[ConversationEvent.ParameterAttribute(Reference = true)]
		public string Text;

		// Token: 0x040037AB RID: 14251
		[ConversationEvent.ParameterAttribute(Required = true)]
		public GameObject Subject;

		// Token: 0x040037AC RID: 14252
		[ConversationEvent.ParameterAttribute(Required = true)]
		public GameObject Object;
	}
}
