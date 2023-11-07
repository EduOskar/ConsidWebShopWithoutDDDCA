using System.ComponentModel.DataAnnotations;

namespace CondisWebshop.Web.Models;

public class CheckoutForm
{
    [Required(ErrorMessage = "Firstname is Required.")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Lastname is Required.")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Adress is Required.")]
    public string Adress { get; set; } = string.Empty;
    [Required(ErrorMessage = "E-mail is Required.")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Phonenumber is Required.")]
    public string Phonenumber { get; set; } = string.Empty;
}
