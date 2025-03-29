using Equipment_Editor.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Equipment_Editor.Tools
{
    public static class NameOfTools
    {
        public static string NameOfController(string nameController)
        {
            var startIndexController = nameController.IndexOf(nameof(Controller));
            return nameController[..startIndexController];
        }
    }
}
