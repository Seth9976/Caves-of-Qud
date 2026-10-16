using System;
using UnityEngine;

namespace XRL.UI.Framework
{
	/// <summary>
	///   Data Structure for various UI elements to use.
	/// </summary>
	// Token: 0x02001517 RID: 5399
	[Serializable]
	public class ChoiceWithColorIcon : FrameworkDataElement, IFrameworkDataHotkey
	{
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x0600D243 RID: 53827 RVA: 0x004E0E20 File Offset: 0x004DF020
		// (set) Token: 0x0600D244 RID: 53828 RVA: 0x004E0E28 File Offset: 0x004DF028
		public string Hotkey { get; set; }

		// Token: 0x0600D245 RID: 53829 RVA: 0x004E0E31 File Offset: 0x004DF031
		public bool IsChosen()
		{
			return this.Chosen(this);
		}

		// Token: 0x04005358 RID: 21336
		public string Title;

		// Token: 0x0400535A RID: 21338
		public string IconPath;

		// Token: 0x0400535B RID: 21339
		public bool HFlip;

		// Token: 0x0400535C RID: 21340
		public bool VFlip;

		// Token: 0x0400535D RID: 21341
		[NonSerialized]
		public Predicate<ChoiceWithColorIcon> Chosen = (ChoiceWithColorIcon choice) => false;

		// Token: 0x0400535E RID: 21342
		public Color IconForegroundColor;

		// Token: 0x0400535F RID: 21343
		public Color IconDetailColor;
	}
}
