using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000012 RID: 18
	public class AccessibilityNode
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00003D90 File Offset: 0x00001F90
		internal AccessibilityNode(int id, AccessibilityHierarchy hierarchy)
		{
			this.id = id;
			this.m_Hierarchy = hierarchy;
			this.m_Children = new AccessibilityNode.ObservableList<AccessibilityNode>();
			this.m_Actions = new AccessibilityNode.ObservableList<AccessibilityAction>();
			bool flag = !this.IsInActiveHierarchy();
			if (!flag)
			{
				AccessibilityNodeData accessibilityNodeData = new AccessibilityNodeData
				{
					id = id,
					isActive = this.isActive,
					parentId = -1
				};
				this.CreateNativeNodeWithData(ref accessibilityNodeData);
				this.m_Actions.listChanged += this.ActionsChanged;
				this.m_Children.listChanged += this.ChildrenChanged;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003E4C File Offset: 0x0000204C
		private void CreateNativeNodeWithData(ref AccessibilityNodeData nodeData)
		{
			bool flag = !AccessibilityManager.isSupportedPlatform;
			if (!flag)
			{
				bool flag2 = !AccessibilityNodeManager.CreateNativeNodeWithData(nodeData);
				if (flag2)
				{
					throw new InvalidOperationException("CreateNativeNodeWithData: Could not create native " + string.Format("accessibility node with ID {0}. Please check the Player log for more details.", nodeData.id));
				}
			}
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003EA4 File Offset: 0x000020A4
		internal void AllocateNative()
		{
			bool flag = !this.IsInActiveHierarchy();
			if (!flag)
			{
				AccessibilityNodeData accessibilityNodeData = default(AccessibilityNodeData);
				accessibilityNodeData.id = this.id;
				accessibilityNodeData.label = this.label;
				accessibilityNodeData.value = this.value;
				accessibilityNodeData.hint = this.hint;
				accessibilityNodeData.isActive = this.isActive;
				accessibilityNodeData.role = this.role;
				accessibilityNodeData.allowsDirectInteraction = this.allowsDirectInteraction;
				accessibilityNodeData.state = this.state;
				AccessibilityNode parent = this.parent;
				accessibilityNodeData.parentId = ((parent != null) ? parent.id : (-1));
				accessibilityNodeData.frame = this.frame;
				accessibilityNodeData.language = this.language;
				accessibilityNodeData.implementsSelected = this.selected != null;
				accessibilityNodeData.implementsDismissed = this.dismissed != null;
				AccessibilityNodeData accessibilityNodeData2 = accessibilityNodeData;
				this.CreateNativeNodeWithData(ref accessibilityNodeData2);
				this.ActionsChanged();
				this.m_Actions.listChanged += this.ActionsChanged;
				foreach (AccessibilityNode accessibilityNode in this.m_Children)
				{
					accessibilityNode.AllocateNative();
				}
				this.ChildrenChanged();
				this.m_Children.listChanged += this.ChildrenChanged;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004024 File Offset: 0x00002224
		internal void FreeNative(bool freeChildren)
		{
			if (freeChildren)
			{
				foreach (AccessibilityNode accessibilityNode in this.m_Children)
				{
					accessibilityNode.FreeNative(true);
				}
			}
			this.m_Children.listChanged -= this.ChildrenChanged;
			this.m_Actions.listChanged -= this.ActionsChanged;
			bool flag = this.IsInActiveHierarchy();
			if (flag)
			{
				AccessibilityNode parent = this.parent;
				int num = ((parent != null) ? parent.id : (-1));
				AccessibilityNodeManager.DestroyNativeNode(this.id, num);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000040DC File Offset: 0x000022DC
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000040E4 File Offset: 0x000022E4
		public int id { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000040ED File Offset: 0x000022ED
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x000040F8 File Offset: 0x000022F8
		public string label
		{
			get
			{
				return this.m_Label;
			}
			set
			{
				bool flag = string.Equals(this.m_Label, value);
				if (!flag)
				{
					this.m_Label = value;
					bool flag2 = this.IsInActiveHierarchy();
					if (flag2)
					{
						AccessibilityNodeManager.SetLabel(this.id, value);
					}
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004139 File Offset: 0x00002339
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004144 File Offset: 0x00002344
		public string value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				bool flag = string.Equals(this.m_Value, value);
				if (!flag)
				{
					this.m_Value = value;
					bool flag2 = this.IsInActiveHierarchy();
					if (flag2)
					{
						AccessibilityNodeManager.SetValue(this.id, value);
					}
				}
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004185 File Offset: 0x00002385
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00004190 File Offset: 0x00002390
		public string hint
		{
			get
			{
				return this.m_Hint;
			}
			set
			{
				bool flag = string.Equals(this.m_Hint, value);
				if (!flag)
				{
					this.m_Hint = value;
					bool flag2 = this.IsInActiveHierarchy();
					if (flag2)
					{
						AccessibilityNodeManager.SetHint(this.id, value);
					}
				}
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000041D1 File Offset: 0x000023D1
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000041DC File Offset: 0x000023DC
		public bool isActive
		{
			get
			{
				return this.m_IsActive;
			}
			set
			{
				bool flag = this.m_IsActive == value;
				if (!flag)
				{
					this.m_IsActive = value;
					bool flag2 = this.IsInActiveHierarchy();
					if (flag2)
					{
						AccessibilityNodeManager.SetIsActive(this.id, value);
					}
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000421A File Offset: 0x0000241A
		// (set) Token: 0x060000DE RID: 222 RVA: 0x00004224 File Offset: 0x00002424
		public AccessibilityRole role
		{
			get
			{
				return this.m_Role;
			}
			set
			{
				bool flag = this.m_Role == value;
				if (!flag)
				{
					this.m_Role = value;
					bool flag2 = this.IsInActiveHierarchy();
					if (flag2)
					{
						AccessibilityNodeManager.SetRole(this.id, value);
					}
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000DF RID: 223 RVA: 0x00004262 File Offset: 0x00002462
		// (set) Token: 0x060000E0 RID: 224 RVA: 0x0000426C File Offset: 0x0000246C
		public bool allowsDirectInteraction
		{
			get
			{
				return this.m_AllowsDirectInteraction;
			}
			set
			{
				bool flag = value && !Application.isEditor && Application.platform != RuntimePlatform.IPhonePlayer;
				if (flag)
				{
					throw new PlatformNotSupportedException("allowsDirectInteraction is only supported on iOS.");
				}
				bool flag2 = this.m_AllowsDirectInteraction == value;
				if (!flag2)
				{
					this.m_AllowsDirectInteraction = value;
					bool flag3 = this.IsInActiveHierarchy();
					if (flag3)
					{
						AccessibilityNodeManager.SetAllowsDirectInteraction(this.id, value);
					}
				}
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000042D5 File Offset: 0x000024D5
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x000042E0 File Offset: 0x000024E0
		public AccessibilityState state
		{
			get
			{
				return this.m_State;
			}
			set
			{
				bool flag = this.m_State == value;
				if (!flag)
				{
					this.m_State = value;
					bool flag2 = this.IsInActiveHierarchy();
					if (flag2)
					{
						AccessibilityNodeManager.SetState(this.id, value);
					}
				}
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000431E File Offset: 0x0000251E
		public AccessibilityNode parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004328 File Offset: 0x00002528
		internal void SetParent(AccessibilityNode parent, int index = -1)
		{
			this.m_Parent = parent;
			bool flag = this.IsInActiveHierarchy();
			if (flag)
			{
				int num = ((parent != null) ? parent.id : (-1));
				AccessibilityNodeManager.SetParent(this.id, num, index);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004364 File Offset: 0x00002564
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000436C File Offset: 0x0000256C
		internal IList<AccessibilityNode> childList
		{
			get
			{
				return this.m_Children;
			}
			set
			{
				bool flag = this.m_Children != null;
				if (flag)
				{
					this.m_Children.listChanged -= this.ChildrenChanged;
				}
				this.m_Children = new AccessibilityNode.ObservableList<AccessibilityNode>(value);
				this.ChildrenChanged();
				this.m_Children.listChanged += this.ChildrenChanged;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00004364 File Offset: 0x00002564
		public IReadOnlyList<AccessibilityNode> children
		{
			get
			{
				return this.m_Children;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000043CC File Offset: 0x000025CC
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x000043D4 File Offset: 0x000025D4
		internal IList<AccessibilityAction> actions
		{
			get
			{
				return this.m_Actions;
			}
			set
			{
				bool flag = this.m_Actions != null;
				if (flag)
				{
					this.m_Actions.listChanged -= this.ActionsChanged;
				}
				this.m_Actions = new AccessibilityNode.ObservableList<AccessibilityAction>(value);
				this.ActionsChanged();
				this.m_Actions.listChanged += this.ActionsChanged;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004434 File Offset: 0x00002634
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000446D File Offset: 0x0000266D
		public Rect frame
		{
			get
			{
				bool flag = this.m_Frame == default(Rect);
				if (flag)
				{
					this.CalculateFrame();
				}
				return this.m_Frame;
			}
			set
			{
				this.SetFrame(value);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004478 File Offset: 0x00002678
		private void SetFrame(Rect frame)
		{
			bool flag = this.m_Frame == frame;
			if (!flag)
			{
				this.m_Frame = frame;
				bool flag2 = this.IsInActiveHierarchy();
				if (flag2)
				{
					AccessibilityNodeManager.SetFrame(this.id, frame);
				}
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000044B9 File Offset: 0x000026B9
		// (set) Token: 0x060000EE RID: 238 RVA: 0x000044C4 File Offset: 0x000026C4
		public Func<Rect> frameGetter
		{
			get
			{
				return this.m_FrameGetter;
			}
			set
			{
				bool flag = this.m_FrameGetter == value;
				if (!flag)
				{
					this.m_FrameGetter = value;
					bool flag2 = this.IsInActiveHierarchy();
					if (flag2)
					{
						AccessibilityNodeManager.SetFrame(this.id, this.frame);
					}
				}
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000450A File Offset: 0x0000270A
		internal void CalculateFrame()
		{
			Func<Rect> frameGetter = this.frameGetter;
			this.SetFrame((frameGetter != null) ? frameGetter() : Rect.zero);
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000452A File Offset: 0x0000272A
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00004534 File Offset: 0x00002734
		internal SystemLanguage language
		{
			get
			{
				return this.m_Language;
			}
			set
			{
				bool flag = this.m_Language == value;
				if (!flag)
				{
					this.m_Language = value;
					bool flag2 = this.IsInActiveHierarchy();
					if (flag2)
					{
						AccessibilityNodeManager.SetLanguage(this.id, value);
					}
				}
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004572 File Offset: 0x00002772
		public bool isFocused
		{
			get
			{
				return this.IsInActiveHierarchy() && AccessibilityNodeManager.GetIsFocused(this.id);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060000F3 RID: 243 RVA: 0x0000458C File Offset: 0x0000278C
		// (remove) Token: 0x060000F4 RID: 244 RVA: 0x000045C4 File Offset: 0x000027C4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<AccessibilityNode, bool> focusChanged;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000F5 RID: 245 RVA: 0x000045FC File Offset: 0x000027FC
		// (remove) Token: 0x060000F6 RID: 246 RVA: 0x00004634 File Offset: 0x00002834
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Func<bool> selected;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060000F7 RID: 247 RVA: 0x0000466C File Offset: 0x0000286C
		// (remove) Token: 0x060000F8 RID: 248 RVA: 0x000046A4 File Offset: 0x000028A4
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action incremented;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000F9 RID: 249 RVA: 0x000046DC File Offset: 0x000028DC
		// (remove) Token: 0x060000FA RID: 250 RVA: 0x00004714 File Offset: 0x00002914
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action decremented;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060000FB RID: 251 RVA: 0x0000474C File Offset: 0x0000294C
		// (remove) Token: 0x060000FC RID: 252 RVA: 0x00004784 File Offset: 0x00002984
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Func<bool> dismissed;

		// Token: 0x060000FD RID: 253 RVA: 0x000047BC File Offset: 0x000029BC
		internal void GetNodeData(ref AccessibilityNodeData nodeData)
		{
			nodeData.id = this.id;
			nodeData.isActive = this.isActive;
			nodeData.label = this.label;
			nodeData.value = this.value;
			nodeData.hint = this.hint;
			nodeData.role = this.role;
			nodeData.allowsDirectInteraction = this.allowsDirectInteraction;
			nodeData.state = this.state;
			nodeData.frame = this.frame;
			AccessibilityNode parent = this.parent;
			nodeData.parentId = ((parent != null) ? parent.id : (-1));
			int[] array = new int[this.m_Children.Count];
			for (int i = 0; i < this.m_Children.Count; i++)
			{
				array[i] = this.m_Children[i].id;
			}
			nodeData.childIds = array;
			nodeData.language = this.language;
			nodeData.implementsSelected = this.selected != null;
			nodeData.implementsDismissed = this.dismissed != null;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000048D0 File Offset: 0x00002AD0
		internal void Destroy(bool destroyChildren)
		{
			this.FreeNative(destroyChildren);
			AccessibilityNode parent = this.parent;
			if (parent != null)
			{
				parent.childList.Remove(this);
			}
			if (destroyChildren)
			{
				for (int i = this.childList.Count - 1; i >= 0; i--)
				{
					this.childList[i].Destroy(true);
				}
			}
			else
			{
				foreach (AccessibilityNode accessibilityNode in this.childList)
				{
					accessibilityNode.SetParent(this.parent, -1);
					AccessibilityNode parent2 = this.parent;
					if (parent2 != null)
					{
						parent2.childList.Add(accessibilityNode);
					}
				}
			}
			this.childList.Clear();
			this.m_Hierarchy = null;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000049B8 File Offset: 0x00002BB8
		public override int GetHashCode()
		{
			return this.id;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000049D0 File Offset: 0x00002BD0
		public override string ToString()
		{
			return string.Format("AccessibilityNode(ID: {0}, Label: {1})", this.id, this.label);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004A00 File Offset: 0x00002C00
		private void ChildrenChanged()
		{
			bool flag = !this.IsInActiveHierarchy();
			if (!flag)
			{
				int[] array = new int[this.m_Children.Count];
				for (int i = 0; i < this.m_Children.Count; i++)
				{
					array[i] = this.m_Children[i].id;
				}
				AccessibilityNodeManager.SetChildren(this.id, array);
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004A6C File Offset: 0x00002C6C
		private void ActionsChanged()
		{
			bool flag = !this.IsInActiveHierarchy();
			if (!flag)
			{
				AccessibilityAction[] array = new AccessibilityAction[this.m_Actions.Count];
				for (int i = 0; i < this.m_Actions.Count; i++)
				{
					array[i] = this.m_Actions[i];
				}
				AccessibilityNodeManager.SetActions(this.id, array);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004AD4 File Offset: 0x00002CD4
		private bool IsInActiveHierarchy()
		{
			return this.m_Hierarchy != null && AssistiveSupport.activeHierarchy == this.m_Hierarchy;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004B00 File Offset: 0x00002D00
		internal void NotifyFocusChanged(bool isNodeFocused)
		{
			AccessibilityManager.QueueNotification(new AccessibilityManager.NotificationContext
			{
				notification = (isNodeFocused ? AccessibilityNotification.ElementFocused : AccessibilityNotification.ElementUnfocused),
				currentNode = this
			});
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004B35 File Offset: 0x00002D35
		internal void InvokeFocusChanged(bool isNodeFocused)
		{
			Action<AccessibilityNode, bool> action = this.focusChanged;
			if (action != null)
			{
				action(this, isNodeFocused);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004B4C File Offset: 0x00002D4C
		internal bool InvokeSelected()
		{
			Func<bool> func = this.selected;
			return func != null && func();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004B70 File Offset: 0x00002D70
		internal void InvokeIncremented()
		{
			Action action = this.incremented;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004B85 File Offset: 0x00002D85
		internal void InvokeDecremented()
		{
			Action action = this.decremented;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004B9C File Offset: 0x00002D9C
		internal bool Dismissed()
		{
			Func<bool> func = this.dismissed;
			return func != null && func();
		}

		// Token: 0x04000055 RID: 85
		private Func<Rect> m_FrameGetter;

		// Token: 0x0400005B RID: 91
		private string m_Label;

		// Token: 0x0400005C RID: 92
		private string m_Value;

		// Token: 0x0400005D RID: 93
		private string m_Hint;

		// Token: 0x0400005E RID: 94
		private bool m_IsActive = true;

		// Token: 0x0400005F RID: 95
		private AccessibilityRole m_Role;

		// Token: 0x04000060 RID: 96
		private bool m_AllowsDirectInteraction;

		// Token: 0x04000061 RID: 97
		private AccessibilityState m_State;

		// Token: 0x04000062 RID: 98
		private AccessibilityNode m_Parent;

		// Token: 0x04000063 RID: 99
		private AccessibilityNode.ObservableList<AccessibilityNode> m_Children;

		// Token: 0x04000064 RID: 100
		private AccessibilityNode.ObservableList<AccessibilityAction> m_Actions;

		// Token: 0x04000065 RID: 101
		private Rect m_Frame;

		// Token: 0x04000066 RID: 102
		private SystemLanguage m_Language = SystemLanguage.Unknown;

		// Token: 0x04000067 RID: 103
		private AccessibilityHierarchy m_Hierarchy;

		// Token: 0x02000013 RID: 19
		private class ObservableList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>, IList, ICollection
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x0600010A RID: 266 RVA: 0x00004BC0 File Offset: 0x00002DC0
			public int Count
			{
				get
				{
					return this.m_Items.Count;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x0600010B RID: 267 RVA: 0x00004BCD File Offset: 0x00002DCD
			public bool IsSynchronized
			{
				get
				{
					List<T> items = this.m_Items;
					return items != null && ((ICollection)items).IsSynchronized;
				}
			}

			// Token: 0x1700003D RID: 61
			// (get) Token: 0x0600010C RID: 268 RVA: 0x00004BE1 File Offset: 0x00002DE1
			public object SyncRoot
			{
				get
				{
					List<T> items = this.m_Items;
					return ((items != null) ? ((ICollection)items).SyncRoot : null) ?? false;
				}
			}

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x0600010D RID: 269 RVA: 0x00004BFF File Offset: 0x00002DFF
			public bool IsReadOnly
			{
				get
				{
					List<T> items = this.m_Items;
					return items != null && ((IList)items).IsReadOnly;
				}
			}

			// Token: 0x1700003F RID: 63
			object IList.this[int index]
			{
				get
				{
					return this.m_Items[index];
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000110 RID: 272 RVA: 0x00004C2D File Offset: 0x00002E2D
			public ObservableList()
			{
				this.m_Items = new List<T>();
			}

			// Token: 0x06000111 RID: 273 RVA: 0x00004C42 File Offset: 0x00002E42
			public ObservableList(IEnumerable<T> enumerable)
			{
				this.m_Items = new List<T>(enumerable);
			}

			// Token: 0x06000112 RID: 274 RVA: 0x00004C58 File Offset: 0x00002E58
			public void CopyTo(Array array, int index)
			{
				List<T> items = this.m_Items;
				if (items != null)
				{
					((ICollection)items).CopyTo(array, index);
				}
			}

			// Token: 0x06000113 RID: 275 RVA: 0x00004C6E File Offset: 0x00002E6E
			public void Add(T item)
			{
				this.m_Items.Add(item);
				Action action = this.listChanged;
				if (action != null)
				{
					action();
				}
			}

			// Token: 0x06000114 RID: 276 RVA: 0x00004C90 File Offset: 0x00002E90
			public void Insert(int index, T item)
			{
				this.m_Items.Insert(index, item);
				Action action = this.listChanged;
				if (action != null)
				{
					action();
				}
			}

			// Token: 0x06000115 RID: 277 RVA: 0x00004CB3 File Offset: 0x00002EB3
			public void Remove(T item)
			{
				this.m_Items.Remove(item);
				Action action = this.listChanged;
				if (action != null)
				{
					action();
				}
			}

			// Token: 0x06000116 RID: 278 RVA: 0x00004CD8 File Offset: 0x00002ED8
			bool ICollection<T>.Remove(T item)
			{
				bool flag = this.m_Items.Remove(item);
				bool flag2 = flag;
				if (flag2)
				{
					Action action = this.listChanged;
					if (action != null)
					{
						action();
					}
				}
				return flag;
			}

			// Token: 0x06000117 RID: 279 RVA: 0x00004D11 File Offset: 0x00002F11
			public void Remove(object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000118 RID: 280 RVA: 0x00004D19 File Offset: 0x00002F19
			public void RemoveAt(int index)
			{
				this.m_Items.RemoveAt(index);
				Action action = this.listChanged;
				if (action != null)
				{
					action();
				}
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000119 RID: 281 RVA: 0x00004D3B File Offset: 0x00002F3B
			public bool IsFixedSize { get; }

			// Token: 0x0600011A RID: 282 RVA: 0x00004D11 File Offset: 0x00002F11
			public int Add(object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600011B RID: 283 RVA: 0x00004D43 File Offset: 0x00002F43
			public void Clear()
			{
				this.m_Items.Clear();
				Action action = this.listChanged;
				if (action != null)
				{
					action();
				}
			}

			// Token: 0x0600011C RID: 284 RVA: 0x00004D11 File Offset: 0x00002F11
			public bool Contains(object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600011D RID: 285 RVA: 0x00004D11 File Offset: 0x00002F11
			public int IndexOf(object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600011E RID: 286 RVA: 0x00004D11 File Offset: 0x00002F11
			public void Insert(int index, object value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x17000041 RID: 65
			public T this[int index]
			{
				get
				{
					return this.m_Items[index];
				}
				set
				{
					this.m_Items[index] = value;
				}
			}

			// Token: 0x06000121 RID: 289 RVA: 0x00004D82 File Offset: 0x00002F82
			public int IndexOf(T item)
			{
				return this.m_Items.IndexOf(item);
			}

			// Token: 0x06000122 RID: 290 RVA: 0x00004D90 File Offset: 0x00002F90
			public bool Contains(T item)
			{
				return this.m_Items.Contains(item);
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00004D9E File Offset: 0x00002F9E
			public void CopyTo(T[] array, int arrayIndex)
			{
				this.m_Items.CopyTo(array, arrayIndex);
			}

			// Token: 0x06000124 RID: 292 RVA: 0x00004DAE File Offset: 0x00002FAE
			public IEnumerator<T> GetEnumerator()
			{
				return this.m_Items.GetEnumerator();
			}

			// Token: 0x06000125 RID: 293 RVA: 0x00004DAE File Offset: 0x00002FAE
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.m_Items.GetEnumerator();
			}

			// Token: 0x14000011 RID: 17
			// (add) Token: 0x06000126 RID: 294 RVA: 0x00004DC0 File Offset: 0x00002FC0
			// (remove) Token: 0x06000127 RID: 295 RVA: 0x00004DF8 File Offset: 0x00002FF8
			[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
			public event Action listChanged;

			// Token: 0x04000068 RID: 104
			private readonly List<T> m_Items;
		}
	}
}
