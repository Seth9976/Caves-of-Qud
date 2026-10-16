using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200017A RID: 378
	internal readonly struct RenderInstancedDataLayout
	{
		// Token: 0x06000DD5 RID: 3541 RVA: 0x000199E4 File Offset: 0x00017BE4
		public RenderInstancedDataLayout(Type t)
		{
			this.size = Marshal.SizeOf(t);
			this.offsetObjectToWorld = ((t == typeof(Matrix4x4)) ? 0 : Marshal.OffsetOf(t, "objectToWorld").ToInt32());
			try
			{
				this.offsetPrevObjectToWorld = Marshal.OffsetOf(t, "prevObjectToWorld").ToInt32();
			}
			catch (ArgumentException)
			{
				this.offsetPrevObjectToWorld = -1;
			}
			try
			{
				this.offsetRenderingLayerMask = Marshal.OffsetOf(t, "renderingLayerMask").ToInt32();
			}
			catch (ArgumentException)
			{
				this.offsetRenderingLayerMask = -1;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00019A9C File Offset: 0x00017C9C
		public int size { get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public int offsetObjectToWorld { get; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x00019AAC File Offset: 0x00017CAC
		public int offsetPrevObjectToWorld { get; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00019AB4 File Offset: 0x00017CB4
		public int offsetRenderingLayerMask { get; }
	}
}
