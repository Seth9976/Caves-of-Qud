using System;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02000FEC RID: 4076
	[Serializable]
	public class AddMod : IPart
	{
		// Token: 0x0600A279 RID: 41593 RVA: 0x003B5D6D File Offset: 0x003B3F6D
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600A27A RID: 41594 RVA: 0x003B5D70 File Offset: 0x003B3F70
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A27B RID: 41595 RVA: 0x003B5D88 File Offset: 0x003B3F88
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			string[] array = this.Mods.Split(',', StringSplitOptions.None);
			string[] array2 = this.Tiers.Split(',', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				if (!string.IsNullOrEmpty(array[i]))
				{
					int num = 1;
					if (!string.IsNullOrEmpty(this.Tiers) && array2.Length > i)
					{
						num = Convert.ToInt32(array2[i]);
					}
					if (array[i].Length > 0 && array[i][0] == '@')
					{
						ItemModding.ApplyModificationFromPopulationTable(this.ParentObject, array[i].Substring(1), num, true);
					}
					else
					{
						ItemModding.ApplyModification(this.ParentObject, array[i], num, true, null, true);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A27C RID: 41596 RVA: 0x003B5E34 File Offset: 0x003B4034
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004353 RID: 17235
		public string Mods = "";

		// Token: 0x04004354 RID: 17236
		public string Tiers = "";
	}
}
