using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200016E RID: 366
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	public abstract class Diagnostic : IEquatable<Diagnostic>, IFormattable
	{
		// Token: 0x06000F03 RID: 3843 RVA: 0x0002C9E4 File Offset: 0x0002ABE4
		public static Diagnostic Create(DiagnosticDescriptor descriptor, [Nullable(2)] Location location, [Nullable(2)] params object[] messageArgs)
		{
			return Diagnostic.Create(descriptor, location, null, null, messageArgs);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
		public static Diagnostic Create(DiagnosticDescriptor descriptor, [Nullable(2)] Location location, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties, [Nullable(2)] params object[] messageArgs)
		{
			return Diagnostic.Create(descriptor, location, null, properties, messageArgs);
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		public static Diagnostic Create(DiagnosticDescriptor descriptor, [Nullable(2)] Location location, [Nullable(new byte[] { 2, 1 })] IEnumerable<Location> additionalLocations, [Nullable(2)] params object[] messageArgs)
		{
			return Diagnostic.Create(descriptor, location, additionalLocations, null, messageArgs);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0002CA08 File Offset: 0x0002AC08
		public static Diagnostic Create(DiagnosticDescriptor descriptor, [Nullable(2)] Location location, [Nullable(new byte[] { 2, 1 })] IEnumerable<Location> additionalLocations, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties, [Nullable(2)] params object[] messageArgs)
		{
			return Diagnostic.Create(descriptor, location, descriptor.DefaultSeverity, additionalLocations, properties, messageArgs);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0002CA1C File Offset: 0x0002AC1C
		public static Diagnostic Create(DiagnosticDescriptor descriptor, [Nullable(2)] Location location, DiagnosticSeverity effectiveSeverity, [Nullable(new byte[] { 2, 1 })] IEnumerable<Location> additionalLocations, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties, [Nullable(2)] params object[] messageArgs)
		{
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			int defaultWarningLevel = Diagnostic.GetDefaultWarningLevel(effectiveSeverity);
			return Diagnostic.SimpleDiagnostic.Create(descriptor, effectiveSeverity, defaultWarningLevel, location ?? Location.None, additionalLocations, messageArgs, properties, false);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0002CA58 File Offset: 0x0002AC58
		public static Diagnostic Create(string id, string category, LocalizableString message, DiagnosticSeverity severity, DiagnosticSeverity defaultSeverity, bool isEnabledByDefault, int warningLevel, [Nullable(2)] LocalizableString title = null, [Nullable(2)] LocalizableString description = null, [Nullable(2)] string helpLink = null, [Nullable(2)] Location location = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<Location> additionalLocations = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> customTags = null, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties = null)
		{
			return Diagnostic.Create(id, category, message, severity, defaultSeverity, isEnabledByDefault, warningLevel, false, title, description, helpLink, location, additionalLocations, customTags, properties);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0002CA84 File Offset: 0x0002AC84
		public static Diagnostic Create(string id, string category, LocalizableString message, DiagnosticSeverity severity, DiagnosticSeverity defaultSeverity, bool isEnabledByDefault, int warningLevel, bool isSuppressed, [Nullable(2)] LocalizableString title = null, [Nullable(2)] LocalizableString description = null, [Nullable(2)] string helpLink = null, [Nullable(2)] Location location = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<Location> additionalLocations = null, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> customTags = null, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties = null)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (category == null)
			{
				throw new ArgumentNullException("category");
			}
			if (message == null)
			{
				throw new ArgumentNullException("message");
			}
			return Diagnostic.SimpleDiagnostic.Create(id, title ?? string.Empty, category, message, description ?? string.Empty, helpLink ?? string.Empty, severity, defaultSeverity, isEnabledByDefault, warningLevel, location ?? Location.None, additionalLocations, customTags, properties, isSuppressed);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002CB08 File Offset: 0x0002AD08
		internal static Diagnostic Create(CommonMessageProvider messageProvider, int errorCode)
		{
			return Diagnostic.Create(new DiagnosticInfo(messageProvider, errorCode));
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002CB16 File Offset: 0x0002AD16
		internal static Diagnostic Create(CommonMessageProvider messageProvider, int errorCode, params object[] arguments)
		{
			return Diagnostic.Create(new DiagnosticInfo(messageProvider, errorCode, arguments));
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0002CB25 File Offset: 0x0002AD25
		internal static Diagnostic Create(DiagnosticInfo info)
		{
			return new DiagnosticWithInfo(info, Location.None, false);
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000F0D RID: 3853
		public abstract DiagnosticDescriptor Descriptor { get; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000F0E RID: 3854
		public abstract string Id { get; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0002CB33 File Offset: 0x0002AD33
		internal virtual string Category
		{
			get
			{
				return this.Descriptor.Category;
			}
		}

		// Token: 0x06000F10 RID: 3856
		public abstract string GetMessage([Nullable(2)] IFormatProvider formatProvider = null);

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x0002CB40 File Offset: 0x0002AD40
		public virtual DiagnosticSeverity DefaultSeverity
		{
			get
			{
				return this.Descriptor.DefaultSeverity;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000F12 RID: 3858
		public abstract DiagnosticSeverity Severity { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000F13 RID: 3859
		public abstract int WarningLevel { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000F14 RID: 3860
		public abstract bool IsSuppressed { get; }

		// Token: 0x06000F15 RID: 3861 RVA: 0x0002CB50 File Offset: 0x0002AD50
		[return: Nullable(2)]
		public SuppressionInfo GetSuppressionInfo(Compilation compilation)
		{
			if (!this.IsSuppressed)
			{
				return null;
			}
			AttributeData attributeData;
			if (!new SuppressMessageAttributeState(compilation).IsDiagnosticSuppressed(this, out attributeData))
			{
				attributeData = null;
			}
			return new SuppressionInfo(this.Id, attributeData);
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0002CB85 File Offset: 0x0002AD85
		internal virtual bool IsEnabledByDefault
		{
			get
			{
				return this.Descriptor.IsEnabledByDefault;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0002CB92 File Offset: 0x0002AD92
		public bool IsWarningAsError
		{
			get
			{
				return this.DefaultSeverity == DiagnosticSeverity.Warning && this.Severity == DiagnosticSeverity.Error;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000F18 RID: 3864
		public abstract Location Location { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000F19 RID: 3865
		public abstract IReadOnlyList<Location> AdditionalLocations { get; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0002CBA8 File Offset: 0x0002ADA8
		internal virtual IReadOnlyList<string> CustomTags
		{
			get
			{
				return (IReadOnlyList<string>)this.Descriptor.CustomTags;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x0002CBBA File Offset: 0x0002ADBA
		[Nullable(new byte[] { 1, 1, 2 })]
		public virtual ImmutableDictionary<string, string> Properties
		{
			[return: Nullable(new byte[] { 1, 1, 2 })]
			get
			{
				return ImmutableDictionary<string, string>.Empty;
			}
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0002CBC1 File Offset: 0x0002ADC1
		[NullableContext(2)]
		[return: Nullable(1)]
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return DiagnosticFormatter.Instance.Format(this, formatProvider);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0002CBCF File Offset: 0x0002ADCF
		public override string ToString()
		{
			return DiagnosticFormatter.Instance.Format(this, CultureInfo.CurrentUICulture);
		}

		// Token: 0x06000F1E RID: 3870
		[NullableContext(2)]
		public abstract override bool Equals(object obj);

		// Token: 0x06000F1F RID: 3871
		public abstract override int GetHashCode();

		// Token: 0x06000F20 RID: 3872
		[NullableContext(2)]
		public abstract bool Equals(Diagnostic obj);

		// Token: 0x06000F21 RID: 3873 RVA: 0x0002CBE4 File Offset: 0x0002ADE4
		private string GetDebuggerDisplay()
		{
			DiagnosticSeverity severity = this.Severity;
			if (severity == (DiagnosticSeverity)(-2))
			{
				string text = "Void diagnostic at ";
				Location location = this.Location;
				return text + ((location != null) ? location.ToString() : null);
			}
			if (severity == (DiagnosticSeverity)(-1))
			{
				string text2 = "Unresolved diagnostic at ";
				Location location2 = this.Location;
				return text2 + ((location2 != null) ? location2.ToString() : null);
			}
			return this.ToString();
		}

		// Token: 0x06000F22 RID: 3874
		internal abstract Diagnostic WithLocation(Location location);

		// Token: 0x06000F23 RID: 3875
		internal abstract Diagnostic WithSeverity(DiagnosticSeverity severity);

		// Token: 0x06000F24 RID: 3876
		internal abstract Diagnostic WithIsSuppressed(bool isSuppressed);

		// Token: 0x06000F25 RID: 3877 RVA: 0x0002CC41 File Offset: 0x0002AE41
		internal Diagnostic WithProgrammaticSuppression(ProgrammaticSuppressionInfo programmaticSuppressionInfo)
		{
			return new Diagnostic.DiagnosticWithProgrammaticSuppression(this, programmaticSuppressionInfo);
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0002CC4A File Offset: 0x0002AE4A
		[Nullable(2)]
		internal virtual ProgrammaticSuppressionInfo ProgrammaticSuppressionInfo
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0002CC4D File Offset: 0x0002AE4D
		internal virtual int Code
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0002CC50 File Offset: 0x0002AE50
		[Nullable(new byte[] { 1, 2 })]
		internal virtual IReadOnlyList<object> Arguments
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				return SpecializedCollections.EmptyReadOnlyList<object>();
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0002CC58 File Offset: 0x0002AE58
		internal bool HasIntersectingLocation(SyntaxTree tree, TextSpan? filterSpanWithinTree = null)
		{
			if (Diagnostic.<HasIntersectingLocation>g__isLocationWithinSpan|58_0(this.Location, tree, filterSpanWithinTree))
			{
				return true;
			}
			if (this.AdditionalLocations == null || this.AdditionalLocations.Count == 0)
			{
				return false;
			}
			using (IEnumerator<Location> enumerator = this.AdditionalLocations.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (Diagnostic.<HasIntersectingLocation>g__isLocationWithinSpan|58_0(enumerator.Current, tree, filterSpanWithinTree))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0002CCD8 File Offset: 0x0002AED8
		[NullableContext(2)]
		internal Diagnostic WithReportDiagnostic(ReportDiagnostic reportAction)
		{
			switch (reportAction)
			{
			case ReportDiagnostic.Default:
				return this;
			case ReportDiagnostic.Error:
				return this.WithSeverity(DiagnosticSeverity.Error);
			case ReportDiagnostic.Warn:
				return this.WithSeverity(DiagnosticSeverity.Warning);
			case ReportDiagnostic.Info:
				return this.WithSeverity(DiagnosticSeverity.Info);
			case ReportDiagnostic.Hidden:
				return this.WithSeverity(DiagnosticSeverity.Hidden);
			case ReportDiagnostic.Suppress:
				return null;
			default:
				throw ExceptionUtilities.UnexpectedValue(reportAction);
			}
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0002CD34 File Offset: 0x0002AF34
		internal static int GetDefaultWarningLevel(DiagnosticSeverity severity)
		{
			if (severity != DiagnosticSeverity.Warning && severity == DiagnosticSeverity.Error)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0002CD41 File Offset: 0x0002AF41
		internal virtual bool IsNotConfigurable()
		{
			return AnalyzerManager.HasNotConfigurableTag(this.CustomTags);
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0002CD4E File Offset: 0x0002AF4E
		internal bool IsUnsuppressableError()
		{
			return this.DefaultSeverity == DiagnosticSeverity.Error && this.IsNotConfigurable();
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0002CD61 File Offset: 0x0002AF61
		internal bool IsUnsuppressedError
		{
			get
			{
				return this.Severity == DiagnosticSeverity.Error && !this.IsSuppressed;
			}
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x0002CD80 File Offset: 0x0002AF80
		[CompilerGenerated]
		internal static bool <HasIntersectingLocation>g__isLocationWithinSpan|58_0(Location location, SyntaxTree tree, TextSpan? filterSpan)
		{
			return location.SourceTree == tree && (filterSpan == null || filterSpan.GetValueOrDefault().IntersectsWith(location.SourceSpan));
		}

		// Token: 0x04000427 RID: 1063
		internal const string CompilerDiagnosticCategory = "Compiler";

		// Token: 0x04000428 RID: 1064
		internal const int DefaultWarningLevel = 4;

		// Token: 0x04000429 RID: 1065
		internal const int InfoAndHiddenWarningLevel = 1;

		// Token: 0x0400042A RID: 1066
		internal const int MaxWarningLevel = 9999;

		// Token: 0x020005A1 RID: 1441
		[Nullable(0)]
		private sealed class DiagnosticWithProgrammaticSuppression : Diagnostic
		{
			// Token: 0x06003570 RID: 13680 RVA: 0x0009670B File Offset: 0x0009490B
			public DiagnosticWithProgrammaticSuppression(Diagnostic originalUnsuppressedDiagnostic, ProgrammaticSuppressionInfo programmaticSuppressionInfo)
			{
				this._originalUnsuppressedDiagnostic = originalUnsuppressedDiagnostic;
				this._programmaticSuppressionInfo = programmaticSuppressionInfo;
			}

			// Token: 0x17000F2E RID: 3886
			// (get) Token: 0x06003571 RID: 13681 RVA: 0x00096721 File Offset: 0x00094921
			public override DiagnosticDescriptor Descriptor
			{
				get
				{
					return this._originalUnsuppressedDiagnostic.Descriptor;
				}
			}

			// Token: 0x17000F2F RID: 3887
			// (get) Token: 0x06003572 RID: 13682 RVA: 0x0009672E File Offset: 0x0009492E
			public override string Id
			{
				get
				{
					return this.Descriptor.Id;
				}
			}

			// Token: 0x06003573 RID: 13683 RVA: 0x0009673B File Offset: 0x0009493B
			public override string GetMessage([Nullable(2)] IFormatProvider formatProvider = null)
			{
				return this._originalUnsuppressedDiagnostic.GetMessage(formatProvider);
			}

			// Token: 0x17000F30 RID: 3888
			// (get) Token: 0x06003574 RID: 13684 RVA: 0x00096749 File Offset: 0x00094949
			[Nullable(new byte[] { 1, 2 })]
			internal override IReadOnlyList<object> Arguments
			{
				[return: Nullable(new byte[] { 1, 2 })]
				get
				{
					return this._originalUnsuppressedDiagnostic.Arguments;
				}
			}

			// Token: 0x17000F31 RID: 3889
			// (get) Token: 0x06003575 RID: 13685 RVA: 0x00096756 File Offset: 0x00094956
			public override DiagnosticSeverity Severity
			{
				get
				{
					return this._originalUnsuppressedDiagnostic.Severity;
				}
			}

			// Token: 0x17000F32 RID: 3890
			// (get) Token: 0x06003576 RID: 13686 RVA: 0x00096763 File Offset: 0x00094963
			public override bool IsSuppressed
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000F33 RID: 3891
			// (get) Token: 0x06003577 RID: 13687 RVA: 0x00096766 File Offset: 0x00094966
			internal override ProgrammaticSuppressionInfo ProgrammaticSuppressionInfo
			{
				get
				{
					return this._programmaticSuppressionInfo;
				}
			}

			// Token: 0x17000F34 RID: 3892
			// (get) Token: 0x06003578 RID: 13688 RVA: 0x0009676E File Offset: 0x0009496E
			public override int WarningLevel
			{
				get
				{
					return this._originalUnsuppressedDiagnostic.WarningLevel;
				}
			}

			// Token: 0x17000F35 RID: 3893
			// (get) Token: 0x06003579 RID: 13689 RVA: 0x0009677B File Offset: 0x0009497B
			public override Location Location
			{
				get
				{
					return this._originalUnsuppressedDiagnostic.Location;
				}
			}

			// Token: 0x17000F36 RID: 3894
			// (get) Token: 0x0600357A RID: 13690 RVA: 0x00096788 File Offset: 0x00094988
			public override IReadOnlyList<Location> AdditionalLocations
			{
				get
				{
					return this._originalUnsuppressedDiagnostic.AdditionalLocations;
				}
			}

			// Token: 0x17000F37 RID: 3895
			// (get) Token: 0x0600357B RID: 13691 RVA: 0x00096795 File Offset: 0x00094995
			[Nullable(new byte[] { 1, 1, 2 })]
			public override ImmutableDictionary<string, string> Properties
			{
				[return: Nullable(new byte[] { 1, 1, 2 })]
				get
				{
					return this._originalUnsuppressedDiagnostic.Properties;
				}
			}

			// Token: 0x0600357C RID: 13692 RVA: 0x000967A4 File Offset: 0x000949A4
			[NullableContext(2)]
			public override bool Equals(Diagnostic obj)
			{
				if (this == obj)
				{
					return true;
				}
				Diagnostic.DiagnosticWithProgrammaticSuppression diagnosticWithProgrammaticSuppression = obj as Diagnostic.DiagnosticWithProgrammaticSuppression;
				return diagnosticWithProgrammaticSuppression != null && object.Equals(this._originalUnsuppressedDiagnostic, diagnosticWithProgrammaticSuppression._originalUnsuppressedDiagnostic) && object.Equals(this._programmaticSuppressionInfo, diagnosticWithProgrammaticSuppression._programmaticSuppressionInfo);
			}

			// Token: 0x0600357D RID: 13693 RVA: 0x000967E9 File Offset: 0x000949E9
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as Diagnostic);
			}

			// Token: 0x0600357E RID: 13694 RVA: 0x000967F7 File Offset: 0x000949F7
			public override int GetHashCode()
			{
				return Hash.Combine(this._originalUnsuppressedDiagnostic.GetHashCode(), this._programmaticSuppressionInfo.GetHashCode());
			}

			// Token: 0x0600357F RID: 13695 RVA: 0x00096814 File Offset: 0x00094A14
			internal override Diagnostic WithLocation(Location location)
			{
				if (location == null)
				{
					throw new ArgumentNullException("location");
				}
				if (this.Location != location)
				{
					return new Diagnostic.DiagnosticWithProgrammaticSuppression(this._originalUnsuppressedDiagnostic.WithLocation(location), this._programmaticSuppressionInfo);
				}
				return this;
			}

			// Token: 0x06003580 RID: 13696 RVA: 0x00096851 File Offset: 0x00094A51
			internal override Diagnostic WithSeverity(DiagnosticSeverity severity)
			{
				if (this.Severity != severity)
				{
					return new Diagnostic.DiagnosticWithProgrammaticSuppression(this._originalUnsuppressedDiagnostic.WithSeverity(severity), this._programmaticSuppressionInfo);
				}
				return this;
			}

			// Token: 0x06003581 RID: 13697 RVA: 0x00096875 File Offset: 0x00094A75
			internal override Diagnostic WithIsSuppressed(bool isSuppressed)
			{
				if (!isSuppressed)
				{
					throw new ArgumentException("isSuppressed");
				}
				return this;
			}

			// Token: 0x0400163D RID: 5693
			private readonly Diagnostic _originalUnsuppressedDiagnostic;

			// Token: 0x0400163E RID: 5694
			private readonly ProgrammaticSuppressionInfo _programmaticSuppressionInfo;
		}

		// Token: 0x020005A2 RID: 1442
		[Nullable(0)]
		internal sealed class SimpleDiagnostic : Diagnostic
		{
			// Token: 0x06003582 RID: 13698 RVA: 0x00096888 File Offset: 0x00094A88
			private SimpleDiagnostic(DiagnosticDescriptor descriptor, DiagnosticSeverity severity, int warningLevel, Location location, [Nullable(new byte[] { 2, 1 })] IEnumerable<Location> additionalLocations, [Nullable(2)] object[] messageArgs, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties, bool isSuppressed)
			{
				if ((warningLevel == 0 && severity != DiagnosticSeverity.Error) || (warningLevel != 0 && severity == DiagnosticSeverity.Error))
				{
					throw new ArgumentException(string.Format("{0} ({1}) and {2} ({3}) are not compatible.", new object[] { "warningLevel", warningLevel, "severity", severity }), "warningLevel");
				}
				if (descriptor == null)
				{
					throw new ArgumentNullException("descriptor");
				}
				this._descriptor = descriptor;
				this._severity = severity;
				this._warningLevel = warningLevel;
				this._location = location ?? Location.None;
				ImmutableArray<Location>? immutableArray = ((additionalLocations != null) ? new ImmutableArray<Location>?(additionalLocations.ToImmutableArray<Location>()) : default(ImmutableArray<Location>?));
				IReadOnlyList<Location> readOnlyList;
				if (immutableArray == null)
				{
					readOnlyList = SpecializedCollections.EmptyReadOnlyList<Location>();
				}
				else
				{
					IReadOnlyList<Location> readOnlyList2 = immutableArray.GetValueOrDefault();
					readOnlyList = readOnlyList2;
				}
				this._additionalLocations = readOnlyList;
				this._messageArgs = messageArgs ?? Array.Empty<object>();
				this._properties = properties ?? ImmutableDictionary<string, string>.Empty;
				this._isSuppressed = isSuppressed;
			}

			// Token: 0x06003583 RID: 13699 RVA: 0x00096985 File Offset: 0x00094B85
			internal static Diagnostic.SimpleDiagnostic Create(DiagnosticDescriptor descriptor, DiagnosticSeverity severity, int warningLevel, Location location, [Nullable(new byte[] { 2, 1 })] IEnumerable<Location> additionalLocations, [Nullable(2)] object[] messageArgs, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties, bool isSuppressed = false)
			{
				return new Diagnostic.SimpleDiagnostic(descriptor, severity, warningLevel, location, additionalLocations, messageArgs, properties, isSuppressed);
			}

			// Token: 0x06003584 RID: 13700 RVA: 0x00096998 File Offset: 0x00094B98
			internal static Diagnostic.SimpleDiagnostic Create(string id, LocalizableString title, string category, LocalizableString message, LocalizableString description, string helpLink, DiagnosticSeverity severity, DiagnosticSeverity defaultSeverity, bool isEnabledByDefault, int warningLevel, Location location, [Nullable(new byte[] { 2, 1 })] IEnumerable<Location> additionalLocations, [Nullable(new byte[] { 2, 1 })] IEnumerable<string> customTags, [Nullable(new byte[] { 2, 1, 2 })] ImmutableDictionary<string, string> properties, bool isSuppressed = false)
			{
				return new Diagnostic.SimpleDiagnostic(new DiagnosticDescriptor(id, title, message, category, defaultSeverity, isEnabledByDefault, description, helpLink, customTags.ToImmutableArrayOrEmpty<string>()), severity, warningLevel, location, additionalLocations, null, properties, isSuppressed);
			}

			// Token: 0x17000F38 RID: 3896
			// (get) Token: 0x06003585 RID: 13701 RVA: 0x000969CF File Offset: 0x00094BCF
			public override DiagnosticDescriptor Descriptor
			{
				get
				{
					return this._descriptor;
				}
			}

			// Token: 0x17000F39 RID: 3897
			// (get) Token: 0x06003586 RID: 13702 RVA: 0x000969D7 File Offset: 0x00094BD7
			public override string Id
			{
				get
				{
					return this._descriptor.Id;
				}
			}

			// Token: 0x06003587 RID: 13703 RVA: 0x000969E4 File Offset: 0x00094BE4
			public override string GetMessage([Nullable(2)] IFormatProvider formatProvider = null)
			{
				if (this._messageArgs.Length == 0)
				{
					return this._descriptor.MessageFormat.ToString(formatProvider);
				}
				string text = this._descriptor.MessageFormat.ToString(formatProvider);
				string text2;
				try
				{
					text2 = string.Format(formatProvider, text, this._messageArgs);
				}
				catch (Exception)
				{
					text2 = text;
				}
				return text2;
			}

			// Token: 0x17000F3A RID: 3898
			// (get) Token: 0x06003588 RID: 13704 RVA: 0x00096A44 File Offset: 0x00094C44
			[Nullable(new byte[] { 1, 2 })]
			internal override IReadOnlyList<object> Arguments
			{
				[return: Nullable(new byte[] { 1, 2 })]
				get
				{
					return this._messageArgs;
				}
			}

			// Token: 0x17000F3B RID: 3899
			// (get) Token: 0x06003589 RID: 13705 RVA: 0x00096A4C File Offset: 0x00094C4C
			public override DiagnosticSeverity Severity
			{
				get
				{
					return this._severity;
				}
			}

			// Token: 0x17000F3C RID: 3900
			// (get) Token: 0x0600358A RID: 13706 RVA: 0x00096A54 File Offset: 0x00094C54
			public override bool IsSuppressed
			{
				get
				{
					return this._isSuppressed;
				}
			}

			// Token: 0x17000F3D RID: 3901
			// (get) Token: 0x0600358B RID: 13707 RVA: 0x00096A5C File Offset: 0x00094C5C
			public override int WarningLevel
			{
				get
				{
					return this._warningLevel;
				}
			}

			// Token: 0x17000F3E RID: 3902
			// (get) Token: 0x0600358C RID: 13708 RVA: 0x00096A64 File Offset: 0x00094C64
			public override Location Location
			{
				get
				{
					return this._location;
				}
			}

			// Token: 0x17000F3F RID: 3903
			// (get) Token: 0x0600358D RID: 13709 RVA: 0x00096A6C File Offset: 0x00094C6C
			public override IReadOnlyList<Location> AdditionalLocations
			{
				get
				{
					return this._additionalLocations;
				}
			}

			// Token: 0x17000F40 RID: 3904
			// (get) Token: 0x0600358E RID: 13710 RVA: 0x00096A74 File Offset: 0x00094C74
			[Nullable(new byte[] { 1, 1, 2 })]
			public override ImmutableDictionary<string, string> Properties
			{
				[return: Nullable(new byte[] { 1, 1, 2 })]
				get
				{
					return this._properties;
				}
			}

			// Token: 0x0600358F RID: 13711 RVA: 0x00096A7C File Offset: 0x00094C7C
			[NullableContext(2)]
			public override bool Equals(Diagnostic obj)
			{
				if (this == obj)
				{
					return true;
				}
				Diagnostic.SimpleDiagnostic simpleDiagnostic = obj as Diagnostic.SimpleDiagnostic;
				if (simpleDiagnostic == null)
				{
					return false;
				}
				if (AnalyzerExecutor.IsAnalyzerExceptionDiagnostic(this))
				{
					return AnalyzerExecutor.AreEquivalentAnalyzerExceptionDiagnostics(this, simpleDiagnostic);
				}
				if (this._descriptor.Equals(simpleDiagnostic._descriptor))
				{
					if (this._messageArgs.SequenceEqual<object>(simpleDiagnostic._messageArgs, (object a, object b) => a == b) && this._location == simpleDiagnostic._location && this._severity == simpleDiagnostic._severity)
					{
						return this._warningLevel == simpleDiagnostic._warningLevel;
					}
				}
				return false;
			}

			// Token: 0x06003590 RID: 13712 RVA: 0x00096B21 File Offset: 0x00094D21
			[NullableContext(2)]
			public override bool Equals(object obj)
			{
				return this.Equals(obj as Diagnostic);
			}

			// Token: 0x06003591 RID: 13713 RVA: 0x00096B2F File Offset: 0x00094D2F
			public override int GetHashCode()
			{
				return Hash.Combine<DiagnosticDescriptor>(this._descriptor, Hash.CombineValues<object>(this._messageArgs, Hash.Combine(this._warningLevel, Hash.Combine<Location>(this._location, (int)this._severity))));
			}

			// Token: 0x06003592 RID: 13714 RVA: 0x00096B64 File Offset: 0x00094D64
			internal override Diagnostic WithLocation(Location location)
			{
				if (location == null)
				{
					throw new ArgumentNullException("location");
				}
				if (location != this._location)
				{
					return new Diagnostic.SimpleDiagnostic(this._descriptor, this._severity, this._warningLevel, location, this._additionalLocations, this._messageArgs, this._properties, this._isSuppressed);
				}
				return this;
			}

			// Token: 0x06003593 RID: 13715 RVA: 0x00096BC0 File Offset: 0x00094DC0
			internal override Diagnostic WithSeverity(DiagnosticSeverity severity)
			{
				if (this.Severity != severity)
				{
					int defaultWarningLevel = Diagnostic.GetDefaultWarningLevel(severity);
					return new Diagnostic.SimpleDiagnostic(this._descriptor, severity, defaultWarningLevel, this._location, this._additionalLocations, this._messageArgs, this._properties, this._isSuppressed);
				}
				return this;
			}

			// Token: 0x06003594 RID: 13716 RVA: 0x00096C0A File Offset: 0x00094E0A
			internal override Diagnostic WithIsSuppressed(bool isSuppressed)
			{
				if (this.IsSuppressed != isSuppressed)
				{
					return new Diagnostic.SimpleDiagnostic(this._descriptor, this._severity, this._warningLevel, this._location, this._additionalLocations, this._messageArgs, this._properties, isSuppressed);
				}
				return this;
			}

			// Token: 0x0400163F RID: 5695
			private readonly DiagnosticDescriptor _descriptor;

			// Token: 0x04001640 RID: 5696
			private readonly DiagnosticSeverity _severity;

			// Token: 0x04001641 RID: 5697
			private readonly int _warningLevel;

			// Token: 0x04001642 RID: 5698
			private readonly Location _location;

			// Token: 0x04001643 RID: 5699
			private readonly IReadOnlyList<Location> _additionalLocations;

			// Token: 0x04001644 RID: 5700
			[Nullable(new byte[] { 1, 2 })]
			private readonly object[] _messageArgs;

			// Token: 0x04001645 RID: 5701
			[Nullable(new byte[] { 1, 1, 2 })]
			private readonly ImmutableDictionary<string, string> _properties;

			// Token: 0x04001646 RID: 5702
			private readonly bool _isSuppressed;
		}
	}
}
