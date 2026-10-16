using System;
using System.Reflection;

namespace NAudio.Utils
{
	// Token: 0x02000069 RID: 105
	public static class FieldDescriptionHelper
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00007678 File Offset: 0x00005878
		public static string Describe(Type t, Guid guid)
		{
			foreach (FieldInfo fieldInfo in t.GetFields(24))
			{
				if (fieldInfo.IsPublic && fieldInfo.IsStatic && fieldInfo.FieldType == typeof(Guid) && (Guid)fieldInfo.GetValue(null) == guid)
				{
					foreach (object obj in fieldInfo.GetCustomAttributes(false))
					{
						FieldDescriptionAttribute fieldDescriptionAttribute = obj as FieldDescriptionAttribute;
						if (fieldDescriptionAttribute != null)
						{
							return fieldDescriptionAttribute.Description;
						}
					}
					return fieldInfo.Name;
				}
			}
			return guid.ToString();
		}
	}
}
