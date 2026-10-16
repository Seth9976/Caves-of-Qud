using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Text;
using XRL;

namespace ConsoleLib.Console
{
	// Token: 0x0200153B RID: 5435
	public class MarkupControlNode : IMarkupNode
	{
		// Token: 0x0600D3C3 RID: 54211 RVA: 0x004EAC95 File Offset: 0x004E8E95
		public MarkupControlNode()
		{
		}

		// Token: 0x0600D3C4 RID: 54212 RVA: 0x004EACAC File Offset: 0x004E8EAC
		public static MarkupControlNode next()
		{
			Queue<MarkupControlNode> queue = MarkupControlNode.markupControlNodePool;
			MarkupControlNode markupControlNode;
			lock (queue)
			{
				markupControlNode = ((MarkupControlNode.markupControlNodePool.Count > 0) ? MarkupControlNode.markupControlNodePool.Dequeue() : null) ?? new MarkupControlNode();
			}
			return markupControlNode;
		}

		// Token: 0x0600D3C5 RID: 54213 RVA: 0x004EAD0C File Offset: 0x004E8F0C
		public static MarkupControlNode next(ReadOnlySpan<char> text)
		{
			MarkupControlNode markupControlNode = MarkupControlNode.next();
			markupControlNode.set(text);
			return markupControlNode;
		}

		// Token: 0x0600D3C6 RID: 54214 RVA: 0x004EAD1C File Offset: 0x004E8F1C
		public override void release()
		{
			foreach (IMarkupNode markupNode in this.Children)
			{
				markupNode.release();
			}
			this.Action = null;
			this.Children.Clear();
			Queue<MarkupControlNode> queue = MarkupControlNode.markupControlNodePool;
			lock (queue)
			{
				MarkupControlNode.markupControlNodePool.Enqueue(this);
			}
		}

		// Token: 0x0600D3C7 RID: 54215 RVA: 0x004EADB4 File Offset: 0x004E8FB4
		public void set(ReadOnlySpan<char> Text)
		{
			this.Action = Text.ToString();
		}

		// Token: 0x0600D3C8 RID: 54216 RVA: 0x004EADC9 File Offset: 0x004E8FC9
		public MarkupControlNode(string Action)
		{
			this.set(Action);
			this.Action = Action;
		}

		// Token: 0x0600D3C9 RID: 54217 RVA: 0x004EADF1 File Offset: 0x004E8FF1
		public MarkupControlNode(string Action, List<IMarkupNode> Children)
			: this(Action)
		{
			this.Children = Children;
		}

		// Token: 0x0600D3CA RID: 54218 RVA: 0x004EAE01 File Offset: 0x004E9001
		public void Add(ReadOnlySpan<char> text)
		{
			this.Children.Add(MarkupTextNode.next(text));
		}

		// Token: 0x0600D3CB RID: 54219 RVA: 0x004EAE14 File Offset: 0x004E9014
		public void Add(MarkupControlNode node)
		{
			this.Children.Add(node);
		}

		// Token: 0x0600D3CC RID: 54220 RVA: 0x004EAE24 File Offset: 0x004E9024
		public void ClearChildText()
		{
			int i = 0;
			int count = this.Children.Count;
			while (i < count)
			{
				MarkupTextNode markupTextNode = this.Children[i] as MarkupTextNode;
				if (markupTextNode != null)
				{
					markupTextNode.Text = "";
				}
				MarkupControlNode markupControlNode = this.Children[i] as MarkupControlNode;
				if (markupControlNode != null)
				{
					markupControlNode.ClearChildText();
				}
				i++;
			}
		}

		// Token: 0x0600D3CD RID: 54221 RVA: 0x004EAE84 File Offset: 0x004E9084
		public static string charToString(char? c)
		{
			if (c == null)
			{
				return null;
			}
			string text = null;
			if (MarkupControlNode._charToString.TryGetValue(c, out text))
			{
				return text;
			}
			string text2 = c.ToString();
			MarkupControlNode._charToString.Add(c, text2);
			return text2;
		}

		// Token: 0x0600D3CE RID: 54222 RVA: 0x004EAECC File Offset: 0x004E90CC
		private void AssembleCode(StringBuilder SB, char? foreground = null, char? background = null)
		{
			if (foreground != null)
			{
				SB.Append(MarkupControlNode.charToString(new char?('&'))).Append(MarkupControlNode.charToString(foreground));
			}
			if (background != null)
			{
				SB.Append(MarkupControlNode.charToString(new char?('^'))).Append(MarkupControlNode.charToString(background));
			}
		}

		// Token: 0x0600D3CF RID: 54223 RVA: 0x004EAF28 File Offset: 0x004E9128
		private string AssembleCode(char? foreground = null, char? background = null)
		{
			StringBuilder markupBuilderSB = MarkupControlNode._markupBuilderSB;
			string text;
			lock (markupBuilderSB)
			{
				MarkupControlNode._markupBuilderSB.Length = 0;
				this.AssembleCode(MarkupControlNode._markupBuilderSB, foreground, background);
				text = MarkupControlNode._markupBuilderSB.ToString();
			}
			return text;
		}

		// Token: 0x0600D3D0 RID: 54224 RVA: 0x004EAF88 File Offset: 0x004E9188
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x0600D3D1 RID: 54225 RVA: 0x004EAF94 File Offset: 0x004E9194
		public override string ToString(bool RefreshAtNewline)
		{
			StringBuilder markupBuilderSB = MarkupControlNode._markupBuilderSB;
			string text;
			lock (markupBuilderSB)
			{
				MarkupControlNode._markupBuilderSB.Clear();
				this.ToStringBuilder(MarkupControlNode._markupBuilderSB, RefreshAtNewline);
				text = MarkupControlNode._markupBuilderSB.ToString();
			}
			return text;
		}

		// Token: 0x0600D3D2 RID: 54226 RVA: 0x004EAFF0 File Offset: 0x004E91F0
		public unsafe override void ToStringBuilder(StringBuilder SB, bool RefreshAtNewline, ref char? lastForeground, ref char? lastBackground)
		{
			IMarkupShader markupShader = null;
			if (!MarkupControlNode.WarnedAbout.Contains(this.Action))
			{
				try
				{
					if (!string.IsNullOrEmpty(this.Action))
					{
						if (this.Action == "?KB")
						{
							if (ControlManager.activeControllerType == ControlManager.InputDeviceType.Gamepad)
							{
								this.ClearChildText();
							}
							markupShader = MarkupShaders.Find("y");
						}
						else if (this.Action == "?Gamepad")
						{
							if (ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad)
							{
								this.ClearChildText();
							}
							markupShader = MarkupShaders.Find("y");
						}
						else
						{
							markupShader = MarkupShaders.Find(this.Action);
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogError(new Exception("Error finding Markup Shader \"" + this.Action + "\"", ex));
					MarkupControlNode.WarnedAbout.Add(this.Action);
				}
			}
			int num = 0;
			int num2 = 0;
			if (markupShader != null)
			{
				int i = 0;
				int count = this.Children.Count;
				while (i < count)
				{
					MarkupTextNode markupTextNode = this.Children[i] as MarkupTextNode;
					if (markupTextNode != null)
					{
						num2 += markupTextNode.TextLength;
					}
					i++;
				}
			}
			bool flag = false;
			char? c = lastForeground;
			char? c2 = lastBackground;
			int j = 0;
			int count2 = this.Children.Count;
			while (j < count2)
			{
				MarkupTextNode markupTextNode2 = this.Children[j] as MarkupTextNode;
				if (markupTextNode2 != null)
				{
					if (flag)
					{
						if (markupShader == null && !string.IsNullOrEmpty(this.Action))
						{
							SB.Append(this.Action);
						}
						flag = false;
					}
					if (markupShader == null)
					{
						markupTextNode2.ToStringBuilder(SB, RefreshAtNewline, ref lastForeground, ref lastBackground);
						c = lastForeground;
						c2 = lastBackground;
					}
					else
					{
						int num3 = 0;
						int textLength = markupTextNode2.TextLength;
						char? c3 = null;
						int k = 0;
						int length = markupTextNode2.Text.Length;
						while (k < length)
						{
							ReadOnlySpan<char> readOnlySpan = GraphemeSplitter.GetNextPrintableSpan(markupTextNode2.Text, ref k);
							if (!readOnlySpan.IsEmpty)
							{
								if (*readOnlySpan[0] == 38 && k < length)
								{
									readOnlySpan = GraphemeSplitter.GetNextPrintableSpan(markupTextNode2.Text, ref k);
									if (*readOnlySpan[0] == 38)
									{
										c3 = new char?('&');
									}
									else
									{
										SB.Append('&').Append(readOnlySpan);
										lastForeground = new char?((char)(*readOnlySpan[0]));
										if (num3 == 0)
										{
											c = markupShader.GetForegroundColor((char)(*readOnlySpan[0]), num3, textLength, num, num2);
											continue;
										}
										continue;
									}
								}
								else if (*readOnlySpan[0] == 94 && k < length)
								{
									readOnlySpan = GraphemeSplitter.GetNextPrintableSpan(markupTextNode2.Text, ref k);
									if (*readOnlySpan[0] == 94)
									{
										c3 = new char?('^');
									}
									else
									{
										SB.Append('^').Append(readOnlySpan);
										lastBackground = new char?((char)(*readOnlySpan[0]));
										if (num3 == 0)
										{
											c2 = markupShader.GetBackgroundColor((char)(*readOnlySpan[0]), num3, textLength, num, num2);
											continue;
										}
										continue;
									}
								}
								char? foregroundColor = markupShader.GetForegroundColor((char)(*readOnlySpan[0]), num3, textLength, num, num2);
								char? backgroundColor = markupShader.GetBackgroundColor((char)(*readOnlySpan[0]), num3, textLength, num, num2);
								if (foregroundColor == null)
								{
									goto IL_0471;
								}
								char? c4 = foregroundColor;
								int? num4 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
								c4 = c;
								int? num5 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
								if ((num4.GetValueOrDefault() == num5.GetValueOrDefault()) & (num4 != null == (num5 != null)))
								{
									goto IL_0471;
								}
								if (backgroundColor != null)
								{
									c4 = backgroundColor;
									num5 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
									c4 = c2;
									num4 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
									if (!((num5.GetValueOrDefault() == num4.GetValueOrDefault()) & (num5 != null == (num4 != null))))
									{
										this.AssembleCode(SB, foregroundColor, backgroundColor);
										c = (lastForeground = foregroundColor);
										c2 = (lastBackground = backgroundColor);
										goto IL_051A;
									}
								}
								this.AssembleCode(SB, foregroundColor, null);
								c = (lastForeground = foregroundColor);
								IL_051A:
								SB.Append(readOnlySpan);
								if (c3 != null)
								{
									SB.Append(c3);
									c3 = null;
								}
								if (RefreshAtNewline && *readOnlySpan[0] == 10)
								{
									c4 = foregroundColor;
									num5 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
									int num6 = 121;
									char? c5 = (((num5.GetValueOrDefault() == num6) & (num5 != null)) ? null : foregroundColor);
									c4 = backgroundColor;
									num5 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
									num6 = 107;
									this.AssembleCode(SB, c5, ((num5.GetValueOrDefault() == num6) & (num5 != null)) ? null : backgroundColor);
								}
								num3++;
								num++;
								continue;
								IL_0471:
								if (backgroundColor == null)
								{
									goto IL_051A;
								}
								c4 = backgroundColor;
								num4 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
								c4 = lastBackground;
								num5 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
								if (!((num4.GetValueOrDefault() == num5.GetValueOrDefault()) & (num4 != null == (num5 != null))))
								{
									c4 = backgroundColor;
									this.AssembleCode(SB, null, c4);
									c2 = (lastBackground = backgroundColor);
									goto IL_051A;
								}
								goto IL_051A;
							}
						}
					}
				}
				else
				{
					flag = this.Children[j] is MarkupControlNode;
					this.Children[j].ToStringBuilder(SB, RefreshAtNewline, ref lastForeground, ref lastBackground);
					if (markupShader == null && string.IsNullOrEmpty(this.Action))
					{
						char? c4 = c;
						int? num5 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
						c4 = lastForeground;
						int? num4 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
						if (!((num5.GetValueOrDefault() == num4.GetValueOrDefault()) & (num5 != null == (num4 != null))))
						{
							SB.Append('&').Append(c ?? 'y');
							lastForeground = c;
						}
						c4 = c2;
						num4 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
						c4 = lastBackground;
						num5 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
						if (!((num4.GetValueOrDefault() == num5.GetValueOrDefault()) & (num4 != null == (num5 != null))))
						{
							SB.Append('^').Append(c2 ?? 'k');
							lastBackground = c2;
						}
					}
					else
					{
						c = lastForeground;
						c2 = lastBackground;
					}
				}
				j++;
			}
		}

		// Token: 0x0600D3D3 RID: 54227 RVA: 0x004EB7D0 File Offset: 0x004E99D0
		public override void ToStringBuilder(StringBuilder SB, bool RefreshAtNewline)
		{
			char? c = null;
			char? c2 = new char?('k');
			this.ToStringBuilder(SB, RefreshAtNewline, ref c, ref c2);
		}

		// Token: 0x0600D3D4 RID: 54228 RVA: 0x004EB7FC File Offset: 0x004E99FC
		public override string DebugDump(int depth = 0)
		{
			Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder();
			string text;
			try
			{
				this.DebugDump(ref utf16ValueStringBuilder, depth);
				text = utf16ValueStringBuilder.ToString();
			}
			finally
			{
				utf16ValueStringBuilder.Dispose();
			}
			return text;
		}

		// Token: 0x0600D3D5 RID: 54229 RVA: 0x004EB844 File Offset: 0x004E9A44
		public override void DebugDump(ref Utf16ValueStringBuilder SB, int depth)
		{
			int i = 0;
			int num = depth * 2;
			while (i < num)
			{
				SB.Append('_');
				i++;
			}
			SB.Append("control node, ");
			SB.Append(this.Children.Count.Things("child", "children"));
			SB.Append(":");
			SB.Append((this.Action == null) ? "null" : this.Action.Replace("&", "&&").Replace("^", "^^"));
			SB.Append('\n');
			int j = 0;
			int count = this.Children.Count;
			while (j < count)
			{
				this.Children[j].DebugDump(ref SB, depth + 1);
				j++;
			}
		}

		// Token: 0x040054F8 RID: 21752
		public string Action;

		// Token: 0x040054F9 RID: 21753
		public List<IMarkupNode> Children = new List<IMarkupNode>(64);

		// Token: 0x040054FA RID: 21754
		private static Queue<MarkupControlNode> markupControlNodePool = new Queue<MarkupControlNode>();

		// Token: 0x040054FB RID: 21755
		[NonSerialized]
		public static Dictionary<char?, string> _charToString = new Dictionary<char?, string>();

		// Token: 0x040054FC RID: 21756
		private static StringBuilder _markupBuilderSB = new StringBuilder(2048);

		// Token: 0x040054FD RID: 21757
		private static HashSet<string> WarnedAbout = new HashSet<string>();
	}
}
