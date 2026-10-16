using System;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.WebRequest" /> objects.</summary>
	// Token: 0x0200078C RID: 1932
	public class RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class. </summary>
		// Token: 0x06003CCC RID: 15564 RVA: 0x000CEC04 File Offset: 0x000CCE04
		public RequestCachePolicy()
			: this(RequestCacheLevel.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.RequestCachePolicy" /> class. using the specified cache policy.</summary>
		/// <param name="level">A <see cref="T:System.Net.Cache.RequestCacheLevel" /> that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">level is not a valid <see cref="T:System.Net.Cache.RequestCacheLevel" />.value.</exception>
		// Token: 0x06003CCD RID: 15565 RVA: 0x000CEC0D File Offset: 0x000CCE0D
		public RequestCachePolicy(RequestCacheLevel level)
		{
			if (level < RequestCacheLevel.Default || level > RequestCacheLevel.NoCacheNoStore)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.m_Level = level;
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.RequestCacheLevel" /> value specified when this instance was constructed.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCacheLevel" /> value that specifies the cache behavior for resources obtained using <see cref="T:System.Net.WebRequest" /> objects.</returns>
		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000CEC2F File Offset: 0x000CCE2F
		public RequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the <see cref="P:System.Net.Cache.RequestCachePolicy.Level" /> for this instance.</returns>
		// Token: 0x06003CCF RID: 15567 RVA: 0x000CEC37 File Offset: 0x000CCE37
		public override string ToString()
		{
			return "Level:" + this.m_Level.ToString();
		}

		// Token: 0x04002414 RID: 9236
		private RequestCacheLevel m_Level;
	}
}
