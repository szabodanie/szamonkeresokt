using System;

public class szamonkeresapi.Moldes
{
	public class Dto()

	{
    public record CreateUserDto(string Title, string description);
    public record UpdateUserDto(string Title, string description);
}
}
