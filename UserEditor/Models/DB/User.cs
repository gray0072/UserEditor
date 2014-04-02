namespace UserEditor.Models.DB
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Status { get; set; }

        public string[] Pages
        {
            get
            {
                return string.IsNullOrEmpty(this.PagesString) ? null : this.PagesString.Split(';');
            }
            set
            {
                this.PagesString = value == null ? null : string.Join(";", value);
            }
        }

        public bool IsAdmin { get; set; }

        public string PagesString { get; set; }
    }
}
