using Equipment_Editor.Models;
using Microsoft.EntityFrameworkCore;

namespace Equipment_Editor.Repository;

public class EquipmentModelRepository(EquipmentContext context) : IRepositoryBase<Equipment_Model>
{
    private readonly EquipmentContext _context = context;
    public async Task<int> CreateAsync(Equipment_Model entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);
            await this._context.Equipment_Models.AddAsync(entity);
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
            Equipment_Model? deleteEntity = await this._context.Equipment_Models.FirstOrDefaultAsync(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(deleteEntity);
            await Task.Run(() => this._context.Equipment_Models.Remove(deleteEntity));
            await this._context.SaveChangesAsync();
            return id;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return -1;
        }
    }

    public async Task<List<Equipment_Model>?> GetAllAsync()
    {
        try
        {
            return await _context.Equipment_Models.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }

    public async Task<Equipment_Model> GetByIdAysnc(int id)
    {
        Equipment_Model? findEntity = await _context.Equipment_Models.FirstOrDefaultAsync(x => x.Id == id);
        ArgumentNullException.ThrowIfNull(findEntity);
        return findEntity;
    }

    public async Task<int> UpdateAsync(Equipment_Model entity)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(entity);
            Equipment_Model? findEntity = await _context.Equipment_Models.FirstOrDefaultAsync(x => x.Id == entity.Id);
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
