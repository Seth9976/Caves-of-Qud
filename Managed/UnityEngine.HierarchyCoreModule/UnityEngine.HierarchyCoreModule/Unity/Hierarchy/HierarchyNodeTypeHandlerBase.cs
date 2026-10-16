using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x0200000C RID: 12
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNodeTypeHandlerBase.h")]
	[NativeHeader("Modules/HierarchyCore/HierarchyNodeTypeHandlerBaseBindings.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class HierarchyNodeTypeHandlerBase : IDisposable
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002740 File Offset: 0x00000940
		public Hierarchy Hierarchy
		{
			get
			{
				return this.m_Hierarchy;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002748 File Offset: 0x00000948
		protected HierarchyCommandList CommandList
		{
			get
			{
				return this.m_CommandList;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002750 File Offset: 0x00000950
		protected HierarchyNodeTypeHandlerBase()
		{
			this.m_Ptr = HierarchyNodeTypeHandlerBase.ConstructorScope.Ptr;
			this.m_Hierarchy = HierarchyNodeTypeHandlerBase.ConstructorScope.Hierarchy;
			this.m_CommandList = HierarchyNodeTypeHandlerBase.ConstructorScope.CommandList;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000277B File Offset: 0x0000097B
		[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
		internal HierarchyNodeTypeHandlerBase(IntPtr nativePtr, Hierarchy hierarchy, HierarchyCommandList cmdList)
		{
			this.m_Ptr = nativePtr;
			this.m_Hierarchy = hierarchy;
			this.m_CommandList = cmdList;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000279C File Offset: 0x0000099C
		~HierarchyNodeTypeHandlerBase()
		{
			this.Dispose(false);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000027D0 File Offset: 0x000009D0
		protected virtual void Initialize()
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000027D0 File Offset: 0x000009D0
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027D3 File Offset: 0x000009D3
		public HierarchyNodeType GetNodeType()
		{
			return new HierarchyNodeType(HierarchyNodeTypeHandlerBase.GetNodeTypeFromType(base.GetType()));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000027E8 File Offset: 0x000009E8
		[NativeMethod(IsThreadSafe = true)]
		public virtual string GetNodeTypeName()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				HierarchyNodeTypeHandlerBase.GetNodeTypeName_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002828 File Offset: 0x00000A28
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public virtual HierarchyNodeFlags GetDefaultNodeFlags(in HierarchyNode node, HierarchyNodeFlags defaultFlags = HierarchyNodeFlags.None)
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyNodeTypeHandlerBase.GetDefaultNodeFlags_Injected(intPtr, in node, defaultFlags);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000284C File Offset: 0x00000A4C
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::SearchBegin", HasExplicitThis = true, IsThreadSafe = true)]
		protected virtual void SearchBegin(HierarchySearchQueryDescriptor query)
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNodeTypeHandlerBase.SearchBegin_Injected(intPtr, query);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002870 File Offset: 0x00000A70
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::SearchMatch", HasExplicitThis = true, IsThreadSafe = true)]
		protected virtual bool SearchMatch(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyNodeTypeHandlerBase.SearchMatch_Injected(intPtr, in node);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002894 File Offset: 0x00000A94
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::SearchEnd", HasExplicitThis = true, IsThreadSafe = true)]
		protected virtual void SearchEnd()
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNodeTypeHandlerBase.SearchEnd_Injected(intPtr);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000028B8 File Offset: 0x00000AB8
		[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HierarchyNodeTypeHandlerBase FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((HierarchyNodeTypeHandlerBase)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000028E8 File Offset: 0x00000AE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void Internal_SearchBegin(HierarchySearchQueryDescriptor query)
		{
			this.SearchBegin(query);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000028F2 File Offset: 0x00000AF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool Internal_SearchMatch(in HierarchyNode node)
		{
			return this.SearchMatch(in node);
		}

		// Token: 0x06000038 RID: 56
		[FreeFunction("HierarchyNodeTypeHandlerManager::Get().GetNodeType", IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodeTypeFromType(Type type);

		// Token: 0x06000039 RID: 57 RVA: 0x000028FC File Offset: 0x00000AFC
		[RequiredByNativeCode]
		private static IntPtr CreateNodeTypeHandlerFromType(IntPtr nativePtr, Type handlerType, IntPtr hierarchyPtr, IntPtr cmdListPtr)
		{
			bool flag = nativePtr == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentNullException("nativePtr");
			}
			bool flag2 = hierarchyPtr == IntPtr.Zero;
			if (flag2)
			{
				throw new ArgumentNullException("hierarchyPtr");
			}
			bool flag3 = cmdListPtr == IntPtr.Zero;
			if (flag3)
			{
				throw new ArgumentNullException("cmdListPtr");
			}
			Hierarchy hierarchy = Hierarchy.FromIntPtr(hierarchyPtr);
			HierarchyCommandList hierarchyCommandList = HierarchyCommandList.FromIntPtr(cmdListPtr);
			IntPtr intPtr;
			using (new HierarchyNodeTypeHandlerBase.ConstructorScope(nativePtr, hierarchy, hierarchyCommandList))
			{
				BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
				HierarchyNodeTypeHandlerBase hierarchyNodeTypeHandlerBase = (HierarchyNodeTypeHandlerBase)Activator.CreateInstance(handlerType, bindingFlags, null, null, null);
				bool flag4 = hierarchyNodeTypeHandlerBase == null;
				if (flag4)
				{
					intPtr = IntPtr.Zero;
				}
				else
				{
					hierarchyNodeTypeHandlerBase.Initialize();
					intPtr = GCHandle.ToIntPtr(GCHandle.Alloc(hierarchyNodeTypeHandlerBase));
				}
			}
			return intPtr;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029D8 File Offset: 0x00000BD8
		[RequiredByNativeCode]
		private static bool TryGetStaticNodeType(Type handlerType, out int nodeType)
		{
			bool flag = HierarchyNodeTypeHandlerBase.s_NodeTypes.TryGetValue(handlerType, out nodeType);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				MethodInfo method = handlerType.GetMethod("GetStaticNodeType", BindingFlags.Static | BindingFlags.NonPublic);
				bool flag3 = method != null;
				if (flag3)
				{
					nodeType = (int)method.Invoke(null, null);
					HierarchyNodeTypeHandlerBase.s_NodeTypes.Add(handlerType, nodeType);
					flag2 = true;
				}
				else
				{
					nodeType = 0;
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A3D File Offset: 0x00000C3D
		[RequiredByNativeCode]
		private static void InvokeInitialize(IntPtr handlePtr)
		{
			HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).Initialize();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A4C File Offset: 0x00000C4C
		[RequiredByNativeCode]
		private static void InvokeDispose(IntPtr handlePtr)
		{
			HierarchyNodeTypeHandlerBase hierarchyNodeTypeHandlerBase = HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr);
			hierarchyNodeTypeHandlerBase.Dispose(true);
			GC.SuppressFinalize(hierarchyNodeTypeHandlerBase);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002A70 File Offset: 0x00000C70
		[RequiredByNativeCode]
		private static string InvokeGetNodeTypeName(IntPtr handlePtr)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).GetNodeTypeName();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002A7D File Offset: 0x00000C7D
		[RequiredByNativeCode]
		private static HierarchyNodeFlags InvokeGetDefaultNodeFlags(IntPtr handlePtr, in HierarchyNode node, HierarchyNodeFlags defaultFlags)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).GetDefaultNodeFlags(in node, defaultFlags);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002A8C File Offset: 0x00000C8C
		[RequiredByNativeCode]
		private static bool InvokeChangesPending(IntPtr handlePtr)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).ChangesPending();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002A99 File Offset: 0x00000C99
		[RequiredByNativeCode]
		private static bool InvokeIntegrateChanges(IntPtr handlePtr, IntPtr cmdListPtr)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).IntegrateChanges(HierarchyCommandList.FromIntPtr(cmdListPtr));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002AAC File Offset: 0x00000CAC
		[RequiredByNativeCode]
		private static bool InvokeSearchMatch(IntPtr handlePtr, in HierarchyNode node)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).SearchMatch(in node);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002ABA File Offset: 0x00000CBA
		[RequiredByNativeCode]
		private static void InvokeSearchEnd(IntPtr handlePtr)
		{
			HierarchyNodeTypeHandlerBase.FromIntPtr(handlePtr).SearchEnd();
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002AC8 File Offset: 0x00000CC8
		[Obsolete("The constructor with a hierarchy parameter is obsolete and is no longer used. Remove the hierarchy parameter from your constructor.")]
		protected HierarchyNodeTypeHandlerBase(Hierarchy hierarchy)
			: this()
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000027D0 File Offset: 0x000009D0
		[Obsolete("The IDisposable interface is obsolete and no longer has any effect. Instances of handlers are owned and disposed by the hierarchy so they do not need to be disposed by user code.")]
		public void Dispose()
		{
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002AD4 File Offset: 0x00000CD4
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::ChangesPending", HasExplicitThis = true, IsThreadSafe = true)]
		[Obsolete("ChangesPending is obsolete, it is replaced by adding commands into the hierarchy node type handler's CommandList.", false)]
		protected virtual bool ChangesPending()
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyNodeTypeHandlerBase.ChangesPending_Injected(intPtr);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AF8 File Offset: 0x00000CF8
		[Obsolete("IntegrateChanges is obsolete, it is replaced by adding commands into the hierarchy node type handler's CommandList.", false)]
		[FreeFunction("HierarchyNodeTypeHandlerBaseBindings::IntegrateChanges", HasExplicitThis = true, IsThreadSafe = true)]
		protected virtual bool IntegrateChanges(HierarchyCommandList cmdList)
		{
			IntPtr intPtr = HierarchyNodeTypeHandlerBase.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyNodeTypeHandlerBase.IntegrateChanges_Injected(intPtr, (cmdList == null) ? ((IntPtr)0) : HierarchyCommandList.BindingsMarshaller.ConvertToNative(cmdList));
		}

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNodeTypeName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HierarchyNodeFlags GetDefaultNodeFlags_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags defaultFlags);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SearchBegin_Injected(IntPtr _unity_self, HierarchySearchQueryDescriptor query);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SearchMatch_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SearchEnd_Injected(IntPtr _unity_self);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ChangesPending_Injected(IntPtr _unity_self);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IntegrateChanges_Injected(IntPtr _unity_self, IntPtr cmdList);

		// Token: 0x0400001E RID: 30
		internal readonly IntPtr m_Ptr;

		// Token: 0x0400001F RID: 31
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x04000020 RID: 32
		private readonly HierarchyCommandList m_CommandList;

		// Token: 0x04000021 RID: 33
		private static readonly Dictionary<Type, int> s_NodeTypes = new Dictionary<Type, int>();

		// Token: 0x0200000D RID: 13
		internal static class BindingsMarshaller
		{
			// Token: 0x0600004F RID: 79 RVA: 0x00002B35 File Offset: 0x00000D35
			public static IntPtr ConvertToNative(HierarchyNodeTypeHandlerBase handler)
			{
				return handler.m_Ptr;
			}
		}

		// Token: 0x0200000E RID: 14
		private struct ConstructorScope : IDisposable
		{
			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000050 RID: 80 RVA: 0x00002B3D File Offset: 0x00000D3D
			// (set) Token: 0x06000051 RID: 81 RVA: 0x00002B44 File Offset: 0x00000D44
			public static IntPtr Ptr
			{
				get
				{
					return HierarchyNodeTypeHandlerBase.ConstructorScope.m_Ptr;
				}
				private set
				{
					HierarchyNodeTypeHandlerBase.ConstructorScope.m_Ptr = value;
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000052 RID: 82 RVA: 0x00002B4C File Offset: 0x00000D4C
			// (set) Token: 0x06000053 RID: 83 RVA: 0x00002B53 File Offset: 0x00000D53
			public static Hierarchy Hierarchy
			{
				get
				{
					return HierarchyNodeTypeHandlerBase.ConstructorScope.m_Hierarchy;
				}
				private set
				{
					HierarchyNodeTypeHandlerBase.ConstructorScope.m_Hierarchy = value;
				}
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000054 RID: 84 RVA: 0x00002B5B File Offset: 0x00000D5B
			// (set) Token: 0x06000055 RID: 85 RVA: 0x00002B62 File Offset: 0x00000D62
			public static HierarchyCommandList CommandList
			{
				get
				{
					return HierarchyNodeTypeHandlerBase.ConstructorScope.m_CommandList;
				}
				private set
				{
					HierarchyNodeTypeHandlerBase.ConstructorScope.m_CommandList = value;
				}
			}

			// Token: 0x06000056 RID: 86 RVA: 0x00002B6A File Offset: 0x00000D6A
			public ConstructorScope(IntPtr nativePtr, Hierarchy hierarchy, HierarchyCommandList cmdList)
			{
				HierarchyNodeTypeHandlerBase.ConstructorScope.Ptr = nativePtr;
				HierarchyNodeTypeHandlerBase.ConstructorScope.Hierarchy = hierarchy;
				HierarchyNodeTypeHandlerBase.ConstructorScope.CommandList = cmdList;
			}

			// Token: 0x06000057 RID: 87 RVA: 0x00002B82 File Offset: 0x00000D82
			public void Dispose()
			{
				HierarchyNodeTypeHandlerBase.ConstructorScope.Ptr = IntPtr.Zero;
				HierarchyNodeTypeHandlerBase.ConstructorScope.Hierarchy = null;
				HierarchyNodeTypeHandlerBase.ConstructorScope.CommandList = null;
			}

			// Token: 0x04000022 RID: 34
			[ThreadStatic]
			private static IntPtr m_Ptr;

			// Token: 0x04000023 RID: 35
			[ThreadStatic]
			private static Hierarchy m_Hierarchy;

			// Token: 0x04000024 RID: 36
			[ThreadStatic]
			private static HierarchyCommandList m_CommandList;
		}
	}
}
