using System;
using System.Collections.Generic;
using CS.ERP_MOB.Views.Frame;
using CS.ERP_MOB.Views.POS;

namespace CS.ERP_MOB.Route
{
    public class Pos_Route
    {
        public static Dictionary<string, Type> DicRouteList { get; private set; }
        static Pos_Route()
        {
            DicRouteList = new Dictionary<string, Type>();
            DicRouteList.Add("home", typeof(HomePage));

            //DicRouteList.Add("pos-supplier-lst", typeof(FrmPosSupplierLst));
            //DicRouteList.Add("pos-customer-lst", typeof(FrmPosCustomerLst));

            //DicRouteList.Add("pos-stock-list-lst", typeof(FrmPosStockLst));

            //DicRouteList.Add("pos-purchase-lst", typeof(FrmPosPurchaseLst));
            //DicRouteList.Add("pos-sales-lst", typeof(FrmPosSalesLst));
            //DicRouteList.Add("pos-purchase-eod-lst", typeof(FrmPosPurchaseEodLst));
            //DicRouteList.Add("pos-sales-eod-lst", typeof(FrmPosSalesEodLst));
            //DicRouteList.Add("pos-inventory-lst", typeof(FrmPosInventoryEODLst));

            //DicRouteList.Add("pos-collection-lst", typeof(FrmPosCollectionLst));                      
            //DicRouteList.Add("pos-issue-lst", typeof(FrmPosStockIssueLst));           
            //DicRouteList.Add("pos-damage-lst", typeof(FrmPosStockDamageLst));
            //DicRouteList.Add("pos-sales-order-lst", typeof(FrmPosSaleOrderLst));
            //DicRouteList.Add("pos-received-lst", typeof(FrmPosStockReceivedLst));
            //DicRouteList.Add("pos-transfer-lst", typeof(FrmPosStockTransferLst));    
                        
            //DicRouteList.Add("pos-quotation-lst", typeof(FrmPosPurchaseQuotationLst));
            //DicRouteList.Add("pos-invoice-lst", typeof(FrmPosPurchaseInvoiceLst));
            //DicRouteList.Add("pos-billing-lst", typeof(FrmPosPurchaseBillingLst));
            //DicRouteList.Add("pos-order-lst", typeof(FrmPosPurchaseOrderLst));
            //DicRouteList.Add("pos-return-lst", typeof(FrmPosPurchaseReturnLst));
            //DicRouteList.Add("pos-requestion-lst", typeof(FrmPosPurchaseRequestionLst));
            //DicRouteList.Add("pos-payment-lst", typeof(FrmPosPurchasePaymentLst));


            //DicRouteList.Add("pos-sales-enquiry-lst", typeof(FrmPosSaleEnquiryLst));
            DicRouteList.Add("pos-sales-quotation-jun-ova-lst", typeof(FrmPosSaleQuotationLst));
            DicRouteList.Add("pos-sales-invoice-jun-ova-lst", typeof(FrmPosSaleInvoiceLst));
            //DicRouteList.Add("pos-sales-billing-lst", typeof(FrmPosSaleBiilingLst));
            //DicRouteList.Add("pos-sales-payment-lst", typeof(FrmPosSalePaymentLst));
            //DicRouteList.Add("pos-sales-return-lst", typeof(FrmPosSaleReturnLst));

            //DicRouteList.Add("pos-supplier-mm-lst", typeof(FrmPosSupplierLst));         
            //DicRouteList.Add("pos-customer-mm-lst", typeof(FrmPosCustomerLst));

            //DicRouteList.Add("pos-stock-list-mm-lst", typeof(FrmPosStockLst));


            //DicRouteList.Add("pos-purchase-eod-mm-lst", typeof(FrmPosPurchaseEodLst));
            //DicRouteList.Add("pos-sales-eod-mm-lst", typeof(FrmPosSalesEodLst));
            //DicRouteList.Add("pos-inventory-mm-lst", typeof(FrmPosInventoryEODLst));

            //DicRouteList.Add("pos-collection-mm-lst", typeof(FrmPosCollectionLst));
            //DicRouteList.Add("pos-issue-mm-lst", typeof(FrmPosStockIssueLst));
            //DicRouteList.Add("pos-damage-mm-lst", typeof(FrmPosStockDamageLst));
            //DicRouteList.Add("pos-sales-order-mm-lst", typeof(FrmPosSaleOrderLst));
            //DicRouteList.Add("pos-received-mm-lst", typeof(FrmPosStockReceivedLst));
            //DicRouteList.Add("pos-transfer-mm-lst", typeof(FrmPosStockTransferLst));

            //DicRouteList.Add("pos-quotation-mm-lst", typeof(FrmPosPurchaseQuotationLst));
            //DicRouteList.Add("pos-invoice-mm-lst", typeof(FrmPosPurchaseInvoiceLst));
            //DicRouteList.Add("pos-billing-mm-lst", typeof(FrmPosPurchaseBillingLst));
            //DicRouteList.Add("pos-order-mm-lst", typeof(FrmPosPurchaseOrderLst));
            //DicRouteList.Add("pos-return-mm-lst", typeof(FrmPosPurchaseReturnLst));
            //DicRouteList.Add("pos-requestion-mm-lst", typeof(FrmPosPurchaseRequestionLst));
            //DicRouteList.Add("pos-payment-mm-lst", typeof(FrmPosPurchasePaymentLst));


            //DicRouteList.Add("pos-sales-enquiry-mm-lst", typeof(FrmPosSaleEnquiryLst));
            //DicRouteList.Add("pos-sale-quotation-mm-lst", typeof(FrmPosSaleQuotationLst));
            //DicRouteList.Add("pos-sales-invoice-mm-lst", typeof(FrmPosSaleInvoiceLst));
            //DicRouteList.Add("pos-sales-billing-mm-lst", typeof(FrmPosSaleBiilingLst));
            //DicRouteList.Add("pos-sales-payment-mm-lst", typeof(FrmPosSalePaymentLst));
            //DicRouteList.Add("pos-sales-return-mm-lst", typeof(FrmPosSaleReturnLst));

        }
    }
}
