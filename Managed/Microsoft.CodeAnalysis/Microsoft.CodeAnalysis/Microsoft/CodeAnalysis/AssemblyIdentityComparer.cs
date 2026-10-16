using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001B1 RID: 433
	[NullableContext(1)]
	[Nullable(0)]
	public class AssemblyIdentityComparer
	{
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x000388E4 File Offset: 0x00036AE4
		public static AssemblyIdentityComparer Default { get; } = new AssemblyIdentityComparer();

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x000388EB File Offset: 0x00036AEB
		public static StringComparer SimpleNameComparer
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x000388F2 File Offset: 0x00036AF2
		public static StringComparer CultureComparer
		{
			get
			{
				return StringComparer.OrdinalIgnoreCase;
			}
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000388F9 File Offset: 0x00036AF9
		internal AssemblyIdentityComparer()
		{
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00038904 File Offset: 0x00036B04
		public bool ReferenceMatchesDefinition(string referenceDisplayName, AssemblyIdentity definition)
		{
			bool flag;
			return this.Compare(null, referenceDisplayName, definition, out flag, false) > AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00038920 File Offset: 0x00036B20
		public bool ReferenceMatchesDefinition(AssemblyIdentity reference, AssemblyIdentity definition)
		{
			bool flag;
			return this.Compare(reference, null, definition, out flag, false) > AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0003893C File Offset: 0x00036B3C
		public AssemblyIdentityComparer.ComparisonResult Compare(AssemblyIdentity reference, AssemblyIdentity definition)
		{
			bool flag;
			return this.Compare(reference, null, definition, out flag, true);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00038958 File Offset: 0x00036B58
		[NullableContext(2)]
		internal AssemblyIdentityComparer.ComparisonResult Compare(AssemblyIdentity reference, string referenceDisplayName, [Nullable(1)] AssemblyIdentity definition, out bool unificationApplied, bool ignoreVersion)
		{
			unificationApplied = false;
			AssemblyIdentityParts assemblyIdentityParts;
			if (reference != null)
			{
				bool? flag = AssemblyIdentityComparer.TriviallyEquivalent(reference, definition);
				if (flag != null)
				{
					if (!flag.GetValueOrDefault())
					{
						return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
					}
					return AssemblyIdentityComparer.ComparisonResult.Equivalent;
				}
				else
				{
					assemblyIdentityParts = AssemblyIdentityParts.Name | AssemblyIdentityParts.Version | AssemblyIdentityParts.Culture | AssemblyIdentityParts.PublicKeyToken;
				}
			}
			else if (!AssemblyIdentity.TryParseDisplayName(referenceDisplayName, out reference, out assemblyIdentityParts) || reference.ContentType != definition.ContentType)
			{
				return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
			}
			bool flag2;
			if (!this.ApplyUnificationPolicies(ref reference, ref definition, assemblyIdentityParts, out flag2))
			{
				return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
			}
			if (reference == definition)
			{
				return AssemblyIdentityComparer.ComparisonResult.Equivalent;
			}
			bool flag3 = (assemblyIdentityParts & AssemblyIdentityParts.Culture) > (AssemblyIdentityParts)0;
			bool flag4 = (assemblyIdentityParts & AssemblyIdentityParts.PublicKeyOrToken) > (AssemblyIdentityParts)0;
			if (!definition.IsStrongName)
			{
				if (reference.IsStrongName)
				{
					return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
				}
				if (!AssemblyIdentity.IsFullName(assemblyIdentityParts))
				{
					if (!AssemblyIdentityComparer.SimpleNameComparer.Equals(reference.Name, definition.Name))
					{
						return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
					}
					if (flag3 && !AssemblyIdentityComparer.CultureComparer.Equals(reference.CultureName, definition.CultureName))
					{
						return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
					}
					return AssemblyIdentityComparer.ComparisonResult.Equivalent;
				}
				else
				{
					flag2 = false;
				}
			}
			if (!AssemblyIdentityComparer.SimpleNameComparer.Equals(reference.Name, definition.Name))
			{
				return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
			}
			if (flag3 && !AssemblyIdentityComparer.CultureComparer.Equals(reference.CultureName, definition.CultureName))
			{
				return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
			}
			if (flag4 && !AssemblyIdentity.KeysEqual(reference, definition))
			{
				return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
			}
			bool flag5 = (assemblyIdentityParts & AssemblyIdentityParts.Version) > (AssemblyIdentityParts)0;
			bool flag6 = (assemblyIdentityParts & AssemblyIdentityParts.Version) != AssemblyIdentityParts.Version;
			if (!definition.IsStrongName || !flag5 || (!flag6 && !(reference.Version != definition.Version)))
			{
				return AssemblyIdentityComparer.ComparisonResult.Equivalent;
			}
			if (flag2)
			{
				unificationApplied = true;
				return AssemblyIdentityComparer.ComparisonResult.Equivalent;
			}
			if (ignoreVersion)
			{
				return AssemblyIdentityComparer.ComparisonResult.EquivalentIgnoringVersion;
			}
			return AssemblyIdentityComparer.ComparisonResult.NotEquivalent;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00038AC0 File Offset: 0x00036CC0
		private static bool? TriviallyEquivalent(AssemblyIdentity x, AssemblyIdentity y)
		{
			if (x.ContentType != y.ContentType)
			{
				return new bool?(false);
			}
			if (x.IsRetargetable || y.IsRetargetable)
			{
				return default(bool?);
			}
			return AssemblyIdentity.MemberwiseEqual(x, y);
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00038B03 File Offset: 0x00036D03
		internal virtual bool ApplyUnificationPolicies(ref AssemblyIdentity reference, ref AssemblyIdentity definition, AssemblyIdentityParts referenceParts, out bool isDefinitionFxAssembly)
		{
			isDefinitionFxAssembly = false;
			return true;
		}

		// Token: 0x020005C5 RID: 1477
		[NullableContext(0)]
		public enum ComparisonResult
		{
			// Token: 0x0400169A RID: 5786
			NotEquivalent,
			// Token: 0x0400169B RID: 5787
			Equivalent,
			// Token: 0x0400169C RID: 5788
			EquivalentIgnoringVersion
		}
	}
}
