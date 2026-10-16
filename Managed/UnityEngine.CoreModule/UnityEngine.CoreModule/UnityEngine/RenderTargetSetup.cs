using System;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000178 RID: 376
	public struct RenderTargetSetup
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x00019678 File Offset: 0x00017878
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face, RenderBufferLoadAction[] colorLoad, RenderBufferStoreAction[] colorStore, RenderBufferLoadAction depthLoad, RenderBufferStoreAction depthStore)
		{
			this.color = color;
			this.depth = depth;
			this.mipLevel = mip;
			this.cubemapFace = face;
			this.depthSlice = 0;
			this.colorLoad = colorLoad;
			this.colorStore = colorStore;
			this.depthLoad = depthLoad;
			this.depthStore = depthStore;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x000196CC File Offset: 0x000178CC
		internal static RenderBufferLoadAction[] LoadActions(RenderBuffer[] buf)
		{
			RenderBufferLoadAction[] array = new RenderBufferLoadAction[buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i].loadAction;
				buf[i].loadAction = RenderBufferLoadAction.Load;
			}
			return array;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0001971C File Offset: 0x0001791C
		internal static RenderBufferStoreAction[] StoreActions(RenderBuffer[] buf)
		{
			RenderBufferStoreAction[] array = new RenderBufferStoreAction[buf.Length];
			for (int i = 0; i < buf.Length; i++)
			{
				array[i] = buf[i].storeAction;
				buf[i].storeAction = RenderBufferStoreAction.Store;
			}
			return array;
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00019769 File Offset: 0x00017969
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth)
		{
			this = new RenderTargetSetup(new RenderBuffer[] { color }, depth);
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00019782 File Offset: 0x00017982
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel)
		{
			this = new RenderTargetSetup(new RenderBuffer[] { color }, depth, mipLevel);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0001979C File Offset: 0x0001799C
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face)
		{
			this = new RenderTargetSetup(new RenderBuffer[] { color }, depth, mipLevel, face);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000197B8 File Offset: 0x000179B8
		public RenderTargetSetup(RenderBuffer color, RenderBuffer depth, int mipLevel, CubemapFace face, int depthSlice)
		{
			this = new RenderTargetSetup(new RenderBuffer[] { color }, depth, mipLevel, face);
			this.depthSlice = depthSlice;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000197DC File Offset: 0x000179DC
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth)
		{
			this = new RenderTargetSetup(color, depth, 0, CubemapFace.Unknown);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000197EA File Offset: 0x000179EA
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mipLevel)
		{
			this = new RenderTargetSetup(color, depth, mipLevel, CubemapFace.Unknown);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000197F8 File Offset: 0x000179F8
		public RenderTargetSetup(RenderBuffer[] color, RenderBuffer depth, int mip, CubemapFace face)
		{
			this = new RenderTargetSetup(color, depth, mip, face, RenderTargetSetup.LoadActions(color), RenderTargetSetup.StoreActions(color), depth.loadAction, depth.storeAction);
		}

		// Token: 0x040004CD RID: 1229
		public RenderBuffer[] color;

		// Token: 0x040004CE RID: 1230
		public RenderBuffer depth;

		// Token: 0x040004CF RID: 1231
		public int mipLevel;

		// Token: 0x040004D0 RID: 1232
		public CubemapFace cubemapFace;

		// Token: 0x040004D1 RID: 1233
		public int depthSlice;

		// Token: 0x040004D2 RID: 1234
		public RenderBufferLoadAction[] colorLoad;

		// Token: 0x040004D3 RID: 1235
		public RenderBufferStoreAction[] colorStore;

		// Token: 0x040004D4 RID: 1236
		public RenderBufferLoadAction depthLoad;

		// Token: 0x040004D5 RID: 1237
		public RenderBufferStoreAction depthStore;
	}
}
