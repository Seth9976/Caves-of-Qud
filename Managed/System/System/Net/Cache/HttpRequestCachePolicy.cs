using System;
using System.Globalization;

namespace System.Net.Cache
{
	/// <summary>Defines an application's caching requirements for resources obtained by using <see cref="T:System.Net.HttpWebRequest" /> objects.</summary>
	// Token: 0x0200078F RID: 1935
	public class HttpRequestCachePolicy : RequestCachePolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class. </summary>
		// Token: 0x06003CD0 RID: 15568 RVA: 0x000CEC54 File Offset: 0x000CCE54
		public HttpRequestCachePolicy()
			: this(HttpRequestCacheLevel.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified cache policy.</summary>
		/// <param name="level">An <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value. </param>
		// Token: 0x06003CD1 RID: 15569 RVA: 0x000CEC60 File Offset: 0x000CCE60
		public HttpRequestCachePolicy(HttpRequestCacheLevel level)
			: base(HttpRequestCachePolicy.MapLevel(level))
		{
			this.m_Level = level;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified age control and time values. </summary>
		/// <param name="cacheAgeControl">One of the following <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> enumeration values: <see cref="F:System.Net.Cache.HttpCacheAgeControl.MaxAge" />, <see cref="F:System.Net.Cache.HttpCacheAgeControl.MaxStale" />, or <see cref="F:System.Net.Cache.HttpCacheAgeControl.MinFresh" />.</param>
		/// <param name="ageOrFreshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information. </param>
		/// <exception cref="T:System.ArgumentException">The value specified for the <paramref name="cacheAgeControl" /> parameter cannot be used with this constructor.</exception>
		// Token: 0x06003CD2 RID: 15570 RVA: 0x000CECAC File Offset: 0x000CCEAC
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan ageOrFreshOrStale)
			: this(HttpRequestCacheLevel.Default)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.m_MinFresh = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.m_MaxAge = ageOrFreshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.m_MaxStale = ageOrFreshOrStale;
				return;
			}
			throw new ArgumentException(SR.GetString("The specified value is not valid in the '{0}' enumeration.", new object[] { "HttpCacheAgeControl" }), "cacheAgeControl");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified maximum age, age control value, and time value.</summary>
		/// <param name="cacheAgeControl">An <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> value. </param>
		/// <param name="maxAge">A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for resources.</param>
		/// <param name="freshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information.  </param>
		/// <exception cref="T:System.ArgumentException">The value specified for the <paramref name="cacheAgeControl" /> parameter is not valid.</exception>
		// Token: 0x06003CD3 RID: 15571 RVA: 0x000CED14 File Offset: 0x000CCF14
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale)
			: this(HttpRequestCacheLevel.Default)
		{
			switch (cacheAgeControl)
			{
			case HttpCacheAgeControl.MinFresh:
				this.m_MinFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxAge:
				this.m_MaxAge = maxAge;
				return;
			case HttpCacheAgeControl.MaxAgeAndMinFresh:
				this.m_MaxAge = maxAge;
				this.m_MinFresh = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxStale:
				this.m_MaxStale = freshOrStale;
				return;
			case HttpCacheAgeControl.MaxAgeAndMaxStale:
				this.m_MaxAge = maxAge;
				this.m_MaxStale = freshOrStale;
				return;
			}
			throw new ArgumentException(SR.GetString("The specified value is not valid in the '{0}' enumeration.", new object[] { "HttpCacheAgeControl" }), "cacheAgeControl");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified cache synchronization date.</summary>
		/// <param name="cacheSyncDate">A <see cref="T:System.DateTime" /> object that specifies the time when resources stored in the cache must be revalidated.</param>
		// Token: 0x06003CD4 RID: 15572 RVA: 0x000CEDA2 File Offset: 0x000CCFA2
		public HttpRequestCachePolicy(DateTime cacheSyncDate)
			: this(HttpRequestCacheLevel.Default)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cache.HttpRequestCachePolicy" /> class using the specified maximum age, age control value, time value, and cache synchronization date.</summary>
		/// <param name="cacheAgeControl">An <see cref="T:System.Net.Cache.HttpCacheAgeControl" /> value. </param>
		/// <param name="maxAge">A <see cref="T:System.TimeSpan" /> value that specifies the maximum age for resources.</param>
		/// <param name="freshOrStale">A <see cref="T:System.TimeSpan" /> value that specifies an amount of time. See the Remarks section for more information.  </param>
		/// <param name="cacheSyncDate">A <see cref="T:System.DateTime" /> object that specifies the time when resources stored in the cache must be revalidated.</param>
		// Token: 0x06003CD5 RID: 15573 RVA: 0x000CEDB8 File Offset: 0x000CCFB8
		public HttpRequestCachePolicy(HttpCacheAgeControl cacheAgeControl, TimeSpan maxAge, TimeSpan freshOrStale, DateTime cacheSyncDate)
			: this(cacheAgeControl, maxAge, freshOrStale)
		{
			this.m_LastSyncDateUtc = cacheSyncDate.ToUniversalTime();
		}

		/// <summary>Gets the <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that was specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.HttpRequestCacheLevel" /> value that specifies the cache behavior for resources that were obtained using <see cref="T:System.Net.HttpWebRequest" /> objects.</returns>
		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x000CEDD0 File Offset: 0x000CCFD0
		public new HttpRequestCacheLevel Level
		{
			get
			{
				return this.m_Level;
			}
		}

		/// <summary>Gets the cache synchronization date for this instance.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value set to the date specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000CEDD8 File Offset: 0x000CCFD8
		public DateTime CacheSyncDate
		{
			get
			{
				if (this.m_LastSyncDateUtc == DateTime.MinValue || this.m_LastSyncDateUtc == DateTime.MaxValue)
				{
					return this.m_LastSyncDateUtc;
				}
				return this.m_LastSyncDateUtc.ToLocalTime();
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003CD8 RID: 15576 RVA: 0x000CEE10 File Offset: 0x000CD010
		internal DateTime InternalCacheSyncDateUtc
		{
			get
			{
				return this.m_LastSyncDateUtc;
			}
		}

		/// <summary>Gets the maximum age permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum age value specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003CD9 RID: 15577 RVA: 0x000CEE18 File Offset: 0x000CD018
		public TimeSpan MaxAge
		{
			get
			{
				return this.m_MaxAge;
			}
		}

		/// <summary>Gets the minimum freshness that is permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the minimum freshness specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003CDA RID: 15578 RVA: 0x000CEE20 File Offset: 0x000CD020
		public TimeSpan MinFresh
		{
			get
			{
				return this.m_MinFresh;
			}
		}

		/// <summary>Gets the maximum staleness value that is permitted for a resource returned from the cache.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that is set to the maximum staleness value specified when this instance was created. If no date was specified, this property's value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003CDB RID: 15579 RVA: 0x000CEE28 File Offset: 0x000CD028
		public TimeSpan MaxStale
		{
			get
			{
				return this.m_MaxStale;
			}
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the property values for this instance.</returns>
		// Token: 0x06003CDC RID: 15580 RVA: 0x000CEE30 File Offset: 0x000CD030
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Level:",
				this.m_Level.ToString(),
				(this.m_MaxAge == TimeSpan.MaxValue) ? string.Empty : (" MaxAge:" + this.m_MaxAge.ToString()),
				(this.m_MinFresh == TimeSpan.MinValue) ? string.Empty : (" MinFresh:" + this.m_MinFresh.ToString()),
				(this.m_MaxStale == TimeSpan.MinValue) ? string.Empty : (" MaxStale:" + this.m_MaxStale.ToString()),
				(this.CacheSyncDate == DateTime.MinValue) ? string.Empty : (" CacheSyncDate:" + this.CacheSyncDate.ToString(CultureInfo.CurrentCulture))
			});
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000CEF42 File Offset: 0x000CD142
		private static RequestCacheLevel MapLevel(HttpRequestCacheLevel level)
		{
			if (level <= HttpRequestCacheLevel.NoCacheNoStore)
			{
				return (RequestCacheLevel)level;
			}
			if (level == HttpRequestCacheLevel.CacheOrNextCacheOnly)
			{
				return RequestCacheLevel.CacheOnly;
			}
			if (level == HttpRequestCacheLevel.Refresh)
			{
				return RequestCacheLevel.Reload;
			}
			throw new ArgumentOutOfRangeException("level");
		}

		// Token: 0x04002426 RID: 9254
		internal static readonly HttpRequestCachePolicy BypassCache = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);

		// Token: 0x04002427 RID: 9255
		private HttpRequestCacheLevel m_Level;

		// Token: 0x04002428 RID: 9256
		private DateTime m_LastSyncDateUtc = DateTime.MinValue;

		// Token: 0x04002429 RID: 9257
		private TimeSpan m_MaxAge = TimeSpan.MaxValue;

		// Token: 0x0400242A RID: 9258
		private TimeSpan m_MinFresh = TimeSpan.MinValue;

		// Token: 0x0400242B RID: 9259
		private TimeSpan m_MaxStale = TimeSpan.MinValue;
	}
}
