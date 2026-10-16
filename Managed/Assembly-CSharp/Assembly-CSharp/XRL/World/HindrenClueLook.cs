using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000850 RID: 2128
	[GenerateSerializationPartial]
	[Serializable]
	public sealed class HindrenClueLook : IComposite
	{
		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060054D9 RID: 21721 RVA: 0x00195E58 File Offset: 0x00194058
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060054DA RID: 21722 RVA: 0x00195E5B File Offset: 0x0019405B
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.target);
			Writer.WriteOptimized(this.text);
		}

		// Token: 0x060054DB RID: 21723 RVA: 0x00195E75 File Offset: 0x00194075
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public void Read(SerializationReader Reader)
		{
			this.target = Reader.ReadOptimizedString();
			this.text = Reader.ReadOptimizedString();
		}

		// Token: 0x060054DC RID: 21724 RVA: 0x00195E8F File Offset: 0x0019408F
		public HindrenClueLook()
		{
		}

		// Token: 0x060054DD RID: 21725 RVA: 0x00195E97 File Offset: 0x00194097
		public HindrenClueLook(string target, string text)
		{
			this.target = target;
			this.text = text;
		}

		// Token: 0x060054DE RID: 21726 RVA: 0x00195EB0 File Offset: 0x001940B0
		public void apply(GameObject go)
		{
			HindrenClueItem hindrenClueItem = new HindrenClueItem();
			go.AddPart<HindrenClueItem>(hindrenClueItem, true, false);
			Description part = go.GetPart<Description>();
			part._Short = part._Short + " " + this.text;
			if (this.target == "Kesehind")
			{
				GameObject equipped = go.Body.GetPart("Body")[0].Equipped;
				if (equipped != null)
				{
					equipped.MakeBloodstained("blood", 7, 9999);
				}
			}
		}

		// Token: 0x04002AB6 RID: 10934
		public string target;

		// Token: 0x04002AB7 RID: 10935
		public string text;
	}
}
