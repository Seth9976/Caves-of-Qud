using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F5F RID: 3935
	[Serializable]
	public class Notes : IPart
	{
		// Token: 0x06009CE0 RID: 40160 RVA: 0x0038D65B File Offset: 0x0038B85B
		public override bool SameAs(IPart p)
		{
			return !((p as Notes).Text != this.Text) && base.SameAs(p);
		}

		// Token: 0x06009CE1 RID: 40161 RVA: 0x0038D67E File Offset: 0x0038B87E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == GetUnknownShortDescriptionEvent.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009CE2 RID: 40162 RVA: 0x0038D6A4 File Offset: 0x0038B8A4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.Text))
			{
				E.Postfix.Append("\n{{playernotes|Notes: ").Append(this.Text).Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009CE3 RID: 40163 RVA: 0x0038D6E0 File Offset: 0x0038B8E0
		public override bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.Text))
			{
				E.Postfix.Append("\n{{playernotes|Notes: ").Append(this.Text).Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009CE4 RID: 40164 RVA: 0x0038D71C File Offset: 0x0038B91C
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040040D8 RID: 16600
		public string Text;
	}
}
