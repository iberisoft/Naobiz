using BlazorInputFile;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Naobiz.Models
{
    public class AttachmentModel : IDisposable
    {
        readonly IFileListEntry m_Source;

        public AttachmentModel(IFileListEntry source)
        {
            m_Source = source;
            Name = source.Name;
            Size = source.Size;
        }

        public AttachmentModel(string name, long size)
        {
            Name = name;
            Size = size;
        }

        public void Dispose()
        {
            Data?.Dispose();
        }

        public string Name { get; }

        public long Size { get; }

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

        public int Progress => (int)(Data?.Position * 100 / Size ?? 100);
    }
}
