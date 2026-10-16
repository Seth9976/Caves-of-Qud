using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine.Events;

namespace XRL.UI.Framework
{
	// Token: 0x020014F2 RID: 5362
	public class ButtonBar : HorizontalScroller
	{
		// Token: 0x0600D196 RID: 53654 RVA: 0x004DDFAC File Offset: 0x004DC1AC
		public void SetupContext(InputAxisTypes axis = InputAxisTypes.NavigationXAxis)
		{
			this.scrollContext.SetAxis(axis, false);
			this.scrollContext.contexts.Clear();
			this.scrollContext.wraps = false;
			this.scrollContext.Setup();
		}

		// Token: 0x0600D197 RID: 53655 RVA: 0x004DDFE4 File Offset: 0x004DC1E4
		public void SetButtons(IEnumerable<ButtonBar.ButtonBarButtonData> buttons)
		{
			this.choices.Clear();
			this.choices.AddRange(buttons);
			for (int i = 0; i < this.choices.Count; i++)
			{
				((ButtonBar.ButtonBarButtonData)this.choices[i]).n = i;
			}
			this.lastContexts.Clear();
			this.lastContexts.AddRange(this.scrollContext.contexts);
			this.scrollContext.contexts.Clear();
			this.LayoutChildren();
			this.scrollContext.Setup();
		}

		// Token: 0x040052CF RID: 21199
		public List<ButtonBar.ButtonBarButtonData> categoryButtons;

		// Token: 0x040052D0 RID: 21200
		public UnityEvent<string> OnSearchTextChange = new UnityEvent<string>();

		// Token: 0x02001E1C RID: 7708
		public class ButtonBarButtonData : PooledFrameworkDataElement<ButtonBar.ButtonBarButtonData>
		{
			// Token: 0x0600F62E RID: 63022 RVA: 0x005594B3 File Offset: 0x005576B3
			public ButtonBar.ButtonBarButtonData set(string label, IRenderable icon = null, ButtonBar.ButtonBarButtonData.HighlightState highlighted = ButtonBar.ButtonBarButtonData.HighlightState.Normal, Action onSelect = null)
			{
				this.label = label;
				this.icon = icon;
				this.onSelect = onSelect;
				this.Highlighted = highlighted;
				return this;
			}

			// Token: 0x0600F62F RID: 63023 RVA: 0x005594D3 File Offset: 0x005576D3
			public override void free()
			{
				this.button = null;
				this.onSelect = null;
				this.label = null;
				this.button = null;
				this.icon = null;
				this.n = 0;
				this.Highlighted = ButtonBar.ButtonBarButtonData.HighlightState.Normal;
			}

			// Token: 0x0400758A RID: 30090
			public int n;

			// Token: 0x0400758B RID: 30091
			public ButtonBarButton button;

			// Token: 0x0400758C RID: 30092
			public IRenderable icon;

			// Token: 0x0400758D RID: 30093
			public string label;

			// Token: 0x0400758E RID: 30094
			public Action onSelect;

			// Token: 0x0400758F RID: 30095
			public ButtonBar.ButtonBarButtonData.HighlightState Highlighted;

			// Token: 0x02001E9E RID: 7838
			public enum HighlightState
			{
				// Token: 0x040076E4 RID: 30436
				Invalid = -1,
				// Token: 0x040076E5 RID: 30437
				Normal,
				// Token: 0x040076E6 RID: 30438
				Highlighted,
				// Token: 0x040076E7 RID: 30439
				NotHighlighted
			}
		}
	}
}
