using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000553 RID: 1363
	public class UxmlUnsignedLongAttributeDescription : TypedUxmlAttributeDescription<ulong>
	{
		// Token: 0x06002991 RID: 10641 RVA: 0x00096571 File Offset: 0x00094771
		public UxmlUnsignedLongAttributeDescription()
		{
			base.type = "unsignedLong";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0UL;
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x0009659C File Offset: 0x0009479C
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000965C8 File Offset: 0x000947C8
		public override ulong GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<ulong>(bag, cc, (string s, ulong l) => UxmlUnsignedLongAttributeDescription.ConvertValueToUlong(s, l), base.defaultValue);
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x00096608 File Offset: 0x00094808
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref ulong value)
		{
			return base.TryGetValueFromBag<ulong>(bag, cc, (string s, ulong l) => UxmlUnsignedLongAttributeDescription.ConvertValueToUlong(s, l), base.defaultValue, ref value);
		}

		// Token: 0x06002995 RID: 10645 RVA: 0x00096648 File Offset: 0x00094848
		private static ulong ConvertValueToUlong(string v, ulong defaultValue)
		{
			ulong num;
			bool flag = v == null || !ulong.TryParse(v, out num);
			ulong num2;
			if (flag)
			{
				num2 = defaultValue;
			}
			else
			{
				num2 = num;
			}
			return num2;
		}
	}
}
