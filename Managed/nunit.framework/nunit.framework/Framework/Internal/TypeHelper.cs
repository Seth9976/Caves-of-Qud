using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200008F RID: 143
	public class TypeHelper
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public static string GetDisplayName(Type type)
		{
			if (type.IsGenericParameter)
			{
				return type.Name;
			}
			if (type.GetTypeInfo().IsGenericType)
			{
				string text = type.FullName;
				int num = text.IndexOf('[');
				if (num >= 0)
				{
					text = text.Substring(0, num);
				}
				num = text.LastIndexOf('.');
				if (num >= 0)
				{
					text = text.Substring(num + 1);
				}
				Type[] genericArguments = type.GetGenericArguments();
				int num2 = 0;
				StringBuilder stringBuilder = new StringBuilder();
				bool flag = false;
				foreach (string text2 in text.Split(new char[] { '+' }))
				{
					if (flag)
					{
						stringBuilder.Append("+");
					}
					flag = true;
					num = text2.IndexOf('`');
					if (num >= 0)
					{
						string text3 = text2.Substring(0, num);
						stringBuilder.Append(text3);
						stringBuilder.Append("<");
						int num3 = int.Parse(text2.Substring(num + 1));
						for (int j = 0; j < num3; j++)
						{
							if (j > 0)
							{
								stringBuilder.Append(",");
							}
							stringBuilder.Append(TypeHelper.GetDisplayName(genericArguments[num2++]));
						}
						stringBuilder.Append(">");
					}
					else
					{
						stringBuilder.Append(text2);
					}
				}
				return stringBuilder.ToString();
			}
			int num4 = type.FullName.LastIndexOf('.');
			if (num4 < 0)
			{
				return type.FullName;
			}
			return type.FullName.Substring(num4 + 1);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0000C958 File Offset: 0x0000AB58
		public static string GetDisplayName(Type type, object[] arglist)
		{
			string displayName = TypeHelper.GetDisplayName(type);
			if (arglist == null || arglist.Length == 0)
			{
				return displayName;
			}
			StringBuilder stringBuilder = new StringBuilder(displayName);
			stringBuilder.Append("(");
			for (int i = 0; i < arglist.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(",");
				}
				object obj = arglist[i];
				string text = ((obj == null) ? "null" : obj.ToString());
				if (obj is double || obj is float)
				{
					if (text.IndexOf('.') == -1)
					{
						text += ".0";
					}
					text += ((obj is double) ? "d" : "f");
				}
				else if (obj is decimal)
				{
					text += "m";
				}
				else if (obj is long)
				{
					text += "L";
				}
				else if (obj is ulong)
				{
					text += "UL";
				}
				else if (obj is string)
				{
					if (text.Length > 40)
					{
						text = text.Substring(0, 37) + "...";
					}
					text = "\"" + text + "\"";
				}
				stringBuilder.Append(text);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0000CAB0 File Offset: 0x0000ACB0
		public static Type BestCommonType(Type type1, Type type2)
		{
			if (type1 == TypeHelper.NonmatchingType)
			{
				return TypeHelper.NonmatchingType;
			}
			if (type2 == TypeHelper.NonmatchingType)
			{
				return TypeHelper.NonmatchingType;
			}
			if (type1 == type2)
			{
				return type1;
			}
			if (type1 == null)
			{
				return type2;
			}
			if (type2 == null)
			{
				return type1;
			}
			if (TypeHelper.IsNumeric(type1) && TypeHelper.IsNumeric(type2))
			{
				if (type1 == typeof(double))
				{
					return type1;
				}
				if (type2 == typeof(double))
				{
					return type2;
				}
				if (type1 == typeof(float))
				{
					return type1;
				}
				if (type2 == typeof(float))
				{
					return type2;
				}
				if (type1 == typeof(decimal))
				{
					return type1;
				}
				if (type2 == typeof(decimal))
				{
					return type2;
				}
				if (type1 == typeof(ulong))
				{
					return type1;
				}
				if (type2 == typeof(ulong))
				{
					return type2;
				}
				if (type1 == typeof(long))
				{
					return type1;
				}
				if (type2 == typeof(long))
				{
					return type2;
				}
				if (type1 == typeof(uint))
				{
					return type1;
				}
				if (type2 == typeof(uint))
				{
					return type2;
				}
				if (type1 == typeof(int))
				{
					return type1;
				}
				if (type2 == typeof(int))
				{
					return type2;
				}
				if (type1 == typeof(ushort))
				{
					return type1;
				}
				if (type2 == typeof(ushort))
				{
					return type2;
				}
				if (type1 == typeof(short))
				{
					return type1;
				}
				if (type2 == typeof(short))
				{
					return type2;
				}
				if (type1 == typeof(byte))
				{
					return type1;
				}
				if (type2 == typeof(byte))
				{
					return type2;
				}
				if (type1 == typeof(sbyte))
				{
					return type1;
				}
				if (type2 == typeof(sbyte))
				{
					return type2;
				}
			}
			if (type1.IsAssignableFrom(type2))
			{
				return type1;
			}
			if (type2.IsAssignableFrom(type1))
			{
				return type2;
			}
			return TypeHelper.NonmatchingType;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0000CCF0 File Offset: 0x0000AEF0
		public static bool IsNumeric(Type type)
		{
			return type == typeof(double) || type == typeof(float) || type == typeof(decimal) || type == typeof(long) || type == typeof(int) || type == typeof(short) || type == typeof(ulong) || type == typeof(uint) || type == typeof(ushort) || type == typeof(byte) || type == typeof(sbyte);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		public static void ConvertArgumentList(object[] arglist, IParameterInfo[] parameters)
		{
			for (int i = 0; i < arglist.Length; i++)
			{
				object obj = arglist[i];
				if (obj != null && obj is IConvertible)
				{
					Type type = obj.GetType();
					Type parameterType = parameters[i].ParameterType;
					bool flag = false;
					if (type != parameterType && !type.IsAssignableFrom(parameterType) && TypeHelper.IsNumeric(type) && TypeHelper.IsNumeric(parameterType))
					{
						if (parameterType == typeof(double) || parameterType == typeof(float))
						{
							flag = obj is int || obj is long || obj is short || obj is byte || obj is sbyte;
						}
						else if (parameterType == typeof(long))
						{
							flag = obj is int || obj is short || obj is byte || obj is sbyte;
						}
						else if (parameterType == typeof(short))
						{
							flag = obj is byte || obj is sbyte;
						}
					}
					if (flag)
					{
						arglist[i] = Convert.ChangeType(obj, parameterType, CultureInfo.InvariantCulture);
					}
				}
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public static bool CanDeduceTypeArgsFromArgs(Type type, object[] arglist, ref Type[] typeArgsOut)
		{
			Type[] genericArguments = type.GetGenericArguments();
			ConstructorInfo[] constructors = type.GetConstructors();
			for (int i = 0; i < constructors.Length; i++)
			{
				ParameterInfo[] parameters = constructors[i].GetParameters();
				if (parameters.Length == arglist.Length)
				{
					Type[] array = new Type[genericArguments.Length];
					for (int j = 0; j < array.Length; j++)
					{
						for (int k = 0; k < arglist.Length; k++)
						{
							if (genericArguments[j].IsGenericParameter || parameters[k].ParameterType.Equals(genericArguments[j]))
							{
								array[j] = TypeHelper.BestCommonType(array[j], arglist[k].GetType());
							}
						}
						if (array[j] == null)
						{
							array = null;
							break;
						}
					}
					if (array != null)
					{
						typeArgsOut = array;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0000CFD2 File Offset: 0x0000B1D2
		public static Array GetEnumValues(Type enumType)
		{
			return Enum.GetValues(enumType);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000CFDA File Offset: 0x0000B1DA
		public static string[] GetEnumNames(Type enumType)
		{
			return Enum.GetNames(enumType);
		}

		// Token: 0x04000118 RID: 280
		private const int STRING_MAX = 40;

		// Token: 0x04000119 RID: 281
		private const int STRING_LIMIT = 37;

		// Token: 0x0400011A RID: 282
		private const string THREE_DOTS = "...";

		// Token: 0x0400011B RID: 283
		public static readonly Type NonmatchingType = typeof(TypeHelper.NonmatchingTypeClass);

		// Token: 0x02000187 RID: 391
		internal sealed class NonmatchingTypeClass
		{
		}
	}
}
