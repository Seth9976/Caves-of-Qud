using System;
using System.Reflection.Metadata;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200028C RID: 652
	internal static class SynthesizedLocalKindExtensions
	{
		// Token: 0x06001AE5 RID: 6885 RVA: 0x0004F9CF File Offset: 0x0004DBCF
		public static bool IsLongLived(this SynthesizedLocalKind kind)
		{
			return kind >= SynthesizedLocalKind.UserDefined;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x0004F9D8 File Offset: 0x0004DBD8
		public static bool MustSurviveStateMachineSuspension(this SynthesizedLocalKind kind)
		{
			return kind.IsLongLived() && kind != SynthesizedLocalKind.ConditionalBranchDiscriminator;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x0004F9EB File Offset: 0x0004DBEB
		public static bool IsSlotReusable(this SynthesizedLocalKind kind, OptimizationLevel optimizations)
		{
			return kind.IsSlotReusable(optimizations != OptimizationLevel.Release);
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x0004F9FA File Offset: 0x0004DBFA
		public static bool IsSlotReusable(this SynthesizedLocalKind kind, bool isDebug)
		{
			if (isDebug)
			{
				return !kind.IsLongLived();
			}
			return kind != SynthesizedLocalKind.UserDefined && kind != SynthesizedLocalKind.With && kind != SynthesizedLocalKind.LambdaDisplayClass;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x0004FA19 File Offset: 0x0004DC19
		public static LocalVariableAttributes PdbAttributes(this SynthesizedLocalKind kind)
		{
			if (kind == SynthesizedLocalKind.LambdaDisplayClass || kind == SynthesizedLocalKind.UserDefined || kind == SynthesizedLocalKind.With)
			{
				return LocalVariableAttributes.None;
			}
			return LocalVariableAttributes.DebuggerHidden;
		}
	}
}
