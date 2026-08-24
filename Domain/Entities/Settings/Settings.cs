using Domain.Base;
using Domain.Exceptions;

namespace Domain.Entities.Settings
{
    public class Settings : BaseEntity<int>
    {
        public string Key { get; private set; } = string.Empty;

        public string Value { get; private set; } = string.Empty;

        // EF Core
        private Settings() { }

        private Settings(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public static Settings Create(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new DomainException(
                    "La clave de configuración es obligatoria.");

            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException(
                    "El valor de configuración es obligatorio.");

            return new Settings(
                key.Trim(),
                value.Trim());
        }

        public void UpdateValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException(
                    "El valor de configuración es obligatorio.");

            Value = value.Trim();
        }
    }
}