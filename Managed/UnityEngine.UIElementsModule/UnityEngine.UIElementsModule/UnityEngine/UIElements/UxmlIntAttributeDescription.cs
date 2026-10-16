using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200054F RID: 1359
	public class UxmlIntAttributeDescription : TypedUxmlAttributeDescription<int>
	{
		// Token: 0x0600297F RID: 10623 RVA: 0x0009633F File Offset: 0x0009453F
		public UxmlIntAttributeDescription()
		{
			base.type = "int";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0;
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x0009636C File Offset: 0x0009456C
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x00096398 File Offset: 0x00094598
		public override int GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<int>(bag, cc, (string s, int i) => UxmlIntAttributeDescription.ConvertValueToInt(s, i), base.defaultValue);
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x000963D8 File Offset: 0x000945D8
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref int value)
		{
			return base.TryGetValueFromBag<int>(bag, cc, (string s, int i) => UxmlIntAttributeDescription.ConvertValueToInt(s, i), base.defaultValue, ref value);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x00096418 File Offset: 0x00094618
		private static int ConvertValueToInt(string v, int defaultValue)
		{
			int num;
			bool flag = v == null || !int.TryParse(v, out num);
			int num2;
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
