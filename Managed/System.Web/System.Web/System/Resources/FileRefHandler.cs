using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.Resources
{
	// Token: 0x0200001E RID: 30
	internal class FileRefHandler : ResXDataNodeHandler
	{
		// Token: 0x0600007A RID: 122 RVA: 0x00003ABF File Offset: 0x00001CBF
		public FileRefHandler(ResXFileRef fileRef)
		{
			this.resXFileRef = fileRef;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003ACE File Offset: 0x00001CCE
		public override object GetValue(ITypeResolutionService typeResolver)
		{
			return this.GetValue();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003ACE File Offset: 0x00001CCE
		public override object GetValue(AssemblyName[] assemblyNames)
		{
			return this.GetValue();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public override string GetValueTypeName(ITypeResolutionService typeResolver)
		{
			Type type = base.ResolveType(this.resXFileRef.TypeName, typeResolver);
			if (type == null)
			{
				return this.resXFileRef.TypeName;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003B14 File Offset: 0x00001D14
		public override string GetValueTypeName(AssemblyName[] assemblyNames)
		{
			Type type = base.ResolveType(this.resXFileRef.TypeName, assemblyNames);
			if (type == null)
			{
				return this.resXFileRef.TypeName;
			}
			return type.AssemblyQualifiedName;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003B50 File Offset: 0x00001D50
		private object GetValue()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(ResXFileRef));
			object obj;
			try
			{
				obj = converter.ConvertFromInvariantString(this.resXFileRef.ToString());
			}
			catch (ArgumentNullException ex)
			{
				if (ex.ParamName == "type")
				{
					throw new TypeLoadException("Could not find type", ex);
				}
				throw ex;
			}
			return obj;
		}

		// Token: 0x04000D66 RID: 3430
		private ResXFileRef resXFileRef;
	}
}
