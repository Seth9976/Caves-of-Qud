using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200055E RID: 1374
	public class UxmlHash128AttributeDescription : TypedUxmlAttributeDescription<Hash128>
	{
		// Token: 0x060029C7 RID: 10695 RVA: 0x00096E28 File Offset: 0x00095028
		public UxmlHash128AttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = default(Hash128);
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x060029C8 RID: 10696 RVA: 0x00096E68 File Offset: 0x00095068
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString();
			}
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x00096E90 File Offset: 0x00095090
		public override Hash128 GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Hash128>(bag, cc, (string s, Hash128 i) => Hash128.Parse(s), base.defaultValue);
		}

		// Token: 0x060029CA RID: 10698 RVA: 0x00096ED0 File Offset: 0x000950D0
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Hash128 value)
		{
			return base.TryGetValueFromBag<Hash128>(bag, cc, (string s, Hash128 i) => Hash128.Parse(s), base.defaultValue, ref value);
		}
	}
}
