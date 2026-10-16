using System;
using System.Text;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000568 RID: 1384
	internal struct SymbolCompletionState
	{
		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x060074D0 RID: 29904 RVA: 0x001B3C78 File Offset: 0x001B1E78
		internal int IncompleteParts
		{
			get
			{
				return ~this._completeParts & 131071;
			}
		}

		// Token: 0x060074D1 RID: 29905 RVA: 0x001B3C89 File Offset: 0x001B1E89
		internal void DefaultForceComplete(Symbol symbol, CancellationToken cancellationToken)
		{
			if (!this.HasComplete(CompletionPart.Attributes))
			{
				symbol.GetAttributes();
				this.SpinWaitComplete(CompletionPart.Attributes, cancellationToken);
			}
			this.NotePartComplete(CompletionPart.All);
		}

		// Token: 0x060074D2 RID: 29906 RVA: 0x001B3CAF File Offset: 0x001B1EAF
		internal bool HasComplete(CompletionPart part)
		{
			return (this._completeParts & (int)part) == (int)part;
		}

		// Token: 0x060074D3 RID: 29907 RVA: 0x001B3CBE File Offset: 0x001B1EBE
		internal bool NotePartComplete(CompletionPart part)
		{
			return ThreadSafeFlagOperations.Set(ref this._completeParts, (int)part);
		}

		// Token: 0x17002037 RID: 8247
		// (get) Token: 0x060074D4 RID: 29908 RVA: 0x001B3CCC File Offset: 0x001B1ECC
		internal CompletionPart NextIncompletePart
		{
			get
			{
				int incompleteParts = this.IncompleteParts;
				return (CompletionPart)(incompleteParts & ~(CompletionPart)(incompleteParts - 1));
			}
		}

		// Token: 0x060074D5 RID: 29909 RVA: 0x001B3CD9 File Offset: 0x001B1ED9
		internal static bool HasAtMostOneBitSet(int bits)
		{
			return (bits & (bits - 1)) == 0;
		}

		// Token: 0x060074D6 RID: 29910 RVA: 0x001B3CE4 File Offset: 0x001B1EE4
		internal void SpinWaitComplete(CompletionPart part, CancellationToken cancellationToken)
		{
			if (this.HasComplete(part))
			{
				return;
			}
			SpinWait spinWait = default(SpinWait);
			while (!this.HasComplete(part))
			{
				cancellationToken.ThrowIfCancellationRequested();
				spinWait.SpinOnce();
			}
		}

		// Token: 0x060074D7 RID: 29911 RVA: 0x001B3D1C File Offset: 0x001B1F1C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CompletionParts(");
			bool flag = false;
			int num = 0;
			for (;;)
			{
				int num2 = 1 << num;
				if ((num2 & 131071) == 0)
				{
					break;
				}
				if ((num2 & this._completeParts) != 0)
				{
					if (flag)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(num);
					flag = true;
				}
				num++;
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04001C62 RID: 7266
		private volatile int _completeParts;
	}
}
