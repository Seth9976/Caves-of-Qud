using System;
using System.Runtime.Serialization;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000AB RID: 171
	[Serializable]
	public sealed class ResolutionException : Exception
	{
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00015B0E File Offset: 0x00013D0E
		public MemberReference Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00015B18 File Offset: 0x00013D18
		public IMetadataScope Scope
		{
			get
			{
				TypeReference typeReference = this.member as TypeReference;
				if (typeReference != null)
				{
					return typeReference.Scope;
				}
				TypeReference declaringType = this.member.DeclaringType;
				if (declaringType != null)
				{
					return declaringType.Scope;
				}
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00015B56 File Offset: 0x00013D56
		public ResolutionException(MemberReference member)
			: base("Failed to resolve " + member.FullName)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.member = member;
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00015B83 File Offset: 0x00013D83
		private ResolutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x04000253 RID: 595
		private readonly MemberReference member;
	}
}
