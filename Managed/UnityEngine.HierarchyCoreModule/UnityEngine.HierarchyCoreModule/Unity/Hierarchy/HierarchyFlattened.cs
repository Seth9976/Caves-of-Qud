using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x02000023 RID: 35
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Modules/HierarchyCore/HierarchyFlattenedBindings.h")]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyFlattened.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HierarchyFlattened : IDisposable
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600016A RID: 362 RVA: 0x000050CA File Offset: 0x000032CA
		public bool IsCreated
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600016B RID: 363 RVA: 0x000050DC File Offset: 0x000032DC
		public int Count
		{
			get
			{
				return this.m_NodesCount;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600016C RID: 364 RVA: 0x000050E4 File Offset: 0x000032E4
		public bool Updating
		{
			[NativeMethod("Updating", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyFlattened.get_Updating_Injected(intPtr);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005108 File Offset: 0x00003308
		public bool UpdateNeeded
		{
			[NativeMethod("UpdateNeeded", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyFlattened.get_UpdateNeeded_Injected(intPtr);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000512A File Offset: 0x0000332A
		public Hierarchy Hierarchy
		{
			get
			{
				return this.m_Hierarchy;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00005132 File Offset: 0x00003332
		internal unsafe HierarchyFlattenedNode* NodesPtr
		{
			get
			{
				return (HierarchyFlattenedNode*)(void*)this.m_NodesPtr;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000513F File Offset: 0x0000333F
		internal int Version
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005148 File Offset: 0x00003348
		public HierarchyFlattened(Hierarchy hierarchy)
		{
			IntPtr intPtr;
			int num;
			int num2;
			this.m_Ptr = HierarchyFlattened.Create(GCHandle.ToIntPtr(GCHandle.Alloc(this)), hierarchy, out intPtr, out num, out num2);
			this.m_Hierarchy = hierarchy;
			this.m_NodesPtr = intPtr;
			this.m_NodesCount = num;
			this.m_Version = num2;
			this.m_IsOwner = true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000519D File Offset: 0x0000339D
		private HierarchyFlattened(IntPtr nativePtr, Hierarchy hierarchy, IntPtr nodesPtr, int nodesCount, int version)
		{
			this.m_Ptr = nativePtr;
			this.m_Hierarchy = hierarchy;
			this.m_NodesPtr = nodesPtr;
			this.m_NodesCount = nodesCount;
			this.m_Version = version;
			this.m_IsOwner = false;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000051D4 File Offset: 0x000033D4
		~HierarchyFlattened()
		{
			this.Dispose(false);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005208 File Offset: 0x00003408
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000521C File Offset: 0x0000341C
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool isOwner = this.m_IsOwner;
				if (isOwner)
				{
					HierarchyFlattened.Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x1700002C RID: 44
		public unsafe ref HierarchyFlattenedNode this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= this.m_NodesCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return (byte*)(void*)this.m_NodesPtr + (IntPtr)index * (IntPtr)sizeof(HierarchyFlattenedNode);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000052B0 File Offset: 0x000034B0
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int IndexOf(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.IndexOf_Injected(intPtr, in node);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000052D4 File Offset: 0x000034D4
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Contains(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.Contains_Injected(intPtr, in node);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000052F8 File Offset: 0x000034F8
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode GetParent(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			HierarchyFlattened.GetParent_Injected(intPtr, in node, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005320 File Offset: 0x00003520
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode GetNextSibling(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			HierarchyFlattened.GetNextSibling_Injected(intPtr, in node, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005346 File Offset: 0x00003546
		public HierarchyFlattenedNodeChildren EnumerateChildren(in HierarchyNode node)
		{
			return new HierarchyFlattenedNodeChildren(this, in node);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005350 File Offset: 0x00003550
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCount(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.GetChildrenCount_Injected(intPtr, in node);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005374 File Offset: 0x00003574
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCountRecursive(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.GetChildrenCountRecursive_Injected(intPtr, in node);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005398 File Offset: 0x00003598
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetDepth(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.GetDepth_Injected(intPtr, in node);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x000053BC File Offset: 0x000035BC
		[NativeMethod(IsThreadSafe = true)]
		public void Update()
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyFlattened.Update_Injected(intPtr);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000053E0 File Offset: 0x000035E0
		[NativeMethod(IsThreadSafe = true)]
		public bool UpdateIncremental()
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.UpdateIncremental_Injected(intPtr);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00005404 File Offset: 0x00003604
		[NativeMethod(IsThreadSafe = true)]
		public bool UpdateIncrementalTimed(double milliseconds)
		{
			IntPtr intPtr = HierarchyFlattened.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyFlattened.UpdateIncrementalTimed_Injected(intPtr, milliseconds);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00005427 File Offset: 0x00003627
		public HierarchyFlattened.Enumerator GetEnumerator()
		{
			return new HierarchyFlattened.Enumerator(this);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00005430 File Offset: 0x00003630
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HierarchyFlattened FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((HierarchyFlattened)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00005460 File Offset: 0x00003660
		[FreeFunction("HierarchyFlattenedBindings::Create", IsThreadSafe = true)]
		private static IntPtr Create(IntPtr handlePtr, Hierarchy hierarchy, out IntPtr nodesPtr, out int nodesCount, out int version)
		{
			return HierarchyFlattened.Create_Injected(handlePtr, (hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), out nodesPtr, out nodesCount, out version);
		}

		// Token: 0x06000185 RID: 389
		[FreeFunction("HierarchyFlattenedBindings::Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr nativePtr);

		// Token: 0x06000186 RID: 390 RVA: 0x00005486 File Offset: 0x00003686
		[RequiredByNativeCode]
		private static IntPtr CreateHierarchyFlattened(IntPtr nativePtr, IntPtr hierarchyPtr, IntPtr nodesPtr, int nodesCount, int version)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new HierarchyFlattened(nativePtr, Hierarchy.FromIntPtr(hierarchyPtr), nodesPtr, nodesCount, version)));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000054A4 File Offset: 0x000036A4
		[RequiredByNativeCode]
		private static void UpdateHierarchyFlattened(IntPtr handlePtr, IntPtr nodesPtr, int nodesCount, int version)
		{
			HierarchyFlattened hierarchyFlattened = HierarchyFlattened.FromIntPtr(handlePtr);
			hierarchyFlattened.m_NodesPtr = nodesPtr;
			hierarchyFlattened.m_NodesCount = nodesCount;
			hierarchyFlattened.m_Version = version;
		}

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_Updating_Injected(IntPtr _unity_self);

		// Token: 0x06000189 RID: 393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_UpdateNeeded_Injected(IntPtr _unity_self);

		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int IndexOf_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Contains_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600018C RID: 396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParent_Injected(IntPtr _unity_self, in HierarchyNode node, out HierarchyNode ret);

		// Token: 0x0600018D RID: 397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextSibling_Injected(IntPtr _unity_self, in HierarchyNode node, out HierarchyNode ret);

		// Token: 0x0600018E RID: 398
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCount_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600018F RID: 399
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCountRecursive_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000190 RID: 400
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDepth_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000191 RID: 401
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self);

		// Token: 0x06000192 RID: 402
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateIncremental_Injected(IntPtr _unity_self);

		// Token: 0x06000193 RID: 403
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateIncrementalTimed_Injected(IntPtr _unity_self, double milliseconds);

		// Token: 0x06000194 RID: 404
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(IntPtr handlePtr, IntPtr hierarchy, out IntPtr nodesPtr, out int nodesCount, out int version);

		// Token: 0x0400004D RID: 77
		private IntPtr m_Ptr;

		// Token: 0x0400004E RID: 78
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x0400004F RID: 79
		private IntPtr m_NodesPtr;

		// Token: 0x04000050 RID: 80
		private int m_NodesCount;

		// Token: 0x04000051 RID: 81
		private int m_Version;

		// Token: 0x04000052 RID: 82
		private readonly bool m_IsOwner;

		// Token: 0x02000024 RID: 36
		internal static class BindingsMarshaller
		{
			// Token: 0x06000195 RID: 405 RVA: 0x000054CE File Offset: 0x000036CE
			public static IntPtr ConvertToNative(HierarchyFlattened hierarchyFlattened)
			{
				return hierarchyFlattened.m_Ptr;
			}
		}

		// Token: 0x02000025 RID: 37
		public struct Enumerator
		{
			// Token: 0x06000196 RID: 406 RVA: 0x000054D6 File Offset: 0x000036D6
			internal unsafe Enumerator(HierarchyFlattened hierarchyFlattened)
			{
				this.m_HierarchyFlattened = hierarchyFlattened;
				this.m_NodesPtr = (HierarchyFlattenedNode*)(void*)hierarchyFlattened.m_NodesPtr;
				this.m_NodesCount = hierarchyFlattened.m_NodesCount;
				this.m_Version = hierarchyFlattened.Version;
				this.m_Index = -1;
			}

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000197 RID: 407 RVA: 0x00005510 File Offset: 0x00003710
			public readonly ref HierarchyFlattenedNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					bool flag = this.m_Version != this.m_HierarchyFlattened.m_Version;
					if (flag)
					{
						throw new InvalidOperationException("HierarchyFlattened was modified.");
					}
					return this.m_NodesPtr + this.m_Index;
				}
			}

			// Token: 0x06000198 RID: 408 RVA: 0x0000555C File Offset: 0x0000375C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_NodesCount;
			}

			// Token: 0x04000053 RID: 83
			private readonly HierarchyFlattened m_HierarchyFlattened;

			// Token: 0x04000054 RID: 84
			private unsafe readonly HierarchyFlattenedNode* m_NodesPtr;

			// Token: 0x04000055 RID: 85
			private readonly int m_NodesCount;

			// Token: 0x04000056 RID: 86
			private readonly int m_Version;

			// Token: 0x04000057 RID: 87
			private int m_Index;
		}
	}
}
