using System;
using Qud.API;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C13 RID: 3091
	public class SecretHandler : IConversationPart
	{
		// Token: 0x06007BE1 RID: 31713 RVA: 0x002E9035 File Offset: 0x002E7235
		public SecretHandler()
		{
		}

		// Token: 0x06007BE2 RID: 31714 RVA: 0x002E903D File Offset: 0x002E723D
		public SecretHandler(string SecretID, bool Require = false, bool Override = false)
			: this()
		{
			this.SecretID = SecretID;
			this.Require = Require;
			this.Override = Override;
		}

		// Token: 0x06007BE3 RID: 31715 RVA: 0x002E905C File Offset: 0x002E725C
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || (ID == IsElementVisibleEvent.ID && !this.Require && !this.Override) || (ID == EnterElementEvent.ID && this.Require) || ID == ColorTextEvent.ID || ID == DisplayTextEvent.ID;
		}

		// Token: 0x06007BE4 RID: 31716 RVA: 0x002E90AD File Offset: 0x002E72AD
		public override bool HandleEvent(IsElementVisibleEvent E)
		{
			return this.Require || this.Override || JournalAPI.HasNote(this.SecretID);
		}

		// Token: 0x06007BE5 RID: 31717 RVA: 0x002E90CC File Offset: 0x002E72CC
		public override bool HandleEvent(EnterElementEvent E)
		{
			return !this.Require || JournalAPI.HasNote(this.SecretID);
		}

		// Token: 0x06007BE6 RID: 31718 RVA: 0x002E90E4 File Offset: 0x002E72E4
		public override bool HandleEvent(ColorTextEvent E)
		{
			if (this.Require && !JournalAPI.HasNote(this.SecretID))
			{
				E.Color = "K";
			}
			else
			{
				Choice choice = E.Element as Choice;
				if (choice != null && choice.Visited)
				{
					E.Color = "g";
				}
				else
				{
					E.Color = "M";
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007BE7 RID: 31719 RVA: 0x002E9149 File Offset: 0x002E7349
		public override bool HandleEvent(DisplayTextEvent E)
		{
			E.Text.Insert(0, "<!> ");
			return base.HandleEvent(E);
		}

		// Token: 0x0400384F RID: 14415
		public string SecretID;

		// Token: 0x04003850 RID: 14416
		public bool Require;

		// Token: 0x04003851 RID: 14417
		public bool Override;
	}
}
