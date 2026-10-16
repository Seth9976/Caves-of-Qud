using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200056D RID: 1389
	public static class UxmlDescriptionCache
	{
		// Token: 0x060029EC RID: 10732 RVA: 0x000971E5 File Offset: 0x000953E5
		public static void RegisterType(Type type, UxmlAttributeNames[] attributeNames)
		{
			UxmlDescriptionCache.s_NamesPerType[type] = attributeNames;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x000971F8 File Offset: 0x000953F8
		internal static bool TryGetCachedDescription(Type type, out UxmlAttributeNames[] attributes)
		{
			return UxmlDescriptionCache.s_NamesPerType.TryGetValue(type, out attributes);
		}

		// Token: 0x040012CF RID: 4815
		private static readonly Dictionary<Type, UxmlAttributeNames[]> s_NamesPerType = new Dictionary<Type, UxmlAttributeNames[]>();
	}
}
