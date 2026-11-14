namespace EduHub.Domain.Entities;

public class Member
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }

    private Member(Guid id, string email)
    {
        Id = id;
        Email = email;
    }

    public static Member Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email)
            || !email.Contains('@'))
        {
            throw new ArgumentException(
                "Email không hợp lệ.");
        }

        return new Member(Guid.NewGuid(), email);
    }
}