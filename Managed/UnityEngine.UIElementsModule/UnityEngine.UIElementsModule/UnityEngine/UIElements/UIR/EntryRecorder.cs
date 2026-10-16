using System;
using System.Runtime.CompilerServices;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200063F RID: 1599
	internal class EntryRecorder
	{
		// Token: 0x06002EE4 RID: 12004 RVA: 0x000AB1CC File Offset: 0x000A93CC
		public EntryRecorder(EntryPool entryPool)
		{
			Debug.Assert(entryPool != null);
			this.m_EntryPool = entryPool;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000AB1E7 File Offset: 0x000A93E7
		public void DrawMesh(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices)
		{
			this.DrawMesh(parentEntry, vertices, indices, null, false);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000AB1F8 File Offset: 0x000A93F8
		public void DrawMesh(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture, bool skipAtlas)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.vertices = vertices;
			entry.indices = indices;
			entry.texture = texture;
			bool flag = texture == null;
			if (flag)
			{
				entry.type = EntryType.DrawSolidMesh;
			}
			else
			{
				entry.type = (skipAtlas ? EntryType.DrawTexturedMeshSkipAtlas : EntryType.DrawTexturedMesh);
			}
			EntryRecorder.AppendMeshEntry(parentEntry, entry);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x000AB250 File Offset: 0x000A9450
		public void DrawRasterText(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture, bool multiChannel)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = (multiChannel ? EntryType.DrawTexturedMeshSkipAtlas : EntryType.DrawTextMesh);
			entry.flags = EntryFlags.UsesTextCoreSettings;
			entry.vertices = vertices;
			entry.indices = indices;
			entry.texture = texture;
			entry.textScale = 0f;
			entry.fontSharpness = 0f;
			EntryRecorder.AppendMeshEntry(parentEntry, entry);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x000AB2B4 File Offset: 0x000A94B4
		public void DrawSdfText(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, Texture texture, float scale, float sharpness)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.DrawTextMesh;
			entry.flags = EntryFlags.UsesTextCoreSettings;
			entry.vertices = vertices;
			entry.indices = indices;
			entry.texture = texture;
			entry.textScale = scale;
			entry.fontSharpness = sharpness;
			EntryRecorder.AppendMeshEntry(parentEntry, entry);
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x000AB30C File Offset: 0x000A950C
		public void DrawGradients(Entry parentEntry, NativeSlice<Vertex> vertices, NativeSlice<ushort> indices, VectorImage gradientsOwner)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.DrawGradients;
			entry.vertices = vertices;
			entry.indices = indices;
			entry.gradientsOwner = gradientsOwner;
			EntryRecorder.AppendMeshEntry(parentEntry, entry);
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x000AB34C File Offset: 0x000A954C
		public void DrawImmediate(Entry parentEntry, Action callback, bool cullingEnabled)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = (cullingEnabled ? EntryType.DrawImmediateCull : EntryType.DrawImmediate);
			entry.immediateCallback = callback;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x000AB384 File Offset: 0x000A9584
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void DrawChildren(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.DrawChildren;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x000AB3B0 File Offset: 0x000A95B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void BeginStencilMask(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.BeginStencilMask;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000AB3DC File Offset: 0x000A95DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void EndStencilMask(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.EndStencilMask;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000AB408 File Offset: 0x000A9608
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopStencilMask(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopStencilMask;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x000AB434 File Offset: 0x000A9634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushClippingRect(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushClippingRect;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000AB460 File Offset: 0x000A9660
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopClippingRect(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopClippingRect;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000AB48C File Offset: 0x000A968C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushScissors(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushScissors;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000AB4B8 File Offset: 0x000A96B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopScissors(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopScissors;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000AB4E4 File Offset: 0x000A96E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushGroupMatrix(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushGroupMatrix;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x000AB510 File Offset: 0x000A9710
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopGroupMatrix(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopGroupMatrix;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000AB53C File Offset: 0x000A973C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushRenderTexture(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushRenderTexture;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x000AB568 File Offset: 0x000A9768
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void BlitAndPopRenderTexture(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.BlitAndPopRenderTexture;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x000AB594 File Offset: 0x000A9794
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PushDefaultMaterial(Entry parentEntry, Material material)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PushDefaultMaterial;
			entry.material = material;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x000AB5C8 File Offset: 0x000A97C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void PopDefaultMaterial(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.PopDefaultMaterial;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x000AB5F4 File Offset: 0x000A97F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CutRenderChain(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.CutRenderChain;
			EntryRecorder.Append(parentEntry, entry);
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000AB620 File Offset: 0x000A9820
		public Entry InsertPlaceholder(Entry parentEntry)
		{
			Entry entry = this.m_EntryPool.Get();
			entry.type = EntryType.DedicatedPlaceholder;
			EntryRecorder.Append(parentEntry, entry);
			return entry;
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000AB650 File Offset: 0x000A9850
		private static void AppendMeshEntry(Entry parentEntry, Entry entry)
		{
			int length = entry.vertices.Length;
			int length2 = entry.indices.Length;
			bool flag = length == 0;
			if (flag)
			{
				Debug.LogError("Attempting to add an entry without vertices.");
			}
			else
			{
				bool flag2 = (long)length > (long)((ulong)UIRenderDevice.maxVerticesPerPage);
				if (flag2)
				{
					Debug.LogError(string.Format("Attempting to add an entry with {0} vertices. The maximum number of vertices per entry is {1}.", length, UIRenderDevice.maxVerticesPerPage));
				}
				else
				{
					bool flag3 = length2 == 0;
					if (flag3)
					{
						Debug.LogError("Attempting to add an entry without indices.");
					}
					else
					{
						EntryRecorder.Append(parentEntry, entry);
					}
				}
			}
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000AB6E0 File Offset: 0x000A98E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Append(Entry parentEntry, Entry entry)
		{
			bool flag = parentEntry.lastChild == null;
			if (flag)
			{
				Debug.Assert(parentEntry.firstChild == null);
				parentEntry.firstChild = entry;
				parentEntry.lastChild = entry;
			}
			else
			{
				parentEntry.lastChild.nextSibling = entry;
				parentEntry.lastChild = entry;
			}
		}

		// Token: 0x04001619 RID: 5657
		private EntryPool m_EntryPool;
	}
}
