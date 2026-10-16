using System;
using XRL.UI;
using XRL.World.Parts;
using XRL.World.Quests.GolemQuest;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000BF4 RID: 3060
	public class BuildGolem : IConversationPart
	{
		// Token: 0x06007AFD RID: 31485 RVA: 0x002E3068 File Offset: 0x002E1268
		public override void Awake()
		{
			this.System = The.Game.GetSystem<GolemQuestSystem>();
			if (this.System != null)
			{
				GolemQuestSystem system = this.System;
				GameObject firstObjectWithPart = The.Speaker.CurrentZone.GetFirstObjectWithPart("GolemQuestMound");
				system.Mound = ((firstObjectWithPart != null) ? firstObjectWithPart.GetPart<GolemQuestMound>() : null);
			}
		}

		// Token: 0x06007AFE RID: 31486 RVA: 0x002E30B8 File Offset: 0x002E12B8
		public override void Dispose()
		{
			if (this.System != null)
			{
				this.System.Mound = null;
				this.System = null;
			}
		}

		// Token: 0x06007AFF RID: 31487 RVA: 0x002E30D5 File Offset: 0x002E12D5
		public override void LoadText(string Text)
		{
			this.Message = Text;
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x002E30DE File Offset: 0x002E12DE
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnterElementEvent.ID || ID == EnteredElementEvent.ID || ID == GetChoiceTagEvent.ID;
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x002E3104 File Offset: 0x002E1304
		public override bool HandleEvent(EnterElementEvent E)
		{
			if (this.System == null || !this.System.AllValid())
			{
				if (!this.Message.IsNullOrEmpty())
				{
					Popup.Show(this.Message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x002E3154 File Offset: 0x002E1354
		public override bool HandleEvent(EnteredElementEvent E)
		{
			if (this.System != null)
			{
				this.System.UpdateQuest();
				GolemQuestMound mound = this.System.Mound;
				if (mound != null)
				{
					mound.Build(The.Player, this.TimeDays * 1200);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x002E31A2 File Offset: 0x002E13A2
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = "{{W|[Build Golem]}}";
			return base.HandleEvent(E);
		}

		// Token: 0x04003810 RID: 14352
		public string Message;

		// Token: 0x04003811 RID: 14353
		public int TimeDays;

		// Token: 0x04003812 RID: 14354
		private GolemQuestSystem System;
	}
}
