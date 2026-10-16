using System;
using ConsoleLib.Console;
using UnityEngine;
using XRL.World.Parts;

namespace QupKit
{
	// Token: 0x020003A5 RID: 933
	[ExecuteAlways]
	public class ParticleRandomizer : MonoBehaviour
	{
		// Token: 0x060022FA RID: 8954 RVA: 0x000B1CF4 File Offset: 0x000AFEF4
		public void Awake()
		{
			if (this.RandomizeColor)
			{
				ParticleSystem component = base.GetComponent<ParticleSystem>();
				component.time = (float)new global::System.Random().Next(1, 90);
				ParticleSystem.MainModule main = component.main;
				new global::System.Random().Next(1, 9);
				main.startColor = ConsoleLib.Console.ColorUtility.colorFromChar(Crayons.GetRandomColor(new global::System.Random())[0]);
			}
			if (this.RandomizeFungalColor)
			{
				ParticleSystem component2 = base.GetComponent<ParticleSystem>();
				component2.time = (float)new global::System.Random().Next(1, 90);
				ParticleSystem.MainModule main2 = component2.main;
				int num = new global::System.Random().Next(1, 5);
				if (num == 1)
				{
					main2.startColor = ConsoleLib.Console.ColorUtility.colorFromChar('Y');
				}
				if (num == 2)
				{
					main2.startColor = ConsoleLib.Console.ColorUtility.colorFromChar('C');
				}
				if (num == 3)
				{
					main2.startColor = ConsoleLib.Console.ColorUtility.colorFromChar('W');
				}
				if (num == 4)
				{
					main2.startColor = ConsoleLib.Console.ColorUtility.colorFromChar('R');
				}
			}
		}

		// Token: 0x0400119F RID: 4511
		public bool RandomizeColor;

		// Token: 0x040011A0 RID: 4512
		public bool RandomizeFungalColor;
	}
}
