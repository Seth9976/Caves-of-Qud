using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200004F RID: 79
	internal sealed class TypeConversions : ConversionsBase
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x00042426 File Offset: 0x00040626
		public TypeConversions(AssemblySymbol corLibrary, bool includeNullability = false)
			: this(corLibrary, 0, includeNullability, null)
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00042432 File Offset: 0x00040632
		private TypeConversions(AssemblySymbol corLibrary, int currentRecursionDepth, bool includeNullability, TypeConversions otherNullabilityOpt)
			: base(corLibrary, currentRecursionDepth, includeNullability, otherNullabilityOpt)
		{
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0004243F File Offset: 0x0004063F
		protected override ConversionsBase CreateInstance(int currentRecursionDepth)
		{
			return new TypeConversions(this.corLibrary, currentRecursionDepth, this.IncludeNullability, null);
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00042454 File Offset: 0x00040654
		protected override ConversionsBase WithNullabilityCore(bool includeNullability)
		{
			return new TypeConversions(this.corLibrary, this.currentRecursionDepth, includeNullability, this);
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00042469 File Offset: 0x00040669
		public override Conversion GetMethodGroupDelegateConversion(BoundMethodGroup source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00042470 File Offset: 0x00040670
		public override Conversion GetMethodGroupFunctionPointerConversion(BoundMethodGroup source, FunctionPointerTypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00042477 File Offset: 0x00040677
		public override Conversion GetStackAllocConversion(BoundStackAllocArrayCreation sourceExpression, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0004247E File Offset: 0x0004067E
		protected override Conversion GetInterpolatedStringConversion(BoundUnconvertedInterpolatedString source, TypeSymbol destination, ref CompoundUseSiteInfo<AssemblySymbol> useSiteInfo)
		{
			throw ExceptionUtilities.Unreachable;
		}
	}
}
