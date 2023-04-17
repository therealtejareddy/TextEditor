using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TextEditorMVC.Models
{
    public class TextEditorModel
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }

    }
}
