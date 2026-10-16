using System;

namespace System.Web.UI
{
	/// <summary>Encapsulates the output cache initialization settings parsed from an @ OutputCache page directive by ASP.NET. This class cannot be inherited.</summary>
	// Token: 0x02000206 RID: 518
	public sealed class OutputCacheParameters
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.OutputCacheParameters" /> class. </summary>
		// Token: 0x0600142D RID: 5165 RVA: 0x0003448A File Offset: 0x0003268A
		public OutputCacheParameters()
		{
			this.Duration = 0;
			this.Enabled = true;
			this.Location = OutputCacheLocation.Any;
			this.NoStore = false;
		}

		/// <summary>Gets or sets an <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name that is associated with the settings of the output cache entry.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name that is associated with the settings of the output cache entry.</returns>
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x000344AE File Offset: 0x000326AE
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x000344B6 File Offset: 0x000326B6
		public string CacheProfile
		{
			get
			{
				return this._cacheProfile;
			}
			set
			{
				this._cacheProfile = value;
			}
		}

		/// <summary>Gets or sets the amount of time that a cache entry is to remain in the output cache.</summary>
		/// <returns>The amount of time, in seconds, that a cache entry is to remain in the output cache. The default is 0, which indicates an infinite duration.</returns>
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x000344BF File Offset: 0x000326BF
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x000344C7 File Offset: 0x000326C7
		public int Duration
		{
			get
			{
				return this._duration;
			}
			set
			{
				this._duration = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether output caching is enabled for the current content.</summary>
		/// <returns>true if output caching is enabled for the current content; otherwise, false. The default is true.</returns>
		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x000344D0 File Offset: 0x000326D0
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x000344D8 File Offset: 0x000326D8
		public bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		/// <summary>Gets or sets a value that determines the location of the cache entry.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> values.</returns>
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x000344E1 File Offset: 0x000326E1
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x000344E9 File Offset: 0x000326E9
		public OutputCacheLocation Location
		{
			get
			{
				return this._location;
			}
			set
			{
				this._location = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether the HTTP Cache-Control: no-store directive is set.</summary>
		/// <returns>true if the Cache-Control: no-store directive is set on <see cref="T:System.Web.HttpResponse" />; otherwise, false. The default is false.</returns>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x000344F2 File Offset: 0x000326F2
		// (set) Token: 0x06001437 RID: 5175 RVA: 0x000344FA File Offset: 0x000326FA
		public bool NoStore
		{
			get
			{
				return this._noStore;
			}
			set
			{
				this._noStore = value;
			}
		}

		/// <summary>Gets or sets a set of database and table name pairs that the cache entry depends on.</summary>
		/// <returns>A string that identifies a set of database and table name pairs that the cache entry depends on. The cache entry is expired when the table's data is updated or changes.</returns>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x00034503 File Offset: 0x00032703
		// (set) Token: 0x06001439 RID: 5177 RVA: 0x0003450B File Offset: 0x0003270B
		public string SqlDependency
		{
			get
			{
				return this._sqlDependency;
			}
			set
			{
				this._sqlDependency = value;
			}
		}

		/// <summary>Gets or sets a comma-delimited set of character sets (content encodings) used to vary the cache entry.</summary>
		/// <returns>A list of character sets by which to vary the content.</returns>
		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x00034514 File Offset: 0x00032714
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x0003451C File Offset: 0x0003271C
		public string VaryByContentEncoding
		{
			get
			{
				return this._varyByContentEncoding;
			}
			set
			{
				this._varyByContentEncoding = value;
			}
		}

		/// <summary>Gets or sets a semicolon-delimited set of control identifiers contained within the current page or user control used to vary the current cache entry.</summary>
		/// <returns>A semicolon-separated list of strings used to vary an entry's output cache. The <see cref="P:System.Web.UI.OutputCacheParameters.VaryByControl" /> property is set to fully qualified control identifiers, where the identifier is a concatenation of control IDs starting from the top-level parent control and delimited with a dollar sign ($) character.</returns>
		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00034525 File Offset: 0x00032725
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x0003452D File Offset: 0x0003272D
		public string VaryByControl
		{
			get
			{
				return this._varByControl;
			}
			set
			{
				this._varByControl = value;
			}
		}

		/// <summary>Gets a list of custom strings that the output cache uses to vary the cache entry.</summary>
		/// <returns>The list of custom strings.</returns>
		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00034536 File Offset: 0x00032736
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x0003453E File Offset: 0x0003273E
		public string VaryByCustom
		{
			get
			{
				return this._varByCustom;
			}
			set
			{
				this._varByCustom = value;
			}
		}

		/// <summary>Gets or sets a comma-delimited set of header names used to vary the cache entry. The header names identify HTTP headers associated with the request.</summary>
		/// <returns>A list of headers by which to vary the content.</returns>
		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00034547 File Offset: 0x00032747
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x0003454F File Offset: 0x0003274F
		public string VaryByHeader
		{
			get
			{
				return this._varByHeader;
			}
			set
			{
				this._varByHeader = value;
			}
		}

		/// <summary>Gets a semicolon-delimited list of query string or form POST parameters that the output cache uses to vary the cache entry.</summary>
		/// <returns>The list of query string or form POST parameters.</returns>
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00034558 File Offset: 0x00032758
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00034560 File Offset: 0x00032760
		public string VaryByParam
		{
			get
			{
				return this._varByParam;
			}
			set
			{
				this._varByParam = value;
			}
		}

		// Token: 0x04001497 RID: 5271
		private string _cacheProfile;

		// Token: 0x04001498 RID: 5272
		private int _duration;

		// Token: 0x04001499 RID: 5273
		private bool _enabled;

		// Token: 0x0400149A RID: 5274
		private OutputCacheLocation _location;

		// Token: 0x0400149B RID: 5275
		private bool _noStore;

		// Token: 0x0400149C RID: 5276
		private string _sqlDependency;

		// Token: 0x0400149D RID: 5277
		private string _varByControl;

		// Token: 0x0400149E RID: 5278
		private string _varByCustom;

		// Token: 0x0400149F RID: 5279
		private string _varByHeader;

		// Token: 0x040014A0 RID: 5280
		private string _varByParam;

		// Token: 0x040014A1 RID: 5281
		private string _varyByContentEncoding;
	}
}
