using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000352 RID: 850
	public interface ICustomStyle
	{
		// Token: 0x06001930 RID: 6448
		bool TryGetValue(CustomStyleProperty<float> property, out float value);

		// Token: 0x06001931 RID: 6449
		bool TryGetValue(CustomStyleProperty<int> property, out int value);

		// Token: 0x06001932 RID: 6450
		bool TryGetValue(CustomStyleProperty<bool> property, out bool value);

		// Token: 0x06001933 RID: 6451
		bool TryGetValue(CustomStyleProperty<Color> property, out Color value);

		// Token: 0x06001934 RID: 6452
		bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value);

		// Token: 0x06001935 RID: 6453
		bool TryGetValue(CustomStyleProperty<Sprite> property, out Sprite value);

		// Token: 0x06001936 RID: 6454
		bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value);

		// Token: 0x06001937 RID: 6455
		bool TryGetValue<T>(CustomStyleProperty<T> property, out T value) where T : Object;

		// Token: 0x06001938 RID: 6456
		bool TryGetValue(CustomStyleProperty<string> property, out string value);
	}
}
