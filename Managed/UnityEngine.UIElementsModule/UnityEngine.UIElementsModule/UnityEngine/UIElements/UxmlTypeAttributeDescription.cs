using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200055B RID: 1371
	public class UxmlTypeAttributeDescription<TBase> : TypedUxmlAttributeDescription<Type>
	{
		// Token: 0x060029B5 RID: 10677 RVA: 0x000969F1 File Offset: 0x00094BF1
		public UxmlTypeAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = null;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x00096A1C File Offset: 0x00094C1C
		public override string defaultValueAsString
		{
			get
			{
				return (base.defaultValue == null) ? "null" : base.defaultValue.FullName;
			}
		}

		// Token: 0x060029B7 RID: 10679 RVA: 0x00096A50 File Offset: 0x00094C50
		public override Type GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue);
		}

		// Token: 0x060029B8 RID: 10680 RVA: 0x00096A7C File Offset: 0x00094C7C
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref Type value)
		{
			return base.TryGetValueFromBag<Type>(bag, cc, (string s, Type type1) => this.ConvertValueToType(s, type1), base.defaultValue, ref value);
		}

		// Token: 0x060029B9 RID: 10681 RVA: 0x00096AAC File Offset: 0x00094CAC
		private Type ConvertValueToType(string v, Type defaultValue)
		{
			bool flag = string.IsNullOrEmpty(v);
			Type type;
			if (flag)
			{
				type = defaultValue;
			}
			else
			{
				try
				{
					Type type2 = Type.GetType(v, true);
					bool flag2 = !typeof(TBase).IsAssignableFrom(type2);
					if (!flag2)
					{
						return type2;
					}
					Debug.LogError(string.Concat(new string[]
					{
						"Type: Invalid type \"",
						v,
						"\". Type must derive from ",
						typeof(TBase).FullName,
						"."
					}));
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
				type = defaultValue;
			}
			return type;
		}
	}
}
