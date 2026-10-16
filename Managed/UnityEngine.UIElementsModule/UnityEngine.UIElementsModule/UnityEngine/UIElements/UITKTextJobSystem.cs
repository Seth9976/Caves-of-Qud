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
	// Token: 0x020004F2 RID: 1266
	internal class UITKTextJobSystem
	{
		// Token: 0x060026B5 RID: 9909 RVA: 0x0008E610 File Offset: 0x0008C810
		public UITKTextJobSystem()
		{
			this.m_PrepareTextJobifiedCallback = new MeshGenerationCallback(this.PrepareTextJobified);
			this.m_GenerateTextJobifiedCallback = new MeshGenerationCallback(this.GenerateTextJobified);
			this.m_AddDrawEntriesCallback = new MeshGenerationCallback(this.AddDrawEntries);
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x0008E666 File Offset: 0x0008C866
		private static void OnGetManagedJob(UITKTextJobSystem.ManagedJobData managedJobData)
		{
			managedJobData.vertices = null;
			managedJobData.indices = null;
			managedJobData.materials = null;
			managedJobData.renderModes = null;
			managedJobData.prepareSuccess = false;
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x0008E68C File Offset: 0x0008C88C
		internal void GenerateText(MeshGenerationContext mgc, TextElement textElement)
		{
			MeshGenerationNode meshGenerationNode;
			mgc.InsertMeshGenerationNode(out meshGenerationNode);
			UITKTextJobSystem.ManagedJobData managedJobData = UITKTextJobSystem.s_JobDataPool.Get();
			managedJobData.visualElement = textElement;
			managedJobData.node = meshGenerationNode;
			this.textJobDatas.Add(managedJobData);
			bool flag = this.hasPendingTextWork;
			if (!flag)
			{
				this.hasPendingTextWork = true;
				this.textJobDatasHandle = GCHandle.Alloc(this.textJobDatas);
				mgc.AddMeshGenerationCallback(this.m_PrepareTextJobifiedCallback, null, MeshGenerationCallbackType.WorkThenFork, false);
			}
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x0008E700 File Offset: 0x0008C900
		internal void PrepareTextJobified(MeshGenerationContext mgc, object _)
		{
			TextHandle.InitThreadArrays();
			PanelTextSettings.InitializeDefaultPanelTextSettingsIfNull();
			TextHandle.UpdateCurrentFrame();
			this.hasPendingTextWork = false;
			UITKTextJobSystem.PrepareTextJobData prepareTextJobData = new UITKTextJobSystem.PrepareTextJobData
			{
				managedJobDataHandle = this.textJobDatasHandle
			};
			TextGenerator.IsExecutingJob = true;
			JobHandle jobHandle = prepareTextJobData.ScheduleOrRunJob(this.textJobDatas.Count, 1, default(JobHandle));
			mgc.AddMeshGenerationJob(jobHandle);
			mgc.AddMeshGenerationCallback(this.m_GenerateTextJobifiedCallback, null, MeshGenerationCallbackType.Work, true);
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x0008E77C File Offset: 0x0008C97C
		private void GenerateTextJobified(MeshGenerationContext mgc, object _)
		{
			TextGenerator.IsExecutingJob = false;
			foreach (UITKTextJobSystem.ManagedJobData managedJobData in this.textJobDatas)
			{
				TextSettings textSettingsFrom = TextUtilities.GetTextSettingsFrom(managedJobData.visualElement);
				if (textSettingsFrom != null)
				{
					UnicodeLineBreakingRules lineBreakingRules = textSettingsFrom.lineBreakingRules;
					if (lineBreakingRules != null)
					{
						lineBreakingRules.LoadLineBreakingRules();
					}
				}
				List<FontAsset> list = ((textSettingsFrom != null) ? textSettingsFrom.fallbackOSFontAssets : null);
				bool prepareSuccess = managedJobData.prepareSuccess;
				if (!prepareSuccess)
				{
					managedJobData.visualElement.uitkTextHandle.ConvertUssToTextGenerationSettings();
					managedJobData.visualElement.uitkTextHandle.PrepareFontAsset();
				}
			}
			FontAsset.UpdateFontAssetsInUpdateQueue();
			TempMeshAllocator tempMeshAllocator;
			mgc.GetTempMeshAllocator(out tempMeshAllocator);
			UITKTextJobSystem.GenerateTextJobData generateTextJobData = new UITKTextJobSystem.GenerateTextJobData
			{
				managedJobDataHandle = this.textJobDatasHandle,
				alloc = tempMeshAllocator
			};
			TextHandle.UpdateCurrentFrame();
			TextGenerator.IsExecutingJob = true;
			JobHandle jobHandle = generateTextJobData.ScheduleOrRunJob(this.textJobDatas.Count, 1, default(JobHandle));
			mgc.AddMeshGenerationJob(jobHandle);
			mgc.AddMeshGenerationCallback(this.m_AddDrawEntriesCallback, null, MeshGenerationCallbackType.Work, true);
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x0008E8B0 File Offset: 0x0008CAB0
		private static void ConvertMeshInfoToUIRVertex(MeshInfo[] meshInfos, TempMeshAllocator alloc, TextElement visualElement, ref List<Material> materials, ref List<NativeSlice<Vertex>> verticesArray, ref List<NativeSlice<ushort>> indicesArray, ref List<GlyphRenderMode> renderModes)
		{
			ObjectPool<List<Material>> objectPool = UITKTextJobSystem.s_MaterialsPool;
			lock (objectPool)
			{
				materials = UITKTextJobSystem.s_MaterialsPool.Get();
				verticesArray = UITKTextJobSystem.s_VerticesPool.Get();
				indicesArray = UITKTextJobSystem.s_IndicesPool.Get();
				renderModes = UITKTextJobSystem.s_RenderModesPool.Get();
			}
			Vector2 min = visualElement.contentRect.min;
			bool hasMultipleColors = visualElement.uitkTextHandle.textInfo.hasMultipleColors;
			bool flag2 = hasMultipleColors;
			if (flag2)
			{
				visualElement.renderChainData.flags = visualElement.renderChainData.flags | RenderDataFlags.IsIgnoringDynamicColorHint;
			}
			else
			{
				visualElement.renderChainData.flags = visualElement.renderChainData.flags & ~RenderDataFlags.IsIgnoringDynamicColorHint;
			}
			foreach (MeshInfo meshInfo in meshInfos)
			{
				Debug.Assert((meshInfo.vertexCount & 3) == 0);
				int num = (int)((ulong)UIRenderDevice.maxVerticesPerPage & 18446744073709551612UL);
				int j = meshInfo.vertexCount;
				int num2 = 0;
				while (j > 0)
				{
					int num3 = Mathf.Min(j, num);
					int num4 = num3 >> 2;
					int num5 = num4 * 6;
					materials.Add(meshInfo.material);
					renderModes.Add(meshInfo.glyphRenderMode);
					bool flag3 = meshInfo.glyphRenderMode != GlyphRenderMode.SMOOTH && meshInfo.glyphRenderMode != GlyphRenderMode.COLOR;
					bool flag4 = meshInfo.applySDF && !hasMultipleColors && (RenderEvents.NeedsColorID(visualElement) || (flag3 && RenderEvents.NeedsTextCoreSettings(visualElement)));
					NativeSlice<Vertex> nativeSlice;
					NativeSlice<ushort> nativeSlice2;
					alloc.AllocateTempMesh(num3, num5, out nativeSlice, out nativeSlice2);
					int k = 0;
					int num6 = 0;
					while (k < num3)
					{
						nativeSlice[k] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[num2], min, flag4, false);
						nativeSlice[k + 1] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[num2 + 1], min, flag4, false);
						nativeSlice[k + 2] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[num2 + 2], min, flag4, false);
						nativeSlice[k + 3] = MeshGenerator.ConvertTextVertexToUIRVertex(meshInfo.vertexData[num2 + 3], min, flag4, false);
						nativeSlice2[num6] = (ushort)k;
						nativeSlice2[num6 + 1] = (ushort)(k + 1);
						nativeSlice2[num6 + 2] = (ushort)(k + 2);
						nativeSlice2[num6 + 3] = (ushort)(k + 2);
						nativeSlice2[num6 + 4] = (ushort)(k + 3);
						nativeSlice2[num6 + 5] = (ushort)k;
						k += 4;
						num2 += 4;
						num6 += 6;
					}
					verticesArray.Add(nativeSlice);
					indicesArray.Add(nativeSlice2);
					j -= num3;
				}
				Debug.Assert(j == 0);
			}
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x0008EBB0 File Offset: 0x0008CDB0
		private void AddDrawEntries(MeshGenerationContext mgc, object _)
		{
			TextGenerator.IsExecutingJob = false;
			foreach (UITKTextJobSystem.ManagedJobData managedJobData in this.textJobDatas)
			{
				mgc.Begin(managedJobData.node.GetParentEntry(), managedJobData.visualElement);
				managedJobData.visualElement.uitkTextHandle.HandleLinkAndATagCallbacks();
				mgc.meshGenerator.DrawText(managedJobData.vertices, managedJobData.indices, managedJobData.materials, managedJobData.renderModes);
				managedJobData.visualElement.OnGenerateTextOver(mgc);
				mgc.End();
				managedJobData.Release();
			}
			this.textJobDatas.Clear();
			this.textJobDatasHandle.Free();
		}

		// Token: 0x0400118B RID: 4491
		private static readonly ProfilerMarker k_ExecuteMarker = new ProfilerMarker("TextJob.GenerateText");

		// Token: 0x0400118C RID: 4492
		private static readonly ProfilerMarker k_UpdateMainThreadMarker = new ProfilerMarker("TextJob.UpdateMainThread");

		// Token: 0x0400118D RID: 4493
		private static readonly ProfilerMarker k_PrepareMainThreadMarker = new ProfilerMarker("TextJob.PrepareMainThread");

		// Token: 0x0400118E RID: 4494
		private static readonly ProfilerMarker k_PrepareJobifiedMarker = new ProfilerMarker("TextJob.PrepareJobified");

		// Token: 0x0400118F RID: 4495
		private GCHandle textJobDatasHandle;

		// Token: 0x04001190 RID: 4496
		private List<UITKTextJobSystem.ManagedJobData> textJobDatas = new List<UITKTextJobSystem.ManagedJobData>();

		// Token: 0x04001191 RID: 4497
		private bool hasPendingTextWork;

		// Token: 0x04001192 RID: 4498
		private static ObjectPool<UITKTextJobSystem.ManagedJobData> s_JobDataPool = new ObjectPool<UITKTextJobSystem.ManagedJobData>(() => new UITKTextJobSystem.ManagedJobData(), new Action<UITKTextJobSystem.ManagedJobData>(UITKTextJobSystem.OnGetManagedJob), delegate(UITKTextJobSystem.ManagedJobData inst)
		{
			inst.visualElement = null;
		}, null, false, 10, 10000);

		// Token: 0x04001193 RID: 4499
		private static ObjectPool<List<Material>> s_MaterialsPool = new ObjectPool<List<Material>>(() => new List<Material>(), null, delegate(List<Material> list)
		{
			list.Clear();
		}, null, false, 10, 10000);

		// Token: 0x04001194 RID: 4500
		private static ObjectPool<List<GlyphRenderMode>> s_RenderModesPool = new ObjectPool<List<GlyphRenderMode>>(() => new List<GlyphRenderMode>(), null, delegate(List<GlyphRenderMode> list)
		{
			list.Clear();
		}, null, false, 10, 10000);

		// Token: 0x04001195 RID: 4501
		private static ObjectPool<List<NativeSlice<Vertex>>> s_VerticesPool = new ObjectPool<List<NativeSlice<Vertex>>>(() => new List<NativeSlice<Vertex>>(), null, delegate(List<NativeSlice<Vertex>> list)
		{
			list.Clear();
		}, null, false, 10, 10000);

		// Token: 0x04001196 RID: 4502
		private static ObjectPool<List<NativeSlice<ushort>>> s_IndicesPool = new ObjectPool<List<NativeSlice<ushort>>>(() => new List<NativeSlice<ushort>>(), null, delegate(List<NativeSlice<ushort>> list)
		{
			list.Clear();
		}, null, false, 10, 10000);

		// Token: 0x04001197 RID: 4503
		internal MeshGenerationCallback m_PrepareTextJobifiedCallback;

		// Token: 0x04001198 RID: 4504
		internal MeshGenerationCallback m_GenerateTextJobifiedCallback;

		// Token: 0x04001199 RID: 4505
		internal MeshGenerationCallback m_AddDrawEntriesCallback;

		// Token: 0x020004F3 RID: 1267
		private class ManagedJobData
		{
			// Token: 0x060026BD RID: 9917 RVA: 0x0008EDE0 File Offset: 0x0008CFE0
			public void Release()
			{
				bool flag = this.materials != null;
				if (flag)
				{
					UITKTextJobSystem.s_MaterialsPool.Release(this.materials);
					UITKTextJobSystem.s_VerticesPool.Release(this.vertices);
					UITKTextJobSystem.s_IndicesPool.Release(this.indices);
					UITKTextJobSystem.s_RenderModesPool.Release(this.renderModes);
				}
				UITKTextJobSystem.s_JobDataPool.Release(this);
			}

			// Token: 0x0400119A RID: 4506
			public TextElement visualElement;

			// Token: 0x0400119B RID: 4507
			public MeshGenerationNode node;

			// Token: 0x0400119C RID: 4508
			public List<Material> materials;

			// Token: 0x0400119D RID: 4509
			public List<GlyphRenderMode> renderModes;

			// Token: 0x0400119E RID: 4510
			public List<NativeSlice<Vertex>> vertices;

			// Token: 0x0400119F RID: 4511
			public List<NativeSlice<ushort>> indices;

			// Token: 0x040011A0 RID: 4512
			public bool prepareSuccess;
		}

		// Token: 0x020004F4 RID: 1268
		private struct PrepareTextJobData : IJobParallelFor
		{
			// Token: 0x060026BF RID: 9919 RVA: 0x0008EE50 File Offset: 0x0008D050
			public void Execute(int index)
			{
				List<UITKTextJobSystem.ManagedJobData> list = (List<UITKTextJobSystem.ManagedJobData>)this.managedJobDataHandle.Target;
				UITKTextJobSystem.ManagedJobData managedJobData = list[index];
				TextElement visualElement = managedJobData.visualElement;
				managedJobData.prepareSuccess = visualElement.uitkTextHandle.ConvertUssToTextGenerationSettings();
				bool prepareSuccess = managedJobData.prepareSuccess;
				if (prepareSuccess)
				{
					managedJobData.prepareSuccess = visualElement.uitkTextHandle.PrepareFontAsset();
				}
			}

			// Token: 0x040011A1 RID: 4513
			public GCHandle managedJobDataHandle;
		}

		// Token: 0x020004F5 RID: 1269
		private struct GenerateTextJobData : IJobParallelFor
		{
			// Token: 0x060026C0 RID: 9920 RVA: 0x0008EEAC File Offset: 0x0008D0AC
			public void Execute(int index)
			{
				List<UITKTextJobSystem.ManagedJobData> list = (List<UITKTextJobSystem.ManagedJobData>)this.managedJobDataHandle.Target;
				UITKTextJobSystem.ManagedJobData managedJobData = list[index];
				TextElement visualElement = managedJobData.visualElement;
				visualElement.uitkTextHandle.UpdateMesh();
				TextInfo textInfo = visualElement.uitkTextHandle.textInfo;
				MeshInfo[] meshInfo = textInfo.meshInfo;
				List<Material> list2 = null;
				List<NativeSlice<Vertex>> list3 = null;
				List<NativeSlice<ushort>> list4 = null;
				List<GlyphRenderMode> list5 = null;
				UITKTextJobSystem.ConvertMeshInfoToUIRVertex(meshInfo, this.alloc, visualElement, ref list2, ref list3, ref list4, ref list5);
				managedJobData.materials = list2;
				managedJobData.vertices = list3;
				managedJobData.indices = list4;
				managedJobData.renderModes = list5;
				visualElement.uitkTextHandle.HandleATag();
				visualElement.uitkTextHandle.HandleLinkTag();
			}

			// Token: 0x040011A2 RID: 4514
			public GCHandle managedJobDataHandle;

			// Token: 0x040011A3 RID: 4515
			[ReadOnly]
			public TempMeshAllocator alloc;
		}
	}
}
