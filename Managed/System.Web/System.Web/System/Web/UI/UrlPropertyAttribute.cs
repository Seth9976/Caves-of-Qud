using System;

namespace System.Web.UI
{
	/// <summary>Defines the attribute that controls use to identify string properties containing URL values. This class cannot be inherited.</summary>
	// Token: 0x0200023A RID: 570
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class UrlPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Web.UI.UrlPropertyAttribute" /> class.</summary>
		// Token: 0x06001778 RID: 6008 RVA: 0x0003DCC9 File Offset: 0x0003BEC9
		public UrlPropertyAttribute()
			: this("*.*")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.UrlPropertyAttribute" /> class, setting the <see cref="P:System.Web.UI.UrlPropertyAttribute.Filter" /> property to the specified string.</summary>
		/// <param name="filter">A file filter associated with the URL-specific property.</param>
		// Token: 0x06001779 RID: 6009 RVA: 0x0003DCD6 File Offset: 0x0003BED6
		public UrlPropertyAttribute(string filter)
		{
			this.filter = filter;
		}

		/// <summary>Gets a file filter associated with the URL-specific property. </summary>
		/// <returns>A file filter associated with the URL-specific property. The default is "*.*".</returns>
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0003DCE5 File Offset: 0x0003BEE5
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x0600177B RID: 6011 RVA: 0x0003DCF0 File Offset: 0x0003BEF0
		public override bool Equals(object obj)
		{
			UrlPropertyAttribute urlPropertyAttribute = obj as UrlPropertyAttribute;
			return urlPropertyAttribute != null && this.filter.Equals(urlPropertyAttribute.Filter);
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600177C RID: 6012 RVA: 0x0003DD1A File Offset: 0x0003BF1A
		public override int GetHashCode()
		{
			return this.filter.GetHashCode();
		}

		// Token: 0x040015EB RID: 5611
		private string filter;
	}
}
