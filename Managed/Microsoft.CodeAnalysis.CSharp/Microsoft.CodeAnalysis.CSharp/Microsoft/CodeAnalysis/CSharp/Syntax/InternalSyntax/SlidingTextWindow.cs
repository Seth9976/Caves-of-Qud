using System;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Syntax.InternalSyntax
{
	// Token: 0x02000366 RID: 870
	internal sealed class SlidingTextWindow : IDisposable
	{
		// Token: 0x06004ADE RID: 19166 RVA: 0x001363F8 File Offset: 0x001345F8
		public SlidingTextWindow(SourceText text)
		{
			this._text = text;
			this._basis = 0;
			this._offset = 0;
			this._textEnd = text.Length;
			this._strings = StringTable.GetInstance();
			this._characterWindow = SlidingTextWindow.s_windowPool.Allocate();
			this._lexemeStart = 0;
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x0013644E File Offset: 0x0013464E
		public void Dispose()
		{
			if (this._characterWindow != null)
			{
				SlidingTextWindow.s_windowPool.Free(this._characterWindow);
				this._characterWindow = null;
				this._strings.Free();
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x0013647A File Offset: 0x0013467A
		public SourceText Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06004AE1 RID: 19169 RVA: 0x00136482 File Offset: 0x00134682
		public int Position
		{
			get
			{
				return this._basis + this._offset;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x00136491 File Offset: 0x00134691
		public int Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x00136499 File Offset: 0x00134699
		public char[] CharacterWindow
		{
			get
			{
				return this._characterWindow;
			}
		}

		// Token: 0x17001293 RID: 4755
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x001364A1 File Offset: 0x001346A1
		public int LexemeRelativeStart
		{
			get
			{
				return this._lexemeStart;
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06004AE5 RID: 19173 RVA: 0x001364A9 File Offset: 0x001346A9
		public int CharacterWindowCount
		{
			get
			{
				return this._characterWindowCount;
			}
		}

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x001364B1 File Offset: 0x001346B1
		public int LexemeStartPosition
		{
			get
			{
				return this._basis + this._lexemeStart;
			}
		}

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06004AE7 RID: 19175 RVA: 0x001364C0 File Offset: 0x001346C0
		public int Width
		{
			get
			{
				return this._offset - this._lexemeStart;
			}
		}

		// Token: 0x06004AE8 RID: 19176 RVA: 0x001364CF File Offset: 0x001346CF
		public void Start()
		{
			this._lexemeStart = this._offset;
		}

		// Token: 0x06004AE9 RID: 19177 RVA: 0x001364E0 File Offset: 0x001346E0
		public void Reset(int position)
		{
			int num = position - this._basis;
			if (num >= 0 && num <= this._characterWindowCount)
			{
				this._offset = num;
				return;
			}
			int num2 = Math.Min(this._text.Length, position + this._characterWindow.Length) - position;
			num2 = Math.Max(num2, 0);
			if (num2 > 0)
			{
				this._text.CopyTo(position, this._characterWindow, 0, num2);
			}
			this._lexemeStart = 0;
			this._offset = 0;
			this._basis = position;
			this._characterWindowCount = num2;
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x00136564 File Offset: 0x00134764
		private bool MoreChars()
		{
			if (this._offset < this._characterWindowCount)
			{
				return true;
			}
			if (this.Position >= this._textEnd)
			{
				return false;
			}
			if (this._lexemeStart > this._characterWindowCount / 4)
			{
				Array.Copy(this._characterWindow, this._lexemeStart, this._characterWindow, 0, this._characterWindowCount - this._lexemeStart);
				this._characterWindowCount -= this._lexemeStart;
				this._offset -= this._lexemeStart;
				this._basis += this._lexemeStart;
				this._lexemeStart = 0;
			}
			if (this._characterWindowCount >= this._characterWindow.Length)
			{
				Array characterWindow = this._characterWindow;
				char[] array = new char[this._characterWindow.Length * 2];
				Array.Copy(characterWindow, 0, array, 0, this._characterWindowCount);
				this._characterWindow = array;
			}
			int num = Math.Min(this._textEnd - (this._basis + this._characterWindowCount), this._characterWindow.Length - this._characterWindowCount);
			this._text.CopyTo(this._basis + this._characterWindowCount, this._characterWindow, this._characterWindowCount, num);
			this._characterWindowCount += num;
			return num > 0;
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x001366A4 File Offset: 0x001348A4
		internal bool IsReallyAtEnd()
		{
			return this._offset >= this._characterWindowCount && this.Position >= this._textEnd;
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x001366C7 File Offset: 0x001348C7
		public void AdvanceChar()
		{
			this._offset++;
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x001366D7 File Offset: 0x001348D7
		public void AdvanceChar(int n)
		{
			this._offset += n;
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x001366E7 File Offset: 0x001348E7
		public char NextChar()
		{
			char c = this.PeekChar();
			if (c != '\uffff')
			{
				this.AdvanceChar();
			}
			return c;
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x001366FD File Offset: 0x001348FD
		public char PeekChar()
		{
			if (this._offset >= this._characterWindowCount && !this.MoreChars())
			{
				return char.MaxValue;
			}
			return this._characterWindow[this._offset];
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00136728 File Offset: 0x00134928
		public char PeekChar(int delta)
		{
			int position = this.Position;
			this.AdvanceChar(delta);
			char c;
			if (this._offset >= this._characterWindowCount && !this.MoreChars())
			{
				c = char.MaxValue;
			}
			else
			{
				c = this._characterWindow[this._offset];
			}
			this.Reset(position);
			return c;
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00136778 File Offset: 0x00134978
		public bool IsUnicodeEscape()
		{
			if (this.PeekChar() == '\\')
			{
				char c = this.PeekChar(1);
				if (c == 'U' || c == 'u')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x001367A4 File Offset: 0x001349A4
		public char PeekCharOrUnicodeEscape(out char surrogateCharacter)
		{
			if (this.IsUnicodeEscape())
			{
				return this.PeekUnicodeEscape(out surrogateCharacter);
			}
			surrogateCharacter = char.MaxValue;
			return this.PeekChar();
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x001367C4 File Offset: 0x001349C4
		public char PeekUnicodeEscape(out char surrogateCharacter)
		{
			int position = this.Position;
			SyntaxDiagnosticInfo syntaxDiagnosticInfo;
			char c = this.ScanUnicodeEscape(true, out surrogateCharacter, out syntaxDiagnosticInfo);
			this.Reset(position);
			return c;
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x001367EC File Offset: 0x001349EC
		public char NextCharOrUnicodeEscape(out char surrogateCharacter, out SyntaxDiagnosticInfo info)
		{
			char c = this.PeekChar();
			if (c == '\\')
			{
				char c2 = this.PeekChar(1);
				if (c2 == 'U' || c2 == 'u')
				{
					return this.ScanUnicodeEscape(false, out surrogateCharacter, out info);
				}
			}
			surrogateCharacter = char.MaxValue;
			info = null;
			this.AdvanceChar();
			return c;
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00136832 File Offset: 0x00134A32
		public char NextUnicodeEscape(out char surrogateCharacter, out SyntaxDiagnosticInfo info)
		{
			return this.ScanUnicodeEscape(false, out surrogateCharacter, out info);
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x00136840 File Offset: 0x00134A40
		private char ScanUnicodeEscape(bool peek, out char surrogateCharacter, out SyntaxDiagnosticInfo info)
		{
			surrogateCharacter = char.MaxValue;
			info = null;
			int position = this.Position;
			char c = this.PeekChar();
			this.AdvanceChar();
			c = this.PeekChar();
			if (c == 'U')
			{
				uint num = 0U;
				this.AdvanceChar();
				if (!SyntaxFacts.IsHexDigit(this.PeekChar()))
				{
					if (!peek)
					{
						info = this.CreateIllegalEscapeDiagnostic(position);
					}
				}
				else
				{
					int i = 0;
					while (i < 8)
					{
						c = this.PeekChar();
						if (!SyntaxFacts.IsHexDigit(c))
						{
							if (!peek)
							{
								info = this.CreateIllegalEscapeDiagnostic(position);
								break;
							}
							break;
						}
						else
						{
							num = (uint)((ulong)((ulong)num << 4) + (ulong)((long)SyntaxFacts.HexValue(c)));
							this.AdvanceChar();
							i++;
						}
					}
					if (num > 1114111U)
					{
						if (!peek)
						{
							info = this.CreateIllegalEscapeDiagnostic(position);
						}
					}
					else
					{
						c = SlidingTextWindow.GetCharsFromUtf32(num, out surrogateCharacter);
					}
				}
			}
			else
			{
				int num2 = 0;
				this.AdvanceChar();
				if (!SyntaxFacts.IsHexDigit(this.PeekChar()))
				{
					if (!peek)
					{
						info = this.CreateIllegalEscapeDiagnostic(position);
					}
				}
				else
				{
					int j = 0;
					while (j < 4)
					{
						char c2 = this.PeekChar();
						if (!SyntaxFacts.IsHexDigit(c2))
						{
							if (c == 'u' && !peek)
							{
								info = this.CreateIllegalEscapeDiagnostic(position);
								break;
							}
							break;
						}
						else
						{
							num2 = (num2 << 4) + SyntaxFacts.HexValue(c2);
							this.AdvanceChar();
							j++;
						}
					}
					c = (char)num2;
				}
			}
			return c;
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x00136974 File Offset: 0x00134B74
		public bool TryScanXmlEntity(out char ch, out char surrogate)
		{
			ch = '&';
			this.AdvanceChar();
			surrogate = char.MaxValue;
			char c = this.PeekChar();
			if (c <= 'a')
			{
				if (c != '#')
				{
					if (c == 'a')
					{
						if (this.AdvanceIfMatches("amp;"))
						{
							ch = '&';
							return true;
						}
						if (this.AdvanceIfMatches("apos;"))
						{
							ch = '\'';
							return true;
						}
					}
				}
				else
				{
					this.AdvanceChar();
					uint num = 0U;
					if (this.AdvanceIfMatches("x"))
					{
						char c2;
						while (SyntaxFacts.IsHexDigit(c2 = this.PeekChar()))
						{
							this.AdvanceChar();
							if (num > 134217727U)
							{
								return false;
							}
							num = (num << 4) + (uint)SyntaxFacts.HexValue(c2);
						}
					}
					else
					{
						char c3;
						while (SyntaxFacts.IsDecDigit(c3 = this.PeekChar()))
						{
							this.AdvanceChar();
							if (num > 134217727U)
							{
								return false;
							}
							num = (num << 3) + (num << 1) + (uint)SyntaxFacts.DecValue(c3);
						}
					}
					if (this.AdvanceIfMatches(";"))
					{
						ch = SlidingTextWindow.GetCharsFromUtf32(num, out surrogate);
						return true;
					}
				}
			}
			else if (c != 'g')
			{
				if (c != 'l')
				{
					if (c == 'q')
					{
						if (this.AdvanceIfMatches("quot;"))
						{
							ch = '"';
							return true;
						}
					}
				}
				else if (this.AdvanceIfMatches("lt;"))
				{
					ch = '<';
					return true;
				}
			}
			else if (this.AdvanceIfMatches("gt;"))
			{
				ch = '>';
				return true;
			}
			return false;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x00136AC0 File Offset: 0x00134CC0
		private bool AdvanceIfMatches(string desired)
		{
			int length = desired.Length;
			for (int i = 0; i < length; i++)
			{
				if (this.PeekChar(i) != desired.get_Chars(i))
				{
					return false;
				}
			}
			this.AdvanceChar(length);
			return true;
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x00136AFA File Offset: 0x00134CFA
		private SyntaxDiagnosticInfo CreateIllegalEscapeDiagnostic(int start)
		{
			return new SyntaxDiagnosticInfo(start - this.LexemeStartPosition, this.Position - start, ErrorCode.ERR_IllegalEscape);
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x00136B16 File Offset: 0x00134D16
		public string Intern(StringBuilder text)
		{
			return this._strings.Add(text);
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00136B24 File Offset: 0x00134D24
		public string Intern(char[] array, int start, int length)
		{
			return this._strings.Add(array, start, length);
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x00136B34 File Offset: 0x00134D34
		public string GetInternedText()
		{
			return this.Intern(this._characterWindow, this._lexemeStart, this.Width);
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x00136B4E File Offset: 0x00134D4E
		public string GetText(bool intern)
		{
			return this.GetText(this.LexemeStartPosition, this.Width, intern);
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x00136B64 File Offset: 0x00134D64
		public string GetText(int position, int length, bool intern)
		{
			int num = position - this._basis;
			switch (length)
			{
			case 0:
				return string.Empty;
			case 1:
				if (this._characterWindow[num] == ' ')
				{
					return " ";
				}
				if (this._characterWindow[num] == '\n')
				{
					return "\n";
				}
				break;
			case 2:
			{
				char c = this._characterWindow[num];
				if (c == '\r' && this._characterWindow[num + 1] == '\n')
				{
					return "\r\n";
				}
				if (c == '/' && this._characterWindow[num + 1] == '/')
				{
					return "//";
				}
				break;
			}
			case 3:
				if (this._characterWindow[num] == '/' && this._characterWindow[num + 1] == '/' && this._characterWindow[num + 2] == ' ')
				{
					return "// ";
				}
				break;
			}
			if (intern)
			{
				return this.Intern(this._characterWindow, num, length);
			}
			return new string(this._characterWindow, num, length);
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x00136C47 File Offset: 0x00134E47
		internal static char GetCharsFromUtf32(uint codepoint, out char lowSurrogate)
		{
			if (codepoint < 65536U)
			{
				lowSurrogate = char.MaxValue;
				return (char)codepoint;
			}
			lowSurrogate = (char)((codepoint - 65536U) % 1024U + 56320U);
			return (char)((codepoint - 65536U) / 1024U + 55296U);
		}

		// Token: 0x0400159D RID: 5533
		public const char InvalidCharacter = '\uffff';

		// Token: 0x0400159E RID: 5534
		private const int DefaultWindowLength = 2048;

		// Token: 0x0400159F RID: 5535
		private readonly SourceText _text;

		// Token: 0x040015A0 RID: 5536
		private int _basis;

		// Token: 0x040015A1 RID: 5537
		private int _offset;

		// Token: 0x040015A2 RID: 5538
		private readonly int _textEnd;

		// Token: 0x040015A3 RID: 5539
		private char[] _characterWindow;

		// Token: 0x040015A4 RID: 5540
		private int _characterWindowCount;

		// Token: 0x040015A5 RID: 5541
		private int _lexemeStart;

		// Token: 0x040015A6 RID: 5542
		private readonly StringTable _strings;

		// Token: 0x040015A7 RID: 5543
		private static readonly ObjectPool<char[]> s_windowPool = new ObjectPool<char[]>(() => new char[2048]);
	}
}
