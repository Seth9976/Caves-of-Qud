using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.Diagnostics;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000171 RID: 369
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class DiagnosticDescriptor : IEquatable<DiagnosticDescriptor>
	{
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x0002D21A File Offset: 0x0002B41A
		public string Id { get; }

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0002D222 File Offset: 0x0002B422
		public LocalizableString Title { get; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0002D22A File Offset: 0x0002B42A
		public LocalizableString Description { get; }

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0002D232 File Offset: 0x0002B432
		public string HelpLinkUri { get; }

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0002D23A File Offset: 0x0002B43A
		public LocalizableString MessageFormat { get; }

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0002D242 File Offset: 0x0002B442
		public string Category { get; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0002D24A File Offset: 0x0002B44A
		public DiagnosticSeverity DefaultSeverity { get; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0002D252 File Offset: 0x0002B452
		public bool IsEnabledByDefault { get; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0002D25A File Offset: 0x0002B45A
		public IEnumerable<string> CustomTags { get; }

		// Token: 0x06000F57 RID: 3927 RVA: 0x0002D264 File Offset: 0x0002B464
		public DiagnosticDescriptor(string id, string title, string messageFormat, string category, DiagnosticSeverity defaultSeverity, bool isEnabledByDefault, [Nullable(2)] string description = null, [Nullable(2)] string helpLinkUri = null, params string[] customTags)
			: this(id, title, messageFormat, category, defaultSeverity, isEnabledByDefault, description, helpLinkUri, customTags.AsImmutableOrEmpty<string>())
		{
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002D29C File Offset: 0x0002B49C
		public DiagnosticDescriptor(string id, LocalizableString title, LocalizableString messageFormat, string category, DiagnosticSeverity defaultSeverity, bool isEnabledByDefault, [Nullable(2)] LocalizableString description = null, [Nullable(2)] string helpLinkUri = null, params string[] customTags)
			: this(id, title, messageFormat, category, defaultSeverity, isEnabledByDefault, description, helpLinkUri, customTags.AsImmutableOrEmpty<string>())
		{
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002D2C4 File Offset: 0x0002B4C4
		internal DiagnosticDescriptor(string id, LocalizableString title, LocalizableString messageFormat, string category, DiagnosticSeverity defaultSeverity, bool isEnabledByDefault, [Nullable(2)] LocalizableString description, [Nullable(2)] string helpLinkUri, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> customTags)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				throw new ArgumentException(CodeAnalysisResources.DiagnosticIdCantBeNullOrWhitespace, "id");
			}
			if (messageFormat == null)
			{
				throw new ArgumentNullException("messageFormat");
			}
			if (category == null)
			{
				throw new ArgumentNullException("category");
			}
			if (title == null)
			{
				throw new ArgumentNullException("title");
			}
			this.Id = id;
			this.Title = title;
			this.Category = category;
			this.MessageFormat = messageFormat;
			this.DefaultSeverity = defaultSeverity;
			this.IsEnabledByDefault = isEnabledByDefault;
			this.Description = description ?? string.Empty;
			this.HelpLinkUri = helpLinkUri ?? string.Empty;
			this.CustomTags = customTags;
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002D37C File Offset: 0x0002B57C
		[NullableContext(2)]
		public bool Equals(DiagnosticDescriptor other)
		{
			return this == other || (other != null && this.Category == other.Category && this.DefaultSeverity == other.DefaultSeverity && this.Description.Equals(other.Description) && this.HelpLinkUri == other.HelpLinkUri && this.Id == other.Id && this.IsEnabledByDefault == other.IsEnabledByDefault && this.MessageFormat.Equals(other.MessageFormat) && this.Title.Equals(other.Title));
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002D423 File Offset: 0x0002B623
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DiagnosticDescriptor);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0002D434 File Offset: 0x0002B634
		public override int GetHashCode()
		{
			return Hash.Combine(this.Category.GetHashCode(), Hash.Combine(this.DefaultSeverity.GetHashCode(), Hash.Combine(this.Description.GetHashCode(), Hash.Combine(this.HelpLinkUri.GetHashCode(), Hash.Combine(this.Id.GetHashCode(), Hash.Combine(this.IsEnabledByDefault.GetHashCode(), Hash.Combine(this.MessageFormat.GetHashCode(), this.Title.GetHashCode())))))));
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0002D4C8 File Offset: 0x0002B6C8
		public ReportDiagnostic GetEffectiveSeverity(CompilationOptions compilationOptions)
		{
			if (compilationOptions == null)
			{
				throw new ArgumentNullException("compilationOptions");
			}
			Diagnostic diagnostic = compilationOptions.FilterDiagnostic(Diagnostic.Create(this, Location.None, Array.Empty<object>()), CancellationToken.None);
			if (diagnostic == null)
			{
				return ReportDiagnostic.Suppress;
			}
			return DiagnosticDescriptor.MapSeverityToReport(diagnostic.Severity);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0002D515 File Offset: 0x0002B715
		internal static ReportDiagnostic MapSeverityToReport(DiagnosticSeverity severity)
		{
			switch (severity)
			{
			case DiagnosticSeverity.Hidden:
				return ReportDiagnostic.Hidden;
			case DiagnosticSeverity.Info:
				return ReportDiagnostic.Info;
			case DiagnosticSeverity.Warning:
				return ReportDiagnostic.Warn;
			case DiagnosticSeverity.Error:
				return ReportDiagnostic.Error;
			default:
				throw ExceptionUtilities.UnexpectedValue(severity);
			}
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0002D542 File Offset: 0x0002B742
		internal bool IsNotConfigurable()
		{
			return AnalyzerManager.HasNotConfigurableTag(this.CustomTags);
		}
	}
}
