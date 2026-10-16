using System;
using System.Text;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Terminals
{
	// Token: 0x020009BE RID: 2494
	public class CrematoryMachineRoomTerminalWelcome : GenericTerminalScreen
	{
		// Token: 0x06006B2F RID: 27439 RVA: 0x0022887C File Offset: 0x00226A7C
		public CrematoryMachineRoomTerminalWelcome()
		{
			this.mainText = "                            \r\n            .   .          .                                       \r\n      .     |\\  |  o       |                                .      \r\n    __|__   | \\ |  .  .--. |--. .  . .--..--. .-.  .-..   __|__     \r\n      |     |  \\|  |  |  | |  | |  | |   `--.(   )(   |     |       \r\n      '     '   '-' `-'  `-'  `-`--`-'   `--' `-'`-`-`|     '   \r\n                                                   ._.'           \r\n                                                                      ";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.mainText.Length; i++)
			{
				if (Stat.RandomCosmetic(1, 100) <= 2)
				{
					stringBuilder.Append((char)Stat.RandomCosmetic(45, 230));
				}
				else
				{
					stringBuilder.Append(this.mainText[i]);
				}
			}
			this.mainText = stringBuilder.ToString();
			this.Options.Add("...");
		}

		// Token: 0x06006B30 RID: 27440 RVA: 0x00228906 File Offset: 0x00226B06
		public override void Back()
		{
			base.terminal.currentScreen = null;
		}

		// Token: 0x06006B31 RID: 27441 RVA: 0x00228914 File Offset: 0x00226B14
		public override void Activate()
		{
			base.terminal.currentScreen = null;
		}
	}
}
