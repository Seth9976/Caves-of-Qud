using System;
using XRL.Rules;
using XRL.World.ObjectBuilders;

namespace XRL.World.Parts
{
	// Token: 0x02000EFA RID: 3834
	[Obsolete("Use XRL.World.ObjectBuilders.Roboticized")]
	[Serializable]
	public class Roboticized : IPart
	{
		// Token: 0x0600990B RID: 39179 RVA: 0x00378A13 File Offset: 0x00376C13
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600990C RID: 39180 RVA: 0x00378A16 File Offset: 0x00376C16
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600990D RID: 39181 RVA: 0x00378A19 File Offset: 0x00376C19
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600990E RID: 39182 RVA: 0x00378A2F File Offset: 0x00376C2F
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (Stat.Random(1, this.ChanceOneIn) == 1)
			{
				Roboticized.Roboticize(this.ParentObject, this.NamePrefix, this.DescriptionPostfix);
			}
			this.ParentObject.RemovePart(this);
			return base.HandleEvent(E);
		}

		// Token: 0x0600990F RID: 39183 RVA: 0x00378A6A File Offset: 0x00376C6A
		public static void Roboticize(GameObject Object, string NamePrefix = "{{c|mechanical}}", string DescriptionPostfix = "There is a low, persistent hum emanating outward.")
		{
			Roboticized.Roboticize(Object, NamePrefix, DescriptionPostfix);
		}

		// Token: 0x04003FC0 RID: 16320
		public const string PREFIX_NAME = "{{c|mechanical}}";

		// Token: 0x04003FC1 RID: 16321
		public const string POSTFIX_DESC = "There is a low, persistent hum emanating outward.";

		// Token: 0x04003FC2 RID: 16322
		public int ChanceOneIn = 10000;

		// Token: 0x04003FC3 RID: 16323
		public string NamePrefix = "{{c|mechanical}}";

		// Token: 0x04003FC4 RID: 16324
		public string DescriptionPostfix = "There is a low, persistent hum emanating outward.";
	}
}
