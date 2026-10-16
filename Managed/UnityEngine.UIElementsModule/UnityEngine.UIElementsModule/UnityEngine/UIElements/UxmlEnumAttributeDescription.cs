using System;
using System.Collections.Generic;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200055C RID: 1372
	public class UxmlEnumAttributeDescription<T> : TypedUxmlAttributeDescription<T> where T : struct, IConvertible
	{
		// Token: 0x060029BC RID: 10684 RVA: 0x00096B60 File Offset: 0x00094D60
		public UxmlEnumAttributeDescription()
		{
			bool flag = !typeof(T).IsEnum;
			if (flag)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = new T();
			UxmlEnumeration uxmlEnumeration = new UxmlEnumeration();
			List<string> list = new List<string>();
			foreach (object obj in Enum.GetValues(typeof(T)))
			{
				T t = (T)((object)obj);
				list.Add(t.ToString(CultureInfo.InvariantCulture));
			}
			uxmlEnumeration.values = list;
			base.restriction = uxmlEnumeration;
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x060029BD RID: 10685 RVA: 0x00096C48 File Offset: 0x00094E48
		public override string defaultValueAsString
		{
			get
			{
				T defaultValue = base.defaultValue;
				return defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00096C78 File Offset: 0x00094E78
		public override T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<T>(bag, cc, (string s, T convertible) => UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible), base.defaultValue);
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00096CB8 File Offset: 0x00094EB8
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref T value)
		{
			return base.TryGetValueFromBag<T>(bag, cc, (string s, T convertible) => UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible), base.defaultValue, ref value);
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00096CF8 File Offset: 0x00094EF8
		private static U ConvertValueToEnum<U>(string v, U defaultValue) where U : struct
		{
			try
			{
				bool flag = string.IsNullOrEmpty(v);
				if (flag)
				{
					return defaultValue;
				}
				return (U)((object)Enum.Parse(typeof(U), v, true));
			}
			catch (ArgumentException)
			{
				Debug.LogError(UxmlEnumAttributeDescription<T>.GetEnumNameErrorMessage(v, typeof(U)));
			}
			catch (OverflowException)
			{
				Debug.LogError(UxmlEnumAttributeDescription<T>.GetEnumRangeErrorMessage(v, typeof(U)));
			}
			return defaultValue;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00096D88 File Offset: 0x00094F88
		private static string GetEnumNameErrorMessage(string v, Type enumType)
		{
			return string.Concat(new string[]
			{
				"The ",
				enumType.Name,
				" enum does not contain the value `",
				v,
				"`. Value must be in range [",
				string.Join(" | ", Enum.GetNames(enumType)),
				"]."
			});
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00096DE8 File Offset: 0x00094FE8
		private static string GetEnumRangeErrorMessage(string v, Type enumType)
		{
			return v + " is outside of the range of possible values for the " + enumType.Name + " enum.";
		}
	}
}
