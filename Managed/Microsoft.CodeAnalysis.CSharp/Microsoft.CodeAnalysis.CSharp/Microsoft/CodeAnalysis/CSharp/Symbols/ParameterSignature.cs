using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200050C RID: 1292
	internal class ParameterSignature
	{
		// Token: 0x06006CAC RID: 27820 RVA: 0x00191AA4 File Offset: 0x0018FCA4
		private ParameterSignature(ImmutableArray<TypeWithAnnotations> parameterTypesWithAnnotations, ImmutableArray<RefKind> parameterRefKinds)
		{
			this.parameterTypesWithAnnotations = parameterTypesWithAnnotations;
			this.parameterRefKinds = parameterRefKinds;
		}

		// Token: 0x06006CAD RID: 27821 RVA: 0x00191ABC File Offset: 0x0018FCBC
		private static ParameterSignature MakeParamTypesAndRefKinds(ImmutableArray<ParameterSymbol> parameters)
		{
			if (parameters.Length == 0)
			{
				return ParameterSignature.NoParams;
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			ArrayBuilder<RefKind> arrayBuilder = null;
			for (int i = 0; i < parameters.Length; i++)
			{
				ParameterSymbol parameterSymbol = parameters[i];
				instance.Add(parameterSymbol.TypeWithAnnotations);
				RefKind refKind = parameterSymbol.RefKind;
				if (arrayBuilder == null)
				{
					if (refKind != RefKind.None)
					{
						arrayBuilder = ArrayBuilder<RefKind>.GetInstance(i, RefKind.None);
						arrayBuilder.Add(refKind);
					}
				}
				else
				{
					arrayBuilder.Add(refKind);
				}
			}
			ImmutableArray<RefKind> immutableArray = ((arrayBuilder != null) ? arrayBuilder.ToImmutableAndFree() : default(ImmutableArray<RefKind>));
			return new ParameterSignature(instance.ToImmutableAndFree(), immutableArray);
		}

		// Token: 0x06006CAE RID: 27822 RVA: 0x00191B55 File Offset: 0x0018FD55
		internal static void PopulateParameterSignature(ImmutableArray<ParameterSymbol> parameters, ref ParameterSignature lazySignature)
		{
			if (lazySignature == null)
			{
				Interlocked.CompareExchange<ParameterSignature>(ref lazySignature, ParameterSignature.MakeParamTypesAndRefKinds(parameters), null);
			}
		}

		// Token: 0x04001AE7 RID: 6887
		internal readonly ImmutableArray<TypeWithAnnotations> parameterTypesWithAnnotations;

		// Token: 0x04001AE8 RID: 6888
		internal readonly ImmutableArray<RefKind> parameterRefKinds;

		// Token: 0x04001AE9 RID: 6889
		internal static readonly ParameterSignature NoParams = new ParameterSignature(ImmutableArray<TypeWithAnnotations>.Empty, default(ImmutableArray<RefKind>));
	}
}
