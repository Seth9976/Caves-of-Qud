using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200054D RID: 1357
	public class UxmlDoubleAttributeDescription : TypedUxmlAttributeDescription<double>
	{
		// Token: 0x06002976 RID: 10614 RVA: 0x00096213 File Offset: 0x00094413
		public UxmlDoubleAttributeDescription()
		{
			base.type = "double";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0.0;
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x00096248 File Offset: 0x00094448
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x00096274 File Offset: 0x00094474
		public override double GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue);
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000962B4 File Offset: 0x000944B4
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref double value)
		{
			return base.TryGetValueFromBag<double>(bag, cc, (string s, double d) => UxmlDoubleAttributeDescription.ConvertValueToDouble(s, d), base.defaultValue, ref value);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000962F4 File Offset: 0x000944F4
		private static double ConvertValueToDouble(string v, double defaultValue)
		{
			double num;
			bool flag = v == null || !double.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out num);
			double num2;
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
