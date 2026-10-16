using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil;
using Trivial.Mono.Collections.Generic;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x0200001D RID: 29
	internal class TypeReferenceChecker : CodeChecker<TypeReference>
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004721 File Offset: 0x00002921
		public TypeReferenceChecker(ICodeUsageProvider usageProvider)
		{
			if (usageProvider == null)
			{
				throw new ArgumentNullException("usageProvider");
			}
			this.usageProvider = usageProvider;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004740 File Offset: 0x00002940
		public override void SecurityCheckCode(CodeSecurityContext context, TypeReference typeReference)
		{
			foreach (TypeReference typeReference2 in this.ExpandTypeReferenceGenerics(typeReference))
			{
				base.SecurityCheckAndReportTypeReferenceAndNamespace(context, typeReference2, this.usageProvider);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004798 File Offset: 0x00002998
		private IEnumerable<TypeReference> ExpandTypeReferenceGenerics(TypeReference reference)
		{
			yield return reference;
			if (reference.HasGenericParameters)
			{
				foreach (GenericParameter genericParameter in reference.GenericParameters)
				{
					TypeDefinition typeDefinition = genericParameter.Resolve();
					if (typeDefinition != null)
					{
						foreach (TypeReference typeReference in this.ExpandTypeReferenceGenerics(typeDefinition))
						{
							yield return typeReference;
						}
						IEnumerator<TypeReference> enumerator2 = null;
					}
				}
				Collection<GenericParameter>.Enumerator enumerator = default(Collection<GenericParameter>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x04000059 RID: 89
		private ICodeUsageProvider usageProvider;
	}
}
