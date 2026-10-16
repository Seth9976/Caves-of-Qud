using System;
using System.Collections.Generic;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x0200111F RID: 4383
	[Serializable]
	public class NoteImprovedRateOfFire : IPart
	{
		// Token: 0x0600AD7E RID: 44414 RVA: 0x00403A7E File Offset: 0x00401C7E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AD7F RID: 44415 RVA: 0x00403A94 File Offset: 0x00401C94
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			List<string> list = new List<string>();
			if (this.ShotsAmount != 0)
			{
				list.Add(this.ShotsAmount.Signed(false) + " to " + this.ParentObject.its + " shots per action");
			}
			if (this.AmmoAmount != 0)
			{
				list.Add(this.AmmoAmount.Signed(false) + " to " + this.ParentObject.its + " ammo per action");
			}
			if (this.AmmoCapacityAmount != 0)
			{
				list.Add(this.AmmoCapacityAmount.Signed(false) + " to " + this.ParentObject.its + " ammo capacity");
			}
			if (list.Count > 0)
			{
				E.Postfix.AppendRules(string.Concat(new string[]
				{
					this.ParentObject.IndicativeProximal,
					" ",
					this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
					this.ParentObject.GetVerb("have", true, false, false, true, false),
					" ",
					Grammar.MakeAndList(list, true),
					"."
				}));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040047BE RID: 18366
		public int ShotsAmount;

		// Token: 0x040047BF RID: 18367
		public int AmmoAmount;

		// Token: 0x040047C0 RID: 18368
		public int AmmoCapacityAmount;
	}
}
