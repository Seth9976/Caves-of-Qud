using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000544 RID: 1348
	public class UxmlAssetAttributeDescription<T> : TypedUxmlAttributeDescription<T>, IUxmlAssetAttributeDescription where T : Object
	{
		// Token: 0x06002945 RID: 10565 RVA: 0x0009599C File Offset: 0x00093B9C
		public UxmlAssetAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = default(T);
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x000959D9 File Offset: 0x00093BD9
		public override string defaultValueAsString
		{
			get
			{
				T t = base.defaultValue;
				return ((t != null) ? t.ToString() : null) ?? "null";
			}
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000959FC File Offset: 0x00093BFC
		public override T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			string text;
			VisualTreeAsset visualTreeAsset;
			bool flag = base.TryGetValueFromBagAsString(bag, cc, out text, out visualTreeAsset) && visualTreeAsset != null;
			T t;
			if (flag)
			{
				t = visualTreeAsset.GetAsset<T>(text);
			}
			else
			{
				t = default(T);
			}
			return t;
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x00095A40 File Offset: 0x00093C40
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, out T value)
		{
			string text;
			VisualTreeAsset visualTreeAsset;
			bool flag = base.TryGetValueFromBagAsString(bag, cc, out text, out visualTreeAsset) && visualTreeAsset != null;
			bool flag2;
			if (flag)
			{
				value = visualTreeAsset.GetAsset<T>(text);
				flag2 = true;
			}
			else
			{
				value = default(T);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002949 RID: 10569 RVA: 0x00095A88 File Offset: 0x00093C88
		Type IUxmlAssetAttributeDescription.assetType
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
