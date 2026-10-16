using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000557 RID: 1367
	public class UxmlBoolAttributeDescription : TypedUxmlAttributeDescription<bool>
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x000967A1 File Offset: 0x000949A1
		public UxmlBoolAttributeDescription()
		{
			base.type = "boolean";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = false;
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x000967CC File Offset: 0x000949CC
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString().ToLowerInvariant();
			}
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000967F4 File Offset: 0x000949F4
		public override bool GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue);
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x00096834 File Offset: 0x00094A34
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref bool value)
		{
			return base.TryGetValueFromBag<bool>(bag, cc, (string s, bool b) => UxmlBoolAttributeDescription.ConvertValueToBool(s, b), base.defaultValue, ref value);
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00096874 File Offset: 0x00094A74
		private static bool ConvertValueToBool(string v, bool defaultValue)
		{
			bool flag2;
			bool flag = v == null || !bool.TryParse(v, out flag2);
			bool flag3;
			if (flag)
			{
				flag3 = defaultValue;
			}
			else
			{
				flag3 = flag2;
			}
			return flag3;
		}
	}
}
