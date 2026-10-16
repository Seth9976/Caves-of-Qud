using System;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x02000011 RID: 17
	public readonly struct HierarchyPropertyString : IEquatable<HierarchyPropertyString>, IHierarchyProperty<string>
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002C70 File Offset: 0x00000E70
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

		// Token: 0x0600005F RID: 95 RVA: 0x00002CA8 File Offset: 0x00000EA8
		internal HierarchyPropertyString(Hierarchy hierarchy, in HierarchyPropertyId property)
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

		// Token: 0x06000060 RID: 96 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public string GetValue(in HierarchyNode node)
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
			return this.m_Hierarchy.GetPropertyString(in this.m_Property, in node);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D54 File Offset: 0x00000F54
		public void SetValue(in HierarchyNode node, string value)
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
			this.m_Hierarchy.SetPropertyString(in this.m_Property, in node, value);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002DAC File Offset: 0x00000FAC
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

		// Token: 0x06000063 RID: 99 RVA: 0x00002E02 File Offset: 0x00001002
		[ExcludeFromDocs]
		public static bool operator ==(in HierarchyPropertyString lhs, in HierarchyPropertyString rhs)
		{
			return lhs.m_Hierarchy == rhs.m_Hierarchy && (in lhs.m_Property) == (in rhs.m_Property);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002E26 File Offset: 0x00001026
		[ExcludeFromDocs]
		public static bool operator !=(in HierarchyPropertyString lhs, in HierarchyPropertyString rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002E32 File Offset: 0x00001032
		[ExcludeFromDocs]
		public bool Equals(HierarchyPropertyString other)
		{
			return this.m_Hierarchy == other.m_Hierarchy && (in this.m_Property) == (in other.m_Property);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E57 File Offset: 0x00001057
		[ExcludeFromDocs]
		public override string ToString()
		{
			return this.m_Property.ToString();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002E6C File Offset: 0x0000106C
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyPropertyString)
			{
				HierarchyPropertyString hierarchyPropertyString = (HierarchyPropertyString)obj;
				flag = this.Equals(hierarchyPropertyString);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E92 File Offset: 0x00001092
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return this.m_Property.GetHashCode();
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002EA5 File Offset: 0x000010A5
		string IHierarchyProperty<string>.GetValue(in HierarchyNode node)
		{
			return this.GetValue(in node);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002EAE File Offset: 0x000010AE
		void IHierarchyProperty<string>.SetValue(in HierarchyNode node, string value)
		{
			this.SetValue(in node, value);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002EB8 File Offset: 0x000010B8
		void IHierarchyProperty<string>.ClearValue(in HierarchyNode node)
		{
			this.ClearValue(in node);
		}

		// Token: 0x04000029 RID: 41
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x0400002A RID: 42
		internal readonly HierarchyPropertyId m_Property;
	}
}
