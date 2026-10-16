using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AEC RID: 2796
	public class PlaceRelicBuilder
	{
		// Token: 0x0600701E RID: 28702 RVA: 0x0027BE40 File Offset: 0x0027A040
		public bool BuildZoneWithRelic(Zone Z, GameObject relic)
		{
			GameObject objectWithTag = Z.GetObjectWithTag("RelicContainer");
			Inventory inventory = ((objectWithTag != null) ? objectWithTag.Inventory : null);
			if (inventory == null)
			{
				int num = Stat.Random(1, 3);
				for (;;)
				{
					if (num == 1)
					{
						foreach (GameObject gameObject in Z.GetObjectsWithPart("Container").Shuffle<GameObject>(null))
						{
							if (gameObject.HasPart<Inventory>())
							{
								gameObject.Inventory.AddObject(relic, null, false, false, true, null);
								gameObject.SetImportant(true, true, false);
								return true;
							}
						}
					}
					if (num == 1 || num == 2)
					{
						foreach (GameObject gameObject2 in Z.GetObjectsWithPart("Brain").Shuffle<GameObject>(null))
						{
							if (gameObject2.HasPart<Inventory>() && gameObject2.IsCombat && !gameObject2.IsTemporary)
							{
								gameObject2.Inventory.AddObject(relic, null, false, false, true, null);
								gameObject2.SetImportant(true, true, false);
								return true;
							}
						}
					}
					using (List<Cell>.Enumerator enumerator2 = Z.GetEmptyReachableCells().Shuffle<Cell>(null).GetEnumerator())
					{
						if (enumerator2.MoveNext())
						{
							enumerator2.Current.AddObject(relic, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							return true;
						}
					}
					if (num == 3)
					{
						num = 1;
					}
					else
					{
						if (num != 2)
						{
							break;
						}
						num = 1;
					}
				}
				return true;
			}
			inventory.AddObject(relic, null, false, false, true, null);
			inventory.ParentObject.SetImportant(true, true, false);
			if (!this.AddCreditWedges)
			{
				return true;
			}
			switch (Z.NewTier)
			{
			case 0:
			case 1:
			case 2:
				inventory.AddObject("CyberneticsCreditWedge2", false, false);
				break;
			case 3:
				inventory.AddObject("CyberneticsCreditWedge3", false, false);
				break;
			case 4:
			case 5:
				inventory.AddObject("CyberneticsCreditWedge2", false, false);
				inventory.AddObject("CyberneticsCreditWedge2", false, false);
				break;
			case 6:
			case 7:
				inventory.AddObject("CyberneticsCreditWedge3", false, false);
				inventory.AddObject("CyberneticsCreditWedge3", false, false);
				break;
			case 8:
				inventory.AddObject("CyberneticsCreditWedge3", false, false);
				inventory.AddObject("CyberneticsCreditWedge3", false, false);
				inventory.AddObject("CyberneticsCreditWedge2", false, false);
				break;
			}
			return true;
		}

		// Token: 0x0600701F RID: 28703 RVA: 0x0027C0D8 File Offset: 0x0027A2D8
		public bool BuildZone(Zone Z)
		{
			if (!this.Relic.IsNullOrEmpty())
			{
				return this.BuildZoneWithRelic(Z, The.ZoneManager.GetCachedObjects(this.Relic));
			}
			return this.Blueprint.IsNullOrEmpty() || this.BuildZoneWithRelic(Z, GameObject.Create(this.Blueprint));
		}

		// Token: 0x040033AF RID: 13231
		public string Relic;

		// Token: 0x040033B0 RID: 13232
		public string Blueprint;

		// Token: 0x040033B1 RID: 13233
		public bool AddCreditWedges = true;
	}
}
