using System;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005DA RID: 1498
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal interface IVisualTreeUpdater : IDisposable
	{
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06002C2B RID: 11307
		// (set) Token: 0x06002C2C RID: 11308
		long FrameCount { get; set; }

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06002C2D RID: 11309
		// (set) Token: 0x06002C2E RID: 11310
		BaseVisualElementPanel panel { get; set; }

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002C2F RID: 11311
		ProfilerMarker profilerMarker { get; }

		// Token: 0x06002C30 RID: 11312
		void Update();

		// Token: 0x06002C31 RID: 11313
		void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);
	}
}
