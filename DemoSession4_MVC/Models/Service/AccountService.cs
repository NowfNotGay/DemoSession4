using DemoSession4_MVC.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace DemoSession4_MVC.Models.Service;

public class AccountService : IAccountService
{
    private readonly DatabaseContext _databaseContext;

    public AccountService(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public bool AddAccount(Account account)
    {
        try
        {
            _databaseContext.Accounts.Add(account);
            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool CheckAccount(string username) => _databaseContext.Accounts.Where(a => a.Username == username).Count() > 0;

    public bool CheckLogin(string username, string password)
    {
        try
        {
            var acc = _databaseContext.Accounts.FirstOrDefault(a => a.Username == username && a.Status == true);
            if(acc != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, acc.Password))
                {
                    return true;
                };
            }
            return false;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public Account GetAccountByEmailNoTracking(string email) => _databaseContext.Accounts.AsNoTracking().FirstOrDefault(p => p.Email == email);

    //Lazy 
    public Account GetAccountById(string username)
    {
        throw new NotImplementedException();
    }

    public Account GetAccountByUsername(string username) => _databaseContext.Accounts.FirstOrDefault(a => a.Username == username)!;
    //!Lazy
    public Account GetAccountByUsernameNoTracking(string username) => _databaseContext.Accounts.AsNoTracking().SingleOrDefault(a=>a.Username == username);

    public bool UpdateAccount(Account account)
    {
        try
        {
            _databaseContext.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return _databaseContext.SaveChanges() > 0;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
