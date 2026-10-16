using System;
using System.Collections.Generic;
using XRL;
using XRL.World.Parts;

namespace Qud.API
{
	// Token: 0x02000404 RID: 1028
	public static class AbilityAPI
	{
		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x000C6168 File Offset: 0x000C4368
		public static int abilityCount
		{
			get
			{
				if (The.Player == null)
				{
					return 0;
				}
				ActivatedAbilities part = The.Player.GetPart<ActivatedAbilities>();
				if (part == null)
				{
					return 0;
				}
				return part.AbilityByGuid.Keys.Count;
			}
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000C61A0 File Offset: 0x000C43A0
		public static ActivatedAbilityEntry GetAbility(int nAbility)
		{
			if (The.Player == null)
			{
				return null;
			}
			ActivatedAbilities part = The.Player.GetPart<ActivatedAbilities>();
			if (part == null)
			{
				return null;
			}
			int num = 0;
			foreach (KeyValuePair<Guid, ActivatedAbilityEntry> keyValuePair in part.AbilityByGuid)
			{
				if (num == nAbility)
				{
					return keyValuePair.Value;
				}
				num++;
			}
			return null;
		}
	}
}
