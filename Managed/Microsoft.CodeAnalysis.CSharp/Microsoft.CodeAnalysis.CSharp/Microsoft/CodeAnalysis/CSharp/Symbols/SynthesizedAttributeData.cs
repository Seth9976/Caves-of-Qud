using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200055A RID: 1370
	internal sealed class SynthesizedAttributeData : SourceAttributeData
	{
		// Token: 0x06007419 RID: 29721 RVA: 0x001B2084 File Offset: 0x001B0284
		internal SynthesizedAttributeData([global::System.Runtime.CompilerServices.Nullable(1)] MethodSymbol wellKnownMember, ImmutableArray<TypedConstant> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })] ImmutableArray<KeyValuePair<string, TypedConstant>> namedArguments)
			: base(null, wellKnownMember.ContainingType, wellKnownMember, arguments, default(ImmutableArray<int>), namedArguments, false, false)
		{
		}

		// Token: 0x0600741A RID: 29722 RVA: 0x001B20AC File Offset: 0x001B02AC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal SynthesizedAttributeData(SourceAttributeData original)
			: base(original.ApplicationSyntaxReference, original.AttributeClass, original.AttributeConstructor, original.CommonConstructorArguments, original.ConstructorArgumentsSourceIndices, original.CommonNamedArguments, original.HasErrors, original.IsConditionallyOmitted)
		{
		}
	}
}
