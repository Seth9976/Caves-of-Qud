using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BF3 RID: 3059
	public class AskName : IConversationPart
	{
		// Token: 0x06007AF9 RID: 31481 RVA: 0x002E2F82 File Offset: 0x002E1182
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnteredElementEvent.ID || ID == IsElementVisibleEvent.ID;
		}

		// Token: 0x06007AFA RID: 31482 RVA: 0x002E2FA0 File Offset: 0x002E11A0
		public override bool HandleEvent(EnteredElementEvent E)
		{
			GameObject speaker = The.Speaker;
			if (!speaker.HasProperName)
			{
				speaker.GiveProperName(null, false, null, false, null, null, null, null);
				string propertyOrTag = speaker.GetPropertyOrTag("TitleIfNamed", null);
				if (!propertyOrTag.IsNullOrEmpty())
				{
					speaker.RequirePart<Titles>(false).AddTitle(propertyOrTag, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007AFB RID: 31483 RVA: 0x002E3004 File Offset: 0x002E1204
		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			GameObject speaker = The.Speaker;
			return ConversationUI.StartNode.AllowEscape && GlobalConfig.GetBoolSetting("GeneralAskName", false) && speaker.IsCreature && !speaker.HasProperName && !speaker.HasPropertyOrTag("NoAskName") && base.HandleEvent(E);
		}
	}
}
