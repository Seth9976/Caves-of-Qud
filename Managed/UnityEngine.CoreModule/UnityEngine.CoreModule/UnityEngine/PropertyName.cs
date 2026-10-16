using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200023F RID: 575
	[UsedByNativeCode]
	public struct PropertyName : IEquatable<PropertyName>
	{
		// Token: 0x06001E75 RID: 7797 RVA: 0x0003AAE4 File Offset: 0x00038CE4
		public PropertyName(string name)
		{
			this = new PropertyName(PropertyNameUtils.PropertyNameFromString(name));
		}

		// Token: 0x06001E76 RID: 7798 RVA: 0x0003AAF4 File Offset: 0x00038CF4
		public PropertyName(PropertyName other)
		{
			this.id = other.id;
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0003AB03 File Offset: 0x00038D03
		public PropertyName(int id)
		{
			this.id = id;
		}

		// Token: 0x06001E78 RID: 7800 RVA: 0x0003AB10 File Offset: 0x00038D10
		public static bool IsNullOrEmpty(PropertyName prop)
		{
			return prop.id == 0;
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x0003AB2C File Offset: 0x00038D2C
		public static bool operator ==(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id == rhs.id;
		}

		// Token: 0x06001E7A RID: 7802 RVA: 0x0003AB4C File Offset: 0x00038D4C
		public static bool operator !=(PropertyName lhs, PropertyName rhs)
		{
			return lhs.id != rhs.id;
		}

		// Token: 0x06001E7B RID: 7803 RVA: 0x0003AB70 File Offset: 0x00038D70
		public override int GetHashCode()
		{
			return this.id;
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x0003AB88 File Offset: 0x00038D88
		public override bool Equals(object other)
		{
			return other is PropertyName && this.Equals((PropertyName)other);
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x0003ABB4 File Offset: 0x00038DB4
		public bool Equals(PropertyName other)
		{
			return this == other;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0003ABD4 File Offset: 0x00038DD4
		public static implicit operator PropertyName(string name)
		{
			return new PropertyName(name);
		}

		// Token: 0x06001E7F RID: 7807 RVA: 0x0003ABEC File Offset: 0x00038DEC
		public static implicit operator PropertyName(int id)
		{
			return new PropertyName(id);
		}

		// Token: 0x06001E80 RID: 7808 RVA: 0x0003AC04 File Offset: 0x00038E04
		public override string ToString()
		{
			return string.Format("Unknown:{0}", this.id);
		}

		// Token: 0x04000913 RID: 2323
		internal int id;
	}
}
