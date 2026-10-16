using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Conversations
{
	/// <summary>Fired before displaying the prepared text to screen.</summary>
	/// <remarks>This is where you will typically add unspoken text like tooltips or other metagame information.</remarks>
	// Token: 0x02000BC9 RID: 3017
	[ConversationEvent(Action = ConversationEvent.Action.Send)]
	public class DisplayTextEvent : ITemplateTextEvent
	{
		// Token: 0x06007942 RID: 31042 RVA: 0x002DC2A2 File Offset: 0x002DA4A2
		public DisplayTextEvent()
			: base(DisplayTextEvent.ID)
		{
		}

		// Token: 0x06007943 RID: 31043 RVA: 0x002DC2AF File Offset: 0x002DA4AF
		public static int CountPool()
		{
			if (DisplayTextEvent.Pool != null)
			{
				return DisplayTextEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06007944 RID: 31044 RVA: 0x002DC2C4 File Offset: 0x002DA4C4
		public static void ResetPool()
		{
			while (DisplayTextEvent.PoolCounter > 0)
			{
				DisplayTextEvent.Pool[--DisplayTextEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x002DC2EC File Offset: 0x002DA4EC
		public static void ResetTo(ref DisplayTextEvent E)
		{
			ConversationEvent.ResetTo<DisplayTextEvent>(E, DisplayTextEvent.Pool, ref DisplayTextEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x002DC302 File Offset: 0x002DA502
		public static DisplayTextEvent FromPool()
		{
			return ConversationEvent.FromPool<DisplayTextEvent>(ref DisplayTextEvent.Pool, ref DisplayTextEvent.PoolCounter, 8192);
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x002DC318 File Offset: 0x002DA518
		public static DisplayTextEvent FromPool(IConversationElement Element, StringBuilder Text, GameObject Subject, GameObject Object)
		{
			DisplayTextEvent displayTextEvent = DisplayTextEvent.FromPool();
			displayTextEvent.Element = Element;
			displayTextEvent.Text = Text;
			displayTextEvent.Subject = Subject;
			displayTextEvent.Object = Object;
			return displayTextEvent;
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x002DC33B File Offset: 0x002DA53B
		public override void Reset()
		{
			base.Reset();
			this.VariableReplace = false;
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x002DC34A File Offset: 0x002DA54A
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x0600794A RID: 31050 RVA: 0x002DC354 File Offset: 0x002DA554
		public static void Send(IConversationElement Element, StringBuilder Text, ref GameObject Subject, ref GameObject Object, out string ExplicitSubject, out bool ExplicitSubjectPlural, out string ExplicitObject, out bool ExplicitObjectPlural, out bool VariableReplace)
		{
			if (Element.WantEvent(DisplayTextEvent.ID))
			{
				DisplayTextEvent displayTextEvent = DisplayTextEvent.FromPool(Element, Text, Subject, Object);
				Element.HandleEvent(displayTextEvent);
				Subject = displayTextEvent.Subject;
				Object = displayTextEvent.Object;
				ExplicitSubject = displayTextEvent.ExplicitSubject;
				ExplicitSubjectPlural = displayTextEvent.ExplicitSubjectPlural;
				ExplicitObject = displayTextEvent.ExplicitObject;
				ExplicitObjectPlural = displayTextEvent.ExplicitObjectPlural;
				VariableReplace = displayTextEvent.VariableReplace;
				return;
			}
			ExplicitSubject = null;
			ExplicitSubjectPlural = false;
			ExplicitObject = null;
			ExplicitObjectPlural = false;
			VariableReplace = false;
		}

		// Token: 0x04003771 RID: 14193
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(DisplayTextEvent), null, new ConversationEvent.EventPoolCount(DisplayTextEvent.CountPool), new ConversationEvent.EventPoolReset(DisplayTextEvent.ResetPool));

		// Token: 0x04003772 RID: 14194
		private static List<DisplayTextEvent> Pool;

		// Token: 0x04003773 RID: 14195
		private static int PoolCounter;

		// Token: 0x04003774 RID: 14196
		[ConversationEvent.ParameterAttribute(Output = true)]
		public bool VariableReplace;
	}
}
