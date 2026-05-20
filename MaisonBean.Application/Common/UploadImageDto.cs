using System.IO;

namespace MaisonBean.Application.Common;

public class UploadImageDto
{
    public Stream Stream { get; set; }
        = default!;

    public string FileName { get; set; }
        = string.Empty;
}