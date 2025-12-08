using System;
namespace CS.ERP_MOB.Services.POS
{
    public class Pos_Name
    {
        public Pos_Name()
        {
        }
        public static string wssaveShopping = "saveShopping";
        #region "List"
        public static string wsgetSalePaymentJun = "getSalePaymentJun";
        public static string wsgetCollection = "getCollection";
        public static string wsgetCustomerAndContact = "getCustomerNContact";
        public static string wsgetSupplierAndContact = "getSupplierNContact";
        public static string wsgetPurchaseSettlementDetail = "getPurchaseSettlementDetail";
        public static string wsgetPurchaseSettlement = "getPurchaseSettlement";
        public static string wsgetSaleSettlement = "getSalesSettlement";
        public static string wsgetInventryReceived = "getInventryReceived";        
        public static string wsgetPurchaseOrder = "getPurchaseOrder";
        public static string wsgetPurchaseQuotation = "getPurchaseQuotation";
        public static string wsgetPurchaseBillingJun = "getPurchaseBillJun";
        public static string wsgetPurchaseReturn = "getPurchaseReturn";
        public static string wsgetPurchaseRequestion = "getPurchaseRequestion";
        public static string wsgetPurchaseInvoice = "getPurchaseInvoice";
        public static string wsgetPurchasePaymentJun = "getPurchasePayJun";      
        public static string wsgetSaleInvoice = "getSaleInvoice";
        public static string wsgetSaleInvoiceJun = "getSaleInvoiceJun";
        public static string wsgetSaleEnquiry = "getSaleEnquiry";
        public static string wsgetSaleOrder = "getSaleOrder";
        public static string wsgetSaleOrderJun = "getSaleOrderJun";
        public static string wsgetSaleBilling = "getSaleBilling";
        public static string wsgetSaleBillingJun = "getSaleBillJun";
        public static string wsgetSaleQuotation = "getSaleQuotation";
        public static string wsgetSaleQuotationJun = "getSaleQuotationJun";
        public static string wsgetSaleReturn = "getSaleReturn";
        public static string wsgetSaleReturnJun = "getSaleReturnJun";
        public static string wsgetInventryIssueJun = "getInventoryIssueJun";
        public static string wsgetInventryTransfer = "getInventryTransfer";
        public static string wsgetInventryDamage = "getInventryDamage";
        public static string wsgetInventoryReceivedJun = "getInventoryReceivedJun";
        public static string wsgetInventoryStock = "getInventryStock";
        public static string wsgetInventorySettlement = "getInventorySettlement";
        public static string wsloadSaleTransHis = "loadSaleTranHis";
        public static string wsgetSaleBillJun = "getSaleBillJun";
        public static string wsloadSalePayHis = "loadSalePayHis";
        public static string wsloadSalePayment = "loadSalePayment";

        #endregion
        #region "Save"
        public static string wssaveCollection = "saveCollection";
        public static string wssaveCustomerAndContact = "saveCustomernContact";
        public static string wssaveCustomer = "saveCustomer";
        public static string wssaveCustomerContact = "saveCustomerContact";
        public static string wsconfirmCustomernContact = "confirmCustomernContact";
        public static string wssaveSupplierAndContact = "saveSuppliernContact";
        public static string wssaveSupplierContact = "saveSupplierContact";
        public static string wsconfirmSuppliernContact = "confirmSupplierContact";
        public static string wssaveCustomerUser = "saveCustomerUser";
        public static string wssaveSupplierUser = "saveSupplierUser";
        public static string wssavePurchaseSettlement = "savePurchaseSettlement";
        public static string wssaveSaleSettlement = "saveSalesSettlement";
        public static string wssaveInventryReceived = "saveInventryReceived";
        public static string wssaveInventryReceivedJun = "saveInventoryReceivedJun";
        public static string wsBrowseInventryReceivedJun = "browseInventoryReceivedJun";
        public static string wssavePurchaseOrder = "savePurchaseOrder";
        public static string wssavePurchaseOrderJun = "savePurchaseOrderJun";
        public static string wssavePurchaseQuotation = "savePurchaseQuotation";
        public static string wssavePurchaseQuotationJun = "savePurchaseQuotationJun";       
        public static string wssavePurchaseBillingJun = "savePurchaseBillJun";
        public static string wssavePurchaseReturn = "savePurchaseReturn";
        public static string wssavePurchaseReturnJun = "savePurchaseReturnJun";
        public static string wssavePurchaseReturnPay = "savePurchaseReturnPay";
        public static string wssavePurchaseRequestion = "savePurchaseRequestion";
        public static string wsSavePurchaseInvoice = "savePurchaseInvoice";
        public static string wssavePurchaseInvoiceJun = "savePurchaseInvoiceJun";
        public static string wssavePurchasePaymentJun = "savePurchaseInvoiceJun";
        public static string wsbrowsePurchasePayJun = "savePurchaseInvoiceJun";
        public static string wssaveSaleInvoice = "saveSaleInvoice";
        public static string wssaveSaleInvoiceJun = "saveSaleInvoiceJun";
        public static string wsSaveSaleEnquiry = "saveSaleEnquiry";
        public static string wssaveSaleOrderJun = "saveSaleOrderJun";
        public static string wssaveSaleBilling = "saveSaleBilling";
        public static string wssaveSaleBillingJun = "saveSaleBillJun";
        public static string wssaveSaleQuotation = "saveSaleQuotation";
        public static string wssaveSaleQuotationJun = "saveSaleQuotationJun";
        public static string wssaveSaleReturn = "saveSaleReturn";
        public static string wssaveSaleReturnPay = "saveSaleReturnPay";
        public static string wssaveSaleReturnJun = "saveSaleReturnJun";
        public static string wssaveInventryIssueJun = "saveInventoryIssueJun";
        public static string wsbrowseInventryIssueJun = "browseInventoryIssueJun";
        public static string wssaveInventryTransfer = "saveInventryTransfer";
        public static string wssaveInventryDamage = "saveInventryDamage";
       

        #endregion
        #region "Load"

        public static string wsLoadCollection = "loadCollection";
        public static string wsLoadCustomer = "loadCustomer";
        public static string wsLoadSupplier = "loadSupplier";
        public static string wsLoadPurchaseSettlement = "loadPurchaseSettlement";      
        public static string wsLoadSaleSettlement = "loadSalesSettlement";
        public static string wsLoadInventryReceived = "loadInventryReceived";
        public static string wsLoadPurchaseOrder = "loadPurchaseOrder";
        public static string wsLoadPurchaseQuotation = "loadPurchaseQuotation";
        public static string wsLoadPurchaseBilling = "loadPurchaseBilling";
        public static string wsLoadPurchaseReturn = "loadPurchaseReturn";
        public static string wsLoadPurchaseRequestion = "loadPurchaseRequestion";
        public static string wsLoadPurchaseInvoice = "loadPurchaseInvoice";
        public static string wsLoadPurchasePayment = "loadPurchasePayment";
        public static string wsLoadSaleInvoice = "loadSaleInvoice";
        public static string wsLoadSaleEnquiry = "loadSaleEnquiry";
        public static string wsLoadSaleOrder = "loadSaleOrder";
        public static string wsLoadSaleBilling = "loadSaleBilling";
        public static string wsLoadSaleQuotation = "loadSaleQuotation";
        public static string wsLoadSaleReturn = "loadSaleReturn";
        public static string wsLoadInventryTransfer = "loadInventryTransfer";
        public static string wsLoadInventryDamage = "loadInventryDamage";
        public static string wsLoadInventryStock = "loadInventryStock";
        public static string wsLoadInventryIssue = "loadInventryIssue";
        #endregion

    }
}
