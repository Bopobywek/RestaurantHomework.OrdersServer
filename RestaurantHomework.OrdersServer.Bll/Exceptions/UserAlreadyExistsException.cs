namespace RestaurantHomework.OrdersServer.Bll.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base("Такой пользователь уже существует")
    {
    }
}