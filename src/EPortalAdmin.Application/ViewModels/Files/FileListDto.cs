using EPortalAdmin.Core.Persistence.Paging;

namespace EPortalAdmin.Application.ViewModels.Files
{
    public class FileListDto : BasePageableModel
    {
        public IList<FileDto> Items { get; set; }
    }
}
