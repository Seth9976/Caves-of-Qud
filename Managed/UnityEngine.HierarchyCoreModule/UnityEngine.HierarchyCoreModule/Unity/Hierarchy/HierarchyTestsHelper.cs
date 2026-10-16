using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x02000031 RID: 49
	[NativeHeader("Modules/HierarchyCore/HierarchyTestsHelper.h")]
	internal static class HierarchyTestsHelper
	{
		// Token: 0x060001F7 RID: 503 RVA: 0x0000624C File Offset: 0x0000444C
		[NativeMethod(IsThreadSafe = true)]
		internal static int GenerateNodesTree(Hierarchy hierarchy, in HierarchyNode root, int width, int depth, int maxCount = 0)
		{
			return HierarchyTestsHelper.GenerateNodesTree_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), in root, width, depth, maxCount);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006274 File Offset: 0x00004474
		[NativeMethod(IsThreadSafe = true)]
		internal static void GenerateNodesCount(Hierarchy hierarchy, in HierarchyNode root, int count, int width, int depth)
		{
			HierarchyTestsHelper.GenerateNodesCount_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), in root, count, width, depth);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000629C File Offset: 0x0000449C
		[NativeMethod(IsThreadSafe = true)]
		internal static void GenerateSortIndex(Hierarchy hierarchy, in HierarchyNode root, HierarchyTestsHelper.SortOrder order)
		{
			HierarchyTestsHelper.GenerateSortIndex_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), in root, order);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000062C0 File Offset: 0x000044C0
		internal unsafe static void ForEach(Hierarchy hierarchy, in HierarchyNode root, HierarchyTestsHelper.ForEachDelegate func)
		{
			Stack<HierarchyNode> stack = new Stack<HierarchyNode>();
			stack.Push(root);
			using (NativeArray<HierarchyNode> nativeArray = new NativeArray<HierarchyNode>(hierarchy.Count, Allocator.Temp, NativeArrayOptions.ClearMemory))
			{
				while (stack.Count > 0)
				{
					HierarchyNode hierarchyNode = stack.Pop();
					int childrenCount = hierarchy.GetChildrenCount(in hierarchyNode);
					Span<HierarchyNode> span = new Span<HierarchyNode>(nativeArray.GetUnsafePtr<HierarchyNode>(), childrenCount);
					int children = hierarchy.GetChildren(in hierarchyNode, span);
					bool flag = children != childrenCount;
					if (flag)
					{
						throw new InvalidOperationException(string.Format("Expected GetChildren to return {0}, but was {1}.", childrenCount, children));
					}
					int i = 0;
					int length = span.Length;
					while (i < length)
					{
						HierarchyNode hierarchyNode2 = *span[i];
						func(in hierarchyNode2, i);
						stack.Push(hierarchyNode2);
						i++;
					}
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000063C8 File Offset: 0x000045C8
		[NativeMethod(IsThreadSafe = true)]
		internal static void SetNextHierarchyNodeId(Hierarchy hierarchy, int id)
		{
			HierarchyTestsHelper.SetNextHierarchyNodeId_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), id);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000063EA File Offset: 0x000045EA
		internal static int GetNodeType<T>() where T : HierarchyNodeTypeHandlerBase
		{
			return HierarchyTestsHelper.GetNodeType(typeof(T));
		}

		// Token: 0x060001FD RID: 509
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodeType(Type type);

		// Token: 0x060001FE RID: 510 RVA: 0x000063FC File Offset: 0x000045FC
		[NativeMethod(IsThreadSafe = true)]
		internal static int[] GetRegisteredNodeTypes(Hierarchy hierarchy)
		{
			int[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				HierarchyTestsHelper.GetRegisteredNodeTypes_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006440 File Offset: 0x00004640
		[NativeMethod(IsThreadSafe = true)]
		internal static int GetCapacity(Hierarchy hierarchy)
		{
			return HierarchyTestsHelper.GetCapacity_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy));
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006464 File Offset: 0x00004664
		[NativeMethod(IsThreadSafe = true)]
		internal static int GetVersion(Hierarchy hierarchy)
		{
			return HierarchyTestsHelper.GetVersion_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy));
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006488 File Offset: 0x00004688
		[NativeMethod(IsThreadSafe = true)]
		internal static int GetChildrenCapacity(Hierarchy hierarchy, in HierarchyNode node)
		{
			return HierarchyTestsHelper.GetChildrenCapacity_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy), in node);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000064AC File Offset: 0x000046AC
		internal static bool SearchMatch(HierarchyViewModel model, in HierarchyNode node)
		{
			HierarchyNodeTypeHandlerBase nodeTypeHandlerBase = model.Hierarchy.GetNodeTypeHandlerBase(in node);
			return nodeTypeHandlerBase != null && nodeTypeHandlerBase.Internal_SearchMatch(in node);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000064D8 File Offset: 0x000046D8
		[NativeMethod(IsThreadSafe = true)]
		internal static object GetHierarchyScriptingObject(Hierarchy hierarchy)
		{
			return HierarchyTestsHelper.GetHierarchyScriptingObject_Injected((hierarchy == null) ? ((IntPtr)0) : Hierarchy.BindingsMarshaller.ConvertToNative(hierarchy));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000064FC File Offset: 0x000046FC
		[NativeMethod(IsThreadSafe = true)]
		internal static object GetHierarchyFlattenedScriptingObject(HierarchyFlattened hierarchyFlattened)
		{
			return HierarchyTestsHelper.GetHierarchyFlattenedScriptingObject_Injected((hierarchyFlattened == null) ? ((IntPtr)0) : HierarchyFlattened.BindingsMarshaller.ConvertToNative(hierarchyFlattened));
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006520 File Offset: 0x00004720
		[NativeMethod(IsThreadSafe = true)]
		internal static object GetHierarchyViewModelScriptingObject(HierarchyViewModel viewModel)
		{
			return HierarchyTestsHelper.GetHierarchyViewModelScriptingObject_Injected((viewModel == null) ? ((IntPtr)0) : HierarchyViewModel.BindingsMarshaller.ConvertToNative(viewModel));
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006544 File Offset: 0x00004744
		[NativeMethod(IsThreadSafe = true)]
		internal static object GetHierarchyCommandListScriptingObject(HierarchyCommandList cmdList)
		{
			return HierarchyTestsHelper.GetHierarchyCommandListScriptingObject_Injected((cmdList == null) ? ((IntPtr)0) : HierarchyCommandList.BindingsMarshaller.ConvertToNative(cmdList));
		}

		// Token: 0x06000207 RID: 519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GenerateNodesTree_Injected(IntPtr hierarchy, in HierarchyNode root, int width, int depth, int maxCount);

		// Token: 0x06000208 RID: 520
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateNodesCount_Injected(IntPtr hierarchy, in HierarchyNode root, int count, int width, int depth);

		// Token: 0x06000209 RID: 521
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateSortIndex_Injected(IntPtr hierarchy, in HierarchyNode root, HierarchyTestsHelper.SortOrder order);

		// Token: 0x0600020A RID: 522
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNextHierarchyNodeId_Injected(IntPtr hierarchy, int id);

		// Token: 0x0600020B RID: 523
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRegisteredNodeTypes_Injected(IntPtr hierarchy, out BlittableArrayWrapper ret);

		// Token: 0x0600020C RID: 524
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetCapacity_Injected(IntPtr hierarchy);

		// Token: 0x0600020D RID: 525
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVersion_Injected(IntPtr hierarchy);

		// Token: 0x0600020E RID: 526
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCapacity_Injected(IntPtr hierarchy, in HierarchyNode node);

		// Token: 0x0600020F RID: 527
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetHierarchyScriptingObject_Injected(IntPtr hierarchy);

		// Token: 0x06000210 RID: 528
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetHierarchyFlattenedScriptingObject_Injected(IntPtr hierarchyFlattened);

		// Token: 0x06000211 RID: 529
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetHierarchyViewModelScriptingObject_Injected(IntPtr viewModel);

		// Token: 0x06000212 RID: 530
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetHierarchyCommandListScriptingObject_Injected(IntPtr cmdList);

		// Token: 0x02000032 RID: 50
		[NativeHeader("Modules/HierarchyCore/HierarchyTestsHelper.h")]
		internal enum SortOrder
		{
			// Token: 0x04000092 RID: 146
			Ascending,
			// Token: 0x04000093 RID: 147
			Descending
		}

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x06000214 RID: 532
		internal delegate void ForEachDelegate(in HierarchyNode node, int index);
	}
}
