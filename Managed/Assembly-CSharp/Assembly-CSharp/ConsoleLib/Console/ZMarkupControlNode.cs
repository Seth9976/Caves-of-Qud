using System;
using System.Collections.Generic;
using Cysharp.Text;
using XRL;
using XRL.World.Text;

namespace ConsoleLib.Console
{
	// Token: 0x0200154D RID: 5453
	public class ZMarkupControlNode : IZMarkupNode
	{
		// Token: 0x0600D4A8 RID: 54440 RVA: 0x004F07ED File Offset: 0x004EE9ED
		public ZMarkupControlNode()
		{
		}

		// Token: 0x0600D4A9 RID: 54441 RVA: 0x004F0804 File Offset: 0x004EEA04
		public static ZMarkupControlNode next()
		{
			Queue<ZMarkupControlNode> queue = ZMarkupControlNode.markupControlNodePool;
			ZMarkupControlNode zmarkupControlNode;
			lock (queue)
			{
				zmarkupControlNode = ((ZMarkupControlNode.markupControlNodePool.Count > 0) ? ZMarkupControlNode.markupControlNodePool.Dequeue() : null) ?? new ZMarkupControlNode();
			}
			return zmarkupControlNode;
		}

		// Token: 0x0600D4AA RID: 54442 RVA: 0x004F0864 File Offset: 0x004EEA64
		public static ZMarkupControlNode next(ReadOnlySpan<char> text)
		{
			ZMarkupControlNode zmarkupControlNode = ZMarkupControlNode.next();
			zmarkupControlNode.set(text);
			return zmarkupControlNode;
		}

		// Token: 0x0600D4AB RID: 54443 RVA: 0x004F0874 File Offset: 0x004EEA74
		public override void release()
		{
			foreach (IZMarkupNode izmarkupNode in this.Children)
			{
				izmarkupNode.release();
			}
			this.Action = null;
			this.Children.Clear();
			Queue<ZMarkupControlNode> queue = ZMarkupControlNode.markupControlNodePool;
			lock (queue)
			{
				ZMarkupControlNode.markupControlNodePool.Enqueue(this);
			}
		}

		// Token: 0x0600D4AC RID: 54444 RVA: 0x004F090C File Offset: 0x004EEB0C
		public void set(ReadOnlySpan<char> Text)
		{
			this.Action = Text.ToString();
		}

		// Token: 0x0600D4AD RID: 54445 RVA: 0x004F0921 File Offset: 0x004EEB21
		public ZMarkupControlNode(string Action)
		{
			this.set(Action);
			this.Action = Action;
		}

		// Token: 0x0600D4AE RID: 54446 RVA: 0x004F0949 File Offset: 0x004EEB49
		public ZMarkupControlNode(string Action, List<IZMarkupNode> Children)
			: this(Action)
		{
			this.Children = Children;
		}

		// Token: 0x0600D4AF RID: 54447 RVA: 0x004F0959 File Offset: 0x004EEB59
		public void Add(ReadOnlySpan<char> text, int Start, int Length)
		{
			this.Children.Add(ZMarkupTextNode.next(text, Start, Length));
		}

		// Token: 0x0600D4B0 RID: 54448 RVA: 0x004F096E File Offset: 0x004EEB6E
		public void Add(ZMarkupControlNode node)
		{
			this.Children.Add(node);
		}

		// Token: 0x0600D4B1 RID: 54449 RVA: 0x004F097C File Offset: 0x004EEB7C
		public void ClearChildText()
		{
			int i = 0;
			int count = this.Children.Count;
			while (i < count)
			{
				ZMarkupTextNode zmarkupTextNode = this.Children[i] as ZMarkupTextNode;
				if (zmarkupTextNode != null)
				{
					zmarkupTextNode.Length = 0;
				}
				ZMarkupControlNode zmarkupControlNode = this.Children[i] as ZMarkupControlNode;
				if (zmarkupControlNode != null)
				{
					zmarkupControlNode.ClearChildText();
				}
				i++;
			}
		}

		// Token: 0x0600D4B2 RID: 54450 RVA: 0x004F09D8 File Offset: 0x004EEBD8
		private void AssembleCode(TextBuilder SB, char? foreground = null, char? background = null)
		{
			if (foreground != null)
			{
				SB.Append('&').Append(foreground);
			}
			if (background != null)
			{
				SB.Append('^').Append(background);
			}
		}

		// Token: 0x0600D4B3 RID: 54451 RVA: 0x004F0A0C File Offset: 0x004EEC0C
		private string AssembleCode(char? foreground = null, char? background = null)
		{
			TextBuilder markupBuilderSB = ZMarkupControlNode._markupBuilderSB;
			string text;
			lock (markupBuilderSB)
			{
				ZMarkupControlNode._markupBuilderSB.Length = 0;
				this.AssembleCode(ZMarkupControlNode._markupBuilderSB, foreground, background);
				text = ZMarkupControlNode._markupBuilderSB.ToString();
			}
			return text;
		}

		// Token: 0x0600D4B4 RID: 54452 RVA: 0x004F0A6C File Offset: 0x004EEC6C
		public unsafe override void ToTextBuilder(TextBuilder SB, ReadOnlySpan<char> Source, bool RefreshAtNewline, ref char? lastForeground, ref char? lastBackground)
		{
			IMarkupShader markupShader = null;
			if (!ZMarkupControlNode.WarnedAbout.Contains(this.Action))
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
					ZMarkupControlNode.WarnedAbout.Add(this.Action);
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
					ZMarkupTextNode zmarkupTextNode = this.Children[i] as ZMarkupTextNode;
					if (zmarkupTextNode != null)
					{
						num2 += zmarkupTextNode.GetTextLengthWithoutFormatting(Source);
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
				ZMarkupTextNode zmarkupTextNode2 = this.Children[j] as ZMarkupTextNode;
				if (zmarkupTextNode2 != null)
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
						zmarkupTextNode2.ToTextBuilder(SB, Source, RefreshAtNewline, ref lastForeground, ref lastBackground);
						c = lastForeground;
						c2 = lastBackground;
					}
					else
					{
						int num3 = 0;
						int textLengthWithoutFormatting = zmarkupTextNode2.GetTextLengthWithoutFormatting(Source);
						ReadOnlySpan<char> span = zmarkupTextNode2.GetSpan(Source);
						char? c3 = null;
						int k = 0;
						int length = zmarkupTextNode2.Length;
						while (k < length)
						{
							ReadOnlySpan<char> readOnlySpan = GraphemeSplitter.GetNextPrintableSpan(span, ref k);
							if (!readOnlySpan.IsEmpty)
							{
								if (*readOnlySpan[0] == 38 && k < length)
								{
									readOnlySpan = GraphemeSplitter.GetNextPrintableSpan(span, ref k);
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
											c = markupShader.GetForegroundColor((char)(*readOnlySpan[0]), num3, textLengthWithoutFormatting, num, num2);
											continue;
										}
										continue;
									}
								}
								else if (*readOnlySpan[0] == 94 && k < length)
								{
									readOnlySpan = GraphemeSplitter.GetNextPrintableSpan(span, ref k);
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
											c2 = markupShader.GetBackgroundColor((char)(*readOnlySpan[0]), num3, textLengthWithoutFormatting, num, num2);
											continue;
										}
										continue;
									}
								}
								char? foregroundColor = markupShader.GetForegroundColor((char)(*readOnlySpan[0]), num3, textLengthWithoutFormatting, num, num2);
								char? backgroundColor = markupShader.GetBackgroundColor((char)(*readOnlySpan[0]), num3, textLengthWithoutFormatting, num, num2);
								if (foregroundColor == null)
								{
									goto IL_0461;
								}
								char? c4 = foregroundColor;
								int? num4 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
								c4 = c;
								int? num5 = ((c4 != null) ? new int?((int)c4.GetValueOrDefault()) : null);
								if ((num4.GetValueOrDefault() == num5.GetValueOrDefault()) & (num4 != null == (num5 != null)))
								{
									goto IL_0461;
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
										goto IL_050A;
									}
								}
								this.AssembleCode(SB, foregroundColor, null);
								c = (lastForeground = foregroundColor);
								IL_050A:
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
								IL_0461:
								if (backgroundColor == null)
								{
									goto IL_050A;
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
									goto IL_050A;
								}
								goto IL_050A;
							}
						}
					}
				}
				else
				{
					flag = this.Children[j] is ZMarkupControlNode;
					this.Children[j].ToTextBuilder(SB, Source, RefreshAtNewline, ref lastForeground, ref lastBackground);
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

		// Token: 0x0600D4B5 RID: 54453 RVA: 0x004F123C File Offset: 0x004EF43C
		public override void ToTextBuilder(TextBuilder SB, ReadOnlySpan<char> Source, bool RefreshAtNewline)
		{
			char? c = null;
			char? c2 = new char?('k');
			this.ToTextBuilder(SB, Source, RefreshAtNewline, ref c, ref c2);
		}

		// Token: 0x0600D4B6 RID: 54454 RVA: 0x004F1268 File Offset: 0x004EF468
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

		// Token: 0x0600D4B7 RID: 54455 RVA: 0x004F12B0 File Offset: 0x004EF4B0
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

		// Token: 0x04005551 RID: 21841
		public string Action;

		// Token: 0x04005552 RID: 21842
		public List<IZMarkupNode> Children = new List<IZMarkupNode>(64);

		// Token: 0x04005553 RID: 21843
		private static Queue<ZMarkupControlNode> markupControlNodePool = new Queue<ZMarkupControlNode>();

		// Token: 0x04005554 RID: 21844
		private static readonly ZMarkupControlNode.Destructor Finalise = new ZMarkupControlNode.Destructor();

		// Token: 0x04005555 RID: 21845
		private static TextBuilder _markupBuilderSB = new CompositedTextBuilder(2048);

		// Token: 0x04005556 RID: 21846
		private static HashSet<string> WarnedAbout = new HashSet<string>();

		// Token: 0x02001E55 RID: 7765
		private sealed class Destructor
		{
			// Token: 0x0600F72B RID: 63275 RVA: 0x0055B404 File Offset: 0x00559604
			~Destructor()
			{
				ZMarkupControlNode._markupBuilderSB.Dispose(true);
			}
		}
	}
}
