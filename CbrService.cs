using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CurrencyConverter 
{
    public class CbrService
    {
        private readonly HttpClient _httpClient;

        public CbrService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Get latest currency rates from CBR (to RUB)
        /// </summary>
        /// <returns></returns>
        public async Task<List<CurrencyRateModel>> GetCurrencyRate()
        {
            var todayString = DateTime.Now.ToString("dd/MM/yyyy");
            var bytes = await _httpClient.GetByteArrayAsync($"http://www.cbr.ru/scripts/XML_daily.asp?date_req={todayString}");

            Encoding encoding = Encoding.GetEncoding("windows-1251");
            string responseString = encoding.GetString(bytes, 0, bytes.Length);

            var cbrRates = new List<CurrencyRateModel>();

            XmlSerializer serializer = new XmlSerializer(typeof(SingleDateCurrencyXMLModel));
            using (TextReader reader = new StringReader(responseString))
            {
                var parseResult = (SingleDateCurrencyXMLModel)serializer.Deserialize(reader);

                foreach (var rate in parseResult.Rates)
                {
                    var item = new CurrencyRateModel();
                    item.Name = rate.Name;
                    item.Code = rate.Code;
                    item.Rate = decimal.Parse(rate.Value.Replace(",", "."), CultureInfo.InvariantCulture);
                    cbrRates.Add(item);
                }
            }

            return cbrRates;
        }
    }

    /// <summary>
    /// Latest currency rate from CBR
    /// </summary>
    public class CurrencyRateModel
    {
        /// <summary>
        /// Full currency name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Currency code, for ex. USD, EUR, etc.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Currency Rate to RUB
        /// </summary>
        public decimal Rate { get; set; }
    }

    [XmlRoot("ValCurs")]
    public class SingleDateCurrencyXMLModel
    {
        [XmlElement("Valute")]
        public List<SingleDateCurrencyXMLItemModel> Rates { get; set; }
    }

    public class SingleDateCurrencyXMLItemModel
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("CharCode")]
        public string Code { get; set; }

        [XmlElement("Value")]
        public string Value { get; set; }
    }
}
