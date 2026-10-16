using System;
using System.Collections;
using System.Collections.Generic;

namespace ex2D.Detail
{
	// Token: 0x02000354 RID: 852
	public struct DiceEnumerator : IEnumerator<exTextureInfo.Dice>, IEnumerator, IDisposable, IEnumerable<exTextureInfo.Dice>, IEnumerable
	{
		// Token: 0x06001FA0 RID: 8096 RVA: 0x00099CE4 File Offset: 0x00097EE4
		public DiceEnumerator(List<int> _diceData, exTextureInfo _textureInfo)
		{
			this.diceData = _diceData;
			this.diceUnitWidth = _diceData[0];
			this.diceUnitHeight = _diceData[1];
			this.dataIndex = -1;
			this.Reset();
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00099D14 File Offset: 0x00097F14
		public IEnumerator<exTextureInfo.Dice> GetEnumerator()
		{
			return this;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00099D21 File Offset: 0x00097F21
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00099D30 File Offset: 0x00097F30
		public static void AddDiceData(exTextureInfo _textureInfo, List<int> _diceData, exTextureInfo.Dice _dice)
		{
			_diceData.Add(_dice.offset_x);
			_diceData.Add(_dice.offset_y);
			_diceData.Add(_dice.rotated ? (-_dice.width) : _dice.width);
			_diceData.Add(_dice.height);
			_diceData.Add(_dice.x);
			_diceData.Add(_dice.y);
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x00099D98 File Offset: 0x00097F98
		public exTextureInfo.Dice Current
		{
			get
			{
				exTextureInfo.Dice dice = default(exTextureInfo.Dice);
				if (this.diceData[this.dataIndex] == -1)
				{
					dice.sizeType = exTextureInfo.DiceType.Empty;
					return dice;
				}
				if (this.diceData[this.dataIndex] >= 0)
				{
					dice.offset_x = this.diceData[this.dataIndex];
					dice.offset_y = this.diceData[this.dataIndex + 1];
					dice.width = this.diceData[this.dataIndex + 2];
					dice.height = this.diceData[this.dataIndex + 3];
					dice.x = this.diceData[this.dataIndex + 4];
					dice.y = this.diceData[this.dataIndex + 5];
					if (dice.width < 0)
					{
						dice.rotated = true;
						dice.width = -dice.width;
					}
					if (dice.width == this.diceUnitWidth && dice.height == this.diceUnitHeight)
					{
						dice.sizeType = exTextureInfo.DiceType.Max;
					}
					else if (dice.width == 0 || dice.height == 0)
					{
						dice.sizeType = exTextureInfo.DiceType.Empty;
					}
					else
					{
						dice.sizeType = exTextureInfo.DiceType.Trimmed;
					}
				}
				else
				{
					dice.sizeType = exTextureInfo.DiceType.Max;
					dice.x = this.diceData[this.dataIndex + 1];
					dice.y = this.diceData[this.dataIndex + 2];
					dice.width = this.diceUnitWidth;
					dice.height = this.diceUnitHeight;
					dice.rotated = this.diceData[this.dataIndex] == -3;
				}
				return dice;
			}
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00099F5C File Offset: 0x0009815C
		public bool MoveNext()
		{
			if (this.dataIndex == -1)
			{
				this.dataIndex = 2;
				if (this.diceData == null)
				{
					return false;
				}
			}
			else if (this.diceData[this.dataIndex] >= 0)
			{
				this.dataIndex += 6;
			}
			else if (this.diceData[this.dataIndex] == -2 || this.diceData[this.dataIndex] == -3)
			{
				this.dataIndex += 3;
			}
			else
			{
				this.dataIndex++;
			}
			return this.dataIndex < this.diceData.Count;
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x0009A002 File Offset: 0x00098202
		public void Dispose()
		{
			this.diceData = null;
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x0009A00B File Offset: 0x0009820B
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x0009A018 File Offset: 0x00098218
		public void Reset()
		{
			this.dataIndex = -1;
		}

		// Token: 0x04001050 RID: 4176
		public const int EMPTY = -1;

		// Token: 0x04001051 RID: 4177
		public const int MAX = -2;

		// Token: 0x04001052 RID: 4178
		public const int MAX_ROTATED = -3;

		// Token: 0x04001053 RID: 4179
		private List<int> diceData;

		// Token: 0x04001054 RID: 4180
		private int dataIndex;

		// Token: 0x04001055 RID: 4181
		private int diceUnitWidth;

		// Token: 0x04001056 RID: 4182
		private int diceUnitHeight;
	}
}
