using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x0200002F RID: 47
	internal readonly struct PropertyMember : IMemberInfo
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004D54 File Offset: 0x00002F54
		public string Name { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004D5C File Offset: 0x00002F5C
		public bool IsReadOnly
		{
			get
			{
				return !this.m_PropertyInfo.CanWrite;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004D6C File Offset: 0x00002F6C
		public Type ValueType
		{
			get
			{
				return this.m_PropertyInfo.PropertyType;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004D79 File Offset: 0x00002F79
		public PropertyMember(PropertyInfo propertyInfo)
		{
			this.m_PropertyInfo = propertyInfo;
			this.Name = ReflectionUtilities.SanitizeMemberName(this.m_PropertyInfo);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004D94 File Offset: 0x00002F94
		public object GetValue(object obj)
		{
			return this.m_PropertyInfo.GetValue(obj);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004DA2 File Offset: 0x00002FA2
		public void SetValue(object obj, object value)
		{
			this.m_PropertyInfo.SetValue(obj, value);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004DB2 File Offset: 0x00002FB2
		public IEnumerable<Attribute> GetCustomAttributes()
		{
			return this.m_PropertyInfo.GetCustomAttributes();
		}

		// Token: 0x0400004F RID: 79
		internal readonly PropertyInfo m_PropertyInfo;
	}
}
