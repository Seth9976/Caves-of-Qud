using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity
{
	// Token: 0x0200000C RID: 12
	public class IllegalNamespaceReference
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002E1D File Offset: 0x0000101D
		public string ReferencedNamespace
		{
			get
			{
				return this.referencedNamespace;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002E25 File Offset: 0x00001025
		public ICollection<IllegalTypeReference> IndirectIllegalTypeReferences
		{
			get
			{
				return this.indirectIllegalTypeReferences;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002E2D File Offset: 0x0000102D
		public ICollection<IllegalReferenceUsage> ReferenceUsages
		{
			get
			{
				return this.referenceUsages;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002E35 File Offset: 0x00001035
		public IllegalNamespaceReference(string illegalNamespace, TypeReference illegalTypeInIllegalNamespace, IllegalReferenceUsage illegalUsage)
		{
			this.referencedNamespace = illegalNamespace;
			this.AddIllegalNamespaceUsage(illegalTypeInIllegalNamespace, illegalUsage);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E64 File Offset: 0x00001064
		public void AddIllegalNamespaceUsage(TypeReference illegalTypeInIllegalNamespace, IllegalReferenceUsage illegalUsage)
		{
			this.referenceUsages.Add(illegalUsage);
			foreach (IllegalTypeReference illegalTypeReference in this.indirectIllegalTypeReferences)
			{
				if (illegalTypeReference.ReferencedType == illegalTypeInIllegalNamespace)
				{
					illegalTypeReference.AddIllegalReferenceUsage(illegalUsage);
					return;
				}
			}
			this.indirectIllegalTypeReferences.Add(new IllegalTypeReference(illegalTypeInIllegalNamespace, illegalUsage, true));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002EE4 File Offset: 0x000010E4
		public override string ToString()
		{
			return string.Format("Illegal reference to disallowed namespace: {0}", this.referencedNamespace);
		}

		// Token: 0x04000025 RID: 37
		private string referencedNamespace;

		// Token: 0x04000026 RID: 38
		private List<IllegalTypeReference> indirectIllegalTypeReferences = new List<IllegalTypeReference>();

		// Token: 0x04000027 RID: 39
		private List<IllegalReferenceUsage> referenceUsages = new List<IllegalReferenceUsage>();
	}
}
