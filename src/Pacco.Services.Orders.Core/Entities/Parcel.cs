using System;

namespace Pacco.Services.Orders.Core.Entities;

public class Parcel : IEquatable<Parcel>
{
	public Guid Id { get; }
	public string Name { get; }
	public string Variant { get; }
	public string Size { get; }

	public Parcel(Guid id, string name, string variant, string size)
	{
		Id = id;
		Name = name;
		Variant = variant;
		Size = size;
	}

	public bool Equals(Parcel other)
	{
		if (other is null) return false;
		return ReferenceEquals(this, other) || Id.Equals(other.Id);
	}

	public override bool Equals(object obj)
	{
		if (obj is null) return false;
		if (ReferenceEquals(this, obj)) return true;
		return obj.GetType() == GetType() && Equals((Parcel)obj);
	}

	public override int GetHashCode() => Id.GetHashCode();
}
