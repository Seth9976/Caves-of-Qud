using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000036 RID: 54
	internal static class LookupOptionExtensions
	{
		// Token: 0x06000687 RID: 1671 RVA: 0x0003B214 File Offset: 0x00039414
		internal static bool AreValid(this LookupOptions options)
		{
			if (options == LookupOptions.Default)
			{
				return true;
			}
			if ((options & LookupOptions.LabelsOnly) != LookupOptions.Default)
			{
				return options == LookupOptions.LabelsOnly;
			}
			LookupOptions lookupOptions = LookupOptions.MustBeInstance | LookupOptions.MustNotBeInstance;
			return (options & lookupOptions) != lookupOptions && ((options & (LookupOptions.MustNotBeNamespace | LookupOptions.MustNotBeMethodTypeParameter)) == LookupOptions.Default || (options & (LookupOptions.NamespaceAliasesOnly | LookupOptions.NamespacesOrTypesOnly)) == LookupOptions.Default) && LookupOptionExtensions.OnlyOneBitSet(options & (LookupOptions.NamespaceAliasesOnly | LookupOptions.NamespacesOrTypesOnly | LookupOptions.AllMethodsOnArityZero));
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0003B25F File Offset: 0x0003945F
		internal static void ThrowIfInvalid(this LookupOptions options)
		{
			if (!options.AreValid())
			{
				throw new ArgumentException(CSharpResources.LookupOptionsHasInvalidCombo);
			}
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0003B274 File Offset: 0x00039474
		private static bool OnlyOneBitSet(LookupOptions o)
		{
			return (o & (o - 1)) == LookupOptions.Default;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0003B27E File Offset: 0x0003947E
		internal static bool CanConsiderMembers(this LookupOptions options)
		{
			return (options & (LookupOptions.NamespaceAliasesOnly | LookupOptions.NamespacesOrTypesOnly | LookupOptions.LabelsOnly)) == LookupOptions.Default;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0003B28A File Offset: 0x0003948A
		internal static bool CanConsiderLocals(this LookupOptions options)
		{
			return (options & (LookupOptions.NamespaceAliasesOnly | LookupOptions.NamespacesOrTypesOnly | LookupOptions.LabelsOnly)) == LookupOptions.Default;
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0003B296 File Offset: 0x00039496
		internal static bool CanConsiderTypes(this LookupOptions options)
		{
			return (options & (LookupOptions.NamespaceAliasesOnly | LookupOptions.MustBeInvocableIfMember | LookupOptions.MustBeInstance | LookupOptions.LabelsOnly)) == LookupOptions.Default;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0003B2A2 File Offset: 0x000394A2
		internal static bool CanConsiderNamespaces(this LookupOptions options)
		{
			return (options & (LookupOptions.MustBeInvocableIfMember | LookupOptions.MustBeInstance | LookupOptions.MustNotBeNamespace | LookupOptions.LabelsOnly)) == LookupOptions.Default;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0003B2AE File Offset: 0x000394AE
		internal static bool IsAttributeTypeLookup(this LookupOptions options)
		{
			return (options & LookupOptions.AttributeTypeOnly) == LookupOptions.AttributeTypeOnly;
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0003B2BE File Offset: 0x000394BE
		internal static bool IsVerbatimNameAttributeTypeLookup(this LookupOptions options)
		{
			return (options & LookupOptions.VerbatimNameAttributeTypeOnly) == LookupOptions.VerbatimNameAttributeTypeOnly;
		}
	}
}
