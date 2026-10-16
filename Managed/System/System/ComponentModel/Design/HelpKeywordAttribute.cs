using System;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the context keyword for a class or member. This class cannot be inherited.</summary>
	// Token: 0x02000457 RID: 1111
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class HelpKeywordAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class. </summary>
		// Token: 0x060023FC RID: 9212 RVA: 0x00003D9F File Offset: 0x00001F9F
		public HelpKeywordAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class. </summary>
		/// <param name="keyword">The Help keyword value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null.</exception>
		// Token: 0x060023FD RID: 9213 RVA: 0x00081809 File Offset: 0x0007FA09
		public HelpKeywordAttribute(string keyword)
		{
			if (keyword == null)
			{
				throw new ArgumentNullException("keyword");
			}
			this.HelpKeyword = keyword;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> class from the given type. </summary>
		/// <param name="t">The type from which the Help keyword will be taken.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="t" /> is null.</exception>
		// Token: 0x060023FE RID: 9214 RVA: 0x00081826 File Offset: 0x0007FA26
		public HelpKeywordAttribute(Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			this.HelpKeyword = t.FullName;
		}

		/// <summary>Gets the Help keyword supplied by this attribute.</summary>
		/// <returns>The Help keyword supplied by this attribute.</returns>
		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x0008184E File Offset: 0x0007FA4E
		public string HelpKeyword { get; }

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> is equal to the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" /> to compare with the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</param>
		// Token: 0x06002400 RID: 9216 RVA: 0x00081856 File Offset: 0x0007FA56
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is HelpKeywordAttribute && ((HelpKeywordAttribute)obj).HelpKeyword == this.HelpKeyword);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />.</returns>
		// Token: 0x06002401 RID: 9217 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines whether the Help keyword is null.</summary>
		/// <returns>true if the Help keyword is null; otherwise, false.</returns>
		// Token: 0x06002402 RID: 9218 RVA: 0x00081881 File Offset: 0x0007FA81
		public override bool IsDefaultAttribute()
		{
			return this.Equals(HelpKeywordAttribute.Default);
		}

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.Design.HelpKeywordAttribute" />. This field is read-only.</summary>
		// Token: 0x040010ED RID: 4333
		public static readonly HelpKeywordAttribute Default = new HelpKeywordAttribute();
	}
}
