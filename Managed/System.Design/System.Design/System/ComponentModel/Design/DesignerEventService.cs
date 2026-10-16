using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x0200011E RID: 286
	internal sealed class DesignerEventService : IDesignerEventService
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x0000DA6D File Offset: 0x0000BC6D
		public DesignerEventService()
		{
			this._designerList = new ArrayList();
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0000DA80 File Offset: 0x0000BC80
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0000DA88 File Offset: 0x0000BC88
		public IDesignerHost ActiveDesigner
		{
			get
			{
				return this._activeDesigner;
			}
			internal set
			{
				IDesignerHost activeDesigner = this._activeDesigner;
				this._activeDesigner = value;
				if (this.ActiveDesignerChanged != null)
				{
					this.ActiveDesignerChanged(this, new ActiveDesignerEventArgs(activeDesigner, value));
				}
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0000DABE File Offset: 0x0000BCBE
		public DesignerCollection Designers
		{
			get
			{
				return new DesignerCollection(this._designerList);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000846 RID: 2118 RVA: 0x0000DACC File Offset: 0x0000BCCC
		// (remove) Token: 0x06000847 RID: 2119 RVA: 0x0000DB04 File Offset: 0x0000BD04
		public event ActiveDesignerEventHandler ActiveDesignerChanged;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000848 RID: 2120 RVA: 0x0000DB3C File Offset: 0x0000BD3C
		// (remove) Token: 0x06000849 RID: 2121 RVA: 0x0000DB74 File Offset: 0x0000BD74
		public event DesignerEventHandler DesignerCreated;

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600084A RID: 2122 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		// (remove) Token: 0x0600084B RID: 2123 RVA: 0x0000DBE4 File Offset: 0x0000BDE4
		public event DesignerEventHandler DesignerDisposed;

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600084C RID: 2124 RVA: 0x0000DC1C File Offset: 0x0000BE1C
		// (remove) Token: 0x0600084D RID: 2125 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public event EventHandler SelectionChanged;

		// Token: 0x0600084E RID: 2126 RVA: 0x0000DC89 File Offset: 0x0000BE89
		public void RaiseDesignerCreated(IDesignerHost host)
		{
			if (this.DesignerCreated != null)
			{
				this.DesignerCreated(this, new DesignerEventArgs(host));
			}
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0000DCA5 File Offset: 0x0000BEA5
		public void RaiseDesignerDisposed(IDesignerHost host)
		{
			if (this.DesignerDisposed != null)
			{
				this.DesignerDisposed(this, new DesignerEventArgs(host));
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0000DCC1 File Offset: 0x0000BEC1
		public void RaiseSelectionChanged()
		{
			if (this.SelectionChanged != null)
			{
				this.SelectionChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x040001CA RID: 458
		private ArrayList _designerList;

		// Token: 0x040001CB RID: 459
		private IDesignerHost _activeDesigner;
	}
}
