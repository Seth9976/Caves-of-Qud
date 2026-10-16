using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001EE RID: 494
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class AdditionalFileAddedEdit : AdditionalFileEdit
	{
		// Token: 0x06001426 RID: 5158 RVA: 0x0003F17A File Offset: 0x0003D37A
		public AdditionalFileAddedEdit(AdditionalText addedText)
		{
			this.AddedText = addedText;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0003F189 File Offset: 0x0003D389
		public AdditionalText AddedText { get; }

		// Token: 0x06001428 RID: 5160 RVA: 0x0003F194 File Offset: 0x0003D394
		internal override GeneratorDriverState Commit(GeneratorDriverState state)
		{
			ImmutableArray<AdditionalText>? immutableArray = new ImmutableArray<AdditionalText>?(state.AdditionalTexts.Add(this.AddedText));
			return state.With(default(ImmutableArray<ISourceGenerator>?), default(ImmutableArray<GeneratorState>?), immutableArray, default(ImmutableArray<PendingEdit>?), default(bool?));
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0003F1EA File Offset: 0x0003D3EA
		internal override bool AcceptedBy(GeneratorInfo info)
		{
			return info.EditCallback != null;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0003F1F6 File Offset: 0x0003D3F6
		internal override bool TryApply(GeneratorInfo info, GeneratorEditContext context)
		{
			return info.EditCallback(context, this);
		}
	}
}
