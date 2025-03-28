using Equipment_Editor.Models;
using Microsoft.EntityFrameworkCore;

namespace Equipment_Editor.Repository;

public class EquipmentTypeRepository(EquipmentContext context) : IRepositoryBase<Equipment_Type>
{
    private readonly EquipmentContext _context = context;

    public async Task<int> CreateAsync(Equipment_Type entity)
    {
        try {
            ArgumentNullException.ThrowIfNull(entity);
            await this._context.Equipment_Types.AddAsync(entity);
            await this._context.SaveChangesAsync();
            return entity.Id;
        }
        catch(Exception ex) { 
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }

    public async Task<int> DeleteAsync(int id)
    {
        try
        {
            Equipment_Type? deleteEntity = await this._context.Equipment_Types.FirstOrDefaultAsync(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(deleteEntity);
            await Task.Run(() =>  this._context.Equipment_Types.Remove(deleteEntity) );
            await this._context.SaveChangesAsync();
            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }

    public async Task<List<Equipment_Type>?> GetAllAsync()
    {
        try
        {
            return await _context.Equipment_Types.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null; 
        }
    }

    public async Task<Equipment_Type> GetByIdAysnc(int id)
    {

        Equipment_Type? findEntity = await _context.Equipment_Types.FirstOrDefaultAsync(x => x.Id == id);
        ArgumentNullException.ThrowIfNull(findEntity);
        return findEntity;
    }

    public async Task<int> UpdateAsync(Equipment_Type entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);
            Equipment_Type? findEntity = await _context.Equipment_Types.FirstOrDefaultAsync(x => x.Id == entity.Id);
            ArgumentNullException.ThrowIfNull(findEntity);
            findEntity.Name = entity.Name;
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
