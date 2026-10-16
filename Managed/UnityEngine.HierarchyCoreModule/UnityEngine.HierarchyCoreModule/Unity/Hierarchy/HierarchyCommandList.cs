using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x02000021 RID: 33
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Modules/HierarchyCore/HierarchyCommandListBindings.h")]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyCommandList.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HierarchyCommandList : IDisposable
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004B48 File Offset: 0x00002D48
		public bool IsCreated
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004B5C File Offset: 0x00002D5C
		public int Size
		{
			[NativeMethod("Size", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyCommandList.get_Size_Injected(intPtr);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00004B80 File Offset: 0x00002D80
		public int Capacity
		{
			[NativeMethod("Capacity", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyCommandList.get_Capacity_Injected(intPtr);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004BA4 File Offset: 0x00002DA4
		public bool IsEmpty
		{
			[NativeMethod("IsEmpty", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyCommandList.get_IsEmpty_Injected(intPtr);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public bool IsExecuting
		{
			[NativeMethod("IsExecuting", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyCommandList.get_IsExecuting_Injected(intPtr);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004BEA File Offset: 0x00002DEA
		public unsafe HierarchyCommandList(Hierarchy hierarchy, int initialCapacity = 65536)
			: this(hierarchy, *HierarchyNodeType.Null, initialCapacity)
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004C00 File Offset: 0x00002E00
		internal HierarchyCommandList(Hierarchy hierarchy, HierarchyNodeType nodeType, int initialCapacity = 65536)
		{
			this.m_Ptr = HierarchyCommandList.Create(GCHandle.ToIntPtr(GCHandle.Alloc(this)), hierarchy, nodeType, initialCapacity);
			this.m_IsOwner = true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004C2A File Offset: 0x00002E2A
		private HierarchyCommandList(IntPtr nativePtr)
		{
			this.m_Ptr = nativePtr;
			this.m_IsOwner = false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004C44 File Offset: 0x00002E44
		~HierarchyCommandList()
		{
			this.Dispose(false);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004C78 File Offset: 0x00002E78
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004C8C File Offset: 0x00002E8C
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool isOwner = this.m_IsOwner;
				if (isOwner)
				{
					HierarchyCommandList.Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004CD4 File Offset: 0x00002ED4
		[NativeMethod(IsThreadSafe = true)]
		public void Clear()
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyCommandList.Clear_Injected(intPtr);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004CF8 File Offset: 0x00002EF8
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Reserve(int count)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.Reserve_Injected(intPtr, count);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004D1B File Offset: 0x00002F1B
		public bool Add(in HierarchyNode parent, out HierarchyNode node)
		{
			return this.AddNode(in parent, out node);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004D28 File Offset: 0x00002F28
		public bool Add(in HierarchyNode parent, int count, out HierarchyNode[] nodes)
		{
			nodes = new HierarchyNode[count];
			return this.AddNodeSpan(in parent, nodes);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004D50 File Offset: 0x00002F50
		public bool Add(in HierarchyNode parent, Span<HierarchyNode> outNodes)
		{
			return this.AddNodeSpan(in parent, outNodes);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004D5C File Offset: 0x00002F5C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Remove(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.Remove_Injected(intPtr, in node);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004D80 File Offset: 0x00002F80
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool RemoveChildren(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.RemoveChildren_Injected(intPtr, in node);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004DA4 File Offset: 0x00002FA4
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool SetParent(in HierarchyNode node, in HierarchyNode parent)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.SetParent_Injected(intPtr, in node, in parent);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004DC8 File Offset: 0x00002FC8
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool SetSortIndex(in HierarchyNode node, int sortIndex)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.SetSortIndex_Injected(intPtr, in node, sortIndex);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004DEC File Offset: 0x00002FEC
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool SortChildren(in HierarchyNode node, bool recurse = false)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.SortChildren_Injected(intPtr, in node, recurse);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004E10 File Offset: 0x00003010
		public unsafe bool SetProperty<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(in HierarchyPropertyUnmanaged<T> property, in HierarchyNode node, T value) where T : struct, ValueType
		{
			return this.SetNodePropertyRaw(in property.m_Property, in node, (void*)(&value), sizeof(T));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004E39 File Offset: 0x00003039
		public bool SetProperty(in HierarchyPropertyString property, in HierarchyNode node, string value)
		{
			return this.SetNodePropertyString(in property.m_Property, in node, value);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004E49 File Offset: 0x00003049
		public bool ClearProperty<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(in HierarchyPropertyUnmanaged<T> property, in HierarchyNode node) where T : struct, ValueType
		{
			return this.ClearNodeProperty(in property.m_Property, in node);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004E58 File Offset: 0x00003058
		public bool ClearProperty(in HierarchyPropertyString property, in HierarchyNode node)
		{
			return this.ClearNodeProperty(in property.m_Property, in node);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004E68 File Offset: 0x00003068
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public unsafe bool SetName(in HierarchyNode node, string name)
		{
			bool flag;
			try
			{
				IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
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
				flag = HierarchyCommandList.SetName_Injected(intPtr, in node, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004ED0 File Offset: 0x000030D0
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public void Execute()
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyCommandList.Execute_Injected(intPtr);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004EF4 File Offset: 0x000030F4
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool ExecuteIncremental()
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.ExecuteIncremental_Injected(intPtr);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004F18 File Offset: 0x00003118
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool ExecuteIncrementalTimed(double milliseconds)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.ExecuteIncrementalTimed_Injected(intPtr, milliseconds);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004F3C File Offset: 0x0000313C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HierarchyCommandList FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((HierarchyCommandList)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004F6C File Offset: 0x0000316C
		[FreeFunction("HierarchyCommandListBindings::Create", IsThreadSafe = true)]
		private static IntPtr Create(IntPtr handlePtr, Hierarchy hierarchy, HierarchyNodeType nodeType, int initialCapacity)
		{
			return HierarchyCommandList.Create_Injected(handlePtr, (hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), ref nodeType, initialCapacity);
		}

		// Token: 0x0600014D RID: 333
		[FreeFunction("HierarchyCommandListBindings::Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr nativePtr);

		// Token: 0x0600014E RID: 334 RVA: 0x00004F94 File Offset: 0x00003194
		[FreeFunction("HierarchyCommandListBindings::AddNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool AddNode(in HierarchyNode parent, out HierarchyNode node)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.AddNode_Injected(intPtr, in parent, out node);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004FB8 File Offset: 0x000031B8
		[FreeFunction("HierarchyCommandListBindings::AddNodeSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe bool AddNodeSpan(in HierarchyNode parent, Span<HierarchyNode> outNodes)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<HierarchyNode> span = outNodes;
			bool flag;
			fixed (HierarchyNode* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				flag = HierarchyCommandList.AddNodeSpan_Injected(intPtr, in parent, ref managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004FFC File Offset: 0x000031FC
		[FreeFunction("HierarchyCommandListBindings::SetNodePropertyRaw", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe bool SetNodePropertyRaw(in HierarchyPropertyId property, in HierarchyNode node, void* ptr, int size)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.SetNodePropertyRaw_Injected(intPtr, in property, in node, ptr, size);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005024 File Offset: 0x00003224
		[FreeFunction("HierarchyCommandListBindings::SetNodePropertyString", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe bool SetNodePropertyString(in HierarchyPropertyId property, in HierarchyNode node, string value)
		{
			bool flag;
			try
			{
				IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = value.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = HierarchyCommandList.SetNodePropertyString_Injected(intPtr, in property, in node, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000508C File Offset: 0x0000328C
		[FreeFunction("HierarchyCommandListBindings::ClearNodeProperty", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool ClearNodeProperty(in HierarchyPropertyId property, in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyCommandList.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyCommandList.ClearNodeProperty_Injected(intPtr, in property, in node);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000050B0 File Offset: 0x000032B0
		[RequiredByNativeCode]
		private static IntPtr CreateCommandList(IntPtr nativePtr)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new HierarchyCommandList(nativePtr)));
		}

		// Token: 0x06000154 RID: 340
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_Size_Injected(IntPtr _unity_self);

		// Token: 0x06000155 RID: 341
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_Capacity_Injected(IntPtr _unity_self);

		// Token: 0x06000156 RID: 342
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_IsEmpty_Injected(IntPtr _unity_self);

		// Token: 0x06000157 RID: 343
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_IsExecuting_Injected(IntPtr _unity_self);

		// Token: 0x06000158 RID: 344
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self);

		// Token: 0x06000159 RID: 345
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Reserve_Injected(IntPtr _unity_self, int count);

		// Token: 0x0600015A RID: 346
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Remove_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600015B RID: 347
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveChildren_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600015C RID: 348
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetParent_Injected(IntPtr _unity_self, in HierarchyNode node, in HierarchyNode parent);

		// Token: 0x0600015D RID: 349
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetSortIndex_Injected(IntPtr _unity_self, in HierarchyNode node, int sortIndex);

		// Token: 0x0600015E RID: 350
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SortChildren_Injected(IntPtr _unity_self, in HierarchyNode node, bool recurse);

		// Token: 0x0600015F RID: 351
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetName_Injected(IntPtr _unity_self, in HierarchyNode node, ref ManagedSpanWrapper name);

		// Token: 0x06000160 RID: 352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Execute_Injected(IntPtr _unity_self);

		// Token: 0x06000161 RID: 353
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ExecuteIncremental_Injected(IntPtr _unity_self);

		// Token: 0x06000162 RID: 354
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ExecuteIncrementalTimed_Injected(IntPtr _unity_self, double milliseconds);

		// Token: 0x06000163 RID: 355
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(IntPtr handlePtr, IntPtr hierarchy, [In] ref HierarchyNodeType nodeType, int initialCapacity);

		// Token: 0x06000164 RID: 356
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddNode_Injected(IntPtr _unity_self, in HierarchyNode parent, out HierarchyNode node);

		// Token: 0x06000165 RID: 357
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddNodeSpan_Injected(IntPtr _unity_self, in HierarchyNode parent, ref ManagedSpanWrapper outNodes);

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool SetNodePropertyRaw_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node, void* ptr, int size);

		// Token: 0x06000167 RID: 359
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetNodePropertyString_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node, ref ManagedSpanWrapper value);

		// Token: 0x06000168 RID: 360
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ClearNodeProperty_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node);

		// Token: 0x0400004B RID: 75
		private IntPtr m_Ptr;

		// Token: 0x0400004C RID: 76
		private readonly bool m_IsOwner;

		// Token: 0x02000022 RID: 34
		internal static class BindingsMarshaller
		{
			// Token: 0x06000169 RID: 361 RVA: 0x000050C2 File Offset: 0x000032C2
			public static IntPtr ConvertToNative(HierarchyCommandList cmdList)
			{
				return cmdList.m_Ptr;
			}
		}
	}
}
