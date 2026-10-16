using System;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	internal sealed class GUIAspectSizer : GUILayoutEntry
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0000F230 File Offset: 0x0000D430
		public GUIAspectSizer(float aspect, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
			this.aspect = aspect;
			this.ApplyOptions(options);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0000F264 File Offset: 0x0000D464
		public override void CalcHeight()
		{
			this.minHeight = (this.maxHeight = this.rect.width / this.aspect);
		}

		// Token: 0x04000110 RID: 272
		private float aspect;
	}
}
