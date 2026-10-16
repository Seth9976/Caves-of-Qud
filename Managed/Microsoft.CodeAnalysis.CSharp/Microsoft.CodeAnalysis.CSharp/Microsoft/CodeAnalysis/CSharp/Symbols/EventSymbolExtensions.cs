using System;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004E4 RID: 1252
	internal static class EventSymbolExtensions
	{
		// Token: 0x060069E1 RID: 27105 RVA: 0x0018ADF5 File Offset: 0x00188FF5
		internal static MethodSymbol GetOwnOrInheritedAccessor(this EventSymbol @event, bool isAdder)
		{
			if (!isAdder)
			{
				return @event.GetOwnOrInheritedRemoveMethod();
			}
			return @event.GetOwnOrInheritedAddMethod();
		}
	}
}
