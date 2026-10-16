using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A7 RID: 1447
	internal static class TypeParameterSymbolExtensions
	{
		// Token: 0x060078C9 RID: 30921 RVA: 0x001B9C68 File Offset: 0x001B7E68
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static bool DependsOn(this TypeParameterSymbol typeParameter1, TypeParameterSymbol typeParameter2)
		{
			Stack<TypeParameterSymbol> stack = null;
			HashSet<TypeParameterSymbol> hashSet = null;
			for (;;)
			{
				foreach (TypeWithAnnotations typeWithAnnotations in typeParameter1.ConstraintTypesNoUseSiteDiagnostics)
				{
					TypeParameterSymbol typeParameterSymbol = typeWithAnnotations.Type as TypeParameterSymbol;
					if (typeParameterSymbol != null)
					{
						if (typeParameterSymbol.Equals(typeParameter2))
						{
							return true;
						}
						if (hashSet == null)
						{
							hashSet = new HashSet<TypeParameterSymbol>();
						}
						if (hashSet.Add(typeParameterSymbol))
						{
							if (stack == null)
							{
								stack = new Stack<TypeParameterSymbol>();
							}
							stack.Push(typeParameterSymbol);
						}
					}
				}
				if (stack == null || stack.Count == 0)
				{
					return false;
				}
				typeParameter1 = stack.Pop();
			}
			return true;
		}
	}
}
