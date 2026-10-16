using System;
using Qud.API;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C09 RID: 3081
	public class KithAndKinIfExcluded : IKithAndKinPart
	{
		// Token: 0x06007BAC RID: 31660 RVA: 0x002E77A0 File Offset: 0x002E59A0
		public bool IsExcluded(JournalObservation Motive)
		{
			string text;
			if (!Motive.TryGetAttribute("motive:", out text))
			{
				text = "keh";
			}
			if (base.Eliminated.Contains(text))
			{
				return true;
			}
			IConversationElement parent = this.ParentElement.Parent;
			string[] array = new string[2];
			array[0] = "KithAndKinExclusion";
			int num = 1;
			JournalObservation circumstance = base.Circumstance;
			array[num] = ((circumstance != null) ? circumstance.ID : null);
			IConversationElement element = parent.GetElement(array);
			string text2;
			return element != null && element.Attributes.TryGetValue("Thief", out text2) && IKithAndKinPart.KeyOf(text) == text2;
		}

		// Token: 0x06007BAD RID: 31661 RVA: 0x002E782B File Offset: 0x002E5A2B
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetTargetElementEvent.ID;
		}

		// Token: 0x06007BAE RID: 31662 RVA: 0x002E7844 File Offset: 0x002E5A44
		public override bool HandleEvent(GetTargetElementEvent E)
		{
			JournalObservation journalObservation = base.Motives.Find((JournalObservation x) => x.ID == E.Element.ID);
			if (journalObservation != null && this.IsExcluded(journalObservation))
			{
				E.Target = this.Target;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003836 RID: 14390
		public string Target;
	}
}
