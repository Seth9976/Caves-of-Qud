using System;
using System.Collections.Generic;

namespace XRL.UI
{
	// Token: 0x020014B5 RID: 5301
	public class BookInfo
	{
		// Token: 0x17000C51 RID: 3153
		public BookPage this[int Index]
		{
			get
			{
				return this.Pages[Index];
			}
		}

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600D029 RID: 53289 RVA: 0x004CC50B File Offset: 0x004CA70B
		public int Count
		{
			get
			{
				return this.Pages.Count;
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600D02A RID: 53290 RVA: 0x004CC518 File Offset: 0x004CA718
		// (set) Token: 0x0600D02B RID: 53291 RVA: 0x004CC60C File Offset: 0x004CA80C
		public List<BookPage> Pages
		{
			get
			{
				if (this._Pages.IsNullOrEmpty<BookPage>() && !this.Texts.IsNullOrEmpty<string>())
				{
					if (this._Pages == null)
					{
						this._Pages = new List<BookPage>(this.Texts.Count);
					}
					foreach (string text in this.Texts)
					{
						if (!this.Format.IsNullOrEmpty())
						{
							List<BookPage> pages = this._Pages;
							string title = this.Title;
							string text2 = text;
							string format = this.Format;
							int topMargin = this.TopMargin;
							int rightMargin = this.RightMargin;
							int bottomMargin = this.BottomMargin;
							pages.AddRange(BookUI.AutoformatPages(title, text2, format, this.LeftMargin, rightMargin, topMargin, bottomMargin));
						}
						else
						{
							this._Pages.Add(new BookPage(this.Title, text));
						}
					}
				}
				return this._Pages;
			}
			set
			{
				this._Pages = value;
			}
		}

		// Token: 0x0600D02C RID: 53292 RVA: 0x004CC615 File Offset: 0x004CA815
		public void Reset()
		{
			List<BookPage> pages = this._Pages;
			if (pages == null)
			{
				return;
			}
			pages.Clear();
		}

		// Token: 0x0600D02D RID: 53293 RVA: 0x004CC627 File Offset: 0x004CA827
		public List<BookPage>.Enumerator GetEnumerator()
		{
			return this.Pages.GetEnumerator();
		}

		// Token: 0x040051A2 RID: 20898
		public string ID;

		// Token: 0x040051A3 RID: 20899
		public string Title;

		// Token: 0x040051A4 RID: 20900
		public string Format;

		// Token: 0x040051A5 RID: 20901
		public int LeftMargin = 2;

		// Token: 0x040051A6 RID: 20902
		public int RightMargin = 2;

		// Token: 0x040051A7 RID: 20903
		public int TopMargin = 2;

		// Token: 0x040051A8 RID: 20904
		public int BottomMargin = 2;

		// Token: 0x040051A9 RID: 20905
		public bool Dynamic;

		// Token: 0x040051AA RID: 20906
		public List<string> Texts;

		// Token: 0x040051AB RID: 20907
		private List<BookPage> _Pages;
	}
}
