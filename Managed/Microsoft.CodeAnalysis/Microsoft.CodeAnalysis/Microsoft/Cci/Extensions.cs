using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.Cci
{
	// Token: 0x020000AB RID: 171
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Extensions
	{
		// Token: 0x06000474 RID: 1140 RVA: 0x0000E01D File Offset: 0x0000C21D
		internal static bool HasBody(this IMethodDefinition methodDef)
		{
			return !methodDef.IsAbstract && !methodDef.IsExternal && (methodDef.ContainingTypeDefinition == null || !methodDef.ContainingTypeDefinition.IsComObject);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000E04C File Offset: 0x0000C24C
		public static bool ShouldInclude(this ITypeDefinitionMember member, EmitContext context)
		{
			if (context.IncludePrivateMembers)
			{
				return true;
			}
			IMethodDefinition methodDefinition = member as IMethodDefinition;
			if (methodDefinition != null && methodDefinition.IsVirtual)
			{
				return true;
			}
			TypeMemberVisibility visibility = member.Visibility;
			if (visibility == TypeMemberVisibility.Private)
			{
				return context.IncludePrivateMembers;
			}
			if (visibility - TypeMemberVisibility.FamilyAndAssembly > 1)
			{
				return true;
			}
			if (!context.IncludePrivateMembers)
			{
				ISourceAssemblySymbolInternal sourceAssemblyOpt = context.Module.SourceAssemblyOpt;
				return sourceAssemblyOpt != null && sourceAssemblyOpt.InternalsAreVisible;
			}
			return true;
		}
	}
}
