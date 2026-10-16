using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000684 RID: 1668
	internal class VectorImageManager : IDisposable
	{
		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x000B7A94 File Offset: 0x000B5C94
		public Texture2D atlas
		{
			get
			{
				GradientSettingsAtlas gradientSettingsAtlas = this.m_GradientSettingsAtlas;
				return (gradientSettingsAtlas != null) ? gradientSettingsAtlas.atlas : null;
			}
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x000B7AB8 File Offset: 0x000B5CB8
		public VectorImageManager(AtlasBase atlas)
		{
			VectorImageManager.instances.Add(this);
			this.m_Atlas = atlas;
			this.m_Registered = new Dictionary<VectorImage, VectorImageRenderInfo>(32);
			this.m_RenderInfoPool = new VectorImageRenderInfoPool();
			this.m_GradientRemapPool = new GradientRemapPool();
			this.m_GradientSettingsAtlas = new GradientSettingsAtlas(4096);
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x000B7B13 File Offset: 0x000B5D13
		// (set) Token: 0x06003080 RID: 12416 RVA: 0x000B7B1B File Offset: 0x000B5D1B
		private protected bool disposed { protected get; private set; }

		// Token: 0x06003081 RID: 12417 RVA: 0x000B7B24 File Offset: 0x000B5D24
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000B7B38 File Offset: 0x000B5D38
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_Registered.Clear();
					this.m_RenderInfoPool.Clear();
					this.m_GradientRemapPool.Clear();
					this.m_GradientSettingsAtlas.Dispose();
					VectorImageManager.instances.Remove(this);
				}
				this.disposed = true;
			}
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000B7BA0 File Offset: 0x000B5DA0
		public void Reset()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_Registered.Clear();
				this.m_RenderInfoPool.Clear();
				this.m_GradientRemapPool.Clear();
				this.m_GradientSettingsAtlas.Reset();
			}
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000B7BF4 File Offset: 0x000B5DF4
		public void Commit()
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_GradientSettingsAtlas.Commit();
			}
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000B7C24 File Offset: 0x000B5E24
		public GradientRemap AddUser(VectorImage vi, VisualElement context)
		{
			bool disposed = this.disposed;
			GradientRemap gradientRemap;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				gradientRemap = null;
			}
			else
			{
				bool flag = vi == null;
				if (flag)
				{
					gradientRemap = null;
				}
				else
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, out vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount++;
					}
					else
					{
						vectorImageRenderInfo = this.Register(vi, context);
					}
					gradientRemap = vectorImageRenderInfo.firstGradientRemap;
				}
			}
			return gradientRemap;
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000B7C90 File Offset: 0x000B5E90
		public void RemoveUser(VectorImage vi)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = vi == null;
				if (!flag)
				{
					VectorImageRenderInfo vectorImageRenderInfo;
					bool flag2 = this.m_Registered.TryGetValue(vi, out vectorImageRenderInfo);
					if (flag2)
					{
						vectorImageRenderInfo.useCount--;
						bool flag3 = vectorImageRenderInfo.useCount == 0;
						if (flag3)
						{
							this.Unregister(vi, vectorImageRenderInfo);
						}
					}
				}
			}
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000B7CFC File Offset: 0x000B5EFC
		private VectorImageRenderInfo Register(VectorImage vi, VisualElement context)
		{
			VectorImageRenderInfo vectorImageRenderInfo = this.m_RenderInfoPool.Get();
			vectorImageRenderInfo.useCount = 1;
			this.m_Registered[vi] = vectorImageRenderInfo;
			GradientSettings[] settings = vi.settings;
			bool flag = settings != null && settings.Length != 0;
			if (flag)
			{
				int num = vi.settings.Length;
				Alloc alloc = this.m_GradientSettingsAtlas.Add(num);
				bool flag2 = alloc.size > 0U;
				if (flag2)
				{
					TextureId textureId;
					RectInt rectInt;
					bool flag3 = this.m_Atlas.TryGetAtlas(context, vi.atlas, out textureId, out rectInt);
					if (flag3)
					{
						GradientRemap gradientRemap = null;
						for (int i = 0; i < num; i++)
						{
							GradientRemap gradientRemap2 = this.m_GradientRemapPool.Get();
							bool flag4 = i > 0;
							if (flag4)
							{
								gradientRemap.next = gradientRemap2;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap2;
							}
							gradientRemap = gradientRemap2;
							gradientRemap2.origIndex = i;
							gradientRemap2.destIndex = (int)(alloc.start + (uint)i);
							GradientSettings gradientSettings = vi.settings[i];
							RectInt location = gradientSettings.location;
							location.x += rectInt.x;
							location.y += rectInt.y;
							gradientRemap2.location = location;
							gradientRemap2.atlas = textureId;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, vectorImageRenderInfo.firstGradientRemap);
					}
					else
					{
						GradientRemap gradientRemap3 = null;
						for (int j = 0; j < num; j++)
						{
							GradientRemap gradientRemap4 = this.m_GradientRemapPool.Get();
							bool flag5 = j > 0;
							if (flag5)
							{
								gradientRemap3.next = gradientRemap4;
							}
							else
							{
								vectorImageRenderInfo.firstGradientRemap = gradientRemap4;
							}
							gradientRemap3 = gradientRemap4;
							gradientRemap4.origIndex = j;
							gradientRemap4.destIndex = (int)(alloc.start + (uint)j);
							gradientRemap4.atlas = TextureId.invalid;
						}
						this.m_GradientSettingsAtlas.Write(alloc, vi.settings, null);
					}
				}
				else
				{
					bool flag6 = !this.m_LoggedExhaustedSettingsAtlas;
					if (flag6)
					{
						string text = "Exhausted max gradient settings (";
						string text2 = this.m_GradientSettingsAtlas.length.ToString();
						string text3 = ") for atlas: ";
						Texture2D atlas = this.m_GradientSettingsAtlas.atlas;
						Debug.LogError(text + text2 + text3 + ((atlas != null) ? atlas.name : null));
						this.m_LoggedExhaustedSettingsAtlas = true;
					}
				}
			}
			return vectorImageRenderInfo;
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000B7F58 File Offset: 0x000B6158
		private void Unregister(VectorImage vi, VectorImageRenderInfo renderInfo)
		{
			bool flag = renderInfo.gradientSettingsAlloc.size > 0U;
			if (flag)
			{
				this.m_GradientSettingsAtlas.Remove(renderInfo.gradientSettingsAlloc);
			}
			GradientRemap next;
			for (GradientRemap gradientRemap = renderInfo.firstGradientRemap; gradientRemap != null; gradientRemap = next)
			{
				next = gradientRemap.next;
				this.m_GradientRemapPool.Return(gradientRemap);
			}
			this.m_Registered.Remove(vi);
			this.m_RenderInfoPool.Return(renderInfo);
		}

		// Token: 0x040017D7 RID: 6103
		public static List<VectorImageManager> instances = new List<VectorImageManager>(16);

		// Token: 0x040017D8 RID: 6104
		private static ProfilerMarker s_MarkerRegister = new ProfilerMarker("UIR.VectorImageManager.Register");

		// Token: 0x040017D9 RID: 6105
		private static ProfilerMarker s_MarkerUnregister = new ProfilerMarker("UIR.VectorImageManager.Unregister");

		// Token: 0x040017DA RID: 6106
		private readonly AtlasBase m_Atlas;

		// Token: 0x040017DB RID: 6107
		private Dictionary<VectorImage, VectorImageRenderInfo> m_Registered;

		// Token: 0x040017DC RID: 6108
		private VectorImageRenderInfoPool m_RenderInfoPool;

		// Token: 0x040017DD RID: 6109
		private GradientRemapPool m_GradientRemapPool;

		// Token: 0x040017DE RID: 6110
		private GradientSettingsAtlas m_GradientSettingsAtlas;

		// Token: 0x040017DF RID: 6111
		private bool m_LoggedExhaustedSettingsAtlas;
	}
}
