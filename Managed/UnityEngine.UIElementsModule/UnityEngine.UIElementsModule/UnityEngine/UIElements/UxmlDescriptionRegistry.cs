using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000570 RID: 1392
	internal static class UxmlDescriptionRegistry
	{
		// Token: 0x060029F6 RID: 10742 RVA: 0x00097B00 File Offset: 0x00095D00
		public static UxmlTypeDescription GetDescription(Type type)
		{
			UxmlTypeDescription uxmlTypeDescription;
			bool flag = !UxmlDescriptionRegistry.s_UxmlDescriptions.TryGetValue(type, out uxmlTypeDescription);
			if (flag)
			{
				Dictionary<Type, UxmlTypeDescription> dictionary = UxmlDescriptionRegistry.s_UxmlDescriptions;
				uxmlTypeDescription = new UxmlTypeDescription(type);
				dictionary.Add(type, uxmlTypeDescription);
			}
			return uxmlTypeDescription;
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x00097B3C File Offset: 0x00095D3C
		public static void Clear()
		{
			UxmlDescriptionRegistry.s_UxmlDescriptions.Clear();
		}

		// Token: 0x040012DC RID: 4828
		private static readonly Dictionary<Type, UxmlTypeDescription> s_UxmlDescriptions = new Dictionary<Type, UxmlTypeDescription>();
	}
}
