using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.Properties.Internal;

namespace Unity.Properties
{
	// Token: 0x0200002E RID: 46
	internal readonly struct FieldMember : IMemberInfo
	{
		// Token: 0x060000DA RID: 218 RVA: 0x00004CEC File Offset: 0x00002EEC
		public FieldMember(FieldInfo fieldInfo)
		{
			this.m_FieldInfo = fieldInfo;
			this.Name = ReflectionUtilities.SanitizeMemberName(this.m_FieldInfo);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000DB RID: 219 RVA: 0x00004D07 File Offset: 0x00002F07
		public string Name { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004D0F File Offset: 0x00002F0F
		public bool IsReadOnly
		{
			get
			{
				return this.m_FieldInfo.IsInitOnly;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00004D1C File Offset: 0x00002F1C
		public Type ValueType
		{
			get
			{
				return this.m_FieldInfo.FieldType;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004D29 File Offset: 0x00002F29
		public object GetValue(object obj)
		{
			return this.m_FieldInfo.GetValue(obj);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004D37 File Offset: 0x00002F37
		public void SetValue(object obj, object value)
		{
			this.m_FieldInfo.SetValue(obj, value);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004D47 File Offset: 0x00002F47
		public IEnumerable<Attribute> GetCustomAttributes()
		{
			return this.m_FieldInfo.GetCustomAttributes();
		}

		// Token: 0x0400004D RID: 77
		internal readonly FieldInfo m_FieldInfo;
	}
}
