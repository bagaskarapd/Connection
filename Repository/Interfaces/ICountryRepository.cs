using Connection.Models;

namespace Connection.Repositories.Interfaces;
public interface ICountryRepository
{
    List<Country> GetAll();
    Country GetById(string id);
    int Insert(Country country);
    int Update(Country country);
    int Delete(string id);
}