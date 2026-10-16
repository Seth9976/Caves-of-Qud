using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001CC RID: 460
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MergedAliases
	{
		// Token: 0x0600136B RID: 4971 RVA: 0x0003CA90 File Offset: 0x0003AC90
		internal void Merge(MetadataReference reference)
		{
			ArrayBuilder<string> arrayBuilder;
			if (reference.Properties.HasRecursiveAliases)
			{
				if (this.RecursiveAliasesOpt == null)
				{
					this.RecursiveAliasesOpt = ArrayBuilder<string>.GetInstance();
					this.RecursiveAliasesOpt.AddRange(reference.Properties.Aliases);
					return;
				}
				arrayBuilder = this.RecursiveAliasesOpt;
			}
			else
			{
				if (this.AliasesOpt == null)
				{
					this.AliasesOpt = ArrayBuilder<string>.GetInstance();
					this.AliasesOpt.AddRange(reference.Properties.Aliases);
					return;
				}
				arrayBuilder = this.AliasesOpt;
			}
			MergedAliases.Merge(arrayBuilder, reference.Properties.Aliases);
			ArrayBuilder<MetadataReference> arrayBuilder2;
			if ((arrayBuilder2 = this.MergedReferencesOpt) == null)
			{
				arrayBuilder2 = (this.MergedReferencesOpt = ArrayBuilder<MetadataReference>.GetInstance());
			}
			arrayBuilder2.Add(reference);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0003CB49 File Offset: 0x0003AD49
		internal static void Merge(ArrayBuilder<string> aliases, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> newAliases)
		{
			if ((aliases.Count == 0) ^ newAliases.IsEmpty)
			{
				MergedAliases.AddNonIncluded(aliases, MetadataReferenceProperties.GlobalAlias);
			}
			MergedAliases.AddNonIncluded(aliases, newAliases);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0003CB70 File Offset: 0x0003AD70
		[return: Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<string> Merge([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliasesOpt, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> newAliases)
		{
			if (aliasesOpt.IsDefault)
			{
				return newAliases;
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance(aliasesOpt.Length);
			instance.AddRange(aliasesOpt);
			MergedAliases.Merge(instance, newAliases);
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0003CB9C File Offset: 0x0003AD9C
		private static void AddNonIncluded(ArrayBuilder<string> builder, string item)
		{
			if (!builder.Contains(item))
			{
				builder.Add(item);
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		private static void AddNonIncluded(ArrayBuilder<string> builder, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> items)
		{
			int count = builder.Count;
			foreach (string text in items)
			{
				if (builder.IndexOf(text, 0, count) < 0)
				{
					builder.Add(text);
				}
			}
		}

		// Token: 0x040005C8 RID: 1480
		[Nullable(new byte[] { 2, 1 })]
		public ArrayBuilder<string> AliasesOpt;

		// Token: 0x040005C9 RID: 1481
		[Nullable(new byte[] { 2, 1 })]
		public ArrayBuilder<string> RecursiveAliasesOpt;

		// Token: 0x040005CA RID: 1482
		[Nullable(new byte[] { 2, 1 })]
		public ArrayBuilder<MetadataReference> MergedReferencesOpt;
	}
}
