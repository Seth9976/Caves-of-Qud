using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000149 RID: 329
	internal static class OptimizationLevelFacts
	{
		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00026F1A File Offset: 0x0002511A
		[TupleElementNames(new string[] { "OptimizationLevel", "DebugPlus" })]
		internal static ValueTuple<OptimizationLevel, bool> DefaultValues
		{
			[return: TupleElementNames(new string[] { "OptimizationLevel", "DebugPlus" })]
			get
			{
				return new ValueTuple<OptimizationLevel, bool>(OptimizationLevel.Debug, false);
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00026F24 File Offset: 0x00025124
		[NullableContext(1)]
		public static string ToPdbSerializedString(this OptimizationLevel optimization, bool debugPlusMode)
		{
			string text;
			if (optimization != OptimizationLevel.Debug)
			{
				if (optimization != OptimizationLevel.Release)
				{
					throw ExceptionUtilities.UnexpectedValue(optimization);
				}
				if (!debugPlusMode)
				{
					text = "release";
				}
				else
				{
					text = "release-debug-plus";
				}
			}
			else if (!debugPlusMode)
			{
				text = "debug";
			}
			else
			{
				text = "debug-plus";
			}
			return text;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00026F70 File Offset: 0x00025170
		[NullableContext(1)]
		public static bool TryParsePdbSerializedString(string value, out OptimizationLevel optimizationLevel, out bool debugPlusMode)
		{
			if (value == "release-debug-plus")
			{
				optimizationLevel = OptimizationLevel.Release;
				debugPlusMode = true;
				return true;
			}
			if (value == "release")
			{
				optimizationLevel = OptimizationLevel.Release;
				debugPlusMode = false;
				return true;
			}
			if (value == "debug-plus")
			{
				optimizationLevel = OptimizationLevel.Debug;
				debugPlusMode = true;
				return true;
			}
			if (!(value == "debug"))
			{
				optimizationLevel = OptimizationLevel.Debug;
				debugPlusMode = false;
				return false;
			}
			optimizationLevel = OptimizationLevel.Debug;
			debugPlusMode = false;
			return true;
		}
	}
}
