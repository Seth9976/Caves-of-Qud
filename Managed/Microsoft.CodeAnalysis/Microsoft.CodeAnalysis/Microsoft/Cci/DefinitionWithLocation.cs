using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.Cci
{
	// Token: 0x020000E7 RID: 231
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal struct DefinitionWithLocation : IEquatable<DefinitionWithLocation>
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x000180E7 File Offset: 0x000162E7
		public DefinitionWithLocation(IDefinition definition, int startLine, int startColumn, int endLine, int endColumn)
		{
			this.Definition = definition;
			this.StartLine = startLine;
			this.StartColumn = startColumn;
			this.EndLine = endLine;
			this.EndColumn = endColumn;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00018110 File Offset: 0x00016310
		private string GetDebuggerDisplay()
		{
			return string.Format("{0} => ({1},{2}) - ({3}, {4})", new object[] { this.Definition, this.StartLine, this.StartColumn, this.EndLine, this.EndColumn });
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00018170 File Offset: 0x00016370
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is DefinitionWithLocation)
			{
				DefinitionWithLocation definitionWithLocation = (DefinitionWithLocation)obj;
				return this.Equals(definitionWithLocation);
			}
			return false;
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00018198 File Offset: 0x00016398
		public bool Equals(DefinitionWithLocation other)
		{
			return this.Definition == other.Definition && this.StartLine == other.StartLine && this.StartColumn == other.StartColumn && this.EndLine == other.EndLine && this.EndColumn == other.EndColumn;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000181F0 File Offset: 0x000163F0
		public override int GetHashCode()
		{
			return Hash.Combine(RuntimeHelpers.GetHashCode(this.Definition), this.StartLine.GetHashCode());
		}

		// Token: 0x04000240 RID: 576
		public readonly IDefinition Definition;

		// Token: 0x04000241 RID: 577
		public readonly int StartLine;

		// Token: 0x04000242 RID: 578
		public readonly int StartColumn;

		// Token: 0x04000243 RID: 579
		public readonly int EndLine;

		// Token: 0x04000244 RID: 580
		public readonly int EndColumn;
	}
}
