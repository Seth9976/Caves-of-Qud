using System;
using System.Collections.Generic;
using System.Text;
using XRL.Messages;
using XRL.UI;
using XRL.Wish;

namespace XRL.World
{
	// Token: 0x0200096E RID: 2414
	[HasWishCommand]
	public class StatWishHandler
	{
		// Token: 0x060063D8 RID: 25560 RVA: 0x001EC76C File Offset: 0x001EA96C
		[WishCommand(null, null)]
		public void ClearStatShifts()
		{
			foreach (Statistic statistic in The.Player.Statistics.Values)
			{
				if (statistic.Name != "MP" && statistic.Name != "SP" && statistic.Name != "AP")
				{
					statistic.Penalty = 0;
					statistic.Bonus = 0;
					statistic.Shifts = null;
				}
			}
			MessageQueue.AddPlayerMessage("Clearing player body stat shifts...", null, true);
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x001EC818 File Offset: 0x001EAA18
		[WishCommand(null, null)]
		public void ShowStatShifts()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			foreach (KeyValuePair<string, Statistic> keyValuePair in The.Player.Statistics)
			{
				if (keyValuePair.Value.Shifts != null)
				{
					stringBuilder.Append(keyValuePair.Key);
					stringBuilder.Append(":");
					foreach (Statistic.StatShift statShift in keyValuePair.Value.Shifts)
					{
						stringBuilder.AppendLine();
						stringBuilder.Append("  ");
						if (statShift.Amount > 0)
						{
							stringBuilder.Append('+');
						}
						stringBuilder.Append(statShift.Amount);
						stringBuilder.Append(" from ");
						stringBuilder.Append(statShift.DisplayName);
					}
					stringBuilder.AppendLine();
				}
			}
			Popup.Show(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
		}
	}
}
