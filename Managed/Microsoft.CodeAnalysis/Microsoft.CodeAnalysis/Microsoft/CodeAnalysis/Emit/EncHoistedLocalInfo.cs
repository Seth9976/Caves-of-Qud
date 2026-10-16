using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000486 RID: 1158
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal readonly struct EncHoistedLocalInfo : IEquatable<EncHoistedLocalInfo>
	{
		// Token: 0x06002D9F RID: 11679 RVA: 0x0008168F File Offset: 0x0007F88F
		public EncHoistedLocalInfo(bool _)
		{
			this.SlotInfo = new LocalSlotDebugInfo(SynthesizedLocalKind.EmitterTemp, LocalDebugId.None);
			this.Type = null;
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000816A9 File Offset: 0x0007F8A9
		[NullableContext(1)]
		public EncHoistedLocalInfo(LocalSlotDebugInfo slotInfo, ITypeReference type)
		{
			this.SlotInfo = slotInfo;
			this.Type = type;
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06002DA1 RID: 11681 RVA: 0x000816B9 File Offset: 0x0007F8B9
		public bool IsUnused
		{
			get
			{
				return this.Type == null;
			}
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000816C4 File Offset: 0x0007F8C4
		public bool Equals(EncHoistedLocalInfo other)
		{
			return this.SlotInfo.Equals(other.SlotInfo) && SymbolEquivalentEqualityComparer.Instance.Equals(this.Type, other.Type);
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x00081700 File Offset: 0x0007F900
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is EncHoistedLocalInfo)
			{
				EncHoistedLocalInfo encHoistedLocalInfo = (EncHoistedLocalInfo)obj;
				return this.Equals(encHoistedLocalInfo);
			}
			return false;
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x00081728 File Offset: 0x0007F928
		public override int GetHashCode()
		{
			return Hash.Combine(SymbolEquivalentEqualityComparer.Instance.GetHashCode(this.Type), this.SlotInfo.GetHashCode());
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x0008175E File Offset: 0x0007F95E
		[NullableContext(1)]
		private string GetDebuggerDisplay()
		{
			if (this.IsUnused)
			{
				return "[invalid]";
			}
			return string.Format("[Id={0}, SynthesizedKind={1}, Type={2}]", this.SlotInfo.Id, this.SlotInfo.SynthesizedKind, this.Type);
		}

		// Token: 0x0400122A RID: 4650
		public readonly LocalSlotDebugInfo SlotInfo;

		// Token: 0x0400122B RID: 4651
		[Nullable(2)]
		public readonly ITypeReference Type;
	}
}
