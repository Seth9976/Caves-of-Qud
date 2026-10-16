using System;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x0200044A RID: 1098
	public abstract class OptionsDataRow<T> : OptionsDataRow where T : IEquatable<T>
	{
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x000DA42D File Offset: 0x000D862D
		// (set) Token: 0x060029AC RID: 10668 RVA: 0x000DA438 File Offset: 0x000D8638
		public T Value
		{
			get
			{
				return this._Value;
			}
			set
			{
				if (!this._Value.Equals(value))
				{
					this._Value = value;
					base.OnChange();
					if (this.Option.Restart)
					{
						if (value.Equals(this.Initial))
						{
							OptionsUI.RestartOptions.Remove(this.Option);
							return;
						}
						OptionsUI.RestartOptions.Add(this.Option);
					}
				}
			}
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x000DA4AC File Offset: 0x000D86AC
		public OptionsDataRow(GameOption Option)
		{
			this.Id = Option.ID;
			this.Option = Option;
			this.Title = Option.DisplayText;
			this.CategoryId = Option.Category;
			this.SearchWords = string.Concat(new string[] { Option.SearchKeywords, " ", Option.DisplayText, " ", Option.Category });
			this.IsEnabled = delegate
			{
				GameOption.RequiresSpec requires = Option.Requires;
				return (requires == null || requires.RequirementsMet) && Option.RequiresCapabilityMet();
			};
			this.HelpText = Option.HelpText;
		}

		// Token: 0x060029AE RID: 10670 RVA: 0x000DA579 File Offset: 0x000D8779
		protected void Initialize(T Value)
		{
			this._Value = Value;
			this.Initial = Value;
		}

		// Token: 0x04001597 RID: 5527
		public readonly GameOption Option;

		// Token: 0x04001598 RID: 5528
		protected T _Value;

		// Token: 0x04001599 RID: 5529
		protected T Initial;
	}
}
