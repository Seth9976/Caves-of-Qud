using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000218 RID: 536
	internal sealed class CommonAttributeDataComparer : IEqualityComparer<AttributeData>
	{
		// Token: 0x0600151F RID: 5407 RVA: 0x00043BDE File Offset: 0x00041DDE
		private CommonAttributeDataComparer()
		{
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00043BE8 File Offset: 0x00041DE8
		public bool Equals(AttributeData attr1, AttributeData attr2)
		{
			return attr1.AttributeClass == attr2.AttributeClass && attr1.AttributeConstructor == attr2.AttributeConstructor && attr1.HasErrors == attr2.HasErrors && attr1.IsConditionallyOmitted == attr2.IsConditionallyOmitted && attr1.CommonConstructorArguments.SequenceEqual<TypedConstant, TypedConstant>(attr2.CommonConstructorArguments, null) && attr1.NamedArguments.SequenceEqual<KeyValuePair<string, TypedConstant>, KeyValuePair<string, TypedConstant>>(attr2.NamedArguments, null);
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00043C58 File Offset: 0x00041E58
		public int GetHashCode(AttributeData attr)
		{
			INamedTypeSymbol attributeClass = attr.AttributeClass;
			int num = ((attributeClass != null) ? attributeClass.GetHashCode() : 0);
			num = ((attr.AttributeConstructor != null) ? Hash.Combine(attr.AttributeConstructor.GetHashCode(), num) : num);
			num = Hash.Combine(attr.HasErrors, num);
			num = Hash.Combine(attr.IsConditionallyOmitted, num);
			num = Hash.Combine(CommonAttributeDataComparer.GetHashCodeForConstructorArguments(attr.CommonConstructorArguments), num);
			return Hash.Combine(CommonAttributeDataComparer.GetHashCodeForNamedArguments(attr.NamedArguments), num);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x00043CD4 File Offset: 0x00041ED4
		private static int GetHashCodeForConstructorArguments(ImmutableArray<TypedConstant> constructorArguments)
		{
			int num = 0;
			foreach (TypedConstant typedConstant in constructorArguments)
			{
				num = Hash.Combine(typedConstant.GetHashCode(), num);
			}
			return num;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00043D14 File Offset: 0x00041F14
		private static int GetHashCodeForNamedArguments(ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments)
		{
			int num = 0;
			foreach (KeyValuePair<string, TypedConstant> keyValuePair in namedArguments)
			{
				if (keyValuePair.Key != null)
				{
					num = Hash.Combine(keyValuePair.Key.GetHashCode(), num);
				}
				num = Hash.Combine(keyValuePair.Value.GetHashCode(), num);
			}
			return num;
		}

		// Token: 0x040008B0 RID: 2224
		public static CommonAttributeDataComparer Instance = new CommonAttributeDataComparer();
	}
}
