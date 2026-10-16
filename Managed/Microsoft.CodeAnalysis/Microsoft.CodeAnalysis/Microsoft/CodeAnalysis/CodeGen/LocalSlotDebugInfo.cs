using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004CE RID: 1230
	internal struct LocalSlotDebugInfo : IEquatable<LocalSlotDebugInfo>
	{
		// Token: 0x06003181 RID: 12673 RVA: 0x0008DB3B File Offset: 0x0008BD3B
		public LocalSlotDebugInfo(SynthesizedLocalKind synthesizedKind, LocalDebugId id)
		{
			this.SynthesizedKind = synthesizedKind;
			this.Id = id;
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x0008DB4C File Offset: 0x0008BD4C
		public bool Equals(LocalSlotDebugInfo other)
		{
			return this.SynthesizedKind == other.SynthesizedKind && this.Id.Equals(other.Id);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x0008DB7D File Offset: 0x0008BD7D
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LocalSlotDebugInfo && this.Equals((LocalSlotDebugInfo)obj);
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x0008DB98 File Offset: 0x0008BD98
		public override int GetHashCode()
		{
			return Hash.Combine((int)this.SynthesizedKind, this.Id.GetHashCode());
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x0008DBC4 File Offset: 0x0008BDC4
		[NullableContext(1)]
		public override string ToString()
		{
			return this.SynthesizedKind.ToString() + " " + this.Id.ToString();
		}

		// Token: 0x04001318 RID: 4888
		public readonly SynthesizedLocalKind SynthesizedKind;

		// Token: 0x04001319 RID: 4889
		public readonly LocalDebugId Id;
	}
}
