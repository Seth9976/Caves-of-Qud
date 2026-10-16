using System;
using System.Collections.Generic;
using System.Text;
using XRL.UI;

namespace XRL.World.Skills
{
	// Token: 0x02000BA7 RID: 2983
	[Serializable]
	public class PowerEntryRequirement
	{
		// Token: 0x060076C9 RID: 30409 RVA: 0x002CBAA1 File Offset: 0x002C9CA1
		public PowerEntryRequirement(PowerEntry Entry)
		{
			this.Entry = Entry;
		}

		// Token: 0x060076CA RID: 30410 RVA: 0x002CBAC8 File Offset: 0x002C9CC8
		public bool MeetsRequirement(GameObject Object, bool ShowPopup = false)
		{
			for (int i = 0; i < this.Attributes.Count; i++)
			{
				if (Object.BaseStat(this.Attributes[i], 0) < this.Minimums[i])
				{
					if (ShowPopup)
					{
						Popup.Show(string.Concat(new string[]
						{
							"Your ",
							this.Attributes[i],
							" isn't high enough to buy ",
							this.Entry.Name,
							"!"
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x060076CB RID: 30411 RVA: 0x002CBB68 File Offset: 0x002C9D68
		public void Render(GameObject GO, StringBuilder sb)
		{
			for (int i = 0; i < this.Attributes.Count; i++)
			{
				string text = "R";
				if (GO.BaseStat(this.Attributes[i], 0) >= this.Minimums[i])
				{
					text = "G";
				}
				if (i > 0)
				{
					sb.Append(", ");
				}
				sb.Append("{{C|").Append(this.Minimums[i]).Append("}} {{")
					.Append(text)
					.Append('|')
					.Append((GameManager.IsOnUIContext() && Media.sizeClass <= Media.SizeClass.Small) ? Statistic.GetStatTitleCaseShortNames(this.Attributes[i]) : this.Attributes[i])
					.Append("}}");
			}
		}

		// Token: 0x040036AE RID: 13998
		public PowerEntry Entry;

		// Token: 0x040036AF RID: 13999
		public List<string> Attributes = new List<string>();

		// Token: 0x040036B0 RID: 14000
		public List<int> Minimums = new List<int>();
	}
}
