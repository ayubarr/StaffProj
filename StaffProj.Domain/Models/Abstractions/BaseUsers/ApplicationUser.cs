using Microsoft.AspNetCore.Identity;

namespace StaffProj.Domain.Models.Abstractions.BaseUsers
{
    /// <summary>
    /// Custom application user class derived from IdentityUser.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the refresh token used to obtain new access tokens when the original token expires.
        /// </summary>
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Gets or sets the expiry time of the refresh token.
        /// </summary>
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
