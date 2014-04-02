namespace UserEditor.Models
{
    public interface IUser
    {
        int Id { get; set; }

        string Firstname { get; set; }

        string Lastname { get; set; }

        string Status { get; set; }

        string[] Pages { get; set; }

        bool IsAdmin { get; set; }
    }
}
