using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x0200058B RID: 1419
	public static class UxmlSerializedDataUtility
	{
		// Token: 0x06002A7F RID: 10879 RVA: 0x00098D08 File Offset: 0x00096F08
		public static object CopySerialized(object value)
		{
			bool flag = value == null;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				object obj2 = null;
				try
				{
					UxmlSerializableAdapterBase uxmlSerializableAdapterBase;
					bool flag2 = !UxmlSerializedDataUtility.s_Adapters.TryGetValue(value.GetType(), out uxmlSerializableAdapterBase);
					if (flag2)
					{
						Type type = typeof(UxmlSerializableAdapter<>).MakeGenericType(new Type[] { value.GetType() });
						FieldInfo field = type.GetField("SharedInstance", BindingFlags.Static | BindingFlags.Public);
						uxmlSerializableAdapterBase = (UxmlSerializableAdapterBase)field.GetValue(null);
						UxmlSerializedDataUtility.s_Adapters[value.GetType()] = uxmlSerializableAdapterBase;
					}
					obj2 = uxmlSerializableAdapterBase.CloneInstanceBoxed(value);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				obj = obj2;
			}
			return obj;
		}

		// Token: 0x06002A80 RID: 10880 RVA: 0x00098DC4 File Offset: 0x00096FC4
		public static T CopySerialized<T>(object value)
		{
			UxmlSerializableAdapter<T> sharedInstance = UxmlSerializableAdapter<T>.SharedInstance;
			return sharedInstance.CloneInstance((T)((object)value));
		}

		// Token: 0x04001310 RID: 4880
		internal static Dictionary<Type, UxmlSerializableAdapterBase> s_Adapters = new Dictionary<Type, UxmlSerializableAdapterBase>();
	}
}
