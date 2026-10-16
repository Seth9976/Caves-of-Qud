using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000259 RID: 601
	internal class AttributeHelperEngine
	{
		// Token: 0x06001F4C RID: 8012 RVA: 0x0003C0D0 File Offset: 0x0003A2D0
		[RequiredByNativeCode]
		private static Type GetParentTypeDisallowingMultipleInclusion(Type type)
		{
			Type type2 = null;
			while (type != null && type != typeof(MonoBehaviour))
			{
				bool flag = Attribute.IsDefined(type, typeof(DisallowMultipleComponent));
				if (flag)
				{
					type2 = type;
				}
				type = type.BaseType;
			}
			return type2;
		}

		// Token: 0x06001F4D RID: 8013 RVA: 0x0003C128 File Offset: 0x0003A328
		[RequiredByNativeCode]
		private static Type[] GetRequiredComponents(Type klass)
		{
			List<Type> list = null;
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				RequireComponent[] array = (RequireComponent[])klass.GetCustomAttributes(typeof(RequireComponent), false);
				Type baseType = klass.BaseType;
				foreach (RequireComponent requireComponent in array)
				{
					bool flag = list == null && array.Length == 1 && baseType == typeof(MonoBehaviour);
					if (flag)
					{
						return new Type[] { requireComponent.m_Type0, requireComponent.m_Type1, requireComponent.m_Type2 };
					}
					bool flag2 = list == null;
					if (flag2)
					{
						list = new List<Type>();
					}
					bool flag3 = requireComponent.m_Type0 != null;
					if (flag3)
					{
						list.Add(requireComponent.m_Type0);
					}
					bool flag4 = requireComponent.m_Type1 != null;
					if (flag4)
					{
						list.Add(requireComponent.m_Type1);
					}
					bool flag5 = requireComponent.m_Type2 != null;
					if (flag5)
					{
						list.Add(requireComponent.m_Type2);
					}
				}
				klass = baseType;
			}
			bool flag6 = list == null;
			if (flag6)
			{
				return null;
			}
			return list.ToArray();
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0003C28C File Offset: 0x0003A48C
		private static int GetExecuteMode(Type klass)
		{
			object[] customAttributes = klass.GetCustomAttributes(typeof(ExecuteAlways), false);
			bool flag = customAttributes.Length != 0;
			int num;
			if (flag)
			{
				num = 2;
			}
			else
			{
				object[] customAttributes2 = klass.GetCustomAttributes(typeof(ExecuteInEditMode), false);
				bool flag2 = customAttributes2.Length != 0;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			return num;
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0003C2E0 File Offset: 0x0003A4E0
		[RequiredByNativeCode]
		private static int CheckIsEditorScript(Type klass)
		{
			while (klass != null && klass != typeof(MonoBehaviour))
			{
				int executeMode = AttributeHelperEngine.GetExecuteMode(klass);
				bool flag = executeMode > 0;
				if (flag)
				{
					return executeMode;
				}
				klass = klass.BaseType;
			}
			return 0;
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0003C334 File Offset: 0x0003A534
		[RequiredByNativeCode]
		private static int GetDefaultExecutionOrderFor(Type klass)
		{
			DefaultExecutionOrder customAttributeOfType = AttributeHelperEngine.GetCustomAttributeOfType<DefaultExecutionOrder>(klass);
			bool flag = customAttributeOfType == null;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				num = customAttributeOfType.order;
			}
			return num;
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0003C360 File Offset: 0x0003A560
		private static T GetCustomAttributeOfType<T>(Type klass) where T : Attribute
		{
			Type typeFromHandle = typeof(T);
			object[] customAttributes = klass.GetCustomAttributes(typeFromHandle, true);
			bool flag = customAttributes != null && customAttributes.Length != 0;
			T t;
			if (flag)
			{
				t = (T)((object)customAttributes[0]);
			}
			else
			{
				t = default(T);
			}
			return t;
		}
	}
}
