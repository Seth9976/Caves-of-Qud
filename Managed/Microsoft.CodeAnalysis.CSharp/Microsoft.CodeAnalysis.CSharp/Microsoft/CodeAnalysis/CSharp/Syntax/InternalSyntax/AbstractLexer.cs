using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000357 RID: 855
	internal class AbstractLexer : IDisposable
	{
		// Token: 0x06004814 RID: 18452 RVA: 0x0011B30D File Offset: 0x0011950D
		protected AbstractLexer(SourceText text)
		{
			this.TextWindow = new SlidingTextWindow(text);
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x0011B321 File Offset: 0x00119521
		public virtual void Dispose()
		{
			this.TextWindow.Dispose();
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x0011B32E File Offset: 0x0011952E
		protected void Start()
		{
			this.TextWindow.Start();
			this._errors = null;
		}

		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06004817 RID: 18455 RVA: 0x0011B342 File Offset: 0x00119542
		protected bool HasErrors
		{
			get
			{
				return this._errors != null;
			}
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x0011B350 File Offset: 0x00119550
		protected SyntaxDiagnosticInfo[] GetErrors(int leadingTriviaWidth)
		{
			if (this._errors == null)
			{
				return null;
			}
			if (leadingTriviaWidth > 0)
			{
				SyntaxDiagnosticInfo[] array = new SyntaxDiagnosticInfo[this._errors.Count];
				for (int i = 0; i < this._errors.Count; i++)
				{
					array[i] = this._errors[i].WithOffset(this._errors[i].Offset + leadingTriviaWidth);
				}
				return array;
			}
			return this._errors.ToArray();
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x0011B3C6 File Offset: 0x001195C6
		protected void AddError(int position, int width, ErrorCode code)
		{
			this.AddError(this.MakeError(position, width, code));
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x0011B3D7 File Offset: 0x001195D7
		protected void AddError(int position, int width, ErrorCode code, params object[] args)
		{
			this.AddError(this.MakeError(position, width, code, args));
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0011B3EA File Offset: 0x001195EA
		protected void AddError(int position, int width, XmlParseErrorCode code, params object[] args)
		{
			this.AddError(this.MakeError(position, width, code, args));
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x0011B3FD File Offset: 0x001195FD
		protected void AddError(ErrorCode code)
		{
			this.AddError(AbstractLexer.MakeError(code));
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x0011B40B File Offset: 0x0011960B
		protected void AddError(ErrorCode code, params object[] args)
		{
			this.AddError(AbstractLexer.MakeError(code, args));
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x0011B41A File Offset: 0x0011961A
		protected void AddError(XmlParseErrorCode code)
		{
			this.AddError(AbstractLexer.MakeError(code));
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0011B428 File Offset: 0x00119628
		protected void AddError(XmlParseErrorCode code, params object[] args)
		{
			this.AddError(AbstractLexer.MakeError(code, args));
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x0011B437 File Offset: 0x00119637
		protected void AddError(SyntaxDiagnosticInfo error)
		{
			if (error != null)
			{
				if (this._errors == null)
				{
					this._errors = new List<SyntaxDiagnosticInfo>(8);
				}
				this._errors.Add(error);
			}
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x0011B45C File Offset: 0x0011965C
		protected SyntaxDiagnosticInfo MakeError(int position, int width, ErrorCode code)
		{
			return new SyntaxDiagnosticInfo(this.GetLexemeOffsetFromPosition(position), width, code);
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x0011B46C File Offset: 0x0011966C
		protected SyntaxDiagnosticInfo MakeError(int position, int width, ErrorCode code, params object[] args)
		{
			return new SyntaxDiagnosticInfo(this.GetLexemeOffsetFromPosition(position), width, code, args);
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x0011B47E File Offset: 0x0011967E
		protected XmlSyntaxDiagnosticInfo MakeError(int position, int width, XmlParseErrorCode code, params object[] args)
		{
			return new XmlSyntaxDiagnosticInfo(this.GetLexemeOffsetFromPosition(position), width, code, args);
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x0011B490 File Offset: 0x00119690
		private int GetLexemeOffsetFromPosition(int position)
		{
			if (position < this.TextWindow.LexemeStartPosition)
			{
				return position;
			}
			return position - this.TextWindow.LexemeStartPosition;
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x0011B4AF File Offset: 0x001196AF
		protected static SyntaxDiagnosticInfo MakeError(ErrorCode code)
		{
			return new SyntaxDiagnosticInfo(code);
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x0011B4B7 File Offset: 0x001196B7
		protected static SyntaxDiagnosticInfo MakeError(ErrorCode code, params object[] args)
		{
			return new SyntaxDiagnosticInfo(code, args);
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x0011B4C0 File Offset: 0x001196C0
		protected static XmlSyntaxDiagnosticInfo MakeError(XmlParseErrorCode code)
		{
			return new XmlSyntaxDiagnosticInfo(0, 0, code, Array.Empty<object>());
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x0011B4CF File Offset: 0x001196CF
		protected static XmlSyntaxDiagnosticInfo MakeError(XmlParseErrorCode code, params object[] args)
		{
			return new XmlSyntaxDiagnosticInfo(0, 0, code, args);
		}

		// Token: 0x0400152D RID: 5421
		internal readonly SlidingTextWindow TextWindow;

		// Token: 0x0400152E RID: 5422
		private List<SyntaxDiagnosticInfo> _errors;
	}
}
