using System;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x02000298 RID: 664
	public class PatternMatch
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0007B4C0 File Offset: 0x000796C0
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06001792 RID: 6034 RVA: 0x0007B4C7 File Offset: 0x000796C7
		// (set) Token: 0x06001793 RID: 6035 RVA: 0x0007B4D0 File Offset: 0x000796D0
		public string LoggerNamePattern
		{
			get
			{
				return this.loggerNamePattern;
			}
			set
			{
				this.loggerNamePattern = value;
				int num = this.loggerNamePattern.IndexOf('*');
				int num2 = this.loggerNamePattern.LastIndexOf('*');
				if (num < 0)
				{
					this.loggerNameMatchMode = PatternMatch.MatchMode.Equals;
					this.loggerNameMatchArgument = value;
					return;
				}
				if (num == num2)
				{
					string text = this.LoggerNamePattern.Substring(0, num);
					string text2 = this.LoggerNamePattern.Substring(num + 1);
					if (text.Length > 0)
					{
						this.loggerNameMatchMode = PatternMatch.MatchMode.StartsWith;
						this.loggerNameMatchArgument = text;
						return;
					}
					if (text2.Length > 0)
					{
						this.loggerNameMatchMode = PatternMatch.MatchMode.EndsWith;
						this.loggerNameMatchArgument = text2;
						return;
					}
					return;
				}
				else
				{
					if (num == 0 && num2 == this.LoggerNamePattern.Length - 1)
					{
						string text3 = this.LoggerNamePattern.Substring(1, this.LoggerNamePattern.Length - 2);
						this.loggerNameMatchMode = PatternMatch.MatchMode.Contains;
						this.loggerNameMatchArgument = text3;
						return;
					}
					this.loggerNameMatchMode = PatternMatch.MatchMode.None;
					this.loggerNameMatchArgument = string.Empty;
					return;
				}
			}
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0007B5B5 File Offset: 0x000797B5
		public PatternMatch(string loggerNamePattern = "*")
		{
			this.LoggerNamePattern = loggerNamePattern;
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0007B5C4 File Offset: 0x000797C4
		public bool NameMatches(string loggerName)
		{
			PatternMatch.Logger.Debug("Matching rule = {0} (Pattern={1}) LoggerName={2}", new object[] { this.loggerNameMatchArgument, this.loggerNamePattern, loggerName });
			switch (this.loggerNameMatchMode)
			{
			case PatternMatch.MatchMode.All:
				return true;
			case PatternMatch.MatchMode.Equals:
				return loggerName.Equals(this.loggerNameMatchArgument, StringComparison.Ordinal);
			case PatternMatch.MatchMode.StartsWith:
				return loggerName.StartsWith(this.loggerNameMatchArgument, StringComparison.Ordinal);
			case PatternMatch.MatchMode.EndsWith:
				return loggerName.EndsWith(this.loggerNameMatchArgument, StringComparison.Ordinal);
			case PatternMatch.MatchMode.Contains:
				return loggerName.IndexOf(this.loggerNameMatchArgument, StringComparison.Ordinal) >= 0;
			}
			return false;
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0007B664 File Offset: 0x00079864
		public override string ToString()
		{
			return string.Format("logNamePattern: ({0}:{1})", this.loggerNameMatchArgument, this.loggerNameMatchMode);
		}

		// Token: 0x04000E2B RID: 3627
		private string loggerNamePattern;

		// Token: 0x04000E2C RID: 3628
		private PatternMatch.MatchMode loggerNameMatchMode;

		// Token: 0x04000E2D RID: 3629
		private string loggerNameMatchArgument;

		// Token: 0x02001682 RID: 5762
		internal enum MatchMode
		{
			// Token: 0x04005A8A RID: 23178
			All,
			// Token: 0x04005A8B RID: 23179
			None,
			// Token: 0x04005A8C RID: 23180
			Equals,
			// Token: 0x04005A8D RID: 23181
			StartsWith,
			// Token: 0x04005A8E RID: 23182
			EndsWith,
			// Token: 0x04005A8F RID: 23183
			Contains
		}
	}
}
