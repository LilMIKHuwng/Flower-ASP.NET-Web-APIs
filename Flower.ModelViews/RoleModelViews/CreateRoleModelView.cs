using System.ComponentModel.DataAnnotations;

namespace Flower.ModelViews.RoleModelViews
{
    public class CreateRoleModelView
    {
        [Required(ErrorMessage = "RoleName is required.")]
        public string Name { get; set; }

    }
}
