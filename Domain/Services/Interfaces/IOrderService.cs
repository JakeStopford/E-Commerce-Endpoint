using Domain.Models;

namespace Domain.Services.Interfaces;

public interface IOrderService
{
    Task<int> SaveOrder(OrderToSaveDto order);
}