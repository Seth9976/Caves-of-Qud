using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000551 RID: 1361
	public class UxmlUnsignedIntAttributeDescription : TypedUxmlAttributeDescription<uint>
	{
		// Token: 0x06002988 RID: 10632 RVA: 0x00096459 File Offset: 0x00094659
		public UxmlUnsignedIntAttributeDescription()
		{
			base.type = "unsignedInt";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0U;
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002989 RID: 10633 RVA: 0x00096484 File Offset: 0x00094684
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000964B0 File Offset: 0x000946B0
		public override uint GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<uint>(bag, cc, (string s, uint i) => UxmlUnsignedIntAttributeDescription.ConvertValueToUInt(s, i), base.defaultValue);
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000964F0 File Offset: 0x000946F0
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref uint value)
		{
			return base.TryGetValueFromBag<uint>(bag, cc, (string s, uint i) => UxmlUnsignedIntAttributeDescription.ConvertValueToUInt(s, i), base.defaultValue, ref value);
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x00096530 File Offset: 0x00094730
		private static uint ConvertValueToUInt(string v, uint defaultValue)
		{
			uint num;
			bool flag = v == null || !uint.TryParse(v, out num);
			uint num2;
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
