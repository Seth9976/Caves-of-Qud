using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020004FF RID: 1279
	public interface ITextSelection
	{
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060027B2 RID: 10162
		// (set) Token: 0x060027B3 RID: 10163
		bool isSelectable { get; set; }

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060027B4 RID: 10164
		// (set) Token: 0x060027B5 RID: 10165
		[Obsolete("cursorColor is deprecated. Please use the corresponding USS property (--unity-cursor-color) instead.")]
		Color cursorColor { get; set; }

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060027B6 RID: 10166
		// (set) Token: 0x060027B7 RID: 10167
		[Obsolete("selectionColor is deprecated. Please use the corresponding USS property (--unity-selection-color) instead.")]
		Color selectionColor { get; set; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060027B8 RID: 10168
		// (set) Token: 0x060027B9 RID: 10169
		int cursorIndex { get; set; }

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060027BA RID: 10170
		// (set) Token: 0x060027BB RID: 10171
		bool doubleClickSelectsWord { get; set; }

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060027BC RID: 10172
		// (set) Token: 0x060027BD RID: 10173
		int selectIndex { get; set; }

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060027BE RID: 10174
		// (set) Token: 0x060027BF RID: 10175
		bool tripleClickSelectsLine { get; set; }

		// Token: 0x060027C0 RID: 10176
		bool HasSelection();

		// Token: 0x060027C1 RID: 10177
		void SelectAll();

		// Token: 0x060027C2 RID: 10178
		void SelectNone();

		// Token: 0x060027C3 RID: 10179
		void SelectRange(int cursorIndex, int selectionIndex);

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060027C4 RID: 10180
		// (set) Token: 0x060027C5 RID: 10181
		bool selectAllOnFocus { get; set; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060027C6 RID: 10182
		// (set) Token: 0x060027C7 RID: 10183
		bool selectAllOnMouseUp { get; set; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060027C8 RID: 10184
		Vector2 cursorPosition { get; }

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060027C9 RID: 10185
		float lineHeightAtCursorPosition { get; }

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060027CA RID: 10186
		// (set) Token: 0x060027CB RID: 10187
		float cursorWidth { get; set; }

		// Token: 0x060027CC RID: 10188
		void MoveTextEnd();
	}
}
