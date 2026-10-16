using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000288 RID: 648
	public interface IPointerEvent
	{
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060012AB RID: 4779
		int pointerId { get; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060012AC RID: 4780
		string pointerType { get; }

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060012AD RID: 4781
		bool isPrimary { get; }

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060012AE RID: 4782
		int button { get; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060012AF RID: 4783
		int pressedButtons { get; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060012B0 RID: 4784
		Vector3 position { get; }

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060012B1 RID: 4785
		Vector3 localPosition { get; }

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060012B2 RID: 4786
		Vector3 deltaPosition { get; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060012B3 RID: 4787
		float deltaTime { get; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060012B4 RID: 4788
		int clickCount { get; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060012B5 RID: 4789
		float pressure { get; }

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060012B6 RID: 4790
		float tangentialPressure { get; }

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060012B7 RID: 4791
		float altitudeAngle { get; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060012B8 RID: 4792
		float azimuthAngle { get; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060012B9 RID: 4793
		float twist { get; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060012BA RID: 4794
		Vector2 tilt { get; }

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060012BB RID: 4795
		PenStatus penStatus { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060012BC RID: 4796
		Vector2 radius { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060012BD RID: 4797
		Vector2 radiusVariance { get; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060012BE RID: 4798
		EventModifiers modifiers { get; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x060012BF RID: 4799
		bool shiftKey { get; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x060012C0 RID: 4800
		bool ctrlKey { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x060012C1 RID: 4801
		bool commandKey { get; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x060012C2 RID: 4802
		bool altKey { get; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x060012C3 RID: 4803
		bool actionKey { get; }
	}
}
