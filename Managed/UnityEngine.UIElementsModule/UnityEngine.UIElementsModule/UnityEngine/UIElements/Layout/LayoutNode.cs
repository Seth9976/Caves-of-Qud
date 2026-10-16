using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000693 RID: 1683
	internal struct LayoutNode : IEquatable<LayoutNode>
	{
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060030ED RID: 12525 RVA: 0x000B9F55 File Offset: 0x000B8155
		public LayoutDirection LayoutDirection
		{
			get
			{
				return this.Layout.Direction;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060030EE RID: 12526 RVA: 0x000B9F62 File Offset: 0x000B8162
		public float LayoutX
		{
			get
			{
				return this.Layout.Position.FixedElementField;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060030EF RID: 12527 RVA: 0x000B9F75 File Offset: 0x000B8175
		public unsafe float LayoutY
		{
			get
			{
				return *((ref this.Layout.Position.FixedElementField) + 4);
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060030F0 RID: 12528 RVA: 0x000B9F8A File Offset: 0x000B818A
		public unsafe float LayoutRight
		{
			get
			{
				return *((ref this.Layout.Position.FixedElementField) + (IntPtr)2 * 4);
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060030F1 RID: 12529 RVA: 0x000B9FA2 File Offset: 0x000B81A2
		public unsafe float LayoutBottom
		{
			get
			{
				return *((ref this.Layout.Position.FixedElementField) + (IntPtr)3 * 4);
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060030F2 RID: 12530 RVA: 0x000B9FBA File Offset: 0x000B81BA
		public float LayoutWidth
		{
			get
			{
				return this.Layout.Dimensions.FixedElementField;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060030F3 RID: 12531 RVA: 0x000B9FCD File Offset: 0x000B81CD
		public unsafe float LayoutHeight
		{
			get
			{
				return *((ref this.Layout.Dimensions.FixedElementField) + 4);
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x000B9FE2 File Offset: 0x000B81E2
		public float LayoutMarginLeft
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Left);
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060030F5 RID: 12533 RVA: 0x000B9FF6 File Offset: 0x000B81F6
		public float LayoutMarginTop
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Top);
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000BA00A File Offset: 0x000B820A
		public float LayoutMarginRight
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Right);
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060030F7 RID: 12535 RVA: 0x000BA01E File Offset: 0x000B821E
		public float LayoutMarginBottom
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Bottom);
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x000BA032 File Offset: 0x000B8232
		public float LayoutMarginStart
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.Start);
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060030F9 RID: 12537 RVA: 0x000BA046 File Offset: 0x000B8246
		public float LayoutMarginEnd
		{
			get
			{
				return this.GetLayoutValue(this.Layout.MarginBuffer, LayoutEdge.End);
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000BA05A File Offset: 0x000B825A
		public float LayoutPaddingLeft
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Left);
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060030FB RID: 12539 RVA: 0x000BA06E File Offset: 0x000B826E
		public float LayoutPaddingTop
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Top);
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000BA082 File Offset: 0x000B8282
		public float LayoutPaddingRight
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Right);
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x060030FD RID: 12541 RVA: 0x000BA096 File Offset: 0x000B8296
		public float LayoutPaddingBottom
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Bottom);
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000BA0AA File Offset: 0x000B82AA
		public float LayoutPaddingStart
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.Start);
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x060030FF RID: 12543 RVA: 0x000BA0BE File Offset: 0x000B82BE
		public float LayoutPaddingEnd
		{
			get
			{
				return this.GetLayoutValue(this.Layout.PaddingBuffer, LayoutEdge.End);
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000BA0D2 File Offset: 0x000B82D2
		public float LayoutBorderLeft
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Left);
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x000BA0E6 File Offset: 0x000B82E6
		public float LayoutBorderTop
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Top);
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000BA0FA File Offset: 0x000B82FA
		public float LayoutBorderRight
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Right);
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x000BA10E File Offset: 0x000B830E
		public float LayoutBorderBottom
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Bottom);
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x000BA122 File Offset: 0x000B8322
		public float LayoutBorderStart
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.Start);
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000BA136 File Offset: 0x000B8336
		public float LayoutBorderEnd
		{
			get
			{
				return this.GetLayoutValue(this.Layout.BorderBuffer, LayoutEdge.End);
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x000BA14A File Offset: 0x000B834A
		public float ComputedFlexBasis
		{
			get
			{
				return this.Layout.ComputedFlexBasis;
			}
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000BA158 File Offset: 0x000B8358
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe float GetLayoutValue(float* buffer, LayoutEdge edge)
		{
			if (!true)
			{
			}
			float num;
			if (edge != LayoutEdge.Left)
			{
				if (edge != LayoutEdge.Right)
				{
					num = buffer[(IntPtr)edge];
				}
				else
				{
					num = ((this.Layout.Direction == LayoutDirection.RTL) ? buffer[4] : buffer[5]);
				}
			}
			else
			{
				num = ((this.Layout.Direction == LayoutDirection.RTL) ? buffer[5] : buffer[4]);
			}
			if (!true)
			{
			}
			return num;
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003108 RID: 12552 RVA: 0x000BA1CA File Offset: 0x000B83CA
		// (set) Token: 0x06003109 RID: 12553 RVA: 0x000BA1ED File Offset: 0x000B83ED
		public LayoutNode Parent
		{
			get
			{
				return new LayoutNode(this.m_Access, this.m_Access.GetNodeData(this.m_Handle).Parent);
			}
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).Parent = value.m_Handle;
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x000BA20B File Offset: 0x000B840B
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x000BA22E File Offset: 0x000B842E
		public LayoutNode NextChild
		{
			get
			{
				return new LayoutNode(this.m_Access, this.m_Access.GetNodeData(this.m_Handle).NextChild);
			}
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).NextChild = value.m_Handle;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000BA24C File Offset: 0x000B844C
		private LayoutList<LayoutHandle> Children
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).Children;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x0600310D RID: 12557 RVA: 0x000BA264 File Offset: 0x000B8464
		public int Count
		{
			get
			{
				return this.Children.IsCreated ? this.Children.Count : 0;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		public unsafe LayoutNode this[int index]
		{
			get
			{
				return new LayoutNode(this.m_Access, *this.Children[index]);
			}
			set
			{
				*this.Children[index] = value.Handle;
			}
		}

		// Token: 0x06003110 RID: 12560 RVA: 0x000BA2E8 File Offset: 0x000B84E8
		public void AddChild(LayoutNode child)
		{
			this.Insert(this.Count, child);
		}

		// Token: 0x06003111 RID: 12561 RVA: 0x000BA2FC File Offset: 0x000B84FC
		public void RemoveChild(LayoutNode child)
		{
			ref LayoutNodeData nodeData = ref this.m_Access.GetNodeData(this.m_Handle);
			Assert.IsTrue(nodeData.Children.IsCreated);
			int num = nodeData.Children.IndexOf(child.m_Handle);
			bool flag = num >= 0;
			if (flag)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x000BA354 File Offset: 0x000B8554
		public int IndexOf(LayoutNode child)
		{
			ref LayoutNodeData nodeData = ref this.m_Access.GetNodeData(this.m_Handle);
			bool isCreated = nodeData.Children.IsCreated;
			int num;
			if (isCreated)
			{
				num = nodeData.Children.IndexOf(child.m_Handle);
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x06003113 RID: 12563 RVA: 0x000BA39C File Offset: 0x000B859C
		public void Insert(int index, LayoutNode child)
		{
			ref LayoutNodeData nodeData = ref this.m_Access.GetNodeData(this.m_Handle);
			bool flag = !nodeData.Children.IsCreated;
			if (flag)
			{
				nodeData.Children = new LayoutList<LayoutHandle>(4, Allocator.Persistent);
			}
			nodeData.Children.Insert(index, child.Handle);
			child.Parent = this;
			this.MarkDirty();
		}

		// Token: 0x06003114 RID: 12564 RVA: 0x000BA404 File Offset: 0x000B8604
		public unsafe void RemoveAt(int index)
		{
			ref LayoutNodeData nodeData = ref this.m_Access.GetNodeData(this.m_Handle);
			Assert.IsTrue(nodeData.Children.IsCreated);
			bool flag = (ulong)index >= (ulong)((long)nodeData.Children.Count);
			if (flag)
			{
				throw new ArgumentOutOfRangeException();
			}
			LayoutHandle layoutHandle = *nodeData.Children[index];
			ref LayoutNodeData nodeData2 = ref this.m_Access.GetNodeData(layoutHandle);
			bool flag2 = nodeData2.Parent.Equals(this.m_Handle);
			nodeData2.Parent = LayoutHandle.Undefined;
			nodeData.Children.RemoveAt(index);
			bool flag3 = flag2;
			if (flag3)
			{
				this.MarkDirty();
			}
		}

		// Token: 0x06003115 RID: 12565 RVA: 0x000BA4AC File Offset: 0x000B86AC
		public void Clear()
		{
			ref LayoutNodeData nodeData = ref this.m_Access.GetNodeData(this.m_Handle);
			bool flag = !nodeData.Children.IsCreated;
			if (!flag)
			{
				while (nodeData.Children.Count > 0)
				{
					this.RemoveAt(nodeData.Children.Count - 1);
				}
			}
		}

		// Token: 0x06003116 RID: 12566 RVA: 0x000BA508 File Offset: 0x000B8708
		public LayoutNode.Enumerator GetEnumerator()
		{
			return new LayoutNode.Enumerator(this.m_Access, this.Children);
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x000BA52B File Offset: 0x000B872B
		// (set) Token: 0x06003118 RID: 12568 RVA: 0x000BA538 File Offset: 0x000B8738
		public LayoutDirection StyleDirection
		{
			get
			{
				return this.Style.Direction;
			}
			set
			{
				bool flag = this.Style.Direction == value;
				if (!flag)
				{
					this.Style.Direction = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x000BA56D File Offset: 0x000B876D
		// (set) Token: 0x0600311A RID: 12570 RVA: 0x000BA57C File Offset: 0x000B877C
		public LayoutFlexDirection FlexDirection
		{
			get
			{
				return this.Style.FlexDirection;
			}
			set
			{
				bool flag = this.Style.FlexDirection == value;
				if (!flag)
				{
					this.Style.FlexDirection = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600311B RID: 12571 RVA: 0x000BA5B1 File Offset: 0x000B87B1
		// (set) Token: 0x0600311C RID: 12572 RVA: 0x000BA5C0 File Offset: 0x000B87C0
		public LayoutJustify JustifyContent
		{
			get
			{
				return this.Style.JustifyContent;
			}
			set
			{
				bool flag = this.Style.JustifyContent == value;
				if (!flag)
				{
					this.Style.JustifyContent = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x0600311D RID: 12573 RVA: 0x000BA5F5 File Offset: 0x000B87F5
		// (set) Token: 0x0600311E RID: 12574 RVA: 0x000BA604 File Offset: 0x000B8804
		public LayoutDisplay Display
		{
			get
			{
				return this.Style.Display;
			}
			set
			{
				bool flag = this.Style.Display == value;
				if (!flag)
				{
					this.Style.Display = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x0600311F RID: 12575 RVA: 0x000BA639 File Offset: 0x000B8839
		// (set) Token: 0x06003120 RID: 12576 RVA: 0x000BA648 File Offset: 0x000B8848
		public LayoutAlign AlignItems
		{
			get
			{
				return this.Style.AlignItems;
			}
			set
			{
				bool flag = this.Style.AlignItems == value;
				if (!flag)
				{
					this.Style.AlignItems = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003121 RID: 12577 RVA: 0x000BA67D File Offset: 0x000B887D
		// (set) Token: 0x06003122 RID: 12578 RVA: 0x000BA68C File Offset: 0x000B888C
		public LayoutAlign AlignSelf
		{
			get
			{
				return this.Style.AlignSelf;
			}
			set
			{
				bool flag = this.Style.AlignSelf == value;
				if (!flag)
				{
					this.Style.AlignSelf = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003123 RID: 12579 RVA: 0x000BA6C1 File Offset: 0x000B88C1
		// (set) Token: 0x06003124 RID: 12580 RVA: 0x000BA6D0 File Offset: 0x000B88D0
		public LayoutAlign AlignContent
		{
			get
			{
				return this.Style.AlignContent;
			}
			set
			{
				bool flag = this.Style.AlignContent == value;
				if (!flag)
				{
					this.Style.AlignContent = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003125 RID: 12581 RVA: 0x000BA705 File Offset: 0x000B8905
		// (set) Token: 0x06003126 RID: 12582 RVA: 0x000BA714 File Offset: 0x000B8914
		public LayoutPositionType PositionType
		{
			get
			{
				return this.Style.PositionType;
			}
			set
			{
				bool flag = this.Style.PositionType == value;
				if (!flag)
				{
					this.Style.PositionType = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x06003127 RID: 12583 RVA: 0x000BA749 File Offset: 0x000B8949
		// (set) Token: 0x06003128 RID: 12584 RVA: 0x000BA758 File Offset: 0x000B8958
		public LayoutWrap Wrap
		{
			get
			{
				return this.Style.FlexWrap;
			}
			set
			{
				bool flag = this.Style.FlexWrap == value;
				if (!flag)
				{
					this.Style.FlexWrap = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000BA78D File Offset: 0x000B898D
		// (set) Token: 0x0600312A RID: 12586 RVA: 0x000BA79A File Offset: 0x000B899A
		public float FlexGrow
		{
			get
			{
				return this.Style.FlexGrow;
			}
			set
			{
				this.SetValue(ref this.Style.FlexGrow, value);
			}
		}

		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600312B RID: 12587 RVA: 0x000BA7AF File Offset: 0x000B89AF
		// (set) Token: 0x0600312C RID: 12588 RVA: 0x000BA7BC File Offset: 0x000B89BC
		public float FlexShrink
		{
			get
			{
				return this.Style.FlexShrink;
			}
			set
			{
				this.SetValue(ref this.Style.FlexShrink, value);
			}
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000BA7D1 File Offset: 0x000B89D1
		// (set) Token: 0x0600312E RID: 12590 RVA: 0x000BA7DE File Offset: 0x000B89DE
		public LayoutValue FlexBasis
		{
			get
			{
				return this.Style.FlexBasis;
			}
			set
			{
				this.SetStyleValueUnit(ref this.Style.FlexBasis, value);
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000BA7F3 File Offset: 0x000B89F3
		// (set) Token: 0x06003130 RID: 12592 RVA: 0x000BA80B File Offset: 0x000B8A0B
		public unsafe LayoutValue Width
		{
			get
			{
				return *this.Style.dimensions[0];
			}
			set
			{
				this.SetStyleValueUnit(this.Style.dimensions[0], value);
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x000BA826 File Offset: 0x000B8A26
		// (set) Token: 0x06003132 RID: 12594 RVA: 0x000BA83E File Offset: 0x000B8A3E
		public unsafe LayoutValue Height
		{
			get
			{
				return *this.Style.dimensions[1];
			}
			set
			{
				this.SetStyleValueUnit(this.Style.dimensions[1], value);
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003133 RID: 12595 RVA: 0x000BA859 File Offset: 0x000B8A59
		// (set) Token: 0x06003134 RID: 12596 RVA: 0x000BA871 File Offset: 0x000B8A71
		public unsafe LayoutValue MaxWidth
		{
			get
			{
				return *this.Style.maxDimensions[0];
			}
			set
			{
				this.SetStyleValue(this.Style.maxDimensions[0], value);
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003135 RID: 12597 RVA: 0x000BA88C File Offset: 0x000B8A8C
		// (set) Token: 0x06003136 RID: 12598 RVA: 0x000BA8A4 File Offset: 0x000B8AA4
		public unsafe LayoutValue MaxHeight
		{
			get
			{
				return *this.Style.maxDimensions[1];
			}
			set
			{
				this.SetStyleValue(this.Style.maxDimensions[1], value);
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x000BA8BF File Offset: 0x000B8ABF
		// (set) Token: 0x06003138 RID: 12600 RVA: 0x000BA8D7 File Offset: 0x000B8AD7
		public unsafe LayoutValue MinWidth
		{
			get
			{
				return *this.Style.minDimensions[0];
			}
			set
			{
				this.SetStyleValue(this.Style.minDimensions[0], value);
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000BA8F2 File Offset: 0x000B8AF2
		// (set) Token: 0x0600313A RID: 12602 RVA: 0x000BA90A File Offset: 0x000B8B0A
		public unsafe LayoutValue MinHeight
		{
			get
			{
				return *this.Style.minDimensions[1];
			}
			set
			{
				this.SetStyleValue(this.Style.minDimensions[1], value);
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x000BA925 File Offset: 0x000B8B25
		// (set) Token: 0x0600313C RID: 12604 RVA: 0x000BA932 File Offset: 0x000B8B32
		public float AspectRatio
		{
			get
			{
				return this.Style.AspectRatio;
			}
			set
			{
				this.SetValue(ref this.Style.AspectRatio, value);
			}
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600313D RID: 12605 RVA: 0x000BA947 File Offset: 0x000B8B47
		// (set) Token: 0x0600313E RID: 12606 RVA: 0x000BA954 File Offset: 0x000B8B54
		public LayoutOverflow Overflow
		{
			get
			{
				return this.Style.Overflow;
			}
			set
			{
				bool flag = this.Style.Overflow == value;
				if (!flag)
				{
					this.Style.Overflow = value;
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x000BA989 File Offset: 0x000B8B89
		// (set) Token: 0x06003140 RID: 12608 RVA: 0x000BA9A1 File Offset: 0x000B8BA1
		public unsafe LayoutValue Left
		{
			get
			{
				return *this.Style.position[0];
			}
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Left, value);
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000BA9AC File Offset: 0x000B8BAC
		// (set) Token: 0x06003142 RID: 12610 RVA: 0x000BA9C4 File Offset: 0x000B8BC4
		public unsafe LayoutValue Top
		{
			get
			{
				return *this.Style.position[1];
			}
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Top, value);
			}
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000BA9CF File Offset: 0x000B8BCF
		// (set) Token: 0x06003144 RID: 12612 RVA: 0x000BA9E7 File Offset: 0x000B8BE7
		public unsafe LayoutValue Right
		{
			get
			{
				return *this.Style.position[2];
			}
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Right, value);
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000BA9F2 File Offset: 0x000B8BF2
		// (set) Token: 0x06003146 RID: 12614 RVA: 0x000BAA0A File Offset: 0x000B8C0A
		public unsafe LayoutValue Bottom
		{
			get
			{
				return *this.Style.position[3];
			}
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Bottom, value);
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000BAA15 File Offset: 0x000B8C15
		// (set) Token: 0x06003148 RID: 12616 RVA: 0x000BAA2D File Offset: 0x000B8C2D
		public unsafe LayoutValue Start
		{
			get
			{
				return *this.Style.position[4];
			}
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.Start, value);
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000BAA38 File Offset: 0x000B8C38
		// (set) Token: 0x0600314A RID: 12618 RVA: 0x000BAA50 File Offset: 0x000B8C50
		public unsafe LayoutValue End
		{
			get
			{
				return *this.Style.position[5];
			}
			set
			{
				this.SetStyleEdgePosition(LayoutEdge.End, value);
			}
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x000BAA5B File Offset: 0x000B8C5B
		// (set) Token: 0x0600314C RID: 12620 RVA: 0x000BAA73 File Offset: 0x000B8C73
		public unsafe LayoutValue MarginLeft
		{
			get
			{
				return *this.Style.margin[0];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Left, value);
			}
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x000BAA7E File Offset: 0x000B8C7E
		// (set) Token: 0x0600314E RID: 12622 RVA: 0x000BAA96 File Offset: 0x000B8C96
		public unsafe LayoutValue MarginTop
		{
			get
			{
				return *this.Style.margin[1];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Top, value);
			}
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000BAAA1 File Offset: 0x000B8CA1
		// (set) Token: 0x06003150 RID: 12624 RVA: 0x000BAAB9 File Offset: 0x000B8CB9
		public unsafe LayoutValue MarginRight
		{
			get
			{
				return *this.Style.margin[2];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Right, value);
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000BAAC4 File Offset: 0x000B8CC4
		// (set) Token: 0x06003152 RID: 12626 RVA: 0x000BAADC File Offset: 0x000B8CDC
		public unsafe LayoutValue MarginBottom
		{
			get
			{
				return *this.Style.margin[3];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Bottom, value);
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000BAAE7 File Offset: 0x000B8CE7
		// (set) Token: 0x06003154 RID: 12628 RVA: 0x000BAAFF File Offset: 0x000B8CFF
		public unsafe LayoutValue MarginStart
		{
			get
			{
				return *this.Style.margin[4];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Start, value);
			}
		}

		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x000BAB0A File Offset: 0x000B8D0A
		// (set) Token: 0x06003156 RID: 12630 RVA: 0x000BAB22 File Offset: 0x000B8D22
		public unsafe LayoutValue MarginEnd
		{
			get
			{
				return *this.Style.margin[5];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.End, value);
			}
		}

		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x000BAB2D File Offset: 0x000B8D2D
		// (set) Token: 0x06003158 RID: 12632 RVA: 0x000BAB45 File Offset: 0x000B8D45
		public unsafe LayoutValue MarginHorizontal
		{
			get
			{
				return *this.Style.margin[6];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Horizontal, value);
			}
		}

		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000BAB50 File Offset: 0x000B8D50
		// (set) Token: 0x0600315A RID: 12634 RVA: 0x000BAB68 File Offset: 0x000B8D68
		public unsafe LayoutValue MarginVertical
		{
			get
			{
				return *this.Style.margin[7];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.Vertical, value);
			}
		}

		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000BAB73 File Offset: 0x000B8D73
		// (set) Token: 0x0600315C RID: 12636 RVA: 0x000BAB8B File Offset: 0x000B8D8B
		public unsafe LayoutValue Margin
		{
			get
			{
				return *this.Style.margin[8];
			}
			set
			{
				this.SetStyleEdgeMargin(LayoutEdge.All, value);
			}
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000BAB96 File Offset: 0x000B8D96
		// (set) Token: 0x0600315E RID: 12638 RVA: 0x000BABAE File Offset: 0x000B8DAE
		public unsafe LayoutValue PaddingLeft
		{
			get
			{
				return *this.Style.padding[0];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Left, value);
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000BABB9 File Offset: 0x000B8DB9
		// (set) Token: 0x06003160 RID: 12640 RVA: 0x000BABD1 File Offset: 0x000B8DD1
		public unsafe LayoutValue PaddingTop
		{
			get
			{
				return *this.Style.padding[1];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Top, value);
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000BABDC File Offset: 0x000B8DDC
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x000BABF4 File Offset: 0x000B8DF4
		public unsafe LayoutValue PaddingRight
		{
			get
			{
				return *this.Style.padding[2];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Right, value);
			}
		}

		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000BABFF File Offset: 0x000B8DFF
		// (set) Token: 0x06003164 RID: 12644 RVA: 0x000BAC17 File Offset: 0x000B8E17
		public unsafe LayoutValue PaddingBottom
		{
			get
			{
				return *this.Style.padding[3];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Bottom, value);
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000BAC22 File Offset: 0x000B8E22
		// (set) Token: 0x06003166 RID: 12646 RVA: 0x000BAC3A File Offset: 0x000B8E3A
		public unsafe LayoutValue PaddingStart
		{
			get
			{
				return *this.Style.padding[4];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Start, value);
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000BAC45 File Offset: 0x000B8E45
		// (set) Token: 0x06003168 RID: 12648 RVA: 0x000BAC5D File Offset: 0x000B8E5D
		public unsafe LayoutValue PaddingEnd
		{
			get
			{
				return *this.Style.padding[5];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.End, value);
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000BAC68 File Offset: 0x000B8E68
		// (set) Token: 0x0600316A RID: 12650 RVA: 0x000BAC80 File Offset: 0x000B8E80
		public unsafe LayoutValue PaddingHorizontal
		{
			get
			{
				return *this.Style.padding[6];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Horizontal, value);
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000BAC8B File Offset: 0x000B8E8B
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x000BACA3 File Offset: 0x000B8EA3
		public unsafe LayoutValue PaddingVertical
		{
			get
			{
				return *this.Style.padding[7];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.Vertical, value);
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000BACAE File Offset: 0x000B8EAE
		// (set) Token: 0x0600316E RID: 12654 RVA: 0x000BACC6 File Offset: 0x000B8EC6
		public unsafe LayoutValue Padding
		{
			get
			{
				return *this.Style.padding[8];
			}
			set
			{
				this.SetStyleEdgePadding(LayoutEdge.All, value);
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000BACD1 File Offset: 0x000B8ED1
		// (set) Token: 0x06003170 RID: 12656 RVA: 0x000BACE9 File Offset: 0x000B8EE9
		public float BorderLeftWidth
		{
			get
			{
				return this.Style.border[0].Value;
			}
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[0], value);
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x000BAD04 File Offset: 0x000B8F04
		// (set) Token: 0x06003172 RID: 12658 RVA: 0x000BAD1C File Offset: 0x000B8F1C
		public float BorderTopWidth
		{
			get
			{
				return this.Style.border[1].Value;
			}
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[1], value);
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x000BAD37 File Offset: 0x000B8F37
		// (set) Token: 0x06003174 RID: 12660 RVA: 0x000BAD4F File Offset: 0x000B8F4F
		public float BorderRightWidth
		{
			get
			{
				return this.Style.border[2].Value;
			}
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[2], value);
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x000BAD6A File Offset: 0x000B8F6A
		// (set) Token: 0x06003176 RID: 12662 RVA: 0x000BAD82 File Offset: 0x000B8F82
		public float BorderBottomWidth
		{
			get
			{
				return this.Style.border[3].Value;
			}
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[3], value);
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x000BAD9D File Offset: 0x000B8F9D
		// (set) Token: 0x06003178 RID: 12664 RVA: 0x000BADB5 File Offset: 0x000B8FB5
		public float BorderStartWidth
		{
			get
			{
				return this.Style.border[4].Value;
			}
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[4], value);
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x000BADD0 File Offset: 0x000B8FD0
		// (set) Token: 0x0600317A RID: 12666 RVA: 0x000BADE8 File Offset: 0x000B8FE8
		public float BorderEndWidth
		{
			get
			{
				return this.Style.border[5].Value;
			}
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[5], value);
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600317B RID: 12667 RVA: 0x000BAE03 File Offset: 0x000B9003
		// (set) Token: 0x0600317C RID: 12668 RVA: 0x000BAE1B File Offset: 0x000B901B
		public float BorderWidth
		{
			get
			{
				return this.Style.border[8].Value;
			}
			set
			{
				this.StyleEdgeSetPoint(this.Style.border[8], value);
			}
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x000BAE38 File Offset: 0x000B9038
		private void SetValue(ref float currentValue, float newValue)
		{
			bool flag = currentValue.Equals(newValue);
			if (!flag)
			{
				currentValue = newValue;
				this.MarkDirty();
			}
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x000BAE60 File Offset: 0x000B9060
		private void SetStyleValue(ref LayoutValue currentValue, LayoutValue newValue)
		{
			bool flag = newValue.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.SetStyleValuePercent(ref currentValue, newValue);
			}
			else
			{
				this.SetStyleValuePoint(ref currentValue, newValue);
			}
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x000BAE94 File Offset: 0x000B9094
		private void SetStyleValueUnit(ref LayoutValue currentValue, LayoutValue newValue)
		{
			bool flag = newValue.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.SetStyleValuePercent(ref currentValue, newValue);
			}
			else
			{
				bool flag2 = newValue.Unit == LayoutUnit.Auto;
				if (flag2)
				{
					this.SetStyleValueAuto(ref currentValue);
				}
				else
				{
					this.SetStyleValuePoint(ref currentValue, newValue);
				}
			}
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x000BAEE4 File Offset: 0x000B90E4
		private void SetStyleValuePoint(ref LayoutValue currentValue, LayoutValue newValue)
		{
			bool flag = float.IsNaN(currentValue.Value) && float.IsNaN(newValue.Value) && newValue.Unit == currentValue.Unit;
			if (!flag)
			{
				bool flag2 = currentValue.Value != newValue.Value || currentValue.Unit != LayoutUnit.Point;
				if (flag2)
				{
					bool flag3 = float.IsNaN(newValue.Value);
					if (flag3)
					{
						currentValue = LayoutValue.Auto();
					}
					else
					{
						currentValue = LayoutValue.Point(newValue.Value);
					}
					this.MarkDirty();
				}
			}
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x000BAF80 File Offset: 0x000B9180
		private void SetStyleValuePercent(ref LayoutValue currentValue, LayoutValue newValue)
		{
			bool flag = currentValue.Value != newValue.Value || currentValue.Unit != LayoutUnit.Percent;
			if (flag)
			{
				bool flag2 = float.IsNaN(newValue.Value);
				if (flag2)
				{
					currentValue = LayoutValue.Auto();
				}
				else
				{
					currentValue = newValue;
				}
				this.MarkDirty();
			}
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x000BAFE0 File Offset: 0x000B91E0
		private void SetStyleValueAuto(ref LayoutValue currentValue)
		{
			bool flag = currentValue.Unit != LayoutUnit.Auto;
			if (flag)
			{
				currentValue = LayoutValue.Auto();
				this.MarkDirty();
			}
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x000BB014 File Offset: 0x000B9214
		private void SetStyleEdgePosition(LayoutEdge edge, LayoutValue value)
		{
			bool flag = value.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.StyleEdgeSetPercent(this.Style.position[(int)edge], value.Value);
			}
			else
			{
				this.StyleEdgeSetPoint(this.Style.position[(int)edge], value.Value);
			}
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x000BB074 File Offset: 0x000B9274
		private void SetStyleEdgeMargin(LayoutEdge edge, LayoutValue value)
		{
			bool flag = value.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.StyleEdgeSetPercent(this.Style.margin[(int)edge], value.Value);
			}
			else
			{
				bool flag2 = value.Unit == LayoutUnit.Auto;
				if (flag2)
				{
					this.StyleEdgeSetAuto(this.Style.margin[(int)edge]);
				}
				else
				{
					this.StyleEdgeSetPoint(this.Style.margin[(int)edge], value.Value);
				}
			}
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x000BB100 File Offset: 0x000B9300
		private void SetStyleEdgePadding(LayoutEdge edge, LayoutValue value)
		{
			bool flag = value.Unit == LayoutUnit.Percent;
			if (flag)
			{
				this.StyleEdgeSetPercent(this.Style.padding[(int)edge], value.Value);
			}
			else
			{
				this.StyleEdgeSetPoint(this.Style.padding[(int)edge], value.Value);
			}
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x000BB160 File Offset: 0x000B9360
		private void StyleEdgeSetPercent(ref LayoutValue value, float newValue)
		{
			bool flag = value.Value != newValue || value.Unit != LayoutUnit.Percent;
			if (flag)
			{
				value = (float.IsNaN(newValue) ? LayoutValue.Undefined() : LayoutValue.Percent(newValue));
				this.MarkDirty();
			}
		}

		// Token: 0x06003187 RID: 12679 RVA: 0x000BB1B0 File Offset: 0x000B93B0
		private void StyleEdgeSetAuto(ref LayoutValue value)
		{
			bool flag = value.Unit != LayoutUnit.Auto;
			if (flag)
			{
				value = LayoutValue.Auto();
				this.MarkDirty();
			}
		}

		// Token: 0x06003188 RID: 12680 RVA: 0x000BB1E4 File Offset: 0x000B93E4
		private void StyleEdgeSetPoint(ref LayoutValue value, float newValue)
		{
			bool flag = float.IsNaN(value.Value) && float.IsNaN(newValue);
			if (!flag)
			{
				bool flag2 = value.Value != newValue || value.Unit != LayoutUnit.Point;
				if (flag2)
				{
					value = (float.IsNaN(newValue) ? LayoutValue.Undefined() : LayoutValue.Point(newValue));
					this.MarkDirty();
				}
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000BB250 File Offset: 0x000B9450
		public static LayoutNode Undefined
		{
			get
			{
				return new LayoutNode(default(LayoutDataAccess), LayoutHandle.Undefined);
			}
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x000BB270 File Offset: 0x000B9470
		internal LayoutNode(LayoutDataAccess access, LayoutHandle handle)
		{
			this.m_Access = access;
			this.m_Handle = handle;
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x000BB281 File Offset: 0x000B9481
		public bool IsUndefined
		{
			get
			{
				return this.m_Handle.Equals(LayoutHandle.Undefined);
			}
		}

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x000BB293 File Offset: 0x000B9493
		public LayoutHandle Handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000BB29B File Offset: 0x000B949B
		public ref LayoutComputedData Layout
		{
			get
			{
				return this.m_Access.GetComputedData(this.m_Handle);
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600318E RID: 12686 RVA: 0x000BB2AE File Offset: 0x000B94AE
		public ref LayoutStyleData Style
		{
			get
			{
				return this.m_Access.GetStyleData(this.m_Handle);
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600318F RID: 12687 RVA: 0x000BB2C1 File Offset: 0x000B94C1
		// (set) Token: 0x06003190 RID: 12688 RVA: 0x000BB2D9 File Offset: 0x000B94D9
		public bool IsDirty
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).IsDirty;
			}
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).IsDirty = value;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06003191 RID: 12689 RVA: 0x000BB2F3 File Offset: 0x000B94F3
		// (set) Token: 0x06003192 RID: 12690 RVA: 0x000BB30B File Offset: 0x000B950B
		public bool HasNewLayout
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).HasNewLayout;
			}
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).HasNewLayout = value;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06003193 RID: 12691 RVA: 0x000BB325 File Offset: 0x000B9525
		public bool IsMeasureDefined
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).ManagedMeasureFunctionIndex != 0;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06003194 RID: 12692 RVA: 0x000BB340 File Offset: 0x000B9540
		// (set) Token: 0x06003195 RID: 12693 RVA: 0x000BB353 File Offset: 0x000B9553
		public LayoutMeasureFunction Measure
		{
			get
			{
				return this.m_Access.GetMeasureFunction(this.m_Handle);
			}
			set
			{
				this.m_Access.SetMeasureFunction(this.m_Handle, value);
			}
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x000BB368 File Offset: 0x000B9568
		public void SetOwner(VisualElement func)
		{
			this.m_Access.SetOwner(this.m_Handle, func);
		}

		// Token: 0x06003197 RID: 12695 RVA: 0x000BB380 File Offset: 0x000B9580
		public VisualElement GetOwner()
		{
			return this.m_Access.GetOwner(this.m_Handle);
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06003198 RID: 12696 RVA: 0x000BB3A3 File Offset: 0x000B95A3
		public bool IsBaselineDefined
		{
			get
			{
				return this.m_Access.GetNodeData(this.m_Handle).ManagedBaselineFunctionIndex != 0;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x000BB3BE File Offset: 0x000B95BE
		// (set) Token: 0x0600319A RID: 12698 RVA: 0x000BB3D1 File Offset: 0x000B95D1
		public LayoutBaselineFunction Baseline
		{
			get
			{
				return this.m_Access.GetBaselineFunction(this.m_Handle);
			}
			set
			{
				this.m_Access.SetBaselineFunction(this.m_Handle, value);
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000BB3E6 File Offset: 0x000B95E6
		public ref int LineIndex
		{
			get
			{
				return ref this.m_Access.GetNodeData(this.m_Handle).LineIndex;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000BB3FE File Offset: 0x000B95FE
		// (set) Token: 0x0600319D RID: 12701 RVA: 0x000BB421 File Offset: 0x000B9621
		public LayoutConfig Config
		{
			get
			{
				return new LayoutConfig(this.m_Access, this.m_Access.GetNodeData(this.m_Handle).Config);
			}
			set
			{
				this.m_Access.GetNodeData(this.m_Handle).Config = value.Handle;
			}
		}

		// Token: 0x0600319E RID: 12702 RVA: 0x000BB440 File Offset: 0x000B9640
		public void MarkDirty()
		{
			bool isDirty = this.IsDirty;
			if (!isDirty)
			{
				this.IsDirty = true;
				this.Layout.ComputedFlexBasis = float.NaN;
				bool flag = !this.Parent.IsUndefined;
				if (flag)
				{
					this.Parent.MarkDirty();
				}
			}
		}

		// Token: 0x0600319F RID: 12703 RVA: 0x000BB496 File Offset: 0x000B9696
		public void MarkLayoutSeen()
		{
			this.HasNewLayout = false;
		}

		// Token: 0x060031A0 RID: 12704 RVA: 0x000BB4A4 File Offset: 0x000B96A4
		public void CopyFromComputedStyle(ComputedStyle style)
		{
			this.FlexGrow = style.flexGrow;
			this.FlexShrink = style.flexShrink;
			this.FlexBasis = style.flexBasis.ToLayoutValue();
			this.Left = style.left.ToLayoutValue();
			this.Top = style.top.ToLayoutValue();
			this.Right = style.right.ToLayoutValue();
			this.Bottom = style.bottom.ToLayoutValue();
			this.MarginLeft = style.marginLeft.ToLayoutValue();
			this.MarginTop = style.marginTop.ToLayoutValue();
			this.MarginRight = style.marginRight.ToLayoutValue();
			this.MarginBottom = style.marginBottom.ToLayoutValue();
			this.PaddingLeft = style.paddingLeft.ToLayoutValue();
			this.PaddingTop = style.paddingTop.ToLayoutValue();
			this.PaddingRight = style.paddingRight.ToLayoutValue();
			this.PaddingBottom = style.paddingBottom.ToLayoutValue();
			this.BorderLeftWidth = style.borderLeftWidth;
			this.BorderTopWidth = style.borderTopWidth;
			this.BorderRightWidth = style.borderRightWidth;
			this.BorderBottomWidth = style.borderBottomWidth;
			this.Width = style.width.ToLayoutValue();
			this.Height = style.height.ToLayoutValue();
			this.PositionType = (LayoutPositionType)style.position;
			this.Overflow = (LayoutOverflow)style.overflow;
			this.AlignSelf = (LayoutAlign)style.alignSelf;
			this.MaxWidth = style.maxWidth.ToLayoutValue();
			this.MaxHeight = style.maxHeight.ToLayoutValue();
			this.MinWidth = style.minWidth.ToLayoutValue();
			this.MinHeight = style.minHeight.ToLayoutValue();
			this.FlexDirection = (LayoutFlexDirection)style.flexDirection;
			this.AlignContent = (LayoutAlign)style.alignContent;
			this.AlignItems = (LayoutAlign)style.alignItems;
			this.JustifyContent = (LayoutJustify)style.justifyContent;
			this.Wrap = (LayoutWrap)style.flexWrap;
			this.Display = (LayoutDisplay)style.display;
		}

		// Token: 0x060031A1 RID: 12705 RVA: 0x000BB6F0 File Offset: 0x000B98F0
		public unsafe void CopyStyle(LayoutNode node)
		{
			bool flag = false;
			fixed (LayoutStyleData* style = this.Style)
			{
				LayoutStyleData* ptr = style;
				fixed (LayoutStyleData* style2 = node.Style)
				{
					LayoutStyleData* ptr2 = style2;
					bool flag2 = UnsafeUtility.MemCmp((void*)ptr, (void*)ptr2, (long)UnsafeUtility.SizeOf<LayoutStyleData>()) != 0;
					if (flag2)
					{
						*this.Style = *node.Style;
						flag = true;
					}
				}
			}
			bool flag3 = flag;
			if (flag3)
			{
				this.MarkDirty();
			}
		}

		// Token: 0x060031A2 RID: 12706 RVA: 0x000BB764 File Offset: 0x000B9964
		public void SoftReset()
		{
			ref LayoutNodeData nodeData = ref this.m_Access.GetNodeData(this.m_Handle);
			nodeData.HasNewLayout = true;
		}

		// Token: 0x060031A3 RID: 12707 RVA: 0x000BB78C File Offset: 0x000B998C
		public unsafe void Reset()
		{
			ref LayoutNodeData nodeData = ref this.m_Access.GetNodeData(this.m_Handle);
			Assert.IsTrue(!nodeData.Children.IsCreated || nodeData.Children.Count == 0, "Cannot reset a node which still has children attached");
			nodeData.Parent = default(LayoutHandle);
			nodeData.HasNewLayout = true;
			ref LayoutNodeData ptr = ref nodeData;
			FixedBuffer2<LayoutValue> fixedBuffer = default(FixedBuffer2<LayoutValue>);
			*fixedBuffer[0] = LayoutValue.Undefined();
			*fixedBuffer[1] = LayoutValue.Undefined();
			ptr.ResolvedDimensions = fixedBuffer;
			this.Measure = null;
			this.Baseline = null;
			this.SetOwner(null);
			*this.Layout = LayoutComputedData.Default;
			*this.Style = LayoutStyleData.Default;
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000BB854 File Offset: 0x000B9A54
		public bool Equals(LayoutNode other)
		{
			return this.m_Handle.Equals(other.m_Handle);
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000BB878 File Offset: 0x000B9A78
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is LayoutNode)
			{
				LayoutNode layoutNode = (LayoutNode)obj;
				flag = this.Equals(layoutNode);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000BB8A4 File Offset: 0x000B9AA4
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode();
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000BB8C8 File Offset: 0x000B9AC8
		public static bool operator ==(LayoutNode lhs, LayoutNode rhs)
		{
			bool isUndefined = lhs.IsUndefined;
			bool flag;
			if (isUndefined)
			{
				bool isUndefined2 = rhs.IsUndefined;
				flag = isUndefined2;
			}
			else
			{
				flag = lhs.Equals(rhs);
			}
			return flag;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000BB901 File Offset: 0x000B9B01
		public static bool operator !=(LayoutNode lhs, LayoutNode rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000BB90D File Offset: 0x000B9B0D
		public void CalculateLayout(float width = float.NaN, float height = float.NaN)
		{
			LayoutProcessor.CalculateLayout(this, width, height, this.Style.Direction);
		}

		// Token: 0x04001830 RID: 6192
		private const int k_DefaultChildCapacity = 4;

		// Token: 0x04001831 RID: 6193
		private readonly LayoutDataAccess m_Access;

		// Token: 0x04001832 RID: 6194
		private readonly LayoutHandle m_Handle;

		// Token: 0x02000694 RID: 1684
		public struct Enumerator : IEnumerator<LayoutNode>, IEnumerator, IDisposable
		{
			// Token: 0x060031AA RID: 12714 RVA: 0x000BB929 File Offset: 0x000B9B29
			public Enumerator(LayoutDataAccess access, LayoutList<LayoutHandle> children)
			{
				this.m_Access = access;
				this.m_Enumerator = children.GetEnumerator();
			}

			// Token: 0x17000C04 RID: 3076
			// (get) Token: 0x060031AB RID: 12715 RVA: 0x000BB940 File Offset: 0x000B9B40
			public LayoutNode Current
			{
				get
				{
					return new LayoutNode(this.m_Access, this.m_Enumerator.Current);
				}
			}

			// Token: 0x17000C05 RID: 3077
			// (get) Token: 0x060031AC RID: 12716 RVA: 0x000BB958 File Offset: 0x000B9B58
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060031AD RID: 12717 RVA: 0x00002152 File Offset: 0x00000352
			public void Dispose()
			{
			}

			// Token: 0x060031AE RID: 12718 RVA: 0x000BB965 File Offset: 0x000B9B65
			public void Reset()
			{
				this.m_Enumerator.Reset();
			}

			// Token: 0x060031AF RID: 12719 RVA: 0x000BB974 File Offset: 0x000B9B74
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x04001833 RID: 6195
			private readonly LayoutDataAccess m_Access;

			// Token: 0x04001834 RID: 6196
			private LayoutList<LayoutHandle>.Enumerator m_Enumerator;
		}
	}
}
