using Connection.Models;
using Connection.Repositories;
using Connection.Repositories.Interfaces;
using Connection.Views;
using System;

namespace Connection.Controllers;
public class CountryController
{
    private readonly ICountryRepository _countryRepository;
    private readonly VCountry _vCountry;

    public CountryController(ICountryRepository countryRepository, VCountry vCountry)
    {
        _countryRepository = countryRepository;
        _vCountry = vCountry;
    }

    // GET ALL
    public void GetAll()
    {
        var countries = _countryRepository.GetAll();
        if (countries == null)
        {
            _vCountry.DataNotFound();
        }
        _vCountry.GetAll(countries);
    }

    // GET BY ID
    public void GetById(string id)
    {
        var country = _countryRepository.GetById(id);
        if (country == null)
        {
            _vCountry.DataNotFound();
        }
    }

    // INSERT
    public void Insert(Country country)
    {
        var result = _countryRepository.Insert(country);
        if (result > 0)
        {
            _vCountry.Success("inserted");
        }
        else
        {
            _vCountry.Failure("insert");
        }
    }

    // UPDATE
    public void Update(Country country)
    {
        var result = _countryRepository.Update(country);
        if (result > 0)
        {
            _vCountry.Success("Updated");
        }
        else
        {
            _vCountry.Failure("Update");
        }
    }

    // DELETE
    public void Delete(string id)
    {
        var result = _countryRepository.Delete(id);
        if (result > 0)
        {
            _vCountry.Success("Deleted");
        }
        else
        {
            _vCountry.Failure("Delete");
        }
    }
}