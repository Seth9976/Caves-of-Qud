using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CodeGen;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000488 RID: 1160
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal readonly struct EncLocalInfo : IEquatable<EncLocalInfo>
	{
		// Token: 0x06002DA7 RID: 11687 RVA: 0x000817B5 File Offset: 0x0007F9B5
		public EncLocalInfo(byte[] signature)
		{
			this.SlotInfo = new LocalSlotDebugInfo(SynthesizedLocalKind.EmitterTemp, LocalDebugId.None);
			this.Type = null;
			this.Constraints = LocalSlotConstraints.None;
			this.Signature = signature;
			this.IsUnused = true;
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000817E4 File Offset: 0x0007F9E4
		public EncLocalInfo(LocalSlotDebugInfo slotInfo, ITypeReference type, LocalSlotConstraints constraints, [Nullable(2)] byte[] signature)
		{
			this.SlotInfo = slotInfo;
			this.Type = type;
			this.Constraints = constraints;
			this.Signature = signature;
			this.IsUnused = false;
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x0008180A File Offset: 0x0007FA0A
		public bool IsDefault
		{
			get
			{
				return this.Type == null && this.Signature == null;
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x00081820 File Offset: 0x0007FA20
		public bool Equals(EncLocalInfo other)
		{
			return this.SlotInfo.Equals(other.SlotInfo) && SymbolEquivalentEqualityComparer.Instance.Equals(this.Type, other.Type) && this.Constraints == other.Constraints && this.IsUnused == other.IsUnused;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x0008187C File Offset: 0x0007FA7C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is EncLocalInfo)
			{
				EncLocalInfo encLocalInfo = (EncLocalInfo)obj;
				return this.Equals(encLocalInfo);
			}
			return false;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000818A4 File Offset: 0x0007FAA4
		public override int GetHashCode()
		{
			return Hash.Combine(this.SlotInfo.GetHashCode(), Hash.Combine(SymbolEquivalentEqualityComparer.Instance.GetHashCode(this.Type), Hash.Combine((int)this.Constraints, Hash.Combine(this.IsUnused, 0))));
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000818F8 File Offset: 0x0007FAF8
		private string GetDebuggerDisplay()
		{
			if (this.IsDefault)
			{
				return "[default]";
			}
			if (this.IsUnused)
			{
				return "[invalid]";
			}
			return string.Format("[Id={0}, SynthesizedKind={1}, Type={2}, Constraints={3}, Sig={4}]", new object[]
			{
				this.SlotInfo.Id,
				this.SlotInfo.SynthesizedKind,
				this.Type,
				this.Constraints,
				(this.Signature != null) ? BitConverter.ToString(this.Signature) : "null"
			});
		}

		// Token: 0x0400122F RID: 4655
		public readonly LocalSlotDebugInfo SlotInfo;

		// Token: 0x04001230 RID: 4656
		[Nullable(2)]
		public readonly ITypeReference Type;

		// Token: 0x04001231 RID: 4657
		public readonly LocalSlotConstraints Constraints;

		// Token: 0x04001232 RID: 4658
		[Nullable(2)]
		public readonly byte[] Signature;

		// Token: 0x04001233 RID: 4659
		public readonly bool IsUnused;
	}
}
