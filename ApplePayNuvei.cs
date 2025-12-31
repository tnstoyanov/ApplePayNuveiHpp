// This is an HTTPS request generator based on Nuvei's Web Cashier API.
// The flow goes like your end --> Nuvei's Web Cashier (proxy API) --> PSP --> your end (response URLs)
// This API contains async callbacks

using System.Security.Cryptography;
using System.Text;
using System.Web;

// Configuration properties
string endpoint = "https://ppp-test.safecharge.com/ppp/purchase.do?";
string merchant_id = "3832456837996201334";
string merchant_site_id = "184063";
string merchantSecretKey = "puT8KQYqIbbQDHN5cQNAlYyuDedZxRYjA9WmEsKq1wrIPhxQqOx77Ep1uOA7sUde";
// End of configuration properties

// Request parameters
string time_stamp = DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss");
string currency = "USD";
string merchantLocale = "en_US";
string userid = "1234567";
string merchant_unique_id = DateTime.Now.ToString("yyyyMMddHHmmss") + "7654321";
string item_name_1 = "cashierTestOld";
string item_number_1 = "1";
double item_amount_1 = 0.10;
int item_quantity_1 = 1;
double total_amount = item_amount_1;
string user_token_id = "7654321";
string first_name = "Tony";
string last_name = "Stoyanov";
string email = "tony.stoyanov@tiebreak.dev";
string address1 = "3 Nikola Tesla St";
string city = "Sofia";
string country = "BG";
string zip = "0.10";
string phone1 = "359888123456";
string version = "4.0.0";
// This calls Apple Pay up as your only payment method on Nuvei's cashier
string payment_method = "ppp_ApplePay";
// This hides all the other payment methods on Nuvei's cashier
string payment_method_mode = "filter";
// PROfit callback handler. In PROfit, set it to your Billing Server callback listener
string notify_url = "https://34028ab3c57f9867b19b64a815e6a373.m.pipedream.net";
// PROfit Deposit site response URLs
// approved tx
string success_url = "https://tnstoyanov.wixsite.com/payment-response/success";
// failed tx
string error_url = "https://tnstoyanov.wixsite.com/payment-response/failed";
// no status
string pending_url = "https://tnstoyanov.wixsite.com/payment-response/pending";
// if you close Nuvei's cashier or cancel the payment
string back_url = "https://tnstoyanov.wixsite.com/payment-response/cancel";
// Optional: your Nuvei cashier theme ID
// int theme_id = 146382; 

string checksum = ComputeSha256Hash(
    merchantSecretKey
    + merchant_id
    + merchant_site_id
    + time_stamp
    + currency
    + merchantLocale
    + userid
    + merchant_unique_id
    + item_name_1
    + item_number_1
    + item_amount_1.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
    + item_quantity_1
    + total_amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)
    + user_token_id
    + first_name
    + last_name
    + email
    + city
    + country
    + address1
    + zip
    + phone1
    + version
    + payment_method
    + payment_method_mode
    + notify_url
    + success_url
    + error_url
    + pending_url
    + back_url
   // + theme_id.ToString()
    );

// This calculates your checksum
string ComputeSha256Hash(string rawData)
{
    // Creates a SHA256   (Works the same way for SHA256)
    using (SHA256 sha256Hash = SHA256.Create())
    {
        // ComputeHash - returns byte array  
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

        // Converts byte array to a string   
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            //x2 for lowercase chars, X2 for uppercase chars
            builder.Append(bytes[i].ToString("x2"));
        }
        return builder.ToString();
    }
}

//Build HTTPS request
Console.WriteLine(
    endpoint
    + "merchant_id="
    + HttpUtility.UrlEncode(merchant_id)
    + "&merchant_site_id="
    + HttpUtility.UrlEncode(merchant_site_id)
    + "&time_stamp="
    + HttpUtility.UrlEncode(time_stamp)
    + "&currency="
    + HttpUtility.UrlEncode(currency)
    + "&merchantLocale="
    + HttpUtility.UrlEncode(merchantLocale)
    + "&userid="
    + HttpUtility.UrlEncode(userid)
    + "&merchant_unique_id="
    + HttpUtility.UrlEncode(merchant_unique_id)
    + "&item_name_1="
    + HttpUtility.UrlEncode(item_name_1)
    + "&item_number_1="
    + HttpUtility.UrlEncode(item_number_1)
    + "&item_amount_1="
    + HttpUtility.UrlEncode(item_amount_1.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
    + "&item_quantity_1="
    + HttpUtility.UrlEncode(item_quantity_1.ToString())
    + "&total_amount="
    + HttpUtility.UrlEncode(total_amount.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture))
    + "&user_token_id="
    + HttpUtility.UrlEncode(user_token_id)
    + "&first_name="
    + HttpUtility.UrlEncode(first_name)
    + "&last_name="
    + HttpUtility.UrlEncode(last_name)
    + "&email="
    + HttpUtility.UrlEncode(email)
    + "&city="
    + HttpUtility.UrlEncode(city)
    + "&country="
    + HttpUtility.UrlEncode(country)
    + "&address1="
    + HttpUtility.UrlEncode(address1)
    + "&zip="
    + HttpUtility.UrlEncode(zip)
    + "&phone1="
    + HttpUtility.UrlEncode(phone1)
    + "&version="
    + HttpUtility.UrlEncode(version)
    + "&payment_method="
    + HttpUtility.UrlEncode(payment_method)
    + "&payment_method_mode="
    + HttpUtility.UrlEncode(payment_method_mode)
    + "&notify_url="
    + HttpUtility.UrlEncode(notify_url)
    + "&success_url="
    + HttpUtility.UrlEncode(success_url)
    + "&error_url="
    + HttpUtility.UrlEncode(error_url)
    + "&pending_url="
    + HttpUtility.UrlEncode(pending_url)
    + "&back_url="
    + HttpUtility.UrlEncode(back_url)
   // + "&theme_id="
   // + HttpUtility.UrlEncode(theme_id.ToString())
    + "&checksum="
    + HttpUtility.UrlEncode(checksum)
    );

// This keeps your console window open
Console.ReadLine();