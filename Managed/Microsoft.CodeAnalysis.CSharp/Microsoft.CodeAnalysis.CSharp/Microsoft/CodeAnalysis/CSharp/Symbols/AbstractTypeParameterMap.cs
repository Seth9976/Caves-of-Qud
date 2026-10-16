using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004BC RID: 1212
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal abstract class AbstractTypeParameterMap : AbstractTypeMap
	{
		// Token: 0x060067BE RID: 26558 RVA: 0x00186D02 File Offset: 0x00184F02
		protected AbstractTypeParameterMap(SmallDictionary<TypeParameterSymbol, TypeWithAnnotations> mapping)
		{
			this.Mapping = mapping;
		}

		// Token: 0x060067BF RID: 26559 RVA: 0x00186D14 File Offset: 0x00184F14
		protected sealed override TypeWithAnnotations SubstituteTypeParameter(TypeParameterSymbol typeParameter)
		{
			TypeWithAnnotations typeWithAnnotations;
			if (this.Mapping.TryGetValue(typeParameter, out typeWithAnnotations))
			{
				return typeWithAnnotations;
			}
			return TypeWithAnnotations.Create(typeParameter, NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
		}

		// Token: 0x060067C0 RID: 26560 RVA: 0x00186D44 File Offset: 0x00184F44
		private string GetDebuggerDisplay()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			stringBuilder.Append(base.GetType().Name);
			foreach (KeyValuePair<TypeParameterSymbol, TypeWithAnnotations> keyValuePair in this.Mapping)
			{
				stringBuilder.Append(" ").Append(keyValuePair.Key).Append(":")
					.Append(keyValuePair.Value.Type);
			}
			return stringBuilder.Append("]").ToString();
		}

		// Token: 0x040019BD RID: 6589
		protected readonly SmallDictionary<TypeParameterSymbol, TypeWithAnnotations> Mapping;
	}
}
