using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x02000602 RID: 1538
	internal class VisualNodePropertyRegistry
	{
		// Token: 0x06002DB8 RID: 11704 RVA: 0x000A2550 File Offset: 0x000A0750
		public static void RegisterInternalProperty<TProperty>()
		{
			bool flag = VisualNodePropertyRegistry.TypeIndex<TProperty>.Index != 0;
			if (flag)
			{
				throw new InvalidOperationException("TProperty has already been registered");
			}
			VisualNodePropertyRegistry.TypeIndex<TProperty>.Index = -(++VisualNodePropertyRegistry.s_InternalPropertyCount);
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000A2588 File Offset: 0x000A0788
		public static void RegisterHierarchyProperty<TProperty>()
		{
			bool flag = VisualNodePropertyRegistry.TypeIndex<TProperty>.Index != 0;
			if (flag)
			{
				throw new InvalidOperationException("TProperty has already been registered");
			}
			VisualNodePropertyRegistry.TypeIndex<TProperty>.Index = ++VisualNodePropertyRegistry.s_HierarchyPropertyCount;
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000A25C0 File Offset: 0x000A07C0
		public unsafe VisualNodePropertyRegistry(VisualManager manager)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
			this.m_Manager = manager;
			this.m_InternalPropertyData = new VisualNodePropertyData*[VisualNodePropertyRegistry.s_InternalPropertyCount];
			for (int i = 0; i < VisualNodePropertyRegistry.s_InternalPropertyCount; i++)
			{
				this.m_InternalPropertyData[i] = (VisualNodePropertyData*)(void*)this.m_Manager.GetPropertyPtr(i);
			}
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000A2635 File Offset: 0x000A0835
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool IsInternalProperty(int typeIndex)
		{
			return typeIndex < 0;
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000A263B File Offset: 0x000A083B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private VisualNodeProperty<T> GetInternalProperty<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int typeIndex) where T : struct, ValueType
		{
			return new VisualNodeProperty<T>(this.m_InternalPropertyData[-typeIndex - 1]);
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x00026C86 File Offset: 0x00024E86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private HierarchyPropertyUnmanaged<T> GetHierarchyProperty<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(int typeIndex) where T : struct, ValueType
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000A2650 File Offset: 0x000A0850
		public unsafe T GetProperty<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(VisualNodeHandle handle) where T : struct, ValueType
		{
			int index = VisualNodePropertyRegistry.TypeIndex<T>.Index;
			bool flag = index == 0;
			if (flag)
			{
				throw new InvalidOperationException("The property type has not been registered");
			}
			bool flag2 = this.IsInternalProperty(index);
			T t;
			if (flag2)
			{
				t = *this.GetInternalProperty<T>(index)[handle];
			}
			else
			{
				t = this.GetHierarchyProperty<T>(index).GetValue(UnsafeUtility.As<VisualNodeHandle, HierarchyNode>(ref handle));
			}
			return t;
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000A26BC File Offset: 0x000A08BC
		public unsafe void SetProperty<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(VisualNodeHandle handle, in T value) where T : struct, ValueType
		{
			int index = VisualNodePropertyRegistry.TypeIndex<T>.Index;
			bool flag = index == 0;
			if (flag)
			{
				throw new InvalidOperationException("The property type has not been registered");
			}
			bool flag2 = this.IsInternalProperty(index);
			if (flag2)
			{
				*this.GetInternalProperty<T>(index)[handle] = value;
			}
			else
			{
				this.GetHierarchyProperty<T>(index).SetValue(UnsafeUtility.As<VisualNodeHandle, HierarchyNode>(ref handle), value);
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000A2730 File Offset: 0x000A0930
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref T GetPropertyRef<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(VisualNodeHandle handle) where T : struct, ValueType
		{
			int index = VisualNodePropertyRegistry.TypeIndex<T>.Index;
			bool flag = index == 0;
			if (flag)
			{
				throw new InvalidOperationException("The property type has not been registered");
			}
			bool flag2 = index > 0;
			if (flag2)
			{
				throw new InvalidOperationException("The property type is not an internal property");
			}
			return this.GetInternalProperty<T>(index)[handle];
		}

		// Token: 0x040014AC RID: 5292
		private static int s_InternalPropertyCount;

		// Token: 0x040014AD RID: 5293
		private static int s_HierarchyPropertyCount;

		// Token: 0x040014AE RID: 5294
		private readonly VisualManager m_Manager;

		// Token: 0x040014AF RID: 5295
		private unsafe readonly VisualNodePropertyData*[] m_InternalPropertyData;

		// Token: 0x040014B0 RID: 5296
		private readonly ChunkAllocatingArray<object> m_Bindings = new ChunkAllocatingArray<object>();

		// Token: 0x02000603 RID: 1539
		private struct TypeIndex<T>
		{
			// Token: 0x040014B1 RID: 5297
			public static int Index;
		}

		// Token: 0x02000604 RID: 1540
		private class HierarchyPropertyBinding<[global::System.Runtime.CompilerServices.IsUnmanaged] TProperty> where TProperty : struct, ValueType
		{
			// Token: 0x06002DC1 RID: 11713 RVA: 0x000A2780 File Offset: 0x000A0980
			public HierarchyPropertyBinding(HierarchyPropertyUnmanaged<TProperty> property)
			{
				this.Property = property;
			}

			// Token: 0x040014B2 RID: 5298
			public readonly HierarchyPropertyUnmanaged<TProperty> Property;
		}
	}
}
