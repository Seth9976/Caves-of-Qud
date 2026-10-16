using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000027 RID: 39
	public abstract class InputBindingComposite
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002DC RID: 732
		public abstract Type valueType { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002DD RID: 733
		public abstract int valueSizeInBytes { get; }

		// Token: 0x060002DE RID: 734
		public unsafe abstract void ReadValue(ref InputBindingCompositeContext context, void* buffer, int bufferSize);

		// Token: 0x060002DF RID: 735
		public abstract object ReadValueAsObject(ref InputBindingCompositeContext context);

		// Token: 0x060002E0 RID: 736 RVA: 0x0000CEBE File Offset: 0x0000B0BE
		public virtual float EvaluateMagnitude(ref InputBindingCompositeContext context)
		{
			return -1f;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000CEC5 File Offset: 0x0000B0C5
		protected virtual void FinishSetup(ref InputBindingCompositeContext context)
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000CEC7 File Offset: 0x0000B0C7
		internal void CallFinishSetup(ref InputBindingCompositeContext context)
		{
			this.FinishSetup(ref context);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		internal static Type GetValueType(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			return TypeHelpers.GetGenericTypeArgumentFromHierarchy(type, typeof(InputBindingComposite<>), 0);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000CF18 File Offset: 0x0000B118
		public static string GetExpectedControlLayoutName(string composite, string part)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			if (string.IsNullOrEmpty(part))
			{
				throw new ArgumentNullException("part");
			}
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			FieldInfo field = type.GetField(part, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (field == null)
			{
				return null;
			}
			InputControlAttribute customAttribute = field.GetCustomAttribute<InputControlAttribute>(false);
			if (customAttribute == null)
			{
				return null;
			}
			return customAttribute.layout;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000CF89 File Offset: 0x0000B189
		internal static IEnumerable<string> GetPartNames(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				yield break;
			}
			foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.Public))
			{
				if (fieldInfo.GetCustomAttribute<InputControlAttribute>() != null)
				{
					yield return fieldInfo.Name;
				}
			}
			FieldInfo[] array = null;
			yield break;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000CF9C File Offset: 0x0000B19C
		internal static string GetDisplayFormatString(string composite)
		{
			if (string.IsNullOrEmpty(composite))
			{
				throw new ArgumentNullException("composite");
			}
			Type type = InputBindingComposite.s_Composites.LookupTypeRegistration(composite);
			if (type == null)
			{
				return null;
			}
			DisplayStringFormatAttribute customAttribute = type.GetCustomAttribute<DisplayStringFormatAttribute>();
			if (customAttribute == null)
			{
				return null;
			}
			return customAttribute.formatString;
		}

		// Token: 0x040000F0 RID: 240
		internal static TypeTable s_Composites;
	}
}
