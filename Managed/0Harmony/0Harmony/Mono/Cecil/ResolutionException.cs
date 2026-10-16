using System;
using System.Runtime.Serialization;

namespace Mono.Cecil
{
	// Token: 0x02000232 RID: 562
	[Serializable]
	public sealed class ResolutionException : Exception
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00029AB9 File Offset: 0x00027CB9
		public MemberReference Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00029AC4 File Offset: 0x00027CC4
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

		// Token: 0x06000C36 RID: 3126 RVA: 0x00029B02 File Offset: 0x00027D02
		public ResolutionException(MemberReference member)
			: base("Failed to resolve " + member.FullName)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.member = member;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00029B2F File Offset: 0x00027D2F
		public ResolutionException(MemberReference member, Exception innerException)
			: base("Failed to resolve " + member.FullName, innerException)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.member = member;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00029B5D File Offset: 0x00027D5D
		private ResolutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x0400036D RID: 877
		private readonly MemberReference member;
	}
}
