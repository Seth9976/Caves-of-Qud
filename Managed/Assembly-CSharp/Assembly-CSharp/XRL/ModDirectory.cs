using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using XRL.UI;

namespace XRL
{
	// Token: 0x0200051D RID: 1309
	public class ModDirectory
	{
		/// <summary>A single value initializer for <see cref="F:XRL.ModDirectory.Dependencies" />.</summary>
		// Token: 0x170005F8 RID: 1528
		// (set) Token: 0x06003817 RID: 14359 RVA: 0x00111D8D File Offset: 0x0010FF8D
		[JsonProperty]
		private string Dependency
		{
			set
			{
				this.Dependencies = new Dictionary<string, string> { { value, "*" } };
			}
		}

		/// <summary>A single value initializer for <see cref="F:XRL.ModDirectory.Exclusions" />.</summary>
		// Token: 0x170005F9 RID: 1529
		// (set) Token: 0x06003818 RID: 14360 RVA: 0x00111DA6 File Offset: 0x0010FFA6
		[JsonProperty]
		private string Exclusion
		{
			set
			{
				this.Exclusions = new Dictionary<string, string> { { value, "*" } };
			}
		}

		/// <summary>A single value initializer for <see cref="F:XRL.ModDirectory.Paths" />.</summary>
		// Token: 0x170005FA RID: 1530
		// (set) Token: 0x06003819 RID: 14361 RVA: 0x00111DBF File Offset: 0x0010FFBF
		[JsonProperty]
		private string Path
		{
			set
			{
				this.Paths = new string[] { value };
			}
		}

		/// <inheritdoc cref="F:XRL.ModDirectory.Options" />
		// Token: 0x170005FB RID: 1531
		// (set) Token: 0x0600381A RID: 14362 RVA: 0x00111DD1 File Offset: 0x0010FFD1
		[JsonProperty]
		private GameOption.RequiresSpec Option
		{
			set
			{
				this.Options = value;
			}
		}

		/// <summary>The paths to include in mod loading if this directory is valid.</summary>
		/// <remarks>Loading is recursive and subsumes any subdirectories, no paths will be loaded twice even if duplicated across directories.</remarks>
		// Token: 0x04001C1C RID: 7196
		public string[] Paths;

		/// <summary>A semantic version range that is required to match <see cref="F:XRL.XRLGame.MarketingVersion" />.</summary>
		/// <seealso cref="M:XRL.Version.EqualsSemantic(System.ReadOnlySpan{System.Char})" />
		// Token: 0x04001C1D RID: 7197
		public string Version;

		/// <summary>A semantic version range that is required to match <see cref="F:XRL.XRLGame.CoreVersion" />.</summary>
		/// <seealso cref="M:XRL.Version.EqualsSemantic(System.ReadOnlySpan{System.Char})" />
		// Token: 0x04001C1E RID: 7198
		public string Build;

		/// <summary>A set of required option state.</summary>
		/// <remarks>
		/// The XML file containing the referenced options is required to have "Option" somewhere in its file name
		/// in order for its option defaults to populate prior to directory resolution. 
		/// </remarks>
		// Token: 0x04001C1F RID: 7199
		public GameOption.RequiresSpec Options;

		/// <summary>A map of required dependencies with semantic version ranges.</summary>
		/// <seealso cref="M:XRL.Version.EqualsSemantic(System.ReadOnlySpan{System.Char})" />
		// Token: 0x04001C20 RID: 7200
		public Dictionary<string, string> Dependencies;

		/// <summary>A map of exlusions with semantic version ranges.</summary>
		/// <seealso cref="M:XRL.Version.EqualsSemantic(System.ReadOnlySpan{System.Char})" />
		// Token: 0x04001C21 RID: 7201
		public Dictionary<string, string> Exclusions;
	}
}
