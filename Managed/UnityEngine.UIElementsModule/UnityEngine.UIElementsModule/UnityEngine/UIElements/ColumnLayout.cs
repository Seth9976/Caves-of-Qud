using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000147 RID: 327
	internal class ColumnLayout
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0002DBE6 File Offset: 0x0002BDE6
		public Columns columns
		{
			get
			{
				return this.m_Columns;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002DBEE File Offset: 0x0002BDEE
		public bool isDirty
		{
			get
			{
				return this.m_IsDirty;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0002DBF8 File Offset: 0x0002BDF8
		public float columnsWidth
		{
			get
			{
				bool columnsWidthDirty = this.m_ColumnsWidthDirty;
				if (columnsWidthDirty)
				{
					this.m_ColumnsWidth = 0f;
					foreach (Column column in this.m_Columns.visibleList)
					{
						bool flag = float.IsNaN(column.desiredWidth);
						if (!flag)
						{
							this.m_ColumnsWidth += column.desiredWidth;
						}
					}
					this.m_ColumnsWidthDirty = false;
				}
				return this.m_ColumnsWidth;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0002DC98 File Offset: 0x0002BE98
		public float layoutWidth
		{
			get
			{
				return this.m_LayoutWidth;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002DCA0 File Offset: 0x0002BEA0
		public float minColumnsWidth
		{
			get
			{
				return this.m_MinColumnsWidth;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x0002DCA8 File Offset: 0x0002BEA8
		public float maxColumnsWidth
		{
			get
			{
				return this.m_MaxColumnsWidth;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002DCB0 File Offset: 0x0002BEB0
		public bool hasStretchableColumns
		{
			get
			{
				return this.m_StretchableColumns.Count > 0;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0002DCC0 File Offset: 0x0002BEC0
		public bool hasRelativeWidthColumns
		{
			get
			{
				return this.m_RelativeWidthColumns.Count > 0 || this.m_MixedWidthColumns.Count > 0;
			}
		}

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06000A88 RID: 2696 RVA: 0x0002DCE4 File Offset: 0x0002BEE4
		// (remove) Token: 0x06000A89 RID: 2697 RVA: 0x0002DD1C File Offset: 0x0002BF1C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action layoutRequested;

		// Token: 0x06000A8A RID: 2698 RVA: 0x0002DD54 File Offset: 0x0002BF54
		public ColumnLayout(Columns columns)
		{
			this.m_Columns = columns;
			for (int i = 0; i < columns.Count; i++)
			{
				this.OnColumnAdded(columns[i], i);
			}
			columns.columnAdded += this.OnColumnAdded;
			columns.columnRemoved += this.OnColumnRemoved;
			columns.columnReordered += this.OnColumnReordered;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0002DE68 File Offset: 0x0002C068
		public void Dirty()
		{
			bool isDirty = this.m_IsDirty;
			if (!isDirty)
			{
				this.m_IsDirty = true;
				this.ClearCache();
				Action action = this.layoutRequested;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002DEA2 File Offset: 0x0002C0A2
		private void OnColumnAdded(Column column, int index)
		{
			column.changed += this.OnColumnChanged;
			column.resized += this.OnColumnResized;
			this.Dirty();
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0002DED2 File Offset: 0x0002C0D2
		private void OnColumnRemoved(Column column)
		{
			column.changed -= this.OnColumnChanged;
			column.resized -= this.OnColumnResized;
			this.Dirty();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0002DF02 File Offset: 0x0002C102
		private void OnColumnReordered(Column column, int from, int to)
		{
			this.Dirty();
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002DF0C File Offset: 0x0002C10C
		private bool RequiresLayoutUpdate(ColumnDataType type)
		{
			return type - ColumnDataType.Visibility <= 4 || type - ColumnDataType.HeaderTemplate <= 1;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002DF38 File Offset: 0x0002C138
		private void OnColumnChanged(Column column, ColumnDataType type)
		{
			bool flag = this.m_DragResizing || !this.RequiresLayoutUpdate(type);
			if (!flag)
			{
				this.Dirty();
				bool flag2 = this.m_Columns.stretchMode == Columns.StretchMode.Grow && type == ColumnDataType.Visibility && column.visible && column.width.unit != LengthUnit.Percent && !float.IsNaN(this.m_LayoutWidth);
				if (flag2)
				{
					float num = this.columnsWidth - ((!float.IsNaN(column.desiredWidth)) ? column.desiredWidth : 0f);
					bool flag3 = num > this.layoutWidth;
					if (!flag3)
					{
						bool flag4 = false;
						for (int i = 0; i < this.m_Columns.Count; i++)
						{
							Column column2 = this.m_Columns[i];
							bool flag5 = column2.visible && column2.stretchable && column2 != column;
							if (flag5)
							{
								flag4 = true;
								break;
							}
						}
						bool flag6 = !flag4;
						if (!flag6)
						{
							this.MakeRoomForColumn(column);
						}
					}
				}
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002E054 File Offset: 0x0002C254
		private void MakeRoomForColumn(Column column)
		{
			this.UpdateCache();
			float minWidth = column.GetMinWidth(this.m_LayoutWidth);
			float maxWidth = column.GetMaxWidth(this.m_LayoutWidth);
			float num = column.desiredWidth;
			bool flag = float.IsNaN(num);
			if (flag)
			{
				num = column.GetWidth(this.m_LayoutWidth);
			}
			num = Mathf.Clamp(num, minWidth, maxWidth);
			float num2 = this.columnsWidth - ((!float.IsNaN(column.desiredWidth)) ? column.desiredWidth : 0f);
			float num3 = this.m_LayoutWidth - num2;
			float num4 = num - num3;
			bool flag2 = num4 < 0f;
			if (!flag2)
			{
				List<Column> list;
				using (CollectionPool<List<Column>, Column>.Get(out list))
				{
					List<Column> list2;
					using (CollectionPool<List<Column>, Column>.Get(out list2))
					{
						list.AddRange(this.m_StretchableColumns);
						bool stretchable = column.stretchable;
						if (stretchable)
						{
							list.Remove(column);
						}
						this.StretchResizeColumns(list, list2, list2, ref num4, false, false);
					}
				}
			}
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002E174 File Offset: 0x0002C374
		private void OnColumnResized(Column column)
		{
			this.m_ColumnsWidthDirty = true;
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0002E180 File Offset: 0x0002C380
		private static bool IsClamped(float value, float min, float max)
		{
			return value >= min && value <= max;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0002E1A0 File Offset: 0x0002C3A0
		public void DoLayout(float width)
		{
			this.m_LayoutWidth = width;
			bool isDirty = this.m_IsDirty;
			if (isDirty)
			{
				this.UpdateCache();
			}
			bool hasRelativeWidthColumns = this.hasRelativeWidthColumns;
			if (hasRelativeWidthColumns)
			{
				this.UpdateMinAndMaxColumnsWidth();
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			List<Column> list = new List<Column>();
			List<Column> list2 = new List<Column>();
			foreach (Column column in this.m_Columns)
			{
				bool flag = !column.visible;
				if (!flag)
				{
					float minWidth = column.GetMinWidth(this.m_LayoutWidth);
					float maxWidth = column.GetMaxWidth(this.m_LayoutWidth);
					float width2 = column.GetWidth(this.m_LayoutWidth);
					bool flag2 = float.IsNaN(column.desiredWidth);
					if (flag2)
					{
						bool flag3 = this.m_Columns.stretchMode == Columns.StretchMode.GrowAndFill && column.stretchable;
						if (flag3)
						{
							list.Add(column);
							continue;
						}
						column.desiredWidth = Mathf.Clamp(width2, minWidth, maxWidth);
					}
					else
					{
						bool flag4 = this.m_Columns.stretchMode == Columns.StretchMode.GrowAndFill && column.stretchable;
						if (flag4)
						{
							list2.Add(column);
							num4 += this.GetDesiredWidth(column);
						}
						bool flag5 = !ColumnLayout.IsClamped(column.desiredWidth, minWidth, maxWidth);
						if (flag5)
						{
							column.desiredWidth = Mathf.Clamp(width2, minWidth, maxWidth);
						}
						bool flag6 = this.columns.stretchMode == Columns.StretchMode.Grow && column.width.unit == LengthUnit.Percent;
						if (flag6)
						{
							float desiredWidth = column.desiredWidth;
							column.desiredWidth = Mathf.Clamp(width2, minWidth, maxWidth);
							num3 += column.desiredWidth - desiredWidth;
						}
					}
					bool flag7 = !column.stretchable;
					if (flag7)
					{
						num2 += column.desiredWidth;
					}
					num += column.desiredWidth;
				}
			}
			bool flag8 = list.Count > 0;
			if (flag8)
			{
				float num5 = Math.Max(0f, width - num2);
				int num6 = this.m_StretchableColumns.Count;
				list.Sort((Column c1, Column c2) => c1.GetMaxWidth(this.m_LayoutWidth).CompareTo(c2.GetMaxWidth(this.m_LayoutWidth)));
				foreach (Column column2 in list)
				{
					float num7 = num5 / (float)num6;
					column2.desiredWidth = Mathf.Clamp(num7, column2.GetMinWidth(this.m_LayoutWidth), column2.GetMaxWidth(this.m_LayoutWidth));
					num5 = Math.Max(0f, num5 - column2.desiredWidth);
					num6--;
				}
				list2.Sort((Column c1, Column c2) => c1.GetMaxWidth(this.m_LayoutWidth).CompareTo(c2.GetMaxWidth(this.m_LayoutWidth)));
				foreach (Column column3 in list2)
				{
					float desiredWidth2 = this.GetDesiredWidth(column3);
					float num8 = desiredWidth2 / num4;
					float num9 = num5 * num8;
					column3.desiredWidth = Mathf.Clamp(num9, column3.GetMinWidth(this.m_LayoutWidth), column3.GetMaxWidth(this.m_LayoutWidth));
					num5 = Math.Max(0f, num5 - column3.desiredWidth);
					num4 -= desiredWidth2;
					num6--;
				}
			}
			bool flag9 = this.hasStretchableColumns || (this.hasRelativeWidthColumns && this.m_Columns.stretchMode == Columns.StretchMode.GrowAndFill);
			if (flag9)
			{
				float num10 = 0f;
				bool flag10 = this.m_Columns.stretchMode == Columns.StretchMode.Grow;
				if (flag10)
				{
					bool flag11 = !float.IsNaN(this.m_PreviousWidth);
					if (flag11)
					{
						bool flag12 = this.m_PreviousWidth < width;
						if (flag12)
						{
							bool flag13 = width > this.columnsWidth;
							if (flag13)
							{
								num10 = Math.Max(this.m_PreviousWidth, this.columnsWidth) - width + num3;
							}
							else
							{
								num10 = 0f;
							}
						}
						else
						{
							bool flag14 = width < this.columnsWidth - 0.5f && this.m_PreviousWidth < this.columnsWidth - 0.5f;
							if (flag14)
							{
								num10 = 0f;
							}
							else
							{
								bool flag15 = width < this.columnsWidth - 0.5f && this.m_PreviousWidth > this.columnsWidth + 0.5f;
								if (flag15)
								{
									num10 = this.columnsWidth - width + num3;
								}
								else
								{
									num10 = this.m_PreviousWidth - width + num3;
								}
							}
						}
					}
				}
				else
				{
					num10 = this.columnsWidth - Mathf.Clamp(width, this.minColumnsWidth, this.maxColumnsWidth);
				}
				bool flag16 = num10 != 0f;
				if (flag16)
				{
					List<Column> list3;
					using (CollectionPool<List<Column>, Column>.Get(out list3))
					{
						List<Column> list4;
						using (CollectionPool<List<Column>, Column>.Get(out list4))
						{
							List<Column> list5;
							using (CollectionPool<List<Column>, Column>.Get(out list5))
							{
								list3.AddRange(this.m_StretchableColumns);
								list4.AddRange(this.m_FixedColumns);
								list5.AddRange(this.m_RelativeWidthColumns);
								this.StretchResizeColumns(list3, list4, list5, ref num10, false, false);
							}
						}
					}
				}
			}
			this.m_PreviousWidth = width;
			this.m_IsDirty = false;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0002E7A4 File Offset: 0x0002C9A4
		public void StretchResizeColumns(List<Column> stretchableColumns, List<Column> fixedColumns, List<Column> relativeWidthColumns, ref float delta, bool resizeToFit, bool dragResize)
		{
			bool flag = stretchableColumns.Count == 0 && relativeWidthColumns.Count == 0 && fixedColumns.Count == 0;
			if (!flag)
			{
				bool flag2 = delta > 0f;
				if (flag2)
				{
					this.DistributeOverflow(stretchableColumns, fixedColumns, relativeWidthColumns, ref delta, resizeToFit, dragResize);
				}
				else
				{
					this.DistributeExcess(stretchableColumns, fixedColumns, relativeWidthColumns, ref delta, resizeToFit, dragResize);
				}
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0002E808 File Offset: 0x0002CA08
		private void DistributeOverflow(List<Column> stretchableColumns, List<Column> fixedColumns, List<Column> relativeWidthColumns, ref float delta, bool resizeToFit, bool dragResize)
		{
			float num = Math.Abs(delta);
			bool flag = !resizeToFit && !dragResize;
			if (flag)
			{
				num = this.RecomputeToDesiredWidth(fixedColumns, num, true, true);
				num = this.RecomputeToDesiredWidth(relativeWidthColumns, num, true, true);
			}
			num = this.RecomputeToMinWidthProportionally(stretchableColumns, num, !resizeToFit && !dragResize);
			if (resizeToFit)
			{
				num = this.RecomputeToMinWidthProportionally(relativeWidthColumns, num, false);
				num = this.RecomputeToMinWidthProportionally(fixedColumns, num, false);
				num = this.RecomputeToMinWidth(relativeWidthColumns, num, false);
				num = this.RecomputeToMinWidth(fixedColumns, num, false);
			}
			else if (dragResize)
			{
				num = this.RecomputeToMinWidth(relativeWidthColumns, num, true);
				num = this.RecomputeToMinWidth(fixedColumns, num, true);
			}
			else
			{
				bool flag2 = num > 0f;
				if (flag2)
				{
					num = this.RecomputeToMinWidth(relativeWidthColumns, num, true);
					num = this.RecomputeToMinWidth(fixedColumns, num, true);
				}
			}
			delta = Math.Max(0f, delta - num);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0002E8E0 File Offset: 0x0002CAE0
		private void DistributeExcess(List<Column> stretchableColumns, List<Column> fixedColumns, List<Column> relativeWidthColumns, ref float delta, bool resizeToFit, bool dragResize)
		{
			float num = Math.Abs(delta);
			bool flag = !resizeToFit && !dragResize;
			if (flag)
			{
				num = this.RecomputeToDesiredWidth(fixedColumns, num, true, false);
				num = this.RecomputeToDesiredWidth(relativeWidthColumns, num, true, false);
			}
			if (dragResize)
			{
				num = this.RecomputeToDesiredWidth(fixedColumns, num, true, false);
				num = this.RecomputeToDesiredWidth(relativeWidthColumns, num, true, false);
			}
			num = this.RecomputeToMaxWidthProportionally(stretchableColumns, num, !resizeToFit && !dragResize);
			if (resizeToFit)
			{
				num = this.RecomputeToMaxWidthProportionally(relativeWidthColumns, num, false);
				num = this.RecomputeToMaxWidthProportionally(fixedColumns, num, false);
				num = this.RecomputeToMaxWidth(relativeWidthColumns, num, false);
				num = this.RecomputeToMaxWidth(fixedColumns, num, false);
			}
			delta += num;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0002E988 File Offset: 0x0002CB88
		private float RecomputeToMaxWidthProportionally(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly = false)
		{
			bool flag = distributedDelta > 0f;
			if (flag)
			{
				columns.Sort((Column c1, Column c2) => c1.GetMaxWidth(this.m_LayoutWidth).CompareTo(c2.GetMaxWidth(this.m_LayoutWidth)));
				float totalColumnWidth = 0f;
				columns.ForEach(delegate(Column c)
				{
					totalColumnWidth += this.GetDesiredWidth(c);
				});
				for (int i = 0; i < columns.Count; i++)
				{
					Column column = columns[i];
					float desiredWidth = this.GetDesiredWidth(column);
					float num = this.GetDesiredWidth(column) / totalColumnWidth;
					float num2 = distributedDelta * num;
					float num3 = 0f;
					float maxWidth = column.GetMaxWidth(this.m_LayoutWidth);
					bool flag2 = this.GetDesiredWidth(column) < maxWidth;
					if (flag2)
					{
						num3 = Math.Min(num2, maxWidth - this.GetDesiredWidth(column));
					}
					bool flag3 = num3 > 0f;
					if (flag3)
					{
						this.ResizeColumn(column, this.GetDesiredWidth(column) + num3, setDesiredWidthOnly);
					}
					totalColumnWidth -= desiredWidth;
					distributedDelta -= num3;
					bool flag4 = distributedDelta <= 0f;
					if (flag4)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002EAB8 File Offset: 0x0002CCB8
		private float RecomputeToMinWidthProportionally(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly = false)
		{
			bool flag = distributedDelta > 0f;
			if (flag)
			{
				columns.Sort((Column c1, Column c2) => c2.GetMinWidth(this.m_LayoutWidth).CompareTo(c1.GetMinWidth(this.m_LayoutWidth)));
				float totalColumnsWidth = 0f;
				columns.ForEach(delegate(Column c)
				{
					totalColumnsWidth += this.GetDesiredWidth(c);
				});
				for (int i = 0; i < columns.Count; i++)
				{
					Column column = columns[i];
					float desiredWidth = this.GetDesiredWidth(column);
					float num = this.GetDesiredWidth(column) / totalColumnsWidth;
					float num2 = distributedDelta * num;
					float num3 = 0f;
					bool flag2 = this.GetDesiredWidth(column) > column.GetMinWidth(this.m_LayoutWidth);
					if (flag2)
					{
						num3 = Math.Min(num2, this.GetDesiredWidth(column) - column.GetMinWidth(this.m_LayoutWidth));
					}
					bool flag3 = num3 > 0f;
					if (flag3)
					{
						this.ResizeColumn(column, this.GetDesiredWidth(column) - num3, setDesiredWidthOnly);
					}
					totalColumnsWidth -= desiredWidth;
					distributedDelta -= num3;
					bool flag4 = distributedDelta <= 0f;
					if (flag4)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002EBEC File Offset: 0x0002CDEC
		private float RecomputeToDesiredWidth(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly, bool distributeOverflow)
		{
			if (distributeOverflow)
			{
				for (int i = columns.Count - 1; i >= 0; i--)
				{
					distributedDelta = this.RecomputeToDesiredWidth(columns[i], distributedDelta, setDesiredWidthOnly, true);
					bool flag = distributedDelta <= 0f;
					if (flag)
					{
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < columns.Count; j++)
				{
					distributedDelta = this.RecomputeToDesiredWidth(columns[j], distributedDelta, setDesiredWidthOnly, false);
					bool flag2 = distributedDelta <= 0f;
					if (flag2)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002EC90 File Offset: 0x0002CE90
		private float RecomputeToDesiredWidth(Column column, float distributedDelta, bool setDesiredWidthOnly, bool distributeOverflow)
		{
			float num = 0f;
			float num2 = Mathf.Clamp(column.GetWidth(this.m_LayoutWidth), column.GetMinWidth(this.m_LayoutWidth), column.GetMaxWidth(this.m_LayoutWidth));
			bool flag = this.GetDesiredWidth(column) > num2 && distributeOverflow;
			if (flag)
			{
				num = Math.Min(distributedDelta, Math.Abs(this.GetDesiredWidth(column) - num2));
			}
			bool flag2 = this.GetDesiredWidth(column) < num2 && !distributeOverflow;
			if (flag2)
			{
				num = Math.Min(distributedDelta, Math.Abs(num2 - this.GetDesiredWidth(column)));
			}
			float num3 = (distributeOverflow ? (this.GetDesiredWidth(column) - num) : (this.GetDesiredWidth(column) + num));
			bool flag3 = num > 0f;
			if (flag3)
			{
				this.ResizeColumn(column, num3, setDesiredWidthOnly);
			}
			distributedDelta -= num;
			return distributedDelta;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002ED60 File Offset: 0x0002CF60
		private float RecomputeToMinWidth(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly = false)
		{
			bool flag = distributedDelta > 0f;
			if (flag)
			{
				for (int i = columns.Count - 1; i >= 0; i--)
				{
					Column column = columns[i];
					float num = 0f;
					bool flag2 = this.GetDesiredWidth(column) > column.GetMinWidth(this.m_LayoutWidth);
					if (flag2)
					{
						num = Math.Min(distributedDelta, this.GetDesiredWidth(column) - column.GetMinWidth(this.m_LayoutWidth));
					}
					bool flag3 = num > 0f;
					if (flag3)
					{
						this.ResizeColumn(column, this.GetDesiredWidth(column) - num, setDesiredWidthOnly);
					}
					distributedDelta -= num;
					bool flag4 = distributedDelta <= 0f;
					if (flag4)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002EE28 File Offset: 0x0002D028
		private float RecomputeToMaxWidth(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly = false)
		{
			bool flag = distributedDelta > 0f;
			if (flag)
			{
				for (int i = 0; i < columns.Count; i++)
				{
					Column column = columns[i];
					float num = 0f;
					bool flag2 = this.GetDesiredWidth(column) < column.GetMaxWidth(this.m_LayoutWidth);
					if (flag2)
					{
						num = Math.Min(distributedDelta, Math.Abs(column.GetMaxWidth(this.m_LayoutWidth) - this.GetDesiredWidth(column)));
					}
					bool flag3 = num > 0f;
					if (flag3)
					{
						this.ResizeColumn(column, this.GetDesiredWidth(column) + num, setDesiredWidthOnly);
					}
					distributedDelta -= num;
					bool flag4 = distributedDelta <= 0f;
					if (flag4)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002EEF0 File Offset: 0x0002D0F0
		public void ResizeToFit(float width)
		{
			float num = this.columnsWidth - Mathf.Clamp(width, this.minColumnsWidth, this.maxColumnsWidth);
			List<Column> list;
			using (CollectionPool<List<Column>, Column>.Get(out list))
			{
				List<Column> list2;
				using (CollectionPool<List<Column>, Column>.Get(out list2))
				{
					List<Column> list3;
					using (CollectionPool<List<Column>, Column>.Get(out list3))
					{
						list.AddRange(this.m_StretchableColumns);
						list2.AddRange(this.m_FixedColumns);
						list3.AddRange(this.m_RelativeWidthColumns);
						this.StretchResizeColumns(list, list2, list3, ref num, true, false);
						bool isDirty = this.m_IsDirty;
						if (isDirty)
						{
							this.UpdateCache();
						}
					}
				}
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002EFD4 File Offset: 0x0002D1D4
		private void ResizeColumn(Column column, float width, bool setDesiredWidthOnly = false)
		{
			Length length = new Length(width / this.layoutWidth * 100f, LengthUnit.Percent);
			bool dragResizeInPreviewMode = this.m_DragResizeInPreviewMode;
			if (dragResizeInPreviewMode)
			{
				this.m_PreviewDesiredWidths[column] = width;
			}
			else
			{
				bool flag = !setDesiredWidthOnly;
				if (flag)
				{
					column.width = ((column.width.unit == LengthUnit.Percent) ? length : width);
				}
				column.desiredWidth = width;
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002F04C File Offset: 0x0002D24C
		internal void BeginDragResize(Column column, float pos, bool previewMode)
		{
			bool isDirty = this.m_IsDirty;
			if (isDirty)
			{
				throw new Exception("Cannot begin resizing columns because the layout needs to be updated");
			}
			this.m_DragResizeInPreviewMode = previewMode;
			this.m_DragResizing = true;
			int visibleIndex = column.visibleIndex;
			this.m_DragStartPos = pos;
			this.m_DragLastPos = pos;
			this.m_DragInitialColumnWidth = column.desiredWidth;
			this.m_DragStretchableColumns.Clear();
			this.m_DragFixedColumns.Clear();
			this.m_DragRelativeColumns.Clear();
			bool dragResizeInPreviewMode = this.m_DragResizeInPreviewMode;
			if (dragResizeInPreviewMode)
			{
				bool flag = this.m_PreviewDesiredWidths == null;
				if (flag)
				{
					this.m_PreviewDesiredWidths = new Dictionary<Column, float>();
				}
				this.m_PreviewDesiredWidths[column] = column.desiredWidth;
			}
			for (int i = visibleIndex + 1; i < this.m_Columns.visibleList.Count<Column>(); i++)
			{
				Column column2 = this.m_Columns.visibleList.ElementAt<Column>(i);
				bool flag2 = !column2.visible;
				if (!flag2)
				{
					bool stretchable = column2.stretchable;
					if (stretchable)
					{
						this.m_DragStretchableColumns.Add(column2);
					}
					else
					{
						bool flag3 = column2.width.unit == LengthUnit.Percent;
						if (flag3)
						{
							this.m_DragRelativeColumns.Add(column2);
						}
						else
						{
							this.m_DragFixedColumns.Add(column2);
						}
					}
					bool dragResizeInPreviewMode2 = this.m_DragResizeInPreviewMode;
					if (dragResizeInPreviewMode2)
					{
						this.m_PreviewDesiredWidths[column2] = column2.desiredWidth;
					}
				}
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002F1CC File Offset: 0x0002D3CC
		public float GetDesiredPosition(Column column)
		{
			bool flag = !column.visible;
			float num;
			if (flag)
			{
				num = float.NaN;
			}
			else
			{
				float num2 = 0f;
				for (int i = 0; i < column.visibleIndex; i++)
				{
					Column column2 = this.m_Columns.visibleList.ElementAt<Column>(i);
					float desiredWidth = this.GetDesiredWidth(column2);
					bool flag2 = float.IsNaN(desiredWidth);
					if (!flag2)
					{
						num2 += desiredWidth;
					}
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002F244 File Offset: 0x0002D444
		public float GetDesiredWidth(Column c)
		{
			bool flag = this.m_DragResizeInPreviewMode && this.m_PreviewDesiredWidths.ContainsKey(c);
			float num;
			if (flag)
			{
				num = this.m_PreviewDesiredWidths[c];
			}
			else
			{
				num = c.desiredWidth;
			}
			return num;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0002F288 File Offset: 0x0002D488
		public void DragResize(Column column, float pos)
		{
			float minWidth = column.GetMinWidth(this.m_LayoutWidth);
			float maxWidth = column.GetMaxWidth(this.m_LayoutWidth);
			bool flag = this.m_Columns.stretchMode == Columns.StretchMode.GrowAndFill;
			if (flag)
			{
				float num = pos - this.m_DragLastPos;
				float num2 = Mathf.Clamp(this.GetDesiredWidth(column) + num, minWidth, maxWidth);
				num = num2 - this.GetDesiredWidth(column);
				bool flag2 = this.m_DragStretchableColumns.Count == 0 && num < 0f;
				if (flag2)
				{
					this.StretchResizeColumns(this.m_DragStretchableColumns, this.m_DragFixedColumns, this.m_DragRelativeColumns, ref num, false, true);
					num2 = Mathf.Clamp(this.GetDesiredWidth(column) + num2 - this.GetDesiredWidth(column), minWidth, maxWidth);
				}
				else
				{
					bool flag3 = num > 0f && this.columnsWidth + num < this.m_LayoutWidth;
					if (flag3)
					{
						float num3 = ((num < this.m_LayoutWidth - this.columnsWidth) ? 0f : (num - (this.m_LayoutWidth - this.columnsWidth)));
						this.StretchResizeColumns(this.m_DragStretchableColumns, this.m_DragFixedColumns, this.m_DragRelativeColumns, ref num3, false, true);
						num2 = Mathf.Clamp(this.GetDesiredWidth(column) + num - num3, minWidth, maxWidth);
					}
					else
					{
						this.StretchResizeColumns(this.m_DragStretchableColumns, this.m_DragFixedColumns, this.m_DragRelativeColumns, ref num, false, true);
						num2 = Mathf.Clamp(this.GetDesiredWidth(column) + num, minWidth, maxWidth);
					}
				}
				this.ResizeColumn(column, num2, false);
			}
			else
			{
				float num4 = pos - this.m_DragStartPos;
				float num5 = Math.Max(minWidth, Math.Min(maxWidth, this.m_DragInitialColumnWidth + num4));
				this.ResizeColumn(column, num5, false);
			}
			this.m_DragLastPos = pos;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0002F440 File Offset: 0x0002D640
		internal void EndDragResize(Column column, bool cancelled)
		{
			bool dragResizeInPreviewMode = this.m_DragResizeInPreviewMode;
			if (dragResizeInPreviewMode)
			{
				this.m_DragResizeInPreviewMode = false;
				bool flag = !cancelled;
				if (flag)
				{
					foreach (KeyValuePair<Column, float> keyValuePair in this.m_PreviewDesiredWidths)
					{
						this.ResizeColumn(keyValuePair.Key, keyValuePair.Value, keyValuePair.Key != column);
					}
				}
				this.m_PreviewDesiredWidths.Clear();
			}
			this.m_DragResizing = false;
			this.m_DragStretchableColumns.Clear();
			this.m_DragFixedColumns.Clear();
			this.m_DragRelativeColumns.Clear();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0002F508 File Offset: 0x0002D708
		private void UpdateCache()
		{
			this.ClearCache();
			foreach (Column column in this.m_Columns.visibleList)
			{
				bool stretchable = column.stretchable;
				if (stretchable)
				{
					this.m_StretchableColumns.Add(column);
				}
				else
				{
					bool flag = column.width.unit == LengthUnit.Pixel;
					if (flag)
					{
						this.m_FixedColumns.Add(column);
					}
				}
				bool flag2 = column.width.unit == LengthUnit.Percent;
				if (flag2)
				{
					this.m_RelativeWidthColumns.Add(column);
				}
				bool flag3 = column.width.unit == LengthUnit.Pixel && (column.minWidth.unit == LengthUnit.Percent || column.maxWidth.unit == LengthUnit.Percent);
				if (flag3)
				{
					this.m_MixedWidthColumns.Add(column);
				}
				this.m_MaxColumnsWidth += column.GetMaxWidth(this.m_LayoutWidth);
				this.m_MinColumnsWidth += column.GetMinWidth(this.m_LayoutWidth);
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0002F648 File Offset: 0x0002D848
		private void UpdateMinAndMaxColumnsWidth()
		{
			this.m_MaxColumnsWidth = 0f;
			this.m_MinColumnsWidth = 0f;
			foreach (Column column in this.m_Columns.visibleList)
			{
				this.m_MaxColumnsWidth += column.GetMaxWidth(this.m_LayoutWidth);
				this.m_MinColumnsWidth += column.GetMinWidth(this.m_LayoutWidth);
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0002F6E0 File Offset: 0x0002D8E0
		private void ClearCache()
		{
			this.m_StretchableColumns.Clear();
			this.m_RelativeWidthColumns.Clear();
			this.m_FixedColumns.Clear();
			this.m_MaxColumnsWidth = 0f;
			this.m_MinColumnsWidth = 0f;
			this.m_ColumnsWidthDirty = true;
		}

		// Token: 0x04000628 RID: 1576
		private List<Column> m_StretchableColumns = new List<Column>();

		// Token: 0x04000629 RID: 1577
		private List<Column> m_FixedColumns = new List<Column>();

		// Token: 0x0400062A RID: 1578
		private List<Column> m_RelativeWidthColumns = new List<Column>();

		// Token: 0x0400062B RID: 1579
		private List<Column> m_MixedWidthColumns = new List<Column>();

		// Token: 0x0400062C RID: 1580
		private Columns m_Columns;

		// Token: 0x0400062D RID: 1581
		private float m_ColumnsWidth = 0f;

		// Token: 0x0400062E RID: 1582
		private bool m_ColumnsWidthDirty = true;

		// Token: 0x0400062F RID: 1583
		private float m_MaxColumnsWidth = 0f;

		// Token: 0x04000630 RID: 1584
		private float m_MinColumnsWidth = 0f;

		// Token: 0x04000631 RID: 1585
		private bool m_IsDirty = false;

		// Token: 0x04000632 RID: 1586
		private float m_PreviousWidth = float.NaN;

		// Token: 0x04000633 RID: 1587
		private float m_LayoutWidth = float.NaN;

		// Token: 0x04000634 RID: 1588
		private bool m_DragResizeInPreviewMode;

		// Token: 0x04000635 RID: 1589
		private bool m_DragResizing = false;

		// Token: 0x04000636 RID: 1590
		private float m_DragStartPos;

		// Token: 0x04000637 RID: 1591
		private float m_DragLastPos;

		// Token: 0x04000638 RID: 1592
		private float m_DragInitialColumnWidth;

		// Token: 0x04000639 RID: 1593
		private List<Column> m_DragStretchableColumns = new List<Column>();

		// Token: 0x0400063A RID: 1594
		private List<Column> m_DragRelativeColumns = new List<Column>();

		// Token: 0x0400063B RID: 1595
		private List<Column> m_DragFixedColumns = new List<Column>();

		// Token: 0x0400063C RID: 1596
		private Dictionary<Column, float> m_PreviewDesiredWidths;
	}
}
