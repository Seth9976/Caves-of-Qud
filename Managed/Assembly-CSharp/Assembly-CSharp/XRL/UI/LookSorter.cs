using System;
using System.Collections.Generic;
using XRL.World;
using XRL.World.Parts;

namespace XRL.UI
{
	// Token: 0x020014A6 RID: 5286
	public class LookSorter : Comparer<GameObject>
	{
		// Token: 0x0600CF8E RID: 53134 RVA: 0x004C2114 File Offset: 0x004C0314
		public override int Compare(GameObject x, GameObject y)
		{
			bool flag = x.HasPart<TerrainTravel>();
			int num = flag.CompareTo(y.HasPart<TerrainTravel>());
			if (num != 0)
			{
				return -num;
			}
			flag = x.ConsiderSolidInRenderingContext();
			int num2 = flag.CompareTo(y.ConsiderSolidInRenderingContext());
			if (num2 != 0)
			{
				return -num2;
			}
			flag = x.IsPlayer();
			int num3 = flag.CompareTo(y.IsPlayer());
			if (num3 != 0)
			{
				return -num3;
			}
			flag = x.IsCombatObject();
			int num4 = flag.CompareTo(y.IsCombatObject());
			if (num4 != 0)
			{
				return -num4;
			}
			flag = x.HasPart<SultanShrine>();
			int num5 = flag.CompareTo(y.HasPart<SultanShrine>());
			if (num5 != 0)
			{
				return -num5;
			}
			flag = x.HasPart<ModPainted>() || x.HasPart<ModEngraved>() || x.HasPart<Tombstone>();
			int num6 = flag.CompareTo(y.HasPart<ModPainted>() || y.HasPart<ModEngraved>() || y.HasPart<Tombstone>());
			if (num6 != 0)
			{
				return -num6;
			}
			Render render = x.Render;
			int? num7;
			if (render == null)
			{
				num7 = null;
			}
			else
			{
				Render render2 = y.Render;
				num7 = new int?(render.RenderLayer.CompareTo((render2 != null) ? new int?(render2.RenderLayer) : null));
			}
			int? num8 = num7;
			int? num9 = num8;
			int num10 = 0;
			if (!((num9.GetValueOrDefault() == num10) & (num9 != null)) && num8 != null)
			{
				return (-num8).Value;
			}
			return 0;
		}
	}
}
