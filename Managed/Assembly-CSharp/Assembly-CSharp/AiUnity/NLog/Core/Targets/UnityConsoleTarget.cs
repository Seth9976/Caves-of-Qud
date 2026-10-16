using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AiUnity.Common.Attributes;
using AiUnity.Common.Extensions;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Layouts;
using UnityEngine;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200022B RID: 555
	[Target("UnityConsole")]
	public class UnityConsoleTarget : TargetWithLayout
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001496 RID: 5270 RVA: 0x00073CBA File Offset: 0x00071EBA
		// (set) Token: 0x06001497 RID: 5271 RVA: 0x00073CC2 File Offset: 0x00071EC2
		[RequiredParameter]
		[DefaultValue("<color=olive>[${level}] ${callsite}</color>${newline}<color=black>${message}</color>${newline}<color=red>${exception}</color>")]
		[Display("Layout", "Specifies the layout and content of log message.  The + icon will present a list of variables that can be added.  For the Unity Console all excepted xml formating can be used (http://docs.unity3d.com/Manual/StyledText.html)", false, -100)]
		[Preserve]
		public override Layout Layout { get; set; }

		// Token: 0x06001498 RID: 5272 RVA: 0x00073CCB File Offset: 0x00071ECB
		public UnityConsoleTarget()
		{
			this.Layout = "<color=olive>[${level}] ${callsite}</color>${newline}<color=black>${message}</color>${newline}<color=red>${exception}</color>";
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00073CE4 File Offset: 0x00071EE4
		protected override void Write(LogEventInfo logEvent)
		{
			string text = this.Layout.Render(logEvent);
			MonoBehaviour monoBehaviour = logEvent.Context as MonoBehaviour;
			GameObject gameObject = ((monoBehaviour != null) ? monoBehaviour.gameObject : (logEvent.Context as GameObject));
			if (logEvent.Level.Has(LogLevels.Info) || logEvent.Level.Has(LogLevels.Debug) || logEvent.Level.Has(LogLevels.Trace))
			{
				Debug.Log(text, gameObject);
				return;
			}
			if (logEvent.Level.Has(LogLevels.Warn))
			{
				Debug.LogWarning(text, gameObject);
				return;
			}
			Debug.LogError(text, gameObject);
			if (logEvent.Level.Has(LogLevels.Assert) && Singleton<NLogManager>.Instance.AssertException)
			{
				Debug.Break();
				throw new AssertException(text, logEvent.Exception);
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00073DC0 File Offset: 0x00071FC0
		protected string FixUnityConsoleXML(string message)
		{
			Match match = Regex.Match(message.TrimEnd('\n'), "\\r?\\n").NextMatch();
			int index = match.Index;
			if (index > 0)
			{
				Stack<string> stack = new Stack<string>();
				string text = message.Substring(0, index);
				foreach (object obj in Regex.Matches(text, "(<(b|i|size|color)\\s*>)|(</(b|i|size|color)\\s*)"))
				{
					Match match2 = (Match)obj;
					if (string.IsNullOrEmpty(match2.Groups[1].Value))
					{
						if (stack.Count<string>() == 0)
						{
							return message;
						}
						stack.Pop();
					}
					else
					{
						stack.Push(match2.Groups[1].Value);
					}
				}
				if (stack.Count != 0)
				{
					StringBuilder stringBuilder = new StringBuilder(text);
					foreach (string text2 in stack)
					{
						Match match3 = Regex.Match(text2, "<(\\w+)[^<>]*>");
						stringBuilder.AppendFormat("</{0}>", match3.Groups[1].Value);
					}
					stringBuilder.AppendLine();
					stringBuilder.Append(string.Join("", stack.Reverse<string>().ToArray<string>()));
					stringBuilder.Append(message.Substring(index + match.Value.Length));
					return stringBuilder.ToString();
				}
				return message;
			}
			return message;
		}

		// Token: 0x04000D38 RID: 3384
		private const string LayoutDefault = "<color=olive>[${level}] ${callsite}</color>${newline}<color=black>${message}</color>${newline}<color=red>${exception}</color>";
	}
}
