using Equipment_Editor.Models;
using Microsoft.EntityFrameworkCore;

namespace Equipment_Editor.Repository;

public class EquipmentRepairRepository(EquipmentContext context) : IRepositoryBase<Equipment_Repair>
{
    private readonly EquipmentContext _context = context;

    public async Task<int> CreateAsync(Equipment_Repair entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);
            await this._context.Equipment_Repairs.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        try
        {
            Equipment_Repair? deleteEntity = await this._context.Equipment_Repairs.FirstOrDefaultAsync(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(deleteEntity);
            await Task.Run(() => this._context.Equipment_Repairs.Remove(deleteEntity));
            await this._context.SaveChangesAsync();
            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }

    public async Task<List<Equipment_Repair>?> GetAllAsync()
    {
        try
        {
            return await _context.Equipment_Repairs.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public async Task<Equipment_Repair> GetByIdAysnc(int id)
    {
        Equipment_Repair? findEntity = await _context.Equipment_Repairs.FirstOrDefaultAsync(x => x.Id == id);
        ArgumentNullException.ThrowIfNull(findEntity);
        return findEntity;
    }

    public async Task<int> UpdateAsync(Equipment_Repair entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);
            Equipment_Repair? findEntity = await _context.Equipment_Repairs.FirstOrDefaultAsync(x => x.Id == entity.Id);
            ArgumentNullException.ThrowIfNull(findEntity);
            findEntity.Description = entity.Description;
            findEntity.IsActive = entity.IsActive;
            await this._context.SaveChangesAsync();
            return entity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }
}
