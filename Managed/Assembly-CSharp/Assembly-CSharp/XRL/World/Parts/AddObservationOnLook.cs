using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000E00 RID: 3584
	[Serializable]
	public class AddObservationOnLook : IPart
	{
		// Token: 0x060090F0 RID: 37104 RVA: 0x00343882 File Offset: 0x00341A82
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060090F1 RID: 37105 RVA: 0x00343885 File Offset: 0x00341A85
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x060090F2 RID: 37106 RVA: 0x0034389C File Offset: 0x00341A9C
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "AfterLookedAt"))
			{
				return base.FireEvent(E);
			}
			if (this.bLookedAt)
			{
				return true;
			}
			this.bLookedAt = true;
			string text = Guid.NewGuid().ToString();
			JournalAPI.AddObservation(this.Text, text, this.Category, text, this.Attributes.Split(',', StringSplitOptions.None), true, -1L, null, false, true);
			return true;
		}

		// Token: 0x04003C70 RID: 15472
		public string Text;

		// Token: 0x04003C71 RID: 15473
		public string Category = "Gossip";

		// Token: 0x04003C72 RID: 15474
		public string Attributes = "";

		// Token: 0x04003C73 RID: 15475
		public bool bLookedAt;
	}
}
