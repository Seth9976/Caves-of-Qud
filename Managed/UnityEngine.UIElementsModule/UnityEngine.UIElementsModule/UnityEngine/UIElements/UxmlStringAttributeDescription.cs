using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000549 RID: 1353
	public class UxmlStringAttributeDescription : TypedUxmlAttributeDescription<string>
	{
		// Token: 0x06002965 RID: 10597 RVA: 0x00096017 File Offset: 0x00094217
		public UxmlStringAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = "";
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x00096048 File Offset: 0x00094248
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue;
			}
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x00096060 File Offset: 0x00094260
		public override string GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue);
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x000960A0 File Offset: 0x000942A0
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref string value)
		{
			return base.TryGetValueFromBag<string>(bag, cc, (string s, string t) => s, base.defaultValue, ref value);
		}
	}
}
