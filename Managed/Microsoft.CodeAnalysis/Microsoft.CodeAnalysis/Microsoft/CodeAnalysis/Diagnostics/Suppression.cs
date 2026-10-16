using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Diagnostics
{
	// Token: 0x0200034C RID: 844
	[NullableContext(1)]
	[Nullable(0)]
	public struct Suppression : IEquatable<Suppression>
	{
		// Token: 0x0600244F RID: 9295 RVA: 0x0007132C File Offset: 0x0006F52C
		private Suppression(SuppressionDescriptor descriptor, Diagnostic suppressedDiagnostic)
		{
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			this.Descriptor = descriptor;
			if (suppressedDiagnostic == null)
			{
				throw new ArgumentNullException("suppressedDiagnostic");
			}
			this.SuppressedDiagnostic = suppressedDiagnostic;
			if (descriptor.SuppressedDiagnosticId != suppressedDiagnostic.Id)
			{
				throw new ArgumentException(string.Format(CodeAnalysisResources.InvalidDiagnosticSuppressionReported, suppressedDiagnostic.Id, descriptor.SuppressedDiagnosticId));
			}
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00071394 File Offset: 0x0006F594
		public static Suppression Create(SuppressionDescriptor descriptor, Diagnostic suppressedDiagnostic)
		{
			return new Suppression(descriptor, suppressedDiagnostic);
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x0007139D File Offset: 0x0006F59D
		public readonly SuppressionDescriptor Descriptor { get; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x000713A5 File Offset: 0x0006F5A5
		public readonly Diagnostic SuppressedDiagnostic { get; }

		// Token: 0x06002453 RID: 9299 RVA: 0x000713AD File Offset: 0x0006F5AD
		public static bool operator ==(Suppression left, Suppression right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x000713B7 File Offset: 0x0006F5B7
		public static bool operator !=(Suppression left, Suppression right)
		{
			return !(left == right);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x000713C4 File Offset: 0x0006F5C4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is Suppression)
			{
				Suppression suppression = (Suppression)obj;
				return this.Equals(suppression);
			}
			return false;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x000713E9 File Offset: 0x0006F5E9
		public bool Equals(Suppression other)
		{
			return EqualityComparer<SuppressionDescriptor>.Default.Equals(this.Descriptor, other.Descriptor) && EqualityComparer<Diagnostic>.Default.Equals(this.SuppressedDiagnostic, other.SuppressedDiagnostic);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0007141D File Offset: 0x0006F61D
		public override int GetHashCode()
		{
			return Hash.Combine(EqualityComparer<SuppressionDescriptor>.Default.GetHashCode(this.Descriptor), EqualityComparer<Diagnostic>.Default.GetHashCode(this.SuppressedDiagnostic));
		}
	}
}
