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
	// Token: 0x02000133 RID: 307
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SarifV2ErrorLogger : SarifErrorLogger, IDisposable
	{
		// Token: 0x06000B6E RID: 2926 RVA: 0x00022CC8 File Offset: 0x00020EC8
		public SarifV2ErrorLogger(Stream stream, string toolName, string toolFileVersion, Version toolAssemblyVersion, CultureInfo culture)
			: base(stream, culture)
		{
			this._descriptors = new SarifV2ErrorLogger.DiagnosticDescriptorSet();
			this._toolName = toolName;
			this._toolFileVersion = toolFileVersion;
			this._toolAssemblyVersion = toolAssemblyVersion;
			base._writer.WriteObjectStart();
			base._writer.Write("$schema", "http://json.schemastore.org/sarif-2.1.0");
			base._writer.Write("version", "2.1.0");
			base._writer.WriteArrayStart("runs");
			base._writer.WriteObjectStart();
			base._writer.WriteArrayStart("results");
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00022D5F File Offset: 0x00020F5F
		protected override string PrimaryLocationPropertyName
		{
			get
			{
				return "physicalLocation";
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00022D68 File Offset: 0x00020F68
		public override void LogDiagnostic(Diagnostic diagnostic, [Nullable(2)] SuppressionInfo suppressionInfo)
		{
			base._writer.WriteObjectStart();
			base._writer.Write("ruleId", diagnostic.Id);
			int num = this._descriptors.Add(diagnostic.Descriptor);
			base._writer.Write("ruleIndex", num);
			base._writer.Write("level", SarifErrorLogger.GetLevel(diagnostic.Severity));
			string message = diagnostic.GetMessage(base._culture);
			if (!RoslynString.IsNullOrEmpty(message))
			{
				base._writer.WriteObjectStart("message");
				base._writer.Write("text", message);
				base._writer.WriteObjectEnd();
			}
			if (diagnostic.IsSuppressed)
			{
				base._writer.WriteArrayStart("suppressions");
				base._writer.WriteObjectStart();
				base._writer.Write("kind", "inSource");
				string text;
				if (suppressionInfo == null)
				{
					text = null;
				}
				else
				{
					AttributeData attribute = suppressionInfo.Attribute;
					text = ((attribute != null) ? attribute.DecodeNamedArgument<string>("Justification", SpecialType.System_String, null) : null);
				}
				string text2 = text;
				if (text2 != null)
				{
					base._writer.Write("justification", text2);
				}
				base._writer.WriteObjectEnd();
				base._writer.WriteArrayEnd();
			}
			this.WriteLocations(diagnostic.Location, diagnostic.AdditionalLocations);
			base.WriteResultProperties(diagnostic);
			base._writer.WriteObjectEnd();
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00022EBC File Offset: 0x000210BC
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

		// Token: 0x06000B72 RID: 2930 RVA: 0x00022FD8 File Offset: 0x000211D8
		protected override void WritePhysicalLocation(Location diagnosticLocation)
		{
			FileLinePositionSpan lineSpan = diagnosticLocation.GetLineSpan();
			base._writer.WriteObjectStart();
			base._writer.WriteObjectStart("artifactLocation");
			base._writer.Write("uri", SarifErrorLogger.GetUri(lineSpan.Path));
			base._writer.WriteObjectEnd();
			base.WriteRegion(lineSpan);
			base._writer.WriteObjectEnd();
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00023040 File Offset: 0x00021240
		public override void Dispose()
		{
			base._writer.WriteArrayEnd();
			this.WriteTool();
			base._writer.Write("columnKind", "utf16CodeUnits");
			base._writer.WriteObjectEnd();
			base._writer.WriteArrayEnd();
			base._writer.WriteObjectEnd();
			base.Dispose();
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0002309C File Offset: 0x0002129C
		private void WriteTool()
		{
			base._writer.WriteObjectStart("tool");
			base._writer.WriteObjectStart("driver");
			base._writer.Write("name", this._toolName);
			base._writer.Write("version", this._toolFileVersion);
			base._writer.Write("dottedQuadFileVersion", this._toolAssemblyVersion.ToString());
			base._writer.Write("semanticVersion", this._toolAssemblyVersion.ToString(3));
			base._writer.Write("language", base._culture.Name);
			this.WriteRules();
			base._writer.WriteObjectEnd();
			base._writer.WriteObjectEnd();
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00023164 File Offset: 0x00021364
		private void WriteRules()
		{
			if (this._descriptors.Count > 0)
			{
				base._writer.WriteArrayStart("rules");
				foreach (KeyValuePair<int, DiagnosticDescriptor> keyValuePair in this._descriptors.ToSortedList())
				{
					DiagnosticDescriptor value = keyValuePair.Value;
					base._writer.WriteObjectStart();
					base._writer.Write("id", value.Id);
					string text = value.Title.ToString(base._culture);
					if (!RoslynString.IsNullOrEmpty(text))
					{
						base._writer.WriteObjectStart("shortDescription");
						base._writer.Write("text", text);
						base._writer.WriteObjectEnd();
					}
					string text2 = value.Description.ToString(base._culture);
					if (!RoslynString.IsNullOrEmpty(text2))
					{
						base._writer.WriteObjectStart("fullDescription");
						base._writer.Write("text", text2);
						base._writer.WriteObjectEnd();
					}
					this.WriteDefaultConfiguration(value);
					if (!string.IsNullOrEmpty(value.HelpLinkUri))
					{
						base._writer.Write("helpUri", value.HelpLinkUri);
					}
					if (!string.IsNullOrEmpty(value.Category) || Enumerable.Any<string>(value.CustomTags))
					{
						base._writer.WriteObjectStart("properties");
						if (!string.IsNullOrEmpty(value.Category))
						{
							base._writer.Write("category", value.Category);
						}
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
					}
					base._writer.WriteObjectEnd();
				}
				base._writer.WriteArrayEnd();
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000233B8 File Offset: 0x000215B8
		private void WriteDefaultConfiguration(DiagnosticDescriptor descriptor)
		{
			string level = SarifErrorLogger.GetLevel(descriptor.DefaultSeverity);
			bool flag = level != "warning";
			bool flag2 = !descriptor.IsEnabledByDefault;
			if (flag || flag2)
			{
				base._writer.WriteObjectStart("defaultConfiguration");
				if (flag)
				{
					base._writer.Write("level", level);
				}
				if (flag2)
				{
					base._writer.Write("enabled", descriptor.IsEnabledByDefault);
				}
				base._writer.WriteObjectEnd();
			}
		}

		// Token: 0x04000339 RID: 825
		private readonly SarifV2ErrorLogger.DiagnosticDescriptorSet _descriptors;

		// Token: 0x0400033A RID: 826
		private readonly string _toolName;

		// Token: 0x0400033B RID: 827
		private readonly string _toolFileVersion;

		// Token: 0x0400033C RID: 828
		private readonly Version _toolAssemblyVersion;

		// Token: 0x0200057B RID: 1403
		[Nullable(0)]
		private sealed class DiagnosticDescriptorSet
		{
			// Token: 0x17000EF8 RID: 3832
			// (get) Token: 0x060034BE RID: 13502 RVA: 0x00095133 File Offset: 0x00093333
			public int Count
			{
				get
				{
					return this._distinctDescriptors.Count;
				}
			}

			// Token: 0x060034BF RID: 13503 RVA: 0x00095140 File Offset: 0x00093340
			public int Add(DiagnosticDescriptor descriptor)
			{
				int num;
				if (this._distinctDescriptors.TryGetValue(descriptor, ref num))
				{
					return num;
				}
				this._distinctDescriptors.Add(descriptor, this.Count);
				return this.Count - 1;
			}

			// Token: 0x060034C0 RID: 13504 RVA: 0x0009517C File Offset: 0x0009337C
			[return: Nullable(new byte[] { 1, 0, 1 })]
			public List<KeyValuePair<int, DiagnosticDescriptor>> ToSortedList()
			{
				List<KeyValuePair<int, DiagnosticDescriptor>> list = new List<KeyValuePair<int, DiagnosticDescriptor>>(this.Count);
				foreach (KeyValuePair<DiagnosticDescriptor, int> keyValuePair in this._distinctDescriptors)
				{
					list.Add(new KeyValuePair<int, DiagnosticDescriptor>(keyValuePair.Value, keyValuePair.Key));
				}
				list.Sort(([Nullable(new byte[] { 0, 1 })] KeyValuePair<int, DiagnosticDescriptor> x, [Nullable(new byte[] { 0, 1 })] KeyValuePair<int, DiagnosticDescriptor> y) => x.Key.CompareTo(y.Key));
				return list;
			}

			// Token: 0x040015CE RID: 5582
			private readonly Dictionary<DiagnosticDescriptor, int> _distinctDescriptors = new Dictionary<DiagnosticDescriptor, int>(SarifDiagnosticComparer.Instance);
		}
	}
}
