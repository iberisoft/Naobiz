using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Naobiz.Services
{
    public class PaypalService
    {
        readonly PayPalHttpClient m_PayPalHttpClient;
        readonly ApplicationContext m_ApplicationContext;
        readonly decimal m_TaxRate;

        public PaypalService(Settings settings)
        {
            var environment = new LiveEnvironment(settings.Paypal.ClientId, settings.Paypal.ClientSecret);
            m_PayPalHttpClient = new PayPalHttpClient(environment);
            m_ApplicationContext = new ApplicationContext
            {
                BrandName = "Naobiz",
                ReturnUrl = settings.SiteUrl + "_complete-order",
                CancelUrl = settings.SiteUrl + "_cancel-order"
            };
            m_TaxRate = settings.TaxRate;
        }

        public async Task<string> CreateOrder(Data.Order order)
        {
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(CreateOrderRequest(order));
            var response = await m_PayPalHttpClient.Execute(request);
            var order2 = response.Result<Order>();
            order.PaypalToken = order2.Id;
            var link = order2.Links.Single(link => link.Rel == "approve");
            return link.Href;
        }

        private OrderRequest CreateOrderRequest(Data.Order order)
        {
            var request = new OrderRequest
            {
                ApplicationContext = m_ApplicationContext,
                CheckoutPaymentIntent = "CAPTURE"
            };
            var purchaseUnitRequest = new PurchaseUnitRequest
            {
                InvoiceId = order.InvoicelId,
                AmountWithBreakdown = new AmountWithBreakdown
                {
                    Value = order.Value.ToString("f2", CultureInfo.InvariantCulture),
                    CurrencyCode = "EUR",
                    AmountBreakdown = new AmountBreakdown
                    {
                        ItemTotal = new Money
                        {
                            Value = order.ValueWithoutTax(m_TaxRate).ToString("f2", CultureInfo.InvariantCulture),
                            CurrencyCode = "EUR"
                        },
                        TaxTotal = new Money
                        {
                            Value = order.TaxValue(m_TaxRate).ToString("f2", CultureInfo.InvariantCulture),
                            CurrencyCode = "EUR"
                        }
                    }
                }
            };
            request.PurchaseUnits = new List<PurchaseUnitRequest>() { purchaseUnitRequest };
            return request;
        }

        public async Task<bool> CaptureOrder(string token)
        {
            var request = new OrdersCaptureRequest(token);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());
            var response = await m_PayPalHttpClient.Execute(request);
            var order2 = response.Result<Order>();
            return order2.Status == "COMPLETED";
        }
    }
}
