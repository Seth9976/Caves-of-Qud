using System;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200004F RID: 79
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "UnityEngine.IMGUIModule", "UnityEditor.GraphToolsFoundationModule" })]
	internal readonly struct RenderedText : IEquatable<RenderedText>, IEquatable<string>
	{
		// Token: 0x06000238 RID: 568 RVA: 0x0001F62B File Offset: 0x0001D82B
		public RenderedText(string value)
		{
			this = new RenderedText(value, 0, (value != null) ? value.Length : 0, null);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0001F644 File Offset: 0x0001D844
		public RenderedText(string value, string suffix)
		{
			this = new RenderedText(value, 0, (value != null) ? value.Length : 0, suffix);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0001F660 File Offset: 0x0001D860
		public RenderedText(string value, int start, int length, string suffix = null)
		{
			bool flag = string.IsNullOrEmpty(value);
			if (flag)
			{
				start = 0;
				length = 0;
			}
			else
			{
				bool flag2 = start < 0;
				if (flag2)
				{
					start = 0;
				}
				else
				{
					bool flag3 = start >= value.Length;
					if (flag3)
					{
						start = value.Length;
						length = 0;
					}
				}
				bool flag4 = length < 0;
				if (flag4)
				{
					length = 0;
				}
				else
				{
					bool flag5 = length > value.Length - start;
					if (flag5)
					{
						length = value.Length - start;
					}
				}
			}
			this.value = value;
			this.valueStart = start;
			this.valueLength = length;
			this.suffix = suffix;
			this.repeat = '\0';
			this.repeatCount = 0;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0001F704 File Offset: 0x0001D904
		public RenderedText(char repeat, int repeatCount, string suffix = null)
		{
			bool flag = repeatCount < 0;
			if (flag)
			{
				repeatCount = 0;
			}
			this.value = null;
			this.valueStart = 0;
			this.valueLength = 0;
			this.suffix = suffix;
			this.repeat = repeat;
			this.repeatCount = repeatCount;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0001F748 File Offset: 0x0001D948
		public int CharacterCount
		{
			get
			{
				int num = this.valueLength + this.repeatCount;
				bool flag = this.suffix != null;
				if (flag)
				{
					num += this.suffix.Length;
				}
				return num;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0001F784 File Offset: 0x0001D984
		public RenderedText.Enumerator GetEnumerator()
		{
			return new RenderedText.Enumerator(in this);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001F78C File Offset: 0x0001D98C
		public string CreateString()
		{
			char[] array = new char[this.CharacterCount];
			int num = 0;
			foreach (char c in this)
			{
				array[num++] = c;
			}
			return new string(array);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001F7D8 File Offset: 0x0001D9D8
		public bool Equals(RenderedText other)
		{
			return this.value == other.value && this.valueStart == other.valueStart && this.valueLength == other.valueLength && this.suffix == other.suffix && this.repeat == other.repeat && this.repeatCount == other.repeatCount;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001F84C File Offset: 0x0001DA4C
		public bool Equals(string other)
		{
			int num = ((other != null) ? other.Length : 0);
			int characterCount = this.CharacterCount;
			bool flag = num != characterCount;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = num == 0;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					int num2 = 0;
					foreach (char c in this)
					{
						bool flag4 = c != other[num2++];
						if (flag4)
						{
							return false;
						}
					}
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0001F8D0 File Offset: 0x0001DAD0
		public override bool Equals(object obj)
		{
			string text = obj as string;
			bool flag;
			if (text == null || !this.Equals(text))
			{
				if (obj is RenderedText)
				{
					RenderedText renderedText = (RenderedText)obj;
					flag = this.Equals(renderedText);
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0001F914 File Offset: 0x0001DB14
		public override int GetHashCode()
		{
			return HashCode.Combine<string, int, int, string, char, int>(this.value, this.valueStart, this.valueLength, this.suffix, this.repeat, this.repeatCount);
		}

		// Token: 0x040002E8 RID: 744
		public readonly string value;

		// Token: 0x040002E9 RID: 745
		public readonly int valueStart;

		// Token: 0x040002EA RID: 746
		public readonly int valueLength;

		// Token: 0x040002EB RID: 747
		public readonly string suffix;

		// Token: 0x040002EC RID: 748
		public readonly char repeat;

		// Token: 0x040002ED RID: 749
		public readonly int repeatCount;

		// Token: 0x02000050 RID: 80
		public struct Enumerator
		{
			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000243 RID: 579 RVA: 0x0001F94F File Offset: 0x0001DB4F
			public char Current
			{
				get
				{
					return this.m_Current;
				}
			}

			// Token: 0x06000244 RID: 580 RVA: 0x0001F957 File Offset: 0x0001DB57
			public Enumerator(in RenderedText source)
			{
				this.m_Source = source;
				this.m_Stage = 0;
				this.m_StageIndex = 0;
				this.m_Current = '\0';
			}

			// Token: 0x06000245 RID: 581 RVA: 0x0001F97C File Offset: 0x0001DB7C
			public bool MoveNext()
			{
				bool flag = this.m_Stage == 0;
				if (flag)
				{
					bool flag2 = this.m_Source.value != null;
					if (flag2)
					{
						int valueStart = this.m_Source.valueStart;
						int num = this.m_Source.valueStart + this.m_Source.valueLength;
						bool flag3 = this.m_StageIndex < valueStart;
						if (flag3)
						{
							this.m_StageIndex = valueStart;
						}
						bool flag4 = this.m_StageIndex < num;
						if (flag4)
						{
							this.m_Current = this.m_Source.value[this.m_StageIndex];
							this.m_StageIndex++;
							return true;
						}
					}
					this.m_Stage = 1;
					this.m_StageIndex = 0;
				}
				bool flag5 = this.m_Stage == 1;
				if (flag5)
				{
					bool flag6 = this.m_StageIndex < this.m_Source.repeatCount;
					if (flag6)
					{
						this.m_Current = this.m_Source.repeat;
						this.m_StageIndex++;
						return true;
					}
					this.m_Stage = 2;
					this.m_StageIndex = 0;
				}
				bool flag7 = this.m_Stage == 2;
				if (flag7)
				{
					bool flag8 = this.m_Source.suffix != null && this.m_StageIndex < this.m_Source.suffix.Length;
					if (flag8)
					{
						this.m_Current = this.m_Source.suffix[this.m_StageIndex];
						this.m_StageIndex++;
						return true;
					}
					this.m_Stage = 3;
					this.m_StageIndex = 0;
				}
				return false;
			}

			// Token: 0x06000246 RID: 582 RVA: 0x0001FB1E File Offset: 0x0001DD1E
			public void Reset()
			{
				this.m_Stage = 0;
				this.m_StageIndex = 0;
				this.m_Current = '\0';
			}

			// Token: 0x040002EE RID: 750
			private readonly RenderedText m_Source;

			// Token: 0x040002EF RID: 751
			private const int k_ValueStage = 0;

			// Token: 0x040002F0 RID: 752
			private const int k_RepeatStage = 1;

			// Token: 0x040002F1 RID: 753
			private const int k_SuffixStage = 2;

			// Token: 0x040002F2 RID: 754
			private int m_Stage;

			// Token: 0x040002F3 RID: 755
			private int m_StageIndex;

			// Token: 0x040002F4 RID: 756
			private char m_Current;
		}
	}
}
