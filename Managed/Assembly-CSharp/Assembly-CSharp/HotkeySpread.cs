using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using UnityEngine;
using XRL.UI;

// Token: 0x02000060 RID: 96
public class HotkeySpread : IReadOnlyList<global::UnityEngine.KeyCode>, IEnumerable<global::UnityEngine.KeyCode>, IEnumerable, IReadOnlyCollection<global::UnityEngine.KeyCode>, IReadOnlyList<char>, IEnumerable<char>, IReadOnlyCollection<char>
{
	// Token: 0x06000345 RID: 837 RVA: 0x000166F8 File Offset: 0x000148F8
	public HotkeySpread(List<global::UnityEngine.KeyCode> keys)
	{
		this.keys = keys;
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000346 RID: 838 RVA: 0x00016712 File Offset: 0x00014912
	public int Count
	{
		get
		{
			return this.keys.Count;
		}
	}

	// Token: 0x1700005B RID: 91
	public global::UnityEngine.KeyCode this[int Index]
	{
		get
		{
			return this.keys[Index];
		}
	}

	// Token: 0x1700005C RID: 92
	char IReadOnlyList<char>.this[int Index]
	{
		get
		{
			return Keyboard.ConvertKeycodeToLowercaseChar(this.keys[Index]);
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00016740 File Offset: 0x00014940
	public void restart()
	{
		this.pos = 0;
	}

	// Token: 0x0600034A RID: 842 RVA: 0x00016749 File Offset: 0x00014949
	public void next()
	{
		this.pos++;
	}

	// Token: 0x0600034B RID: 843 RVA: 0x00016759 File Offset: 0x00014959
	public void prev()
	{
		if (this.pos > 0)
		{
			this.pos--;
		}
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00016772 File Offset: 0x00014972
	public global::UnityEngine.KeyCode code()
	{
		return this.codeAt(this.pos);
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00016780 File Offset: 0x00014980
	public char ch()
	{
		return this.charAt(this.pos);
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0001678E File Offset: 0x0001498E
	public global::UnityEngine.KeyCode codeAt(int n)
	{
		if (this.keys.Count <= n)
		{
			return global::UnityEngine.KeyCode.None;
		}
		if (n < 0)
		{
			return global::UnityEngine.KeyCode.None;
		}
		return this.keys[n];
	}

	// Token: 0x0600034F RID: 847 RVA: 0x000167B2 File Offset: 0x000149B2
	public char charAt(int n)
	{
		if (this.keys.Count <= n)
		{
			return '\0';
		}
		if (n < 0)
		{
			return '\0';
		}
		return Keyboard.ConvertKeycodeToLowercaseChar(this.codeAt(n));
	}

	// Token: 0x06000350 RID: 848 RVA: 0x000167D6 File Offset: 0x000149D6
	public static HotkeySpread get(string category)
	{
		return new HotkeySpread(ControlManager.GetHotkeySpread(CommandBindingManager.NavCategories[category].Layers));
	}

	// Token: 0x06000351 RID: 849 RVA: 0x000167F2 File Offset: 0x000149F2
	public static HotkeySpread get()
	{
		return new HotkeySpread(ControlManager.GetHotkeySpread(ControlManager.EnabledLayers));
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00016803 File Offset: 0x00014A03
	public static HotkeySpread get(IEnumerable<string> layers)
	{
		return new HotkeySpread(ControlManager.GetHotkeySpread(layers.ToList<string>()));
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00016815 File Offset: 0x00014A15
	public List<global::UnityEngine.KeyCode>.Enumerator GetEnumerator()
	{
		return this.keys.GetEnumerator();
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00016822 File Offset: 0x00014A22
	public HotkeySpread.CharEnumerator GetCharEnumerator()
	{
		return new HotkeySpread.CharEnumerator(this.keys);
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0001682F File Offset: 0x00014A2F
	IEnumerator<global::UnityEngine.KeyCode> IEnumerable<global::UnityEngine.KeyCode>.GetEnumerator()
	{
		return this.keys.GetEnumerator();
	}

	// Token: 0x06000356 RID: 854 RVA: 0x00016841 File Offset: 0x00014A41
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.keys.GetEnumerator();
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00016853 File Offset: 0x00014A53
	IEnumerator<char> IEnumerable<char>.GetEnumerator()
	{
		return new HotkeySpread.CharEnumerator(this.keys);
	}

	// Token: 0x040002A5 RID: 677
	private List<global::UnityEngine.KeyCode> keys = new List<global::UnityEngine.KeyCode>();

	// Token: 0x040002A6 RID: 678
	private int pos;

	// Token: 0x02001579 RID: 5497
	[Serializable]
	public struct CharEnumerator : IEnumerator<char>, IEnumerator, IDisposable
	{
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x0600D549 RID: 54601 RVA: 0x004F3B01 File Offset: 0x004F1D01
		public char Current
		{
			get
			{
				return this.Item;
			}
		}

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x0600D54A RID: 54602 RVA: 0x004F3B09 File Offset: 0x004F1D09
		object IEnumerator.Current
		{
			get
			{
				return this.Item;
			}
		}

		// Token: 0x0600D54B RID: 54603 RVA: 0x004F3B16 File Offset: 0x004F1D16
		public CharEnumerator(List<global::UnityEngine.KeyCode> List)
		{
			this.List = List;
			this.Index = 0;
			this.Item = '\0';
		}

		// Token: 0x0600D54C RID: 54604 RVA: 0x004F3B2D File Offset: 0x004F1D2D
		public HotkeySpread.CharEnumerator GetEnumerator()
		{
			return this;
		}

		// Token: 0x0600D54D RID: 54605 RVA: 0x004F3B38 File Offset: 0x004F1D38
		public bool MoveNext()
		{
			if (this.Index >= this.List.Count)
			{
				this.Item = '\0';
				return false;
			}
			List<global::UnityEngine.KeyCode> list = this.List;
			int index = this.Index;
			this.Index = index + 1;
			this.Item = Keyboard.ConvertKeycodeToLowercaseChar(list[index]);
			return true;
		}

		// Token: 0x0600D54E RID: 54606 RVA: 0x004F3B89 File Offset: 0x004F1D89
		public void Dispose()
		{
			this.List = null;
		}

		// Token: 0x0600D54F RID: 54607 RVA: 0x004F3B92 File Offset: 0x004F1D92
		void IEnumerator.Reset()
		{
			this.Index = 0;
			this.Item = '\0';
		}

		// Token: 0x0400566F RID: 22127
		private List<global::UnityEngine.KeyCode> List;

		// Token: 0x04005670 RID: 22128
		private int Index;

		// Token: 0x04005671 RID: 22129
		private char Item;
	}
}
