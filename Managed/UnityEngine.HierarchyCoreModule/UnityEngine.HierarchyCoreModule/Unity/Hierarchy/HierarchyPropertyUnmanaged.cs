using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x02000012 RID: 18
	public readonly struct HierarchyPropertyUnmanaged<[global::System.Runtime.CompilerServices.IsUnmanaged] T> : IEquatable<HierarchyPropertyUnmanaged<T>>, IHierarchyProperty<T> where T : struct, ValueType
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002EC4 File Offset: 0x000010C4
		public bool IsCreated
		{
			get
			{
				bool flag;
				if ((in this.m_Property) != HierarchyPropertyId.Null)
				{
					Hierarchy hierarchy = this.m_Hierarchy;
					flag = hierarchy != null && hierarchy.IsCreated;
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002EFC File Offset: 0x000010FC
		internal HierarchyPropertyUnmanaged(Hierarchy hierarchy, in HierarchyPropertyId property)
		{
			bool flag = hierarchy == null;
			if (flag)
			{
				throw new ArgumentNullException("hierarchy");
			}
			bool flag2 = (in property) == HierarchyPropertyId.Null;
			if (flag2)
			{
				throw new ArgumentException("property");
			}
			this.m_Hierarchy = hierarchy;
			this.m_Property = property;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002F4C File Offset: 0x0000114C
		public unsafe void SetValue(in HierarchyNode node, T value)
		{
			bool flag = this.m_Hierarchy == null;
			if (flag)
			{
				throw new NullReferenceException("Hierarchy reference has not been set.");
			}
			bool flag2 = !this.m_Hierarchy.IsCreated;
			if (flag2)
			{
				throw new InvalidOperationException("Hierarchy has been disposed.");
			}
			this.m_Hierarchy.SetPropertyRaw(in this.m_Property, in node, (void*)(&value), sizeof(T));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002FB0 File Offset: 0x000011B0
		public unsafe T GetValue(in HierarchyNode node)
		{
			bool flag = this.m_Hierarchy == null;
			if (flag)
			{
				throw new NullReferenceException("Hierarchy reference has not been set.");
			}
			bool flag2 = !this.m_Hierarchy.IsCreated;
			if (flag2)
			{
				throw new InvalidOperationException("Hierarchy has been disposed.");
			}
			int num;
			void* propertyRaw = this.m_Hierarchy.GetPropertyRaw(in this.m_Property, in node, out num);
			bool flag3 = propertyRaw == null || num != sizeof(T);
			T t;
			if (flag3)
			{
				t = default(T);
			}
			else
			{
				t = *UnsafeUtility.AsRef<T>(propertyRaw);
			}
			return t;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003044 File Offset: 0x00001244
		public void ClearValue(in HierarchyNode node)
		{
			bool flag = this.m_Hierarchy == null;
			if (flag)
			{
				throw new NullReferenceException("Hierarchy reference has not been set.");
			}
			bool flag2 = !this.m_Hierarchy.IsCreated;
			if (flag2)
			{
				throw new InvalidOperationException("Hierarchy has been disposed.");
			}
			this.m_Hierarchy.ClearProperty(in this.m_Property, in node);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000309A File Offset: 0x0000129A
		[ExcludeFromDocs]
		public static bool operator ==(in HierarchyPropertyUnmanaged<T> lhs, in HierarchyPropertyUnmanaged<T> rhs)
		{
			return lhs.m_Hierarchy == rhs.m_Hierarchy && (in lhs.m_Property) == (in rhs.m_Property);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030BE File Offset: 0x000012BE
		[ExcludeFromDocs]
		public static bool operator !=(in HierarchyPropertyUnmanaged<T> lhs, in HierarchyPropertyUnmanaged<T> rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000030CA File Offset: 0x000012CA
		[ExcludeFromDocs]
		public bool Equals(HierarchyPropertyUnmanaged<T> other)
		{
			return this.m_Hierarchy == other.m_Hierarchy && (in this.m_Property) == (in other.m_Property);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000030EF File Offset: 0x000012EF
		[ExcludeFromDocs]
		public override string ToString()
		{
			return this.m_Property.ToString();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003104 File Offset: 0x00001304
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyPropertyUnmanaged<T>)
			{
				HierarchyPropertyUnmanaged<T> hierarchyPropertyUnmanaged = (HierarchyPropertyUnmanaged<T>)obj;
				flag = this.Equals(hierarchyPropertyUnmanaged);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000312A File Offset: 0x0000132A
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return this.m_Property.GetHashCode();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000313D File Offset: 0x0000133D
		T IHierarchyProperty<T>.GetValue(in HierarchyNode node)
		{
			return this.GetValue(in node);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003146 File Offset: 0x00001346
		void IHierarchyProperty<T>.SetValue(in HierarchyNode node, T value)
		{
			this.SetValue(in node, value);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003150 File Offset: 0x00001350
		void IHierarchyProperty<T>.ClearValue(in HierarchyNode node)
		{
			this.ClearValue(in node);
		}

		// Token: 0x0400002B RID: 43
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x0400002C RID: 44
		internal readonly HierarchyPropertyId m_Property;
	}
}
