using Equipment_Editor.DTO.EquipmentModel;
using Equipment_Editor.DTO.EquipmentTypes;
using Equipment_Editor.Models;
using Equipment_Editor.Repository;
using Equipment_Editor.Tools;
using Microsoft.AspNetCore.Mvc;

namespace Equipment_Editor.Controllers
{
    public class EquipmentModelController(IRepositoryBase<Equipment_Model> repository) : Controller
    {
        private readonly EquipmentModelRepository _repository = (EquipmentModelRepository)repository;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Equipment_Model>? models = await _repository.GetAllAsync();
            return View(models);
        }

        [HttpGet]
        public IActionResult ClosePage()
        {
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentModelController)));
        }

        [HttpGet]
        public IActionResult AddPage()
        {
            var newModel = new CreateEquipmentModelDTO() { Name = "", IsActive = false };
            return View(newModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind($"{nameof(CreateEquipmentModelDTO.Name)},{nameof(CreateEquipmentTypeDTO.IsActive)}")] CreateEquipmentModelDTO createEntity)
        {
            await _repository.CreateAsync(new Equipment_Model() { Name = createEntity.Name, IsActive = createEntity.IsActive });
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentModelController)));
        }

        public async Task<IActionResult> Delete(int id)
        {
            int result = await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentModelController)));
        }

        [HttpGet]
        public async Task<IActionResult> EditPage(int id)
        {
            Equipment_Model editModel = await _repository.GetByIdAysnc(id);
            return View(editModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind($"{nameof(Equipment_Model.Id)},{nameof(Equipment_Model.Name)},{nameof(Equipment_Model.IsActive)}")] Equipment_Model editEntity)
        {
            await _repository.UpdateAsync(editEntity);
            return RedirectToAction(nameof(Index), NameOfTools.NameOfController(nameof(EquipmentModelController)));
        }


    }
}
