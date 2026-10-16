using System;
using System.Text;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020011C2 RID: 4546
	[Serializable]
	public class EnergyCellChargeLevel : IPart
	{
		// Token: 0x0600B4A7 RID: 46247 RVA: 0x0041DFEA File Offset: 0x0041C1EA
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B4A8 RID: 46248 RVA: 0x0041DFF0 File Offset: 0x0041C1F0
		public void ApplyEffect(int Level)
		{
			string text;
			switch (Level)
			{
			case 0:
				text = this.Drained;
				break;
			case 1:
				text = this.VeryLow;
				break;
			case 2:
				text = this.Low;
				break;
			case 3:
				text = this.Used;
				break;
			case 4:
				text = this.Fresh;
				break;
			case 5:
				text = this.Full;
				break;
			default:
				text = null;
				break;
			}
			string text2 = text;
			if (text2.IsNullOrEmpty())
			{
				if (this.Applied)
				{
					this.ParentObject.RemoveEffect(typeof(EnergyCellChargeLevelEffect));
					this.Applied = false;
				}
				return;
			}
			EnergyCellChargeLevelEffect energyCellChargeLevelEffect = (this.Applied ? this.ParentObject.GetEffect<EnergyCellChargeLevelEffect>() : new EnergyCellChargeLevelEffect());
			if (energyCellChargeLevelEffect.Level == Level)
			{
				return;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder("{{").Append(EnergyStorage.GetChargeLevelColor(Level)).Append('|');
			energyCellChargeLevelEffect.Level = Level;
			ReadOnlySpan<char> readOnlySpan;
			ReadOnlySpan<char> readOnlySpan2;
			text2.AsDelimitedSpans(':', out readOnlySpan, out readOnlySpan2);
			stringBuilder.Append(readOnlySpan).Append("}}");
			energyCellChargeLevelEffect.DisplayName = Event.FinalizeString(stringBuilder);
			energyCellChargeLevelEffect.Details = ((readOnlySpan2.Length == 0) ? "" : new string(readOnlySpan2));
			if (!this.Applied)
			{
				this.ParentObject.ApplyEffect(energyCellChargeLevelEffect, null);
				this.Applied = true;
			}
		}

		// Token: 0x0600B4A9 RID: 46249 RVA: 0x0041E13C File Offset: 0x0041C33C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Socket != null || this.ParentObject.TryGetPart<EnergyCellSocket>(out this.Socket))
			{
				int num = -1;
				EnergyCell energyCell;
				if (GameObject.Validate(this.Socket.Cell) && this.Socket.Cell.TryGetPartDescendedFrom<EnergyCell>(out energyCell) && energyCell.Charge >= this.ChargeMinimum)
				{
					num = energyCell.GetChargeLevel();
				}
				this.ApplyEffect(num);
				return;
			}
			this.ParentObject.RemovePart(this);
			this.ParentObject.RemoveEffect(typeof(EnergyCellChargeLevelEffect));
		}

		// Token: 0x040048CB RID: 18635
		public string Full;

		// Token: 0x040048CC RID: 18636
		public string Fresh;

		// Token: 0x040048CD RID: 18637
		public string Used;

		// Token: 0x040048CE RID: 18638
		public string Low;

		// Token: 0x040048CF RID: 18639
		public string VeryLow;

		// Token: 0x040048D0 RID: 18640
		public string Drained;

		// Token: 0x040048D1 RID: 18641
		public int ChargeMinimum;

		// Token: 0x040048D2 RID: 18642
		public bool Applied;

		// Token: 0x040048D3 RID: 18643
		[NonSerialized]
		private EnergyCellSocket Socket;
	}
}
