using System;
using Qud.API;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C0A RID: 3082
	public class KithAndKinMotive : IKithAndKinPart
	{
		// Token: 0x06007BB0 RID: 31664 RVA: 0x002E78A7 File Offset: 0x002E5AA7
		public override bool LoadChild(ConversationXMLBlueprint Blueprint)
		{
			this.Blueprint = Blueprint;
			return base.LoadChild(Blueprint);
		}

		// Token: 0x06007BB1 RID: 31665 RVA: 0x002E78B8 File Offset: 0x002E5AB8
		public override void Awake()
		{
			base.Motives = KithAndKinGameState.Instance.getKnownMotiveClues();
			foreach (JournalObservation journalObservation in base.Motives)
			{
				this.Blueprint.ID = journalObservation.ID;
				this.Blueprint.ReplaceText(journalObservation.Text);
				this.ParentElement.LoadChild(this.Blueprint);
			}
		}

		// Token: 0x06007BB2 RID: 31666 RVA: 0x002E794C File Offset: 0x002E5B4C
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == LeftElementEvent.ID || ID == PrepareTextEvent.ID;
		}

		// Token: 0x06007BB3 RID: 31667 RVA: 0x002E796A File Offset: 0x002E5B6A
		public override bool HandleEvent(LeftElementEvent E)
		{
			base.Motive = base.Motives.Find((JournalObservation x) => x.ID == IConversationPart.LastChoiceID);
			return base.HandleEvent(E);
		}

		// Token: 0x06007BB4 RID: 31668 RVA: 0x002E79A3 File Offset: 0x002E5BA3
		public override bool HandleEvent(PrepareTextEvent E)
		{
			E.Text.Replace("=circumstance.influence=", base.CircumstanceInfluence);
			return base.HandleEvent(E);
		}

		// Token: 0x04003837 RID: 14391
		public ConversationXMLBlueprint Blueprint;
	}
}
