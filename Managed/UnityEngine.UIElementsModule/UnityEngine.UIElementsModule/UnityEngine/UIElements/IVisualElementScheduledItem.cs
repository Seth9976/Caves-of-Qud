using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005C7 RID: 1479
	public interface IVisualElementScheduledItem
	{
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002BAD RID: 11181
		VisualElement element { get; }

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002BAE RID: 11182
		bool isActive { get; }

		// Token: 0x06002BAF RID: 11183
		void Resume();

		// Token: 0x06002BB0 RID: 11184
		void Pause();

		// Token: 0x06002BB1 RID: 11185
		void ExecuteLater(long delayMs);

		// Token: 0x06002BB2 RID: 11186
		IVisualElementScheduledItem StartingIn(long delayMs);

		// Token: 0x06002BB3 RID: 11187
		IVisualElementScheduledItem Every(long intervalMs);

		// Token: 0x06002BB4 RID: 11188
		IVisualElementScheduledItem Until(Func<bool> stopCondition);

		// Token: 0x06002BB5 RID: 11189
		IVisualElementScheduledItem ForDuration(long durationMs);
	}
}
