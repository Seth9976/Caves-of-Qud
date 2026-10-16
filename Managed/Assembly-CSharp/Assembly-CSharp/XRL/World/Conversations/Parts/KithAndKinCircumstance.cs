using System;
using Qud.API;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C06 RID: 3078
	public class KithAndKinCircumstance : IKithAndKinPart
	{
		// Token: 0x06007B99 RID: 31641 RVA: 0x002E6881 File Offset: 0x002E4A81
		public override bool LoadChild(ConversationXMLBlueprint Blueprint)
		{
			this.Blueprint = Blueprint;
			return base.LoadChild(Blueprint);
		}

		// Token: 0x06007B9A RID: 31642 RVA: 0x002E6894 File Offset: 0x002E4A94
		public override void Awake()
		{
			base.Circumstances = KithAndKinGameState.Instance.getKnownFreeClues();
			foreach (JournalObservation journalObservation in base.Circumstances)
			{
				this.Blueprint.ID = journalObservation.ID;
				this.Blueprint.ReplaceText(journalObservation.Text);
				this.ParentElement.LoadChild(this.Blueprint);
			}
		}

		// Token: 0x06007B9B RID: 31643 RVA: 0x002E6928 File Offset: 0x002E4B28
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == LeftElementEvent.ID;
		}

		// Token: 0x06007B9C RID: 31644 RVA: 0x002E693E File Offset: 0x002E4B3E
		public override bool HandleEvent(LeftElementEvent E)
		{
			base.Circumstance = base.Circumstances.Find((JournalObservation x) => x.ID == IConversationPart.LastChoiceID);
			return base.HandleEvent(E);
		}

		// Token: 0x04003833 RID: 14387
		public ConversationXMLBlueprint Blueprint;
	}
}
