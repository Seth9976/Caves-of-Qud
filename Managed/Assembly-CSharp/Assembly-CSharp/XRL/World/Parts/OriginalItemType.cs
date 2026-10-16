using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F60 RID: 3936
	[Serializable]
	public class OriginalItemType : IPart
	{
		// Token: 0x06009CE6 RID: 40166 RVA: 0x0038D744 File Offset: 0x0038B944
		public override void Initialize()
		{
			base.Initialize();
			this.DescriptionInject = this.ParentObject.GetDisplayName(int.MaxValue, null, null, true, false, true, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true);
		}

		// Token: 0x06009CE7 RID: 40167 RVA: 0x0038D78B File Offset: 0x0038B98B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009CE8 RID: 40168 RVA: 0x0038D7A1 File Offset: 0x0038B9A1
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.Insert(0, "\nThis item is a named " + this.DescriptionInject + ".\n");
			return base.HandleEvent(E);
		}

		// Token: 0x040040D9 RID: 16601
		public string DescriptionInject;
	}
}
