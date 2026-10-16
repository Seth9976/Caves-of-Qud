using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace XRL.UI.Framework
{
	// Token: 0x0200151B RID: 5403
	public class AbstractScrollContext : NavigationContext
	{
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600D257 RID: 53847 RVA: 0x004E13F0 File Offset: 0x004DF5F0
		// (set) Token: 0x0600D258 RID: 53848 RVA: 0x004E13F8 File Offset: 0x004DF5F8
		public virtual int selectedPosition { get; set; }

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600D259 RID: 53849 RVA: 0x004E1401 File Offset: 0x004DF601
		public virtual int selectedRow
		{
			get
			{
				if (!this.DualAxis)
				{
					return this.selectedPosition;
				}
				return this.selectedPosition / this.rowWidth;
			}
		}

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x0600D25A RID: 53850 RVA: 0x004E141F File Offset: 0x004DF61F
		public virtual int selectedColumn
		{
			get
			{
				if (!this.DualAxis)
				{
					return this.selectedPosition;
				}
				return this.selectedPosition % this.rowWidth;
			}
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x0600D25B RID: 53851 RVA: 0x004E143D File Offset: 0x004DF63D
		public virtual int length { get; } = -1;

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x0600D25C RID: 53852 RVA: 0x004E1445 File Offset: 0x004DF645
		public virtual int rowWidth { get; } = -1;

		// Token: 0x0600D25D RID: 53853 RVA: 0x004E144D File Offset: 0x004DF64D
		public virtual NavigationContext GetContextAt(int index)
		{
			return null;
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x0600D25E RID: 53854 RVA: 0x004E1450 File Offset: 0x004DF650
		// (set) Token: 0x0600D25F RID: 53855 RVA: 0x004E149D File Offset: 0x004DF69D
		public override bool disabled
		{
			get
			{
				if (!base.disabled && this.length != 0)
				{
					NavigationContext contextAt = this.GetContextAt(this.selectedPosition);
					return contextAt != null && contextAt.disabled && this.NextEnabledContext(1) == null;
				}
				return true;
			}
			set
			{
				base.disabled = value;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x0600D260 RID: 53856 RVA: 0x004E14A8 File Offset: 0x004DF6A8
		public InputAxisTypes SecondaryAxisType
		{
			get
			{
				if (this.AxisType == InputAxisTypes.NavigationXAxis)
				{
					return InputAxisTypes.NavigationYAxis;
				}
				if (this.AxisType == InputAxisTypes.NavigationYAxis)
				{
					return InputAxisTypes.NavigationXAxis;
				}
				if (this.AxisType == InputAxisTypes.NavigationPageXAxis)
				{
					return InputAxisTypes.NavigationPageYAxis;
				}
				if (this.AxisType == InputAxisTypes.NavigationPageYAxis)
				{
					return InputAxisTypes.NavigationPageXAxis;
				}
				if (this.AxisType == InputAxisTypes.NavigationUAxis)
				{
					return InputAxisTypes.NavigationVAxis;
				}
				if (this.AxisType == InputAxisTypes.NavigationVAxis)
				{
					return InputAxisTypes.NavigationUAxis;
				}
				throw new ArgumentOutOfRangeException("Unknown Axis Type to oppose");
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x0600D261 RID: 53857 RVA: 0x004E1500 File Offset: 0x004DF700
		public override bool hasChildren
		{
			get
			{
				return this.length > 0 || base.hasChildren;
			}
		}

		// Token: 0x0600D262 RID: 53858 RVA: 0x004E1514 File Offset: 0x004DF714
		public override void Setup()
		{
			for (int i = 0; i < this.length; i++)
			{
				NavigationContext contextAt = this.GetContextAt(i);
				if (contextAt != null)
				{
					contextAt.parentContext = this;
					contextAt.Setup();
				}
			}
			base.Setup();
		}

		// Token: 0x0600D263 RID: 53859 RVA: 0x004E1550 File Offset: 0x004DF750
		public override void OnEnter()
		{
			NavigationContext navigationContext = (NavigationContext)base.currentEvent.data["to"];
			Event triggeringEvent = NavigationController.instance.triggeringEvent;
			bool flag = false;
			try
			{
				InputAxisTypes? inputAxisTypes = ((triggeringEvent != null) ? triggeringEvent.data["axis"] : null) as InputAxisTypes?;
				InputAxisTypes axisType = this.AxisType;
				flag = (inputAxisTypes.GetValueOrDefault() == axisType) & (inputAxisTypes != null);
			}
			catch (Exception)
			{
			}
			if (navigationContext == this)
			{
				NavigationContext navigationContext2 = (NavigationContext)base.currentEvent.data["from"];
				IEnumerable<NavigationContext> enumerable = ((navigationContext2 != null) ? navigationContext2.parents.Prepend(navigationContext2).TakeWhile<NavigationContext>((NavigationContext c) => !this.parents.Prepend(this).Contains(c)) : null);
				object obj;
				if (enumerable == null)
				{
					obj = null;
				}
				else
				{
					obj = enumerable.FirstOrDefault<NavigationContext>((NavigationContext c) => c is AbstractScrollContext);
				}
				AbstractScrollContext abstractScrollContext = obj as AbstractScrollContext;
				if (triggeringEvent != null && triggeringEvent.axisValue != null && this.rowWidth > 0 && abstractScrollContext != null && abstractScrollContext.AxisType == this.AxisType)
				{
					if (flag)
					{
						if (triggeringEvent != null)
						{
							int? num = triggeringEvent.axisValue;
							int num2 = 0;
							if ((num.GetValueOrDefault() > num2) & (num != null))
							{
								int num3 = (this.length - 1) / this.rowWidth;
								this.SelectIndex(Math.Min(this.length - 1, this.rowWidth * Math.Min(num3, abstractScrollContext.selectedRow)));
								goto IL_0326;
							}
						}
						if (triggeringEvent != null)
						{
							int? num = triggeringEvent.axisValue;
							int num2 = 0;
							if ((num.GetValueOrDefault() < num2) & (num != null))
							{
								int num4 = Math.Min((this.length - 1) / this.rowWidth, abstractScrollContext.selectedRow);
								this.SelectIndex(Math.Min(this.length - 1, this.rowWidth * (num4 + 1) - 1));
							}
						}
					}
					else
					{
						if (triggeringEvent != null)
						{
							int? num = triggeringEvent.axisValue;
							int num2 = 0;
							if ((num.GetValueOrDefault() > num2) & (num != null))
							{
								int selectedColumn = abstractScrollContext.selectedColumn;
								this.SelectIndex(Math.Min(this.length - 1, selectedColumn));
								goto IL_0326;
							}
						}
						if (triggeringEvent != null)
						{
							int? num = triggeringEvent.axisValue;
							int num2 = 0;
							if ((num.GetValueOrDefault() < num2) & (num != null))
							{
								int num5 = abstractScrollContext.selectedColumn;
								while (num5 + this.rowWidth < this.length)
								{
									num5 += this.rowWidth;
								}
								this.SelectIndex(Math.Min(this.length - 1, num5));
							}
						}
					}
				}
				else
				{
					if (flag && triggeringEvent != null)
					{
						int? num = triggeringEvent.axisValue;
						int num2 = 1;
						if ((num.GetValueOrDefault() == num2) & (num != null))
						{
							this.SelectIndex(0);
							goto IL_0326;
						}
					}
					if (flag && triggeringEvent != null)
					{
						int? num = triggeringEvent.axisValue;
						int num2 = -1;
						if ((num.GetValueOrDefault() == num2) & (num != null))
						{
							this.SelectIndex(this.length - 1);
							goto IL_0326;
						}
					}
					this.SelectIndex(Math.Min(Math.Max(0, this.selectedPosition), this.length - 1));
				}
				IL_0326:
				base.currentEvent.Cancel();
			}
			else
			{
				int? num = this.ContextIndex(navigationContext);
				if (num != null)
				{
					int valueOrDefault = num.GetValueOrDefault();
					this.selectedPosition = valueOrDefault;
					this.UpdateGridSiblings();
				}
			}
			base.OnEnter();
		}

		// Token: 0x0600D264 RID: 53860 RVA: 0x004E18D0 File Offset: 0x004DFAD0
		public virtual int? ContextIndex(NavigationContext to)
		{
			for (int i = 0; i < this.length; i++)
			{
				if (to.IsInside(this.GetContextAt(i)))
				{
					return new int?(i);
				}
			}
			return null;
		}

		// Token: 0x0600D265 RID: 53861 RVA: 0x004E1910 File Offset: 0x004DFB10
		public void UpdateGridSiblings()
		{
			if (this.gridSiblings == null)
			{
				return;
			}
			foreach (AbstractScrollContext abstractScrollContext in this.gridSiblings())
			{
				abstractScrollContext.selectedPosition = this.selectedPosition;
			}
		}

		// Token: 0x0600D266 RID: 53862 RVA: 0x004E1970 File Offset: 0x004DFB70
		public virtual void SelectIndex(int index)
		{
			this.selectedPosition = index;
			NavigationContext contextAt = this.GetContextAt(this.selectedPosition);
			AbstractScrollContext abstractScrollContext = contextAt as AbstractScrollContext;
			if (abstractScrollContext != null && abstractScrollContext.AxisType == this.AxisType)
			{
				int? num = base.currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					abstractScrollContext.selectedPosition = 0;
				}
				else
				{
					num = base.currentEvent.axisValue;
					num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						abstractScrollContext.selectedPosition = abstractScrollContext.length - 1;
					}
				}
			}
			if (contextAt == null)
			{
				return;
			}
			contextAt.Activate(false);
		}

		// Token: 0x0600D267 RID: 53863 RVA: 0x004E1A10 File Offset: 0x004DFC10
		public virtual int? NextEnabledContext(int direction)
		{
			AbstractScrollContext.<>c__DisplayClass31_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.position = this.selectedPosition + direction;
			if (this.rowWidth <= 0)
			{
				while (CS$<>8__locals1.position != this.selectedPosition)
				{
					if (CS$<>8__locals1.position >= this.length)
					{
						if (!this.wraps)
						{
							return null;
						}
						CS$<>8__locals1.position -= this.length;
					}
					if (CS$<>8__locals1.position < 0)
					{
						if (!this.wraps)
						{
							return null;
						}
						CS$<>8__locals1.position += this.length;
					}
					if (!this.GetContextAt(CS$<>8__locals1.position).disabled)
					{
						return new int?(CS$<>8__locals1.position);
					}
					if (CS$<>8__locals1.position == this.selectedPosition)
					{
						break;
					}
					CS$<>8__locals1.position += direction;
				}
				return new int?(CS$<>8__locals1.position);
			}
			if (this.wraps)
			{
				throw new Exception("Haven't implemented wrapping for grids... sorry...");
			}
			AbstractScrollContext.<>c__DisplayClass31_1 CS$<>8__locals2;
			CS$<>8__locals2.secondaryAxis = Math.Abs(direction) == this.rowWidth;
			while (CS$<>8__locals1.position != this.selectedPosition)
			{
				if (CS$<>8__locals1.position >= this.length || CS$<>8__locals1.position < 0 || !this.<NextEnabledContext>g__otherAxisSame|31_0(ref CS$<>8__locals1, ref CS$<>8__locals2))
				{
					return null;
				}
				if (!this.GetContextAt(CS$<>8__locals1.position).disabled)
				{
					return new int?(CS$<>8__locals1.position);
				}
				CS$<>8__locals1.position += direction;
			}
			return new int?(CS$<>8__locals1.position);
		}

		// Token: 0x0600D268 RID: 53864 RVA: 0x004E1BA0 File Offset: 0x004DFDA0
		public virtual void Next(int magnitude)
		{
			int? num = this.NextEnabledContext(magnitude);
			if (num != null)
			{
				base.currentEvent.Handle();
				this.SelectIndex(num.Value);
			}
		}

		// Token: 0x0600D269 RID: 53865 RVA: 0x004E1BD8 File Offset: 0x004DFDD8
		public virtual void Prev(int magnitude)
		{
			int? num = this.NextEnabledContext(-magnitude);
			if (num != null)
			{
				base.currentEvent.Handle();
				this.SelectIndex(num.Value);
			}
		}

		// Token: 0x0600D26A RID: 53866 RVA: 0x004E1C10 File Offset: 0x004DFE10
		public virtual void HandleAxisEvent()
		{
			if (base.currentEvent.axisValue != null)
			{
				int? num = base.currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					this.Prev(1);
					return;
				}
				num = base.currentEvent.axisValue;
				num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					this.Next(1);
				}
			}
		}

		// Token: 0x0600D26B RID: 53867 RVA: 0x004E1C84 File Offset: 0x004DFE84
		public virtual void HandleSecondaryAxisEvent()
		{
			if (base.currentEvent.axisValue != null)
			{
				int? num = base.currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					this.Prev(this.rowWidth);
					return;
				}
				num = base.currentEvent.axisValue;
				num2 = 0;
				if ((num.GetValueOrDefault() > num2) & (num != null))
				{
					this.Next(this.rowWidth);
				}
			}
		}

		// Token: 0x0600D26C RID: 53868 RVA: 0x004E1D00 File Offset: 0x004DFF00
		public virtual void SetAxis(InputAxisTypes axis, bool dual = false)
		{
			this.AxisType = axis;
			this.DualAxis = dual;
			this.axisHandlers = new Dictionary<InputAxisTypes, Action> { 
			{
				axis,
				new Action(this.HandleAxisEvent)
			} };
			if (dual)
			{
				this.axisHandlers.Add(this.SecondaryAxisType, new Action(this.HandleSecondaryAxisEvent));
			}
		}

		// Token: 0x0600D26F RID: 53871 RVA: 0x004E1D8F File Offset: 0x004DFF8F
		[CompilerGenerated]
		private bool <NextEnabledContext>g__otherAxisSame|31_0(ref AbstractScrollContext.<>c__DisplayClass31_0 A_1, ref AbstractScrollContext.<>c__DisplayClass31_1 A_2)
		{
			if (!A_2.secondaryAxis)
			{
				return A_1.position / this.rowWidth == this.selectedRow;
			}
			return A_1.position % this.rowWidth == this.selectedColumn;
		}

		// Token: 0x04005367 RID: 21351
		public InputAxisTypes AxisType;

		// Token: 0x04005368 RID: 21352
		public bool DualAxis;

		// Token: 0x04005369 RID: 21353
		public Func<IEnumerable<AbstractScrollContext>> gridSiblings;

		// Token: 0x0400536A RID: 21354
		public bool wraps = true;
	}
}
