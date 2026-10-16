using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000209 RID: 521
	public class DropdownMenu
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00046932 File Offset: 0x00044B32
		internal int Count
		{
			get
			{
				return this.m_MenuItems.Count;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0004693F File Offset: 0x00044B3F
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x00046947 File Offset: 0x00044B47
		public bool allowDuplicateNames { get; set; }

		// Token: 0x06000FED RID: 4077 RVA: 0x00046950 File Offset: 0x00044B50
		public List<DropdownMenuItem> MenuItems()
		{
			return this.m_MenuItems;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00046968 File Offset: 0x00044B68
		public void AppendAction(string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction dropdownMenuAction = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.m_MenuItems.Add(dropdownMenuAction);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00046990 File Offset: 0x00044B90
		public void AppendAction(string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.AppendAction(actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.AppendAction(actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00046A14 File Offset: 0x00044C14
		public void InsertAction(int atIndex, string actionName, Action<DropdownMenuAction> action, Func<DropdownMenuAction, DropdownMenuAction.Status> actionStatusCallback, object userData = null)
		{
			DropdownMenuAction dropdownMenuAction = new DropdownMenuAction(actionName, action, actionStatusCallback, userData);
			this.m_MenuItems.Insert(atIndex, dropdownMenuAction);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00046A3C File Offset: 0x00044C3C
		public void InsertAction(int atIndex, string actionName, Action<DropdownMenuAction> action, DropdownMenuAction.Status status = DropdownMenuAction.Status.Normal)
		{
			bool flag = status == DropdownMenuAction.Status.Normal;
			if (flag)
			{
				this.InsertAction(atIndex, actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysEnabled), null);
			}
			else
			{
				bool flag2 = status == DropdownMenuAction.Status.Disabled;
				if (flag2)
				{
					this.InsertAction(atIndex, actionName, action, new Func<DropdownMenuAction, DropdownMenuAction.Status>(DropdownMenuAction.AlwaysDisabled), null);
				}
				else
				{
					this.InsertAction(atIndex, actionName, action, (DropdownMenuAction e) => status, null);
				}
			}
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00046AC4 File Offset: 0x00044CC4
		public void AppendSeparator(string subMenuPath = null)
		{
			if (subMenuPath == null)
			{
				subMenuPath = string.Empty;
			}
			bool flag = this.m_MenuItems.FindIndex(delegate(DropdownMenuItem item)
			{
				DropdownMenuAction dropdownMenuAction = item as DropdownMenuAction;
				return dropdownMenuAction != null && dropdownMenuAction.name.StartsWith(subMenuPath);
			}) == -1;
			bool flag2;
			if (this.m_MenuItems.Count > 0)
			{
				List<DropdownMenuItem> menuItems = this.m_MenuItems;
				if (menuItems[menuItems.Count - 1] is DropdownMenuSeparator)
				{
					List<DropdownMenuItem> menuItems2 = this.m_MenuItems;
					if (((DropdownMenuSeparator)menuItems2[menuItems2.Count - 1]).subMenuPath == subMenuPath)
					{
						goto IL_0094;
					}
				}
				flag2 = !flag;
				goto IL_0095;
			}
			IL_0094:
			flag2 = false;
			IL_0095:
			bool flag3 = flag2;
			if (flag3)
			{
				DropdownMenuSeparator dropdownMenuSeparator = new DropdownMenuSeparator(subMenuPath);
				this.m_MenuItems.Add(dropdownMenuSeparator);
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00046B88 File Offset: 0x00044D88
		public void InsertSeparator(string subMenuPath, int atIndex)
		{
			bool flag = atIndex > 0 && atIndex <= this.m_MenuItems.Count && !(this.m_MenuItems[atIndex - 1] is DropdownMenuSeparator);
			if (flag)
			{
				DropdownMenuSeparator dropdownMenuSeparator = new DropdownMenuSeparator(subMenuPath ?? string.Empty);
				this.m_MenuItems.Insert(atIndex, dropdownMenuSeparator);
			}
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x00046BE8 File Offset: 0x00044DE8
		public void RemoveItemAt(int index)
		{
			this.m_MenuItems.RemoveAt(index);
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x00046BF8 File Offset: 0x00044DF8
		public void ClearItems()
		{
			this.m_MenuItems.Clear();
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00046C08 File Offset: 0x00044E08
		public void PrepareForDisplay(EventBase e)
		{
			this.m_DropdownMenuEventInfo = ((e != null) ? new DropdownMenuEventInfo(e) : null);
			bool flag = this.m_MenuItems.Count == 0;
			if (!flag)
			{
				foreach (DropdownMenuItem dropdownMenuItem in this.m_MenuItems)
				{
					DropdownMenuAction dropdownMenuAction = dropdownMenuItem as DropdownMenuAction;
					bool flag2 = dropdownMenuAction != null;
					if (flag2)
					{
						dropdownMenuAction.UpdateActionStatus(this.m_DropdownMenuEventInfo);
					}
				}
				bool flag3 = this.m_MenuItems[this.m_MenuItems.Count - 1] is DropdownMenuSeparator;
				if (flag3)
				{
					this.m_MenuItems.RemoveAt(this.m_MenuItems.Count - 1);
				}
			}
		}

		// Token: 0x040009AD RID: 2477
		private List<DropdownMenuItem> m_MenuItems = new List<DropdownMenuItem>();

		// Token: 0x040009AE RID: 2478
		private DropdownMenuEventInfo m_DropdownMenuEventInfo;
	}
}
