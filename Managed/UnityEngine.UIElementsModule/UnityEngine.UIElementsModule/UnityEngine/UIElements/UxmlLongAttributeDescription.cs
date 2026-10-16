using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x02000555 RID: 1365
	public class UxmlLongAttributeDescription : TypedUxmlAttributeDescription<long>
	{
		// Token: 0x0600299A RID: 10650 RVA: 0x00096689 File Offset: 0x00094889
		public UxmlLongAttributeDescription()
		{
			base.type = "long";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = 0L;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x000966B4 File Offset: 0x000948B4
		public override string defaultValueAsString
		{
			get
			{
				return base.defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x0600299C RID: 10652 RVA: 0x000966E0 File Offset: 0x000948E0
		public override long GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue);
		}

		// Token: 0x0600299D RID: 10653 RVA: 0x00096720 File Offset: 0x00094920
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref long value)
		{
			return base.TryGetValueFromBag<long>(bag, cc, (string s, long l) => UxmlLongAttributeDescription.ConvertValueToLong(s, l), base.defaultValue, ref value);
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00096760 File Offset: 0x00094960
		private static long ConvertValueToLong(string v, long defaultValue)
		{
			long num;
			bool flag = v == null || !long.TryParse(v, out num);
			long num2;
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
