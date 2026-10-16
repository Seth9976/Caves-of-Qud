using System;
using System.Collections.Generic;
using XRL.Collections;
using XRL.Core;
using XRL.World;

namespace Qud.API
{
	// Token: 0x0200040B RID: 1035
	public static class QuestsAPI
	{
		// Token: 0x0600275B RID: 10075 RVA: 0x000CC448 File Offset: 0x000CA648
		public static Quest fabricateEmptyQuest()
		{
			return new Quest
			{
				ID = Guid.NewGuid().ToString()
			};
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x000CC473 File Offset: 0x000CA673
		public static IEnumerable<Quest> allQuests()
		{
			foreach (Quest quest in XRLCore.Core.Game.Quests.Values)
			{
				yield return quest;
			}
			StringMap<Quest>.ValueEnumerator valueEnumerator = default(StringMap<Quest>.ValueEnumerator);
			yield break;
			yield break;
		}
	}
}
