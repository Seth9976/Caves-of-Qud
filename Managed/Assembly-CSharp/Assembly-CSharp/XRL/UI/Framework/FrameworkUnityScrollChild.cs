using System;

namespace XRL.UI.Framework
{
	// Token: 0x02001515 RID: 5397
	public class FrameworkUnityScrollChild : FrameworkContext
	{
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x0600D23A RID: 53818 RVA: 0x004E0C90 File Offset: 0x004DEE90
		public IFrameworkControl FrameworkControl
		{
			get
			{
				IFrameworkControl frameworkControl;
				if ((frameworkControl = this._FrameworkControl) == null)
				{
					frameworkControl = (this._FrameworkControl = base.GetComponent<IFrameworkControl>());
				}
				return frameworkControl;
			}
		}

		// Token: 0x0600D23B RID: 53819 RVA: 0x004E0CB8 File Offset: 0x004DEEB8
		public void Setup(FrameworkDataElement data, ScrollChildContext scrollContext)
		{
			this.context = scrollContext;
			if (this.FrameworkControl == null)
			{
				MetricsManager.LogError("The control element " + base.name + " doesn't have an IFrameworkControl component. It needs one to recieve the framework data.");
				return;
			}
			this.FrameworkControl.setData(data);
			IFrameworkControlSubcontexts frameworkControlSubcontexts = this.FrameworkControl as IFrameworkControlSubcontexts;
			if (frameworkControlSubcontexts != null)
			{
				frameworkControlSubcontexts.SetupContexts(scrollContext);
			}
			this.context.Setup();
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x0600D23C RID: 53820 RVA: 0x004E0D1C File Offset: 0x004DEF1C
		// (set) Token: 0x0600D23D RID: 53821 RVA: 0x004E0D29 File Offset: 0x004DEF29
		public virtual ScrollChildContext scrollContext
		{
			get
			{
				return (ScrollChildContext)this.context;
			}
			set
			{
				this.context = value;
			}
		}

		// Token: 0x04005354 RID: 21332
		private IFrameworkControl _FrameworkControl;
	}
}
