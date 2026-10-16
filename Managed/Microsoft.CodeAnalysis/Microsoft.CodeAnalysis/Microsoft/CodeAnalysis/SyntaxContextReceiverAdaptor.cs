using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001F2 RID: 498
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SyntaxContextReceiverAdaptor : ISyntaxContextReceiver
	{
		// Token: 0x06001438 RID: 5176 RVA: 0x0003F34E File Offset: 0x0003D54E
		private SyntaxContextReceiverAdaptor(ISyntaxReceiver receiver)
		{
			if (receiver == null)
			{
				throw new ArgumentNullException("receiver");
			}
			this.Receiver = receiver;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0003F36C File Offset: 0x0003D56C
		public ISyntaxReceiver Receiver { get; }

		// Token: 0x0600143A RID: 5178 RVA: 0x0003F374 File Offset: 0x0003D574
		public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
		{
			this.Receiver.OnVisitSyntaxNode(context.Node);
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0003F388 File Offset: 0x0003D588
		public static SyntaxContextReceiverCreator Create(SyntaxReceiverCreator creator)
		{
			return delegate
			{
				ISyntaxReceiver syntaxReceiver = creator();
				if (syntaxReceiver != null)
				{
					return new SyntaxContextReceiverAdaptor(syntaxReceiver);
				}
				return null;
			};
		}
	}
}
