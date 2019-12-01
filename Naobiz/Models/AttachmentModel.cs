using BlazorInputFile;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Naobiz.Models
{
    class AttachmentModel : IDisposable
    {
        readonly IFileListEntry m_Source;

        public AttachmentModel(IFileListEntry source)
        {
            m_Source = source;
        }

        public void Dispose()
        {
            Data?.Dispose();
        }

        public string Name => m_Source.Name;

        public long Size => m_Source.Size;

        public const int SizeQuota = 20;

        public bool SizeExceedsQuota => Size > SizeQuota * 0x100000;

        public MemoryStream Data { get; private set; }

        public async Task UploadAsync(Action onUploading)
        {
            if (Data == null)
            {
                Data = new MemoryStream();
            }
            m_Source.OnDataRead += (_, e) => onUploading?.Invoke();
            await m_Source.Data.CopyToAsync(Data);
            Uploaded = true;
        }

        public bool Uploaded { get; private set; }

        public int Progress => (int)(Data?.Position * 100 / Size ?? 0);
    }
}
