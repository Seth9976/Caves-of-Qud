using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using AOT;
using JetBrains.Annotations;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements
{
	// Token: 0x020005EB RID: 1515
	[NativeType(Header = "Modules/UIElements/VisualManager.h")]
	internal sealed class VisualManager : IDisposable
	{
		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002CD2 RID: 11474 RVA: 0x000A0988 File Offset: 0x0009EB88
		// (set) Token: 0x06002CD3 RID: 11475 RVA: 0x000A098F File Offset: 0x0009EB8F
		public static VisualManager SharedManager { get; private set; }

		// Token: 0x06002CD4 RID: 11476 RVA: 0x000A0998 File Offset: 0x0009EB98
		static VisualManager()
		{
			VisualManager.Initialize();
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000A0A58 File Offset: 0x0009EC58
		private static void Initialize()
		{
			bool flag = VisualManager.s_Initialized;
			if (!flag)
			{
				VisualNodePropertyRegistry.RegisterInternalProperty<VisualNodeData>();
				VisualNodePropertyRegistry.RegisterInternalProperty<VisualNodePseudoStateData>();
				VisualNodePropertyRegistry.RegisterInternalProperty<VisualNodeClassData>();
				VisualNodePropertyRegistry.RegisterInternalProperty<VisualNodeRenderData>();
				VisualNodePropertyRegistry.RegisterInternalProperty<VisualNodeTextData>();
				VisualNodePropertyRegistry.RegisterInternalProperty<VisualNodeImguiData>();
				VisualManager.s_Initialized = true;
				bool flag2 = !VisualManager.s_AppDomainUnloadRegistered;
				if (flag2)
				{
					AppDomain.CurrentDomain.DomainUnload += delegate(object _, EventArgs __)
					{
						bool flag3 = VisualManager.s_Initialized;
						if (flag3)
						{
							VisualManager.Shutdown();
						}
					};
					VisualManager.s_AppDomainUnloadRegistered = true;
				}
				VisualManager.SharedManager = new VisualManager();
			}
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000A0AE4 File Offset: 0x0009ECE4
		private static void Shutdown()
		{
			bool flag = !VisualManager.s_Initialized;
			if (!flag)
			{
				VisualManager.s_Initialized = false;
				VisualManager.SharedManager.Dispose();
			}
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000A0B14 File Offset: 0x0009ED14
		private int RegisterCallbackInstance(VisualManager instance)
		{
			for (int i = 0; i < VisualManager.s_CallbackInstances.Count; i++)
			{
				VisualManager visualManager;
				bool flag = !VisualManager.s_CallbackInstances[i].TryGetTarget(out visualManager);
				if (flag)
				{
					VisualManager.s_CallbackInstances[i] = new WeakReference<VisualManager>(instance);
					return i + 1;
				}
			}
			VisualManager.s_CallbackInstances.Add(new WeakReference<VisualManager>(this));
			return VisualManager.s_CallbackInstances.Count;
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000A0B90 File Offset: 0x0009ED90
		private void UnregisterCallbackInstance(int id)
		{
			VisualManager.s_CallbackInstances[id - 1] = null;
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x000A0BA4 File Offset: 0x0009EDA4
		[MonoPInvokeCallback(typeof(VisualManager.NativeHierarchyChangedDelegate))]
		private static void InvokeHierarchyChanged(IntPtr instance, in VisualNodeHandle handle, HierarchyChangeType type)
		{
			for (int i = 0; i < VisualManager.s_CallbackInstances.Count; i++)
			{
				VisualManager visualManager;
				bool flag = VisualManager.s_CallbackInstances[i].TryGetTarget(out visualManager) && visualManager.m_Ptr == instance;
				if (flag)
				{
					visualManager.OnHierarchyChanged(visualManager, in handle, type);
				}
			}
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x000A0C04 File Offset: 0x0009EE04
		[MonoPInvokeCallback(typeof(VisualManager.NativeVersionChangedDelegate))]
		private static void InvokeVersionChanged(IntPtr instance, in VisualNodeHandle handle, VersionChangeType type)
		{
			for (int i = 0; i < VisualManager.s_CallbackInstances.Count; i++)
			{
				VisualManager visualManager;
				bool flag = VisualManager.s_CallbackInstances[i].TryGetTarget(out visualManager) && visualManager.m_Ptr == instance;
				if (flag)
				{
					VersionChangedDelegate onVersionChanged = visualManager.OnVersionChanged;
					if (onVersionChanged != null)
					{
						onVersionChanged(visualManager, in handle, type);
					}
				}
			}
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000A0C68 File Offset: 0x0009EE68
		[MonoPInvokeCallback(typeof(VisualManager.NativeVisualNodeDelegate))]
		private static void InvokeBlur(IntPtr instance, in VisualNodeHandle handle)
		{
			for (int i = 0; i < VisualManager.s_CallbackInstances.Count; i++)
			{
				VisualManager visualManager;
				bool flag = VisualManager.s_CallbackInstances[i].TryGetTarget(out visualManager) && visualManager.m_Ptr == instance;
				if (flag)
				{
					VisualNodeDelegate onBlur = visualManager.OnBlur;
					if (onBlur != null)
					{
						onBlur(visualManager, in handle);
					}
				}
			}
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000A0CCC File Offset: 0x0009EECC
		[MonoPInvokeCallback(typeof(VisualManager.NativeVisualNodeChildDelegate))]
		private static void InvokeChildAdded(IntPtr instance, in VisualNodeHandle handle, in VisualNodeHandle child)
		{
			for (int i = 0; i < VisualManager.s_CallbackInstances.Count; i++)
			{
				VisualManager visualManager;
				bool flag = VisualManager.s_CallbackInstances[i].TryGetTarget(out visualManager) && visualManager.m_Ptr == instance;
				if (flag)
				{
					VisualNodeChildDelegate onChildAdded = visualManager.OnChildAdded;
					if (onChildAdded != null)
					{
						onChildAdded(visualManager, in handle, in child);
					}
				}
			}
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000A0D30 File Offset: 0x0009EF30
		[MonoPInvokeCallback(typeof(VisualManager.NativeVisualNodeChildDelegate))]
		private static void InvokeChildRemoved(IntPtr instance, in VisualNodeHandle handle, in VisualNodeHandle child)
		{
			for (int i = 0; i < VisualManager.s_CallbackInstances.Count; i++)
			{
				VisualManager visualManager;
				bool flag = VisualManager.s_CallbackInstances[i].TryGetTarget(out visualManager) && visualManager.m_Ptr == instance;
				if (flag)
				{
					VisualNodeChildDelegate onChildRemoved = visualManager.OnChildRemoved;
					if (onChildRemoved != null)
					{
						onChildRemoved(visualManager, in handle, in child);
					}
				}
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000A0D93 File Offset: 0x0009EF93
		public bool IsCreated
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x000A0DA5 File Offset: 0x0009EFA5
		internal VisualNodeClassNameStore ClassNameStore { get; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002CE0 RID: 11488 RVA: 0x000A0DB0 File Offset: 0x0009EFB0
		[NativeProperty("Root", TargetType.Field)]
		public VisualNodeHandle Root
		{
			get
			{
				IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				VisualNodeHandle visualNodeHandle;
				VisualManager.get_Root_Injected(intPtr, out visualNodeHandle);
				return visualNodeHandle;
			}
		}

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x06002CE1 RID: 11489 RVA: 0x000A0DD8 File Offset: 0x0009EFD8
		// (remove) Token: 0x06002CE2 RID: 11490 RVA: 0x000A0E10 File Offset: 0x0009F010
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event HierarchyChangedDelegate OnHierarchyChanged;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06002CE3 RID: 11491 RVA: 0x000A0E48 File Offset: 0x0009F048
		// (remove) Token: 0x06002CE4 RID: 11492 RVA: 0x000A0E80 File Offset: 0x0009F080
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VersionChangedDelegate OnVersionChanged;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06002CE5 RID: 11493 RVA: 0x000A0EB8 File Offset: 0x0009F0B8
		// (remove) Token: 0x06002CE6 RID: 11494 RVA: 0x000A0EF0 File Offset: 0x0009F0F0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VisualNodeDelegate OnBlur;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06002CE7 RID: 11495 RVA: 0x000A0F28 File Offset: 0x0009F128
		// (remove) Token: 0x06002CE8 RID: 11496 RVA: 0x000A0F60 File Offset: 0x0009F160
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VisualNodeChildDelegate OnChildAdded;

		// Token: 0x14000053 RID: 83
		// (add) Token: 0x06002CE9 RID: 11497 RVA: 0x000A0F98 File Offset: 0x0009F198
		// (remove) Token: 0x06002CEA RID: 11498 RVA: 0x000A0FD0 File Offset: 0x0009F1D0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event VisualNodeChildDelegate OnChildRemoved;

		// Token: 0x06002CEB RID: 11499 RVA: 0x000A1005 File Offset: 0x0009F205
		public VisualManager()
			: this(VisualManager.Internal_Create(), false)
		{
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x000A1018 File Offset: 0x0009F218
		private VisualManager(IntPtr ptr, bool isWrapper)
		{
			this.m_InstanceId = this.RegisterCallbackInstance(this);
			this.m_Ptr = ptr;
			this.m_IsWrapper = isWrapper;
			this.ClassNameStore = this.GetClassNameStore();
			this.SetHierarchyChangedCallback(VisualManager.s_HierarchyChangedPtr);
			this.SetVersionChangedCallback(VisualManager.s_VersionChangedPtr);
			this.SetBlurCallback(VisualManager.s_BlurPtr);
			this.SetChildAddedCallback(VisualManager.s_ChildAddedPtr);
			this.SetChildRemovedCallback(VisualManager.s_ChildRemovedPtr);
			this.m_Registry = new VisualNodePropertyRegistry(this);
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x000A10E0 File Offset: 0x0009F2E0
		~VisualManager()
		{
			this.UnregisterCallbackInstance(this.m_InstanceId);
			this.Dispose(false);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x000A1120 File Offset: 0x0009F320
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x000A1134 File Offset: 0x0009F334
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool flag2 = !this.m_IsWrapper;
				if (flag2)
				{
					VisualManager.Internal_Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x000A117C File Offset: 0x0009F37C
		public VisualPanel CreatePanel()
		{
			this.TryFreePanels();
			VisualPanelHandle visualPanelHandle = this.AddPanel();
			return new VisualPanel(this, visualPanelHandle);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x000A11A4 File Offset: 0x0009F3A4
		public void DestroyPanelThreaded(ref VisualPanel panel)
		{
			VisualPanelHandle handle = panel.Handle;
			bool flag = (in handle) == (in VisualPanelHandle.Null);
			if (!flag)
			{
				object panelLock = this.m_PanelLock;
				lock (panelLock)
				{
					this.m_PanelsToRemove.Push(panel.Handle);
				}
				panel = VisualPanel.Null;
			}
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x000A1218 File Offset: 0x0009F418
		public VisualNode CreateNode()
		{
			this.TryFreeNodes();
			VisualNodeHandle visualNodeHandle = this.AddNode();
			return new VisualNode(this, visualNodeHandle);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x000A1240 File Offset: 0x0009F440
		public void DestroyNodeThreaded(ref VisualNode node)
		{
			VisualNodeHandle handle = node.Handle;
			bool flag = (in handle) == (in VisualNodeHandle.Null);
			if (!flag)
			{
				object nodeLock = this.m_NodeLock;
				lock (nodeLock)
				{
					this.m_NodesToRemove.Push(node.Handle);
				}
				node = VisualNode.Null;
			}
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x000A12B4 File Offset: 0x0009F4B4
		private void TryFreePanels()
		{
			bool flag = false;
			try
			{
				Monitor.TryEnter(this.m_PanelLock, ref flag);
				bool flag2 = flag;
				if (flag2)
				{
					while (this.m_PanelsToRemove.Count > 0)
					{
						VisualPanelHandle visualPanelHandle = this.m_PanelsToRemove.Pop();
						this.RemovePanel(in visualPanelHandle);
					}
				}
			}
			finally
			{
				bool flag3 = flag;
				if (flag3)
				{
					Monitor.Exit(this.m_PanelLock);
				}
			}
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x000A1330 File Offset: 0x0009F530
		private void TryFreeNodes()
		{
			bool flag = false;
			try
			{
				Monitor.TryEnter(this.m_NodeLock, ref flag);
				bool flag2 = flag;
				if (flag2)
				{
					while (this.m_NodesToRemove.Count > 0)
					{
						VisualNodeHandle visualNodeHandle = this.m_NodesToRemove.Pop();
						this.RemoveNode(in visualNodeHandle);
					}
				}
			}
			finally
			{
				bool flag3 = flag;
				if (flag3)
				{
					Monitor.Exit(this.m_NodeLock);
				}
			}
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x000A13AC File Offset: 0x0009F5AC
		public void SetOwner(in VisualNodeHandle handle, VisualElement element)
		{
			this.m_Elements[handle.Id] = ((element != null) ? new WeakReference<VisualElement>(element) : null);
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x000A13D0 File Offset: 0x0009F5D0
		public VisualElement GetOwner(in VisualNodeHandle handle)
		{
			VisualElement visualElement;
			this.m_Elements[handle.Id].TryGetTarget(out visualElement);
			return visualElement;
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x000A13FC File Offset: 0x0009F5FC
		public void SetOwner(in VisualPanelHandle handle, BaseVisualElementPanel panel)
		{
			this.m_Panels[handle.Id] = ((panel != null) ? new WeakReference<BaseVisualElementPanel>(panel) : null);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x000A1420 File Offset: 0x0009F620
		public BaseVisualElementPanel GetOwner(in VisualPanelHandle handle)
		{
			BaseVisualElementPanel baseVisualElementPanel;
			this.m_Panels[handle.Id].TryGetTarget(out baseVisualElementPanel);
			return baseVisualElementPanel;
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x000A144C File Offset: 0x0009F64C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref T GetProperty<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(VisualNodeHandle handle) where T : struct, ValueType
		{
			return this.m_Registry.GetPropertyRef<T>(handle);
		}

		// Token: 0x06002CFB RID: 11515
		[FreeFunction("VisualManager::Create")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Internal_Create();

		// Token: 0x06002CFC RID: 11516
		[FreeFunction("VisualManager::Destroy")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06002CFD RID: 11517 RVA: 0x000A145C File Offset: 0x0009F65C
		[NativeThrows]
		private void SetHierarchyChangedCallback(IntPtr callback)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetHierarchyChangedCallback_Injected(intPtr, callback);
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x000A1480 File Offset: 0x0009F680
		[NativeThrows]
		private void SetVersionChangedCallback(IntPtr callback)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetVersionChangedCallback_Injected(intPtr, callback);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x000A14A4 File Offset: 0x0009F6A4
		[NativeThrows]
		private void SetBlurCallback(IntPtr callback)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetBlurCallback_Injected(intPtr, callback);
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x000A14C8 File Offset: 0x0009F6C8
		[NativeThrows]
		private void SetChildAddedCallback(IntPtr callback)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetChildAddedCallback_Injected(intPtr, callback);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x000A14EC File Offset: 0x0009F6EC
		[NativeThrows]
		private void SetChildRemovedCallback(IntPtr callback)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetChildRemovedCallback_Injected(intPtr, callback);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x000A1510 File Offset: 0x0009F710
		[NativeThrows]
		internal IntPtr GetPropertyPtr(int index)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.GetPropertyPtr_Injected(intPtr, index);
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x000A1534 File Offset: 0x0009F734
		internal int PanelCount()
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.PanelCount_Injected(intPtr);
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x000A1558 File Offset: 0x0009F758
		[NativeThrows]
		internal VisualPanelHandle AddPanel()
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualPanelHandle visualPanelHandle;
			VisualManager.AddPanel_Injected(intPtr, out visualPanelHandle);
			return visualPanelHandle;
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x000A1580 File Offset: 0x0009F780
		[NativeThrows]
		internal bool RemovePanel(in VisualPanelHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.RemovePanel_Injected(intPtr, in handle);
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x000A15A4 File Offset: 0x0009F7A4
		[NativeThrows]
		internal bool ContainsPanel(in VisualPanelHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.ContainsPanel_Injected(intPtr, in handle);
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x000A15C8 File Offset: 0x0009F7C8
		[NativeThrows]
		internal void ClearPanels()
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.ClearPanels_Injected(intPtr);
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x000A15EC File Offset: 0x0009F7EC
		[NativeThrows]
		internal unsafe void* GetPanelDataPtr(in VisualPanelHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.GetPanelDataPtr_Injected(intPtr, in handle);
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x000A1610 File Offset: 0x0009F810
		[NativeThrows]
		internal VisualNodeHandle GetRootContainer(in VisualPanelHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualNodeHandle visualNodeHandle;
			VisualManager.GetRootContainer_Injected(intPtr, in handle, out visualNodeHandle);
			return visualNodeHandle;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000A1638 File Offset: 0x0009F838
		[NativeThrows]
		internal bool SetRootContainer(in VisualPanelHandle handle, in VisualNodeHandle container)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.SetRootContainer_Injected(intPtr, in handle, in container);
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x000A165C File Offset: 0x0009F85C
		internal int NodeCount()
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.NodeCount_Injected(intPtr);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000A1680 File Offset: 0x0009F880
		[NativeThrows]
		internal VisualNodeHandle AddNode()
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualNodeHandle visualNodeHandle;
			VisualManager.AddNode_Injected(intPtr, out visualNodeHandle);
			return visualNodeHandle;
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x000A16A8 File Offset: 0x0009F8A8
		[NativeThrows]
		internal bool RemoveNode(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.RemoveNode_Injected(intPtr, in handle);
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x000A16CC File Offset: 0x0009F8CC
		[NativeThrows]
		internal bool ContainsNode(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.ContainsNode_Injected(intPtr, in handle);
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x000A16F0 File Offset: 0x0009F8F0
		[NativeThrows]
		internal void ClearNodes()
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.ClearNodes_Injected(intPtr);
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x000A1714 File Offset: 0x0009F914
		[NativeThrows]
		internal unsafe void SetName(in VisualNodeHandle handle, string name)
		{
			try
			{
				IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				VisualManager.SetName_Injected(intPtr, in handle, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x000A177C File Offset: 0x0009F97C
		[NativeThrows]
		internal string GetName(in VisualNodeHandle handle)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				VisualManager.GetName_Injected(intPtr, in handle, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000A17BC File Offset: 0x0009F9BC
		[NativeThrows]
		internal int GetChildrenCount(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.GetChildrenCount_Injected(intPtr, in handle);
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x000A17E0 File Offset: 0x0009F9E0
		[NativeThrows]
		internal IntPtr GetChildrenPtr(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.GetChildrenPtr_Injected(intPtr, in handle);
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000A1804 File Offset: 0x0009FA04
		[NativeThrows]
		internal int IndexOfChild(in VisualNodeHandle handle, in VisualNodeHandle child)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.IndexOfChild_Injected(intPtr, in handle, in child);
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000A1828 File Offset: 0x0009FA28
		[NativeThrows]
		internal bool AddChild(in VisualNodeHandle handle, in VisualNodeHandle child)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.AddChild_Injected(intPtr, in handle, in child);
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000A184C File Offset: 0x0009FA4C
		[NativeThrows]
		internal bool RemoveChild(in VisualNodeHandle handle, in VisualNodeHandle child)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.RemoveChild_Injected(intPtr, in handle, in child);
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000A1870 File Offset: 0x0009FA70
		[NativeThrows]
		internal bool InsertChildAtIndex(in VisualNodeHandle handle, int index, in VisualNodeHandle child)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.InsertChildAtIndex_Injected(intPtr, in handle, index, in child);
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000A1898 File Offset: 0x0009FA98
		[NativeThrows]
		internal bool RemoveChildAtIndex(in VisualNodeHandle handle, int index)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.RemoveChildAtIndex_Injected(intPtr, in handle, index);
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x000A18BC File Offset: 0x0009FABC
		[NativeThrows]
		internal bool ClearChildren(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.ClearChildren_Injected(intPtr, in handle);
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000A18E0 File Offset: 0x0009FAE0
		[NativeThrows]
		internal VisualNodeHandle GetParent(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualNodeHandle visualNodeHandle;
			VisualManager.GetParent_Injected(intPtr, in handle, out visualNodeHandle);
			return visualNodeHandle;
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x000A1908 File Offset: 0x0009FB08
		[NativeThrows]
		internal bool RemoveFromParent(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.RemoveFromParent_Injected(intPtr, in handle);
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000A192C File Offset: 0x0009FB2C
		[NativeThrows]
		internal unsafe bool AddToClassList(in VisualNodeHandle handle, string className)
		{
			bool flag;
			try
			{
				IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(className, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = className.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = VisualManager.AddToClassList_Injected(intPtr, in handle, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x000A1994 File Offset: 0x0009FB94
		[NativeThrows]
		internal unsafe bool RemoveFromClassList(in VisualNodeHandle handle, string className)
		{
			bool flag;
			try
			{
				IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(className, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = className.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = VisualManager.RemoveFromClassList_Injected(intPtr, in handle, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000A19FC File Offset: 0x0009FBFC
		[NativeThrows]
		internal unsafe bool ClassListContains(in VisualNodeHandle handle, string className)
		{
			bool flag;
			try
			{
				IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(className, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = className.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = VisualManager.ClassListContains_Injected(intPtr, in handle, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000A1A64 File Offset: 0x0009FC64
		[NativeThrows]
		internal bool ClearClassList(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.ClearClassList_Injected(intPtr, in handle);
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x000A1A88 File Offset: 0x0009FC88
		[NativeThrows]
		private VisualNodeClassNameStore GetClassNameStore()
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr classNameStore_Injected = VisualManager.GetClassNameStore_Injected(intPtr);
			return (classNameStore_Injected == 0) ? null : VisualNodeClassNameStore.BindingsMarshaller.ConvertToManaged(classNameStore_Injected);
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000A1AB8 File Offset: 0x0009FCB8
		[NativeThrows]
		internal bool IsEnabled(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.IsEnabled_Injected(intPtr, in handle);
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000A1ADC File Offset: 0x0009FCDC
		[NativeThrows]
		internal void SetEnabled(in VisualNodeHandle handle, bool enabled)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetEnabled_Injected(intPtr, in handle, enabled);
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x000A1B00 File Offset: 0x0009FD00
		[NativeThrows]
		internal bool IsEnabledInHierarchy(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.IsEnabledInHierarchy_Injected(intPtr, in handle);
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000A1B24 File Offset: 0x0009FD24
		[NativeThrows]
		internal VisualPanelHandle GetPanel(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualPanelHandle visualPanelHandle;
			VisualManager.GetPanel_Injected(intPtr, in handle, out visualPanelHandle);
			return visualPanelHandle;
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000A1B4C File Offset: 0x0009FD4C
		[NativeThrows]
		internal void SetPanel(in VisualNodeHandle handle, in VisualPanelHandle panel)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetPanel_Injected(intPtr, in handle, in panel);
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000A1B70 File Offset: 0x0009FD70
		[NativeThrows]
		[return: Unmarshalled]
		internal PseudoStates GetPseudoStates(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.GetPseudoStates_Injected(intPtr, in handle);
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000A1B94 File Offset: 0x0009FD94
		[NativeThrows]
		internal void SetPseudoStates(in VisualNodeHandle handle, PseudoStates states)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetPseudoStates_Injected(intPtr, in handle, states);
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000A1BB8 File Offset: 0x0009FDB8
		[NativeThrows]
		[return: Unmarshalled]
		internal RenderHints GetRenderHints(in VisualNodeHandle handles)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.GetRenderHints_Injected(intPtr, in handles);
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000A1BDC File Offset: 0x0009FDDC
		[NativeThrows]
		internal void SetRenderHints(in VisualNodeHandle handle, RenderHints hints)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetRenderHints_Injected(intPtr, in handle, hints);
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000A1C00 File Offset: 0x0009FE00
		[NativeThrows]
		[return: Unmarshalled]
		internal LanguageDirection GetLanguageDirection(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.GetLanguageDirection_Injected(intPtr, in handle);
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000A1C24 File Offset: 0x0009FE24
		[NativeThrows]
		internal void SetLanguageDirection(in VisualNodeHandle handle, LanguageDirection direction)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetLanguageDirection_Injected(intPtr, in handle, direction);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x000A1C48 File Offset: 0x0009FE48
		[NativeThrows]
		[return: Unmarshalled]
		internal LanguageDirection GetLocalLanguageDirection(in VisualNodeHandle handle)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VisualManager.GetLocalLanguageDirection_Injected(intPtr, in handle);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000A1C6C File Offset: 0x0009FE6C
		[NativeThrows]
		internal void SetLocalLanguageDirection(in VisualNodeHandle handle, LanguageDirection direction)
		{
			IntPtr intPtr = VisualManager.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VisualManager.SetLocalLanguageDirection_Injected(intPtr, in handle, direction);
		}

		// Token: 0x06002D2E RID: 11566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_Root_Injected(IntPtr _unity_self, out VisualNodeHandle ret);

		// Token: 0x06002D2F RID: 11567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHierarchyChangedCallback_Injected(IntPtr _unity_self, IntPtr callback);

		// Token: 0x06002D30 RID: 11568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVersionChangedCallback_Injected(IntPtr _unity_self, IntPtr callback);

		// Token: 0x06002D31 RID: 11569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBlurCallback_Injected(IntPtr _unity_self, IntPtr callback);

		// Token: 0x06002D32 RID: 11570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetChildAddedCallback_Injected(IntPtr _unity_self, IntPtr callback);

		// Token: 0x06002D33 RID: 11571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetChildRemovedCallback_Injected(IntPtr _unity_self, IntPtr callback);

		// Token: 0x06002D34 RID: 11572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetPropertyPtr_Injected(IntPtr _unity_self, int index);

		// Token: 0x06002D35 RID: 11573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PanelCount_Injected(IntPtr _unity_self);

		// Token: 0x06002D36 RID: 11574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddPanel_Injected(IntPtr _unity_self, out VisualPanelHandle ret);

		// Token: 0x06002D37 RID: 11575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemovePanel_Injected(IntPtr _unity_self, in VisualPanelHandle handle);

		// Token: 0x06002D38 RID: 11576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContainsPanel_Injected(IntPtr _unity_self, in VisualPanelHandle handle);

		// Token: 0x06002D39 RID: 11577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearPanels_Injected(IntPtr _unity_self);

		// Token: 0x06002D3A RID: 11578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* GetPanelDataPtr_Injected(IntPtr _unity_self, in VisualPanelHandle handle);

		// Token: 0x06002D3B RID: 11579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootContainer_Injected(IntPtr _unity_self, in VisualPanelHandle handle, out VisualNodeHandle ret);

		// Token: 0x06002D3C RID: 11580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetRootContainer_Injected(IntPtr _unity_self, in VisualPanelHandle handle, in VisualNodeHandle container);

		// Token: 0x06002D3D RID: 11581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int NodeCount_Injected(IntPtr _unity_self);

		// Token: 0x06002D3E RID: 11582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddNode_Injected(IntPtr _unity_self, out VisualNodeHandle ret);

		// Token: 0x06002D3F RID: 11583
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveNode_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D40 RID: 11584
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContainsNode_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D41 RID: 11585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearNodes_Injected(IntPtr _unity_self);

		// Token: 0x06002D42 RID: 11586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetName_Injected(IntPtr _unity_self, in VisualNodeHandle handle, ref ManagedSpanWrapper name);

		// Token: 0x06002D43 RID: 11587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetName_Injected(IntPtr _unity_self, in VisualNodeHandle handle, out ManagedSpanWrapper ret);

		// Token: 0x06002D44 RID: 11588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCount_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D45 RID: 11589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetChildrenPtr_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D46 RID: 11590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int IndexOfChild_Injected(IntPtr _unity_self, in VisualNodeHandle handle, in VisualNodeHandle child);

		// Token: 0x06002D47 RID: 11591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddChild_Injected(IntPtr _unity_self, in VisualNodeHandle handle, in VisualNodeHandle child);

		// Token: 0x06002D48 RID: 11592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveChild_Injected(IntPtr _unity_self, in VisualNodeHandle handle, in VisualNodeHandle child);

		// Token: 0x06002D49 RID: 11593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool InsertChildAtIndex_Injected(IntPtr _unity_self, in VisualNodeHandle handle, int index, in VisualNodeHandle child);

		// Token: 0x06002D4A RID: 11594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveChildAtIndex_Injected(IntPtr _unity_self, in VisualNodeHandle handle, int index);

		// Token: 0x06002D4B RID: 11595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearChildren_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D4C RID: 11596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParent_Injected(IntPtr _unity_self, in VisualNodeHandle handle, out VisualNodeHandle ret);

		// Token: 0x06002D4D RID: 11597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveFromParent_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D4E RID: 11598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddToClassList_Injected(IntPtr _unity_self, in VisualNodeHandle handle, ref ManagedSpanWrapper className);

		// Token: 0x06002D4F RID: 11599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveFromClassList_Injected(IntPtr _unity_self, in VisualNodeHandle handle, ref ManagedSpanWrapper className);

		// Token: 0x06002D50 RID: 11600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClassListContains_Injected(IntPtr _unity_self, in VisualNodeHandle handle, ref ManagedSpanWrapper className);

		// Token: 0x06002D51 RID: 11601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearClassList_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D52 RID: 11602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetClassNameStore_Injected(IntPtr _unity_self);

		// Token: 0x06002D53 RID: 11603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEnabled_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D54 RID: 11604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnabled_Injected(IntPtr _unity_self, in VisualNodeHandle handle, bool enabled);

		// Token: 0x06002D55 RID: 11605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEnabledInHierarchy_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D56 RID: 11606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPanel_Injected(IntPtr _unity_self, in VisualNodeHandle handle, out VisualPanelHandle ret);

		// Token: 0x06002D57 RID: 11607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPanel_Injected(IntPtr _unity_self, in VisualNodeHandle handle, in VisualPanelHandle panel);

		// Token: 0x06002D58 RID: 11608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PseudoStates GetPseudoStates_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D59 RID: 11609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPseudoStates_Injected(IntPtr _unity_self, in VisualNodeHandle handle, PseudoStates states);

		// Token: 0x06002D5A RID: 11610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RenderHints GetRenderHints_Injected(IntPtr _unity_self, in VisualNodeHandle handles);

		// Token: 0x06002D5B RID: 11611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderHints_Injected(IntPtr _unity_self, in VisualNodeHandle handle, RenderHints hints);

		// Token: 0x06002D5C RID: 11612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LanguageDirection GetLanguageDirection_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D5D RID: 11613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLanguageDirection_Injected(IntPtr _unity_self, in VisualNodeHandle handle, LanguageDirection direction);

		// Token: 0x06002D5E RID: 11614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LanguageDirection GetLocalLanguageDirection_Injected(IntPtr _unity_self, in VisualNodeHandle handle);

		// Token: 0x06002D5F RID: 11615
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalLanguageDirection_Injected(IntPtr _unity_self, in VisualNodeHandle handle, LanguageDirection direction);

		// Token: 0x04001454 RID: 5204
		private static bool s_Initialized;

		// Token: 0x04001455 RID: 5205
		private static bool s_AppDomainUnloadRegistered;

		// Token: 0x04001456 RID: 5206
		private static readonly List<WeakReference<VisualManager>> s_CallbackInstances = new List<WeakReference<VisualManager>>();

		// Token: 0x04001457 RID: 5207
		private static readonly VisualManager.NativeHierarchyChangedDelegate s_HierarchyChanged = new VisualManager.NativeHierarchyChangedDelegate(VisualManager.InvokeHierarchyChanged);

		// Token: 0x04001458 RID: 5208
		private static readonly VisualManager.NativeVersionChangedDelegate s_VersionChanged = new VisualManager.NativeVersionChangedDelegate(VisualManager.InvokeVersionChanged);

		// Token: 0x04001459 RID: 5209
		private static readonly VisualManager.NativeVisualNodeDelegate s_Blur = new VisualManager.NativeVisualNodeDelegate(VisualManager.InvokeBlur);

		// Token: 0x0400145A RID: 5210
		private static readonly VisualManager.NativeVisualNodeChildDelegate s_ChildAdded = new VisualManager.NativeVisualNodeChildDelegate(VisualManager.InvokeChildAdded);

		// Token: 0x0400145B RID: 5211
		private static readonly VisualManager.NativeVisualNodeChildDelegate s_ChildRemoved = new VisualManager.NativeVisualNodeChildDelegate(VisualManager.InvokeChildRemoved);

		// Token: 0x0400145C RID: 5212
		private static readonly IntPtr s_HierarchyChangedPtr = Marshal.GetFunctionPointerForDelegate<VisualManager.NativeHierarchyChangedDelegate>(VisualManager.s_HierarchyChanged);

		// Token: 0x0400145D RID: 5213
		private static readonly IntPtr s_VersionChangedPtr = Marshal.GetFunctionPointerForDelegate<VisualManager.NativeVersionChangedDelegate>(VisualManager.s_VersionChanged);

		// Token: 0x0400145E RID: 5214
		private static readonly IntPtr s_BlurPtr = Marshal.GetFunctionPointerForDelegate<VisualManager.NativeVisualNodeDelegate>(VisualManager.s_Blur);

		// Token: 0x0400145F RID: 5215
		private static readonly IntPtr s_ChildAddedPtr = Marshal.GetFunctionPointerForDelegate<VisualManager.NativeVisualNodeChildDelegate>(VisualManager.s_ChildAdded);

		// Token: 0x04001460 RID: 5216
		private static readonly IntPtr s_ChildRemovedPtr = Marshal.GetFunctionPointerForDelegate<VisualManager.NativeVisualNodeChildDelegate>(VisualManager.s_ChildRemoved);

		// Token: 0x04001461 RID: 5217
		[RequiredByNativeCode]
		private IntPtr m_Ptr;

		// Token: 0x04001462 RID: 5218
		[RequiredByNativeCode]
		private bool m_IsWrapper;

		// Token: 0x04001463 RID: 5219
		private readonly int m_InstanceId;

		// Token: 0x04001464 RID: 5220
		private readonly VisualNodePropertyRegistry m_Registry;

		// Token: 0x04001465 RID: 5221
		private readonly ChunkAllocatingArray<WeakReference<VisualElement>> m_Elements = new ChunkAllocatingArray<WeakReference<VisualElement>>();

		// Token: 0x04001466 RID: 5222
		private readonly ChunkAllocatingArray<WeakReference<BaseVisualElementPanel>> m_Panels = new ChunkAllocatingArray<WeakReference<BaseVisualElementPanel>>();

		// Token: 0x04001467 RID: 5223
		private readonly object m_NodeLock = new object();

		// Token: 0x04001468 RID: 5224
		private readonly Stack<VisualNodeHandle> m_NodesToRemove = new Stack<VisualNodeHandle>();

		// Token: 0x04001469 RID: 5225
		private readonly object m_PanelLock = new object();

		// Token: 0x0400146A RID: 5226
		private readonly Stack<VisualPanelHandle> m_PanelsToRemove = new Stack<VisualPanelHandle>();

		// Token: 0x020005EC RID: 1516
		[UsedImplicitly]
		internal static class BindingsMarshaller
		{
			// Token: 0x06002D60 RID: 11616 RVA: 0x000A1C90 File Offset: 0x0009FE90
			public static IntPtr ConvertToNative(VisualManager store)
			{
				return store.m_Ptr;
			}

			// Token: 0x06002D61 RID: 11617 RVA: 0x000A1C98 File Offset: 0x0009FE98
			public static VisualManager ConvertToManaged(IntPtr ptr)
			{
				return new VisualManager(ptr, true);
			}
		}

		// Token: 0x020005ED RID: 1517
		// (Invoke) Token: 0x06002D63 RID: 11619
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void NativeHierarchyChangedDelegate(IntPtr instance, in VisualNodeHandle handle, HierarchyChangeType type);

		// Token: 0x020005EE RID: 1518
		// (Invoke) Token: 0x06002D67 RID: 11623
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void NativeVersionChangedDelegate(IntPtr instance, in VisualNodeHandle handle, VersionChangeType type);

		// Token: 0x020005EF RID: 1519
		// (Invoke) Token: 0x06002D6B RID: 11627
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void NativeVisualNodeDelegate(IntPtr instance, in VisualNodeHandle handle);

		// Token: 0x020005F0 RID: 1520
		// (Invoke) Token: 0x06002D6F RID: 11631
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void NativeVisualNodeChildDelegate(IntPtr instance, in VisualNodeHandle handle, in VisualNodeHandle child);
	}
}
