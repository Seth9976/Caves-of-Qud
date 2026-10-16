using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000132 RID: 306
	internal sealed class CSDiagnostic : DiagnosticWithInfo
	{
		// Token: 0x060018E1 RID: 6369 RVA: 0x00085383 File Offset: 0x00083583
		internal CSDiagnostic(DiagnosticInfo info, Location location, bool isSuppressed = false)
			: base(info, location, isSuppressed)
		{
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0008538E File Offset: 0x0008358E
		public override string ToString()
		{
			return CSharpDiagnosticFormatter.Instance.Format(this, null);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x0008539C File Offset: 0x0008359C
		internal override Diagnostic WithLocation(Location location)
		{
			if (location == null)
			{
				throw new ArgumentNullException("location");
			}
			if (location != this.Location)
			{
				return new CSDiagnostic(base.Info, location, this.IsSuppressed);
			}
			return this;
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000853D4 File Offset: 0x000835D4
		internal override Diagnostic WithSeverity(DiagnosticSeverity severity)
		{
			if (this.Severity != severity)
			{
				return new CSDiagnostic(base.Info.GetInstanceWithSeverity(severity), this.Location, this.IsSuppressed);
			}
			return this;
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x000853FE File Offset: 0x000835FE
		internal override Diagnostic WithIsSuppressed(bool isSuppressed)
		{
			if (this.IsSuppressed != isSuppressed)
			{
				return new CSDiagnostic(base.Info, this.Location, isSuppressed);
			}
			return this;
		}
	}
}
