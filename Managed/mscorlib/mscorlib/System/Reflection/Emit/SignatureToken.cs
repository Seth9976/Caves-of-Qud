using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent a signature.</summary>
	// Token: 0x02000945 RID: 2373
	[ComVisible(true)]
	public readonly struct SignatureToken : IEquatable<SignatureToken>
	{
		// Token: 0x06005278 RID: 21112 RVA: 0x00102E1B File Offset: 0x0010101B
		internal SignatureToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of SignatureToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of SignatureToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this SignatureToken. </param>
		// Token: 0x06005279 RID: 21113 RVA: 0x00102E24 File Offset: 0x00101024
		public override bool Equals(object obj)
		{
			bool flag = obj is SignatureToken;
			if (flag)
			{
				SignatureToken signatureToken = (SignatureToken)obj;
				flag = this.tokValue == signatureToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.SignatureToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to the current instance.</param>
		// Token: 0x0600527A RID: 21114 RVA: 0x00102E55 File Offset: 0x00101055
		public bool Equals(SignatureToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.SignatureToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600527B RID: 21115 RVA: 0x00102E65 File Offset: 0x00101065
		public static bool operator ==(SignatureToken a, SignatureToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.SignatureToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.SignatureToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x0600527C RID: 21116 RVA: 0x00102E78 File Offset: 0x00101078
		public static bool operator !=(SignatureToken a, SignatureToken b)
		{
			return !object.Equals(a, b);
		}

		/// <summary>Generates the hash code for this signature.</summary>
		/// <returns>Returns the hash code for this signature.</returns>
		// Token: 0x0600527D RID: 21117 RVA: 0x00102E8E File Offset: 0x0010108E
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for the local variable signature for this method.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this signature.</returns>
		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x0600527E RID: 21118 RVA: 0x00102E8E File Offset: 0x0010108E
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x04003318 RID: 13080
		internal readonly int tokValue;

		/// <summary>The default SignatureToken with <see cref="P:System.Reflection.Emit.SignatureToken.Token" /> value 0.</summary>
		// Token: 0x04003319 RID: 13081
		public static readonly SignatureToken Empty;
	}
}
