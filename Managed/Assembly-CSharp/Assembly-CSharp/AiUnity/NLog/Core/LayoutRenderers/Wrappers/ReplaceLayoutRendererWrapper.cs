using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers.Wrappers
{
	// Token: 0x0200025C RID: 604
	[LayoutRenderer("replace", true)]
	[ThreadAgnostic]
	[Preserve]
	public sealed class ReplaceLayoutRendererWrapper : WrapperLayoutRendererBase
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00077C37 File Offset: 0x00075E37
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x00077C3F File Offset: 0x00075E3F
		public string SearchFor { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x00077C48 File Offset: 0x00075E48
		// (set) Token: 0x06001606 RID: 5638 RVA: 0x00077C50 File Offset: 0x00075E50
		public bool Regex { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00077C59 File Offset: 0x00075E59
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x00077C61 File Offset: 0x00075E61
		public string ReplaceWith { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00077C6A File Offset: 0x00075E6A
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x00077C72 File Offset: 0x00075E72
		public string ReplaceGroupName { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600160B RID: 5643 RVA: 0x00077C7B File Offset: 0x00075E7B
		// (set) Token: 0x0600160C RID: 5644 RVA: 0x00077C83 File Offset: 0x00075E83
		public bool IgnoreCase { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x00077C8C File Offset: 0x00075E8C
		// (set) Token: 0x0600160E RID: 5646 RVA: 0x00077C94 File Offset: 0x00075E94
		public bool WholeWords { get; set; }

		// Token: 0x0600160F RID: 5647 RVA: 0x00077CA0 File Offset: 0x00075EA0
		protected override void InitializeLayoutRenderer()
		{
			base.InitializeLayoutRenderer();
			string text = this.SearchFor;
			if (!this.Regex)
			{
				text = global::System.Text.RegularExpressions.Regex.Escape(text);
			}
			RegexOptions regexOptions = RegexOptions.None;
			if (this.IgnoreCase)
			{
				regexOptions |= RegexOptions.IgnoreCase;
			}
			if (this.WholeWords)
			{
				text = "\\b" + text + "\\b";
			}
			this.regex = new Regex(text, regexOptions);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00077D00 File Offset: 0x00075F00
		protected override string Transform(string text)
		{
			ReplaceLayoutRendererWrapper.Replacer replacer = new ReplaceLayoutRendererWrapper.Replacer(text, this.ReplaceGroupName, this.ReplaceWith);
			if (!string.IsNullOrEmpty(this.ReplaceGroupName))
			{
				return this.regex.Replace(text, new MatchEvaluator(replacer.EvaluateMatch));
			}
			return this.regex.Replace(text, this.ReplaceWith);
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00077D58 File Offset: 0x00075F58
		public static string ReplaceNamedGroup(string input, string groupName, string replacement, Match match)
		{
			StringBuilder stringBuilder = new StringBuilder(input);
			int index = match.Index;
			int num = match.Length;
			foreach (Capture capture in from c in match.Groups[groupName].Captures.OfType<Capture>()
				orderby c.Index descending
				select c)
			{
				if (capture != null)
				{
					num += replacement.Length - capture.Length;
					stringBuilder.Remove(capture.Index, capture.Length);
					stringBuilder.Insert(capture.Index, replacement);
				}
			}
			int num2 = index + num;
			stringBuilder.Remove(num2, stringBuilder.Length - num2);
			stringBuilder.Remove(0, index);
			return stringBuilder.ToString();
		}

		// Token: 0x04000DC6 RID: 3526
		private Regex regex;

		// Token: 0x02001673 RID: 5747
		[ThreadAgnostic]
		public class Replacer
		{
			// Token: 0x0600D81C RID: 55324 RVA: 0x004FD670 File Offset: 0x004FB870
			internal Replacer(string text, string replaceGroupName, string replaceWith)
			{
				this.text = text;
				this.replaceGroupName = replaceGroupName;
				this.replaceWith = replaceWith;
			}

			// Token: 0x0600D81D RID: 55325 RVA: 0x004FD68D File Offset: 0x004FB88D
			internal string EvaluateMatch(Match match)
			{
				return ReplaceLayoutRendererWrapper.ReplaceNamedGroup(this.text, this.replaceGroupName, this.replaceWith, match);
			}

			// Token: 0x04005A75 RID: 23157
			private readonly string text;

			// Token: 0x04005A76 RID: 23158
			private readonly string replaceGroupName;

			// Token: 0x04005A77 RID: 23159
			private readonly string replaceWith;
		}
	}
}
