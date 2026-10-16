using System;

namespace System.Web.Compilation
{
	// Token: 0x02000656 RID: 1622
	internal class Location : ILocation
	{
		// Token: 0x06004580 RID: 17792 RVA: 0x000BC452 File Offset: 0x000BA652
		public Location(ILocation location)
		{
			this.Init(location);
		}

		// Token: 0x06004581 RID: 17793 RVA: 0x000BC464 File Offset: 0x000BA664
		public void Init(ILocation location)
		{
			if (location == null)
			{
				this.beginLine = 0;
				this.endLine = 0;
				this.beginColumn = 0;
				this.endColumn = 0;
				this.fileName = null;
				this.plainText = null;
			}
			else
			{
				this.beginLine = location.BeginLine;
				this.endLine = location.EndLine;
				this.beginColumn = location.BeginColumn;
				this.endColumn = location.EndColumn;
				this.fileName = location.Filename;
				this.plainText = location.PlainText;
			}
			this.location = location;
		}

		// Token: 0x170015C8 RID: 5576
		// (get) Token: 0x06004582 RID: 17794 RVA: 0x000BC4EF File Offset: 0x000BA6EF
		// (set) Token: 0x06004583 RID: 17795 RVA: 0x000BC4F7 File Offset: 0x000BA6F7
		public string Filename
		{
			get
			{
				return this.fileName;
			}
			set
			{
				this.fileName = value;
			}
		}

		// Token: 0x170015C9 RID: 5577
		// (get) Token: 0x06004584 RID: 17796 RVA: 0x000BC500 File Offset: 0x000BA700
		// (set) Token: 0x06004585 RID: 17797 RVA: 0x000BC508 File Offset: 0x000BA708
		public int BeginLine
		{
			get
			{
				return this.beginLine;
			}
			set
			{
				this.beginLine = value;
			}
		}

		// Token: 0x170015CA RID: 5578
		// (get) Token: 0x06004586 RID: 17798 RVA: 0x000BC511 File Offset: 0x000BA711
		// (set) Token: 0x06004587 RID: 17799 RVA: 0x000BC519 File Offset: 0x000BA719
		public int EndLine
		{
			get
			{
				return this.endLine;
			}
			set
			{
				this.endLine = value;
			}
		}

		// Token: 0x170015CB RID: 5579
		// (get) Token: 0x06004588 RID: 17800 RVA: 0x000BC522 File Offset: 0x000BA722
		// (set) Token: 0x06004589 RID: 17801 RVA: 0x000BC52A File Offset: 0x000BA72A
		public int BeginColumn
		{
			get
			{
				return this.beginColumn;
			}
			set
			{
				this.beginColumn = value;
			}
		}

		// Token: 0x170015CC RID: 5580
		// (get) Token: 0x0600458A RID: 17802 RVA: 0x000BC533 File Offset: 0x000BA733
		// (set) Token: 0x0600458B RID: 17803 RVA: 0x000BC53B File Offset: 0x000BA73B
		public int EndColumn
		{
			get
			{
				return this.endColumn;
			}
			set
			{
				this.endColumn = value;
			}
		}

		// Token: 0x170015CD RID: 5581
		// (get) Token: 0x0600458C RID: 17804 RVA: 0x000BC544 File Offset: 0x000BA744
		// (set) Token: 0x0600458D RID: 17805 RVA: 0x000BC54C File Offset: 0x000BA74C
		public string PlainText
		{
			get
			{
				return this.plainText;
			}
			set
			{
				this.plainText = value;
			}
		}

		// Token: 0x170015CE RID: 5582
		// (get) Token: 0x0600458E RID: 17806 RVA: 0x000BC555 File Offset: 0x000BA755
		public string FileText
		{
			get
			{
				if (this.location != null)
				{
					return this.location.FileText;
				}
				return null;
			}
		}

		// Token: 0x040024F9 RID: 9465
		private int beginLine;

		// Token: 0x040024FA RID: 9466
		private int endLine;

		// Token: 0x040024FB RID: 9467
		private int beginColumn;

		// Token: 0x040024FC RID: 9468
		private int endColumn;

		// Token: 0x040024FD RID: 9469
		private string fileName;

		// Token: 0x040024FE RID: 9470
		private string plainText;

		// Token: 0x040024FF RID: 9471
		private ILocation location;
	}
}
