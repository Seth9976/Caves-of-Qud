using System;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200066D RID: 1645
	internal abstract class BaseShaderInfoStorage : IDisposable
	{
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x0600301D RID: 12317
		public abstract Texture2D texture { get; }

		// Token: 0x0600301E RID: 12318
		public abstract bool AllocateRect(int width, int height, out RectInt uvs);

		// Token: 0x0600301F RID: 12319
		public abstract void SetTexel(int x, int y, Color color);

		// Token: 0x06003020 RID: 12320
		public abstract void UpdateTexture();

		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000B608B File Offset: 0x000B428B
		// (set) Token: 0x06003022 RID: 12322 RVA: 0x000B6093 File Offset: 0x000B4293
		private protected bool disposed { protected get; private set; }

		// Token: 0x06003023 RID: 12323 RVA: 0x000B609C File Offset: 0x000B429C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x000B60B0 File Offset: 0x000B42B0
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				bool flag = !disposing;
				if (flag)
				{
				}
				this.disposed = true;
			}
		}

		// Token: 0x0400177D RID: 6013
		protected static int s_TextureCounter;

		// Token: 0x0400177E RID: 6014
		internal static ProfilerMarker s_MarkerCopyTexture = new ProfilerMarker("UIR.ShaderInfoStorage.CopyTexture");

		// Token: 0x0400177F RID: 6015
		internal static ProfilerMarker s_MarkerGetTextureData = new ProfilerMarker("UIR.ShaderInfoStorage.GetTextureData");

		// Token: 0x04001780 RID: 6016
		internal static ProfilerMarker s_MarkerUpdateTexture = new ProfilerMarker("UIR.ShaderInfoStorage.UpdateTexture");
	}
}
