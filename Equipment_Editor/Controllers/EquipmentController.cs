using Equipment_Editor.DTO.Equipment;
using Equipment_Editor.DTO.EquipmentModel;
using Equipment_Editor.DTO.EquipmentTypes;
using Equipment_Editor.Models;
using Equipment_Editor.Repository;
using Equipment_Editor.Tools;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Equipment_Editor.Controllers
{
    public class EquipmentController(IRepositoryBase<Equipment> repository) : Controller
    {
        private readonly EquipmentRepository _repository = (EquipmentRepository)repository;
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ReadableEquipmentDTO>? equipments = await _repository.GetAllReadableEquipmentAsync();
            return View(equipments);
        }

        [HttpGet]
        public async Task<IActionResult> AddPage()
        {
            CreateEquipmentDTO equipments = await _repository.GetCreateEquipmentDTOTemplate();
            return View(equipments);
        }


        [HttpPost]
        public async Task<IActionResult> Create([Bind(@$"Equipment,Types,Models")] CreateEquipmentDTO createEntity)
        {
            int modelId = await _repository.GetModelIdByName(createEntity.Equipment.ModelName);
            int typeId = await _repository.GetTypeIdByName(createEntity.Equipment.TypeName);
            var newEquipment = new Equipment() { 
                Name = createEntity.Equipment.Name,
                InvNumber = createEntity.Equipment.InvNumber,
                IsActive = createEntity.Equipment.IsActive,
                ModelId = modelId,
                TypeId = typeId,
                EntryDate = createEntity.Equipment.EntryDate.ToString("yyyy-MM-dd")
            };
            await _repository.CreateAsync(newEquipment);
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentController)));
        }


        public async Task<IActionResult> Delete(int id)
        {
            int result = await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentController)));
        }

        [HttpGet]
        public IActionResult ClosePage()
        {
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentController)));
        }

        [HttpGet]
        public async Task<IActionResult> EditPage(int id)
        {
            EditEquipmentDTO editModel = await _repository.GetEditEquipmentDTOAsync(id);
            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind(@$"Equipment,Types,Models")] CreateEquipmentDTO updateEqupment)
        {
            int modelId = await _repository.GetModelIdByName(updateEqupment.Equipment.ModelName);
            int typeId = await _repository.GetTypeIdByName(updateEqupment.Equipment.TypeName);
            var editEntity = new Equipment()
            {
                Id = updateEqupment.Equipment.Id,
                Name = updateEqupment.Equipment.Name,
                InvNumber = updateEqupment.Equipment.InvNumber,
                IsActive = updateEqupment.Equipment.IsActive,
                ModelId = modelId,
                TypeId = typeId,
                EntryDate = updateEqupment.Equipment.EntryDate.ToString("yyyy-MM-dd")
            };

            Console.WriteLine("vv");
            await _repository.UpdateAsync(editEntity);
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentController)));
        }

    }
}
