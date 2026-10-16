using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity
{
	// Token: 0x0200000B RID: 11
	public class IllegalMemberReference : IllegalTypeReference
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002DD3 File Offset: 0x00000FD3
		public MemberReference ReferencedMember
		{
			get
			{
				return this.referencedMember;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002DDB File Offset: 0x00000FDB
		public IllegalMemberReference(MemberReference illegalMember, IllegalReferenceUsage illegalUsage, bool indirect)
			: base(illegalMember.DeclaringType, illegalUsage, indirect)
		{
			this.referencedMember = illegalMember;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002DF2 File Offset: 0x00000FF2
		public override string ToString()
		{
			if (!this.indirect)
			{
				return string.Format("Illegal reference to disallowed member: {0}", this.referencedMember);
			}
			return string.Format("Indirect illegal reference via type exclusion to disallowed member: {0}", this.referencedMember);
		}

		// Token: 0x04000024 RID: 36
		private MemberReference referencedMember;
	}
}
