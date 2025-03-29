using Equipment_Editor.DTO.EquipmentModel;
using Equipment_Editor.DTO.EquipmentRepair;
using Equipment_Editor.DTO.EquipmentTypes;
using Equipment_Editor.Models;
using Equipment_Editor.Repository;
using Equipment_Editor.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Equipment_Editor.Controllers
{
    public class EquipmentRepairController(IRepositoryBase<Equipment_Repair> repository) : Controller
    {
        private readonly EquipmentRepairRepository _repository = (EquipmentRepairRepository)repository;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Equipment_Repair>? models = await _repository.GetAllAsync();
            return View(models);
        }

        [HttpGet]
        public IActionResult ClosePage()
        {
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentRepairController)));
        }

        [HttpGet]
        public IActionResult AddPage()
        {
            var newModel = new CreateEquipmentRepairDTO() { Description = "", IsActive = false };
            return View(newModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind($"{nameof(CreateEquipmentRepairDTO.Description)},{nameof(CreateEquipmentRepairDTO.IsActive)}")] CreateEquipmentRepairDTO createEntity)
        {
            await _repository.CreateAsync(new Equipment_Repair() { Description = createEntity.Description, IsActive = createEntity.IsActive });
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentRepairController)));
        }

        public async Task<IActionResult> Delete(int id)
        {
            int result = await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentRepairController)));
        }

        [HttpGet]
        public async Task<IActionResult> EditPage(int id)
        {
            Equipment_Repair editRepair = await _repository.GetByIdAysnc(id);
            return View(editRepair);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind($"{nameof(Equipment_Repair.Id)},{nameof(Equipment_Repair.Description)},{nameof(Equipment_Repair.IsActive)}")] Equipment_Repair editEntity)
        {
            await _repository.UpdateAsync(editEntity);
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentRepairController)));
        }
    }
}
