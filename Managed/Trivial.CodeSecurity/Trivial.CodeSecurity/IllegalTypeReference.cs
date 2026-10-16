using System;
using System.Collections.Generic;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity
{
	// Token: 0x0200000E RID: 14
	public class IllegalTypeReference
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00003942 File Offset: 0x00001B42
		public TypeReference ReferencedType
		{
			get
			{
				return this.referencedType;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000394A File Offset: 0x00001B4A
		public ICollection<IllegalReferenceUsage> ReferenceUsages
		{
			get
			{
				return this.referenceUsages;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00003952 File Offset: 0x00001B52
		public int ReferenceUsageCount
		{
			get
			{
				return this.referenceUsages.Count;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000395F File Offset: 0x00001B5F
		public IllegalTypeReference(TypeReference illegalType, IllegalReferenceUsage illegalUsage, bool indirect = false)
		{
			this.referencedType = illegalType;
			this.referenceUsages.Add(illegalUsage);
			this.indirect = indirect;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000398C File Offset: 0x00001B8C
		public void AddIllegalReferenceUsage(IllegalReferenceUsage illegalUsage)
		{
			this.referenceUsages.Add(illegalUsage);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000399A File Offset: 0x00001B9A
		public override string ToString()
		{
			if (!this.indirect)
			{
				return string.Format("Illegal reference to disallowed type: {0}", this.referencedType);
			}
			return string.Format("Indirect illegal reference via namespace exclusion to disallowed type: {0}", this.referencedType);
		}

		// Token: 0x04000034 RID: 52
		private TypeReference referencedType;

		// Token: 0x04000035 RID: 53
		private List<IllegalReferenceUsage> referenceUsages = new List<IllegalReferenceUsage>();

		// Token: 0x04000036 RID: 54
		protected bool indirect;
	}
}
