using System;

namespace System.Reflection
{
	// Token: 0x020008C2 RID: 2242
	internal sealed class SignatureByRefType : SignatureHasElementType
	{
		// Token: 0x06004A20 RID: 18976 RVA: 0x000EF75E File Offset: 0x000ED95E
		internal SignatureByRefType(SignatureType elementType)
			: base(elementType)
		{
		}

		// Token: 0x06004A21 RID: 18977 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x06004A22 RID: 18978 RVA: 0x000040F7 File Offset: 0x000022F7
		protected sealed override bool IsByRefImpl()
		{
			return true;
		}

		// Token: 0x06004A23 RID: 18979 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		protected sealed override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06004A24 RID: 18980 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsSZArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004A25 RID: 18981 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public sealed override bool IsVariableBoundArray
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A26 RID: 18982 RVA: 0x000EF767 File Offset: 0x000ED967
		public sealed override int GetArrayRank()
		{
			throw new ArgumentException("Must be an array type.");
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x000EF773 File Offset: 0x000ED973
		protected sealed override string Suffix
		{
			get
			{
				return "&";
			}
		}
	}
}
