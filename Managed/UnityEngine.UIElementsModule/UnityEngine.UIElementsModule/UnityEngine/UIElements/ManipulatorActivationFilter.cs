using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E8 RID: 744
	public struct ManipulatorActivationFilter : IEquatable<ManipulatorActivationFilter>
	{
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x00055223 File Offset: 0x00053423
		// (set) Token: 0x06001533 RID: 5427 RVA: 0x0005522B File Offset: 0x0005342B
		public MouseButton button { readonly get; set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00055234 File Offset: 0x00053434
		// (set) Token: 0x06001535 RID: 5429 RVA: 0x0005523C File Offset: 0x0005343C
		public EventModifiers modifiers { readonly get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00055245 File Offset: 0x00053445
		// (set) Token: 0x06001537 RID: 5431 RVA: 0x0005524D File Offset: 0x0005344D
		public int clickCount { readonly get; set; }

		// Token: 0x06001538 RID: 5432 RVA: 0x00055258 File Offset: 0x00053458
		public override bool Equals(object obj)
		{
			return obj is ManipulatorActivationFilter && this.Equals((ManipulatorActivationFilter)obj);
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00055284 File Offset: 0x00053484
		public bool Equals(ManipulatorActivationFilter other)
		{
			return this.button == other.button && this.modifiers == other.modifiers && this.clickCount == other.clickCount;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000552C8 File Offset: 0x000534C8
		public override int GetHashCode()
		{
			int num = 390957112;
			num = num * -1521134295 + this.button.GetHashCode();
			num = num * -1521134295 + this.modifiers.GetHashCode();
			return num * -1521134295 + this.clickCount.GetHashCode();
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00055334 File Offset: 0x00053534
		public bool Matches(IMouseEvent e)
		{
			bool flag = e == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				flag2 = this.button == (MouseButton)e.button && this.HasModifiers(e) && flag3;
			}
			return flag2;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0005538C File Offset: 0x0005358C
		private bool HasModifiers(IMouseEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000553C8 File Offset: 0x000535C8
		public bool Matches(IPointerEvent e)
		{
			bool flag = e == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = this.clickCount == 0 || e.clickCount >= this.clickCount;
				flag2 = this.button == (MouseButton)e.button && this.HasModifiers(e) && flag3;
			}
			return flag2;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x00055420 File Offset: 0x00053620
		private bool HasModifiers(IPointerEvent e)
		{
			bool flag = e == null;
			return !flag && this.MatchModifiers(e.altKey, e.ctrlKey, e.shiftKey, e.commandKey);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0005545C File Offset: 0x0005365C
		private bool MatchModifiers(bool alt, bool ctrl, bool shift, bool command)
		{
			bool flag = ((this.modifiers & EventModifiers.Alt) != EventModifiers.None && !alt) || ((this.modifiers & EventModifiers.Alt) == EventModifiers.None && alt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = ((this.modifiers & EventModifiers.Control) != EventModifiers.None && !ctrl) || ((this.modifiers & EventModifiers.Control) == EventModifiers.None && ctrl);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = ((this.modifiers & EventModifiers.Shift) != EventModifiers.None && !shift) || ((this.modifiers & EventModifiers.Shift) == EventModifiers.None && shift);
					flag2 = !flag4 && ((this.modifiers & EventModifiers.Command) == EventModifiers.None || command) && ((this.modifiers & EventModifiers.Command) != EventModifiers.None || !command);
				}
			}
			return flag2;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00055508 File Offset: 0x00053708
		public static bool operator ==(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return filter1.Equals(filter2);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x00055524 File Offset: 0x00053724
		public static bool operator !=(ManipulatorActivationFilter filter1, ManipulatorActivationFilter filter2)
		{
			return !(filter1 == filter2);
		}
	}
}
