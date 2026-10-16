using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004D RID: 77
	internal class VisualTreeBindingsUpdater : BaseVisualTreeHierarchyTrackerUpdater
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000B2D2 File Offset: 0x000094D2
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeBindingsUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000B2D9 File Offset: 0x000094D9
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000B2E0 File Offset: 0x000094E0
		public static bool disableBindingsThrottling { get; set; } = false;

		// Token: 0x06000278 RID: 632 RVA: 0x0000B2E8 File Offset: 0x000094E8
		private IBinding GetBindingObjectFromElement(VisualElement ve)
		{
			IBindable bindable = ve as IBindable;
			bool flag = bindable != null;
			if (flag)
			{
				bool flag2 = bindable.binding != null;
				if (flag2)
				{
					return bindable.binding;
				}
			}
			return VisualTreeBindingsUpdater.GetAdditionalBinding(ve);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B327 File Offset: 0x00009527
		private void StartTracking(VisualElement ve)
		{
			this.m_ElementsToAdd.Add(ve);
			this.m_ElementsToRemove.Remove(ve);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000B344 File Offset: 0x00009544
		private void StopTracking(VisualElement ve)
		{
			this.m_ElementsToRemove.Add(ve);
			this.m_ElementsToAdd.Remove(ve);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000B361 File Offset: 0x00009561
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000B369 File Offset: 0x00009569
		public Dictionary<object, object> temporaryObjectCache { get; private set; } = new Dictionary<object, object>();

		// Token: 0x0600027D RID: 637 RVA: 0x0000B374 File Offset: 0x00009574
		public static void SetAdditionalBinding(VisualElement ve, IBinding b)
		{
			IBinding additionalBinding = VisualTreeBindingsUpdater.GetAdditionalBinding(ve);
			if (additionalBinding != null)
			{
				additionalBinding.Release();
			}
			ve.SetProperty(VisualTreeBindingsUpdater.s_AdditionalBindingObjectVEPropertyName, b);
			ve.IncrementVersion(VersionChangeType.Bindings);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000B3AA File Offset: 0x000095AA
		public static void ClearAdditionalBinding(VisualElement ve)
		{
			VisualTreeBindingsUpdater.SetAdditionalBinding(ve, null);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B3B8 File Offset: 0x000095B8
		public static IBinding GetAdditionalBinding(VisualElement ve)
		{
			return ve.GetProperty(VisualTreeBindingsUpdater.s_AdditionalBindingObjectVEPropertyName) as IBinding;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000B3DC File Offset: 0x000095DC
		public static void AddBindingRequest(VisualElement ve, IBindingRequest req)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list == null;
			if (flag)
			{
				list = ObjectListPool<IBindingRequest>.Get();
				ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, list);
			}
			list.Add(req);
			ve.IncrementVersion(VersionChangeType.Bindings);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000B428 File Offset: 0x00009628
		public static void RemoveBindingRequest(VisualElement ve, IBindingRequest req)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list != null;
			if (flag)
			{
				req.Release();
				list.Remove(req);
				bool flag2 = list.Count == 0;
				if (flag2)
				{
					ObjectListPool<IBindingRequest>.Release(list);
					ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000B484 File Offset: 0x00009684
		public static void ClearBindingRequests(VisualElement ve)
		{
			List<IBindingRequest> list = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
			bool flag = list != null;
			if (flag)
			{
				foreach (IBindingRequest bindingRequest in list)
				{
					bindingRequest.Release();
				}
				ObjectListPool<IBindingRequest>.Release(list);
				ve.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B508 File Offset: 0x00009708
		private void StartTrackingRecursive(VisualElement ve)
		{
			IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(ve);
			bool flag = bindingObjectFromElement != null;
			if (flag)
			{
				this.StartTracking(ve);
			}
			object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
			bool flag2 = property != null;
			if (flag2)
			{
				this.m_ElementsToBind.Add(ve);
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				this.StartTrackingRecursive(visualElement);
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000B59C File Offset: 0x0000979C
		private void StopTrackingRecursive(VisualElement ve)
		{
			this.StopTracking(ve);
			object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
			bool flag = property != null;
			if (flag)
			{
				this.m_ElementsToBind.Remove(ve);
			}
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				this.StopTrackingRecursive(visualElement);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000B618 File Offset: 0x00009818
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			base.OnVersionChanged(ve, versionChangeType);
			bool flag = (versionChangeType & VersionChangeType.Bindings) == VersionChangeType.Bindings;
			if (flag)
			{
				bool flag2 = this.GetBindingObjectFromElement(ve) != null;
				if (flag2)
				{
					this.StartTracking(ve);
				}
				else
				{
					this.StopTracking(ve);
				}
				object property = ve.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName);
				bool flag3 = property != null;
				if (flag3)
				{
					this.m_ElementsToBind.Add(ve);
				}
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000B684 File Offset: 0x00009884
		protected override void OnHierarchyChange(VisualElement ve, HierarchyChangeType type)
		{
			if (type != HierarchyChangeType.AddedToParent)
			{
				if (type == HierarchyChangeType.RemovedFromParent)
				{
					this.StopTrackingRecursive(ve);
				}
			}
			else
			{
				this.StartTrackingRecursive(ve);
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000B6B8 File Offset: 0x000098B8
		private static long CurrentTime()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000B6D0 File Offset: 0x000098D0
		public static bool ShouldProcessBindings(long startTime)
		{
			return VisualTreeBindingsUpdater.disableBindingsThrottling || VisualTreeBindingsUpdater.CurrentTime() - startTime < 100L;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000B6F8 File Offset: 0x000098F8
		public void PerformTrackingOperations()
		{
			foreach (VisualElement visualElement in this.m_ElementsToAdd)
			{
				IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
				bool flag = bindingObjectFromElement != null;
				if (flag)
				{
					this.m_ElementsWithBindings.Add(visualElement);
				}
			}
			this.m_ElementsToAdd.Clear();
			foreach (VisualElement visualElement2 in this.m_ElementsToRemove)
			{
				this.m_ElementsWithBindings.Remove(visualElement2);
			}
			this.m_ElementsToRemove.Clear();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000B7D0 File Offset: 0x000099D0
		public override void Update()
		{
			base.Update();
			bool flag = this.m_ElementsToBind.Count > 0;
			if (flag)
			{
				using (VisualTreeBindingsUpdater.s_ProfilerBindingRequestsMarker.Auto())
				{
					long num = VisualTreeBindingsUpdater.CurrentTime();
					while (this.m_ElementsToBind.Count > 0 && VisualTreeBindingsUpdater.ShouldProcessBindings(num))
					{
						VisualElement visualElement = this.m_ElementsToBind.FirstOrDefault<VisualElement>();
						bool flag2 = visualElement != null;
						if (!flag2)
						{
							break;
						}
						this.m_ElementsToBind.Remove(visualElement);
						List<IBindingRequest> list = visualElement.GetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName) as List<IBindingRequest>;
						bool flag3 = list != null;
						if (flag3)
						{
							visualElement.SetProperty(VisualTreeBindingsUpdater.s_BindingRequestObjectVEPropertyName, null);
							foreach (IBindingRequest bindingRequest in list)
							{
								bindingRequest.Bind(visualElement);
							}
							ObjectListPool<IBindingRequest>.Release(list);
						}
					}
				}
			}
			this.PerformTrackingOperations();
			bool flag4 = this.m_ElementsWithBindings.Count > 0;
			if (flag4)
			{
				long num2 = VisualTreeBindingsUpdater.CurrentTime();
				bool flag5 = VisualTreeBindingsUpdater.disableBindingsThrottling || this.m_LastUpdateTime + 100L < num2;
				if (flag5)
				{
					this.UpdateBindings();
					this.m_LastUpdateTime = num2;
				}
			}
			bool flag6 = this.m_ElementsToBind.Count == 0;
			if (flag6)
			{
				this.temporaryObjectCache.Clear();
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000B97C File Offset: 0x00009B7C
		private void UpdateBindings()
		{
			foreach (VisualElement visualElement in this.m_ElementsWithBindings)
			{
				IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
				bool flag = bindingObjectFromElement == null || visualElement.elementPanel != base.panel;
				if (flag)
				{
					if (bindingObjectFromElement != null)
					{
						bindingObjectFromElement.Release();
					}
					this.StopTracking(visualElement);
				}
				else
				{
					this.updatedBindings.Add(bindingObjectFromElement);
				}
			}
			foreach (IBinding binding in this.updatedBindings)
			{
				binding.PreUpdate();
			}
			foreach (IBinding binding2 in this.updatedBindings)
			{
				binding2.Update();
			}
			this.updatedBindings.Clear();
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		internal override void PollElementsWithBindings(Action<VisualElement, IBinding> callback)
		{
			this.PerformTrackingOperations();
			bool flag = this.m_ElementsWithBindings.Count > 0;
			if (flag)
			{
				foreach (VisualElement visualElement in this.m_ElementsWithBindings)
				{
					IBinding bindingObjectFromElement = this.GetBindingObjectFromElement(visualElement);
					bool flag2 = bindingObjectFromElement == null || visualElement.elementPanel != base.panel;
					if (flag2)
					{
						if (bindingObjectFromElement != null)
						{
							bindingObjectFromElement.Release();
						}
						this.StopTracking(visualElement);
					}
					else
					{
						callback(visualElement, bindingObjectFromElement);
					}
				}
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000BB6C File Offset: 0x00009D6C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			this.PerformTrackingOperations();
		}

		// Token: 0x0400016D RID: 365
		private static readonly PropertyName s_BindingRequestObjectVEPropertyName = "__unity-binding-request-object";

		// Token: 0x0400016E RID: 366
		private static readonly PropertyName s_AdditionalBindingObjectVEPropertyName = "__unity-additional-binding-object";

		// Token: 0x0400016F RID: 367
		private static readonly string s_Description = "Update Bindings";

		// Token: 0x04000170 RID: 368
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeBindingsUpdater.s_Description);

		// Token: 0x04000171 RID: 369
		private static readonly ProfilerMarker s_ProfilerBindingRequestsMarker = new ProfilerMarker("Bindings.Requests");

		// Token: 0x04000172 RID: 370
		private static ProfilerMarker s_MarkerUpdate = new ProfilerMarker("Bindings.Update");

		// Token: 0x04000173 RID: 371
		private static ProfilerMarker s_MarkerPoll = new ProfilerMarker("Bindings.PollElementsWithBindings");

		// Token: 0x04000175 RID: 373
		private readonly HashSet<VisualElement> m_ElementsWithBindings = new HashSet<VisualElement>();

		// Token: 0x04000176 RID: 374
		private readonly HashSet<VisualElement> m_ElementsToAdd = new HashSet<VisualElement>();

		// Token: 0x04000177 RID: 375
		private readonly HashSet<VisualElement> m_ElementsToRemove = new HashSet<VisualElement>();

		// Token: 0x04000178 RID: 376
		internal const int k_MinUpdateDelayMs = 100;

		// Token: 0x04000179 RID: 377
		private const int k_MaxBindingTimeMs = 100;

		// Token: 0x0400017A RID: 378
		private long m_LastUpdateTime = 0L;

		// Token: 0x0400017B RID: 379
		private HashSet<VisualElement> m_ElementsToBind = new HashSet<VisualElement>();

		// Token: 0x0400017D RID: 381
		private List<IBinding> updatedBindings = new List<IBinding>();

		// Token: 0x0200004E RID: 78
		private class RequestObjectListPool : ObjectListPool<IBindingRequest>
		{
		}
	}
}
