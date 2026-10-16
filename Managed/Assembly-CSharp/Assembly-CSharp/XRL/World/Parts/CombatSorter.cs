using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E57 RID: 3671
	public class CombatSorter : Comparer<GameObject>
	{
		// Token: 0x0600948B RID: 38027 RVA: 0x00358181 File Offset: 0x00356381
		public CombatSorter(GameObject POV)
		{
			this.POV = POV;
		}

		// Token: 0x0600948C RID: 38028 RVA: 0x00358190 File Offset: 0x00356390
		public override int Compare(GameObject x, GameObject y)
		{
			bool flag = x == this.POV;
			int num = flag.CompareTo(y == this.POV);
			if (num != 0)
			{
				return num;
			}
			flag = x.HasEffect<Engulfed>();
			int num2 = flag.CompareTo(y.HasEffect<Engulfed>());
			if (num2 != 0)
			{
				return num2;
			}
			if (this.POV != null && !this.POV.IsPlayerControlled())
			{
				flag = x.IsPlayer();
				int num3 = flag.CompareTo(y.IsPlayer());
				if (num3 != 0)
				{
					return -num3;
				}
			}
			flag = x.IsHostileTowards(this.POV);
			int num4 = flag.CompareTo(y.IsHostileTowards(this.POV));
			if (num4 != 0)
			{
				return -num4;
			}
			flag = x.IsCombat;
			int num5 = flag.CompareTo(y.IsCombat);
			if (num5 != 0)
			{
				return -num5;
			}
			flag = x.HasPart<NoDamage>();
			int num6 = flag.CompareTo(y.HasPart<NoDamage>());
			if (num6 != 0)
			{
				return num6;
			}
			flag = x.HasStat("Hitpoints");
			int num7 = flag.CompareTo(y.HasStat("Hitpoints"));
			if (num7 != 0)
			{
				return -num7;
			}
			flag = x.PhaseAndFlightMatches(this.POV);
			int num8 = flag.CompareTo(y.PhaseAndFlightMatches(this.POV));
			if (num8 != 0)
			{
				return -num8;
			}
			flag = x.Physics != null && x.Physics.IsReal;
			int num9 = flag.CompareTo(y.Physics != null && y.Physics.IsReal);
			if (num9 != 0)
			{
				return -num9;
			}
			int num10 = ((x.Render != null) ? x.Render.RenderLayer : 0).CompareTo((y.Render != null) ? y.Render.RenderLayer : 0);
			if (num10 != 0)
			{
				return -num10;
			}
			return 0;
		}

		// Token: 0x04003D73 RID: 15731
		private GameObject POV;
	}
}
