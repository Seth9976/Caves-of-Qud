using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using LitJson;

namespace XRL
{
	// Token: 0x020004C9 RID: 1225
	public static class Analytics
	{
		// Token: 0x060034EC RID: 13548 RVA: 0x000FFDF0 File Offset: 0x000FDFF0
		public static void Log(string Key, string Value)
		{
			Analytics.Message.Clear();
			Analytics.Message.Add(Key, Value);
			Analytics.Log(Analytics.Message);
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000FFE14 File Offset: 0x000FE014
		public static void Log(Dictionary<string, string> Message)
		{
			if (!Analytics.Enabled)
			{
				return;
			}
			string text = "3f2566437044cda60c96a1b9e31e6fcd";
			string text2 = "4857668fc230220314b315d9ad790c34b46ff065";
			string text3 = "http://api.gameanalytics.com/1";
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			string text4 = "";
			NetworkInterface[] array = allNetworkInterfaces;
			for (int i = 0; i < array.Length; i++)
			{
				PhysicalAddress physicalAddress = array[i].GetPhysicalAddress();
				if (physicalAddress.ToString() != "" && text4 == "")
				{
					byte[] bytes = Encoding.UTF8.GetBytes(physicalAddress.ToString());
					text4 = BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(bytes)).Replace("-", "");
				}
			}
			string text5 = "design";
			string text6 = JsonMapper.ToJson(Message);
			HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
			byte[] bytes2 = Encoding.Default.GetBytes(text6 + text2);
			byte[] array2 = hashAlgorithm.ComputeHash(bytes2);
			string text7 = "";
			foreach (byte b in array2)
			{
				text7 += string.Format("{0:x2}", b);
			}
			byte[] bytes3 = Encoding.ASCII.GetBytes(text6);
			WebRequest webRequest = WebRequest.Create(string.Concat(new string[] { text3, "/", text, "/", text5 }));
			webRequest.Headers.Add("Authorization", text7);
			webRequest.Method = "POST";
			webRequest.ContentLength = (long)bytes3.Length;
			webRequest.ContentType = "application/x-www-form-urlencoded";
			Stream requestStream = webRequest.GetRequestStream();
			requestStream.Write(bytes3, 0, bytes3.Length);
			requestStream.Close();
		}

		// Token: 0x04001AC8 RID: 6856
		public static bool Enabled = false;

		// Token: 0x04001AC9 RID: 6857
		private static Dictionary<string, string> Message = new Dictionary<string, string>();
	}
}
