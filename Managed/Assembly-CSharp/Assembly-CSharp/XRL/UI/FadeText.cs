using System;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI
{
	// Token: 0x0200147A RID: 5242
	public class FadeText : MonoBehaviour
	{
		// Token: 0x0600CE80 RID: 52864 RVA: 0x004B1E4E File Offset: 0x004B004E
		public void Reset()
		{
			this.Stage = 0;
			this.Elapsed = 0f;
			this.Skin.color = this.Skin.color.WithAlpha(0f);
		}

		// Token: 0x0600CE81 RID: 52865 RVA: 0x004B1E82 File Offset: 0x004B0082
		public void Awake()
		{
			this.Stage = 0;
			this.Elapsed = 0f;
			this.Skin.color = this.Skin.color.WithAlpha(0f);
		}

		// Token: 0x0600CE82 RID: 52866 RVA: 0x004B1EB6 File Offset: 0x004B00B6
		public void SetStage(int Stage)
		{
			this.Stage = Stage;
			this.Elapsed = 0f;
		}

		// Token: 0x0600CE83 RID: 52867 RVA: 0x004B1ECC File Offset: 0x004B00CC
		public void Update()
		{
			if (this.Stage != 2 && this.Stage != 3)
			{
				this.Elapsed += Time.deltaTime;
			}
			if (this.Stage == 0)
			{
				float num = Mathf.Clamp01(this.Elapsed / this.FadeIn);
				this.Skin.color = this.Skin.color.WithAlpha(num);
				if (this.Elapsed >= this.FadeIn)
				{
					this.SetStage(1);
					return;
				}
			}
			else if (this.Stage == 1)
			{
				this.Skin.color = this.Skin.color.WithAlpha(1f);
				if (this.Elapsed >= this.Hold)
				{
					this.SetStage(2);
					return;
				}
			}
			else
			{
				if (this.Stage == 2)
				{
					this.Skin.color = this.Skin.color.WithAlpha(1f);
					TutorialManager.ShowIntermissionPopupAsync("<nohighlight>", delegate
					{
						this.Stage = 4;
					}, 0, 0f, "s");
					this.Stage = 3;
					return;
				}
				if (this.Stage == 4)
				{
					if (this.After != null)
					{
						Action after = this.After;
						this.After = null;
						after();
					}
					float num2 = Mathf.Clamp01(1f - this.Elapsed / this.FadeIn);
					this.Skin.color = this.Skin.color.WithAlpha(num2);
					if (this.Elapsed >= this.FadeOut)
					{
						this.SetStage(0);
						base.gameObject.SetActive(false);
						if (this.EndDestroy)
						{
							base.gameObject.Destroy();
						}
					}
				}
			}
		}

		// Token: 0x04005015 RID: 20501
		public Image Skin;

		// Token: 0x04005016 RID: 20502
		public float FadeIn;

		// Token: 0x04005017 RID: 20503
		public float Hold;

		// Token: 0x04005018 RID: 20504
		public float FadeOut;

		// Token: 0x04005019 RID: 20505
		public bool EndDestroy;

		// Token: 0x0400501A RID: 20506
		public Action After;

		// Token: 0x0400501B RID: 20507
		private int Stage;

		// Token: 0x0400501C RID: 20508
		private float Elapsed;
	}
}
