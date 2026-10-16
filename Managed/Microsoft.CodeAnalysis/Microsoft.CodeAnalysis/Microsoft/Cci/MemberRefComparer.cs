using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x02000095 RID: 149
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class MemberRefComparer : IEqualityComparer<ITypeMemberReference>
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x0000DE6F File Offset: 0x0000C06F
		internal MemberRefComparer(MetadataWriter metadataWriter)
		{
			this._metadataWriter = metadataWriter;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000DE80 File Offset: 0x0000C080
		[NullableContext(2)]
		public bool Equals(ITypeMemberReference x, ITypeMemberReference y)
		{
			if (x == y)
			{
				return true;
			}
			if (x.GetContainingType(this._metadataWriter.Context) != y.GetContainingType(this._metadataWriter.Context) && this._metadataWriter.GetMemberReferenceParent(x) != this._metadataWriter.GetMemberReferenceParent(y))
			{
				return false;
			}
			if (x.Name != y.Name)
			{
				return false;
			}
			IFieldReference fieldReference = x as IFieldReference;
			IFieldReference fieldReference2 = y as IFieldReference;
			if (fieldReference != null && fieldReference2 != null)
			{
				return this._metadataWriter.GetFieldSignatureIndex(fieldReference) == this._metadataWriter.GetFieldSignatureIndex(fieldReference2);
			}
			IMethodReference methodReference = x as IMethodReference;
			IMethodReference methodReference2 = y as IMethodReference;
			return methodReference != null && methodReference2 != null && this._metadataWriter.GetMethodSignatureHandle(methodReference) == this._metadataWriter.GetMethodSignatureHandle(methodReference2);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000DF54 File Offset: 0x0000C154
		public int GetHashCode(ITypeMemberReference memberRef)
		{
			int num = Hash.Combine<string>(memberRef.Name, this._metadataWriter.GetMemberReferenceParent(memberRef).GetHashCode());
			IFieldReference fieldReference = memberRef as IFieldReference;
			if (fieldReference != null)
			{
				num = Hash.Combine(num, this._metadataWriter.GetFieldSignatureIndex(fieldReference).GetHashCode());
			}
			else
			{
				IMethodReference methodReference = memberRef as IMethodReference;
				if (methodReference != null)
				{
					num = Hash.Combine(num, this._metadataWriter.GetMethodSignatureHandle(methodReference).GetHashCode());
				}
			}
			return num;
		}

		// Token: 0x0400019E RID: 414
		private readonly MetadataWriter _metadataWriter;
	}
}
