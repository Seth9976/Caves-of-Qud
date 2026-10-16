using System;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud.UI
{
	/// <summary>
	///   Data Structure for various UI elements to use.
	/// </summary>
	// Token: 0x020005B2 RID: 1458
	[Serializable]
	public class AttributeDataElement : FrameworkDataElement
	{
		// Token: 0x06003E1D RID: 15901 RVA: 0x0013115A File Offset: 0x0012F35A
		public void Updated()
		{
			this.window.AttributeUpdated(this);
		}

		// Token: 0x06003E1E RID: 15902 RVA: 0x00131168 File Offset: 0x0012F368
		public void AddBonus(int bonus, string source)
		{
			this.Bonus += bonus;
			this.BonusSource += string.Format("{0:+#;-#;0} from {1}\n", bonus, source);
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06003E1F RID: 15903 RVA: 0x0013119A File Offset: 0x0012F39A
		public int Value
		{
			get
			{
				return this.BaseValue + this.Bonus + this.Purchased;
			}
		}

		// Token: 0x06003E20 RID: 15904 RVA: 0x001311B0 File Offset: 0x0012F3B0
		public void raise()
		{
			if (this.window.apRemaining >= this.APToRaise && this.Value - this.Bonus < this.Maximum)
			{
				this.Purchased++;
				this.Updated();
			}
		}

		// Token: 0x06003E21 RID: 15905 RVA: 0x001311EE File Offset: 0x0012F3EE
		public void lower()
		{
			if (this.Purchased > 0 && this.Value - this.Bonus > this.Minimum)
			{
				this.Purchased--;
				this.Updated();
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06003E22 RID: 15906 RVA: 0x00131222 File Offset: 0x0012F422
		public int APRemaining
		{
			get
			{
				return this.window.apRemaining;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06003E23 RID: 15907 RVA: 0x00131230 File Offset: 0x0012F430
		public int APSpent
		{
			get
			{
				int num = Math.Min(this.Purchased, Math.Max(0, this.BaseValue + this.Purchased - 18));
				return 2 * num + Math.Max(0, this.Purchased - num);
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06003E24 RID: 15908 RVA: 0x00131271 File Offset: 0x0012F471
		public int APToRaise
		{
			get
			{
				if (this.Value - this.Bonus >= 18)
				{
					return 2;
				}
				return 1;
			}
		}

		// Token: 0x04001EC1 RID: 7873
		public AttributeSelectionControl control;

		// Token: 0x04001EC2 RID: 7874
		public QudAttributesModuleWindow window;

		// Token: 0x04001EC3 RID: 7875
		public string Attribute;

		// Token: 0x04001EC4 RID: 7876
		public string ShortAttributeName;

		// Token: 0x04001EC5 RID: 7877
		public int BaseValue;

		// Token: 0x04001EC6 RID: 7878
		public int Minimum;

		// Token: 0x04001EC7 RID: 7879
		public int Maximum;

		// Token: 0x04001EC8 RID: 7880
		public int Bonus;

		// Token: 0x04001EC9 RID: 7881
		public string BonusSource = "";

		// Token: 0x04001ECA RID: 7882
		public int Purchased;
	}
}
