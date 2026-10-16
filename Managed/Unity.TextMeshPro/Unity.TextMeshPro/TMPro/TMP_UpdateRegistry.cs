using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200006F RID: 111
	public class TMP_UpdateRegistry
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0002D928 File Offset: 0x0002BB28
		public static TMP_UpdateRegistry instance
		{
			get
			{
				if (TMP_UpdateRegistry.s_Instance == null)
				{
					TMP_UpdateRegistry.s_Instance = new TMP_UpdateRegistry();
				}
				return TMP_UpdateRegistry.s_Instance;
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0002D940 File Offset: 0x0002BB40
		protected TMP_UpdateRegistry()
		{
			Canvas.willRenderCanvases += this.PerformUpdateForCanvasRendererObjects;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0002D990 File Offset: 0x0002BB90
		public static void RegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalRegisterCanvasElementForLayoutRebuild(element);
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x0002D9A0 File Offset: 0x0002BBA0
		private bool InternalRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			if (this.m_LayoutQueueLookup.Contains(instanceID))
			{
				return false;
			}
			this.m_LayoutQueueLookup.Add(instanceID);
			this.m_LayoutRebuildQueue.Add(element);
			return true;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0002D9E3 File Offset: 0x0002BBE3
		public static void RegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0002D9F4 File Offset: 0x0002BBF4
		private bool InternalRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			if (this.m_GraphicQueueLookup.Contains(instanceID))
			{
				return false;
			}
			this.m_GraphicQueueLookup.Add(instanceID);
			this.m_GraphicRebuildQueue.Add(element);
			return true;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002DA38 File Offset: 0x0002BC38
		private void PerformUpdateForCanvasRendererObjects()
		{
			for (int i = 0; i < this.m_LayoutRebuildQueue.Count; i++)
			{
				TMP_UpdateRegistry.instance.m_LayoutRebuildQueue[i].Rebuild(CanvasUpdate.Prelayout);
			}
			if (this.m_LayoutRebuildQueue.Count > 0)
			{
				this.m_LayoutRebuildQueue.Clear();
				this.m_LayoutQueueLookup.Clear();
			}
			for (int j = 0; j < this.m_GraphicRebuildQueue.Count; j++)
			{
				TMP_UpdateRegistry.instance.m_GraphicRebuildQueue[j].Rebuild(CanvasUpdate.PreRender);
			}
			if (this.m_GraphicRebuildQueue.Count > 0)
			{
				this.m_GraphicRebuildQueue.Clear();
				this.m_GraphicQueueLookup.Clear();
			}
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0002DAE5 File Offset: 0x0002BCE5
		private void PerformUpdateForMeshRendererObjects()
		{
			Debug.Log("Perform update of MeshRenderer objects.");
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0002DAF1 File Offset: 0x0002BCF1
		public static void UnRegisterCanvasElementForRebuild(ICanvasElement element)
		{
			TMP_UpdateRegistry.instance.InternalUnRegisterCanvasElementForLayoutRebuild(element);
			TMP_UpdateRegistry.instance.InternalUnRegisterCanvasElementForGraphicRebuild(element);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0002DB0C File Offset: 0x0002BD0C
		private void InternalUnRegisterCanvasElementForLayoutRebuild(ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			TMP_UpdateRegistry.instance.m_LayoutRebuildQueue.Remove(element);
			this.m_GraphicQueueLookup.Remove(instanceID);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0002DB44 File Offset: 0x0002BD44
		private void InternalUnRegisterCanvasElementForGraphicRebuild(ICanvasElement element)
		{
			int instanceID = (element as global::UnityEngine.Object).GetInstanceID();
			TMP_UpdateRegistry.instance.m_GraphicRebuildQueue.Remove(element);
			this.m_LayoutQueueLookup.Remove(instanceID);
		}

		// Token: 0x040004ED RID: 1261
		private static TMP_UpdateRegistry s_Instance;

		// Token: 0x040004EE RID: 1262
		private readonly List<ICanvasElement> m_LayoutRebuildQueue = new List<ICanvasElement>();

		// Token: 0x040004EF RID: 1263
		private HashSet<int> m_LayoutQueueLookup = new HashSet<int>();

		// Token: 0x040004F0 RID: 1264
		private readonly List<ICanvasElement> m_GraphicRebuildQueue = new List<ICanvasElement>();

		// Token: 0x040004F1 RID: 1265
		private HashSet<int> m_GraphicQueueLookup = new HashSet<int>();
	}
}
