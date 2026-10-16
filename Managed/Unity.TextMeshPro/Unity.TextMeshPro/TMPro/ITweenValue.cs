using System;

namespace TMPro
{
	// Token: 0x0200001D RID: 29
	internal interface ITweenValue
	{
		// Token: 0x06000084 RID: 132
		void TweenValue(float floatPercentage);

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000085 RID: 133
		bool ignoreTimeScale { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000086 RID: 134
		float duration { get; }

		// Token: 0x06000087 RID: 135
		bool ValidTarget();
	}
}
