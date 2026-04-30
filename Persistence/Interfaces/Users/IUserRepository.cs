using Domain.Entities.User;
using Domain.Repositories;

namespace Persistence.Interfaces.Users
{
    public interface IUserRepository : IBaseRepository<Domain.Entities.User.Users, int>
    {
        /// <summary>
        /// UC-01: Busca al usuario por Email o Username cargando sus Roles.
        /// Esto permite que el login sea flexible (el usuario elige qué usar).
        /// </summary>
        Task<Domain.Entities.User.Users?> GetByCredentialsWithRolesAsync(string identifier);

        /// <summary>
        /// Valida si el Email ya existe en la base de datos.
        /// </summary>
        Task<bool> IsEmailUniqueAsync(string email);

        /// <summary>
        /// Valida si el Username ya existe en la base de datos.
        /// </summary>
        Task<bool> IsUsernameUniqueAsync(string username);

        /// <summary>
        /// Obtiene usuarios filtrados por el nombre de su Rol.
        /// </summary>
        Task<IEnumerable<Domain.Entities.User.Users>> GetUsersByRoleAsync(string roleName);

        /// <summary>
        /// Cambia el estado de activación (IsActive).
        /// </summary>
        Task UpdateStatusAsync(int userId, bool isActive);

        Task<IEnumerable<Domain.Entities.User.Users>> GetUsersByStatusAsync(bool status);
    }
}
