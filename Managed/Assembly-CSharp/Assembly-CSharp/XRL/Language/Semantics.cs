using System;
using XRL.World;

namespace XRL.Language
{
	// Token: 0x020005E6 RID: 1510
	public static class Semantics
	{
		// Token: 0x0600410F RID: 16655 RVA: 0x001460CB File Offset: 0x001442CB
		public static string GetSingularSemantic(string name, GameObject go, string defaultResult)
		{
			return go.GetTagOrStringProperty("Semantic" + name, defaultResult);
		}

		// Token: 0x06004110 RID: 16656 RVA: 0x001460DF File Offset: 0x001442DF
		public static string GetPluralSemantic(string name, GameObject go, string defaultResult)
		{
			return Grammar.Pluralize(Semantics.GetSingularSemantic(name, go, defaultResult));
		}
	}
}
