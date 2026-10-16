using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002F3 RID: 755
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	public readonly struct TextChange : IEquatable<TextChange>
	{
		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x00063B77 File Offset: 0x00061D77
		[DataMember(Order = 0)]
		public TextSpan Span { get; }

		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600204C RID: 8268 RVA: 0x00063B7F File Offset: 0x00061D7F
		[Nullable(2)]
		[DataMember(Order = 1)]
		public string NewText
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00063B87 File Offset: 0x00061D87
		public TextChange(TextSpan span, string newText)
		{
			this = default(TextChange);
			if (newText == null)
			{
				throw new ArgumentNullException("newText");
			}
			this.Span = span;
			this.NewText = newText;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x00063BAC File Offset: 0x00061DAC
		public override string ToString()
		{
			return string.Format("{0}: {{ {1}, \"{2}\" }}", base.GetType().Name, this.Span, this.NewText);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00063BDE File Offset: 0x00061DDE
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is TextChange && this.Equals((TextChange)obj);
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x00063BF6 File Offset: 0x00061DF6
		public bool Equals(TextChange other)
		{
			return EqualityComparer<TextSpan>.Default.Equals(this.Span, other.Span) && EqualityComparer<string>.Default.Equals(this.NewText, other.NewText);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x00063C2C File Offset: 0x00061E2C
		public override int GetHashCode()
		{
			int hashCode = this.Span.GetHashCode();
			string newText = this.NewText;
			return Hash.Combine(hashCode, (newText != null) ? newText.GetHashCode() : 0);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x00063C64 File Offset: 0x00061E64
		public static bool operator ==(TextChange left, TextChange right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x00063C6E File Offset: 0x00061E6E
		public static bool operator !=(TextChange left, TextChange right)
		{
			return !(left == right);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x00063C7A File Offset: 0x00061E7A
		public static implicit operator TextChangeRange(TextChange change)
		{
			return new TextChangeRange(change.Span, change.NewText.Length);
		}

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x00063C94 File Offset: 0x00061E94
		public static IReadOnlyList<TextChange> NoChanges
		{
			get
			{
				return SpecializedCollections.EmptyReadOnlyList<TextChange>();
			}
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x00063C9C File Offset: 0x00061E9C
		internal string GetDebuggerDisplay()
		{
			string newText = this.NewText;
			string text;
			if (newText != null)
			{
				int length = newText.Length;
				if (length >= 10)
				{
					text = string.Format("(NewLength = {0})", length);
				}
				else
				{
					text = "\"" + this.NewText + "\"";
				}
			}
			else
			{
				text = "null";
			}
			string text2 = text;
			return string.Format("new TextChange(new TextSpan({0}, {1}), {2})", this.Span.Start, this.Span.Length, text2);
		}
	}
}
