using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000033 RID: 51
	internal sealed class DataBindingManager : IDisposable
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00008664 File Offset: 0x00006864
		// (set) Token: 0x060001BF RID: 447 RVA: 0x0000868F File Offset: 0x0000688F
		internal BindingLogLevel logLevel
		{
			get
			{
				return this.m_LogLevel ?? DataBindingManager.globalLogLevel;
			}
			set
			{
				this.m_LogLevel = new BindingLogLevel?(value);
			}
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000869D File Offset: 0x0000689D
		internal void ResetLogLevel()
		{
			this.m_LogLevel = null;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000086AC File Offset: 0x000068AC
		internal DataBindingManager(BaseVisualElementPanel panel)
		{
			this.m_Panel = panel;
			this.m_DataSourceTracker = new DataBindingManager.HierarchyDataSourceTracker(this);
			this.m_BindingsTracker = new DataBindingManager.HierarchyBindingTracker(panel);
			this.m_DetectedChangesFromUI = new List<DataBindingManager.ChangesFromUI>();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000086F8 File Offset: 0x000068F8
		internal int GetTrackedDataSourcesCount()
		{
			return this.m_DataSourceTracker.GetTrackedDataSourcesCount();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008718 File Offset: 0x00006918
		internal bool IsTrackingDataSource(VisualElement element)
		{
			return this.m_DataSourceTracker.IsTrackingDataSource(element);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008738 File Offset: 0x00006938
		internal bool TryGetLastVersion(object source, out long version)
		{
			return this.m_DataSourceTracker.TryGetLastVersion(source, out version);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008757 File Offset: 0x00006957
		internal void UpdateVersion(object source, long version)
		{
			this.m_DataSourceTracker.UpdateVersion(source, version);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008768 File Offset: 0x00006968
		internal void CacheUIBindingResult(DataBindingManager.BindingData bindingData, BindingResult result)
		{
			bindingData.m_SourceToUILastUpdate = new BindingResult?(result);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008778 File Offset: 0x00006978
		internal bool TryGetLastUIBindingResult(DataBindingManager.BindingData bindingData, out BindingResult result)
		{
			bool flag = bindingData.m_SourceToUILastUpdate != null;
			bool flag2;
			if (flag)
			{
				result = bindingData.m_SourceToUILastUpdate.Value;
				flag2 = true;
			}
			else
			{
				result = default(BindingResult);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000087B7 File Offset: 0x000069B7
		internal void CacheSourceBindingResult(DataBindingManager.BindingData bindingData, BindingResult result)
		{
			bindingData.m_UIToSourceLastUpdate = new BindingResult?(result);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000087C8 File Offset: 0x000069C8
		internal bool TryGetLastSourceBindingResult(DataBindingManager.BindingData bindingData, out BindingResult result)
		{
			bool flag = bindingData.m_UIToSourceLastUpdate != null;
			bool flag2;
			if (flag)
			{
				result = bindingData.m_UIToSourceLastUpdate.Value;
				flag2 = true;
			}
			else
			{
				result = default(BindingResult);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008808 File Offset: 0x00006A08
		internal DataSourceContext GetResolvedDataSourceContext(VisualElement element, DataBindingManager.BindingData bindingData)
		{
			return (element.panel == this.m_Panel) ? this.m_DataSourceTracker.GetResolvedDataSourceContext(element, bindingData) : default(DataSourceContext);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00008840 File Offset: 0x00006A40
		internal bool TryGetSource(VisualElement element, out object dataSource)
		{
			bool flag = element.panel == this.m_Panel;
			bool flag2;
			if (flag)
			{
				dataSource = this.m_DataSourceTracker.GetHierarchyDataSource(element);
				flag2 = true;
			}
			else
			{
				dataSource = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000887C File Offset: 0x00006A7C
		internal object TrackHierarchyDataSource(VisualElement element)
		{
			return (element.panel == this.m_Panel) ? this.m_DataSourceTracker.GetHierarchicalDataSourceContext(element).dataSource : null;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000088B4 File Offset: 0x00006AB4
		internal int GetRefCount(object dataSource)
		{
			return this.m_DataSourceTracker.GetRefCount(dataSource);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x000088D4 File Offset: 0x00006AD4
		internal int GetBoundElementsCount()
		{
			return this.m_BindingsTracker.GetTrackedElementsCount();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000088F4 File Offset: 0x00006AF4
		internal IEnumerable<VisualElement> GetBoundElements()
		{
			return this.m_BindingsTracker.GetBoundElements();
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008914 File Offset: 0x00006B14
		internal IEnumerable<VisualElement> GetUnorderedBoundElements()
		{
			return this.m_BindingsTracker.GetUnorderedBoundElements();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008934 File Offset: 0x00006B34
		public DataBindingManager.IgnoreUIChangesScope IgnoreChangesScope(VisualElement target, BindingId bindingId, Binding binding)
		{
			return new DataBindingManager.IgnoreUIChangesScope(this, target, bindingId, binding);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008950 File Offset: 0x00006B50
		internal List<DataBindingManager.ChangesFromUI> GetChangedDetectedFromUI()
		{
			return this.m_DetectedChangesFromUI;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00008968 File Offset: 0x00006B68
		internal HashSet<PropertyPath> GetChangedDetectedFromSource(object dataSource)
		{
			return this.m_DataSourceTracker.GetChangesFromSource(dataSource);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00008986 File Offset: 0x00006B86
		internal void ClearChangesFromSource(object dataSource)
		{
			this.m_DataSourceTracker.ClearChangesFromSource(dataSource);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00008998 File Offset: 0x00006B98
		internal List<DataBindingManager.BindingData> GetBindingData(VisualElement element)
		{
			DataBindingManager.BindingDataCollection bindingDataCollection;
			return (element.panel == this.m_Panel) ? (this.m_BindingsTracker.TryGetBindingCollection(element, out bindingDataCollection) ? bindingDataCollection.GetBindings() : DataBindingManager.s_Empty) : DataBindingManager.s_Empty;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000089E0 File Offset: 0x00006BE0
		internal bool TryGetBindingData(VisualElement element, in BindingId bindingId, out DataBindingManager.BindingData bindingData)
		{
			bindingData = null;
			DataBindingManager.BindingDataCollection bindingDataCollection;
			bool flag = element.panel == this.m_Panel && this.m_BindingsTracker.TryGetBindingCollection(element, out bindingDataCollection);
			bool flag2;
			if (flag)
			{
				flag2 = bindingDataCollection.TryGetBindingData(in bindingId, out bindingData);
			}
			else
			{
				bindingData = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008A2C File Offset: 0x00006C2C
		internal void RegisterBinding(VisualElement element, in BindingId bindingId, Binding binding)
		{
			Assert.IsFalse(binding == null);
			Assert.IsFalse((in bindingId).IsEmpty, "[UI Toolkit] Could not register binding on element of type '" + element.GetType().Name + "': target property path is empty.");
			DataBindingManager.BindingDataCollection bindingDataCollection;
			DataBindingManager.BindingData bindingData;
			bool flag = this.m_BindingsTracker.TryGetBindingCollection(element, out bindingDataCollection) && bindingDataCollection.TryGetBindingData(in bindingId, out bindingData);
			BindingActivationContext bindingActivationContext;
			if (flag)
			{
				Binding binding2 = bindingData.binding;
				bindingActivationContext = new BindingActivationContext(element, in bindingId);
				binding2.OnDeactivated(in bindingActivationContext);
				DataSourceContext resolvedDataSourceContext = this.m_DataSourceTracker.GetResolvedDataSourceContext(element, bindingData);
				IDataSourceProvider dataSourceProvider = bindingData.binding as IDataSourceProvider;
				object obj = ((dataSourceProvider != null) ? dataSourceProvider.dataSource : null);
				PropertyPath propertyPath = ((dataSourceProvider != null) ? dataSourceProvider.dataSourcePath : default(PropertyPath));
				bool flag2 = resolvedDataSourceContext.dataSource != obj || resolvedDataSourceContext.dataSourcePath != propertyPath;
				if (flag2)
				{
					Binding binding3 = bindingData.binding;
					DataSourceContext dataSourceContext = new DataSourceContext(obj, in propertyPath);
					DataSourceContextChanged dataSourceContextChanged = new DataSourceContextChanged(element, in bindingId, in resolvedDataSourceContext, in dataSourceContext);
					binding3.OnDataSourceChanged(in dataSourceContextChanged);
				}
				this.m_DataSourceTracker.DecreaseBindingRefCount(ref bindingData);
			}
			DataBindingManager.BindingData pooledBindingData = this.GetPooledBindingData(new BindingTarget(element, in bindingId), binding);
			this.m_DataSourceTracker.IncreaseBindingRefCount(ref pooledBindingData);
			this.m_BindingsTracker.StartTrackingBinding(element, pooledBindingData);
			bindingActivationContext = new BindingActivationContext(element, in bindingId);
			binding.OnActivated(in bindingActivationContext);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00008B88 File Offset: 0x00006D88
		internal void UnregisterBinding(VisualElement element, in BindingId bindingId)
		{
			DataBindingManager.BindingDataCollection bindingDataCollection;
			bool flag = !this.m_BindingsTracker.TryGetBindingCollection(element, out bindingDataCollection);
			if (!flag)
			{
				DataBindingManager.BindingData bindingData;
				bool flag2 = bindingDataCollection.TryGetBindingData(in bindingId, out bindingData);
				if (flag2)
				{
					DataSourceContext resolvedDataSourceContext = this.m_DataSourceTracker.GetResolvedDataSourceContext(element, bindingData);
					IDataSourceProvider dataSourceProvider = bindingData.binding as IDataSourceProvider;
					object obj = ((dataSourceProvider != null) ? dataSourceProvider.dataSource : null);
					PropertyPath propertyPath = ((dataSourceProvider != null) ? dataSourceProvider.dataSourcePath : default(PropertyPath));
					bool flag3 = resolvedDataSourceContext.dataSource != obj || resolvedDataSourceContext.dataSourcePath != propertyPath;
					if (flag3)
					{
						Binding binding = bindingData.binding;
						DataSourceContext dataSourceContext = new DataSourceContext(obj, in propertyPath);
						DataSourceContextChanged dataSourceContextChanged = new DataSourceContextChanged(element, in bindingId, in resolvedDataSourceContext, in dataSourceContext);
						binding.OnDataSourceChanged(in dataSourceContextChanged);
					}
					Binding binding2 = bindingData.binding;
					BindingActivationContext bindingActivationContext = new BindingActivationContext(element, in bindingId);
					binding2.OnDeactivated(in bindingActivationContext);
					this.m_DataSourceTracker.DecreaseBindingRefCount(ref bindingData);
					this.m_BindingsTracker.StopTrackingBinding(element, bindingData);
					this.ReleasePoolBindingData(bindingData);
				}
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008C8C File Offset: 0x00006E8C
		internal void TransferBindingRequests(VisualElement element)
		{
			bool flag = !this.m_BindingsTracker.IsTrackingElement(element);
			if (!flag)
			{
				DataBindingManager.BindingDataCollection bindingDataCollection;
				bool flag2 = this.m_BindingsTracker.TryGetBindingCollection(element, out bindingDataCollection);
				if (flag2)
				{
					List<DataBindingManager.BindingData> bindings = bindingDataCollection.GetBindings();
					while (bindings.Count > 0)
					{
						List<DataBindingManager.BindingData> list = bindings;
						DataBindingManager.BindingData bindingData = list[list.Count - 1];
						DataBindingManager.CreateBindingRequest(element, in bindingData.target.bindingId, bindingData.binding, true);
						this.UnregisterBinding(element, in bindingData.target.bindingId);
					}
				}
				this.m_BindingsTracker.StopTrackingElement(element);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00008D2A File Offset: 0x00006F2A
		public void InvalidateCachedDataSource(HashSet<VisualElement> addedOrMovedElements, HashSet<VisualElement> removedElements)
		{
			this.m_DataSourceTracker.InvalidateCachedDataSource(addedOrMovedElements, removedElements);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00008D3B File Offset: 0x00006F3B
		public void Dispose()
		{
			this.m_BindingsTracker.Dispose();
			this.m_DataSourceTracker.Dispose();
			this.m_DetectedChangesFromUI.Clear();
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00008D62 File Offset: 0x00006F62
		public static void CreateBindingRequest(VisualElement target, in BindingId bindingId, Binding binding)
		{
			DataBindingManager.CreateBindingRequest(target, in bindingId, binding, false);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00008D70 File Offset: 0x00006F70
		private static void CreateBindingRequest(VisualElement target, in BindingId bindingId, Binding binding, bool isTransferring)
		{
			List<DataBindingManager.BindingRequest> list = (List<DataBindingManager.BindingRequest>)target.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = list == null;
			if (flag)
			{
				list = new List<DataBindingManager.BindingRequest>();
				target.SetProperty(DataBindingManager.k_RequestBindingPropertyName, list);
			}
			bool flag2 = true;
			for (int i = 0; i < list.Count; i++)
			{
				DataBindingManager.BindingRequest bindingRequest = list[i];
				bool flag3 = (in bindingRequest.bindingId) == (in bindingId);
				if (flag3)
				{
					if (isTransferring)
					{
						flag2 = false;
					}
					else
					{
						list[i] = bindingRequest.CancelRequest();
					}
				}
			}
			list.Add(new DataBindingManager.BindingRequest(in bindingId, binding, flag2));
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00008E0E File Offset: 0x0000700E
		public static void CreateClearAllBindingsRequest(VisualElement target)
		{
			DataBindingManager.CreateBindingRequest(target, in DataBindingManager.k_ClearBindingsToken, null);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008E20 File Offset: 0x00007020
		public void ProcessBindingRequests(VisualElement element)
		{
			List<DataBindingManager.BindingRequest> list = (List<DataBindingManager.BindingRequest>)element.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = list == null;
			if (!flag)
			{
				for (int i = 0; i < list.Count; i++)
				{
					DataBindingManager.BindingRequest bindingRequest = list[i];
					bool flag2 = !bindingRequest.shouldProcess;
					if (!flag2)
					{
						bool flag3 = (in bindingRequest.bindingId) == (in DataBindingManager.k_ClearBindingsToken);
						if (flag3)
						{
							this.ClearAllBindings(element);
						}
						else
						{
							bool flag4 = (in bindingRequest.bindingId) == (in BindingId.Invalid);
							if (flag4)
							{
								IPanel panel = element.panel;
								Panel panel2 = panel as Panel;
								string text = ((panel2 != null) ? panel2.name : null) ?? panel.visualTree.name;
								Debug.LogError(string.Concat(new string[]
								{
									"[UI Toolkit] Trying to set a binding on `",
									string.IsNullOrWhiteSpace(element.name) ? "<no name>" : element.name,
									" (",
									TypeUtility.GetTypeDisplayName(element.GetType()),
									")` without setting the \"property\" attribute is not supported (",
									text,
									")."
								}));
							}
							else
							{
								bool flag5 = bindingRequest.binding != null;
								if (flag5)
								{
									this.RegisterBinding(element, in bindingRequest.bindingId, bindingRequest.binding);
								}
								else
								{
									this.UnregisterBinding(element, in bindingRequest.bindingId);
								}
							}
						}
					}
				}
				list.Clear();
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00008F94 File Offset: 0x00007194
		private void ClearAllBindings(VisualElement element)
		{
			List<DataBindingManager.BindingData> list = CollectionPool<List<DataBindingManager.BindingData>, DataBindingManager.BindingData>.Get();
			try
			{
				list.AddRange(this.GetBindingData(element));
				foreach (DataBindingManager.BindingData bindingData in list)
				{
					this.UnregisterBinding(element, in bindingData.target.bindingId);
				}
			}
			finally
			{
				CollectionPool<List<DataBindingManager.BindingData>, DataBindingManager.BindingData>.Release(list);
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009024 File Offset: 0x00007224
		internal static bool AnyPendingBindingRequests(VisualElement element)
		{
			List<DataBindingManager.BindingRequest> list = (List<DataBindingManager.BindingRequest>)element.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = list == null;
			return !flag && list.Count > 0;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000905C File Offset: 0x0000725C
		[return: TupleElementNames(new string[] { "binding", "bindingId" })]
		internal static IEnumerable<ValueTuple<Binding, BindingId>> GetBindingRequests(VisualElement element)
		{
			List<DataBindingManager.BindingRequest> requests = (List<DataBindingManager.BindingRequest>)element.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = requests == null;
			if (flag)
			{
				yield break;
			}
			HashSet<BindingId> visited = CollectionPool<HashSet<BindingId>, BindingId>.Get();
			try
			{
				int num;
				for (int i = requests.Count - 1; i >= 0; i = num)
				{
					DataBindingManager.BindingRequest request = requests[i];
					bool flag2 = visited.Add(request.bindingId);
					if (flag2)
					{
						yield return new ValueTuple<Binding, BindingId>(request.binding, request.bindingId);
					}
					request = default(DataBindingManager.BindingRequest);
					num = i - 1;
				}
			}
			finally
			{
				CollectionPool<HashSet<BindingId>, BindingId>.Release(visited);
			}
			yield break;
			yield break;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000906C File Offset: 0x0000726C
		internal static bool TryGetBindingRequest(VisualElement element, in BindingId bindingId, out Binding binding)
		{
			List<DataBindingManager.BindingRequest> list = (List<DataBindingManager.BindingRequest>)element.GetProperty(DataBindingManager.k_RequestBindingPropertyName);
			bool flag = list == null;
			bool flag2;
			if (flag)
			{
				binding = null;
				flag2 = false;
			}
			else
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					DataBindingManager.BindingRequest bindingRequest = list[i];
					bool flag3 = (in bindingId) != (in bindingRequest.bindingId);
					if (!flag3)
					{
						binding = bindingRequest.binding;
						return true;
					}
				}
				binding = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000090EB File Offset: 0x000072EB
		public void DirtyBindingOrder()
		{
			this.m_BindingsTracker.SetDirty();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000090FA File Offset: 0x000072FA
		public void TrackDataSource(object previous, object current)
		{
			this.m_DataSourceTracker.DecreaseRefCount(previous);
			this.m_DataSourceTracker.IncreaseRefCount(current);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009118 File Offset: 0x00007318
		[return: TupleElementNames(new string[] { "boundElementsCount", "trackedDataSourcesCount" })]
		internal ValueTuple<int, int> GetTrackedInfo()
		{
			int trackedElementsCount = this.m_BindingsTracker.GetTrackedElementsCount();
			int trackedDataSourcesCount = this.m_DataSourceTracker.GetTrackedDataSourcesCount();
			return new ValueTuple<int, int>(trackedElementsCount, trackedDataSourcesCount);
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00009149 File Offset: 0x00007349
		public void ClearSourceCache()
		{
			this.m_DataSourceTracker.ClearSourceCache();
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00009158 File Offset: 0x00007358
		public DataBindingManager.BindingData GetPooledBindingData(BindingTarget target, Binding binding)
		{
			bool flag = this.m_BindingDataLocalPool.Count > 0;
			DataBindingManager.BindingData bindingData;
			if (flag)
			{
				List<DataBindingManager.BindingData> bindingDataLocalPool = this.m_BindingDataLocalPool;
				bindingData = bindingDataLocalPool[bindingDataLocalPool.Count - 1];
				this.m_BindingDataLocalPool.RemoveAt(this.m_BindingDataLocalPool.Count - 1);
			}
			else
			{
				bindingData = new DataBindingManager.BindingData();
			}
			bindingData.target = target;
			bindingData.binding = binding;
			return bindingData;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000091C2 File Offset: 0x000073C2
		public void ReleasePoolBindingData(DataBindingManager.BindingData data)
		{
			data.Reset();
			this.m_BindingDataLocalPool.Add(data);
		}

		// Token: 0x0400011A RID: 282
		private readonly List<DataBindingManager.BindingData> m_BindingDataLocalPool = new List<DataBindingManager.BindingData>(64);

		// Token: 0x0400011B RID: 283
		private static readonly PropertyName k_RequestBindingPropertyName = "__unity-binding-request";

		// Token: 0x0400011C RID: 284
		private static readonly BindingId k_ClearBindingsToken = "$__BindingManager--ClearAllBindings";

		// Token: 0x0400011D RID: 285
		internal static BindingLogLevel globalLogLevel = BindingLogLevel.All;

		// Token: 0x0400011E RID: 286
		private BindingLogLevel? m_LogLevel;

		// Token: 0x0400011F RID: 287
		private static readonly List<DataBindingManager.BindingData> s_Empty = new List<DataBindingManager.BindingData>();

		// Token: 0x04000120 RID: 288
		private readonly BaseVisualElementPanel m_Panel;

		// Token: 0x04000121 RID: 289
		private readonly DataBindingManager.HierarchyDataSourceTracker m_DataSourceTracker;

		// Token: 0x04000122 RID: 290
		private readonly DataBindingManager.HierarchyBindingTracker m_BindingsTracker;

		// Token: 0x04000123 RID: 291
		private readonly List<DataBindingManager.ChangesFromUI> m_DetectedChangesFromUI;

		// Token: 0x04000124 RID: 292
		private DataBindingManager.IgnoreUIChangesData m_IgnoreUIChangesData;

		// Token: 0x02000034 RID: 52
		private readonly struct BindingRequest
		{
			// Token: 0x060001EB RID: 491 RVA: 0x00009209 File Offset: 0x00007409
			public BindingRequest(in BindingId bindingId, Binding binding, bool shouldProcess = true)
			{
				this.bindingId = bindingId;
				this.binding = binding;
				this.shouldProcess = shouldProcess;
			}

			// Token: 0x060001EC RID: 492 RVA: 0x00009228 File Offset: 0x00007428
			public DataBindingManager.BindingRequest CancelRequest()
			{
				return new DataBindingManager.BindingRequest(in this.bindingId, this.binding, false);
			}

			// Token: 0x04000125 RID: 293
			public readonly BindingId bindingId;

			// Token: 0x04000126 RID: 294
			public readonly Binding binding;

			// Token: 0x04000127 RID: 295
			public readonly bool shouldProcess;
		}

		// Token: 0x02000035 RID: 53
		private struct BindingDataCollection : IDisposable
		{
			// Token: 0x060001ED RID: 493 RVA: 0x0000924C File Offset: 0x0000744C
			public static DataBindingManager.BindingDataCollection Create()
			{
				return new DataBindingManager.BindingDataCollection
				{
					m_BindingPerId = CollectionPool<Dictionary<BindingId, DataBindingManager.BindingData>, KeyValuePair<BindingId, DataBindingManager.BindingData>>.Get(),
					m_Bindings = CollectionPool<List<DataBindingManager.BindingData>, DataBindingManager.BindingData>.Get()
				};
			}

			// Token: 0x060001EE RID: 494 RVA: 0x00009280 File Offset: 0x00007480
			public void AddBindingData(DataBindingManager.BindingData bindingData)
			{
				DataBindingManager.BindingData bindingData2;
				bool flag = this.m_BindingPerId.TryGetValue(bindingData.target.bindingId, out bindingData2);
				if (flag)
				{
					this.m_Bindings.Remove(bindingData2);
				}
				this.m_BindingPerId[bindingData.target.bindingId] = bindingData;
				this.m_Bindings.Add(bindingData);
			}

			// Token: 0x060001EF RID: 495 RVA: 0x000092E0 File Offset: 0x000074E0
			public bool TryGetBindingData(in BindingId bindingId, out DataBindingManager.BindingData data)
			{
				return this.m_BindingPerId.TryGetValue(bindingId, out data);
			}

			// Token: 0x060001F0 RID: 496 RVA: 0x00009304 File Offset: 0x00007504
			public bool RemoveBindingData(DataBindingManager.BindingData bindingData)
			{
				DataBindingManager.BindingData bindingData2;
				bool flag = !this.m_BindingPerId.TryGetValue(bindingData.target.bindingId, out bindingData2);
				return !flag && this.m_Bindings.Remove(bindingData2) && this.m_BindingPerId.Remove(bindingData2.target.bindingId);
			}

			// Token: 0x060001F1 RID: 497 RVA: 0x00009360 File Offset: 0x00007560
			public List<DataBindingManager.BindingData> GetBindings()
			{
				return this.m_Bindings;
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x00009378 File Offset: 0x00007578
			public int GetBindingCount()
			{
				return this.m_Bindings.Count;
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x00009398 File Offset: 0x00007598
			public void Dispose()
			{
				bool flag = this.m_BindingPerId != null;
				if (flag)
				{
					CollectionPool<Dictionary<BindingId, DataBindingManager.BindingData>, KeyValuePair<BindingId, DataBindingManager.BindingData>>.Release(this.m_BindingPerId);
				}
				this.m_BindingPerId = null;
				bool flag2 = this.m_Bindings != null;
				if (flag2)
				{
					CollectionPool<List<DataBindingManager.BindingData>, DataBindingManager.BindingData>.Release(this.m_Bindings);
				}
				this.m_Bindings = null;
			}

			// Token: 0x04000128 RID: 296
			private Dictionary<BindingId, DataBindingManager.BindingData> m_BindingPerId;

			// Token: 0x04000129 RID: 297
			private List<DataBindingManager.BindingData> m_Bindings;
		}

		// Token: 0x02000036 RID: 54
		internal class BindingData
		{
			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060001F4 RID: 500 RVA: 0x000093E6 File Offset: 0x000075E6
			// (set) Token: 0x060001F5 RID: 501 RVA: 0x000093EE File Offset: 0x000075EE
			public object localDataSource { get; set; }

			// Token: 0x060001F6 RID: 502 RVA: 0x000093F8 File Offset: 0x000075F8
			public void Reset()
			{
				this.version += 1L;
				this.target = default(BindingTarget);
				this.binding = null;
				this.localDataSource = null;
				this.m_LastContext = default(DataSourceContext);
				this.m_SourceToUILastUpdate = null;
				this.m_UIToSourceLastUpdate = null;
			}

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060001F7 RID: 503 RVA: 0x00009454 File Offset: 0x00007654
			// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000945C File Offset: 0x0000765C
			public DataSourceContext context
			{
				get
				{
					return this.m_LastContext;
				}
				set
				{
					bool flag = this.m_LastContext.dataSource == value.dataSource && this.m_LastContext.dataSourcePath == value.dataSourcePath;
					if (!flag)
					{
						DataSourceContext lastContext = this.m_LastContext;
						this.m_LastContext = value;
						Binding binding = this.binding;
						DataSourceContextChanged dataSourceContextChanged = new DataSourceContextChanged(this.target.element, in this.target.bindingId, in lastContext, in value);
						binding.OnDataSourceChanged(in dataSourceContextChanged);
						this.binding.MarkDirty();
					}
				}
			}

			// Token: 0x0400012A RID: 298
			public long version;

			// Token: 0x0400012B RID: 299
			public BindingTarget target;

			// Token: 0x0400012C RID: 300
			public Binding binding;

			// Token: 0x0400012D RID: 301
			private DataSourceContext m_LastContext;

			// Token: 0x0400012F RID: 303
			public BindingResult? m_SourceToUILastUpdate;

			// Token: 0x04000130 RID: 304
			public BindingResult? m_UIToSourceLastUpdate;
		}

		// Token: 0x02000037 RID: 55
		internal readonly struct ChangesFromUI
		{
			// Token: 0x060001FA RID: 506 RVA: 0x000094E6 File Offset: 0x000076E6
			public ChangesFromUI(DataBindingManager.BindingData bindingData)
			{
				this.bindingData = bindingData;
				this.version = bindingData.version;
				this.binding = bindingData.binding;
			}

			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060001FB RID: 507 RVA: 0x00009508 File Offset: 0x00007708
			public bool IsValid
			{
				get
				{
					return this.version == this.bindingData.version && this.binding == this.bindingData.binding;
				}
			}

			// Token: 0x04000131 RID: 305
			public readonly long version;

			// Token: 0x04000132 RID: 306
			public readonly Binding binding;

			// Token: 0x04000133 RID: 307
			public readonly DataBindingManager.BindingData bindingData;
		}

		// Token: 0x02000038 RID: 56
		private class HierarchyBindingTracker : IDisposable
		{
			// Token: 0x060001FC RID: 508 RVA: 0x00009534 File Offset: 0x00007734
			public int GetTrackedElementsCount()
			{
				return this.m_BoundElements.Count;
			}

			// Token: 0x060001FD RID: 509 RVA: 0x00009554 File Offset: 0x00007754
			public List<VisualElement> GetBoundElements()
			{
				bool isDirty = this.m_IsDirty;
				if (isDirty)
				{
					this.OrderBindings(this.m_Panel.visualTree);
				}
				return this.m_OrderedBindings;
			}

			// Token: 0x060001FE RID: 510 RVA: 0x00009588 File Offset: 0x00007788
			public IEnumerable<VisualElement> GetUnorderedBoundElements()
			{
				return this.m_BoundElements;
			}

			// Token: 0x060001FF RID: 511 RVA: 0x000095A0 File Offset: 0x000077A0
			public HierarchyBindingTracker(BaseVisualElementPanel panel)
			{
				this.m_Panel = panel;
				this.m_BindingSorter = new DataBindingManager.HierarchyBindingTracker.HierarchicalBindingsSorter();
				this.m_BindingDataPerElement = new Dictionary<VisualElement, DataBindingManager.BindingDataCollection>();
				this.m_BoundElements = new HashSet<VisualElement>();
				this.m_OrderedBindings = new List<VisualElement>();
				this.m_IsDirty = true;
				this.m_OnPropertyChanged = new EventCallback<PropertyChangedEvent, Dictionary<VisualElement, DataBindingManager.BindingDataCollection>>(this.OnPropertyChanged);
			}

			// Token: 0x06000200 RID: 512 RVA: 0x00009601 File Offset: 0x00007801
			public void SetDirty()
			{
				this.m_IsDirty = true;
			}

			// Token: 0x06000201 RID: 513 RVA: 0x0000960C File Offset: 0x0000780C
			public bool TryGetBindingCollection(VisualElement element, out DataBindingManager.BindingDataCollection collection)
			{
				return this.m_BindingDataPerElement.TryGetValue(element, out collection);
			}

			// Token: 0x06000202 RID: 514 RVA: 0x0000962C File Offset: 0x0000782C
			public bool IsTrackingElement(VisualElement element)
			{
				return this.m_BoundElements.Contains(element);
			}

			// Token: 0x06000203 RID: 515 RVA: 0x0000964C File Offset: 0x0000784C
			public void StartTrackingBinding(VisualElement element, DataBindingManager.BindingData binding)
			{
				bool flag = this.m_BoundElements.Add(element);
				DataBindingManager.BindingDataCollection bindingDataCollection;
				if (flag)
				{
					bindingDataCollection = DataBindingManager.BindingDataCollection.Create();
					this.m_BindingDataPerElement.Add(element, bindingDataCollection);
					element.RegisterCallback<PropertyChangedEvent, Dictionary<VisualElement, DataBindingManager.BindingDataCollection>>(this.m_OnPropertyChanged, this.m_BindingDataPerElement, TrickleDown.NoTrickleDown);
				}
				else
				{
					bool flag2 = !this.m_BindingDataPerElement.TryGetValue(element, out bindingDataCollection);
					if (flag2)
					{
						throw new InvalidOperationException("Trying to add a binding to an element which doesn't have a binding collection. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
				}
				binding.binding.MarkDirty();
				bindingDataCollection.AddBindingData(binding);
				this.m_BindingDataPerElement[element] = bindingDataCollection;
				this.SetDirty();
			}

			// Token: 0x06000204 RID: 516 RVA: 0x000096E4 File Offset: 0x000078E4
			private void OnPropertyChanged(PropertyChangedEvent evt, Dictionary<VisualElement, DataBindingManager.BindingDataCollection> bindingCollection)
			{
				VisualElement visualElement = evt.target as VisualElement;
				bool flag = visualElement == null;
				if (flag)
				{
					throw new InvalidOperationException("Trying to track property changes on a non 'VisualElement'. This is an internal bug. Please report using `Help > Report a Bug...`");
				}
				DataBindingManager.BindingDataCollection bindingDataCollection;
				bool flag2 = !bindingCollection.TryGetValue(visualElement, out bindingDataCollection);
				if (flag2)
				{
					throw new InvalidOperationException("Trying to track property changes on a 'VisualElement' that is not being tracked. This is an internal bug. Please report using `Help > Report a Bug...`");
				}
				BindingId property = evt.property;
				DataBindingManager.BindingData bindingData;
				Binding binding;
				bool flag3 = bindingDataCollection.TryGetBindingData(in property, out bindingData) && visualElement.TryGetBinding(evt.property, out binding) && bindingData.binding == binding;
				if (flag3)
				{
					bool flag4 = !this.m_Panel.dataBindingManager.m_IgnoreUIChangesData.ShouldIgnoreChange(visualElement, binding, evt.property);
					if (flag4)
					{
						this.m_Panel.dataBindingManager.m_DetectedChangesFromUI.Add(new DataBindingManager.ChangesFromUI(bindingData));
					}
				}
			}

			// Token: 0x06000205 RID: 517 RVA: 0x000097B0 File Offset: 0x000079B0
			public void StopTrackingBinding(VisualElement element, DataBindingManager.BindingData binding)
			{
				DataBindingManager.BindingDataCollection bindingDataCollection;
				bool flag = this.m_BoundElements.Contains(element) && this.m_BindingDataPerElement.TryGetValue(element, out bindingDataCollection);
				if (flag)
				{
					bindingDataCollection.RemoveBindingData(binding);
					bool flag2 = bindingDataCollection.GetBindingCount() == 0;
					if (flag2)
					{
						this.StopTrackingElement(element);
						element.UnregisterCallback<PropertyChangedEvent, Dictionary<VisualElement, DataBindingManager.BindingDataCollection>>(this.m_OnPropertyChanged, TrickleDown.NoTrickleDown);
					}
					else
					{
						this.m_BindingDataPerElement[element] = bindingDataCollection;
					}
					this.SetDirty();
					return;
				}
				throw new InvalidOperationException("Trying to remove a binding to an element which doesn't have a binding collection. This is an internal bug. Please report using `Help > Report a Bug...`");
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000983C File Offset: 0x00007A3C
			public void StopTrackingElement(VisualElement element)
			{
				DataBindingManager.BindingDataCollection bindingDataCollection;
				bool flag = this.m_BindingDataPerElement.TryGetValue(element, out bindingDataCollection);
				if (flag)
				{
					bindingDataCollection.Dispose();
				}
				this.m_BindingDataPerElement.Remove(element);
				this.m_BoundElements.Remove(element);
				this.SetDirty();
			}

			// Token: 0x06000207 RID: 519 RVA: 0x00009888 File Offset: 0x00007A88
			public void Dispose()
			{
				foreach (KeyValuePair<VisualElement, DataBindingManager.BindingDataCollection> keyValuePair in this.m_BindingDataPerElement)
				{
					keyValuePair.Value.Dispose();
				}
				this.m_BindingDataPerElement.Clear();
				this.m_BoundElements.Clear();
				this.m_OrderedBindings.Clear();
			}

			// Token: 0x06000208 RID: 520 RVA: 0x00009910 File Offset: 0x00007B10
			private void OrderBindings(VisualElement root)
			{
				this.m_OrderedBindings.Clear();
				this.m_BindingSorter.boundElements = this.m_BoundElements;
				this.m_BindingSorter.results = this.m_OrderedBindings;
				this.m_BindingSorter.Traverse(root);
				this.m_IsDirty = false;
			}

			// Token: 0x04000134 RID: 308
			private readonly BaseVisualElementPanel m_Panel;

			// Token: 0x04000135 RID: 309
			private readonly DataBindingManager.HierarchyBindingTracker.HierarchicalBindingsSorter m_BindingSorter;

			// Token: 0x04000136 RID: 310
			private readonly Dictionary<VisualElement, DataBindingManager.BindingDataCollection> m_BindingDataPerElement;

			// Token: 0x04000137 RID: 311
			private readonly HashSet<VisualElement> m_BoundElements;

			// Token: 0x04000138 RID: 312
			private readonly List<VisualElement> m_OrderedBindings;

			// Token: 0x04000139 RID: 313
			private bool m_IsDirty;

			// Token: 0x0400013A RID: 314
			private EventCallback<PropertyChangedEvent, Dictionary<VisualElement, DataBindingManager.BindingDataCollection>> m_OnPropertyChanged;

			// Token: 0x02000039 RID: 57
			private class HierarchicalBindingsSorter : HierarchyTraversal
			{
				// Token: 0x17000046 RID: 70
				// (get) Token: 0x06000209 RID: 521 RVA: 0x00009962 File Offset: 0x00007B62
				// (set) Token: 0x0600020A RID: 522 RVA: 0x0000996A File Offset: 0x00007B6A
				public HashSet<VisualElement> boundElements { get; set; }

				// Token: 0x17000047 RID: 71
				// (get) Token: 0x0600020B RID: 523 RVA: 0x00009973 File Offset: 0x00007B73
				// (set) Token: 0x0600020C RID: 524 RVA: 0x0000997B File Offset: 0x00007B7B
				public List<VisualElement> results { get; set; }

				// Token: 0x0600020D RID: 525 RVA: 0x00009984 File Offset: 0x00007B84
				public override void TraverseRecursive(VisualElement element, int depth)
				{
					bool flag = this.boundElements.Count == this.results.Count;
					if (!flag)
					{
						bool flag2 = this.boundElements.Contains(element);
						if (flag2)
						{
							this.results.Add(element);
						}
						base.Recurse(element, depth);
					}
				}
			}
		}

		// Token: 0x0200003A RID: 58
		private class HierarchyDataSourceTracker : IDisposable
		{
			// Token: 0x0600020F RID: 527 RVA: 0x000099E0 File Offset: 0x00007BE0
			private DataBindingManager.HierarchyDataSourceTracker.SourceInfo GetPooledSourceInfo()
			{
				bool flag = this.m_SourceInfosPool.Count > 0;
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
				if (flag)
				{
					List<DataBindingManager.HierarchyDataSourceTracker.SourceInfo> sourceInfosPool = this.m_SourceInfosPool;
					sourceInfo = sourceInfosPool[sourceInfosPool.Count - 1];
					this.m_SourceInfosPool.RemoveAt(this.m_SourceInfosPool.Count - 1);
				}
				else
				{
					sourceInfo = new DataBindingManager.HierarchyDataSourceTracker.SourceInfo();
				}
				return sourceInfo;
			}

			// Token: 0x06000210 RID: 528 RVA: 0x00009A3C File Offset: 0x00007C3C
			private void ReleasePooledSourceInfo(DataBindingManager.HierarchyDataSourceTracker.SourceInfo info)
			{
				info.lastVersion = long.MinValue;
				info.refCount = 0;
				HashSet<PropertyPath> detectedChangesNoAlloc = info.detectedChangesNoAlloc;
				if (detectedChangesNoAlloc != null)
				{
					detectedChangesNoAlloc.Clear();
				}
				this.m_SourceInfosPool.Add(info);
			}

			// Token: 0x06000211 RID: 529 RVA: 0x00009A78 File Offset: 0x00007C78
			public HierarchyDataSourceTracker(DataBindingManager manager)
			{
				this.m_DataBindingManager = manager;
				this.m_ResolvedHierarchicalDataSourceContext = new Dictionary<VisualElement, DataSourceContext>();
				this.m_BindingRefCount = new Dictionary<Binding, int>();
				DataBindingManager.HierarchyDataSourceTracker.ObjectComparer objectComparer = new DataBindingManager.HierarchyDataSourceTracker.ObjectComparer();
				this.m_SourceInfos = new Dictionary<object, DataBindingManager.HierarchyDataSourceTracker.SourceInfo>(objectComparer);
				this.m_SourcesToRemove = new HashSet<object>(objectComparer);
				this.m_InvalidateResolvedDataSources = new DataBindingManager.HierarchyDataSourceTracker.InvalidateDataSourcesTraversal(this);
				this.m_Handler = new EventHandler<BindablePropertyChangedEventArgs>(this.TrackPropertyChanges);
				this.m_VisualElementHandler = new EventCallback<PropertyChangedEvent, VisualElement>(this.OnVisualElementPropertyChanged);
			}

			// Token: 0x06000212 RID: 530 RVA: 0x00009B04 File Offset: 0x00007D04
			internal void IncreaseBindingRefCount(ref DataBindingManager.BindingData bindingData)
			{
				Binding binding = bindingData.binding;
				bool flag = binding == null;
				if (!flag)
				{
					int num;
					bool flag2 = !this.m_BindingRefCount.TryGetValue(binding, out num);
					if (flag2)
					{
						num = 0;
					}
					IDataSourceProvider dataSourceProvider = binding as IDataSourceProvider;
					bool flag3 = dataSourceProvider != null;
					if (flag3)
					{
						this.IncreaseRefCount(dataSourceProvider.dataSource);
						bindingData.localDataSource = dataSourceProvider.dataSource;
					}
					this.m_BindingRefCount[binding] = num + 1;
				}
			}

			// Token: 0x06000213 RID: 531 RVA: 0x00009B80 File Offset: 0x00007D80
			internal void DecreaseBindingRefCount(ref DataBindingManager.BindingData bindingData)
			{
				Binding binding = bindingData.binding;
				bool flag = binding == null;
				if (!flag)
				{
					int num;
					bool flag2 = !this.m_BindingRefCount.TryGetValue(binding, out num);
					if (flag2)
					{
						throw new InvalidOperationException("Trying to release a binding that isn't tracked. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
					bool flag3 = num == 1;
					if (flag3)
					{
						this.m_BindingRefCount.Remove(binding);
					}
					else
					{
						this.m_BindingRefCount[binding] = num - 1;
					}
					IDataSourceProvider dataSourceProvider = binding as IDataSourceProvider;
					bool flag4 = dataSourceProvider != null;
					if (flag4)
					{
						this.DecreaseRefCount(dataSourceProvider.dataSource);
					}
				}
			}

			// Token: 0x06000214 RID: 532 RVA: 0x00009C10 File Offset: 0x00007E10
			internal void IncreaseRefCount(object dataSource)
			{
				bool flag = dataSource == null;
				if (!flag)
				{
					bool flag2 = this.m_SourcesToRemove.Remove(dataSource);
					DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
					bool flag3 = !this.m_SourceInfos.TryGetValue(dataSource, out sourceInfo);
					if (flag3)
					{
						sourceInfo = (this.m_SourceInfos[dataSource] = this.GetPooledSourceInfo());
						flag2 = true;
					}
					bool flag4 = flag2;
					if (flag4)
					{
						INotifyBindablePropertyChanged notifyBindablePropertyChanged = dataSource as INotifyBindablePropertyChanged;
						bool flag5 = notifyBindablePropertyChanged != null;
						if (flag5)
						{
							notifyBindablePropertyChanged.propertyChanged += this.m_Handler;
						}
						VisualElement visualElement = dataSource as VisualElement;
						bool flag6 = visualElement != null;
						if (flag6)
						{
							visualElement.RegisterCallback<PropertyChangedEvent, VisualElement>(this.m_VisualElementHandler, visualElement, TrickleDown.NoTrickleDown);
						}
					}
					DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo2 = sourceInfo;
					int num = sourceInfo2.refCount + 1;
					sourceInfo2.refCount = num;
				}
			}

			// Token: 0x06000215 RID: 533 RVA: 0x00009CC8 File Offset: 0x00007EC8
			private void OnVisualElementPropertyChanged(PropertyChangedEvent evt, VisualElement element)
			{
				BindingId property = evt.property;
				this.TrackPropertyChanges(element, in property);
			}

			// Token: 0x06000216 RID: 534 RVA: 0x00009CEC File Offset: 0x00007EEC
			internal void DecreaseRefCount(object dataSource)
			{
				bool flag = dataSource == null;
				if (!flag)
				{
					DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
					bool flag2 = !this.m_SourceInfos.TryGetValue(dataSource, out sourceInfo) || sourceInfo.refCount == 0;
					if (flag2)
					{
						throw new InvalidOperationException("Trying to release a data source that isn't tracked. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
					bool flag3 = sourceInfo.refCount == 1;
					if (flag3)
					{
						sourceInfo.refCount = 0;
						this.m_SourcesToRemove.Add(dataSource);
						INotifyBindablePropertyChanged notifyBindablePropertyChanged = dataSource as INotifyBindablePropertyChanged;
						bool flag4 = notifyBindablePropertyChanged != null;
						if (flag4)
						{
							notifyBindablePropertyChanged.propertyChanged -= this.m_Handler;
						}
						VisualElement visualElement = dataSource as VisualElement;
						bool flag5 = visualElement != null;
						if (flag5)
						{
							visualElement.UnregisterCallback<PropertyChangedEvent, VisualElement>(this.m_VisualElementHandler, TrickleDown.NoTrickleDown);
						}
					}
					else
					{
						DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo2 = sourceInfo;
						int num = sourceInfo2.refCount - 1;
						sourceInfo2.refCount = num;
					}
				}
			}

			// Token: 0x06000217 RID: 535 RVA: 0x00009DB0 File Offset: 0x00007FB0
			public int GetRefCount(object dataSource)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
				return this.m_SourceInfos.TryGetValue(dataSource, out sourceInfo) ? sourceInfo.refCount : 0;
			}

			// Token: 0x06000218 RID: 536 RVA: 0x00009DDC File Offset: 0x00007FDC
			public int GetTrackedDataSourcesCount()
			{
				return this.m_ResolvedHierarchicalDataSourceContext.Count;
			}

			// Token: 0x06000219 RID: 537 RVA: 0x00009DFC File Offset: 0x00007FFC
			public bool IsTrackingDataSource(VisualElement element)
			{
				return this.m_ResolvedHierarchicalDataSourceContext.ContainsKey(element);
			}

			// Token: 0x0600021A RID: 538 RVA: 0x00009E1C File Offset: 0x0000801C
			public HashSet<PropertyPath> GetChangesFromSource(object dataSource)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
				return this.m_SourceInfos.TryGetValue(dataSource, out sourceInfo) ? sourceInfo.detectedChangesNoAlloc : null;
			}

			// Token: 0x0600021B RID: 539 RVA: 0x00009E48 File Offset: 0x00008048
			public void ClearChangesFromSource(object dataSource)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
				bool flag = !this.m_SourceInfos.TryGetValue(dataSource, out sourceInfo);
				if (!flag)
				{
					HashSet<PropertyPath> detectedChangesNoAlloc = sourceInfo.detectedChangesNoAlloc;
					if (detectedChangesNoAlloc != null)
					{
						detectedChangesNoAlloc.Clear();
					}
				}
			}

			// Token: 0x0600021C RID: 540 RVA: 0x00009E80 File Offset: 0x00008080
			public void InvalidateCachedDataSource(HashSet<VisualElement> elements, HashSet<VisualElement> removedElements)
			{
				List<VisualElement> list = CollectionPool<List<VisualElement>, VisualElement>.Get();
				try
				{
					foreach (VisualElement visualElement in elements)
					{
						list.Add(visualElement);
					}
					this.m_InvalidateResolvedDataSources.Invalidate(list, removedElements);
				}
				finally
				{
					CollectionPool<List<VisualElement>, VisualElement>.Release(list);
				}
			}

			// Token: 0x0600021D RID: 541 RVA: 0x00009F00 File Offset: 0x00008100
			public DataSourceContext GetResolvedDataSourceContext(VisualElement element, DataBindingManager.BindingData bindingData)
			{
				object obj = null;
				PropertyPath propertyPath = default(PropertyPath);
				IDataSourceProvider dataSourceProvider = bindingData.binding as IDataSourceProvider;
				bool flag = dataSourceProvider != null;
				if (flag)
				{
					obj = dataSourceProvider.dataSource;
					propertyPath = dataSourceProvider.dataSourcePath;
				}
				object localDataSource = bindingData.localDataSource;
				object obj2 = obj;
				PropertyPath propertyPath2 = propertyPath;
				try
				{
					bool flag2 = obj == null;
					if (flag2)
					{
						this.DecreaseRefCount(localDataSource);
						DataSourceContext hierarchicalDataSourceContext = this.GetHierarchicalDataSourceContext(element);
						obj2 = hierarchicalDataSourceContext.dataSource;
						PropertyPath propertyPath3;
						if (propertyPath.IsEmpty)
						{
							propertyPath3 = hierarchicalDataSourceContext.dataSourcePath;
						}
						else
						{
							PropertyPath dataSourcePath = hierarchicalDataSourceContext.dataSourcePath;
							propertyPath3 = PropertyPath.Combine(in dataSourcePath, in propertyPath);
						}
						propertyPath2 = propertyPath3;
						return new DataSourceContext(obj2, in propertyPath2);
					}
					bool flag3 = obj != localDataSource;
					if (flag3)
					{
						this.DecreaseRefCount(localDataSource);
						this.IncreaseRefCount(obj);
					}
				}
				finally
				{
					bindingData.localDataSource = obj;
					DataSourceContext dataSourceContext = new DataSourceContext(obj2, in propertyPath2);
					bindingData.context = dataSourceContext;
				}
				return new DataSourceContext(obj2, in propertyPath2);
			}

			// Token: 0x0600021E RID: 542 RVA: 0x0000A004 File Offset: 0x00008204
			private void TrackPropertyChanges(object sender, BindablePropertyChangedEventArgs args)
			{
				BindingId propertyName = args.propertyName;
				this.TrackPropertyChanges(sender, in propertyName);
			}

			// Token: 0x0600021F RID: 543 RVA: 0x0000A028 File Offset: 0x00008228
			private void TrackPropertyChanges(object sender, PropertyPath propertyPath)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
				bool flag = !this.m_SourceInfos.TryGetValue(sender, out sourceInfo);
				if (!flag)
				{
					HashSet<PropertyPath> detectedChanges = sourceInfo.detectedChanges;
					detectedChanges.Add(propertyPath);
				}
			}

			// Token: 0x06000220 RID: 544 RVA: 0x0000A05C File Offset: 0x0000825C
			public bool TryGetLastVersion(object source, out long version)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
				bool flag = source != null && this.m_SourceInfos.TryGetValue(source, out sourceInfo);
				bool flag2;
				if (flag)
				{
					version = sourceInfo.lastVersion;
					flag2 = true;
				}
				else
				{
					version = -1L;
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x06000221 RID: 545 RVA: 0x0000A098 File Offset: 0x00008298
			public void UpdateVersion(object source, long version)
			{
				DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo = this.m_SourceInfos[source];
				sourceInfo.lastVersion = version;
				this.m_SourceInfos[source] = sourceInfo;
			}

			// Token: 0x06000222 RID: 546 RVA: 0x0000A0CC File Offset: 0x000082CC
			internal object GetHierarchyDataSource(VisualElement element)
			{
				return this.GetHierarchicalDataSourceContext(element).dataSource;
			}

			// Token: 0x06000223 RID: 547 RVA: 0x0000A0F0 File Offset: 0x000082F0
			internal DataSourceContext GetHierarchicalDataSourceContext(VisualElement element)
			{
				DataSourceContext dataSourceContext;
				bool flag = this.m_ResolvedHierarchicalDataSourceContext.TryGetValue(element, out dataSourceContext);
				DataSourceContext dataSourceContext2;
				if (flag)
				{
					dataSourceContext2 = dataSourceContext;
				}
				else
				{
					VisualElement visualElement = element;
					PropertyPath propertyPath = default(PropertyPath);
					while (visualElement != null)
					{
						PropertyPath propertyPath2 = visualElement.dataSourcePath;
						bool flag2 = !propertyPath2.IsEmpty;
						if (flag2)
						{
							propertyPath2 = visualElement.dataSourcePath;
							propertyPath = PropertyPath.Combine(in propertyPath2, in propertyPath);
						}
						bool flag3 = visualElement.dataSource != null;
						if (flag3)
						{
							object dataSource = visualElement.dataSource;
							return this.m_ResolvedHierarchicalDataSourceContext[element] = new DataSourceContext(dataSource, in propertyPath);
						}
						visualElement = visualElement.hierarchy.parent;
					}
					dataSourceContext2 = (this.m_ResolvedHierarchicalDataSourceContext[element] = new DataSourceContext(null, in propertyPath));
				}
				return dataSourceContext2;
			}

			// Token: 0x06000224 RID: 548 RVA: 0x0000A1C1 File Offset: 0x000083C1
			internal void RemoveHierarchyDataSourceContextFromElement(VisualElement element)
			{
				this.m_ResolvedHierarchicalDataSourceContext.Remove(element);
			}

			// Token: 0x06000225 RID: 549 RVA: 0x0000A1D1 File Offset: 0x000083D1
			public void Dispose()
			{
				this.m_ResolvedHierarchicalDataSourceContext.Clear();
				this.m_BindingRefCount.Clear();
				this.m_SourcesToRemove.Clear();
				this.m_SourceInfosPool.Clear();
				this.m_SourceInfos.Clear();
			}

			// Token: 0x06000226 RID: 550 RVA: 0x0000A210 File Offset: 0x00008410
			public void ClearSourceCache()
			{
				foreach (object obj in this.m_SourcesToRemove)
				{
					DataBindingManager.HierarchyDataSourceTracker.SourceInfo sourceInfo;
					bool flag = this.m_SourceInfos.TryGetValue(obj, out sourceInfo);
					if (!flag)
					{
						throw new InvalidOperationException("Trying to release a data source that isn't tracked. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
					bool flag2 = sourceInfo.refCount == 0;
					if (!flag2)
					{
						throw new InvalidOperationException("Trying to release a data source that is still being referenced. This is an internal bug. Please report using `Help > Report a Bug...`");
					}
					this.m_SourceInfos.Remove(obj);
					this.ReleasePooledSourceInfo(sourceInfo);
				}
				this.m_SourcesToRemove.Clear();
			}

			// Token: 0x0400013D RID: 317
			private readonly List<DataBindingManager.HierarchyDataSourceTracker.SourceInfo> m_SourceInfosPool = new List<DataBindingManager.HierarchyDataSourceTracker.SourceInfo>();

			// Token: 0x0400013E RID: 318
			private readonly DataBindingManager m_DataBindingManager;

			// Token: 0x0400013F RID: 319
			private readonly Dictionary<VisualElement, DataSourceContext> m_ResolvedHierarchicalDataSourceContext;

			// Token: 0x04000140 RID: 320
			private readonly Dictionary<Binding, int> m_BindingRefCount;

			// Token: 0x04000141 RID: 321
			private readonly Dictionary<object, DataBindingManager.HierarchyDataSourceTracker.SourceInfo> m_SourceInfos;

			// Token: 0x04000142 RID: 322
			private readonly HashSet<object> m_SourcesToRemove;

			// Token: 0x04000143 RID: 323
			private readonly DataBindingManager.HierarchyDataSourceTracker.InvalidateDataSourcesTraversal m_InvalidateResolvedDataSources;

			// Token: 0x04000144 RID: 324
			private readonly EventHandler<BindablePropertyChangedEventArgs> m_Handler;

			// Token: 0x04000145 RID: 325
			private readonly EventCallback<PropertyChangedEvent, VisualElement> m_VisualElementHandler;

			// Token: 0x0200003B RID: 59
			private class SourceInfo
			{
				// Token: 0x17000048 RID: 72
				// (get) Token: 0x06000227 RID: 551 RVA: 0x0000A2C4 File Offset: 0x000084C4
				// (set) Token: 0x06000228 RID: 552 RVA: 0x0000A2CC File Offset: 0x000084CC
				public long lastVersion { get; set; }

				// Token: 0x17000049 RID: 73
				// (get) Token: 0x06000229 RID: 553 RVA: 0x0000A2D5 File Offset: 0x000084D5
				// (set) Token: 0x0600022A RID: 554 RVA: 0x0000A2DD File Offset: 0x000084DD
				public int refCount { get; set; }

				// Token: 0x1700004A RID: 74
				// (get) Token: 0x0600022B RID: 555 RVA: 0x0000A2E8 File Offset: 0x000084E8
				public HashSet<PropertyPath> detectedChanges
				{
					get
					{
						HashSet<PropertyPath> hashSet;
						if ((hashSet = this.m_DetectedChanges) == null)
						{
							hashSet = (this.m_DetectedChanges = new HashSet<PropertyPath>());
						}
						return hashSet;
					}
				}

				// Token: 0x1700004B RID: 75
				// (get) Token: 0x0600022C RID: 556 RVA: 0x0000A30D File Offset: 0x0000850D
				public HashSet<PropertyPath> detectedChangesNoAlloc
				{
					get
					{
						return this.m_DetectedChanges;
					}
				}

				// Token: 0x04000146 RID: 326
				private HashSet<PropertyPath> m_DetectedChanges;
			}

			// Token: 0x0200003C RID: 60
			private class InvalidateDataSourcesTraversal : HierarchyTraversal
			{
				// Token: 0x0600022E RID: 558 RVA: 0x0000A315 File Offset: 0x00008515
				public InvalidateDataSourcesTraversal(DataBindingManager.HierarchyDataSourceTracker dataSourceTracker)
				{
					this.m_DataSourceTracker = dataSourceTracker;
					this.m_VisitedElements = new HashSet<VisualElement>();
				}

				// Token: 0x0600022F RID: 559 RVA: 0x0000A334 File Offset: 0x00008534
				public void Invalidate(List<VisualElement> addedOrMovedElements, HashSet<VisualElement> removedElements)
				{
					this.m_VisitedElements.Clear();
					for (int i = 0; i < addedOrMovedElements.Count; i++)
					{
						VisualElement visualElement = addedOrMovedElements[i];
						this.Traverse(visualElement);
					}
					foreach (VisualElement visualElement2 in removedElements)
					{
						bool flag = this.m_VisitedElements.Contains(visualElement2);
						if (!flag)
						{
							this.Traverse(visualElement2);
						}
					}
				}

				// Token: 0x06000230 RID: 560 RVA: 0x0000A3D4 File Offset: 0x000085D4
				public override void TraverseRecursive(VisualElement element, int depth)
				{
					bool flag = this.m_VisitedElements.Contains(element);
					if (!flag)
					{
						bool flag2 = depth > 0 && element.dataSource != null;
						if (!flag2)
						{
							this.m_VisitedElements.Add(element);
							this.m_DataSourceTracker.RemoveHierarchyDataSourceContextFromElement(element);
							base.Recurse(element, depth);
						}
					}
				}

				// Token: 0x04000149 RID: 329
				private readonly DataBindingManager.HierarchyDataSourceTracker m_DataSourceTracker;

				// Token: 0x0400014A RID: 330
				private readonly HashSet<VisualElement> m_VisitedElements;
			}

			// Token: 0x0200003D RID: 61
			private class ObjectComparer : IEqualityComparer<object>
			{
				// Token: 0x06000231 RID: 561 RVA: 0x0000A430 File Offset: 0x00008630
				bool IEqualityComparer<object>.Equals(object x, object y)
				{
					return x == y || EqualityComparer<object>.Default.Equals(x, y);
				}

				// Token: 0x06000232 RID: 562 RVA: 0x0000A458 File Offset: 0x00008658
				int IEqualityComparer<object>.GetHashCode(object obj)
				{
					return RuntimeHelpers.GetHashCode(obj);
				}
			}
		}

		// Token: 0x0200003E RID: 62
		private struct IgnoreUIChangesData
		{
			// Token: 0x06000234 RID: 564 RVA: 0x0000A470 File Offset: 0x00008670
			public bool ShouldIgnoreChange(VisualElement ve, Binding b, BindingId id)
			{
				return this.element == ve && this.binding == b && (in this.bindingId) == (in id);
			}

			// Token: 0x0400014B RID: 331
			public VisualElement element;

			// Token: 0x0400014C RID: 332
			public Binding binding;

			// Token: 0x0400014D RID: 333
			public BindingId bindingId;
		}

		// Token: 0x0200003F RID: 63
		public struct IgnoreUIChangesScope : IDisposable
		{
			// Token: 0x06000235 RID: 565 RVA: 0x0000A4A4 File Offset: 0x000086A4
			internal IgnoreUIChangesScope(DataBindingManager manager, VisualElement target, BindingId bindingId, Binding binding)
			{
				this.manager = manager;
				this.m_ScopeData = this.manager.m_IgnoreUIChangesData;
				this.manager.m_IgnoreUIChangesData = new DataBindingManager.IgnoreUIChangesData
				{
					element = target,
					binding = binding,
					bindingId = bindingId
				};
			}

			// Token: 0x06000236 RID: 566 RVA: 0x0000A4F7 File Offset: 0x000086F7
			public void Dispose()
			{
				this.manager.m_IgnoreUIChangesData = this.m_ScopeData;
			}

			// Token: 0x0400014E RID: 334
			private DataBindingManager.IgnoreUIChangesData m_ScopeData;

			// Token: 0x0400014F RID: 335
			private DataBindingManager manager;
		}
	}
}
