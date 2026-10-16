using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A9 RID: 425
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public interface IValueField<T>
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000D42 RID: 3394
		// (set) Token: 0x06000D43 RID: 3395
		T value { get; set; }

		// Token: 0x06000D44 RID: 3396
		void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, T startValue);

		// Token: 0x06000D45 RID: 3397
		void StartDragging();

		// Token: 0x06000D46 RID: 3398
		void StopDragging();
	}
}
