﻿using Application.DTOs.ServiceEntry;
using Infrastructure.Database;
using Infrastructure.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Implementations;

public class ServiceEntryRepository : IServiceEntryRepository
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<ServiceEntry> _table;

    public ServiceEntryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _table = _dbContext.Set<ServiceEntry>();
    }

    public async Task<ServiceEntry> Add(ServiceEntryDatabaseAddRequest entryRequest)
    {
        var serviceEntry = new ServiceEntry
        {
            Id = 0,
            CarId = entryRequest.CarId,
            ServiceId = entryRequest.ServiceId,
            ServiceEntryId = entryRequest.ServiceEntryId,
        };
        var dbResult = await _table.AddAsync(serviceEntry);
        await _dbContext.SaveChangesAsync();
        return dbResult.Entity;
    }

    public async Task<ServiceEntry?> Delete(int id)
    {
        var dbEntry = await _table.FindAsync(id);
        if (dbEntry is null)
        {
            return null;
        }

        _table.Remove(dbEntry);
        await _dbContext.SaveChangesAsync();
        return dbEntry;
    }

    public async Task<List<ServiceEntry>?> GetAllPaged(int offset = 0, int takeCount = 50)
    {
        return await _table.Skip(offset).Take(takeCount).ToListAsync();
    }

    public async Task<ServiceEntry?> GetById(int id)
    {
        return await _table.FindAsync(id);
    }

    public async Task<List<ServiceEntry>?> GetAllByCarId(int carId)
    {
        return await _table.Where(x => x.CarId == carId).ToListAsync();
    }

    public async Task<ServiceEntry?> Update(int id, ServiceEntryUpdateRequest entry)
    {
        var dbEntry = await _table.FindAsync(id);
        if (dbEntry is null)
        {
            return null;
        }

        await Detach(dbEntry);
        var serviceEntryUpdate = new ServiceEntry
        {
            Id = id,
            CarId = entry.CarId ?? dbEntry.CarId,
            ServiceId = entry.ServiceId ?? dbEntry.ServiceId,
            ServiceEntryId = entry.ServiceEntryId ?? dbEntry.ServiceEntryId,
        };
        var dbResult = _table.Update(serviceEntryUpdate);
        await _dbContext.SaveChangesAsync();
        return dbResult.Entity;
    }

    private async Task Detach(ServiceEntry entry)
    {
        _dbContext.Entry(entry).State = EntityState.Detached;
        await _dbContext.SaveChangesAsync();
    }
}
