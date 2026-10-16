using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001DD RID: 477
	[NullableContext(1)]
	[Nullable(0)]
	public struct GeneratorInitializationContext
	{
		// Token: 0x060013D0 RID: 5072 RVA: 0x0003DF45 File Offset: 0x0003C145
		internal GeneratorInitializationContext(CancellationToken cancellationToken = default(CancellationToken))
		{
			this.CancellationToken = cancellationToken;
			this.InfoBuilder = new GeneratorInfo.Builder();
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0003DF59 File Offset: 0x0003C159
		public readonly CancellationToken CancellationToken { get; }

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0003DF61 File Offset: 0x0003C161
		internal readonly GeneratorInfo.Builder InfoBuilder { get; }

		// Token: 0x060013D3 RID: 5075 RVA: 0x0003DF69 File Offset: 0x0003C169
		internal void RegisterForAdditionalFileChanges(EditCallback<AdditionalFileEdit> callback)
		{
			GeneratorInitializationContext.CheckIsEmpty<EditCallback<AdditionalFileEdit>>(this.InfoBuilder.EditCallback, null);
			this.InfoBuilder.EditCallback = callback;
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0003DF88 File Offset: 0x0003C188
		public void RegisterForSyntaxNotifications(SyntaxReceiverCreator receiverCreator)
		{
			GeneratorInitializationContext.CheckIsEmpty<SyntaxContextReceiverCreator>(this.InfoBuilder.SyntaxContextReceiverCreator, "SyntaxReceiverCreator / SyntaxContextReceiverCreator");
			this.InfoBuilder.SyntaxContextReceiverCreator = SyntaxContextReceiverAdaptor.Create(receiverCreator);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0003DFB0 File Offset: 0x0003C1B0
		public void RegisterForSyntaxNotifications(SyntaxContextReceiverCreator receiverCreator)
		{
			GeneratorInitializationContext.CheckIsEmpty<SyntaxContextReceiverCreator>(this.InfoBuilder.SyntaxContextReceiverCreator, "SyntaxReceiverCreator / SyntaxContextReceiverCreator");
			this.InfoBuilder.SyntaxContextReceiverCreator = receiverCreator;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0003DFD3 File Offset: 0x0003C1D3
		public void RegisterForPostInitialization(Action<GeneratorPostInitializationContext> callback)
		{
			GeneratorInitializationContext.CheckIsEmpty<Action<GeneratorPostInitializationContext>>(this.InfoBuilder.PostInitCallback, null);
			this.InfoBuilder.PostInitCallback = callback;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0003DFF2 File Offset: 0x0003C1F2
		[NullableContext(2)]
		private static void CheckIsEmpty<T>([Nullable(1)] T x, string typeName = null) where T : class
		{
			if (x != null)
			{
				throw new InvalidOperationException(string.Format(CodeAnalysisResources.Single_type_per_generator_0, typeName ?? typeof(T).Name));
			}
		}
	}
}
