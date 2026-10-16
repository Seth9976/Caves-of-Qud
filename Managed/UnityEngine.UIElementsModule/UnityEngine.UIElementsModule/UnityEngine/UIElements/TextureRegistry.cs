using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000333 RID: 819
	internal class TextureRegistry
	{
		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060017C3 RID: 6083 RVA: 0x0005BC75 File Offset: 0x00059E75
		public static TextureRegistry instance { get; } = new TextureRegistry();

		// Token: 0x060017C4 RID: 6084 RVA: 0x0005BC7C File Offset: 0x00059E7C
		public Texture GetTexture(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			Texture texture;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to get an invalid texture (index={0}).", id.index));
				texture = null;
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to get a texture (index={0}) that is not allocated.", id.index));
					texture = null;
				}
				else
				{
					texture = textureInfo.texture;
				}
			}
			return texture;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0005BD20 File Offset: 0x00059F20
		public TextureId AllocAndAcquireDynamic()
		{
			return this.AllocAndAcquire(null, true);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0005BD3C File Offset: 0x00059F3C
		public void UpdateDynamic(TextureId id, Texture texture)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to update an invalid dynamic texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = !textureInfo.dynamic;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to update a texture (index={0}) that is not dynamic.", id.index));
				}
				else
				{
					bool flag3 = textureInfo.refCount < 1;
					if (flag3)
					{
						Debug.LogError(string.Format("Attempted to update a dynamic texture (index={0}) that is not allocated.", id.index));
					}
					else
					{
						textureInfo.texture = texture;
						this.m_Textures[id.index] = textureInfo;
					}
				}
			}
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0005BE1C File Offset: 0x0005A01C
		private TextureId AllocAndAcquire(Texture texture, bool dynamic)
		{
			TextureRegistry.TextureInfo textureInfo = new TextureRegistry.TextureInfo
			{
				texture = texture,
				dynamic = dynamic,
				refCount = 1
			};
			bool flag = this.m_FreeIds.Count > 0;
			TextureId textureId;
			if (flag)
			{
				textureId = this.m_FreeIds.Pop();
				this.m_Textures[textureId.index] = textureInfo;
			}
			else
			{
				bool flag2 = this.m_Textures.Count == 2048;
				if (flag2)
				{
					Debug.LogError(string.Format("Failed to allocate a {0} because the limit of {1} textures is reached.", "TextureId", 2048));
					return TextureId.invalid;
				}
				textureId = new TextureId(this.m_Textures.Count);
				this.m_Textures.Add(textureInfo);
			}
			bool flag3 = !dynamic;
			if (flag3)
			{
				this.m_TextureToId[texture] = textureId;
			}
			return textureId;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0005BF04 File Offset: 0x0005A104
		public TextureId Acquire(Texture tex)
		{
			TextureId textureId;
			bool flag = this.m_TextureToId.TryGetValue(tex, out textureId);
			TextureId textureId2;
			if (flag)
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[textureId.index];
				Debug.Assert(textureInfo.refCount > 0);
				Debug.Assert(!textureInfo.dynamic);
				textureInfo.refCount++;
				this.m_Textures[textureId.index] = textureInfo;
				textureId2 = textureId;
			}
			else
			{
				textureId2 = this.AllocAndAcquire(tex, false);
			}
			return textureId2;
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0005BF88 File Offset: 0x0005A188
		public void Acquire(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to acquire an invalid texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to acquire a texture (index={0}) that is not allocated.", id.index));
				}
				else
				{
					textureInfo.refCount++;
					this.m_Textures[id.index] = textureInfo;
				}
			}
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0005C03C File Offset: 0x0005A23C
		public void Release(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to release an invalid texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to release a texture (index={0}) that is not allocated.", id.index));
				}
				else
				{
					textureInfo.refCount--;
					bool flag3 = textureInfo.refCount == 0;
					if (flag3)
					{
						bool flag4 = !textureInfo.dynamic;
						if (flag4)
						{
							this.m_TextureToId.Remove(textureInfo.texture);
						}
						textureInfo.texture = null;
						textureInfo.dynamic = false;
						this.m_FreeIds.Push(id);
					}
					this.m_Textures[id.index] = textureInfo;
				}
			}
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0005C140 File Offset: 0x0005A340
		public TextureId TextureToId(Texture texture)
		{
			TextureId textureId;
			bool flag = this.m_TextureToId.TryGetValue(texture, out textureId);
			TextureId textureId2;
			if (flag)
			{
				textureId2 = textureId;
			}
			else
			{
				textureId2 = TextureId.invalid;
			}
			return textureId2;
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0005C170 File Offset: 0x0005A370
		public TextureRegistry.Statistics GatherStatistics()
		{
			TextureRegistry.Statistics statistics = default(TextureRegistry.Statistics);
			statistics.freeIdsCount = this.m_FreeIds.Count;
			statistics.createdIdsCount = this.m_Textures.Count;
			statistics.allocatedIdsTotalCount = this.m_Textures.Count - this.m_FreeIds.Count;
			statistics.allocatedIdsDynamicCount = statistics.allocatedIdsTotalCount - this.m_TextureToId.Count;
			statistics.allocatedIdsStaticCount = statistics.allocatedIdsTotalCount - statistics.allocatedIdsDynamicCount;
			statistics.availableIdsCount = 2048 - statistics.allocatedIdsTotalCount;
			return statistics;
		}

		// Token: 0x04000CF7 RID: 3319
		private List<TextureRegistry.TextureInfo> m_Textures = new List<TextureRegistry.TextureInfo>(128);

		// Token: 0x04000CF8 RID: 3320
		private Dictionary<Texture, TextureId> m_TextureToId = new Dictionary<Texture, TextureId>(128);

		// Token: 0x04000CF9 RID: 3321
		private Stack<TextureId> m_FreeIds = new Stack<TextureId>();

		// Token: 0x04000CFA RID: 3322
		internal const int maxTextures = 2048;

		// Token: 0x02000334 RID: 820
		private struct TextureInfo
		{
			// Token: 0x04000CFC RID: 3324
			public Texture texture;

			// Token: 0x04000CFD RID: 3325
			public bool dynamic;

			// Token: 0x04000CFE RID: 3326
			public int refCount;
		}

		// Token: 0x02000335 RID: 821
		public struct Statistics
		{
			// Token: 0x04000CFF RID: 3327
			public int freeIdsCount;

			// Token: 0x04000D00 RID: 3328
			public int createdIdsCount;

			// Token: 0x04000D01 RID: 3329
			public int allocatedIdsTotalCount;

			// Token: 0x04000D02 RID: 3330
			public int allocatedIdsDynamicCount;

			// Token: 0x04000D03 RID: 3331
			public int allocatedIdsStaticCount;

			// Token: 0x04000D04 RID: 3332
			public int availableIdsCount;
		}
	}
}
