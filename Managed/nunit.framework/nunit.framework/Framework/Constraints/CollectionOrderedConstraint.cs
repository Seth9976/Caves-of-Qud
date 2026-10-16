using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NUnit.Framework.Constraints
{
	// Token: 0x020000F2 RID: 242
	public class CollectionOrderedConstraint : CollectionConstraint
	{
		// Token: 0x06000770 RID: 1904 RVA: 0x000120B8 File Offset: 0x000102B8
		public CollectionOrderedConstraint()
		{
			this._steps = new List<CollectionOrderedConstraint.OrderingStep>();
			this.CreateNextStep(null);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x000120D2 File Offset: 0x000102D2
		public override string DisplayName
		{
			get
			{
				return "Ordered";
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x000120D9 File Offset: 0x000102D9
		public CollectionOrderedConstraint Ascending
		{
			get
			{
				if (this._activeStep.Direction != CollectionOrderedConstraint.OrderDirection.Unspecified)
				{
					throw new InvalidOperationException("Only one directional modifier may be used");
				}
				this._activeStep.Direction = CollectionOrderedConstraint.OrderDirection.Ascending;
				return this;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x00012100 File Offset: 0x00010300
		public CollectionOrderedConstraint Descending
		{
			get
			{
				if (this._activeStep.Direction != CollectionOrderedConstraint.OrderDirection.Unspecified)
				{
					throw new InvalidOperationException("Only one directional modifier may be used");
				}
				this._activeStep.Direction = CollectionOrderedConstraint.OrderDirection.Descending;
				return this;
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00012128 File Offset: 0x00010328
		public CollectionOrderedConstraint Using(IComparer comparer)
		{
			if (this._activeStep.ComparerName != null)
			{
				throw new InvalidOperationException("Only one Using modifier may be used");
			}
			this._activeStep.Comparer = ComparisonAdapter.For(comparer);
			this._activeStep.ComparerName = comparer.GetType().FullName;
			return this;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x00012178 File Offset: 0x00010378
		public CollectionOrderedConstraint Using<T>(IComparer<T> comparer)
		{
			if (this._activeStep.ComparerName != null)
			{
				throw new InvalidOperationException("Only one Using modifier may be used");
			}
			this._activeStep.Comparer = ComparisonAdapter.For<T>(comparer);
			this._activeStep.ComparerName = comparer.GetType().FullName;
			return this;
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x000121C8 File Offset: 0x000103C8
		public CollectionOrderedConstraint Using<T>(Comparison<T> comparer)
		{
			if (this._activeStep.ComparerName != null)
			{
				throw new InvalidOperationException("Only one Using modifier may be used");
			}
			this._activeStep.Comparer = ComparisonAdapter.For<T>(comparer);
			this._activeStep.ComparerName = comparer.GetType().FullName;
			return this;
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x00012215 File Offset: 0x00010415
		public CollectionOrderedConstraint By(string propertyName)
		{
			if (this._activeStep.PropertyName == null)
			{
				this._activeStep.PropertyName = propertyName;
			}
			else
			{
				this.CreateNextStep(propertyName);
			}
			return this;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001223A File Offset: 0x0001043A
		public CollectionOrderedConstraint Then
		{
			get
			{
				this.CreateNextStep(null);
				return this;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00012244 File Offset: 0x00010444
		public override string Description
		{
			get
			{
				string text = "collection ordered";
				int num = 0;
				foreach (CollectionOrderedConstraint.OrderingStep orderingStep in this._steps)
				{
					if (num++ != 0)
					{
						text += " then";
					}
					if (orderingStep.PropertyName != null)
					{
						text = text + " by " + MsgUtils.FormatValue(orderingStep.PropertyName);
					}
					if (orderingStep.Direction == CollectionOrderedConstraint.OrderDirection.Descending)
					{
						text += ", descending";
					}
				}
				return text;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000122E0 File Offset: 0x000104E0
		protected override bool Matches(IEnumerable actual)
		{
			object obj = null;
			int num = 0;
			foreach (object obj2 in actual)
			{
				if (obj2 == null)
				{
					throw new ArgumentNullException("actual", "Null value at index " + num.ToString());
				}
				if (obj != null)
				{
					if (this._steps[0].PropertyName != null)
					{
						using (List<CollectionOrderedConstraint.OrderingStep>.Enumerator enumerator2 = this._steps.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								CollectionOrderedConstraint.OrderingStep orderingStep = enumerator2.Current;
								string propertyName = orderingStep.PropertyName;
								PropertyInfo property = obj.GetType().GetProperty(propertyName);
								PropertyInfo property2 = obj2.GetType().GetProperty(propertyName);
								object value = property.GetValue(obj, null);
								object value2 = property2.GetValue(obj2, null);
								if (value2 == null)
								{
									throw new ArgumentException("actual", "Null property value at index " + num.ToString());
								}
								int num2 = orderingStep.Comparer.Compare(value, value2);
								if (num2 < 0)
								{
									if (orderingStep.Direction == CollectionOrderedConstraint.OrderDirection.Descending)
									{
										return false;
									}
									break;
								}
								else if (num2 > 0)
								{
									if (orderingStep.Direction != CollectionOrderedConstraint.OrderDirection.Descending)
									{
										return false;
									}
									break;
								}
							}
							goto IL_0166;
						}
					}
					int num3 = this._activeStep.Comparer.Compare(obj, obj2);
					if (this._activeStep.Direction == CollectionOrderedConstraint.OrderDirection.Descending && num3 < 0)
					{
						return false;
					}
					if (this._activeStep.Direction != CollectionOrderedConstraint.OrderDirection.Descending && num3 > 0)
					{
						return false;
					}
				}
				IL_0166:
				obj = obj2;
				num++;
			}
			return true;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000124B4 File Offset: 0x000106B4
		protected override string GetStringRepresentation()
		{
			StringBuilder stringBuilder = new StringBuilder("<ordered");
			if (this._steps.Count > 0)
			{
				CollectionOrderedConstraint.OrderingStep orderingStep = this._steps[0];
				if (orderingStep.PropertyName != null)
				{
					stringBuilder.Append("by " + orderingStep.PropertyName);
				}
				if (orderingStep.Direction == CollectionOrderedConstraint.OrderDirection.Descending)
				{
					stringBuilder.Append(" descending");
				}
				if (orderingStep.ComparerName != null)
				{
					stringBuilder.Append(" " + orderingStep.ComparerName);
				}
			}
			stringBuilder.Append(">");
			return stringBuilder.ToString();
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001254C File Offset: 0x0001074C
		private void CreateNextStep(string propertyName)
		{
			this._activeStep = new CollectionOrderedConstraint.OrderingStep(propertyName);
			this._steps.Add(this._activeStep);
		}

		// Token: 0x0400019E RID: 414
		private List<CollectionOrderedConstraint.OrderingStep> _steps;

		// Token: 0x0400019F RID: 415
		private CollectionOrderedConstraint.OrderingStep _activeStep;

		// Token: 0x02000197 RID: 407
		private enum OrderDirection
		{
			// Token: 0x040002A2 RID: 674
			Unspecified,
			// Token: 0x040002A3 RID: 675
			Ascending,
			// Token: 0x040002A4 RID: 676
			Descending
		}

		// Token: 0x02000198 RID: 408
		private class OrderingStep
		{
			// Token: 0x06000AF7 RID: 2807 RVA: 0x0001ADAD File Offset: 0x00018FAD
			public OrderingStep(string propertyName)
			{
				this.PropertyName = propertyName;
				this.Comparer = ComparisonAdapter.Default;
			}

			// Token: 0x17000274 RID: 628
			// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0001ADC7 File Offset: 0x00018FC7
			// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x0001ADCF File Offset: 0x00018FCF
			public string PropertyName { get; set; }

			// Token: 0x17000275 RID: 629
			// (get) Token: 0x06000AFA RID: 2810 RVA: 0x0001ADD8 File Offset: 0x00018FD8
			// (set) Token: 0x06000AFB RID: 2811 RVA: 0x0001ADE0 File Offset: 0x00018FE0
			public CollectionOrderedConstraint.OrderDirection Direction { get; set; }

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0001ADE9 File Offset: 0x00018FE9
			// (set) Token: 0x06000AFD RID: 2813 RVA: 0x0001ADF1 File Offset: 0x00018FF1
			public ComparisonAdapter Comparer { get; set; }

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000AFE RID: 2814 RVA: 0x0001ADFA File Offset: 0x00018FFA
			// (set) Token: 0x06000AFF RID: 2815 RVA: 0x0001AE02 File Offset: 0x00019002
			public string ComparerName { get; set; }
		}
	}
}
