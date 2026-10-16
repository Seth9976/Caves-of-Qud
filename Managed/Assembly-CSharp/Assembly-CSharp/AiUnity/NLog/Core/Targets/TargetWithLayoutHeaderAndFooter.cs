using System;
using System.ComponentModel;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200022A RID: 554
	[Preserve]
	public abstract class TargetWithLayoutHeaderAndFooter : TargetWithLayout
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00073C28 File Offset: 0x00071E28
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x00073C35 File Offset: 0x00071E35
		[RequiredParameter]
		[Display("Layout", "Specifies the layout and content of log message.  The + icon will present a list of variables that can be added.  For the Unity Console all excepted xml formating can be used (http://docs.unity3d.com/Manual/StyledText.html)", false, -100)]
		[DefaultValue("${message}")]
		public override Layout Layout
		{
			get
			{
				return this.LHF.Layout;
			}
			set
			{
				if (value is LayoutWithHeaderAndFooter)
				{
					base.Layout = value;
					return;
				}
				if (this.LHF == null)
				{
					this.LHF = new LayoutWithHeaderAndFooter
					{
						Layout = value
					};
					return;
				}
				this.LHF.Layout = value;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x00073C6E File Offset: 0x00071E6E
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x00073C7B File Offset: 0x00071E7B
		public Layout Footer
		{
			get
			{
				return this.LHF.Footer;
			}
			set
			{
				this.LHF.Footer = value;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00073C89 File Offset: 0x00071E89
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00073C96 File Offset: 0x00071E96
		public Layout Header
		{
			get
			{
				return this.LHF.Header;
			}
			set
			{
				this.LHF.Header = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00073CA4 File Offset: 0x00071EA4
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00073CB1 File Offset: 0x00071EB1
		private LayoutWithHeaderAndFooter LHF
		{
			get
			{
				return (LayoutWithHeaderAndFooter)base.Layout;
			}
			set
			{
				base.Layout = value;
			}
		}
	}
}
