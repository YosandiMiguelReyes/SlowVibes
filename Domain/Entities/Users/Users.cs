using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities.User
{
    public class Users :BaseEntity<int>, IIsActive, ICreatedAt
    {
        //chat gpt read... Como no tengo una propiedad para Rol lo mas conveniente
        //es que la otra capa se encargue de asignar el rol al usuario, ya que en la capa de dominio no se tiene conocimiento de los roles y permisos, por lo que no es necesario tener una propiedad para Rol en la entidad Users.
        //Por que nuestro metodo crear es static?
        //Siento que olvido unas validaciones de los campos nullable, osea si no tiene nada que sea null
        public string FullName { get; private set; } //max length 150
        public string? UserName { get; private set; } //max length 50
        public string Email { get; private set; } //max length 150 
        public string? Phone { get; private set; } //max length 20
        public string PasswordHash { get; private set; } //max length 255


        public bool IsActive { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }

        private Users() { }

        private Users(string fullName, string? userName, string email, string? phone, string passwordHash)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            Phone = phone;
            PasswordHash = passwordHash;
            IsActive = true;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public static Users Create(string fullName, string? userName, string email, string? phone, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("El nombre completo es obligatorio.");

            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("El correo electrónico es obligatorio.");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("La contraseña es obligatoria.");

            return new Users(fullName.Trim(), string.IsNullOrWhiteSpace(userName) ? null : userName.Trim(), email.Trim(), string.IsNullOrWhiteSpace(phone) ? null : phone.Trim(), passwordHash);
        }
        public void Activate()
        {
            if (IsActive)
                throw new DomainException("El usuario ya está activo.");

            IsActive = true;
        }
        public void Deactivate()
        {
            if (!IsActive)
                throw new DomainException("El usuario ya está desactivado.");

            IsActive = false;
        }

        public void UpdateName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                throw new DomainException("El nombre es obligatorio.");

            FullName = fullName.Trim();
        }
        public void UpdateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException("El correo electrónico es obligatorio.");

            Email = email.Trim();
        }
        public void UpdatePassword(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new DomainException("La contraseña es obligatoria.");

            PasswordHash = passwordHash;
        }
        public void UpdateUserName(string? userName)
        {
            UserName = string.IsNullOrWhiteSpace(userName)
                ? null
                : userName.Trim();
        }

        public void UpdatePhone(string? phone)
        {
            Phone = string.IsNullOrWhiteSpace(phone)
                ? null
                : phone.Trim();
        }


    }
}
