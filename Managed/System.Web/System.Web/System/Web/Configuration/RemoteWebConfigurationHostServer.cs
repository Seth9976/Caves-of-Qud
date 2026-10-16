using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Used internally at run time to support accessing configuration data remotely.</summary>
	// Token: 0x0200077C RID: 1916
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[ComVisible(true)]
	[Guid("9FDB6D2C-90EA-4e42-99E6-38B96E28698E")]
	[ProgId("System.Web.Configuration.RemoteWebConfigurationHostServerV4_32")]
	[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
	public class RemoteWebConfigurationHostServer : IRemoteWebConfigurationHostServer
	{
		/// <summary>Used internally at run time to create a new instance of <see cref="T:System.Web.Configuration.RemoteWebConfigurationHostServer" />.</summary>
		// Token: 0x06004DE4 RID: 19940 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public RemoteWebConfigurationHostServer()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Conditionally encrypts or decrypts the value of the string referenced by the <paramref name="xmlString" /> parameter.</summary>
		/// <returns>A string that contains either the encrypted or decrypted value of <paramref name="xmlString" />.</returns>
		/// <param name="doEncrypt">true to encrypt; false to decrypt.</param>
		/// <param name="xmlString">The XML to be encrypted or decrypted.</param>
		/// <param name="protectionProviderName">The provider used to protect the configuration data.</param>
		/// <param name="protectionProviderType">The <see cref="T:System.Type" /> of the protection provider.</param>
		/// <param name="paramKeys">The keys of optional parameters for the protection provider.</param>
		/// <param name="paramValues">The values of optional parameters for the protection provider.</param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="protectionProviderType" /> does not derive from <see cref="T:System.Configuration.ProtectedConfigurationProvider" />.</exception>
		// Token: 0x06004DE5 RID: 19941 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string DoEncryptOrDecrypt(bool doEncrypt, string xmlString, string protectionProviderName, string protectionProviderType, string[] paramKeys, string[] paramValues)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Used internally to support remote access to configuration data.</summary>
		/// <returns>An array of 8-bit unsigned integers (bytes) that contains the configuration data.</returns>
		/// <param name="fileName">The path to the remote configuration file to be accessed.</param>
		/// <param name="getReadTimeOnly">A Boolean value that specifies whether only the <paramref name="readTime" /> is returned.</param>
		/// <param name="readTime">The time when the file was last accessed.</param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="fileName" /> does not point to a file with the .config extension.</exception>
		// Token: 0x06004DE6 RID: 19942 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public byte[] GetData(string fileName, bool getReadTimeOnly, out long readTime)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the details of the configuration file.</summary>
		/// <param name="name">The name of the file.</param>
		/// <param name="exists">true if the file exists; otherwise, false.</param>
		/// <param name="size">The size of the file.</param>
		/// <param name="createDate">The date the file was created.</param>
		/// <param name="lastWriteDate">The date the file was last written.</param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="name" /> does not point to a file with the .config extension.</exception>
		// Token: 0x06004DE7 RID: 19943 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void GetFileDetails(string name, out bool exists, out long size, out long createDate, out long lastWriteDate)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Used internally to support remote access to configuration data.</summary>
		/// <returns>A concatenated string that represents the file path of the configuration file.</returns>
		/// <param name="webLevelAsInt">The level of the configuration file.</param>
		/// <param name="path">The path to the remote configuration file to be accessed.</param>
		/// <param name="site">The path to the remote computer.</param>
		/// <param name="locationSubPath">The subpath of the location of the configuration file.</param>
		// Token: 0x06004DE8 RID: 19944 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string GetFilePaths(int webLevelAsInt, string path, string site, string locationSubPath)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Used internally to support remote access to configuration data.</summary>
		/// <param name="fileName">The path to the remote configuration file to be accessed.</param>
		/// <param name="templateFileName">The file to duplicate file attributes from.</param>
		/// <param name="data">The data to be written.</param>
		/// <param name="readTime">The time when the file was last accessed.</param>
		/// <exception cref="T:System.Exception">
		///   <paramref name="fileName" /> does not point to a file with the .config extension.- or -The file has changed since it was read.- or -The file is hidden or read-only.- or -The method fails to generate a temp file.- or -The method fails to create a <see cref="T:System.IO.FileStream" />.- or -The temp file fails to overwrite the target file.</exception>
		// Token: 0x06004DE9 RID: 19945 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void WriteData(string fileName, string templateFileName, byte[] data, ref long readTime)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
