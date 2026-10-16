using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001E3 RID: 483
	[NullableContext(2)]
	[Nullable(0)]
	internal readonly struct GeneratorInfo
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0003F00B File Offset: 0x0003D20B
		[Nullable(new byte[] { 2, 1 })]
		internal EditCallback<AdditionalFileEdit> EditCallback
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0003F013 File Offset: 0x0003D213
		internal SyntaxContextReceiverCreator SyntaxContextReceiverCreator { get; }

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0003F01B File Offset: 0x0003D21B
		internal Action<GeneratorPostInitializationContext> PostInitCallback { get; }

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0003F023 File Offset: 0x0003D223
		internal bool Initialized { get; }

		// Token: 0x06001401 RID: 5121 RVA: 0x0003F02B File Offset: 0x0003D22B
		internal GeneratorInfo([Nullable(new byte[] { 2, 1 })] EditCallback<AdditionalFileEdit> editCallback, SyntaxContextReceiverCreator receiverCreator, Action<GeneratorPostInitializationContext> postInitCallback)
		{
			this.EditCallback = editCallback;
			this.SyntaxContextReceiverCreator = receiverCreator;
			this.PostInitCallback = postInitCallback;
			this.Initialized = true;
		}

		// Token: 0x020005E0 RID: 1504
		[Nullable(0)]
		internal class Builder
		{
			// Token: 0x17000F89 RID: 3977
			// (get) Token: 0x060036CE RID: 14030 RVA: 0x00099F36 File Offset: 0x00098136
			// (set) Token: 0x060036CF RID: 14031 RVA: 0x00099F3E File Offset: 0x0009813E
			[Nullable(new byte[] { 2, 1 })]
			internal EditCallback<AdditionalFileEdit> EditCallback
			{
				[return: Nullable(new byte[] { 2, 1 })]
				get;
				[param: Nullable(new byte[] { 2, 1 })]
				set;
			}

			// Token: 0x17000F8A RID: 3978
			// (get) Token: 0x060036D0 RID: 14032 RVA: 0x00099F47 File Offset: 0x00098147
			// (set) Token: 0x060036D1 RID: 14033 RVA: 0x00099F4F File Offset: 0x0009814F
			internal SyntaxContextReceiverCreator SyntaxContextReceiverCreator { get; set; }

			// Token: 0x17000F8B RID: 3979
			// (get) Token: 0x060036D2 RID: 14034 RVA: 0x00099F58 File Offset: 0x00098158
			// (set) Token: 0x060036D3 RID: 14035 RVA: 0x00099F60 File Offset: 0x00098160
			internal Action<GeneratorPostInitializationContext> PostInitCallback { get; set; }

			// Token: 0x060036D4 RID: 14036 RVA: 0x00099F69 File Offset: 0x00098169
			public GeneratorInfo ToImmutable()
			{
				return new GeneratorInfo(this.EditCallback, this.SyntaxContextReceiverCreator, this.PostInitCallback);
			}
		}
	}
}
