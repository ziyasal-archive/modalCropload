using System.ComponentModel.DataAnnotations;

namespace Web.UI.Infrastructure
{
    public class NotRequriedAttribute : RequiredAttribute
    {
        public override bool IsValid(object value) {
            return true;
        }
    }
}