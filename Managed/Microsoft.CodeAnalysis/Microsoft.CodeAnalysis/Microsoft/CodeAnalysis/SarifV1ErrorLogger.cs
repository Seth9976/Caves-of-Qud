using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000132 RID: 306
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SarifV1ErrorLogger : SarifErrorLogger, IDisposable
	{
		// Token: 0x06000B67 RID: 2919 RVA: 0x000226E8 File Offset: 0x000208E8
		public SarifV1ErrorLogger(Stream stream, string toolName, string toolFileVersion, Version toolAssemblyVersion, CultureInfo culture)
			: base(stream, culture)
		{
			this._descriptors = new SarifV1ErrorLogger.DiagnosticDescriptorSet();
			base._writer.WriteObjectStart();
			base._writer.Write("$schema", "http://json.schemastore.org/sarif-1.0.0");
			base._writer.Write("version", "1.0.0");
			base._writer.WriteArrayStart("runs");
			base._writer.WriteObjectStart();
			base._writer.WriteObjectStart("tool");
			base._writer.Write("name", toolName);
			base._writer.Write("version", toolAssemblyVersion.ToString());
			base._writer.Write("fileVersion", toolFileVersion);
			base._writer.Write("semanticVersion", toolAssemblyVersion.ToString(3));
			base._writer.Write("language", culture.Name);
			base._writer.WriteObjectEnd();
			base._writer.WriteArrayStart("results");
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x000227EC File Offset: 0x000209EC
		protected override string PrimaryLocationPropertyName
		{
			get
			{
				return "resultFile";
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000227F4 File Offset: 0x000209F4
		public override void LogDiagnostic(Diagnostic diagnostic, [Nullable(2)] SuppressionInfo suppressionInfo)
		{
			base._writer.WriteObjectStart();
			base._writer.Write("ruleId", diagnostic.Id);
			string text = this._descriptors.Add(diagnostic.Descriptor);
			if (text != diagnostic.Id)
			{
				base._writer.Write("ruleKey", text);
			}
			base._writer.Write("level", SarifErrorLogger.GetLevel(diagnostic.Severity));
			string message = diagnostic.GetMessage(base._culture);
			if (!RoslynString.IsNullOrEmpty(message))
			{
				base._writer.Write("message", message);
			}
			if (diagnostic.IsSuppressed)
			{
				base._writer.WriteArrayStart("suppressionStates");
				base._writer.Write("suppressedInSource");
				base._writer.WriteArrayEnd();
			}
			this.WriteLocations(diagnostic.Location, diagnostic.AdditionalLocations);
			base.WriteResultProperties(diagnostic);
			base._writer.WriteObjectEnd();
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000228EC File Offset: 0x00020AEC
		private void WriteLocations(Location location, IReadOnlyList<Location> additionalLocations)
		{
			if (SarifErrorLogger.HasPath(location))
			{
				base._writer.WriteArrayStart("locations");
				base._writer.WriteObjectStart();
				base._writer.WriteKey(this.PrimaryLocationPropertyName);
				this.WritePhysicalLocation(location);
				base._writer.WriteObjectEnd();
				base._writer.WriteArrayEnd();
			}
			if (additionalLocations != null && additionalLocations.Count > 0)
			{
				if (Enumerable.Any<Location>(additionalLocations, (Location l) => SarifErrorLogger.HasPath(l)))
				{
					base._writer.WriteArrayStart("relatedLocations");
					foreach (Location location2 in additionalLocations)
					{
						if (SarifErrorLogger.HasPath(location2))
						{
							base._writer.WriteObjectStart();
							base._writer.WriteKey("physicalLocation");
							this.WritePhysicalLocation(location2);
							base._writer.WriteObjectEnd();
						}
					}
					base._writer.WriteArrayEnd();
				}
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00022A08 File Offset: 0x00020C08
		protected override void WritePhysicalLocation(Location location)
		{
			FileLinePositionSpan lineSpan = location.GetLineSpan();
			base._writer.WriteObjectStart();
			base._writer.Write("uri", SarifErrorLogger.GetUri(lineSpan.Path));
			base.WriteRegion(lineSpan);
			base._writer.WriteObjectEnd();
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00022A58 File Offset: 0x00020C58
		private void WriteRules()
		{
			if (this._descriptors.Count > 0)
			{
				base._writer.WriteObjectStart("rules");
				foreach (KeyValuePair<string, DiagnosticDescriptor> keyValuePair in this._descriptors.ToSortedList())
				{
					DiagnosticDescriptor value = keyValuePair.Value;
					base._writer.WriteObjectStart(keyValuePair.Key);
					base._writer.Write("id", value.Id);
					string text = value.Title.ToString(base._culture);
					if (!RoslynString.IsNullOrEmpty(text))
					{
						base._writer.Write("shortDescription", text);
					}
					string text2 = value.Description.ToString(base._culture);
					if (!RoslynString.IsNullOrEmpty(text2))
					{
						base._writer.Write("fullDescription", text2);
					}
					base._writer.Write("defaultLevel", SarifErrorLogger.GetLevel(value.DefaultSeverity));
					if (!string.IsNullOrEmpty(value.HelpLinkUri))
					{
						base._writer.Write("helpUri", value.HelpLinkUri);
					}
					base._writer.WriteObjectStart("properties");
					if (!string.IsNullOrEmpty(value.Category))
					{
						base._writer.Write("category", value.Category);
					}
					base._writer.Write("isEnabledByDefault", value.IsEnabledByDefault);
					if (Enumerable.Any<string>(value.CustomTags))
					{
						base._writer.WriteArrayStart("tags");
						foreach (string text3 in value.CustomTags)
						{
							base._writer.Write(text3);
						}
						base._writer.WriteArrayEnd();
					}
					base._writer.WriteObjectEnd();
					base._writer.WriteObjectEnd();
				}
				base._writer.WriteObjectEnd();
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00022C8C File Offset: 0x00020E8C
		public override void Dispose()
		{
			base._writer.WriteArrayEnd();
			this.WriteRules();
			base._writer.WriteObjectEnd();
			base._writer.WriteArrayEnd();
			base._writer.WriteObjectEnd();
			base.Dispose();
		}

		// Token: 0x04000338 RID: 824
		private readonly SarifV1ErrorLogger.DiagnosticDescriptorSet _descriptors;

		// Token: 0x02000579 RID: 1401
		[Nullable(0)]
		private sealed class DiagnosticDescriptorSet
		{
			// Token: 0x17000EF7 RID: 3831
			// (get) Token: 0x060034B7 RID: 13495 RVA: 0x00094F9B File Offset: 0x0009319B
			public int Count
			{
				get
				{
					return this._keys.Count;
				}
			}

			// Token: 0x060034B8 RID: 13496 RVA: 0x00094FA8 File Offset: 0x000931A8
			public string Add(DiagnosticDescriptor descriptor)
			{
				string text;
				if (this._keys.TryGetValue(descriptor, ref text))
				{
					return text;
				}
				int num;
				if (!this._counters.TryGetValue(descriptor.Id, ref num))
				{
					this._counters.Add(descriptor.Id, 0);
					this._keys.Add(descriptor, descriptor.Id);
					return descriptor.Id;
				}
				do
				{
					num = (this._counters[descriptor.Id] = num + 1);
					text = descriptor.Id + "-" + num.ToString("000", CultureInfo.InvariantCulture);
				}
				while (this._counters.ContainsKey(text));
				this._keys.Add(descriptor, text);
				return text;
			}

			// Token: 0x060034B9 RID: 13497 RVA: 0x0009505C File Offset: 0x0009325C
			[return: Nullable(new byte[] { 1, 0, 1, 1 })]
			public List<KeyValuePair<string, DiagnosticDescriptor>> ToSortedList()
			{
				List<KeyValuePair<string, DiagnosticDescriptor>> list = new List<KeyValuePair<string, DiagnosticDescriptor>>(this.Count);
				foreach (KeyValuePair<DiagnosticDescriptor, string> keyValuePair in this._keys)
				{
					list.Add(new KeyValuePair<string, DiagnosticDescriptor>(keyValuePair.Value, keyValuePair.Key));
				}
				list.Sort(([Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<string, DiagnosticDescriptor> x, [Nullable(new byte[] { 0, 1, 1 })] KeyValuePair<string, DiagnosticDescriptor> y) => string.CompareOrdinal(x.Key, y.Key));
				return list;
			}

			// Token: 0x040015CA RID: 5578
			private readonly Dictionary<string, int> _counters = new Dictionary<string, int>();

			// Token: 0x040015CB RID: 5579
			private readonly Dictionary<DiagnosticDescriptor, string> _keys = new Dictionary<DiagnosticDescriptor, string>(SarifDiagnosticComparer.Instance);
		}
	}
}
