using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000E73 RID: 3699
	[Serializable]
	public class Galvanized : IPart
	{
		// Token: 0x06009565 RID: 38245 RVA: 0x00360C2E File Offset: 0x0035EE2E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009566 RID: 38246 RVA: 0x00360C31 File Offset: 0x0035EE31
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GetRustableContents");
		}

		// Token: 0x06009567 RID: 38247 RVA: 0x00360C40 File Offset: 0x0035EE40
		public override bool FireEvent(Event E)
		{
			if (E.ID == "GetRustableContents")
			{
				if (this.Inventory)
				{
					E.SetParameter("Inventory", 0);
				}
				if (this.Equipment)
				{
					E.SetParameter("Equipment", 0);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009568 RID: 38248 RVA: 0x00360C90 File Offset: 0x0035EE90
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009569 RID: 38249 RVA: 0x00360CA6 File Offset: 0x0035EEA6
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600956A RID: 38250 RVA: 0x00360CC8 File Offset: 0x0035EEC8
		public void AppendEffect(StringBuilder SB)
		{
			if (this.Inventory)
			{
				SB.Append("Inventory ");
				if (this.Equipment)
				{
					SB.Append("and equipment ");
				}
			}
			else
			{
				SB.Append("Equipment ");
			}
			SB.Append("immune to rusting");
		}

		// Token: 0x04003DDB RID: 15835
		public bool Inventory = true;

		// Token: 0x04003DDC RID: 15836
		public bool Equipment = true;
	}
}
