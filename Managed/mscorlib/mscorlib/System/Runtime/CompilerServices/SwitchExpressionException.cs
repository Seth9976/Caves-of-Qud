using System;
using System.Runtime.Serialization;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000857 RID: 2135
	[Serializable]
	public sealed class SwitchExpressionException : InvalidOperationException
	{
		// Token: 0x0600472D RID: 18221 RVA: 0x000E7FD9 File Offset: 0x000E61D9
		public SwitchExpressionException()
			: base("Non-exhaustive switch expression failed to match its input.")
		{
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x000E7FE6 File Offset: 0x000E61E6
		public SwitchExpressionException(Exception innerException)
			: base("Non-exhaustive switch expression failed to match its input.", innerException)
		{
		}

		// Token: 0x0600472F RID: 18223 RVA: 0x000E7FF4 File Offset: 0x000E61F4
		public SwitchExpressionException(object unmatchedValue)
			: this()
		{
			this.UnmatchedValue = unmatchedValue;
		}

		// Token: 0x06004730 RID: 18224 RVA: 0x000E8003 File Offset: 0x000E6203
		private SwitchExpressionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.UnmatchedValue = info.GetValue("UnmatchedValue", typeof(object));
		}

		// Token: 0x06004731 RID: 18225 RVA: 0x000E8028 File Offset: 0x000E6228
		public SwitchExpressionException(string message)
			: base(message)
		{
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x000E8031 File Offset: 0x000E6231
		public SwitchExpressionException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06004733 RID: 18227 RVA: 0x000E803B File Offset: 0x000E623B
		public object UnmatchedValue { get; }

		// Token: 0x06004734 RID: 18228 RVA: 0x000E8043 File Offset: 0x000E6243
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("UnmatchedValue", this.UnmatchedValue, typeof(object));
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06004735 RID: 18229 RVA: 0x000E8068 File Offset: 0x000E6268
		public override string Message
		{
			get
			{
				if (this.UnmatchedValue == null)
				{
					return base.Message;
				}
				string text = SR.Format("Unmatched value was {0}.", this.UnmatchedValue.ToString());
				return base.Message + Environment.NewLine + text;
			}
		}
	}
}
