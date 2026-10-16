using System;
using Qud.API;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02000E01 RID: 3585
	[Serializable]
	public class AddObservation : IPart
	{
		// Token: 0x060090F4 RID: 37108 RVA: 0x0034392E File Offset: 0x00341B2E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060090F5 RID: 37109 RVA: 0x00343931 File Offset: 0x00341B31
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ObjectCreated");
			base.Register(Object, Registrar);
		}

		// Token: 0x060090F6 RID: 37110 RVA: 0x00343948 File Offset: 0x00341B48
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "ObjectCreated") || XRLCore.Core.Game == null)
			{
				return base.FireEvent(E);
			}
			if (this.bCreated)
			{
				return true;
			}
			this.bCreated = true;
			if (this.ID == null)
			{
				this.ID = Guid.NewGuid().ToString();
			}
			JournalAPI.AddObservation(this.Text, this.ID, this.Category, this.ID, this.Attributes.Split(',', StringSplitOptions.None), false, -1L, null, false, true);
			return true;
		}

		// Token: 0x04003C74 RID: 15476
		public string Text;

		// Token: 0x04003C75 RID: 15477
		public string Category = "Gossip";

		// Token: 0x04003C76 RID: 15478
		public string Attributes = "";

		// Token: 0x04003C77 RID: 15479
		public string ID;

		// Token: 0x04003C78 RID: 15480
		public bool bCreated;
	}
}
