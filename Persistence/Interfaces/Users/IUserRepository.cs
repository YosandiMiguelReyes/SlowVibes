using Domain.Entities.User;
using Domain.Repositories;
using Persistence.DTO.User;

namespace Persistence.Interfaces.Users
{
    public interface IUserRepository : IBaseRepository<Domain.Entities.User.Users, int>
    {

        /// Busca al usuario por Email o Username cargando sus Roles.
        Task<UserWithRolesDTO?> GetByCredentialsWithRolesAsync(string identifier);
        Task<AdminUserWithRole?> AdminGetByCredentialsWithRolesAsync(string identifier);


        /// Valida si el Email ya existe en la base de datos.
        Task<bool> IsEmailUniqueAsync(string email);


        /// Valida si el Username ya existe en la base de datos.
        Task<bool> IsUsernameUniqueAsync(string username);


        /// Obtiene usuarios filtrados por el nombre de su Rol.
        Task<IEnumerable<UserWithRolesDTO>> GetUsersByRoleAsync(string roleName);
        Task<IEnumerable<AdminUserWithRole>> AdminGetUsersByRoleAsync(string roleName);


        /// Cambia el estado de activación (IsActive).
        Task UpdateStatusAsync(int userId, bool isActive);

        Task<IEnumerable<UserWithRolesDTO>> GetUsersByStatusAsync(bool status);
        Task<IEnumerable<AdminUserWithRole>> AdminGetUsersByStatusAsync(bool status);

    }
}
