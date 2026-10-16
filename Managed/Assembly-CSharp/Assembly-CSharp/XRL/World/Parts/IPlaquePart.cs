using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001273 RID: 4723
	[Serializable]
	public abstract class IPlaquePart : IPart
	{
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600BB0E RID: 47886 RVA: 0x0044539A File Offset: 0x0044359A
		public virtual string EdgeColor
		{
			get
			{
				return "^k&y";
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600BB0F RID: 47887 RVA: 0x004453A1 File Offset: 0x004435A1
		public virtual string CornerColor
		{
			get
			{
				return "&Y^y";
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600BB10 RID: 47888 RVA: 0x004453A8 File Offset: 0x004435A8
		public virtual string DarkColor
		{
			get
			{
				return "&y^k";
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600BB11 RID: 47889 RVA: 0x004453AF File Offset: 0x004435AF
		public virtual int MaxWidth
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600BB12 RID: 47890
		public abstract string BaseInscription { get; }

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600BB13 RID: 47891 RVA: 0x004453B4 File Offset: 0x004435B4
		public virtual string Inscription
		{
			get
			{
				if (this._Inscription != null)
				{
					return this._Inscription;
				}
				List<string> list = new List<string>(16);
				list.Add("");
				foreach (string text in StringFormat.ClipText(this.BaseInscription, this.MaxWidth, false, true, false).Split('\n', StringSplitOptions.None))
				{
					list.Add(text);
				}
				list.Add("");
				string edgeColor = this.EdgeColor;
				string cornerColor = this.CornerColor;
				string darkColor = this.DarkColor;
				StringBuilder stringBuilder = Event.NewStringBuilder(null).Append('ÿ', 3).Append(cornerColor)
					.Append('\a')
					.Append(edgeColor)
					.Append('Ä', 31)
					.Append(cornerColor)
					.Append('\a')
					.Append(edgeColor);
				for (int j = 0; j < list.Count; j++)
				{
					stringBuilder.Append('\n').Append('ÿ', 3);
					stringBuilder.Append(edgeColor).Append('³').Append(edgeColor);
					int num = ColorUtility.LengthExceptFormatting(list[j]);
					stringBuilder.Append(list[j].PadLeft(16 + num / 2, 'ÿ').PadRight(31, 'ÿ'));
					stringBuilder.Append(edgeColor).Append('³').Append(edgeColor);
				}
				stringBuilder.Append('\n').Append('ÿ', 3).Append(cornerColor)
					.Append('\a')
					.Append(edgeColor)
					.Append(darkColor)
					.Append('Ä', 31)
					.Append(cornerColor)
					.Append('\a')
					.Append(edgeColor);
				return this._Inscription = Event.FinalizeString(stringBuilder);
			}
		}

		// Token: 0x04004B1B RID: 19227
		[NonSerialized]
		protected string _Inscription;
	}
}
