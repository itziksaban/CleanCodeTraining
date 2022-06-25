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


    public double CalcDeliveryCost(dynamic order)
    {
        var distance = _distanceCalculator.Calc(order.Provisioning.Delivery.City);
        double totalCost = CalcPricePerKM(order);
        return totalCost * distance;
    }

    private double CalcPricePerKM(dynamic order)
    {
        double totalCost = 0;
        if (order.Provisioning.Type == ProvisioningType.Postal)
        {
            double totalWeight = 0;
            foreach (var orderLine in order.OrderLines)
            {
                totalWeight += _productRepository.GetProduct(orderLine.ProductId).Weight * orderLine.Amount;
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

    public double CalcTotalCost(dynamic order)
    {
        if (order.Provisioning.Type == ProvisioningType.SelfCollection)
        {
            return CalcProductsCost(order) * 0.5;
        }
        else if (order.Provisioning.Type == ProvisioningType.Postal)
        {
            return CalcProductsCost(order) + CalcDeliveryCost(order) + TRANSFER_FEE;
        }
        else
        {
            return CalcProductsCost(order) + CalcDeliveryCost(order);
        }
    }

    private double CalcProductsCost(dynamic order)
    {
        double productsCost = 0;
        foreach (var orderLine in order.OrderLines)
        {
            productsCost += _productRepository.GetProduct(orderLine.ProductId).Price * orderLine.Amount;
        }

        return productsCost;
    }
}