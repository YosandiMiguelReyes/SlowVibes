using Domain.Base;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Domain.Entities.Category;

public class Categories : BaseEntity<int>, IIsActive
{
    public string Name { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    private Categories() { }

    private Categories(string name)
    {
        Name = name;
        IsActive = true;
    }

    public static Categories Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(
                "Category name is required.");

        return new Categories(name.Trim());
    }

    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(
                "Category name is required.");

        Name = name.Trim();
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}