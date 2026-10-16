using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E66 RID: 3686
	[Serializable]
	public class Alchemist : IPart
	{
		// Token: 0x060094F7 RID: 38135 RVA: 0x0035E010 File Offset: 0x0035C210
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveItemListEvent.ID;
		}

		// Token: 0x060094F8 RID: 38136 RVA: 0x0035E026 File Offset: 0x0035C226
		public override bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			E.Add("AlchemistExplode", 1, null, false, false, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x060094F9 RID: 38137 RVA: 0x0035E040 File Offset: 0x0035C240
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060094FA RID: 38138 RVA: 0x0035E044 File Offset: 0x0035C244
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AlchemistExplode")
			{
				this.ParentObject.Explode(15000, null, "10d10+250", 1f, true, false, false, 0, null);
			}
			return base.FireEvent(E);
		}
	}
}
