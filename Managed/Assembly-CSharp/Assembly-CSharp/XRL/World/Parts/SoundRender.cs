using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Genkit;

namespace XRL.World.Parts
{
	// Token: 0x02001240 RID: 4672
	[Serializable]
	public class SoundRender : SoundRendererPart
	{
		// Token: 0x0600B9B4 RID: 47540 RVA: 0x0043CC62 File Offset: 0x0043AE62
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B9B5 RID: 47541 RVA: 0x0043CC68 File Offset: 0x0043AE68
		public override bool Render(RenderEvent E)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.Sound) && this.Sound == "sfx_torch_flame_lp")
				{
					E.WantsToPaint = true;
					this.d = this.ParentObject.DistanceTo(The.Player);
					if (!SoundRender.Positions.ContainsKey(this.Sound))
					{
						SoundRender.Positions.Add(this.Sound, new List<Location2D>());
					}
					GameObject parentObject = this.ParentObject;
					Location2D location2D;
					if (parentObject == null)
					{
						location2D = null;
					}
					else
					{
						Cell currentCell = parentObject.CurrentCell;
						location2D = ((currentCell != null) ? currentCell.Location : null);
					}
					if (location2D != null)
					{
						List<Location2D> list = SoundRender.Positions[this.Sound];
						Cell currentCell2 = this.ParentObject.CurrentCell;
						list.Add((currentCell2 != null) ? currentCell2.Location : null);
					}
					if (!SoundRender.ClosestDistance.ContainsKey(this.Sound))
					{
						SoundRender.ClosestDistance.Add(this.Sound, this.d);
					}
					else if (SoundRender.ClosestDistance[this.Sound] > this.d)
					{
						SoundRender.ClosestDistance[this.Sound] = this.d;
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("SoundRender::Render", ex, "game_exception");
			}
			return base.Render(E);
		}

		// Token: 0x0600B9B6 RID: 47542 RVA: 0x0043CDC0 File Offset: 0x0043AFC0
		public override void OnPaint(ScreenBuffer buffer)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.Sound) && SoundRender.ClosestDistance.ContainsKey(this.Sound))
				{
					SoundRender.ClosestDistance.Remove(this.Sound);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("SoundRender::OnPaint", ex, "game_exception");
			}
		}

		// Token: 0x0600B9B7 RID: 47543 RVA: 0x0043CE24 File Offset: 0x0043B024
		public override void Render(RenderSoundEvent E)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.Sound) && this.Sound == "sfx_torch_flame_lp")
				{
					if (!SoundRender.ClosestDistance.ContainsKey(this.Sound) || SoundRender.ClosestDistance[this.Sound] != this.d || !SoundRender.Positions.ContainsKey(this.Sound))
					{
						return;
					}
					int num = 0;
					int num2 = 0;
					for (int i = 0; i < SoundRender.Positions[this.Sound].Count; i++)
					{
						num += SoundRender.Positions[this.Sound][i].X;
						num2 += SoundRender.Positions[this.Sound][i].Y;
					}
					if (SoundRender.Positions[this.Sound].Count > 0)
					{
						num /= SoundRender.Positions[this.Sound].Count;
						num2 /= SoundRender.Positions[this.Sound].Count;
					}
					num = Math.Clamp(num, 0, 79);
					num2 = Math.Clamp(num2, 0, 24);
					SoundRender.ClosestDistance.Remove(this.Sound);
					SoundRender.Positions[this.Sound].Clear();
					if (this.ID == null)
					{
						this.ID = Guid.NewGuid().ToString();
					}
					ConsoleChar consoleChar = E.Buffer[num, num2];
					if (consoleChar != null)
					{
						consoleChar.soundExtra.Add(this.ID, this.Sound, this.Volume, this.PitchVariance, this.CostMultiplier, this.CostMaximum);
					}
					return;
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("SoundRender::OnPaint", ex, "game_exception");
			}
			if (this.ID == null)
			{
				this.ID = Guid.NewGuid().ToString();
			}
			E.Char.soundExtra.Add(this.ID, this.Sound, this.Volume, this.PitchVariance, this.CostMultiplier, this.CostMaximum);
		}

		// Token: 0x04004AA8 RID: 19112
		public string Sound;

		// Token: 0x04004AA9 RID: 19113
		private string ID;

		// Token: 0x04004AAA RID: 19114
		public float Volume = 1f;

		// Token: 0x04004AAB RID: 19115
		public float PitchVariance;

		// Token: 0x04004AAC RID: 19116
		public float CostMultiplier = 1f;

		// Token: 0x04004AAD RID: 19117
		public int CostMaximum = int.MaxValue;

		// Token: 0x04004AAE RID: 19118
		[NonSerialized]
		private int d;

		// Token: 0x04004AAF RID: 19119
		[NonSerialized]
		private static Dictionary<string, int> ClosestDistance = new Dictionary<string, int>();

		// Token: 0x04004AB0 RID: 19120
		[NonSerialized]
		private static Dictionary<string, List<Location2D>> Positions = new Dictionary<string, List<Location2D>>();
	}
}
