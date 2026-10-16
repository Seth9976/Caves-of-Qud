using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000559 RID: 1369
	public class UxmlColorAttributeDescription : TypedUxmlAttributeDescription<Color>
	{
		// Token: 0x060029AC RID: 10668 RVA: 0x000968B8 File Offset: 0x00094AB8
		public UxmlColorAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = new Color(0f, 0f, 0f, 1f);
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x00096908 File Offset: 0x00094B08
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString();
			}
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x00096930 File Offset: 0x00094B30
		public override Color GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Color>(bag, cc, (string s, Color color) => UxmlColorAttributeDescription.ConvertValueToColor(s, color), base.defaultValue);
		}

		// Token: 0x060029AF RID: 10671 RVA: 0x00096970 File Offset: 0x00094B70
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Color value)
		{
			return base.TryGetValueFromBag<Color>(bag, cc, (string s, Color color) => UxmlColorAttributeDescription.ConvertValueToColor(s, color), base.defaultValue, ref value);
		}

		// Token: 0x060029B0 RID: 10672 RVA: 0x000969B0 File Offset: 0x00094BB0
		private static Color ConvertValueToColor(string v, Color defaultValue)
		{
			Color color;
			bool flag = v == null || !ColorUtility.TryParseHtmlString(v, out color);
			Color color2;
			if (flag)
			{
				color2 = defaultValue;
			}
			else
			{
				color2 = color;
			}
			return color2;
		}
	}
}
