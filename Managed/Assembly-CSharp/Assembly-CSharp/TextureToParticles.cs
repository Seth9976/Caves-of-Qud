using System;
using Kobold;
using UnityEngine;

// Token: 0x0200005E RID: 94
[ExecuteAlways]
public class TextureToParticles : MonoBehaviour, CombatJuice.ICombatJuiceConfigurable
{
	// Token: 0x060002CD RID: 717 RVA: 0x00010568 File Offset: 0x0000E768
	public void configure(string configurationString)
	{
		this.texture = SpriteManager.GetUnitySprite(configurationString).texture;
		this.reset = true;
	}

	// Token: 0x060002CE RID: 718 RVA: 0x00010582 File Offset: 0x0000E782
	private void Awake()
	{
		this.reset = true;
	}

	// Token: 0x060002CF RID: 719 RVA: 0x0001058C File Offset: 0x0000E78C
	private void Update()
	{
		if (!this.reset)
		{
			return;
		}
		foreach (ParticleSystem particleSystem in this.particles)
		{
			int num = particleSystem.GetParticles(this.gos);
			if (num > 0 && this.texture != null)
			{
				for (int j = 0; j < this.texture.width; j++)
				{
					for (int k = 0; k < this.texture.height; k++)
					{
						if (this.texture.GetPixel(j, k).a == 0f)
						{
							this.gos[j + k * this.texture.width].remainingLifetime = 0f;
						}
						else
						{
							this.gos[j + k * this.texture.width].position = new Vector3((float)j - 7.5f, (float)k - 11.5f);
						}
					}
				}
				particleSystem.SetParticles(this.gos, num);
				this.reset = false;
			}
		}
	}

	// Token: 0x04000217 RID: 535
	public bool reset = true;

	// Token: 0x04000218 RID: 536
	public Texture2D texture;

	// Token: 0x04000219 RID: 537
	public ParticleSystem[] particles;

	// Token: 0x0400021A RID: 538
	private ParticleSystem.Particle[] gos = new ParticleSystem.Particle[384];
}
