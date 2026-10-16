using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using AiUnity.Common.Attributes;

namespace AiUnity.Common.Extensions
{
	// Token: 0x020002DC RID: 732
	public static class EnumExtensions
	{
		// Token: 0x06001932 RID: 6450 RVA: 0x00080934 File Offset: 0x0007EB34
		public static T Add<T>(this Enum type, T value)
		{
			T t;
			try
			{
				t = (T)((object)((int)type | (int)((object)value)));
			}
			catch (Exception ex)
			{
				throw new ArgumentException(string.Format("Could not append value from enumerated type '{0}'.", typeof(T).Name), ex);
			}
			return t;
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00080994 File Offset: 0x0007EB94
		public static IEnumerable<TEnum> GetFlags<TEnum>(this TEnum input, bool checkZero = false, bool checkCombinators = false) where TEnum : struct, IComparable, IFormattable
		{
			if (!typeof(TEnum).IsEnum)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			long setBits = (long)Convert.ToInt32(input);
			if (!checkZero && setBits == 0L)
			{
				yield break;
			}
			foreach (object obj in Enum.GetValues(typeof(TEnum)))
			{
				TEnum tenum = (TEnum)((object)obj);
				long num = (long)Convert.ToInt32(tenum);
				if (num != 0L && (setBits & num) == num && (checkCombinators || (num & (num - 1L)) == 0L))
				{
					yield return tenum;
				}
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x000809B4 File Offset: 0x0007EBB4
		public static bool Has<T>(this Enum type, T value)
		{
			bool flag;
			try
			{
				flag = ((int)type & (int)((object)value)) == (int)((object)value);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001935 RID: 6453 RVA: 0x000809FC File Offset: 0x0007EBFC
		public static bool Is<T>(this Enum type, T value)
		{
			bool flag;
			try
			{
				flag = (int)type == (int)((object)value);
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001936 RID: 6454 RVA: 0x00080A38 File Offset: 0x0007EC38
		public static bool IsEnum<T>(this string s)
		{
			return s.IsEnum(typeof(T));
		}

		// Token: 0x06001937 RID: 6455 RVA: 0x00080A4C File Offset: 0x0007EC4C
		public static bool IsEnum(this string s, Type type)
		{
			bool flag = Enum.IsDefined(type, s);
			if (!flag && !string.IsNullOrEmpty(s) && type.IsDefined(typeof(FlagsAttribute), false))
			{
				string[] names = Enum.GetNames(type);
				flag = s.Replace(" ", string.Empty).Split(',', StringSplitOptions.None).All<string>((string e) => names.Any<string>((string n) => n.Equals(e)));
			}
			return flag;
		}

		// Token: 0x06001938 RID: 6456 RVA: 0x00080ABC File Offset: 0x0007ECBC
		public static T Remove<T>(this Enum type, T value)
		{
			T t;
			try
			{
				t = (T)((object)((int)type & ~(int)((object)value)));
			}
			catch (Exception ex)
			{
				throw new ArgumentException(string.Format("Could not remove value from enumerated type '{0}'.", typeof(T).Name), ex);
			}
			return t;
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00080B1C File Offset: 0x0007ED1C
		public static T ToEnum<T>(this int i) where T : struct, IComparable, IFormattable, IConvertible
		{
			return (T)((object)i);
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x00080B2C File Offset: 0x0007ED2C
		public static T ToEnum<T>(this string s, T defaultValue = default(T)) where T : struct, IComparable, IFormattable, IConvertible
		{
			try
			{
				if (s.IsEnum<T>())
				{
					return (T)((object)Enum.Parse(typeof(T), s));
				}
			}
			catch
			{
			}
			return defaultValue;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00080B74 File Offset: 0x0007ED74
		public static Enum ToEnum(this string s, Type type)
		{
			try
			{
				int num;
				if (int.TryParse(s, out num))
				{
					return num.ToEnum(type);
				}
				if (s.IsEnum(type))
				{
					return Enum.Parse(type, s) as Enum;
				}
			}
			catch
			{
				return Activator.CreateInstance(type) as Enum;
			}
			return Activator.CreateInstance(type) as Enum;
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00080BDC File Offset: 0x0007EDDC
		public static Enum ToEnum(this int i, Type type)
		{
			return Convert.ChangeType(i, type) as Enum;
		}

		// Token: 0x0600193D RID: 6461 RVA: 0x00080BF0 File Offset: 0x0007EDF0
		public static T? ToEnumSafe<T>(this string s) where T : struct
		{
			if (!s.IsEnum<T>())
			{
				return null;
			}
			return (T?)Enum.Parse(typeof(T), s);
		}

		// Token: 0x0600193E RID: 6462 RVA: 0x00080C24 File Offset: 0x0007EE24
		private static bool IsFlagDefined(Enum e)
		{
			decimal num;
			return !decimal.TryParse(e.ToString(), out num);
		}

		// Token: 0x0600193F RID: 6463 RVA: 0x00080C44 File Offset: 0x0007EE44
		public static string GetDescription(this Enum value)
		{
			Type type = value.GetType();
			string name = Enum.GetName(type, value);
			if (name != null)
			{
				FieldInfo field = type.GetField(name);
				if (field != null)
				{
					DescriptionAttribute descriptionAttribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
					if (descriptionAttribute != null)
					{
						return descriptionAttribute.Description;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x06001940 RID: 6464 RVA: 0x00080C9C File Offset: 0x0007EE9C
		public static string GetSymbol(this Enum value)
		{
			Type type = value.GetType();
			string name = Enum.GetName(type, value);
			if (name != null)
			{
				FieldInfo field = type.GetField(name);
				if (field != null)
				{
					EnumSymbolAttribute enumSymbolAttribute = Attribute.GetCustomAttribute(field, typeof(EnumSymbolAttribute)) as EnumSymbolAttribute;
					if (enumSymbolAttribute != null)
					{
						return enumSymbolAttribute.EnumSymbol;
					}
				}
			}
			return value.ToString();
		}

		// Token: 0x06001941 RID: 6465 RVA: 0x00080CF4 File Offset: 0x0007EEF4
		public static string GetAttributeStringOfType<T>(this Enum enumVal) where T : Attribute
		{
			object[] customAttributes = enumVal.GetType().GetMember(enumVal.ToString())[0].GetCustomAttributes(typeof(T), false);
			if (customAttributes.Length == 0)
			{
				return enumVal.ToString();
			}
			return ((T)((object)customAttributes[0])).ToString();
		}
	}
}
