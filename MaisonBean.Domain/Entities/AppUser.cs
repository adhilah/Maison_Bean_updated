//using Microsoft.AspNetCore.Identity;

//namespace MaisonBean.Domain.Entities;

//public class AppUser : IdentityUser<int>
//{
//    public string FirstName { get; set; } = string.Empty;
//    public string LastName { get; set; } = string.Empty;

//    public int TokenVersion { get; set; }

//    public string? RefreshToken { get; set; }
//    public DateTime RefreshTokenExpiry { get; set; }

//    public bool IsBlocked { get; private set; }

//    public void ToggleBlock()
//    {
//        IsBlocked = !IsBlocked;
//    }
//}



using Microsoft.AspNetCore.Identity;

namespace MaisonBean.Domain.Entities;

public class AppUser
    : IdentityUser<int>
{
    public string FirstName
    { get; set; }
        = string.Empty;

    public string LastName
    { get; set; }
        = string.Empty;

    public int TokenVersion
    { get; set; }

    public string? RefreshToken
    { get; set; }

    public DateTime RefreshTokenExpiry
    { get; set; }

    public bool IsBlocked
    { get; private set; }

    // =====================================
    // NAVIGATION PROPERTIES
    // =====================================

    public ICollection<CartItem> CartItems
    { get; set; }
        = new List<CartItem>();

    public ICollection<WishlistItem> WishlistItems
    { get; set; }
        = new List<WishlistItem>();

    public ICollection<Address> Addresses
    { get; set; }
        = new List<Address>();

    // =====================================
    // METHODS
    // =====================================

    public void ToggleBlock()
    {
        IsBlocked = !IsBlocked;
    }
}