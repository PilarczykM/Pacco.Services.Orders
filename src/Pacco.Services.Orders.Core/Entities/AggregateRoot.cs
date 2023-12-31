using System.Collections.Generic;

namespace Pacco.Services.Orders.Core.Entities;

public abstract class AggregateRoot
{
	private readonly List<IDomainEvent> _events = new();
	public IEnumerable<IDomainEvent> Events => _events;
	public AggregateId Id { get; protected set; }
	public int Version { get; protected set; }

	protected void AddEvent(IDomainEvent @event)
	{
		_events.Add(@event);
	}

	public void ClearEvents() => _events.Clear();
}
