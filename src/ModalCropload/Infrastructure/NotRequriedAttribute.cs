using System.ComponentModel.DataAnnotations;

namespace ModalCropload.Infrastructure
{
    public class NotRequriedAttribute : RequiredAttribute
    {
        public override bool IsValid(object value) {
            return true;
        }
    }
}