using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200050F RID: 1295
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal static class PropertySymbolExtensions
	{
		// Token: 0x06006CBE RID: 27838 RVA: 0x00191CE0 File Offset: 0x0018FEE0
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static MethodSymbol GetOwnOrInheritedGetMethod(this PropertySymbol property)
		{
			while (property != null)
			{
				MethodSymbol getMethod = property.GetMethod;
				if (getMethod != null)
				{
					return getMethod;
				}
				property = property.OverriddenProperty;
			}
			return null;
		}

		// Token: 0x06006CBF RID: 27839 RVA: 0x00191D08 File Offset: 0x0018FF08
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static MethodSymbol GetOwnOrInheritedSetMethod(this PropertySymbol property)
		{
			while (property != null)
			{
				MethodSymbol setMethod = property.SetMethod;
				if (setMethod != null)
				{
					return setMethod;
				}
				property = property.OverriddenProperty;
			}
			return null;
		}

		// Token: 0x06006CC0 RID: 27840 RVA: 0x00191D2F File Offset: 0x0018FF2F
		public static bool CanCallMethodsDirectly(this PropertySymbol property)
		{
			return property.MustCallMethodsDirectly || (property.IsIndexedProperty && (!property.IsIndexer || property.HasRefOrOutParameter()));
		}

		// Token: 0x06006CC1 RID: 27841 RVA: 0x00191D58 File Offset: 0x0018FF58
		public static bool HasRefOrOutParameter(this PropertySymbol property)
		{
			foreach (ParameterSymbol parameterSymbol in property.Parameters)
			{
				if (parameterSymbol.RefKind == RefKind.Ref || parameterSymbol.RefKind == RefKind.Out)
				{
					return true;
				}
			}
			return false;
		}
	}
}
