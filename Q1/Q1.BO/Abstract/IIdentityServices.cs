namespace Q1.BO.Abstract;

public interface IIdentityServices
{ 
    Task<string> Login(string email, string password);
}