using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000135 RID: 309
	public static class SarifVersionFacts
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x00023434 File Offset: 0x00021634
		[NullableContext(1)]
		public static bool TryParse(string version, out SarifVersion result)
		{
			if (version == null)
			{
				result = SarifVersion.Sarif1;
				return true;
			}
			string text = CaseInsensitiveComparison.ToLower(version);
			if (text == "default")
			{
				result = SarifVersion.Sarif1;
				return true;
			}
			if (text == "latest")
			{
				result = SarifVersion.Latest;
				return true;
			}
			if (text == "1" || text == "1.0")
			{
				result = SarifVersion.Sarif1;
				return true;
			}
			if (!(text == "2") && !(text == "2.1"))
			{
				result = SarifVersion.Sarif1;
				return false;
			}
			result = SarifVersion.Sarif2;
			return true;
		}
	}
}
