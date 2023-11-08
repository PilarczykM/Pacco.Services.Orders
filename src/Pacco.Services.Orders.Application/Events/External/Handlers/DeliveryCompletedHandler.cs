using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Convey.CQRS.Events;

using Pacco.Services.Orders.Application.Exceptions;
using Pacco.Services.Orders.Application.Services;
using Pacco.Services.Orders.Core.Repositories;

namespace Pacco.Services.Orders.Application.Events.External.Handlers;

public class DeliveryCompletedHandler : IEventHandler<DeliveryCompleted>
{
	private readonly IOrderRepository _orderRepository;
	private readonly IMessageBroker _messageBroker;
	private readonly IEventMapper _eventMapper;

	public DeliveryCompletedHandler(IOrderRepository orderRepository, IMessageBroker messageBroker,
		IEventMapper eventMapper)
	{
		_orderRepository = orderRepository;
		_messageBroker = messageBroker;
		_eventMapper = eventMapper;
	}

	public async Task HandleAsync(DeliveryCompleted @event, CancellationToken cancellationToken = default)
	{
		var order = await _orderRepository.GetAsync(@event.OrderId)
					?? throw new OrderNotFoundException(@event.OrderId);

		order.Complete();
		await _orderRepository.UpdateAsync(order);
		var events = _eventMapper.MapAll(order.Events);
		await _messageBroker.PublishAsync(events.ToArray());
	}
}
