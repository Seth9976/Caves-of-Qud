using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Xml;

namespace System.Web.Compilation
{
	// Token: 0x0200065F RID: 1631
	internal class PreservationFile
	{
		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x060045C6 RID: 17862 RVA: 0x000BE0F0 File Offset: 0x000BC2F0
		// (set) Token: 0x060045C7 RID: 17863 RVA: 0x000BE0F8 File Offset: 0x000BC2F8
		public string Assembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x060045C8 RID: 17864 RVA: 0x000BE101 File Offset: 0x000BC301
		// (set) Token: 0x060045C9 RID: 17865 RVA: 0x000BE109 File Offset: 0x000BC309
		public string FilePath
		{
			get
			{
				return this._filePath;
			}
			set
			{
				this._filePath = value;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x060045CA RID: 17866 RVA: 0x000BE112 File Offset: 0x000BC312
		// (set) Token: 0x060045CB RID: 17867 RVA: 0x000BE11A File Offset: 0x000BC31A
		public int FileHash
		{
			get
			{
				return this._fileHash;
			}
			set
			{
				this._fileHash = value;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x060045CC RID: 17868 RVA: 0x000BE123 File Offset: 0x000BC323
		// (set) Token: 0x060045CD RID: 17869 RVA: 0x000BE12B File Offset: 0x000BC32B
		public int Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x060045CE RID: 17870 RVA: 0x000BE134 File Offset: 0x000BC334
		// (set) Token: 0x060045CF RID: 17871 RVA: 0x000BE13C File Offset: 0x000BC33C
		public int Hash
		{
			get
			{
				return this._hash;
			}
			set
			{
				this._hash = value;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x000BE145 File Offset: 0x000BC345
		// (set) Token: 0x060045D1 RID: 17873 RVA: 0x000BE14D File Offset: 0x000BC34D
		public BuildResultTypeCode ResultType
		{
			get
			{
				return this._resultType;
			}
			set
			{
				this._resultType = value;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x060045D2 RID: 17874 RVA: 0x000BE156 File Offset: 0x000BC356
		// (set) Token: 0x060045D3 RID: 17875 RVA: 0x000BE15E File Offset: 0x000BC35E
		public string VirtualPath
		{
			get
			{
				return this._virtualPath;
			}
			set
			{
				this._virtualPath = value;
			}
		}

		// Token: 0x170015DE RID: 5598
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x000BE167 File Offset: 0x000BC367
		// (set) Token: 0x060045D5 RID: 17877 RVA: 0x000BE16F File Offset: 0x000BC36F
		public List<string> FileDeps
		{
			get
			{
				return this._filedeps;
			}
			set
			{
				this._filedeps = value;
			}
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x00002050 File Offset: 0x00000250
		public PreservationFile()
		{
		}

		// Token: 0x060045D7 RID: 17879 RVA: 0x000BE178 File Offset: 0x000BC378
		public PreservationFile(string filePath)
		{
			this._filePath = filePath;
			this.Parse(filePath);
		}

		// Token: 0x060045D8 RID: 17880 RVA: 0x000BE18E File Offset: 0x000BC38E
		public void Parse()
		{
			if (this._filePath == null)
			{
				throw new InvalidOperationException("File path is not defined");
			}
			this.Parse(this._filePath);
		}

		// Token: 0x060045D9 RID: 17881 RVA: 0x000BE1B0 File Offset: 0x000BC3B0
		public void Parse(string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("File path is required", "filePath");
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filePath);
			XmlNode documentElement = xmlDocument.DocumentElement;
			if (documentElement.Name != "preserve")
			{
				throw new InvalidOperationException("Invalid assembly mapping file format");
			}
			this.ParseRecursively(documentElement);
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000BE208 File Offset: 0x000BC408
		private void ParseRecursively(XmlNode root)
		{
			this._assembly = this.GetNonEmptyRequiredAttribute(root, "assembly");
			try
			{
				this._virtualPath = this.GetNonEmptyOptionalAttribute(root, "virtualPath");
				this._fileHash = this.GetNonEmptyOptionalAttributeInt32(root, "filehash");
				this._hash = this.GetNonEmptyOptionalAttributeInt32(root, "hash");
				this._flags = this.GetNonEmptyOptionalAttributeInt32(root, "flags");
				this._resultType = (BuildResultTypeCode)this.GetNonEmptyOptionalAttributeInt32(root, "resultType");
				foreach (object obj in root.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.NodeType == XmlNodeType.Element && !(xmlNode.Name != "filedeps"))
					{
						this.ReadFileDeps(xmlNode);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060045DB RID: 17883 RVA: 0x000BE2FC File Offset: 0x000BC4FC
		private void ReadFileDeps(XmlNode node)
		{
			if (this._filedeps == null)
			{
				this._filedeps = new List<string>();
			}
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element && !(xmlNode.Name != "filedep"))
				{
					string nonEmptyRequiredAttribute = this.GetNonEmptyRequiredAttribute(xmlNode, "name");
					this._filedeps.Add(nonEmptyRequiredAttribute);
				}
			}
		}

		// Token: 0x060045DC RID: 17884 RVA: 0x000BE398 File Offset: 0x000BC598
		public void Save()
		{
			if (this._filePath == null)
			{
				throw new InvalidOperationException("File path is not defined");
			}
			this.Save(this._filePath);
		}

		// Token: 0x060045DD RID: 17885 RVA: 0x000BE3BC File Offset: 0x000BC5BC
		public void Save(string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("File path is required", "filePath");
			}
			using (XmlWriter xmlWriter = XmlWriter.Create(filePath, new XmlWriterSettings
			{
				Indent = false,
				OmitXmlDeclaration = false,
				NewLineOnAttributes = false
			}))
			{
				xmlWriter.WriteStartElement("preserve");
				xmlWriter.WriteAttributeString("assembly", this._assembly);
				if (!string.IsNullOrEmpty(this._virtualPath))
				{
					xmlWriter.WriteAttributeString("virtualPath", this._virtualPath);
				}
				if (this._fileHash != 0)
				{
					xmlWriter.WriteAttributeString("filehash", this._fileHash.ToString());
				}
				if (this._flags != 0)
				{
					xmlWriter.WriteAttributeString("flags", this._flags.ToString());
				}
				if (this._hash != 0)
				{
					xmlWriter.WriteAttributeString("hash", this._hash.ToString());
				}
				if (this._resultType != BuildResultTypeCode.Unknown)
				{
					XmlWriter xmlWriter2 = xmlWriter;
					string text = "resultType";
					int resultType = (int)this._resultType;
					xmlWriter2.WriteAttributeString(text, resultType.ToString());
				}
				if (this._filedeps != null && this._filedeps.Count > 0)
				{
					xmlWriter.WriteStartElement("filedeps");
					foreach (string text2 in this._filedeps)
					{
						xmlWriter.WriteStartElement("filedep");
						xmlWriter.WriteAttributeString("name", text2);
						xmlWriter.WriteEndElement();
					}
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteEndElement();
			}
		}

		// Token: 0x060045DE RID: 17886 RVA: 0x00021E10 File Offset: 0x00020010
		private string GetNonEmptyOptionalAttribute(XmlNode n, string name)
		{
			return HandlersUtil.ExtractAttributeValue(name, n, true);
		}

		// Token: 0x060045DF RID: 17887 RVA: 0x000BE570 File Offset: 0x000BC770
		private int GetNonEmptyOptionalAttributeInt32(XmlNode n, string name)
		{
			string nonEmptyOptionalAttribute = this.GetNonEmptyOptionalAttribute(n, name);
			if (nonEmptyOptionalAttribute != null)
			{
				return int.Parse(nonEmptyOptionalAttribute);
			}
			return 0;
		}

		// Token: 0x060045E0 RID: 17888 RVA: 0x000BE591 File Offset: 0x000BC791
		private string GetNonEmptyRequiredAttribute(XmlNode n, string name)
		{
			return HandlersUtil.ExtractAttributeValue(name, n, false, false);
		}

		// Token: 0x04002519 RID: 9497
		private string _filePath;

		// Token: 0x0400251A RID: 9498
		private string _assembly;

		// Token: 0x0400251B RID: 9499
		private int _fileHash;

		// Token: 0x0400251C RID: 9500
		private int _flags;

		// Token: 0x0400251D RID: 9501
		private int _hash;

		// Token: 0x0400251E RID: 9502
		private BuildResultTypeCode _resultType;

		// Token: 0x0400251F RID: 9503
		private string _virtualPath;

		// Token: 0x04002520 RID: 9504
		private List<string> _filedeps;
	}
}
