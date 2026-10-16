using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200006E RID: 110
	public class TMP_UpdateManager
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0002D52C File Offset: 0x0002B72C
		private static TMP_UpdateManager instance
		{
			get
			{
				if (TMP_UpdateManager.s_Instance == null)
				{
					TMP_UpdateManager.s_Instance = new TMP_UpdateManager();
				}
				return TMP_UpdateManager.s_Instance;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002D544 File Offset: 0x0002B744
		private TMP_UpdateManager()
		{
			Canvas.willRenderCanvases += this.DoRebuilds;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002D5C0 File Offset: 0x0002B7C0
		internal static void RegisterTextObjectForUpdate(TMP_Text textObject)
		{
			TMP_UpdateManager.instance.InternalRegisterTextObjectForUpdate(textObject);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002D5D0 File Offset: 0x0002B7D0
		private void InternalRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			int instanceID = textObject.GetInstanceID();
			if (this.m_InternalUpdateLookup.Contains(instanceID))
			{
				return;
			}
			this.m_InternalUpdateLookup.Add(instanceID);
			this.m_InternalUpdateQueue.Add(textObject);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002D60C File Offset: 0x0002B80C
		public static void RegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForLayoutRebuild(element);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002D61C File Offset: 0x0002B81C
		private void InternalRegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (this.m_LayoutQueueLookup.Contains(instanceID))
			{
				return;
			}
			this.m_LayoutQueueLookup.Add(instanceID);
			this.m_LayoutRebuildQueue.Add(element);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0002D658 File Offset: 0x0002B858
		public static void RegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForGraphicRebuild(element);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0002D668 File Offset: 0x0002B868
		private void InternalRegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (this.m_GraphicQueueLookup.Contains(instanceID))
			{
				return;
			}
			this.m_GraphicQueueLookup.Add(instanceID);
			this.m_GraphicRebuildQueue.Add(element);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0002D6A4 File Offset: 0x0002B8A4
		public static void RegisterTextElementForCullingUpdate(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalRegisterTextElementForCullingUpdate(element);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002D6B4 File Offset: 0x0002B8B4
		private void InternalRegisterTextElementForCullingUpdate(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			if (this.m_CullingUpdateLookup.Contains(instanceID))
			{
				return;
			}
			this.m_CullingUpdateLookup.Add(instanceID);
			this.m_CullingUpdateQueue.Add(element);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0002D6F0 File Offset: 0x0002B8F0
		private void OnCameraPreCull()
		{
			this.DoRebuilds();
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0002D6F8 File Offset: 0x0002B8F8
		private void DoRebuilds()
		{
			for (int i = 0; i < this.m_InternalUpdateQueue.Count; i++)
			{
				this.m_InternalUpdateQueue[i].InternalUpdate();
			}
			for (int j = 0; j < this.m_LayoutRebuildQueue.Count; j++)
			{
				this.m_LayoutRebuildQueue[j].Rebuild(CanvasUpdate.Prelayout);
			}
			if (this.m_LayoutRebuildQueue.Count > 0)
			{
				this.m_LayoutRebuildQueue.Clear();
				this.m_LayoutQueueLookup.Clear();
			}
			for (int k = 0; k < this.m_GraphicRebuildQueue.Count; k++)
			{
				this.m_GraphicRebuildQueue[k].Rebuild(CanvasUpdate.PreRender);
			}
			if (this.m_GraphicRebuildQueue.Count > 0)
			{
				this.m_GraphicRebuildQueue.Clear();
				this.m_GraphicQueueLookup.Clear();
			}
			for (int l = 0; l < this.m_CullingUpdateQueue.Count; l++)
			{
				this.m_CullingUpdateQueue[l].UpdateCulling();
			}
			if (this.m_CullingUpdateQueue.Count > 0)
			{
				this.m_CullingUpdateQueue.Clear();
				this.m_CullingUpdateLookup.Clear();
			}
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0002D80F File Offset: 0x0002BA0F
		internal static void UnRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			TMP_UpdateManager.instance.InternalUnRegisterTextObjectForUpdate(textObject);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002D81C File Offset: 0x0002BA1C
		public static void UnRegisterTextElementForRebuild(TMP_Text element)
		{
			TMP_UpdateManager.instance.InternalUnRegisterTextElementForGraphicRebuild(element);
			TMP_UpdateManager.instance.InternalUnRegisterTextElementForLayoutRebuild(element);
			TMP_UpdateManager.instance.InternalUnRegisterTextObjectForUpdate(element);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002D840 File Offset: 0x0002BA40
		private void InternalUnRegisterTextElementForGraphicRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			this.m_GraphicRebuildQueue.Remove(element);
			this.m_GraphicQueueLookup.Remove(instanceID);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002D870 File Offset: 0x0002BA70
		private void InternalUnRegisterTextElementForLayoutRebuild(TMP_Text element)
		{
			int instanceID = element.GetInstanceID();
			this.m_LayoutRebuildQueue.Remove(element);
			this.m_LayoutQueueLookup.Remove(instanceID);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0002D8A0 File Offset: 0x0002BAA0
		private void InternalUnRegisterTextObjectForUpdate(TMP_Text textObject)
		{
			int instanceID = textObject.GetInstanceID();
			this.m_InternalUpdateQueue.Remove(textObject);
			this.m_InternalUpdateLookup.Remove(instanceID);
		}

		// Token: 0x040004DF RID: 1247
		private static TMP_UpdateManager s_Instance;

		// Token: 0x040004E0 RID: 1248
		private readonly HashSet<int> m_LayoutQueueLookup = new HashSet<int>();

		// Token: 0x040004E1 RID: 1249
		private readonly List<TMP_Text> m_LayoutRebuildQueue = new List<TMP_Text>();

		// Token: 0x040004E2 RID: 1250
		private readonly HashSet<int> m_GraphicQueueLookup = new HashSet<int>();

		// Token: 0x040004E3 RID: 1251
		private readonly List<TMP_Text> m_GraphicRebuildQueue = new List<TMP_Text>();

		// Token: 0x040004E4 RID: 1252
		private readonly HashSet<int> m_InternalUpdateLookup = new HashSet<int>();

		// Token: 0x040004E5 RID: 1253
		private readonly List<TMP_Text> m_InternalUpdateQueue = new List<TMP_Text>();

		// Token: 0x040004E6 RID: 1254
		private readonly HashSet<int> m_CullingUpdateLookup = new HashSet<int>();

		// Token: 0x040004E7 RID: 1255
		private readonly List<TMP_Text> m_CullingUpdateQueue = new List<TMP_Text>();

		// Token: 0x040004E8 RID: 1256
		private static ProfilerMarker k_RegisterTextObjectForUpdateMarker = new ProfilerMarker("TMP.RegisterTextObjectForUpdate");

		// Token: 0x040004E9 RID: 1257
		private static ProfilerMarker k_RegisterTextElementForGraphicRebuildMarker = new ProfilerMarker("TMP.RegisterTextElementForGraphicRebuild");

		// Token: 0x040004EA RID: 1258
		private static ProfilerMarker k_RegisterTextElementForCullingUpdateMarker = new ProfilerMarker("TMP.RegisterTextElementForCullingUpdate");

		// Token: 0x040004EB RID: 1259
		private static ProfilerMarker k_UnregisterTextObjectForUpdateMarker = new ProfilerMarker("TMP.UnregisterTextObjectForUpdate");

		// Token: 0x040004EC RID: 1260
		private static ProfilerMarker k_UnregisterTextElementForGraphicRebuildMarker = new ProfilerMarker("TMP.UnregisterTextElementForGraphicRebuild");
	}
}
