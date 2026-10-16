using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine.Pool;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E8 RID: 1256
	internal class ATGTextJobSystem
	{
		// Token: 0x06002661 RID: 9825 RVA: 0x0008BD9C File Offset: 0x00089F9C
		public ATGTextJobSystem()
		{
			this.m_GenerateTextJobifiedCallback = new MeshGenerationCallback(this.GenerateTextJobified);
			this.m_AddDrawEntriesCallback = new MeshGenerationCallback(this.AddDrawEntries);
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x0008BE18 File Offset: 0x0008A018
		public void GenerateText(MeshGenerationContext mgc, TextElement textElement)
		{
			MeshGenerationNode meshGenerationNode;
			mgc.InsertMeshGenerationNode(out meshGenerationNode);
			ATGTextJobSystem.ManagedJobData managedJobData = ATGTextJobSystem.s_JobDataPool.Get();
			managedJobData.textElement = textElement;
			managedJobData.node = meshGenerationNode;
			this.textJobDatas.Add(managedJobData);
			bool flag = this.hasPendingTextWork;
			if (!flag)
			{
				this.hasPendingTextWork = true;
				this.textJobDatasHandle = GCHandle.Alloc(this.textJobDatas);
				MeshGenerationCallbackType meshGenerationCallbackType = (ATGTextJobSystem.k_IsMultiThreaded ? MeshGenerationCallbackType.Fork : MeshGenerationCallbackType.Work);
				mgc.AddMeshGenerationCallback(this.m_GenerateTextJobifiedCallback, null, meshGenerationCallbackType, false);
			}
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0008BE98 File Offset: 0x0008A098
		private void GenerateTextJobified(MeshGenerationContext mgc, object _)
		{
			ATGTextJobSystem.GenerateTextJobData generateTextJobData = new ATGTextJobSystem.GenerateTextJobData
			{
				managedJobDataHandle = this.textJobDatasHandle
			};
			bool flag = this.textJobDatas.Count > 0;
			if (flag)
			{
				this.textJobDatas[0].textElement.uitkTextHandle.InitTextLib();
			}
			FontAsset.CreateHbFaceIfNeeded();
			for (int i = 0; i < this.textJobDatas.Count; i++)
			{
				ATGTextJobSystem.ManagedJobData managedJobData = this.textJobDatas[i];
				TextElement textElement = managedJobData.textElement;
				FontAsset fontAsset = TextUtilities.GetFontAsset(textElement);
				TextUtilities.GetTextSettingsFrom(textElement).UpdateNativeTextSettings();
				fontAsset.EnsureNativeFontAssetIsCreated();
				bool flag2 = textElement.computedStyle.unityFontDefinition.fontAsset == null;
				if (flag2)
				{
					textElement.uitkTextHandle.ConvertUssToNativeTextGenerationSettings();
				}
			}
			bool flag3 = ATGTextJobSystem.k_IsMultiThreaded;
			if (flag3)
			{
				JobHandle jobHandle = generateTextJobData.ScheduleOrRunJob(this.textJobDatas.Count, 1, default(JobHandle));
				mgc.AddMeshGenerationJob(jobHandle);
			}
			else
			{
				for (int j = 0; j < this.textJobDatas.Count; j++)
				{
					generateTextJobData.Execute(j);
				}
			}
			mgc.AddMeshGenerationCallback(this.m_AddDrawEntriesCallback, null, MeshGenerationCallbackType.Work, true);
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0008BFE4 File Offset: 0x0008A1E4
		private void AddDrawEntries(MeshGenerationContext mgc, object _)
		{
			foreach (ATGTextJobSystem.ManagedJobData managedJobData in this.textJobDatas)
			{
				bool success = managedJobData.success;
				if (success)
				{
					NativeTextInfo textInfo = managedJobData.textInfo;
					managedJobData.textElement.uitkTextHandle.ProcessMeshInfos(textInfo);
					managedJobData.textElement.uitkTextHandle.UpdateATGTextEventHandler();
					FontAsset.UpdateFontAssetsInUpdateQueue();
					TempMeshAllocator tempMeshAllocator;
					mgc.GetTempMeshAllocator(out tempMeshAllocator);
					ATGTextJobSystem.ConvertMeshInfoToUIRVertex(textInfo.meshInfos, tempMeshAllocator, managedJobData.textElement, ref this.atlases, ref this.verticesArray, ref this.indicesArray, ref this.renderModes, ref this.sdfScalesArray);
					mgc.Begin(managedJobData.node.GetParentEntry(), managedJobData.textElement);
					mgc.meshGenerator.DrawText(this.verticesArray, this.indicesArray, this.atlases, this.renderModes, this.sdfScalesArray);
					managedJobData.textElement.OnGenerateTextOverNative(mgc);
					this.atlases.Clear();
					this.verticesArray.Clear();
					this.indicesArray.Clear();
					this.renderModes.Clear();
					this.sdfScalesArray.Clear();
					mgc.End();
				}
				managedJobData.Release();
			}
			this.hasPendingTextWork = false;
			this.textJobDatas.Clear();
			this.textJobDatasHandle.Free();
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0008C17C File Offset: 0x0008A37C
		private static void ConvertMeshInfoToUIRVertex(ATGMeshInfo[] meshInfos, TempMeshAllocator alloc, TextElement visualElement, ref List<Texture2D> atlases, ref List<NativeSlice<Vertex>> verticesArray, ref List<NativeSlice<ushort>> indicesArray, ref List<GlyphRenderMode> renderModes, ref List<float> sdfScales)
		{
			float num = 1f / visualElement.scaledPixelsPerPoint;
			foreach (ATGMeshInfo atgmeshInfo in meshInfos)
			{
				int num2 = (int)((ulong)UIRenderDevice.maxVerticesPerPage & 18446744073709551612UL);
				bool hasMultipleColors = atgmeshInfo.hasMultipleColors;
				bool flag = hasMultipleColors;
				if (flag)
				{
					visualElement.renderChainData.flags = visualElement.renderChainData.flags | RenderDataFlags.IsIgnoringDynamicColorHint;
				}
				else
				{
					visualElement.renderChainData.flags = visualElement.renderChainData.flags & ~RenderDataFlags.IsIgnoringDynamicColorHint;
				}
				for (int j = 0; j < atgmeshInfo.textElementInfoIndicesByAtlas.Count; j++)
				{
					List<int> list = atgmeshInfo.textElementInfoIndicesByAtlas[j];
					int k;
					int num3;
					for (k = list.Count * 4; k > 0; k -= num3)
					{
						num3 = Mathf.Min(k, num2);
						int num4 = num3 >> 2;
						int num5 = num4 * 6;
						FontAsset fontAsset = atgmeshInfo.fontAsset;
						atlases.Add(fontAsset.atlasTextures[j]);
						renderModes.Add(fontAsset.atlasRenderMode);
						float num6 = 0f;
						List<GlyphRenderMode> list2 = renderModes;
						bool flag2;
						if (!TextGeneratorUtilities.IsBitmapRendering(list2[list2.Count - 1]))
						{
							List<Texture2D> list3 = atlases;
							flag2 = list3[list3.Count - 1].format == TextureFormat.Alpha8;
						}
						else
						{
							flag2 = false;
						}
						bool flag3 = flag2;
						if (flag3)
						{
							num6 = (float)(fontAsset.atlasPadding + 1);
						}
						sdfScales.Add(num6);
						bool flag4 = fontAsset.atlasRenderMode != GlyphRenderMode.SMOOTH && fontAsset.atlasRenderMode != GlyphRenderMode.COLOR;
						bool flag5 = !hasMultipleColors && (RenderEvents.NeedsColorID(visualElement) || (flag4 && RenderEvents.NeedsTextCoreSettings(visualElement)));
						NativeSlice<Vertex> nativeSlice;
						NativeSlice<ushort> nativeSlice2;
						alloc.AllocateTempMesh(num3, num5, out nativeSlice, out nativeSlice2);
						Vector2 min = visualElement.contentRect.min;
						int l = 0;
						int num7 = 0;
						int num8 = 0;
						while (l < num3)
						{
							bool flag6 = fontAsset.atlasRenderMode == GlyphRenderMode.COLOR || fontAsset.atlasRenderMode == GlyphRenderMode.COLOR_HINTED;
							NativeTextElementInfo nativeTextElementInfo = atgmeshInfo.textElementInfos[list[num7]];
							nativeSlice[l] = MeshGenerator.ConvertTextVertexToUIRVertex(nativeTextElementInfo.bottomLeft, min, flag5, flag6);
							nativeSlice[l + 1] = MeshGenerator.ConvertTextVertexToUIRVertex(nativeTextElementInfo.topLeft, min, flag5, flag6);
							nativeSlice[l + 2] = MeshGenerator.ConvertTextVertexToUIRVertex(nativeTextElementInfo.topRight, min, flag5, flag6);
							nativeSlice[l + 3] = MeshGenerator.ConvertTextVertexToUIRVertex(nativeTextElementInfo.bottomRight, min, flag5, flag6);
							nativeSlice2[num8] = (ushort)l;
							nativeSlice2[num8 + 1] = (ushort)(l + 1);
							nativeSlice2[num8 + 2] = (ushort)(l + 2);
							nativeSlice2[num8 + 3] = (ushort)(l + 2);
							nativeSlice2[num8 + 4] = (ushort)(l + 3);
							nativeSlice2[num8 + 5] = (ushort)l;
							l += 4;
							num7++;
							num8 += 6;
						}
						verticesArray.Add(nativeSlice);
						indicesArray.Add(nativeSlice2);
					}
					Debug.Assert(k == 0);
				}
			}
		}

		// Token: 0x04001155 RID: 4437
		private GCHandle textJobDatasHandle;

		// Token: 0x04001156 RID: 4438
		private List<ATGTextJobSystem.ManagedJobData> textJobDatas = new List<ATGTextJobSystem.ManagedJobData>();

		// Token: 0x04001157 RID: 4439
		private bool hasPendingTextWork;

		// Token: 0x04001158 RID: 4440
		private static ObjectPool<ATGTextJobSystem.ManagedJobData> s_JobDataPool = new ObjectPool<ATGTextJobSystem.ManagedJobData>(() => new ATGTextJobSystem.ManagedJobData(), null, delegate(ATGTextJobSystem.ManagedJobData inst)
		{
			inst.textElement = null;
		}, null, false, 10, 10000);

		// Token: 0x04001159 RID: 4441
		internal MeshGenerationCallback m_GenerateTextJobifiedCallback;

		// Token: 0x0400115A RID: 4442
		internal MeshGenerationCallback m_AddDrawEntriesCallback;

		// Token: 0x0400115B RID: 4443
		private static readonly ProfilerMarker k_GenerateTextMarker = new ProfilerMarker("ATGTextJob.GenerateText");

		// Token: 0x0400115C RID: 4444
		private static readonly ProfilerMarker k_ATGTextJobMarker = new ProfilerMarker("ATGTextJob");

		// Token: 0x0400115D RID: 4445
		private static readonly bool k_IsMultiThreaded = true;

		// Token: 0x0400115E RID: 4446
		private List<Texture2D> atlases = new List<Texture2D>();

		// Token: 0x0400115F RID: 4447
		private List<float> sdfScalesArray = new List<float>();

		// Token: 0x04001160 RID: 4448
		private List<NativeSlice<Vertex>> verticesArray = new List<NativeSlice<Vertex>>();

		// Token: 0x04001161 RID: 4449
		private List<NativeSlice<ushort>> indicesArray = new List<NativeSlice<ushort>>();

		// Token: 0x04001162 RID: 4450
		private List<GlyphRenderMode> renderModes = new List<GlyphRenderMode>();

		// Token: 0x020004E9 RID: 1257
		private class ManagedJobData
		{
			// Token: 0x06002667 RID: 9831 RVA: 0x0008C519 File Offset: 0x0008A719
			public void Release()
			{
				ATGTextJobSystem.s_JobDataPool.Release(this);
			}

			// Token: 0x04001163 RID: 4451
			public TextElement textElement;

			// Token: 0x04001164 RID: 4452
			public MeshGenerationNode node;

			// Token: 0x04001165 RID: 4453
			public NativeTextInfo textInfo;

			// Token: 0x04001166 RID: 4454
			public bool success;
		}

		// Token: 0x020004EA RID: 1258
		private struct GenerateTextJobData : IJobParallelFor
		{
			// Token: 0x06002669 RID: 9833 RVA: 0x0008C528 File Offset: 0x0008A728
			public void Execute(int index)
			{
				List<ATGTextJobSystem.ManagedJobData> list = (List<ATGTextJobSystem.ManagedJobData>)this.managedJobDataHandle.Target;
				ATGTextJobSystem.ManagedJobData managedJobData = list[index];
				TextElement textElement = managedJobData.textElement;
				bool flag = textElement.computedStyle.unityFontDefinition.fontAsset != null;
				ATGTextJobSystem.ManagedJobData managedJobData2 = managedJobData;
				ATGTextJobSystem.ManagedJobData managedJobData3 = managedJobData;
				ValueTuple<NativeTextInfo, bool> valueTuple = textElement.uitkTextHandle.UpdateNative(flag);
				managedJobData2.textInfo = valueTuple.Item1;
				managedJobData3.success = valueTuple.Item2;
			}

			// Token: 0x04001167 RID: 4455
			public GCHandle managedJobDataHandle;
		}
	}
}
