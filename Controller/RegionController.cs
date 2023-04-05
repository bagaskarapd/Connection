using Connection.Models;
using Connection.Repositories;
using Connection.Repositories.Interfaces;
using Connection.Views;
using System;
using System.Diagnostics.Metrics;

namespace Connection.Controllers;
public class RegionController
{
    private readonly IRegionRepository _regionRepository;
    private readonly VRegion _vRegion;

    public RegionController(IRegionRepository regionRepository, VRegion vRegion)
    {
        _regionRepository = regionRepository;
        _vRegion = vRegion;
    }

    // GET ALL
    public void GetAll()
    {
        var regions = _regionRepository.GetAll();
        if (regions == null)
        {
            _vRegion.DataNotFound();
        }
        _vRegion.GetAll(regions);
    }

    // GET BY ID
    public void GetById(int id)
    {
        var regions = _regionRepository.GetById(id);
        if (regions == null)
        {
            _vRegion.DataNotFound();
        }
    }

    // INSERT
    public void Insert(Region region)
    {
        var result = _regionRepository.Insert(region);
        if (result > 0)
        {
            _vRegion.Success("inserted");
        }
        else
        {
            _vRegion.Failure("insert");
        }
    }

    // UPDATE
    public void Update(Region region)
    {
        var result = _regionRepository.Update(region);
        if (result > 0)
        {
            _vRegion.Success("Updated");
        }
        else
        {
            _vRegion.Failure("Update");
        }
    }

    // DELETE
    public void Delete(int id)
    {
        var result = _regionRepository.Delete(id);
        if (result > 0)
        {
            _vRegion.Success("Deleted");
        }
        else
        {
            _vRegion.Failure("Delete");
        }
    }
}