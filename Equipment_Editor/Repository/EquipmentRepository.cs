using Equipment_Editor.DTO.Equipment;
using Equipment_Editor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Equipment_Editor.Repository
{
    public class EquipmentRepository(EquipmentContext context) : IRepositoryBase<Equipment>
    {
        private readonly EquipmentContext _context = context;
        public async Task<int> CreateAsync(Equipment entity)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(entity);
                await this._context.Equipments.AddAsync(entity);
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
                Equipment? deleteEntity = await this._context.Equipments.FirstOrDefaultAsync(x => x.Id == id);
                ArgumentNullException.ThrowIfNull(deleteEntity);
                await Task.Run(() => this._context.Equipments.Remove(deleteEntity));
                await this._context.SaveChangesAsync();
                return id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public async Task<List<Equipment>?> GetAllAsync()
        {
            try
            {
                return await _context.Equipments.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<Equipment> GetByIdAysnc(int id)
        {
            Equipment? findEntity = await _context.Equipments.FirstOrDefaultAsync(x => x.Id == id);
            ArgumentNullException.ThrowIfNull(findEntity);
            return findEntity;
        }

        public async Task<int> UpdateAsync(Equipment entity)
        {

            try
            {
                int idqqq = entity.Id;
                ArgumentNullException.ThrowIfNull(entity);
                Equipment? findEntity = await _context.Equipments.FirstOrDefaultAsync(x => x.Id == entity.Id);
                ArgumentNullException.ThrowIfNull(findEntity);
                findEntity.Name = entity.Name;
                findEntity.IsActive = entity.IsActive;
                findEntity.InvNumber = entity.InvNumber;
                findEntity.EntryDate = entity.EntryDate;
                findEntity.ModelId = entity.ModelId;
                findEntity.TypeId = entity.TypeId;
                await this._context.SaveChangesAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }
    
        public async Task<List<ReadableEquipmentDTO>?> GetAllReadableEquipmentAsync()
        {
            try
            {
                List<ReadableEquipmentDTO> equipments = await _context
                    .Equipments
                    .Include(e => e.Model)
                    .Include(e => e.Type)
                    .Select(e => new ReadableEquipmentDTO {
                            Id = e.Id,
                            Name = e.Name,
                            IsActive = e.IsActive,
                            EntryDate = DateOnly.Parse(e.EntryDate),
                            InvNumber = e.InvNumber,
                            ModelName = e.Model.Name, 
                            TypeName = e.Type.Name 
                        }).ToListAsync();
                return equipments;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<ReadableEquipmentDTO?> GetReadableEquipmentAsync(int id)
        {
            try
            {
                Equipment? equipment = await _context
                    .Equipments
                    .Include(e => e.Model)
                    .Include(e => e.Type)
                    .SingleOrDefaultAsync(e => e.Id == id);
                ArgumentNullException.ThrowIfNull(equipment);

                return new ReadableEquipmentDTO()
                {
                    Id = id,
                    Name = equipment.Name,
                    InvNumber = equipment.InvNumber,
                    IsActive = equipment.IsActive,
                    TypeName = equipment.Type.Name,
                    ModelName = equipment.Model.Name,
                    EntryDate = DateOnly.Parse(equipment.EntryDate)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    
        public async Task<CreateEquipmentDTO> GetCreateEquipmentDTOTemplate()
        {
            List<string> types = await _context.Equipment_Types.Where(e => e.IsActive == true).Select(e => e.Name).ToListAsync();
            List<string> models = await _context.Equipment_Models.Where(e => e.IsActive == true).Select(e => e.Name).ToListAsync();
            return new CreateEquipmentDTO()
            {
                Equipment = new ReadableEquipmentDTO()
                {
                    Name = "",
                    InvNumber = "",
                    IsActive = true,
                    TypeName = "",
                    ModelName = "",
                    EntryDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                },
                Models = new SelectList(models),
                Types = new SelectList(types)
            };
        }
    
        public async Task<int> GetModelIdByName(string modelName)
        {
            Equipment_Model? model = await _context.Equipment_Models.SingleOrDefaultAsync(e => e.Name == modelName);
            ArgumentNullException.ThrowIfNull(model);
            return model.Id;
        }

        public async Task<int> GetTypeIdByName(string typeName)
        {
            Equipment_Type? model = await _context.Equipment_Types.SingleOrDefaultAsync(e => e.Name == typeName);
            ArgumentNullException.ThrowIfNull(model);
            return model.Id;
        }

        public async Task<EditEquipmentDTO> GetEditEquipmentDTOAsync(int id)
        {
            List<string> types = await _context.Equipment_Types.Where(e => e.IsActive == true).Select(e => e.Name).ToListAsync();
            List<string> models = await _context.Equipment_Models.Where(e => e.IsActive == true).Select(e => e.Name).ToListAsync();
            ReadableEquipmentDTO? equipment = await GetReadableEquipmentAsync(id);
            ArgumentNullException.ThrowIfNull(equipment);
            return new EditEquipmentDTO()
            {
                Equipment = equipment,
                Models = new SelectList(models),
                Types = new SelectList(types)
            };
        }
    }
}
