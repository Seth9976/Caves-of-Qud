using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000177 RID: 375
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal class DiagnosticWithInfo : Diagnostic
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x0002DD62 File Offset: 0x0002BF62
		internal DiagnosticWithInfo(DiagnosticInfo info, Location location, bool isSuppressed = false)
		{
			this._info = info;
			this._location = location;
			this._isSuppressed = isSuppressed;
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0002DD7F File Offset: 0x0002BF7F
		public override Location Location
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0002DD87 File Offset: 0x0002BF87
		public override IReadOnlyList<Location> AdditionalLocations
		{
			get
			{
				return this.Info.AdditionalLocations;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x0002DD94 File Offset: 0x0002BF94
		internal override IReadOnlyList<string> CustomTags
		{
			get
			{
				return this.Info.CustomTags;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0002DDA6 File Offset: 0x0002BFA6
		public override DiagnosticDescriptor Descriptor
		{
			get
			{
				return this.Info.Descriptor;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0002DDB3 File Offset: 0x0002BFB3
		public override string Id
		{
			get
			{
				return this.Info.MessageIdentifier;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0002DDC0 File Offset: 0x0002BFC0
		internal override string Category
		{
			get
			{
				return this.Info.Category;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0002DDCD File Offset: 0x0002BFCD
		internal sealed override int Code
		{
			get
			{
				return this.Info.Code;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x0002DDDA File Offset: 0x0002BFDA
		public sealed override DiagnosticSeverity Severity
		{
			get
			{
				return this.Info.Severity;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0002DDE7 File Offset: 0x0002BFE7
		public sealed override DiagnosticSeverity DefaultSeverity
		{
			get
			{
				return this.Info.DefaultSeverity;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0002DDF4 File Offset: 0x0002BFF4
		internal sealed override bool IsEnabledByDefault
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0002DDF7 File Offset: 0x0002BFF7
		public override bool IsSuppressed
		{
			get
			{
				return this._isSuppressed;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x0002DDFF File Offset: 0x0002BFFF
		public sealed override int WarningLevel
		{
			get
			{
				return this.Info.WarningLevel;
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0002DE0C File Offset: 0x0002C00C
		public override string GetMessage([Nullable(2)] IFormatProvider formatProvider = null)
		{
			return this.Info.GetMessage(formatProvider);
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000F9A RID: 3994 RVA: 0x0002DE1A File Offset: 0x0002C01A
		[Nullable(new byte[] { 1, 2 })]
		internal override IReadOnlyList<object> Arguments
		{
			[return: Nullable(new byte[] { 1, 2 })]
			get
			{
				return this.Info.Arguments;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0002DE27 File Offset: 0x0002C027
		public DiagnosticInfo Info
		{
			get
			{
				if (this._info.Severity == (DiagnosticSeverity)(-1))
				{
					return this._info.GetResolvedInfo();
				}
				return this._info;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x0002DE49 File Offset: 0x0002C049
		internal bool HasLazyInfo
		{
			get
			{
				return this._info.Severity == (DiagnosticSeverity)(-1) || this._info.Severity == (DiagnosticSeverity)(-2);
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0002DE6A File Offset: 0x0002C06A
		public override int GetHashCode()
		{
			return Hash.Combine(this.Location.GetHashCode(), this.Info.GetHashCode());
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0002DE87 File Offset: 0x0002C087
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Diagnostic);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0002DE98 File Offset: 0x0002C098
		[NullableContext(2)]
		public override bool Equals(Diagnostic obj)
		{
			if (this == obj)
			{
				return true;
			}
			DiagnosticWithInfo diagnosticWithInfo = obj as DiagnosticWithInfo;
			return diagnosticWithInfo != null && !(base.GetType() != diagnosticWithInfo.GetType()) && (this.Location.Equals(diagnosticWithInfo._location) && this.Info.Equals(diagnosticWithInfo.Info)) && Enumerable.SequenceEqual<Location>(this.AdditionalLocations, diagnosticWithInfo.AdditionalLocations);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0002DF04 File Offset: 0x0002C104
		private string GetDebuggerDisplay()
		{
			DiagnosticSeverity severity = this._info.Severity;
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

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0002DF66 File Offset: 0x0002C166
		internal override Diagnostic WithLocation(Location location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			if (location != this._location)
			{
				return new DiagnosticWithInfo(this._info, location, this._isSuppressed);
			}
			return this;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0002DF9E File Offset: 0x0002C19E
		internal override Diagnostic WithSeverity(DiagnosticSeverity severity)
		{
			if (this.Severity != severity)
			{
				return new DiagnosticWithInfo(this.Info.GetInstanceWithSeverity(severity), this._location, this._isSuppressed);
			}
			return this;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0002DFC8 File Offset: 0x0002C1C8
		internal override Diagnostic WithIsSuppressed(bool isSuppressed)
		{
			if (this.IsSuppressed != isSuppressed)
			{
				return new DiagnosticWithInfo(this.Info, this._location, isSuppressed);
			}
			return this;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0002DFE7 File Offset: 0x0002C1E7
		internal sealed override bool IsNotConfigurable()
		{
			return this.Info.IsNotConfigurable();
		}

		// Token: 0x04000448 RID: 1096
		private readonly DiagnosticInfo _info;

		// Token: 0x04000449 RID: 1097
		private readonly Location _location;

		// Token: 0x0400044A RID: 1098
		private readonly bool _isSuppressed;
	}
}
