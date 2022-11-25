using System.ComponentModel.DataAnnotations;

namespace JCat.BaseService.Model.ReqModel
{
    public class Id_ReqModel
    {
        [Required]
        public string Id { get; set; } = string.Empty;
    }
}
