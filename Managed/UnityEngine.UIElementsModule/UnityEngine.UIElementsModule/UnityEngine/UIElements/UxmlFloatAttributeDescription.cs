using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200054B RID: 1355
	public class UxmlFloatAttributeDescription : TypedUxmlAttributeDescription<float>
	{
		// Token: 0x0600296D RID: 10605 RVA: 0x000960EC File Offset: 0x000942EC
		public UxmlFloatAttributeDescription()
		{
			base.type = "float";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0f;
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x0009611C File Offset: 0x0009431C
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00096148 File Offset: 0x00094348
		public override float GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue);
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x00096188 File Offset: 0x00094388
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref float value)
		{
			return base.TryGetValueFromBag<float>(bag, cc, (string s, float f) => UxmlFloatAttributeDescription.ConvertValueToFloat(s, f), base.defaultValue, ref value);
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000961C8 File Offset: 0x000943C8
		private static float ConvertValueToFloat(string v, float defaultValue)
		{
			float num;
			bool flag = v == null || !float.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out num);
			float num2;
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
