using System;
using System.Collections.Generic;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	/// Loads severed body parts into the parent object as inventory, as for
	/// example with the wild-eyed watervine merchant and his inventory of
	/// naphtaali and goatfolk body parts.
	// Token: 0x02000E0E RID: 3598
	[Serializable]
	public class BodyPartInventory : IPart
	{
		// Token: 0x0600929C RID: 37532 RVA: 0x0034D0BE File Offset: 0x0034B2BE
		public override bool SameAs(IPart p)
		{
			return !((p as BodyPartInventory).ByInherit != this.ByInherit) && base.SameAs(p);
		}

		// Token: 0x0600929D RID: 37533 RVA: 0x0034D0E1 File Offset: 0x0034B2E1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID;
		}

		// Token: 0x0600929E RID: 37534 RVA: 0x0034D0F8 File Offset: 0x0034B2F8
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (!this.ByInherit.IsNullOrEmpty())
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.ByInherit.CachedDictionaryExpansion())
				{
					string key = keyValuePair.Key;
					string value = keyValuePair.Value;
					try
					{
						BodyPart.MakeSeveredBodyParts(value.RollCached(), null, null, null, key, null, this.ParentObject, null);
					}
					catch
					{
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003CB9 RID: 15545
		public string ByInherit;
	}
}
