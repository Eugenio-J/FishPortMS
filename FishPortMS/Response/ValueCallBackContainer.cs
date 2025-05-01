namespace FishPortMS.Client.Response
{
    public class ValueCallBackContainer<T>
    {
        public T? POValue { get; set; }
        public Action<T>? POCallBack { get; set; }

        public T? PaymentValue { get; set; }
        public Action<T>? PaymentCallBack { get; set; }

        public T? DeliveryValue { get; set; }
        public Action<T>? DeliveryCallBack { get; set; }

        public T? ExpenseValue { get; set; }
        public Action<T>? ExpenseCallBack { get; set; }
    }
}
