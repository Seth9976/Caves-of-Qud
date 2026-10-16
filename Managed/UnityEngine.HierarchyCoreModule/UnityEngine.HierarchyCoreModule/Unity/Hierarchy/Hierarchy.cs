using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x0200001F RID: 31
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Modules/HierarchyCore/Public/Hierarchy.h")]
	[NativeHeader("Modules/HierarchyCore/HierarchyBindings.h")]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNodeTypeHandlerBase.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class Hierarchy : IDisposable
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003F74 File Offset: 0x00002174
		public bool IsCreated
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00003F88 File Offset: 0x00002188
		public unsafe readonly ref HierarchyNode Root
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (void*)this.m_RootPtr;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00003FA8 File Offset: 0x000021A8
		public int Count
		{
			[NativeMethod("Count", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Hierarchy.get_Count_Injected(intPtr);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00003FCC File Offset: 0x000021CC
		public bool Updating
		{
			[NativeMethod("Updating", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Hierarchy.get_Updating_Injected(intPtr);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003FF0 File Offset: 0x000021F0
		public bool UpdateNeeded
		{
			[NativeMethod("UpdateNeeded", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Hierarchy.get_UpdateNeeded_Injected(intPtr);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004014 File Offset: 0x00002214
		internal unsafe int Version
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return *(int*)(void*)this.m_VersionPtr;
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004034 File Offset: 0x00002234
		public Hierarchy()
		{
			IntPtr intPtr;
			IntPtr intPtr2;
			this.m_Ptr = Hierarchy.Create(GCHandle.ToIntPtr(GCHandle.Alloc(this)), out intPtr, out intPtr2);
			this.m_RootPtr = intPtr;
			this.m_VersionPtr = intPtr2;
			this.m_IsOwner = true;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004078 File Offset: 0x00002278
		private Hierarchy(IntPtr nativePtr, IntPtr rootPtr, IntPtr versionPtr)
		{
			this.m_Ptr = nativePtr;
			this.m_RootPtr = rootPtr;
			this.m_VersionPtr = versionPtr;
			this.m_IsOwner = false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000040A0 File Offset: 0x000022A0
		~Hierarchy()
		{
			this.Dispose(false);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000040D4 File Offset: 0x000022D4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000040E8 File Offset: 0x000022E8
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool isOwner = this.m_IsOwner;
				if (isOwner)
				{
					Hierarchy.Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000412D File Offset: 0x0000232D
		public T GetOrCreateNodeTypeHandler<T>() where T : HierarchyNodeTypeHandlerBase
		{
			return (T)((object)HierarchyNodeTypeHandlerBase.FromIntPtr(this.GetOrCreateNodeTypeHandler(typeof(T))));
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004149 File Offset: 0x00002349
		public T GetNodeTypeHandlerBase<T>() where T : HierarchyNodeTypeHandlerBase
		{
			return (T)((object)HierarchyNodeTypeHandlerBase.FromIntPtr(this.GetNodeTypeHandlerFromType(typeof(T))));
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004165 File Offset: 0x00002365
		public HierarchyNodeTypeHandlerBase GetNodeTypeHandlerBase(in HierarchyNode node)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(this.GetNodeTypeHandlerFromNode(in node));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004173 File Offset: 0x00002373
		public HierarchyNodeTypeHandlerBase GetNodeTypeHandlerBase(string nodeTypeName)
		{
			return HierarchyNodeTypeHandlerBase.FromIntPtr(this.GetNodeTypeHandlerFromName(nodeTypeName));
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004181 File Offset: 0x00002381
		public HierarchyNodeTypeHandlerBaseEnumerable EnumerateNodeTypeHandlersBase()
		{
			return new HierarchyNodeTypeHandlerBaseEnumerable(this);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004189 File Offset: 0x00002389
		public HierarchyNodeType GetNodeType<T>() where T : HierarchyNodeTypeHandlerBase
		{
			return this.GetNodeTypeFromType(typeof(T));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000419C File Offset: 0x0000239C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNodeType GetNodeType(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNodeType hierarchyNodeType;
			Hierarchy.GetNodeType_Injected(intPtr, in node, out hierarchyNodeType);
			return hierarchyNodeType;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000041C4 File Offset: 0x000023C4
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public void Reserve(int count)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.Reserve_Injected(intPtr, count);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000041E8 File Offset: 0x000023E8
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public void ReserveChildren(in HierarchyNode node, int count)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.ReserveChildren_Injected(intPtr, in node, count);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000420C File Offset: 0x0000240C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Exists(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.Exists_Injected(intPtr, in node);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004230 File Offset: 0x00002430
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode GetNextSibling(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			Hierarchy.GetNextSibling_Injected(intPtr, in node, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004258 File Offset: 0x00002458
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetDepth(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetDepth_Injected(intPtr, in node);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000427B File Offset: 0x0000247B
		public HierarchyNode Add(in HierarchyNode parent)
		{
			return this.AddNode(in parent);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004284 File Offset: 0x00002484
		public HierarchyNode[] Add(in HierarchyNode parent, int count)
		{
			HierarchyNode[] array = new HierarchyNode[count];
			this.AddNodeSpan(in parent, array);
			return array;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000042AC File Offset: 0x000024AC
		public void Add(in HierarchyNode parent, Span<HierarchyNode> outNodes)
		{
			this.AddNodeSpan(in parent, outNodes);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000042B8 File Offset: 0x000024B8
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Remove(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.Remove_Injected(intPtr, in node);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000042DC File Offset: 0x000024DC
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public void RemoveChildren(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.RemoveChildren_Injected(intPtr, in node);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004300 File Offset: 0x00002500
		[NativeMethod(IsThreadSafe = true)]
		public void Clear()
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.Clear_Injected(intPtr);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004324 File Offset: 0x00002524
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool SetParent(in HierarchyNode node, in HierarchyNode parent)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.SetParent_Injected(intPtr, in node, in parent);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004348 File Offset: 0x00002548
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode GetParent(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			Hierarchy.GetParent_Injected(intPtr, in node, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004370 File Offset: 0x00002570
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode GetChild(in HierarchyNode node, int index)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			Hierarchy.GetChild_Injected(intPtr, in node, index, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004398 File Offset: 0x00002598
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildIndex(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetChildIndex_Injected(intPtr, in node);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000043BC File Offset: 0x000025BC
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode[] GetChildren(in HierarchyNode node)
		{
			HierarchyNode[] array2;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Hierarchy.GetChildren_Injected(intPtr, in node, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				HierarchyNode[] array;
				blittableArrayWrapper.Unmarshal<HierarchyNode>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004400 File Offset: 0x00002600
		public int GetChildren(in HierarchyNode node, Span<HierarchyNode> outChildren)
		{
			return this.GetNodeChildrenSpan(in node, outChildren);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000440A File Offset: 0x0000260A
		public HierarchyNodeChildren EnumerateChildren(in HierarchyNode node)
		{
			return new HierarchyNodeChildren(this, this.EnumerateChildrenPtr(in node));
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000441C File Offset: 0x0000261C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCount(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetChildrenCount_Injected(intPtr, in node);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00004440 File Offset: 0x00002640
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCountRecursive(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetChildrenCountRecursive_Injected(intPtr, in node);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00004464 File Offset: 0x00002664
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public void SetSortIndex(in HierarchyNode node, int sortIndex)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.SetSortIndex_Injected(intPtr, in node, sortIndex);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004488 File Offset: 0x00002688
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetSortIndex(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetSortIndex_Injected(intPtr, in node);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000044AC File Offset: 0x000026AC
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public void SortChildren(in HierarchyNode node, bool recurse = false)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.SortChildren_Injected(intPtr, in node, recurse);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000044D0 File Offset: 0x000026D0
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool DoesChildrenNeedsSorting(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.DoesChildrenNeedsSorting_Injected(intPtr, in node);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000044F4 File Offset: 0x000026F4
		public HierarchyPropertyUnmanaged<T> GetOrCreatePropertyUnmanaged<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(string name, HierarchyPropertyStorageType type = HierarchyPropertyStorageType.Dense) where T : struct, ValueType
		{
			HierarchyPropertyDescriptor hierarchyPropertyDescriptor = default(HierarchyPropertyDescriptor);
			hierarchyPropertyDescriptor.Size = UnsafeUtility.SizeOf<T>();
			hierarchyPropertyDescriptor.Type = type;
			HierarchyPropertyId orCreateProperty = this.GetOrCreateProperty(name, in hierarchyPropertyDescriptor);
			return new HierarchyPropertyUnmanaged<T>(this, in orCreateProperty);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004538 File Offset: 0x00002738
		public HierarchyPropertyString GetOrCreatePropertyString(string name)
		{
			HierarchyPropertyDescriptor hierarchyPropertyDescriptor = default(HierarchyPropertyDescriptor);
			hierarchyPropertyDescriptor.Size = 0;
			hierarchyPropertyDescriptor.Type = HierarchyPropertyStorageType.Blob;
			HierarchyPropertyId orCreateProperty = this.GetOrCreateProperty(name, in hierarchyPropertyDescriptor);
			return new HierarchyPropertyString(this, in orCreateProperty);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004578 File Offset: 0x00002778
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public unsafe bool SetName(in HierarchyNode node, string name)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
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
				flag = Hierarchy.SetName_Injected(intPtr, in node, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000045E0 File Offset: 0x000027E0
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public string GetName(in HierarchyNode node)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Hierarchy.GetName_Injected(intPtr, in node, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004620 File Offset: 0x00002820
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public string GetPath(in HierarchyNode node)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Hierarchy.GetPath_Injected(intPtr, in node, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004660 File Offset: 0x00002860
		[NativeMethod(IsThreadSafe = true)]
		public void Update()
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.Update_Injected(intPtr);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004684 File Offset: 0x00002884
		[NativeMethod(IsThreadSafe = true)]
		public bool UpdateIncremental()
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.UpdateIncremental_Injected(intPtr);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000046A8 File Offset: 0x000028A8
		[NativeMethod(IsThreadSafe = true)]
		public bool UpdateIncrementalTimed(double milliseconds)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.UpdateIncrementalTimed_Injected(intPtr, milliseconds);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000046CC File Offset: 0x000028CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Hierarchy FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((Hierarchy)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x060000E7 RID: 231
		[FreeFunction("HierarchyBindings::Create", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(IntPtr handlePtr, out IntPtr rootPtr, out IntPtr versionPtr);

		// Token: 0x060000E8 RID: 232
		[FreeFunction("HierarchyBindings::Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr nativePtr);

		// Token: 0x060000E9 RID: 233 RVA: 0x000046FC File Offset: 0x000028FC
		[FreeFunction("HierarchyBindings::GetOrCreateNodeTypeHandler", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private IntPtr GetOrCreateNodeTypeHandler(Type type)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetOrCreateNodeTypeHandler_Injected(intPtr, type);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004720 File Offset: 0x00002920
		[FreeFunction("HierarchyBindings::GetNodeTypeHandlerFromType", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private IntPtr GetNodeTypeHandlerFromType(Type type)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetNodeTypeHandlerFromType_Injected(intPtr, type);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004744 File Offset: 0x00002944
		[FreeFunction("HierarchyBindings::GetNodeTypeHandlerFromNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private IntPtr GetNodeTypeHandlerFromNode(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetNodeTypeHandlerFromNode_Injected(intPtr, in node);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004768 File Offset: 0x00002968
		[FreeFunction("HierarchyBindings::GetNodeTypeHandlerFromName", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe IntPtr GetNodeTypeHandlerFromName(string nodeTypeName)
		{
			IntPtr nodeTypeHandlerFromName_Injected;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(nodeTypeName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = nodeTypeName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				nodeTypeHandlerFromName_Injected = Hierarchy.GetNodeTypeHandlerFromName_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return nodeTypeHandlerFromName_Injected;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000047D0 File Offset: 0x000029D0
		[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
		[FreeFunction("HierarchyBindings::GetNodeTypeHandlersBaseCount", HasExplicitThis = true, IsThreadSafe = true)]
		internal int GetNodeTypeHandlersBaseCount()
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetNodeTypeHandlersBaseCount_Injected(intPtr);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000047F4 File Offset: 0x000029F4
		[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
		[FreeFunction("HierarchyBindings::GetNodeTypeHandlersBaseSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe int GetNodeTypeHandlersBaseSpan(Span<IntPtr> outHandlers)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<IntPtr> span = outHandlers;
			int nodeTypeHandlersBaseSpan_Injected;
			fixed (IntPtr* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				nodeTypeHandlersBaseSpan_Injected = Hierarchy.GetNodeTypeHandlersBaseSpan_Injected(intPtr, ref managedSpanWrapper);
			}
			return nodeTypeHandlersBaseSpan_Injected;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004838 File Offset: 0x00002A38
		[FreeFunction("HierarchyBindings::GetNodeTypeFromType", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private HierarchyNodeType GetNodeTypeFromType(Type type)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNodeType hierarchyNodeType;
			Hierarchy.GetNodeTypeFromType_Injected(intPtr, type, out hierarchyNodeType);
			return hierarchyNodeType;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004860 File Offset: 0x00002A60
		[FreeFunction("HierarchyBindings::AddNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private HierarchyNode AddNode(in HierarchyNode parent)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			Hierarchy.AddNode_Injected(intPtr, in parent, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004888 File Offset: 0x00002A88
		[FreeFunction("HierarchyBindings::AddNodeSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe void AddNodeSpan(in HierarchyNode parent, Span<HierarchyNode> nodes)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<HierarchyNode> span = nodes;
			fixed (HierarchyNode* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Hierarchy.AddNodeSpan_Injected(intPtr, in parent, ref managedSpanWrapper);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000048CC File Offset: 0x00002ACC
		[FreeFunction("HierarchyBindings::GetNodeChildrenSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetNodeChildrenSpan(in HierarchyNode node, Span<HierarchyNode> outChildren)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<HierarchyNode> span = outChildren;
			int nodeChildrenSpan_Injected;
			fixed (HierarchyNode* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				nodeChildrenSpan_Injected = Hierarchy.GetNodeChildrenSpan_Injected(intPtr, in node, ref managedSpanWrapper);
			}
			return nodeChildrenSpan_Injected;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004910 File Offset: 0x00002B10
		[FreeFunction("HierarchyBindings::EnumerateChildrenPtr", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private IntPtr EnumerateChildrenPtr(in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.EnumerateChildrenPtr_Injected(intPtr, in node);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004934 File Offset: 0x00002B34
		[FreeFunction("HierarchyBindings::GetOrCreateProperty", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe HierarchyPropertyId GetOrCreateProperty(string name, in HierarchyPropertyDescriptor descriptor)
		{
			HierarchyPropertyId hierarchyPropertyId2;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
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
				HierarchyPropertyId hierarchyPropertyId;
				Hierarchy.GetOrCreateProperty_Injected(intPtr, ref managedSpanWrapper, in descriptor, out hierarchyPropertyId);
			}
			finally
			{
				char* ptr = null;
				HierarchyPropertyId hierarchyPropertyId;
				hierarchyPropertyId2 = hierarchyPropertyId;
			}
			return hierarchyPropertyId2;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000049A0 File Offset: 0x00002BA0
		[FreeFunction("HierarchyBindings::SetPropertyRaw", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe void SetPropertyRaw(in HierarchyPropertyId property, in HierarchyNode node, void* ptr, int size)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.SetPropertyRaw_Injected(intPtr, in property, in node, ptr, size);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000049C8 File Offset: 0x00002BC8
		[FreeFunction("HierarchyBindings::GetPropertyRaw", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe void* GetPropertyRaw(in HierarchyPropertyId property, in HierarchyNode node, out int size)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Hierarchy.GetPropertyRaw_Injected(intPtr, in property, in node, out size);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000049F0 File Offset: 0x00002BF0
		[FreeFunction("HierarchyBindings::SetPropertyString", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal unsafe void SetPropertyString(in HierarchyPropertyId property, in HierarchyNode node, string value)
		{
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
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
				Hierarchy.SetPropertyString_Injected(intPtr, in property, in node, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004A58 File Offset: 0x00002C58
		[FreeFunction("HierarchyBindings::GetPropertyString", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal string GetPropertyString(in HierarchyPropertyId property, in HierarchyNode node)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Hierarchy.GetPropertyString_Injected(intPtr, in property, in node, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004A9C File Offset: 0x00002C9C
		[FreeFunction("HierarchyBindings::ClearProperty", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		internal void ClearProperty(in HierarchyPropertyId property, in HierarchyNode node)
		{
			IntPtr intPtr = Hierarchy.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Hierarchy.ClearProperty_Injected(intPtr, in property, in node);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004AC0 File Offset: 0x00002CC0
		[RequiredByNativeCode]
		private static IntPtr CreateHierarchy(IntPtr nativePtr, IntPtr rootPtr, IntPtr versionPtr)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new Hierarchy(nativePtr, rootPtr, versionPtr)));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000412D File Offset: 0x0000232D
		[Obsolete("RegisterNodeTypeHandler has been renamed GetOrCreateNodeTypeHandler (UnityUpgradable) -> GetOrCreateNodeTypeHandler<T>()")]
		public T RegisterNodeTypeHandler<T>() where T : HierarchyNodeTypeHandlerBase
		{
			return (T)((object)HierarchyNodeTypeHandlerBase.FromIntPtr(this.GetOrCreateNodeTypeHandler(typeof(T))));
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000027D0 File Offset: 0x000009D0
		[Obsolete("UnregisterNodeTypeHandler no longer has any effect and will be removed in a future release.")]
		public void UnregisterNodeTypeHandler<T>() where T : HierarchyNodeTypeHandlerBase
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004AD4 File Offset: 0x00002CD4
		[Obsolete("GetAllNodeTypeHandlersBaseCount is obsolete, please use EnumerateNodeTypeHandlersBase instead.")]
		public int GetAllNodeTypeHandlersBaseCount()
		{
			return this.GetNodeTypeHandlersBaseCount();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004ADC File Offset: 0x00002CDC
		[Obsolete("GetAllNodeTypeHandlersBase is obsolete, please use EnumerateNodeTypeHandlersBase instead.")]
		public void GetAllNodeTypeHandlersBase(List<HierarchyNodeTypeHandlerBase> handlers)
		{
			handlers.Clear();
			foreach (HierarchyNodeTypeHandlerBase hierarchyNodeTypeHandlerBase in this.EnumerateNodeTypeHandlersBase())
			{
				handlers.Add(hierarchyNodeTypeHandlerBase);
			}
		}

		// Token: 0x060000FF RID: 255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_Count_Injected(IntPtr _unity_self);

		// Token: 0x06000100 RID: 256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_Updating_Injected(IntPtr _unity_self);

		// Token: 0x06000101 RID: 257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_UpdateNeeded_Injected(IntPtr _unity_self);

		// Token: 0x06000102 RID: 258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNodeType_Injected(IntPtr _unity_self, in HierarchyNode node, out HierarchyNodeType ret);

		// Token: 0x06000103 RID: 259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Reserve_Injected(IntPtr _unity_self, int count);

		// Token: 0x06000104 RID: 260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReserveChildren_Injected(IntPtr _unity_self, in HierarchyNode node, int count);

		// Token: 0x06000105 RID: 261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Exists_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000106 RID: 262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextSibling_Injected(IntPtr _unity_self, in HierarchyNode node, out HierarchyNode ret);

		// Token: 0x06000107 RID: 263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDepth_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000108 RID: 264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Remove_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000109 RID: 265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveChildren_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600010A RID: 266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self);

		// Token: 0x0600010B RID: 267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetParent_Injected(IntPtr _unity_self, in HierarchyNode node, in HierarchyNode parent);

		// Token: 0x0600010C RID: 268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParent_Injected(IntPtr _unity_self, in HierarchyNode node, out HierarchyNode ret);

		// Token: 0x0600010D RID: 269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetChild_Injected(IntPtr _unity_self, in HierarchyNode node, int index, out HierarchyNode ret);

		// Token: 0x0600010E RID: 270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildIndex_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600010F RID: 271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetChildren_Injected(IntPtr _unity_self, in HierarchyNode node, out BlittableArrayWrapper ret);

		// Token: 0x06000110 RID: 272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCount_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000111 RID: 273
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCountRecursive_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000112 RID: 274
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSortIndex_Injected(IntPtr _unity_self, in HierarchyNode node, int sortIndex);

		// Token: 0x06000113 RID: 275
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSortIndex_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000114 RID: 276
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SortChildren_Injected(IntPtr _unity_self, in HierarchyNode node, bool recurse);

		// Token: 0x06000115 RID: 277
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool DoesChildrenNeedsSorting_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000116 RID: 278
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetName_Injected(IntPtr _unity_self, in HierarchyNode node, ref ManagedSpanWrapper name);

		// Token: 0x06000117 RID: 279
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetName_Injected(IntPtr _unity_self, in HierarchyNode node, out ManagedSpanWrapper ret);

		// Token: 0x06000118 RID: 280
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPath_Injected(IntPtr _unity_self, in HierarchyNode node, out ManagedSpanWrapper ret);

		// Token: 0x06000119 RID: 281
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self);

		// Token: 0x0600011A RID: 282
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateIncremental_Injected(IntPtr _unity_self);

		// Token: 0x0600011B RID: 283
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateIncrementalTimed_Injected(IntPtr _unity_self, double milliseconds);

		// Token: 0x0600011C RID: 284
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetOrCreateNodeTypeHandler_Injected(IntPtr _unity_self, Type type);

		// Token: 0x0600011D RID: 285
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNodeTypeHandlerFromType_Injected(IntPtr _unity_self, Type type);

		// Token: 0x0600011E RID: 286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNodeTypeHandlerFromNode_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600011F RID: 287
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNodeTypeHandlerFromName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper nodeTypeName);

		// Token: 0x06000120 RID: 288
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodeTypeHandlersBaseCount_Injected(IntPtr _unity_self);

		// Token: 0x06000121 RID: 289
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodeTypeHandlersBaseSpan_Injected(IntPtr _unity_self, ref ManagedSpanWrapper outHandlers);

		// Token: 0x06000122 RID: 290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNodeTypeFromType_Injected(IntPtr _unity_self, Type type, out HierarchyNodeType ret);

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddNode_Injected(IntPtr _unity_self, in HierarchyNode parent, out HierarchyNode ret);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddNodeSpan_Injected(IntPtr _unity_self, in HierarchyNode parent, ref ManagedSpanWrapper nodes);

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodeChildrenSpan_Injected(IntPtr _unity_self, in HierarchyNode node, ref ManagedSpanWrapper outChildren);

		// Token: 0x06000126 RID: 294
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr EnumerateChildrenPtr_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetOrCreateProperty_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, in HierarchyPropertyDescriptor descriptor, out HierarchyPropertyId ret);

		// Token: 0x06000128 RID: 296
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void SetPropertyRaw_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node, void* ptr, int size);

		// Token: 0x06000129 RID: 297
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* GetPropertyRaw_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node, out int size);

		// Token: 0x0600012A RID: 298
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPropertyString_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node, ref ManagedSpanWrapper value);

		// Token: 0x0600012B RID: 299
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyString_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node, out ManagedSpanWrapper ret);

		// Token: 0x0600012C RID: 300
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearProperty_Injected(IntPtr _unity_self, in HierarchyPropertyId property, in HierarchyNode node);

		// Token: 0x04000047 RID: 71
		private IntPtr m_Ptr;

		// Token: 0x04000048 RID: 72
		private readonly IntPtr m_RootPtr;

		// Token: 0x04000049 RID: 73
		private readonly IntPtr m_VersionPtr;

		// Token: 0x0400004A RID: 74
		private readonly bool m_IsOwner;

		// Token: 0x02000020 RID: 32
		internal static class BindingsMarshaller
		{
			// Token: 0x0600012D RID: 301 RVA: 0x00004B40 File Offset: 0x00002D40
			public static IntPtr ConvertToNative(Hierarchy hierarchy)
			{
				return hierarchy.m_Ptr;
			}
		}
	}
}
