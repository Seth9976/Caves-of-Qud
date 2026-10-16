using System;

namespace System.CodeDom
{
	/// <summary>Specifies the name and mode for a code region.</summary>
	// Token: 0x02000329 RID: 809
	[Serializable]
	public class CodeRegionDirective : CodeDirective
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class with default values. </summary>
		// Token: 0x060019B6 RID: 6582 RVA: 0x0005F608 File Offset: 0x0005D808
		public CodeRegionDirective()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeRegionDirective" /> class, specifying its mode and name. </summary>
		/// <param name="regionMode">One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values.</param>
		/// <param name="regionText">The name for the region.</param>
		// Token: 0x060019B7 RID: 6583 RVA: 0x00060BDD File Offset: 0x0005EDDD
		public CodeRegionDirective(CodeRegionMode regionMode, string regionText)
		{
			this.RegionText = regionText;
			this.RegionMode = regionMode;
		}

		/// <summary>Gets or sets the name of the region.</summary>
		/// <returns>The name of the region.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x00060BF3 File Offset: 0x0005EDF3
		// (set) Token: 0x060019B9 RID: 6585 RVA: 0x00060C04 File Offset: 0x0005EE04
		public string RegionText
		{
			get
			{
				return this._regionText ?? string.Empty;
			}
			set
			{
				this._regionText = value;
			}
		}

		/// <summary>Gets or sets the mode for the region directive.</summary>
		/// <returns>One of the <see cref="T:System.CodeDom.CodeRegionMode" /> values. The default is <see cref="F:System.CodeDom.CodeRegionMode.None" />.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x00060C0D File Offset: 0x0005EE0D
		// (set) Token: 0x060019BB RID: 6587 RVA: 0x00060C15 File Offset: 0x0005EE15
		public CodeRegionMode RegionMode { get; set; }

		// Token: 0x04000DF0 RID: 3568
		private string _regionText;
	}
}
