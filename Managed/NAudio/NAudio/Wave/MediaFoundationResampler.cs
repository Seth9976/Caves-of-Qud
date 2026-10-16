using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NAudio.Dmo;
using NAudio.MediaFoundation;

namespace NAudio.Wave
{
	// Token: 0x020001CD RID: 461
	public class MediaFoundationResampler : MediaFoundationTransform
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x0001CE00 File Offset: 0x0001B000
		private static bool IsPcmOrIeeeFloat(WaveFormat waveFormat)
		{
			WaveFormatExtensible waveFormatExtensible = waveFormat as WaveFormatExtensible;
			return waveFormat.Encoding == WaveFormatEncoding.Pcm || waveFormat.Encoding == WaveFormatEncoding.IeeeFloat || (waveFormatExtensible != null && (waveFormatExtensible.SubFormat == AudioSubtypes.MFAudioFormat_PCM || waveFormatExtensible.SubFormat == AudioSubtypes.MFAudioFormat_Float));
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0001CE54 File Offset: 0x0001B054
		public MediaFoundationResampler(IWaveProvider sourceProvider, WaveFormat outputFormat)
			: base(sourceProvider, outputFormat)
		{
			if (!MediaFoundationResampler.IsPcmOrIeeeFloat(sourceProvider.WaveFormat))
			{
				throw new ArgumentException("Input must be PCM or IEEE float", "sourceProvider");
			}
			if (!MediaFoundationResampler.IsPcmOrIeeeFloat(outputFormat))
			{
				throw new ArgumentException("Output must be PCM or IEEE float", "outputFormat");
			}
			MediaFoundationApi.Startup();
			this.ResamplerQuality = 60;
			object obj = this.CreateResamplerComObject();
			this.FreeComObject(obj);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0001CEB9 File Offset: 0x0001B0B9
		private void FreeComObject(object comObject)
		{
			if (this.activate != null)
			{
				this.activate.ShutdownObject();
			}
			Marshal.ReleaseComObject(comObject);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001CED5 File Offset: 0x0001B0D5
		private object CreateResamplerComObject()
		{
			return new ResamplerMediaComObject();
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0001CEDC File Offset: 0x0001B0DC
		private object CreateResamplerComObjectUsingActivator()
		{
			IEnumerable<IMFActivate> enumerable = MediaFoundationApi.EnumerateTransforms(MediaFoundationTransformCategories.AudioEffect);
			foreach (IMFActivate imfactivate in enumerable)
			{
				Guid guid;
				imfactivate.GetGUID(MediaFoundationAttributes.MFT_TRANSFORM_CLSID_Attribute, out guid);
				if (guid.Equals(MediaFoundationResampler.ResamplerClsid))
				{
					object obj;
					imfactivate.ActivateObject(MediaFoundationResampler.IMFTransformIid, out obj);
					this.activate = imfactivate;
					return obj;
				}
			}
			return null;
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0001CF68 File Offset: 0x0001B168
		public MediaFoundationResampler(IWaveProvider sourceProvider, int outputSampleRate)
			: this(sourceProvider, MediaFoundationResampler.CreateOutputFormat(sourceProvider.WaveFormat, outputSampleRate))
		{
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0001CF80 File Offset: 0x0001B180
		protected override IMFTransform CreateTransform()
		{
			object obj = this.CreateResamplerComObject();
			IMFTransform imftransform = (IMFTransform)obj;
			IMFMediaType imfmediaType = MediaFoundationApi.CreateMediaTypeFromWaveFormat(this.sourceProvider.WaveFormat);
			imftransform.SetInputType(0, imfmediaType, _MFT_SET_TYPE_FLAGS.None);
			Marshal.ReleaseComObject(imfmediaType);
			IMFMediaType imfmediaType2 = MediaFoundationApi.CreateMediaTypeFromWaveFormat(this.outputWaveFormat);
			imftransform.SetOutputType(0, imfmediaType2, _MFT_SET_TYPE_FLAGS.None);
			Marshal.ReleaseComObject(imfmediaType2);
			IWMResamplerProps iwmresamplerProps = (IWMResamplerProps)obj;
			iwmresamplerProps.SetHalfFilterLength(this.ResamplerQuality);
			return imftransform;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0001CFEF File Offset: 0x0001B1EF
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x0001CFF7 File Offset: 0x0001B1F7
		public int ResamplerQuality
		{
			get
			{
				return this.resamplerQuality;
			}
			set
			{
				if (value < 1 || value > 60)
				{
					throw new ArgumentOutOfRangeException("Resampler Quality must be between 1 and 60");
				}
				this.resamplerQuality = value;
			}
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0001D014 File Offset: 0x0001B214
		private static WaveFormat CreateOutputFormat(WaveFormat inputFormat, int outputSampleRate)
		{
			WaveFormat waveFormat;
			if (inputFormat.Encoding == WaveFormatEncoding.Pcm)
			{
				waveFormat = new WaveFormat(outputSampleRate, inputFormat.BitsPerSample, inputFormat.Channels);
			}
			else
			{
				if (inputFormat.Encoding != WaveFormatEncoding.IeeeFloat)
				{
					throw new ArgumentException("Can only resample PCM or IEEE float");
				}
				waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(outputSampleRate, inputFormat.Channels);
			}
			return waveFormat;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0001D063 File Offset: 0x0001B263
		protected override void Dispose(bool disposing)
		{
			if (this.activate != null)
			{
				this.activate.ShutdownObject();
				this.activate = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000B2A RID: 2858
		private int resamplerQuality;

		// Token: 0x04000B2B RID: 2859
		private static readonly Guid ResamplerClsid = new Guid("f447b69e-1884-4a7e-8055-346f74d6edb3");

		// Token: 0x04000B2C RID: 2860
		private static readonly Guid IMFTransformIid = new Guid("bf94c121-5b05-4e6f-8000-ba598961414d");

		// Token: 0x04000B2D RID: 2861
		private IMFActivate activate;
	}
}
