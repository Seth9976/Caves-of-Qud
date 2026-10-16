using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.World.Parts;

namespace XRL.World.AI
{
	// Token: 0x02000B3F RID: 2879
	[GenerateSerializationPartial]
	public class OpinionAttackProperty : IOpinionCombat
	{
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06007446 RID: 29766 RVA: 0x002BE167 File Offset: 0x002BC367
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007447 RID: 29767 RVA: 0x002BE16A File Offset: 0x002BC36A
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.What);
			Writer.Write(this.Magnitude);
			Writer.WriteOptimized(this.Time);
		}

		// Token: 0x06007448 RID: 29768 RVA: 0x002BE190 File Offset: 0x002BC390
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(SerializationReader Reader)
		{
			this.What = Reader.ReadOptimizedString();
			this.Magnitude = Reader.ReadSingle();
			this.Time = Reader.ReadOptimizedInt64();
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06007449 RID: 29769 RVA: 0x002BE1B6 File Offset: 0x002BC3B6
		public override int BaseValue
		{
			get
			{
				return -25;
			}
		}

		// Token: 0x0600744A RID: 29770 RVA: 0x002BE1BA File Offset: 0x002BC3BA
		public override void Initialize(GameObject Actor, GameObject Subject, GameObject Object)
		{
			string text;
			if (Object == null)
			{
				text = null;
			}
			else
			{
				Render render = Object.Render;
				text = ((render != null) ? render.DisplayName : null);
			}
			this.What = text;
		}

		// Token: 0x0600744B RID: 29771 RVA: 0x002BE1DA File Offset: 0x002BC3DA
		public override string GetText(GameObject Actor)
		{
			return "Attacked my " + (this.What ?? "things") + ".";
		}

		// Token: 0x04003587 RID: 13703
		public string What;
	}
}
