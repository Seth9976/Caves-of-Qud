using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.Metadata
{
	/// <summary>Represents the signature characteristics specified by the leading byte of signature blobs.</summary>
	// Token: 0x020000D5 RID: 213
	public struct SignatureHeader : IEquatable<SignatureHeader>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.SignatureHeader" /> structure using the specified byte value.</summary>
		/// <param name="rawValue">The byte.</param>
		// Token: 0x060007BC RID: 1980 RVA: 0x0001290A File Offset: 0x00010B0A
		public SignatureHeader(byte rawValue)
		{
			this._rawValue = rawValue;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.Metadata.SignatureHeader" /> structure using the specified signature kind, calling convention and signature attributes.</summary>
		/// <param name="kind">The signature kind.</param>
		/// <param name="convention">The calling convention.</param>
		/// <param name="attributes">The signature attributes.</param>
		// Token: 0x060007BD RID: 1981 RVA: 0x00012913 File Offset: 0x00010B13
		public SignatureHeader(SignatureKind kind, SignatureCallingConvention convention, SignatureAttributes attributes)
		{
			this = new SignatureHeader((byte)(kind | (SignatureKind)convention | (SignatureKind)attributes));
		}

		/// <summary>Gets the raw value of the header byte.</summary>
		/// <returns>The raw value of the header byte.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00012921 File Offset: 0x00010B21
		public byte RawValue
		{
			get
			{
				return this._rawValue;
			}
		}

		/// <summary>Gets the calling convention.</summary>
		/// <returns>The calling convention.</returns>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001292C File Offset: 0x00010B2C
		public SignatureCallingConvention CallingConvention
		{
			get
			{
				int num = (int)(this._rawValue & 15);
				if (num > 5 && num != 9)
				{
					return SignatureCallingConvention.Default;
				}
				return (SignatureCallingConvention)num;
			}
		}

		/// <summary>Gets the signature kind.</summary>
		/// <returns>The signature kind.</returns>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00012950 File Offset: 0x00010B50
		public SignatureKind Kind
		{
			get
			{
				int num = (int)(this._rawValue & 15);
				if (num <= 5 || num == 9)
				{
					return SignatureKind.Method;
				}
				return (SignatureKind)num;
			}
		}

		/// <summary>Gets the signature attributes.</summary>
		/// <returns>The attributes.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00012974 File Offset: 0x00010B74
		public SignatureAttributes Attributes
		{
			get
			{
				return (SignatureAttributes)((int)this._rawValue & -16);
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Reflection.Metadata.SignatureHeader" /> structure has the <see cref="F:System.Reflection.Metadata.SignatureAttributes.ExplicitThis" /> signature attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="F:System.Reflection.Metadata.SignatureAttributes.ExplicitThis" /> attribute is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00012980 File Offset: 0x00010B80
		public bool HasExplicitThis
		{
			get
			{
				return (this._rawValue & 64) > 0;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Reflection.Metadata.SignatureHeader" /> structure has the <see cref="F:System.Reflection.Metadata.SignatureAttributes.Instance" /> signature attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="F:System.Reflection.Metadata.SignatureAttributes.Instance" /> attribute is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0001298E File Offset: 0x00010B8E
		public bool IsInstance
		{
			get
			{
				return (this._rawValue & 32) > 0;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Reflection.Metadata.SignatureHeader" /> structure has the <see cref="F:System.Reflection.Metadata.SignatureAttributes.Generic" /> signature attribute.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="F:System.Reflection.Metadata.SignatureAttributes.Generic" /> attribute is present; otherwise, <see langword="false" />.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0001299C File Offset: 0x00010B9C
		public bool IsGeneric
		{
			get
			{
				return (this._rawValue & 16) > 0;
			}
		}

		/// <summary>Compares the specified object with this <see cref="T:System.Reflection.Metadata.SignatureHeader" /> for equality.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007C5 RID: 1989 RVA: 0x000129AA File Offset: 0x00010BAA
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is SignatureHeader && this.Equals((SignatureHeader)obj);
		}

		/// <summary>Compares two <see cref="T:System.Reflection.Metadata.SignatureHeader" /> values for equality.</summary>
		/// <param name="other">The value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007C6 RID: 1990 RVA: 0x000129C2 File Offset: 0x00010BC2
		public bool Equals(SignatureHeader other)
		{
			return this._rawValue == other._rawValue;
		}

		/// <summary>Gets a hash code for the current object.</summary>
		/// <returns>A hash code for the current object.</returns>
		// Token: 0x060007C7 RID: 1991 RVA: 0x000129D2 File Offset: 0x00010BD2
		public override int GetHashCode()
		{
			return (int)this._rawValue;
		}

		/// <summary>Compares two <see cref="T:System.Reflection.Metadata.SignatureHeader" /> values for equality.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007C8 RID: 1992 RVA: 0x000129DA File Offset: 0x00010BDA
		public static bool operator ==(SignatureHeader left, SignatureHeader right)
		{
			return left._rawValue == right._rawValue;
		}

		/// <summary>Determines whether two <see cref="T:System.Reflection.Metadata.SignatureHeader" /> values are unequal.</summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the values are unequal; otherwise, <see langword="false" />.</returns>
		// Token: 0x060007C9 RID: 1993 RVA: 0x000129EA File Offset: 0x00010BEA
		public static bool operator !=(SignatureHeader left, SignatureHeader right)
		{
			return left._rawValue != right._rawValue;
		}

		/// <summary>Returns a string that represents the current object.</summary>
		/// <returns>A string that represents the current object.</returns>
		// Token: 0x060007CA RID: 1994 RVA: 0x00012A00 File Offset: 0x00010C00
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Kind.ToString());
			if (this.Kind == SignatureKind.Method)
			{
				stringBuilder.Append(',');
				stringBuilder.Append(this.CallingConvention.ToString());
			}
			if (this.Attributes != SignatureAttributes.None)
			{
				stringBuilder.Append(',');
				stringBuilder.Append(this.Attributes.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040004B5 RID: 1205
		private readonly byte _rawValue;

		/// <summary>Gets the mask value for the calling convention or signature kind. The default <see cref="F:System.Reflection.Metadata.SignatureHeader.CallingConventionOrKindMask" /> value is 15 (0x0F).</summary>
		// Token: 0x040004B6 RID: 1206
		public const byte CallingConventionOrKindMask = 15;

		// Token: 0x040004B7 RID: 1207
		private const byte maxCallingConvention = 5;
	}
}
