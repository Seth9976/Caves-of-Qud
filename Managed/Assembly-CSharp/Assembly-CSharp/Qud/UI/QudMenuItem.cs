using System;
using ConsoleLib.Console;
using UnityEngine;

namespace Qud.UI
{
	// Token: 0x02000424 RID: 1060
	[Serializable]
	public struct QudMenuItem
	{
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x000D00DC File Offset: 0x000CE2DC
		public string simpleText
		{
			get
			{
				string text = this.text;
				if (text == null)
				{
					return text;
				}
				text = text.Strip();
				if (text.Contains(" ") && text.StartsWith("["))
				{
					text = text.Substring(text.IndexOf(" ") + 1);
				}
				return text.ToLower();
			}
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000D0130 File Offset: 0x000CE330
		public override string ToString()
		{
			return JsonUtility.ToJson(this);
		}

		// Token: 0x0600282B RID: 10283 RVA: 0x000D0142 File Offset: 0x000CE342
		public override int GetHashCode()
		{
			return this.text.GetHashCode() & this.command.GetHashCode() & this.hotkey.GetHashCode();
		}

		// Token: 0x040013FB RID: 5115
		public string text;

		// Token: 0x040013FC RID: 5116
		public string command;

		// Token: 0x040013FD RID: 5117
		public string hotkey;

		// Token: 0x040013FE RID: 5118
		public IRenderable icon;
	}
}
