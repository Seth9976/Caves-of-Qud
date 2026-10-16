using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EB RID: 235
	internal static class EnumFieldHelpers
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x00022440 File Offset: 0x00020640
		internal static bool ExtractValue(IUxmlAttributes bag, CreationContext cc, out Type resEnumType, out Enum resEnumValue, out bool resIncludeObsoleteValues)
		{
			resIncludeObsoleteValues = false;
			resEnumValue = null;
			resEnumType = EnumFieldHelpers.type.GetValueFromBag(bag, cc);
			bool flag = resEnumType == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				string text = null;
				object obj = null;
				bool flag3 = EnumFieldHelpers.value.TryGetValueFromBag(bag, cc, ref text) && !Enum.TryParse(resEnumType, text, false, out obj);
				if (flag3)
				{
					Debug.LogErrorFormat("EnumField: Could not parse value of '{0}', because it isn't defined in the {1} enum.", new object[] { text, resEnumType.FullName });
					flag2 = false;
				}
				else
				{
					resEnumValue = ((text != null && obj != null) ? ((Enum)obj) : ((Enum)Enum.ToObject(resEnumType, 0)));
					resIncludeObsoleteValues = EnumFieldHelpers.includeObsoleteValues.GetValueFromBag(bag, cc);
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x04000458 RID: 1112
		internal static readonly UxmlTypeAttributeDescription<Enum> type = new UxmlTypeAttributeDescription<Enum>
		{
			name = "type"
		};

		// Token: 0x04000459 RID: 1113
		internal static readonly UxmlStringAttributeDescription value = new UxmlStringAttributeDescription
		{
			name = "value"
		};

		// Token: 0x0400045A RID: 1114
		internal static readonly UxmlBoolAttributeDescription includeObsoleteValues = new UxmlBoolAttributeDescription
		{
			name = "include-obsolete-values",
			defaultValue = false
		};
	}
}
