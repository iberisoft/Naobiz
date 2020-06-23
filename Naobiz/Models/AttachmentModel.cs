using Blazorise;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Naobiz.Models
{
    public class AttachmentModel : IDisposable
    {
        readonly IFileEntry m_Source;

        public AttachmentModel(IFileEntry source)
        {
            m_Source = source;
            Name = source.Name;
            Size = source.Size;
        }

        public AttachmentModel(string name, long size)
        {
            Name = name;
            Size = size;
            Progress = 100;
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

        public async Task UploadAsync()
        {
            Data ??= new MemoryStream();
            await m_Source.WriteToStreamAsync(Data);
            Uploaded = true;
        }

        public bool Uploaded { get; private set; }

        public int Progress { get; set; }
    }
}
