using System;
using System.Collections.Generic;
using XRL.World;

namespace XRL.Core
{
	// Token: 0x0200063D RID: 1597
	public class JoinPartyLeaderCommand : IActionCommand, IComposite
	{
		// Token: 0x060042A3 RID: 17059 RVA: 0x00161424 File Offset: 0x0015F624
		public static void Issue()
		{
			ActionManager actionManager = The.ActionManager;
			if (!actionManager.HasAction<JoinPartyLeaderCommand>())
			{
				actionManager.EnqueueAction(JoinPartyLeaderCommand.Instance, 0);
			}
		}

		// Token: 0x060042A4 RID: 17060 RVA: 0x0016144C File Offset: 0x0015F64C
		public void Execute(XRLGame Game, ActionManager Manager)
		{
			int num = 0;
			bool flag = true;
			while (flag && ++num < 100)
			{
				Dictionary<string, Zone> cachedZones = Game.ZoneManager.CachedZones;
				int count = cachedZones.Count;
				flag = false;
				foreach (Zone zone in cachedZones.Values)
				{
					for (int i = 0; i < zone.Height; i++)
					{
						for (int j = 0; j < zone.Width; j++)
						{
							Cell cell = zone.GetCell(j, i);
							int k = 0;
							int num2 = cell.Objects.Count;
							while (k < num2)
							{
								if (cell.Objects[k].GoToPartyLeader())
								{
									flag = true;
									k--;
									num2--;
									if (num2 > cell.Objects.Count)
									{
										num2 = cell.Objects.Count;
									}
								}
								k++;
							}
						}
					}
					if (cachedZones.Count != count)
					{
						break;
					}
				}
			}
		}

		// Token: 0x040020FC RID: 8444
		private static JoinPartyLeaderCommand Instance = new JoinPartyLeaderCommand();
	}
}
