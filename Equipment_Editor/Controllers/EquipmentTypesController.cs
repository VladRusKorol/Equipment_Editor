using Equipment_Editor.DTO.EquipmentTypes;
using Equipment_Editor.Models;
using Equipment_Editor.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Equipment_Editor.Controllers
{
    public class EquipmentTypesController(IRepositoryBase<Equipment_Type> repository) : Controller
    {
        private readonly EquipmentTypeRepository _repository = (EquipmentTypeRepository)repository;
        
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Equipment_Type>? types = await _repository.GetAllAsync();
            return View(types);
        }

        [HttpGet]
        public IActionResult AddPage()
        {
            var newType = new CreateEquipmentTypeDTO() { Name = "", IsActive = false };
            return View(newType);
        }        
        
        [HttpPost]
        public async Task<IActionResult> Create([Bind($"{nameof(CreateEquipmentTypeDTO.Name)},{nameof(CreateEquipmentTypeDTO.IsActive)}")] CreateEquipmentTypeDTO createEntity)
        {
            await _repository.CreateAsync(new Equipment_Type() { Name = createEntity.Name, IsActive = createEntity.IsActive });
            var titleEquipmentTypesController = nameof(EquipmentTypesController);
            var startIndexController = titleEquipmentTypesController.IndexOf(nameof(Controller));
            var titleArtist = titleEquipmentTypesController[..startIndexController];
            return RedirectToAction(nameof(Index), titleArtist);
        }

        [HttpGet]
        public IActionResult ClosePage()
        {
            var titleEquipmentTypesController = nameof(EquipmentTypesController);
            var startIndexController = titleEquipmentTypesController.IndexOf(nameof(Controller));
            var titleArtist = titleEquipmentTypesController[..startIndexController];
            return RedirectToAction(nameof(Index), titleArtist);
        }



        public async Task<IActionResult> Delete(int id)
        {
            int result = await _repository.DeleteAsync(id);
            var titleEquipmentTypesController = nameof(EquipmentTypesController);
            var startIndexController = titleEquipmentTypesController.IndexOf(nameof(Controller));
            var titleArtist = titleEquipmentTypesController[..startIndexController];
            return RedirectToAction(nameof(Index), titleArtist);
        }

        [HttpGet]
        public async Task<IActionResult> EditPage(int id)
        {
            Equipment_Type editType = await _repository.GetByIdAysnc(id);
            return View(editType);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind($"{nameof(Equipment_Type.Id)},{nameof(Equipment_Type.Name)},{nameof(Equipment_Type.IsActive)}")] Equipment_Type editEntity)
        {
            await _repository.UpdateAsync(editEntity);
            var titleEquipmentTypesController = nameof(EquipmentTypesController);
            var startIndexController = titleEquipmentTypesController.IndexOf(nameof(Controller));
            var titleArtist = titleEquipmentTypesController[..startIndexController];
            return RedirectToAction(nameof(Index), titleArtist);
        }
    }
}
