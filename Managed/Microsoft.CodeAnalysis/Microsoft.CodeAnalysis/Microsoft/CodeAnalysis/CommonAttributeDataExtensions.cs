using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000236 RID: 566
	internal static class CommonAttributeDataExtensions
	{
		// Token: 0x06001600 RID: 5632 RVA: 0x000454E0 File Offset: 0x000436E0
		[NullableContext(1)]
		public static bool TryGetGuidAttributeValue(this AttributeData attrData, [Nullable(2)] out string guidString)
		{
			if (attrData.CommonConstructorArguments.Length == 1)
			{
				object valueInternal = attrData.CommonConstructorArguments[0].ValueInternal;
				if (valueInternal == null || valueInternal is string)
				{
					guidString = (string)valueInternal;
					return true;
				}
			}
			guidString = null;
			return false;
		}
	}
}
