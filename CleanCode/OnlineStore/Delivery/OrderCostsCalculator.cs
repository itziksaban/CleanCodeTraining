using System;

namespace OnlineStore.Reservations;

public class OrderCostsCalculator
{
    private ProductRepository _productRepository;
    private DistanceCalculator _distanceCalculator;

    private const double REST_KG_COST = 100;

    private const double FIRST_5KG_COST = 200;

    private const int FREGILE_COST = 117;

    private const int NONE_FREGILE_COST = 99;

    private const double TRANSFER_FEE = 23.3;

    public OrderCostsCalculator(ProductRepository productRepository, DistanceCalculator distanceCalculator)
    {
        _productRepository = productRepository;
        _distanceCalculator = distanceCalculator;
    }


    public double CalcDeliveryCost(DeliverableOrder order)
    {
        var distance = _distanceCalculator.Calc(order.Destination);
        double totalCost = CalcPricePerKM(order);
        return totalCost * distance;
    }

    public double CalcTotalCost(Order order)
    {
        if (order.OrderType == OrderType.SelfCollected)
        {
            return CalcProductsCost(order) * 0.5;
        }
        else
        {
            var deliverableOrder = (DeliverableOrder) order;
            if (deliverableOrder.DeliveryType == DeliveryType.Postal)
            {
                return CalcProductsCost(deliverableOrder) + CalcDeliveryCost(deliverableOrder) + TRANSFER_FEE;
            }
            else
            {
                return CalcProductsCost(deliverableOrder) + CalcDeliveryCost(deliverableOrder);
            }
        }
    }

    private double CalcProductsCost(Order order)
    {
        double productsCost = 0;
        foreach (var orderLine in order.OrderLines)
        {
            productsCost += _productRepository.GetProduct(orderLine.ProductId).Price * orderLine.Amount;
        }

        return productsCost;
    }

    private double CalcPricePerKM(DeliverableOrder order)
    {
        double totalCost = 0;
        if (order.DeliveryType == DeliveryType.Postal)
        {
            double totalWeight = 0;
            foreach (var orderLine in order.OrderLines)
            {
                totalWeight += _productRepository.GetProduct(orderLine.ProductId).Weight * 
                               orderLine.Amount;
            }

            totalCost = (totalWeight < 5
                ? totalWeight * FIRST_5KG_COST
                : 5 * FIRST_5KG_COST + REST_KG_COST * (totalWeight - 5));
        }
        else
        {
            foreach (var orderLine in order.OrderLines)
            {
                var product = _productRepository.GetProduct(orderLine.ProductId);
                if (product.IsFregile)
                {
                    totalCost += product.Weight * FREGILE_COST;
                }
                else
                {
                    totalCost += product.Weight * NONE_FREGILE_COST;
                }
            }
        }

        return totalCost;
    }
}